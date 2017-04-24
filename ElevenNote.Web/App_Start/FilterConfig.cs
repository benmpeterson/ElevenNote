using System.Web.Mvc;

namespace ElevenNote.Web
{
    //Handles request filters. Auri has never written a filter. 
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
