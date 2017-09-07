using Kaizen.BusinessLogic.Configuration;
using Kaizen.BusinessLogic.Inventory;
using Kaizen.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Areas.IV.Controllers
{
    public class IV_Set_SetUpController : Controller
    {
        // GET: IV_Set_SetUp
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult SaveData(IV000014 IV000014, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV000014Services service = new IV000014Services(KaizenUser);
            IV000014.SetupID = 1;
            return Json(service.AddIV000014(IV000014), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(IV000014 IV000014, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV000014Services service = new IV000014Services(KaizenUser);
            return Json(service.Update(IV000014), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(IV000014 IV000014, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV000014Services service = new IV000014Services(KaizenUser);
            return Json(service.Delete(IV000014), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, int SetupID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV000014Services service = new IV000014Services(KaizenUser);
            IV000014 L = service.GetSingle(SetupID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV000014Services service = new IV000014Services(KaizenUser);
            IList<IV000014> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
    }
}