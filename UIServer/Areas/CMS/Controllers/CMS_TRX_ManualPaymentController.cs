using Kaizen.BusinessLogic.CMS;
using Kaizen.BusinessLogic.Configuration;
using Kaizen.CMS;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Areas.CMS.Controllers
{
    public class CMS_TRX_ManualPaymentController : Controller
    {
        // GET: CMS_TRX_ManualPayment
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

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00307> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00307
               {
                   PaymentID = o.PaymentID,
                   CurrencyCode = o.CurrencyCode,
                   Amount = o.Amount,
                   PaymentMethodID = o.PaymentMethodID,
                   AgentID = o.AgentID,
                   CreatedDate = o.CreatedDate,
                   PaymentNumber = o.PaymentNumber,
                   BankName = o.BankName,
                   BankAccount = o.BankAccount,
                   BankCheckDate=o.BankCheckDate,
                   BankCode=o.BankCode,
                   CheckbookCode=o.CheckbookCode,
                   DecimalDigit=o.DecimalDigit,
                   ExchangeRate=o.ExchangeRate,
                   ExchangeTableID=o.ExchangeTableID,
                   IsMultiply=o.IsMultiply,
                   ReasonID=o.ReasonID,
                   TransactionDate=o.TransactionDate,
                   TransDescription=o.TransDescription,
                   CreatedBy=o.CreatedBy
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00307>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetList([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00307Services invService = new CM00307Services(KaizenUser);
            DataCollection<CM00307> L = new DataCollection<CM00307>();
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

            CM00307Services serv = new CM00307Services(KaizenUser);
            DataCollection<CM00307> L = new DataCollection<CM00307>();
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

            CM00307Services serv = new CM00307Services(KaizenUser);
            DataSourceResult result = serv.GetAll().ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult SaveData(IList<CM00307> CM00307, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00307Services service = new CM00307Services(KaizenUser);
            return Json(service.AddCM00307(CM00307), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(IList<CM00307> CM00307, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00307Services service = new CM00307Services(KaizenUser);
            return Json(service.Update(CM00307), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(IList<CM00307> CM00307, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00307Services service = new CM00307Services(KaizenUser);
            return Json(service.Delete(CM00307), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveDataObject(CM00307 CM00307, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00307Services service = new CM00307Services(KaizenUser);
            CM00307.CreatedDate = DateTime.Now;
            CM00307.CreatedBy = KaizenUser.UserName;
            return Json(service.AddCM00307(CM00307), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateDataObject(CM00307 CM00307, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00307Services service = new CM00307Services(KaizenUser);
            return Json(service.Update(CM00307), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteDataObject(CM00307 CM00307, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00307Services service = new CM00307Services(KaizenUser);
            return Json(service.Delete(CM00307.PaymentID), JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// Reminder Screen By Mahmoud Gamal
        /// </summary>
        /// <param name="CM00307"></param>
        /// <param name="KaizenPublicKey"></param>
        /// <returns></returns>
        public ActionResult SaveCaseManualPayment(CM00307 CM00307,string CaseRef,string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00307Services service = new CM00307Services(KaizenUser);
            CM00307.CreatedDate = DateTime.Now;
            CM00307.CreatedBy = KaizenUser.UserName;
            var result = service.AddCM00307(CM00307);
            if (result.Status)
            {
                CM00308Services srv = new CM00308Services(KaizenUser);
                CM00308 CM00308 = new CM00308() {
                    CaseRef= CaseRef,
                    Amount= CM00307.Amount,
                    PaymentID= CM00307.PaymentID
                };
                return Json(srv.AddCM00308(CM00308), JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string PaymentID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00307Services service = new CM00307Services(KaizenUser);
            CM00307 o = service.GetSingle(PaymentID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        #region Lines
        public ActionResult GetPaidCases(string KaizenPublicKey, string PaymentID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00307Services service = new CM00307Services(KaizenUser);
            return Json(service.GetPaidCases(PaymentID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveLineData(IList<CM00308> CM00308, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00308Services service = new CM00308Services(KaizenUser);
            return Json(service.AddCM00308(CM00308), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateLineData(IList<CM00308> CM00308, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00308Services service = new CM00308Services(KaizenUser);
            return Json(service.Update(CM00308), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteLineData(IList<CM00308> CM00308, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00308Services service = new CM00308Services(KaizenUser);
            return Json(service.Delete(CM00308), JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult SavePaymentsAgainstCase(CM00307 CM00307,List<CM00308> CM00308, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00307Services service = new CM00307Services(KaizenUser);
            var result = service.AddCM00307(CM00307, CM00308);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdatePaymentsAgainstCase(CM00307 CM00307,List<CM00308> CM00308, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00307Services service = new CM00307Services(KaizenUser);
            var result = service.AddCM00307(CM00307, CM00308);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region Manual Payment for Single Case

        public ActionResult SingleManualPayment(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return View();
        }

        public ActionResult GetCM00203FromCaseRef(string KaizenPublicKey,string CaseRef)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");

            CM00203Services service = new CM00203Services(KaizenUser);
            var data = service.GetCM00203FromCaseRef(CaseRef);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetALLCaseRef(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");

            CM00203Services service = new CM00203Services(KaizenUser);
            var data = service.GetAll().Select(m =>m.CaseRef).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}