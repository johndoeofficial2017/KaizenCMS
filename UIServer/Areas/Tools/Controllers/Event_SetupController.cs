using Kaizen.BusinessLogic.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaizen.Configuration;

namespace UIServer.Areas.Tools.Controllers
{
    public class Event_SetupController : Controller
    {
        // GET: TeamUp/EventSetup
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult SaveEventSetup(Met00011 Met00011, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00011Services service = new Met00011Services(KaizenUser);
            return Json(service.AddMet00011(Met00011), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateEventSetup(Met00011 Met00011, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00011Services service = new Met00011Services(KaizenUser);
            return Json(service.Update00011(Met00011), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteEventSetup(Met00011 Met00011, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00011Services service = new Met00011Services(KaizenUser);
            return Json(service.Delete00011(Met00011), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllCalenderNames(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00011Services service = new Met00011Services(KaizenUser);
            IList<Met00011> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region Calendar Role
        public ActionResult EventSetupRole(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult SaveEventSetupRole(Met00013 Met00013, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00011Services service = new Met00011Services(KaizenUser);
            return Json(service.SaveEventSetupRole(Met00013), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteEventSetupRole(Met00013 Met00013, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00011Services service = new Met00011Services(KaizenUser);
            return Json(service.DeleteEventSetupRole(Met00013), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEventSetupRoles(int CalendarID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00011Services service = new Met00011Services(KaizenUser);
            IList<Met00013> L = service.GetEventSetupRoles(CalendarID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Calendar User
        public ActionResult EventSetupUser(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult SaveEventSetupUser(Met00012 Met00012, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00011Services service = new Met00011Services(KaizenUser);
            return Json(service.SaveEventSetupUser(Met00012), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateEventSetupUser(Met00012 Met00012, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00011Services service = new Met00011Services(KaizenUser);
            return Json(service.UpdateEventSetupUser(Met00012), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteEventSetupUser(Met00012 Met00012, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00011Services service = new Met00011Services(KaizenUser);
            return Json(service.DeleteEventSetupUser(Met00012), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEventSetupUsers(int CalendarID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00011Services service = new Met00011Services(KaizenUser);
            IList<Met00012> L = service.GetEventSetupUsers(CalendarID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}