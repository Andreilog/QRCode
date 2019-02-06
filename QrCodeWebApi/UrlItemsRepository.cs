using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QrCodeWebApi.Model;

namespace QrCodeWebApi
{
    public class UrlItemsRepository : IUrlItemsRepository
    {
        private List<UrlItem> _siteListCache;

        public List<UrlItem> GetAll()
        {
            if (_siteListCache == null)
            {
                var items = Utils.GetHardcodedSiteList();
                _siteListCache = items;
            }
            return _siteListCache;
        } 

        public UrlItem GetByTitle(string title)
        {
            return GetAll().SingleOrDefault(site =>
                string.Compare(site.Title, title, StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}
