using System.Web;
using System.Web.Mvc;

namespace musteri_Kart_İliskisi.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
