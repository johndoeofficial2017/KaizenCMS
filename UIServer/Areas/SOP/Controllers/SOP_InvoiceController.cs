using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.SOP;
using Kaizen.BusinessLogic.SOP;
using Kaizen.BusinessLogic.Configuration;
using System.Collections.Generic;

namespace UIServer.Areas.SOP.Controllers
{
    public class SOP_InvoiceController : Controller
    {
        // GET: SOP_Invoice
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
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<SOP10100> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new SOP10100
               {
                   SOPNUMBE = o.SOPNUMBE,
                   BatchID = o.BatchID,
                   DOCDATE = o.DOCDATE,
                   DOCAMNT = o.DOCAMNT,
                   CUSTNMBR = o.CUSTNMBR,
                   CUSTNAME = o.CUSTNAME,
                   CustomerPoNumber = o.CustomerPoNumber,
                   ShipDate = o.ShipDate,
                   SiteID = o.SiteID,
                   CurrencyCode = o.CurrencyCode,
                   ExchangeTableID = o.ExchangeTableID,
                   ExchangeRateID = o.ExchangeRateID,
                   ExchangeRate = o.ExchangeRate,
                   IsMultiply = o.IsMultiply,
                   DecimalDigit = o.DecimalDigit,
                   TaxAMT = o.TaxAMT,
                   Markdown = o.Markdown,
                   Freight = o.Freight,
                   Miscellaneous = o.Miscellaneous,
                   TradeDiscount = o.TradeDiscount,
                   TrxComments = o.TrxComments
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<SOP10100>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            SOP10100Services invService = new SOP10100Services(KaizenUser);
            DataCollection<SOP10100> L = new DataCollection<SOP10100>();
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
                SortMember = " GroupID";
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

        #endregion

        public ActionResult SaveData(SOP10100 SOP10100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10100Services service = new SOP10100Services(KaizenUser);
            return Json(service.AddSOP10100(SOP10100), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(SOP10100 SOP10100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10100Services service = new SOP10100Services(KaizenUser);
            return Json(service.Update(SOP10100), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(SOP10100 SOP10100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10100Services service = new SOP10100Services(KaizenUser);
            return Json(service.Delete(SOP10100), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string SOPNUMBE,string SOPTypeSetupID,byte SOPTYPE)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10100Services service = new SOP10100Services(KaizenUser);
            //SOP10100 L = service.GetSingle(SOPNUMBE, SOPTypeSetupID, SOPTYPE);
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillSOPTypeDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            SOP10100Services service = new SOP10100Services(KaizenUser);
            IList<SOP00000> L = service.GetAllSOP00000();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
    }
}