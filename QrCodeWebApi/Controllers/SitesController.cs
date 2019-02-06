using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using QrCodeWebApi.Model;

namespace QrCodeWebApi.Controllers
{
    public class SitesController : ApiController
    {
        private IUrlItemsRepository _repo;

        public SitesController(IUrlItemsRepository repo)
        {
            _repo = repo;
        }

        // example GET: api/Sites
        public IEnumerable<UrlItem> Get()
        {
            return _repo.GetAll();
        }

        //// example GET: api/Sites/Vodatone%20RO
        //[Route("api/sites/{title}")]
        //public UrlItem Get([FromUri] string title)
        //{
        //    var item = _repo.GetByTitle(title);

        //    return item ?? UrlItem.NotAvailable;
        //}

        [HttpGet]
        [Route("api/sites/{title}")]
        public async Task<HttpResponseMessage> Get([FromUri] string title)
        {
            var item = _repo.GetByTitle(title);
            if (item != null)
            {
                string uri = $"http://chart.apis.google.com/chart?chs=200x200&cht=qr&chld=|1&chl={item.Url}";
                HttpResponseMessage resp = await Utils.GetGoogleQRImage(uri);
                return resp;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"{title} was not found");
            }
        }
    }
}
