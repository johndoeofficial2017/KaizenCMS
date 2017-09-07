using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.BusinessLogic.GL;
using Kaizen.GL;
using Kendo.Mvc;
using Kaizen.BusinessLogic.Configuration;
using UIServer;

namespace UIServer.Areas.LookUp.Controllers
{
    public class GL_CheckbookLookUpController : Controller
    {
        // GET: LookUp/GL_CheckbookLookUp
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetListGridWithCurrencyCode([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria, string CurrencyCode)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            GL00103Services invService = new GL00103Services(KaizenUser);
            DataCollection<GL00103> L = new DataCollection<GL00103>();
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
                SortMember = "CheckbookCode";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(CurrencyCode))
            {
                L = invService.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, CurrencyCode
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }
            else
            {
                L = invService.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria
               , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<GL00103> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new GL00103
               {
                   CheckbookCode = o.CheckbookCode,
                   CheckbookName = o.CheckbookName,
                   CurrencyCode = o.CurrencyCode,
                   DecimalDigit = o.DecimalDigit,
                   AccountID = o.AccountID,
                   IsMultiply = o.IsMultiply,
                   ACTNUMBR = o.ACTNUMBR,
                   AccountName = o.AccountName,
                   //ExchangeRateID = o.ExchangeRateID,
                   BankAccount = o.BankAccount,
                   BankCode = o.BankCode,
                   BankIBN = o.BankIBN,
                   CheckbookBalance = o.CheckbookBalance,
                   ExchangeRate = o.ExchangeRate,
                   Inactive = o.Inactive,
                   IsOneSerialNumberSale = o.IsOneSerialNumberSale,
                   NextCheckLastNumber = o.NextCheckLastNumber,
                   NextCheckPrefix = o.NextCheckPrefix,
                   NextDepositLastNumber = o.NextDepositLastNumber,
                   NextDepositLenth = o.NextDepositLenth,
                   NextDepositPrefix = o.NextDepositPrefix,
                   PayCashLastNumber = o.PayCashLastNumber,
                   PayCashLenth = o.PayCashLenth,
                   PayCashPrefix = o.PayCashPrefix,
                   PayCreditLastNumber = o.PayCreditLastNumber,
                   PayCreditLenth = o.PayCreditLenth,
                   PayCreditPrefix = o.PayCreditPrefix,
                   PaymentExchangeTableID = o.PaymentExchangeTableID,
                   NextCheckLenth = o.NextCheckLenth,
                   IsSystemPayCashNumber = o.IsSystemPayCashNumber,
                   ExchangeTableID = o.ExchangeTableID,
                   PayCheckLastNumber = o.PayCheckLastNumber,
                   PayCheckLenth = o.PayCheckLenth,
                   PayCheckPrefix = o.PayCheckPrefix,
                   TOTALPAID = o.TOTALPAID
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<GL00103>(),
                    Total = 0
                };
            }
            return result;
        }

    }
}