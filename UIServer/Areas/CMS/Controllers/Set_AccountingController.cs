using Kaizen.BusinessLogic.CMS;
using Kaizen.BusinessLogic.Configuration;
using Kaizen.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Areas.CMS.Controllers
{
    public class Set_AccountingController : Controller
    {
        // GET: CMS/Set_Accounting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPaymentTypes(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00089Services service = new CM00089Services(KaizenUser);
            IList<CM00088> result = service.GetPaymentTypes();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAll(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00089Services service = new CM00089Services(KaizenUser);
            IList<CM00089> result = service.GetAll();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region Account User
        public ActionResult AccountUser(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetCM00090ByUser(string KaizenPublicKey,string UserName)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00089Services service = new CM00089Services(KaizenUser);
            IList<CM00090> result = service.GetCM00090ByUser(UserName);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveUser(CM00090 CM00090, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00089Services service = new CM00089Services(KaizenUser);
            return Json(service.AddCM00090(CM00090), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteUser(CM00090 CM00090, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00089Services service = new CM00089Services(KaizenUser);
            return Json(service.DeleteCM00090(CM00090), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Account Role
        public ActionResult AccountRole(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetCM00091ByRole(string KaizenPublicKey, int RoleID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00089Services service = new CM00089Services(KaizenUser);
            IList<CM00091> result = service.GetCM00091ByRole(RoleID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveRole(CM00091 CM00091, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00089Services service = new CM00089Services(KaizenUser);
            return Json(service.AddCM00091(CM00091), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteRole(CM00091 CM00091, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00089Services service = new CM00089Services(KaizenUser);
            return Json(service.DeleteCM00091(CM00091), JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult GetSingle(string KaizenPublicKey,string CheckbookCode,string CurrencyCode,string CheckbookName)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00089Services service = new CM00089Services(KaizenUser);
            CM00089 result = service.GetSingle(CheckbookCode, CurrencyCode, CheckbookName);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveData(CM00089 CM00089, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00089Services service = new CM00089Services(KaizenUser);
            return Json(service.AddCM00089(CM00089), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(CM00089 CM00089, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00089Services service = new CM00089Services(KaizenUser);
            return Json(service.Update(CM00089), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(CM00089 CM00089, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00089Services service = new CM00089Services(KaizenUser);
            return Json(service.Delete(CM00089), JsonRequestBehavior.AllowGet);
        }
    }
}