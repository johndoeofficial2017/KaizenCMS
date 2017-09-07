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
    public class CMS_PaymentController : Controller
    {
        // GET: CMS_Payment
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return View();
        }
        public ActionResult CreateSinglePayment(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return View();
        }
        public ActionResult BatchApproval(string KaizenPublicKey)
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
                ViewBag.KaizenGridViewColumn = oKaizen00026ColumnList.OrderBy(x => x.ColumnOrder);
            else
                return PartialView("ErrorWindow");

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
        public JsonResult GetDataListGrid([DataSourceRequest]DataSourceRequest request,int IsApproved
          , int TRXTypeID, string whereCondition ,string viewSortCondition,string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00207Services serv = new CM00207Services(KaizenUser);
            DataCollection<CM10207> L = new DataCollection<CM10207>();
            string filters = string.Empty;
            string SortMember = "PaymentID";
            if (request.Sorts != null)
            {
                SortDescriptor sortobject = request.Sorts.FirstOrDefault();
                if (sortobject != null)
                {
                    SortMember = sortobject.Member;
                    if (sortobject.SortDirection == System.ComponentModel.ListSortDirection.Ascending)
                        SortMember += " asc";
                    else
                        SortMember += " desc";
                }
                else
                    SortMember += " asc";
            }
            else
                SortMember += " desc";
            string SQLQuery = "IsApproved=" + IsApproved.ToString();
            if (request.Filters.Count > 0)
                SQLQuery = " and " + Help.ApplyFilter(request.Filters[0]);
            if (!string.IsNullOrEmpty(viewSortCondition))
                viewSortCondition += ",";
            viewSortCondition += SortMember;
            L = serv.GetWhereListWithPaging(SQLQuery, Searchcritaria, TRXTypeID, whereCondition, viewSortCondition
                , request.PageSize, request.Page);
            int tempPage = request.Page;
            DataSourceResult result = null; ;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM10207
               {
                   PaymentID = o.PaymentID,
                   CaseRef = o.CaseRef,
                   CurrencyCode = o.CurrencyCode,
                   CheckbookCode = o.CheckbookCode,
                   PaymentAmount = o.PaymentAmount,
                   PaymentDate = o.PaymentDate,
                   PaymentMethodID = o.PaymentMethodID,
                   AgentID = o.AgentID,
                   CreatedBy = o.CreatedBy,
                   PaymentNumber = o.PaymentNumber,
                   BankCode =o.BankCode,
                   BankName = o.BankName,
                   BankAccount = o.BankAccount,
                   BankCheckDate = o.BankCheckDate,
                   IsApproved= o.IsApproved,
                   DecimalDigit =o.DecimalDigit,
                   CreatedDate =o.CreatedDate 
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM10207>(),
                    Total = 0
                };
            }
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


        public ActionResult SaveData(IList<CM00207> CM00207,string KaizenPublicKey)
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
        public ActionResult SaveDataObject(CM10207 CM10207, string KaizenPublicKey)
        { 
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM10207Services service = new CM10207Services(KaizenUser);
            CM10207.PaymentDate = CM10207.PaymentDate.Date;
            //CM10207.CreatedDate = DateTime.Now;
            //CM10207.CreatedBy = KaizenUser.UserName;
            return Json(service.AddCM10207(CM10207), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ApproveDataObject(IList<CM10207> CM10207List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM10207Services service = new CM10207Services(KaizenUser);
            return Json(service.ApproveDataObject(CM10207List), JsonRequestBehavior.AllowGet);
        }
        public ActionResult PostBatch(IList<CM10207> CM10207List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM10207Services service = new CM10207Services(KaizenUser);
            //CM10207List = CM10207List.ToList().FindAll(uu => uu.IsApproved == true);
            return Json(service.PostBatch(CM10207List.ToList().FindAll(uu => uu.IsApproved == true)), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateDataObject(CM10207 CM10207, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM10207Services service = new CM10207Services(KaizenUser);
            return Json(service.Update(CM10207), JsonRequestBehavior.AllowGet);
        }
        public ActionResult PostSingle(string PaymentID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00207Services service = new CM00207Services(KaizenUser);
            return Json(service.PostSingle(PaymentID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult PostDataObject(CM00207 CM00207, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00207Services service = new CM00207Services(KaizenUser);
            return Json(service.UpdatePost(CM00207), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteDataObject(string PaymentID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00207Services service = new CM00207Services(KaizenUser);
            return Json(service.Delete(PaymentID), JsonRequestBehavior.AllowGet);
        }

        #region Case Payment History
        private DataSourceResult CasePaymentHistoryList([DataSourceRequest]DataSourceRequest request, DataCollection<CM10207> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM10207
                {
                    PaymentID = o.PaymentID,
                    CaseRef = o.CaseRef,
                    CheckbookCode = o.CheckbookCode,
                    CurrencyCode = o.CurrencyCode,
                      IsMultiply = o.IsMultiply,
                    ExchangeTableID = o.ExchangeTableID,
                    ExchangeRateID = o.ExchangeRateID,
                    ExchangeRate = o.ExchangeRate,
                    DecimalDigit = o.DecimalDigit,
                    PaymentAmount = o.PaymentAmount,
                    PaymentDate = o.PaymentDate,
                    Description = o.Description,
                    PaymentMethodID = o.PaymentMethodID,
                    AgentID = o.AgentID,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    PaymentNumber = o.PaymentNumber,
                    BankCode = o.BankCode,
                    BankName = o.BankName,
                    BankAccount = o.BankAccount,
                    BankCheckDate = o.BankCheckDate,
                    IsApproved = o.IsApproved,
                    //VoidBy = o.VoidBy,
                    //VoidDate = o.VoidDate,
                    //VoidSystemDate = o.VoidSystemDate
                }
                );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM10207>(),
                    Total = 0
                };
            }
            return result;
        }
        public JsonResult GetCasePaymentHistoryDataListGrid([DataSourceRequest]DataSourceRequest request,
            string Searchcritaria, string CaseRef,int TRXTypeID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM10207Services serv = new CM10207Services(KaizenUser);
            DataCollection<CM10207> L = new DataCollection<CM10207>();
            string filters = string.Empty;
            string SortMember = "PaymentID";
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
            L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria, CaseRef, TRXTypeID
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
            CM10207Services service = new CM10207Services(KaizenUser);
            CM10207 o = service.GetSingle(PaymentID);
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
            CM00307Services srv = new CM00307Services(KaizenUser);
            return Json(srv.GetNextPaymentID(), JsonRequestBehavior.AllowGet);
        }

      

        // calling from CMS_Reminder / DebtorPayment
        public ActionResult GetNextPaymentNumber(string KaizenPublicKey, int PaymentMethodID, string CheckbookCode)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);
            CM00307Services srv = new CM00307Services(KaizenUser);
            return Json(srv.GetNextBookPaymentID(CheckbookCode.Trim(), PaymentMethodID), JsonRequestBehavior.AllowGet);
            //return Json(22, JsonRequestBehavior.AllowGet);
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

        #region--> Manual Payment | CM10307

        public ActionResult GetNextManualPaymentTransactionNumber(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);
            CM00307Services srv = new CM00307Services(KaizenUser);
            return Json(srv.GetNextPaymentID(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetManualPaymentHistoryDataListGrid([DataSourceRequest]DataSourceRequest request,
           string Searchcritaria, string CaseRef, int TRXTypeID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM10307Services serv = new CM10307Services(KaizenUser);
            DataCollection<CM10307> L = new DataCollection<CM10307>();
            string filters = string.Empty;
            string SortMember = "PaymentID";
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
            L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria, CaseRef, TRXTypeID
                                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = ManualPaymentHistoryList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private DataSourceResult ManualPaymentHistoryList([DataSourceRequest]DataSourceRequest request, DataCollection<CM10307> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM10307
                {
                    PaymentID = o.PaymentID,
                    CaseRef = o.CaseRef,
                    ReasonID = o.ReasonID,
                    CheckbookCode = o.CheckbookCode,
                    CurrencyCode = o.CurrencyCode,
                    IsMultiply = o.IsMultiply,
                    ExchangeTableID = o.ExchangeTableID,
                    ExchangeRate = o.ExchangeRate,
                    DecimalDigit = o.DecimalDigit,
                    PaymentMethodID = o.PaymentMethodID,
                    AgentID = o.AgentID,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    PaymentNumber = o.PaymentNumber,
                    BankCode = o.BankCode,
                    BankName = o.BankName,
                    BankAccount = o.BankAccount,
                    BankCheckDate = o.BankCheckDate,
                    //VoidBy = o.VoidBy,
                    //VoidDate = o.VoidDate,
                    //VoidSystemDate = o.VoidSystemDate
                }
                );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM10307>(),
                    Total = 0
                };
            }
            return result;
        }

        public ActionResult SaveManualDataObject(CM10307 CM10307, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM10307Services service = new CM10307Services(KaizenUser);
            CM10307.TransactionDate = CM10307.TransactionDate.Date;
            return Json(service.AddCM10307(CM10307), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateManualDataObject(CM10307 CM10307, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM10307Services service = new CM10307Services(KaizenUser);
            return Json(service.Update(CM10307), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteManualDataObject(string PaymentID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM10307Services service = new CM10307Services(KaizenUser);
            return Json(service.Delete(PaymentID), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingleManual(string KaizenPublicKey, string PaymentID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM10307Services service = new CM10307Services(KaizenUser);
            CM10307 o = service.GetSingle(PaymentID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
