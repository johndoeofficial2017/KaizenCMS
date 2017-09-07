using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.Inventory;
using Kaizen.BusinessLogic.Inventory;
using Kaizen.BusinessLogic.Configuration;

namespace UIServer.Areas.IV.Controllers
{
    public class INV_ValuationMethodController : Controller
    {
        // GET: INV_ValuationMethod
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

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00007> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00007
               {
                   ValuationMethodID = o.ValuationMethodID,
                   ValuationMethodName = o.ValuationMethodName
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00007>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);
            IV00007Services serv = new IV00007Services(KaizenUser);
            DataCollection<IV00007> L = new DataCollection<IV00007>();
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
                SortMember = "ValuationMethodID";
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
        public ActionResult SaveData(IV00007 IV00007, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00007Services service = new IV00007Services(KaizenUser);
            return Json(service.AddIV00007(IV00007), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(IV00007 IV00007, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00007Services service = new IV00007Services(KaizenUser);
            return Json(service.Update(IV00007), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(IV00007 IV00007, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00007Services service = new IV00007Services(KaizenUser);
            return Json(service.Delete(IV00007), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, byte ValuationMethodID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00007Services service = new IV00007Services(KaizenUser);
            IV00007 L = service.GetSingle(ValuationMethodID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00007Services service = new IV00007Services(KaizenUser);
            IList<IV00007> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
    }
}