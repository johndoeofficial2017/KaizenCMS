using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.Admin;
using Kaizen.BusinessLogic.Admin;
using Kaizen.BusinessLogic.Configuration;

namespace UIServer.Controllers
{
    public class Adm_CountryController : Controller
    {
        // GET: Adm_Country
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult Create(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult PopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<Sys00013> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new Sys00013
               {

                   CountryID = o.CountryID,
                   CountryName = o.CountryName
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<Sys00013>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Sys00013Services invService = new Sys00013Services(KaizenUser);
            DataCollection<Sys00013> L = new DataCollection<Sys00013>();
            string filters = string.Empty;
            string SortMember;
            string SortDirection;
            SortDescriptor sortobject = request.Sorts.FirstOrDefault();
            if (sortobject != null)
            {
                SortMember = sortobject.Member;
                SortDirection = sortobject.SortDirection.ToString();
            }
            else
            {
                SortMember = "CountryID";
                SortDirection = "asc";
            }
            if (request.Filters.Count > 0)
            {
                string SQLQuery = Help.ApplyFilter(request.Filters[0]);
                L = invService.GetAllBYSQLQuery(SQLQuery, request.PageSize, request.Page, SortMember, SortDirection);
            }
            else
            {
                if (string.IsNullOrEmpty(Searchcritaria))
                    L = invService.GetListWithPaging(request.PageSize, request.Page, SortMember, SortDirection);
                else
                    L = invService.GetAllBYSQLQuery(Searchcritaria, request.PageSize, request.Page, SortMember, SortDirection);
            }
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetListPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Sys00013Services invService = new Sys00013Services(KaizenUser);
            DataCollection<Sys00013> L = new DataCollection<Sys00013>();
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
                SortMember = "CountryID";
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
        public ActionResult SaveData(Sys00013 Sys00013, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Sys00013Services service = new Sys00013Services(KaizenUser);
            return Json(service.AddSys00013(Sys00013), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(Sys00013 Sys00013, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Sys00013Services service = new Sys00013Services(KaizenUser);
            return Json(service.Update(Sys00013), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(Sys00013 Sys00013, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Sys00013Services service = new Sys00013Services(KaizenUser);
            return Json(service.Delete(Sys00013), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string CountryID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Sys00013Services service = new Sys00013Services(KaizenUser);
            Sys00013 L = service.GetSingle(CountryID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Sys00013Services service = new Sys00013Services(KaizenUser);
            IList<Sys00013> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
    }
}