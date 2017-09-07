using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.BusinessLogic.CMS;
using Kaizen.CMS;
using Kaizen.BusinessLogic.Configuration;

namespace UIServer.Areas.CMS.Controllers
{
    public class CMS_ReminderController : Controller
    {
        // GET: CMS_Reminder
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult MainGrid(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult CaseView(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");

            CM00029Services CM00029_serv = new CM00029Services(KaizenUser);
            ViewData["CaseTransactionTypes"] = CM00029_serv.GetAll();

            return PartialView();
        }
        public ActionResult DebtorPayment(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult collectionPayment(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return View();
        }
        public ActionResult ManualPayment(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ManualPaymentPopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        #region Reminder
        private DataSourceResult DataList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00203> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM_CaseView
               {
                   CaseRef = o.CaseRef,
                   CaseStatusname = o.CaseStatusname,
                   ContractID = o.ContractID,
                   ClientID = o.ClientID,
                   CurrencyCode = o.CurrencyCode,
                   //DecimalDigit = o.DecimalDigit,
                   DebtorID = o.CIFNumber,
                   //TaskProgress = o.TaskProgress,
                   CaseStatusID = o.CaseStatusID,
                   CaseStatusChild = o.CaseStatusChild,
                   CaseStatusComment = string.IsNullOrEmpty(o.CaseStatusComment) ? "" : o.CaseStatusComment.Length > 15 ? o.CaseStatusComment.Substring(0, 15) : o.CaseStatusComment,
                   ReminderDateTime = o.ReminderDateTime,
                   PTPAMT = o.PTPAMT,
                   AddressCode = string.IsNullOrEmpty(o.AddressCode) ? "" : o.AddressCode.Length > 15 ? o.AddressCode.Substring(0, 15) : o.AddressCode,
                   CaseAddess = string.IsNullOrEmpty(o.CaseAddess) ? "" : o.CaseAddess.Length > 15 ? o.CaseAddess.Substring(0, 15) : o.CaseAddess,
                   ContactTypeID = o.ContactTypeID,
                   CaseAccountNo = o.CaseAccountNo,
                   InvoiceNumber = o.InvoiceNumber,
                   ClosingDate = o.ClosingDate,
                   TransactionDate = o.TransactionDate,
                   BookingDate = o.BookingDate,
                   Remarks = string.IsNullOrEmpty(o.Remarks) ? "" : o.Remarks.Length > 15 ? o.Remarks.Substring(0, 15) : o.Remarks,
                   //OSAmount = o.OSAmount,
                   //ClaimAmount = o.ClaimAmount,
                   //FinanceCharge = o.FinanceCharge,
                   //PrincipleAmount = o.PrincipleAmount,
                   //CreatedDate = o.CreatedDate,
                   AgentID = o.AgentID.Trim(),
                   //AssignDate = o.AssignDate,
                   LastCallactedAMT = o.LastCallactedAMT,
                   LastPaymentBy = o.LastPaymentBy,
                   LastPaymentDate = o.LastPaymentDate,
                   TotalCallactedAMT = o.TotalCallactedAMT
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00203>(),
                    Total = 0
                };
            }
            return result;
        }

        #endregion

        public ActionResult SaveCaseHistoryData(CM00203 CM00203, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            var result = service.Update(CM00203);
            if (result.Status)
            {
                CM10701Services serv = new CM10701Services(KaizenUser);
                CM10701 CM10701 = new CM10701();
                CM10701.CaseRef = CM00203.CaseRef;
                CM10701.CaseStatusID = CM00203.CaseStatusID;
                CM10701.AgentID = CM00203.AgentID;
                CM10701.PTPAMT = CM00203.PTPAMT;
                CM10701.CaseStatusComment = CM00203.CaseStatusComment;
                CM10701.ReminderDateTime = CM00203.ReminderDateTime;
                CM10701.CreatedDate = DateTime.Now;
                CM10701.ChangeStatusSourceID = 1;
                return Json(serv.AddCM10701(CM10701), JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}