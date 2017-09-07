using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.BusinessLogic.CMS;
using Kaizen.CMS;
using Kaizen.BusinessLogic.Configuration;

namespace UIServer.Areas.LookUp.Controllers
{
    public class CMS_ContractLookupController : Controller
    {
        // GET: LookUp/CMS_ContractLookup
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetDataListPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00200Services serv = new CM00200Services(KaizenUser);
            DataCollection<CM00200> L = new DataCollection<CM00200>();
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
                SortMember = "ContractID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00200> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00200
               {
                   ContractID = o.ContractID,
                   ClientID = o.ClientID,
                   ContractName = o.ContractName,
                   ContractStatusID = o.ContractStatusID,
                   PaymentBaseID = o.PaymentBaseID,
                   BillingFrequencyID = o.BillingFrequencyID,
                   Abbreviation = o.Abbreviation,
                   StartDate = o.StartDate,
                   EndDate = o.EndDate,
                   BilltoCustomer = o.BilltoCustomer,
                   CurrencyCode = o.CurrencyCode,
                   LastPaymentDate = o.LastPaymentDate,
                   IsPrivateCase = o.IsPrivateCase,
                   Remarks = o.Remarks
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00200>(),
                    Total = 0
                };
            }
            return result;
        }
    }
}