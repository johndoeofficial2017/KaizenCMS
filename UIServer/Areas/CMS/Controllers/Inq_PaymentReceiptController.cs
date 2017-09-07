using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.BusinessLogic.CMS;
using Kaizen.CMS;
using Kaizen.BusinessLogic.Configuration;
using System.IO;
using System.Transactions;
using System.Web;
using UIServer.Infrastructure.Token_Setup;


namespace UIServer.Areas.CMS.Controllers
{ 
    public class Inq_PaymentReceiptController : Controller
    {
        // GET: CMS/Inq_PaymentReceipt
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return View();
        }
        public ActionResult Create(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return View();
        }
        public ActionResult MainGrid(string KaizenPublicKey, string ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00026Services oKaizen00011ColumnServices = new Kaizen00026Services(KaizenUser);
            List<Kaizen.Configuration.Kaizen00026> oKaizen00026ColumnList = oKaizen00011ColumnServices.GetFieldsByView(int.Parse(ViewID)).ToList();
            if (oKaizen00026ColumnList.Count > 0)
            {
                ViewBag.KaizenGridViewLockedColumn = oKaizen00026ColumnList.FindAll(ss => ss.locked).OrderBy(x => x.ColumnOrder);
                ViewBag.KaizenGridViewColumn = oKaizen00026ColumnList.FindAll(ss => !ss.locked).OrderBy(x => x.ColumnOrder);
            }
            else
            {
                ViewBag.KaizenGridViewLockedColumn = new List<Kaizen.Configuration.Kaizen00026>();
                ViewBag.KaizenGridViewColumn = new List<Kaizen.Configuration.Kaizen00026>();
            }

            return PartialView();
        }
        public ActionResult PivotGrid(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00207> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00207
               {
                   PaymentID = o.PaymentID,
                   //CaseRef = o.CaseRef,
                   CurrencyCode = o.CurrencyCode,
                   Amount = o.Amount,
                   PaymentDate = o.PaymentDate,
                   Description = o.Description,
                   PaymentMethodID = o.PaymentMethodID,
                   AgentID = o.AgentID,
                   CreatedDate = o.CreatedDate,
                   PaymentNumber = o.PaymentNumber,
                   BankName = o.BankName,
                   BankAccount = o.BankAccount
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00207>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetList([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00207Services invService = new CM00207Services(KaizenUser);
            DataCollection<CM00207> L = new DataCollection<CM00207>();
            string filters = string.Empty;
            string SortMember;
            string SortDirection;
            SortDescriptor sortobject = request.Sorts.FirstOrDefault();
            if (sortobject != null)
            {
                SortMember = sortobject.Member;
                if (sortobject.SortDirection == System.ComponentModel.ListSortDirection.Ascending)
                    SortDirection = "asc";
                else
                    SortDirection = "desc";
            }
            else
            {
                SortMember = "PaymentID";
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
        public JsonResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00207Services serv = new CM00207Services(KaizenUser);
            DataCollection<CM00207> L = new DataCollection<CM00207>();
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
                SortMember = "PaymentID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPivotGridData([DataSourceRequest]DataSourceRequest request
            , string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00207Services serv = new CM00207Services(KaizenUser);
            DataSourceResult result = serv.GetAll().ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion


        public ActionResult SaveData(IList<CM00207> CM00207, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00207Services service = new CM00207Services(KaizenUser);
            return Json(service.AddCM00207(CM00207), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(IList<CM00207> CM00207, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00207Services service = new CM00207Services(KaizenUser);
            return Json(service.Update(CM00207), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(IList<CM00207> CM00207, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00207Services service = new CM00207Services(KaizenUser);
            return Json(service.Delete(CM00207), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveDataObject(CM00207 CM00207, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00207Services service = new CM00207Services(KaizenUser);
            return Json(service.AddCM00207(CM00207), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateDataObject(CM00207 CM00207, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00207Services service = new CM00207Services(KaizenUser);
            return Json(service.Update(CM00207), JsonRequestBehavior.AllowGet);
        }
        public ActionResult PostDataObject(CM00207 CM00207, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00207Services service = new CM00207Services(KaizenUser);
            return Json(service.Update(CM00207), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteDataObject(CM00207 CM00207, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00207Services service = new CM00207Services(KaizenUser);
            return Json(service.Delete(CM00207.PaymentID), JsonRequestBehavior.AllowGet);
        }

        #region Case Payment History
        private DataSourceResult CasePaymentHistoryList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00204> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM00204
                {
                    PaymentID = o.PaymentID,
                    Amount = o.Amount,
                    CaseRef = o.CaseRef,
                    ClientID = o.ClientID,
                    ContractID = o.ContractID
                }
                );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00204>(),
                    Total = 0
                };
            }
            return result;
        }
        public JsonResult GetCasePaymentHistoryDataListGrid([DataSourceRequest]DataSourceRequest request,
            string Searchcritaria, string CaseRef, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00204Services serv = new CM00204Services(KaizenUser);
            DataCollection<CM00204> L = new DataCollection<CM00204>();
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
                SortMember = "PaymentID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria, CaseRef
                                , request.PageSize, request.Page, SortMember, IsAscending);

            DataSourceResult result = CasePaymentHistoryList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        /// <summary>
        /// Reminder Screen By Mahmoud Gamal
        /// </summary>
        /// <param name="CM00307"></param>
        /// <param name="KaizenPublicKey"></param>
        /// <returns></returns>
        public ActionResult SaveCasePayment(CM00207 CM00207, string CaseRef,
            string ClientID, string ContractID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00207Services service = new CM00207Services(KaizenUser);
            CM00207.CreatedDate = DateTime.Now;
            CM00207.CreatedBy = KaizenUser.UserName;
            CM00207.IsApproved = false;
            var result = service.AddCM00207(CM00207);
            if (result.Status)
            {
                CM00204Services srv = new CM00204Services(KaizenUser);
                CM00204 CM00204 = new CM00204()
                {
                    CaseRef = CaseRef,
                    Amount = CM00207.Amount,
                    PaymentID = CM00207.PaymentID,
                    ClientID = ClientID,
                    ContractID = ContractID

                };
                return Json(srv.AddCM00204(CM00204), JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string PaymentID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00207Services service = new CM00207Services(KaizenUser);
            CM00207 o = service.GetSingle(PaymentID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetByCaseRef(string KaizenPublicKey, string CaseRef)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00207Services service = new CM00207Services(KaizenUser);
            IList<CM00207> o = service.GetAllByCaseRef(CaseRef);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        // calling from CMS_Reminder / DebtorPayment
        public ActionResult GetNextPaymentTransactionNumber(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);
            CM00207Services srv = new CM00207Services(KaizenUser);
            return Json(srv.GetNextPaymentID(), JsonRequestBehavior.AllowGet);
        }
        // calling from CMS_Reminder / DebtorPayment
        public ActionResult GetNextPaymentNumber(string KaizenPublicKey, int PaymentMethodID, string CheckbookCode)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);
            Kaizen.BusinessLogic.GL.GL00103Services srv = new Kaizen.BusinessLogic.GL.GL00103Services(KaizenUser);
            return Json(srv.GetNextBookPaymentID(CheckbookCode.Trim(), PaymentMethodID), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SavePaymentsAgainstCase(CM00207 CM00207,
    List<CM00204> CM00204, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00207Services service = new CM00207Services(KaizenUser);
            var result = service.AddCM00207(CM00207, CM00204);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region Lines
        public ActionResult GetPaidCases(string KaizenPublicKey, string PaymentID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00204Services service = new CM00204Services(KaizenUser);
            return Json(service.GetPaidCases(PaymentID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveLineData(IList<CM00204> CM00204, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00204Services service = new CM00204Services(KaizenUser);
            return Json(service.AddCM00204(CM00204), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateLineData(IList<CM00204> CM00204, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00204Services service = new CM00204Services(KaizenUser);
            return Json(service.Update(CM00204), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteLineData(IList<CM00204> CM00204, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00204Services service = new CM00204Services(KaizenUser);
            return Json(service.Delete(CM00204), JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}