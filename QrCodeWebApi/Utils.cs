using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Schema;
using Newtonsoft.Json;
using QrCodeWebApi.Model;

namespace QrCodeWebApi
{
    public static class Utils
    {
        public static List<UrlItem> GetHardcodedSiteList()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("urls.json"));

            string jsonContent = String.Empty;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonContent = reader.ReadToEnd();
                }
            }

            var items = JsonConvert.DeserializeObject<List<UrlItem>>(jsonContent);
            return items;
        }

        private static async Task<byte[]> GetBinaryContent(string uri)
        {
            byte[] result = new byte[0];

            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.Timeout = 2000;


                using (HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync())
                using (var stream = response.GetResponseStream())
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    result = reader.ReadBytes(500_000);
                    //var str = System.Text.Encoding.Default.GetString(result);
                    //Console.WriteLine(str);
                }
            }
            catch
            {
                result = new byte[0];
            }
            return result;
        }
        public static async Task<HttpResponseMessage> GetImageResponse(string uri)
        {
            var imgData = await GetBinaryContent(uri);
            if (imgData.Length == 0)
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadGateway};

            MemoryStream ms = new MemoryStream(imgData);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            // ms.Dispose();

            return response;
        }

        public static async Task<HttpResponseMessage> GetGoogleQRImage(string QRdata)
        {
            try
            {
                string uri = $"http://chart.apis.google.com/chart?chs=200x200&cht=qr&chld=|1&chl={QRdata}";
                HttpResponseMessage resp = await Utils.GetImageResponse(uri);
                return resp;
            }
            catch
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadGateway
                };
            }
        }
    }
}