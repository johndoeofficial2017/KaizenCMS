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
    public class Sys_DashboardController : Controller
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
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<Kaizen00050> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new Kaizen00050
               {
                   CompanyID=o.CompanyID,
                   DashboardCode=o.DashboardCode,
                   DashboardName=o.DashboardName,
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<Kaizen00050>(),
                    Total = 0
                };
            }
            return result;
        }
        public JsonResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            Kaizen00050Services serv = new Kaizen00050Services(KaizenUser);
            DataCollection<Kaizen00050> L = new DataCollection<Kaizen00050>();
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
                SortMember = "DashboardCode";
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

        public ActionResult SaveData(Kaizen00050 Kaizen00050, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            return Json(service.AddKaizen00050(Kaizen00050), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(Kaizen00050 Kaizen00050, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            return Json(service.Update(Kaizen00050), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(Kaizen00050 Kaizen00050, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            return Json(service.Delete(Kaizen00050), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, int DashboardCode)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            Kaizen00050 L = service.GetSingle(DashboardCode);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            IList<Kaizen00050> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region Source User
        public ActionResult DashboardUser(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult SaveDashboardUser(Kaizen00055 Kaizen00055, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            return Json(service.SaveDashboardUser(Kaizen00055), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteDashboardUser(Kaizen00055 Kaizen00055, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            return Json(service.DeleteDashboardUser(Kaizen00055), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDashboardUsers(string UserName, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            IList<Kaizen00055> L = service.GetDashboardUsers(UserName);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        
        #endregion

        #region Source Role Security
        public ActionResult DashboardRoleSecurity(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult SaveDashboardRole(Kaizen00054 Kaizen00054, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            return Json(service.SaveDashboardRole(Kaizen00054), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteDashboardRole(Kaizen00054 Kaizen00054, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            return Json(service.DeleteDashboardRole(Kaizen00054), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDashboardRoles(int RoleID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            IList<Kaizen00054> L = service.GetDashboardRoles(RoleID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Dashboard Reports
        public ActionResult DashboardReport(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetReportTypes(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            IList<Kaizen00051> L = service.GetReportTypes();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDashboardReports(int ReportTypeID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            IList<Kaizen00052> L = service.GetDashboardReports(ReportTypeID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetKaizen00053ForReportType(int ReportTypeID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            IList<Kaizen00053> L = service.GetKaizen00053ForReportType(ReportTypeID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddDashboardReportData(IList<Kaizen00053> Kaizen00053List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            return Json(service.AddDashboardReportData(Kaizen00053List), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateDashboardReportData(IList<Kaizen00053> Kaizen00053List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            return Json(service.UpdateDashboardReportData(Kaizen00053List), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteDashboardReportData(IList<Kaizen00053> Kaizen00053List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00050Services service = new Kaizen00050Services(KaizenUser);
            return Json(service.DeleteDashboardReportData(Kaizen00053List), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}