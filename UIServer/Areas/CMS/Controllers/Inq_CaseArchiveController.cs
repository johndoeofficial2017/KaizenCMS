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

namespace UIServer.Areas.CMS.Controllers
{
    public class Inq_CaseArchiveController : Controller
    {
        // GET: Inq_CaseArchive
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
        public ActionResult AdvancedSearch(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult MainGrid(string KaizenPublicKey, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00026Services oKaizen00011ColumnServices = new Kaizen00026Services(KaizenUser);
            List<Kaizen.Configuration.Kaizen00026> oKaizen00026ColumnList = oKaizen00011ColumnServices.GetFieldsByView(ViewID).ToList();
            ViewBag.KaizenGridViewLockedColumn = oKaizen00026ColumnList.FindAll(ss => ss.locked).OrderBy(x => x.ColumnOrder);
            List<Kaizen.Configuration.Kaizen00026> temp = oKaizen00026ColumnList.FindAll(ss => !ss.locked);
            if (temp.Count > 0)
                ViewBag.KaizenGridViewColumn = temp.OrderBy(x => x.ColumnOrder);
            else
                return PartialView("ErrorWindow");
            return PartialView();
        }
        public ActionResult ErrorWindow(string KaizenPublicKey)
        {
            return PartialView();
        }
        #region Grid Actions

        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<CM20203> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM20203
               {
                   //TrxID = o.TrxID,
                   CaseRef = o.CaseRef,
                   TRXTypeID = o.TRXTypeID,
                   //TrxTypeName = o.TrxTypeName,
                   ContractID = o.ContractID,
                   ContractName = o.ContractName,
                   ClientID = o.ClientID,
                   ClientName = o.ClientName,
                   BatchID = o.BatchID,
                   CurrencyCode = o.CurrencyCode,
                   DecimalDigit = o.DecimalDigit,
                   IsMultiply = o.IsMultiply,
                   ExchangeRate = o.ExchangeRate,
                   CIFNumber = o.CIFNumber,
                   CIFName = o.CIFName,
                   IsJoint = o.IsJoint,
                   BucketID = o.BucketID,
                   CaseStatusID = o.CaseStatusID,
                   CaseStatusChild = o.CaseStatusChild,
                   CaseStatusname = o.CaseStatusname,
                   CaseStatusChildName = o.CaseStatusChildName,
                   CaseStatusComment = o.CaseStatusComment,
                   ReminderDateTime = o.ReminderDateTime,
                   PTPAMT = o.PTPAMT,
                   CaseAddess = o.CaseAddess,
                   CaseAccountNo = o.CaseAccountNo,
                   InvoiceNumber = o.InvoiceNumber,
                   ClosingDate = o.ClosingDate,
                   TransactionDate = o.TransactionDate,
                   Remarks = o.Remarks,
                   //OSAmount = o.OSAmount,
                   FinanceCharge = o.FinanceCharge,
                   ClaimAmount = o.ClaimAmount,
                   PrincipleAmount = o.PrincipleAmount,
                   //CreatedDate = o.CreatedDate,
                   AgentID = o.AgentID,
                   AssignDate = o.AssignDate,
                   LastPaymentDate = o.LastPaymentDate,
                   LastCallactedAMT = o.LastCallactedAMT,
                   TotalCallactedAMT = o.TotalCallactedAMT,
                   LastPaymentBy = o.LastPaymentBy
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM20203>(),
                    Total = 0
                };
            } 
            return result;
        }
        public JsonResult GetDataListGrid([DataSourceRequest]DataSourceRequest request
            , string TRXTypeID, int ViewID,string YearCode, string PERIODID, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM20203Services serv = new CM20203Services(KaizenUser);
            DataCollection<CM20203> L = new DataCollection<CM20203>();
            string filters = string.Empty;
            string SortMember = "CaseRef";
            bool IsAscending = true;
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
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);
            if (!string.IsNullOrEmpty(SQLQuery))
            {
                SQLQuery += " and ";
            }
            SQLQuery += " TRXTypeID='" + TRXTypeID.Trim() + "'";
            L = serv.GetAllSQLQueryByView(SQLQuery, Searchcritaria
                , ViewID, YearCode, PERIODID, request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion

        public ActionResult PortfolioGrading(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult PortfolioYearlyGrading(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult PortfolioGradingCategory(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult PortfolioGradingPivot(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult PortfolioGradingStaff(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult PortfolioGradingStaffSummery(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult PortfolioCycleProduct(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }



        public ActionResult PortfolioCollection(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }



        public ActionResult AgentTargetProduct(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }


        public ActionResult PortfolioMovment(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult DelinquencyCycle(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult DelinquencyCycleUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult DelinquencyCycleDown(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
    }
}
