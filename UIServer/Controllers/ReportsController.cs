using System.Web;
using Telerik.Reporting.Cache.Interfaces;
using Telerik.Reporting.Services.Engine;
using Telerik.Reporting.Services.WebApi;


namespace UIServer.Controllers
{
    public class ReportsController : ReportsControllerBase
    {
        // GET: Reports
        static Telerik.Reporting.Services.ReportServiceConfiguration configurationInstance =
        new Telerik.Reporting.Services.ReportServiceConfiguration
        {
            HostAppId = "Application1",
            ReportResolver = new ReportFileResolver(HttpContext.Current.Server.MapPath("~/Reports"))
                .AddFallbackResolver(new ReportTypeResolver()),
            Storage = new Telerik.Reporting.Cache.File.FileStorage(),
        };

        public ReportsController()
        {
            this.ReportServiceConfiguration = configurationInstance;
        }

    }
}