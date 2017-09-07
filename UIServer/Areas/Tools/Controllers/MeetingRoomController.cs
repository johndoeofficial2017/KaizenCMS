using Kaizen.BusinessLogic.Configuration;
using Kaizen.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace UIServer.Areas.Tools.Controllers
{
    public class MeetingRoomController : Controller
    {
        // GET: TeamUp/MeetingRoom
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult SaveMeetingRoom(Met00007 Met00007, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00007Services service = new Met00007Services(KaizenUser);
            return Json(service.AddMet00007(Met00007), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateMeetingRoom(Met00007 Met00007, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00007Services service = new Met00007Services(KaizenUser);
            return Json(service.Update00007(Met00007), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMeetingRoom(Met00007 Met00007, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00007Services service = new Met00007Services(KaizenUser);
            return Json(service.Delete00007(Met00007), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllRooms(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00007Services service = new Met00007Services(KaizenUser);
            IList<Met00007> L = service.GetAll();
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
            Met00007Services service = new Met00007Services(KaizenUser);
            IList<Met00008> o = service.GetRolesByMeetingRoom(MeetingRoomID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveRole(Met00008 Met00008, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00007Services service = new Met00007Services(KaizenUser);
            return Json(service.AddMet00008(Met00008), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteRole(Met00008 Met00008, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00007Services service = new Met00007Services(KaizenUser);
            return Json(service.DeleteMet00008(Met00008), JsonRequestBehavior.AllowGet);
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
            Met00007Services service = new Met00007Services(KaizenUser);
            IList<Met00009> o = service.GetMeetingRoomsByUser(userName);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMet00009(Met00009 Met00009, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00007Services service = new Met00007Services(KaizenUser);
            return Json(service.DeleteMet00009(Met00009), JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddMet00009(Met00009 Met00009, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00007Services service = new Met00007Services(KaizenUser);
            return Json(service.AddMet00009(Met00009), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}