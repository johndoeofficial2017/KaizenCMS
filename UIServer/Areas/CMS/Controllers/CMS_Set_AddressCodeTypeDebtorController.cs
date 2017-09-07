using Kaizen.BusinessLogic.CMS;
using Kaizen.BusinessLogic.Configuration;
using Kaizen.CMS;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Areas.CMS.Controllers
{
    public class CMS_Set_AddressCodeTypeDebtorController : Controller
    {
        // GET: CMS_Set_AddressCodeTypeDebtor
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult Create(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00009> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00009
               {
                   AddressCode = o.AddressCode,
                   AddressName = o.AddressName
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00009>(),
                    Total = 0
                };
            }
            return result;
        }

        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00009Services invService = new CM00009Services(KaizenUser);
            DataCollection<CM00009> L = new DataCollection<CM00009>();
            string filters = string.Empty;
            string SortMember;
            bool IsAscending;
            SortDescriptor sortobject = request.Sorts.FirstOrDefault();
            if (sortobject != null)
            {
                SortMember = sortobject.Member;
                if (sortobject.SortDirection == System.ComponentModel.ListSortDirection.Ascending)
                    IsAscending = true;
                else
                    IsAscending = false;
            }
            else
            {
                SortMember = "AddressCodeType";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetListPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00009Services invService = new CM00009Services(KaizenUser);
            DataCollection<CM00009> L = new DataCollection<CM00009>();
            string filters = string.Empty;
            string SortMember;
            bool IsAscending;
            SortDescriptor sortobject = request.Sorts.FirstOrDefault();
            if (sortobject != null)
            {
                SortMember = sortobject.Member;
                if (sortobject.SortDirection == System.ComponentModel.ListSortDirection.Ascending)
                    IsAscending = true;
                else
                    IsAscending = false;
            }
            else
            {
                SortMember = "AddressCodeType";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult SaveData(CM00009 CM00009, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00009Services service = new CM00009Services(KaizenUser);
            return Json(service.AddCM00009(CM00009), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(CM00009 CM00009, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00009Services service = new CM00009Services(KaizenUser);
            return Json(service.Update(CM00009), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(CM00009 CM00009, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00009Services service = new CM00009Services(KaizenUser);
            return Json(service.Delete(CM00009), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string AddressCode)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00009Services service = new CM00009Services(KaizenUser);
            CM00009 L = service.GetSingle(AddressCode);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00009Services service = new CM00009Services(KaizenUser);
            IList<CM00009> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region Debtor Address Roles
        public ActionResult DebtorAddressRole(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetRolesByAddressCode(string KaizenPublicKey, string AddressCode)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00009Services service = new CM00009Services(KaizenUser);
            IList<CM00032> o = service.GetRolesByAddressCode(AddressCode);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveAddressRole(CM00032 CM00032, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00009Services service = new CM00009Services(KaizenUser);
            return Json(service.AddCM00032(CM00032), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAddressRole(CM00032 CM00032, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00009Services service = new CM00009Services(KaizenUser);
            return Json(service.DeleteCM00032(CM00032), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Debtor Address User
        public ActionResult DebtorAddressUser(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetDebtorAddressByUser(string KaizenPublicKey, string userName)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00009Services service = new CM00009Services(KaizenUser);
            IList<CM00031> o = service.GetDebtorAddressByUser(userName);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddDebtorAddressByUser(CM00031 CM00031, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00009Services service = new CM00009Services(KaizenUser);
            return Json(service.AddCM00031(CM00031), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteDebtorAddressUser(CM00031 CM00031, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00009Services service = new CM00009Services(KaizenUser);
            return Json(service.DeleteCM00031(CM00031), JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult DebtorGrid(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
    }
}