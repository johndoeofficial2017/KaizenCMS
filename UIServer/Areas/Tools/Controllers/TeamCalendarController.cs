using Kaizen.BusinessLogic.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaizen.Configuration;

namespace UIServer.Areas.Tools.Controllers
{
    public class TeamCalendarController : Controller
    {
        // GET: TeamUp/TeamCalendar
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult SaveTeamCalendar(Met00001 Met00001, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00001Services service = new Met00001Services(KaizenUser);
            return Json(service.AddMet00001(Met00001), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateTeamCalendar(Met00001 Met00001, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00001Services service = new Met00001Services(KaizenUser);
            return Json(service.Update00001(Met00001), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteTeamCalendar(Met00001 Met00001, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00001Services service = new Met00001Services(KaizenUser);
            return Json(service.Delete00001(Met00001), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllCalenderNames(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00001Services service = new Met00001Services(KaizenUser);
            IList<Met00001> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region Calendar Role
        public ActionResult CalendarRole(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult SaveCalendarRole(Met00003 Met00003, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00001Services service = new Met00001Services(KaizenUser);
            return Json(service.SaveCalendarRole(Met00003), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCalendarRole(Met00003 Met00003, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00001Services service = new Met00001Services(KaizenUser);
            return Json(service.DeleteCalendarRole(Met00003), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCalendarRoles(int CalendarID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00001Services service = new Met00001Services(KaizenUser);
            IList<Met00003> L = service.GetCalendarRoles(CalendarID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Calendar User
        public ActionResult CalendarUser(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult SaveCalendarUser(Met00002 Met00002, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00001Services service = new Met00001Services(KaizenUser);
            return Json(service.SaveCalendarUser(Met00002), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCalendarUser(Met00002 Met00002, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00001Services service = new Met00001Services(KaizenUser);
            return Json(service.UpdateCalendarUser(Met00002), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCalendarUser(Met00002 Met00002, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00001Services service = new Met00001Services(KaizenUser);
            return Json(service.DeleteCalendarUser(Met00002), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCalendarUsers(int CalendarID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00001Services service = new Met00001Services(KaizenUser);
            IList<Met00002> L = service.GetCalendarUsers(CalendarID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}