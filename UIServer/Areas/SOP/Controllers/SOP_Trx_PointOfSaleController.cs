using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.SOP;
using Kaizen.BusinessLogic.SOP;
using Kaizen.BusinessLogic.Configuration;
using System.Collections.Generic;
using System;
using System.Text;

namespace UIServer.Areas.SOP.Controllers
{
    public class SOP_Trx_PointOfSaleController : Controller
    {
        // GET: SOP_Trx_PointOfSale
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
        public ActionResult View(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult POS200(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<SOP10300> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new SOP10300
               {
                   SOPNUMBE = o.SOPNUMBE,
                   SiteID = o.SiteID,
                   SubBinID = o.SubBinID,
                   BinID = o.BinID,
                   DOCDATE = o.DOCDATE,
                   CurrencyCode = o.CurrencyCode,
                   ExchangeRate = o.ExchangeRate,
                   IsMultiply = o.IsMultiply,
                   DecimalDigit = o.DecimalDigit,
                   DOCAMNT = o.DOCAMNT,
                   CollectedAMT = o.CollectedAMT,
                   PartialAMT = o.PartialAMT,
                   TotalCash = o.TotalCash,
                   TotalCheck = o.TotalCheck,
                   TotalCreditCard = o.TotalCreditCard,
                   TotalVoucher = o.TotalVoucher,
                   TaxAMT = o.TaxAMT,
                   Freight = o.Freight,
                   Markdown = o.Markdown,
                   Miscellaneous = o.Miscellaneous,
                   TradeDiscount = o.TradeDiscount,
                   CUSTNMBR = o.CUSTNMBR,
                   CUSTNAME = o.CUSTNAME,
                   Pnone01 = o.Pnone01,
                   Pnone02 = o.Pnone02,
                   MobileNo1 = o.MobileNo1,
                   MobileNo2 = o.MobileNo2,
                   POBox = o.POBox,
                   Other01 = o.Other01,
                   Other02 = o.Other02,
                   Address1 = o.Address1,
                   Email01 = o.Email01,
                   Email02 = o.Email02,
                   TrxComments = o.TrxComments,
                   UserName = o.UserName
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<SOP10300>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            SOP10300Services invService = new SOP10300Services(KaizenUser);
            DataCollection<SOP10300> L = new DataCollection<SOP10300>();
            string filters = string.Empty;
            string SortMember = "DOCDATE";
            bool IsAscending = false;
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

            L = invService.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult SaveData(SOP10300 SOP10300, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10300Services service = new SOP10300Services(KaizenUser);
            return Json(service.AddSOP10300(SOP10300), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(SOP10300 SOP10300, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10300Services service = new SOP10300Services(KaizenUser);
            return Json(service.Update(SOP10300), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(SOP10300 SOP10300, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10300Services service = new SOP10300Services(KaizenUser);
            return Json(service.Delete(SOP10300), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string SOPNUMBE)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10300Services service = new SOP10300Services(KaizenUser);
            SOP10300 L = service.GetSingle(SOPNUMBE);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetNextInvoice(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10300Services service = new SOP10300Services(KaizenUser);
            string guid = Guid.NewGuid().ToString();
            guid = RemoveSpecialCharacters(guid).Substring(0, 10);
            return Json(guid, JsonRequestBehavior.AllowGet);
        }

        #region Transaction Lines
        public ActionResult ItemsGrid(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        private DataSourceResult CaseItemsList([DataSourceRequest]DataSourceRequest request, DataCollection<SOP10301> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new SOP10301
               {
                   SOPNUMBE = o.SOPNUMBE,
                   LineID = o.LineID,
                   ORGSOPNUMBE = o.ORGSOPNUMBE,
                   ItemID = o.ItemID,
                   ItemName = o.ItemName,
                   PhotoExtension = o.PhotoExtension,
                   DecimalDigitQuantity = o.DecimalDigitQuantity,
                   InvoiceOTY = o.InvoiceOTY,
                   UFMSale = o.UFMSale,
                   BaseUnitSale = o.BaseUnitSale,
                   UnitCost = o.UnitCost,
                   UnitPrice = o.UnitPrice,
                   Commission = o.Commission,
                   TaxAMT = o.TaxAMT,
                   Freight = o.Freight,
                   Miscellaneous = o.Miscellaneous,
                   TradeDiscount = o.TradeDiscount,
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<SOP10300>(),
                    Total = 0
                };
            }
            return result;
        }

        public ActionResult GetDataListItemsGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            SOP10301Services invService = new SOP10301Services(KaizenUser);
            DataCollection<SOP10301> L = new DataCollection<SOP10301>();
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
                SortMember = "SOPNUMBE";
                IsAscending = false;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseItemsList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLinesByTransactionID(string KaizenPublicKey, string SOPNUMBE)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10301Services service = new SOP10301Services(KaizenUser);
            IList<SOP10301> o = service.GetBySOPNUMBE(SOPNUMBE);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveLineData(List<SOP10301> SOP10301, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10301Services service = new SOP10301Services(KaizenUser);
            return Json(service.AddSOP10301(SOP10301), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateLineData(List<SOP10301> SOP10301, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10301Services service = new SOP10301Services(KaizenUser);
            return Json(service.Update(SOP10301), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteLineData(IList<SOP10301> SOP10301, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10301Services service = new SOP10301Services(KaizenUser);
            return Json(service.Delete(SOP10301), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Archive Invoices
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<SOP10310> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new SOP10310
               {
                   TrxNumber = o.TrxNumber,
                   SOPNUMBE = o.SOPNUMBE,
                   CurrencyCode = o.CurrencyCode,
                   ExchangeRate = o.ExchangeRate,
                   IsMultiply = o.IsMultiply,
                   DecimalDigit = o.DecimalDigit,
                   DOCAMNT = o.DOCAMNT,
                   CUSTNMBR = o.CUSTNMBR,
                   CUSTNAME = o.CUSTNAME,
                   Pnone01 = o.Pnone01,
                   Pnone02 = o.Pnone02,
                   MobileNo1 = o.MobileNo1,
                   MobileNo2 = o.MobileNo2,
                   POBox = o.POBox,
                   Other01 = o.Other01,
                   Other02 = o.Other02,
                   Address1 = o.Address1,
                   Email01 = o.Email01,
                   Email02 = o.Email02,
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<SOP10310>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetArchiveDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            SOP10310Services invService = new SOP10310Services(KaizenUser);
            DataCollection<SOP10310> L = new DataCollection<SOP10310>();
            string filters = string.Empty;
            string SortMember = "TrxNumber";
            bool IsAscending = false;
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

            L = invService.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetArchiveDataListPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            SOP10310Services invService = new SOP10310Services(KaizenUser);
            DataCollection<SOP10310> L = new DataCollection<SOP10310>();
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
                SortMember = "TrxNumber";
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
        public ActionResult ArchivePopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult SaveArchiveData(SOP10310 SOP10310, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10310Services service = new SOP10310Services(KaizenUser);
            string guid = Guid.NewGuid().ToString();
            guid = RemoveSpecialCharacters(guid).Substring(0, 10);
            SOP10310.TrxNumber = guid;
            return Json(service.AddSOP10310(SOP10310), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateArchiveData(SOP10310 SOP10310, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10310Services service = new SOP10310Services(KaizenUser);
            return Json(service.Update(SOP10310), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteArchiveData(SOP10310 SOP10310, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10310Services service = new SOP10310Services(KaizenUser);
            return Json(service.Delete(SOP10310), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetArchiveLinesByTransactionID(string KaizenPublicKey, string TrxNumber)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10311Services service = new SOP10311Services(KaizenUser);
            IList<SOP10311> o = service.GetByTrxNumber(TrxNumber);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveArchiveLineData(List<SOP10311> SOP10311, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10311Services service = new SOP10311Services(KaizenUser);
            return Json(service.AddSOP10311(SOP10311), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateArchiveLineData(List<SOP10311> SOP10311, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10311Services service = new SOP10311Services(KaizenUser);
            return Json(service.Update(SOP10311), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteArchiveLineData(IList<SOP10311> SOP10311, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10311Services service = new SOP10311Services(KaizenUser);
            return Json(service.Delete(SOP10311), JsonRequestBehavior.AllowGet);
        }

        #endregion

        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public JsonResult GetDataByID(int id)
        {
            try
            {
                string connString = "Data Source=192.168.15.173;Initial Catalog=MasterPortal;Integrated Security=False;User ID=sa;Password=P@ssw0rd;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
                List<HomeModel> list = new List<HomeModel>();
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(connString, "GetDataByID", id);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        HomeModel obj = new HomeModel();
                        obj.ID = System.Convert.ToInt32(row["ID"]);
                        obj.Data = row["Text"].ToString();

                        list.Add(obj);
                    }
                }
                JsonResult result = new JsonResult();
                result.Data = list;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
    public class HomeModel
    {
        public int ID { get; set; }
        public string Data { get; set; }

        public int LastID { get; set; }
        public List<HomeModel> List { get; set; }
    }
}