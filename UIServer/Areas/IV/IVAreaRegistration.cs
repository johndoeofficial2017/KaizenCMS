using System.Web.Mvc;

namespace UIServer.Areas.IV
{
    public class IVAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "IV";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "IV_default",
                "IV/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}