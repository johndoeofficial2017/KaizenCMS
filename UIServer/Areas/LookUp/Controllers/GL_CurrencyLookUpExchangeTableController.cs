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
    public class GL_CurrencyLookUpExchangeTableController : Controller
    {
        // GET: LookUp/GL_CurrencyLookUpExchangeTable
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetExchangeTableListWithCurrencyCode([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria, string CurrencyCode)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            GL00011Services invService = new GL00011Services(KaizenUser);
            DataCollection<GL00011> L = new DataCollection<GL00011>();
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
                SortMember = "ExchangeTableID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, CurrencyCode
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = ExchangeTableList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private DataSourceResult ExchangeTableList([DataSourceRequest]DataSourceRequest request, DataCollection<GL00011> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new GL00011
               {
                   ExchangeTableID = o.ExchangeTableID,
                   CurrencyCode = o.CurrencyCode,
                   IsMultiply = o.IsMultiply
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<GL00011>(),
                    Total = 0
                };
            }
            return result;
        }
    }
}




