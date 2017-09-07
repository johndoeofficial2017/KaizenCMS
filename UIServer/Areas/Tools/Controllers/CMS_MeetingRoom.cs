using Kaizen.BusinessLogic.Configuration;
using Kaizen.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace UIServer.Areas.Tools.Controllers
{
    public class CMS_MeetingRoomController : Controller
    {
        // GET: TeamUp/CMS_MeetingRoom
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult SaveMeetingRoom(Met00204 Met00204, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00204Services service = new Met00204Services(KaizenUser);
            return Json(service.AddMet00204(Met00204), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateMeetingRoom(Met00204 Met00204, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00204Services service = new Met00204Services(KaizenUser);
            return Json(service.Update00204(Met00204), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMeetingRoom(Met00204 Met00204, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00204Services service = new Met00204Services(KaizenUser);
            return Json(service.Delete00204(Met00204), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllRooms(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00204Services service = new Met00204Services(KaizenUser);
            IList<Met00204> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region Meeting Room Role
        public ActionResult MeetingRoomRole(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult GetRolesByMeetingRoom(string KaizenPublicKey, int MeetingRoomID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00204Services service = new Met00204Services(KaizenUser);
            IList<Met00206> o = service.GetRolesByMeetingRoom(MeetingRoomID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveRole(Met00206 Met00206, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00204Services service = new Met00204Services(KaizenUser);
            return Json(service.AddMet00206(Met00206), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteRole(Met00206 Met00206, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00204Services service = new Met00204Services(KaizenUser);
            return Json(service.DeleteMet00206(Met00206), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Meeting Room User
        public ActionResult MeetingRoomUser(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetMeetingRoomsByUser(string KaizenPublicKey, string userName)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00204Services service = new Met00204Services(KaizenUser);
            IList<Met00205> o = service.GetMeetingRoomsByUser(userName);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMet00205(Met00205 Met00205, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00204Services service = new Met00204Services(KaizenUser);
            return Json(service.DeleteMet00205(Met00205), JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddMet00205(Met00205 Met00205, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00204Services service = new Met00204Services(KaizenUser);
            return Json(service.AddMet00205(Met00205), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}