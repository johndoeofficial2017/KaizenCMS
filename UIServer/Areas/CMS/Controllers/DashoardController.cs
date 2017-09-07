using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Areas.CMS.Controllers
{
    public class DashoardController : Controller
    {
        // GET: CMS/Dashoard
        public ActionResult Index(string KaizenPublicKey)
        {
            return View();
        }
    }
}