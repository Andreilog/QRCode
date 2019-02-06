namespace QrCodeWebApi.Model
{
    public class UrlItem
    {
        public static UrlItem NotAvailable = new UrlItem("Not found", "wwww.empty.ro");

        protected UrlItem() { }

        public UrlItem(string title, string url) : this()
        {
            Title = title;
            Url = url;
        }

        public string Title { get; protected set; }

        public string Url { get; protected set; }
    }
}
