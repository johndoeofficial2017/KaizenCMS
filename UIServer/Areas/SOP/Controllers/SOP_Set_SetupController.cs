using Kaizen.BusinessLogic.Configuration;
using Kaizen.BusinessLogic.SOP;
using Kaizen.SOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Areas.SOP.Controllers
{
    public class SOP_Set_SetupController : Controller
    {
        // GET: SOP_Set_Setup
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult SaveData(SOP000014 SOP000014, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP000014Services service = new SOP000014Services(KaizenUser);
            SOP000014.SetupID = 1;
            return Json(service.AddSOP000014(SOP000014), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(SOP000014 SOP000014, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP000014Services service = new SOP000014Services(KaizenUser);
            return Json(service.Update(SOP000014), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(SOP000014 SOP000014, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP000014Services service = new SOP000014Services(KaizenUser);
            return Json(service.Delete(SOP000014), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, int SetupID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP000014Services service = new SOP000014Services(KaizenUser);
            SOP000014 L = service.GetSingle(SetupID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            SOP000014Services service = new SOP000014Services(KaizenUser);
            IList<SOP000014> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
    }
}