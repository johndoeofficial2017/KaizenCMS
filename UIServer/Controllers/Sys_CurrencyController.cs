using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.BusinessLogic.GL;
using Kaizen.GL;
using Kendo.Mvc;
using Kaizen.BusinessLogic.Configuration;

namespace UIServer.Controllers
{
    public class Sys_CurrencyController : Controller
    {
        // GET: Sys_Currency
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

        #region Grid Data
        private DataSourceResult CurrencyList([DataSourceRequest]DataSourceRequest request, DataCollection<GL00102> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new GL00102
               {
                   CurrencyCode = o.CurrencyCode,
                   CurrencyName = o.CurrencyName,
                   ISOCode = o.ISOCode,
                   CurrencySymbol = o.CurrencySymbol,
                   DecimalDigit = o.DecimalDigit,
                   DecimalSeparator = o.DecimalSeparator,
                   GroupSeparator = o.GroupSeparator,
                   GroupSizes = o.GroupSizes,
                   IsAfterSymbol = o.IsAfterSymbol,
                   ExchangeRateID = o.ExchangeRateID,
                   ExchangeTableID = o.ExchangeTableID,
                   ExchangeRate = o.ExchangeRate,
                   IsMultiply = o.IsMultiply,
                   SubUnit = o.SubUnit,
                   Unit = o.Unit,
                   UnitConnector = o.UnitConnector
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<GL00102>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria, string CompanyID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            KaizenUser.CompanyID = CompanyID;
            GL00102Services invService = new GL00102Services(KaizenUser);
            DataCollection<GL00102> L = new DataCollection<GL00102>();
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
                SortMember = "CurrencyCode";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);
            L = invService.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria, request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CurrencyList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult SaveData(GL00102 SysGL00102, string CompanyID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            KaizenUser.CompanyID = CompanyID;
            GL00102Services service = new GL00102Services(KaizenUser);
            return Json(service.AddGL00102(SysGL00102), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(GL00102 SysGL00102, string CompanyID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            KaizenUser.CompanyID = CompanyID;
            GL00102Services service = new GL00102Services(KaizenUser);
            return Json(service.Update(SysGL00102), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(GL00102 SysGL00102, string CompanyID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            KaizenUser.CompanyID = CompanyID;
            GL00102Services service = new GL00102Services(KaizenUser);
            return Json(service.Delete(SysGL00102), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string CurrencyCode, string CompanyID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            KaizenUser.CompanyID = CompanyID;
            GL00102Services service = new GL00102Services(KaizenUser);
            GL00102 L = service.GetSingle(CurrencyCode);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
    }
}