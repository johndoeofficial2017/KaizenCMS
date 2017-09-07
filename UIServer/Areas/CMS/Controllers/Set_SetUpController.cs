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
    public class Set_SetUpController : Controller
    {
        // GET: Set_SetUp
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult StandardFields(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult SaveData(CM00000 CM00000, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00000Services service = new CM00000Services(KaizenUser);
            return Json(service.AddCM00000(CM00000), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(CM00000 CM00000, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00000Services service = new CM00000Services(KaizenUser);
            return Json(service.Update(CM00000), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(CM00000 CM00000, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00000Services service = new CM00000Services(KaizenUser);
            return Json(service.Delete(CM00000), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSingle(string KaizenPublicKey, string CompanyID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00000Services service = new CM00000Services(KaizenUser);
            CM00000 L = service.GetSingle(CompanyID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00000Services service = new CM00000Services(KaizenUser);
            IList<CM00000> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStandardFields(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00038> o = service.GetStandardFields();
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateStandardFields1(IList<CM00038> CM00038List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00038> standardFields = service.GetStandardFields();

            foreach (CM00038 field in standardFields)
            {
                foreach (CM00038 updateField in CM00038List)
                {
                    if (field.FieldName.Equals(updateField.FieldName, StringComparison.OrdinalIgnoreCase))
                    {
                        updateField.IsEmailable = field.IsEmailable;
                        updateField.IsFilterable = field.IsFilterable;
                        updateField.Islockable = field.Islockable;
                        updateField.Islocked = field.Islocked;
                        updateField.IsPrintable = field.IsPrintable;
                        updateField.IsRequired = field.IsRequired;
                        updateField.IsSortable = field.IsSortable;
                        updateField.IsActive = field.IsActive;
                    }
                }
            }

            return Json(service.UpdateCM00038(CM00038List), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateStandardFields2(IList<CM00038> CM00038List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00038> standardFields = service.GetStandardFields();

            foreach (CM00038 field in standardFields)
            {
                foreach (CM00038 updateField in CM00038List)
                {
                    if (field.FieldName.Equals(updateField.FieldName, StringComparison.OrdinalIgnoreCase))
                    {
                        updateField.FieldDisplay = field.FieldDisplay;
                        updateField.FieldName = field.FieldName;
                        updateField.FieldOrder = field.FieldOrder;
                        updateField.FieldWidth = field.FieldWidth;
                        updateField.FieldTypeID = field.FieldTypeID;
                    }
                }
            }
            
            return Json(service.UpdateCM00038(CM00038List), JsonRequestBehavior.AllowGet);
        }
    }
}