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
    public class GL_CurrencyLookUpController : Controller
    {
        // GET: LookUp/GL_CurrencyLookUp
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetListPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
   , string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            GL00102Services invService = new GL00102Services(KaizenUser);
            DataCollection<GL00102> L = new DataCollection<GL00102>();
            string filters = string.Empty;
            string SortMember;
            bool IsAscending;
            if (request.Sorts != null)
            {
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
            }
            else
            {
                SortMember = "CurrencyCode";
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
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<GL00102> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new GL00102
               {
                   CurrencyCode = o.CurrencyCode.Trim(),
                   CurrencyName = o.CurrencyName.Trim(),
                   CurrencySymbol = o.CurrencySymbol,
                   DecimalDigit = o.DecimalDigit,
                   DecimalSeparator = o.DecimalSeparator,
                   ExchangeRate = o.ExchangeRate,
                   ExchangeRateID = o.ExchangeRateID,
                   ExchangeTableID = o.ExchangeTableID,
                   GroupSeparator = o.GroupSeparator,
                   IsAfterSymbol = o.IsAfterSymbol,
                   IsMultiply = o.IsMultiply,
                   ISOCode = o.ISOCode,
                   SubUnit = o.SubUnit,
                   GroupSizes = o.GroupSizes,
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

    }
}