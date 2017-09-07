using Kaizen.BusinessLogic.CMS;
using Kaizen.BusinessLogic.Configuration;
using Kaizen.CMS;
using Kaizen.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Areas.CMS.Controllers
{
    public class CMS_SecurityController : Controller
    {
        // GET: CMS/CMS_Security
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DebtorRole(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult DebtorUser(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult GetAllCM00100(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00100Services service = new CM00100Services(KaizenUser);
            IList<CM00100> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllCM00107(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CMSSecurityServices service = new CMSSecurityServices(KaizenUser);
            IList<CM00107> L = service.GetAllCM00107();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region Status Group Role
        public ActionResult CaseTypeRole(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult GetRolesByDebtor(string KaizenPublicKey, string DebtorID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CMSSecurityServices service = new CMSSecurityServices(KaizenUser);
            IList<CM00119> o = service.GetRolesByDebtor(DebtorID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveRole(CM00119 CM00119, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CMSSecurityServices service = new CMSSecurityServices(KaizenUser);
            return Json(service.AddCM00119(CM00119), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteRole(CM00119 CM00119, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CMSSecurityServices service = new CMSSecurityServices(KaizenUser);
            return Json(service.DeleteCM00119(CM00119), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Status Group User
        public ActionResult CaseTypeUser(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetDebtorByUser(string KaizenPublicKey, string userName)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CMSSecurityServices service = new CMSSecurityServices(KaizenUser);
            IList<CM00118> o = service.GetDebtorByUser(userName);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteDebtorByUser(CM00118 CM00118, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CMSSecurityServices service = new CMSSecurityServices(KaizenUser);
            return Json(service.DeleteDebtorByUser(CM00118), JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddDebtorByUser(CM00118 CM00118, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CMSSecurityServices service = new CMSSecurityServices(KaizenUser);
            return Json(service.AddCM00118(CM00118), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}