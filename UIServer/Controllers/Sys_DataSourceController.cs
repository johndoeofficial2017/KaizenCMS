using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaizen.BusinessLogic.Admin;
using Kaizen.Configuration;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kaizen.BusinessLogic.Configuration;

namespace UIServer.Controllers
{
    public class Sys_DataSourceController : Controller
    {
        // GET: Sys_DataSource
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        #region
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<DT00100> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new DT00100
               {
                   CompanyID=o.CompanyID,
                   IsAddNew=o.IsAddNew,
                   IsDataTable=o.IsDataTable,
                   IsEdit=o.IsEdit,
                   IsGraph=o.IsGraph,
                   MenueTypeID=o.MenueTypeID,
                   ModuleID=o.ModuleID,
                   SourceHTML=o.SourceHTML,
                   SourceID=o.SourceID,
                   SourceName=o.SourceName,
                   SourceSql=o.SourceSql,
                   ViewSource=o.ViewSource,
                   ViewType=o.ViewType
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<DT00100>(),
                    Total = 0
                };
            }
            return result;
        }
        public JsonResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            DT00100Services serv = new DT00100Services(KaizenUser);
            DataCollection<DT00100> L = new DataCollection<DT00100>();
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
                SortMember = "SourceID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion

        public ActionResult SaveData(DT00100 DT00100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            DT00100Services service = new DT00100Services(KaizenUser);
            return Json(service.AddDT00100(DT00100), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(DT00100 DT00100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            DT00100Services service = new DT00100Services(KaizenUser);
            return Json(service.Update(DT00100), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(DT00100 DT00100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            DT00100Services service = new DT00100Services(KaizenUser);
            return Json(service.Delete(DT00100), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, int SourceID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            DT00100Services service = new DT00100Services(KaizenUser);
            DT00100 L = service.GetSingle(SourceID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            DT00100Services service = new DT00100Services(KaizenUser);
            IList<DT00100> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetViewTypes(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            DT00100Services service = new DT00100Services(KaizenUser);
            IList<DT00001> L = service.GetViewTypes();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMenueTypes(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            DT00100Services service = new DT00100Services(KaizenUser);
            IList<Kaizen001> L = service.GetMenueTypes();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region Source User
        public ActionResult SourceUser(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult SaveSourceUser(DT00103 DT00103, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            DT00100Services service = new DT00100Services(KaizenUser);
            return Json(service.SaveSourceUser(DT00103), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateSourceUser(DT00103 DT00103, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            DT00100Services service = new DT00100Services(KaizenUser);
            return Json(service.UpdateSourceUser(DT00103), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteSourceUser(DT00103 DT00103, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            DT00100Services service = new DT00100Services(KaizenUser);
            return Json(service.DeleteSourceUser(DT00103), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSourceUsers(int SourceID,string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            DT00100Services service = new DT00100Services(KaizenUser);
            IList<DT00103> L = service.GetSourceUsers(SourceID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        
        #endregion

        #region Source Role Security
        public ActionResult SourceRoleSecurity(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult SaveSourceRole(DT00102 DT00102, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            DT00100Services service = new DT00100Services(KaizenUser);
            return Json(service.SaveSourceRole(DT00102), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteSourceRole(DT00102 DT00102, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            DT00100Services service = new DT00100Services(KaizenUser);
            return Json(service.DeleteSourceRole(DT00102), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSourceRoles(int SourceID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            DT00100Services service = new DT00100Services(KaizenUser);
            IList<DT00102> L = service.GetSourceRoles(SourceID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Source Field
        public ActionResult SourceField(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetSourceFields(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            DT00100Services service = new DT00100Services(KaizenUser);
            IList<Kaizen00006> L = service.GetSourceFields();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDT00101(int SourceID,string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            DT00100Services service = new DT00100Services(KaizenUser);
            IList<DT00101> L = service.GetDT00101(SourceID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveSourceField(DT00101 DT00101, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            DT00100Services service = new DT00100Services(KaizenUser);
            return Json(service.SaveSourceField(DT00101), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateSourceField(DT00101 DT00101, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            DT00100Services service = new DT00100Services(KaizenUser);
            return Json(service.UpdateSourceField(DT00101), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSourceField(DT00101 DT00101, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            DT00100Services service = new DT00100Services(KaizenUser);
            return Json(service.DeleteSourceField(DT00101), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}