using Kaizen.BusinessLogic.CMS;
using Kaizen.BusinessLogic.Configuration;
using Kaizen.CMS;
using Kaizen.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace UIServer.Areas.Dashoard.Controllers
{
    public class MainDashoardController : Controller
    {
        // GET: Dashoard/MainDashoard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetMyDashboard(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            IList<Kaizen00050> L = service.GetMyDashboard();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

    }
}