using System.Collections.Generic;
using QrCodeWebApi.Model;

namespace QrCodeWebApi
{
    public interface IUrlItemsRepository
    {
        List<UrlItem> GetAll();
        UrlItem GetByTitle(string title);
    }
}
