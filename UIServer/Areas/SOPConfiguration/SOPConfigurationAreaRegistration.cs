using System.Web.Mvc;

namespace UIServer.Areas.SOPConfiguration
{
    public class SOPConfigurationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SOPConfiguration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SOPConfiguration_default",
                "SOPConfiguration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}