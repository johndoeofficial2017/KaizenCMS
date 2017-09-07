using Kaizen.BusinessLogic.CMS;
using Kaizen.BusinessLogic.Configuration;
using Kaizen.CMS;
using Kaizen.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Controllers
{
    public class DashoardController : Controller
    {
        // GET: Dashoard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCurrentTarget(string KaizenPublicKey,string AgentID)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM10110Services service = new CM10110Services(KaizenUser);
            IList<CM_60610> L = service.GetCurrentTarget(AgentID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
         
    }
}