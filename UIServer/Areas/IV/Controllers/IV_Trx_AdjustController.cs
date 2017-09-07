using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.Inventory;
using Kaizen.BusinessLogic.Inventory;
using Kaizen.BusinessLogic.Configuration;

namespace UIServer.Areas.IV.Controllers
{
    public class IV_Trx_AdjustController : Controller
    {
        // GET: IV_Trx_Adjust
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult Create(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00200> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00200
               {
                   TransactionID = o.TransactionID,
                   TransactionTypeID = o.TransactionTypeID,
                   SiteID = o.SiteID,
                   DOCAMNT = o.DOCAMNT,
                   ReasonID = o.ReasonID,
                   BatchID = o.BatchID,
                   BatchSourceID = o.BatchSourceID,
                   BinTrack = o.BinTrack,
                   TransactionDate = o.TransactionDate,
                   TransactionNote = o.TransactionNote
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00201>(),
                    Total = 0
                };
            }
            return result;
        }
        public JsonResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            IV00200Services serv = new IV00200Services(KaizenUser);
            DataCollection<IV00200> L = new DataCollection<IV00200>();
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
                SortMember = "TransactionID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }


        #endregion

        public ActionResult SaveData(IV00200 IV00200, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00200Services service = new IV00200Services(KaizenUser);
            IV00200.UserCode = KaizenUser.UserName;
            IV00200.EntryDate = System.DateTime.Now;
            return Json(service.AddIV00200(IV00200), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(IV00200 IV00200, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00200Services service = new IV00200Services(KaizenUser);
            return Json(service.Update(IV00200), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(IV00200 IV00200, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00200Services service = new IV00200Services(KaizenUser);
            return Json(service.Delete(IV00200), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string TransactionID, int TransactionTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00200Services service = new IV00200Services(KaizenUser);
            IV00200 L = service.GetSingle(TransactionID, TransactionTypeID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region Transaction Lines
        public ActionResult GetLinesByTransactionID(string KaizenPublicKey, string TransactionID, int TransactionTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00201Services service = new IV00201Services(KaizenUser);
            IList<IV00201> o = service.GetByTransactionID(TransactionID, TransactionTypeID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveLineData(List<IV00201> IV00201, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00201Services service = new IV00201Services(KaizenUser);
            return Json(service.AddIV00201(IV00201), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateLineData(List<IV00201> IV00201, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00201Services service = new IV00201Services(KaizenUser);
            return Json(service.Update(IV00201), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteLineData(IList<IV00201> IV00201, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00201Services service = new IV00201Services(KaizenUser);
            return Json(service.Delete(IV00201), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Line Bin Quantity
        public ActionResult GetLinesBin(string KaizenPublicKey, int TransactionTypeID, string TransactionID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00201Services service = new IV00201Services(KaizenUser);
            IList<IV_205Temp> o = service.GetLines(TransactionTypeID, TransactionID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateLineBinData(List<IV00204> IV00204, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00204Services service = new IV00204Services(KaizenUser);
            return Json(service.Update(IV00204), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteLineBinData(IList<IV00204> IV00204, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00204Services service = new IV00204Services(KaizenUser);
            return Json(service.Delete(IV00204), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Account Distribution
        public ActionResult GetDistributionByTransaction(string KaizenPublicKey, string TransactionID, int TransactionTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00206Services service = new IV00206Services(KaizenUser);
            IList<IV00206> o = service.GetByTransaction(TransactionID, TransactionTypeID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateLineDistributionData(List<IV00206> IV00206, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00206Services service = new IV00206Services(KaizenUser);
            return Json(service.Update(IV00206), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteLineDistributionData(IList<IV00206> IV00206, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00206Services service = new IV00206Services(KaizenUser);
            return Json(service.Delete(IV00206), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Lot Line
        public ActionResult LotLinePopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00140> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00140
               {
                   LOTLineCode = o.LOTLineCode,
                   SiteID = o.SiteID,
                   ItemID = o.ItemID,
                   LotNumber = o.LotNumber,
                   IsExpiryDate = o.IsExpiryDate,
                   ExpiryDate = o.ExpiryDate,
                   BarCode = o.BarCode,
                   ItemQuantity = o.ItemQuantity
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00140>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetLotLineListPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00140Services invService = new IV00140Services(KaizenUser);
            DataCollection<IV00140> L = new DataCollection<IV00140>();
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
                SortMember = "LOTLineCode";
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

        public ActionResult GetLotLineListPopUpGridWithItemSite([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria, string ItemID, string SiteID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00140Services invService = new IV00140Services(KaizenUser);
            DataCollection<IV00140> L = new DataCollection<IV00140>();
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
                SortMember = "LOTLineCode";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, ItemID, SiteID
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Bin Line
        public ActionResult BinLinePopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00130> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00130
               {
                   BinID = o.BinID,
                   SiteID = o.SiteID,
                   ItemID = o.ItemID,
                   BinName = o.BinName,
                   IsBinGroup = o.IsBinGroup,
                   ItemQuantity = o.ItemQuantity
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00130>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetBinLineListPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00130Services invService = new IV00130Services(KaizenUser);
            DataCollection<IV00130> L = new DataCollection<IV00130>();
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
                SortMember = "BinID";
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

        public ActionResult GetBinLineListPopUpGridWithItemSite([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria, string ItemID, string SiteID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00130Services invService = new IV00130Services(KaizenUser);
            DataCollection<IV00130> L = new DataCollection<IV00130>();
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
                SortMember = "BinID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetAllSiteItemBinsViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, SiteID, ItemID
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubBinesByBinID(string KaizenPublicKey, string BinID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00131Services service = new IV00131Services(KaizenUser);
            IList<IV00131> o = service.GetAllByBinID(BinID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult GetNextTransactionID(string KaizenPublicKey, int TransactionTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            IV00200Services service = new IV00200Services(KaizenUser);
            string TransactionID = service.GetNextTransactionID(TransactionTypeID);
            if (string.IsNullOrEmpty(TransactionID))
            {
                KaizenJson resultKaizenJson = new KaizenJson();
                resultKaizenJson.Status = false;
                resultKaizenJson.Massage = "Fail";
                return Json(resultKaizenJson, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(TransactionID, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PostTransaction(string KaizenPublicKey, int TransactionTypeID, string TransactionID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);
            IV00200Services service = new IV00200Services(KaizenUser);
            return Json(service.PostTransaction(TransactionTypeID, TransactionID), JsonRequestBehavior.AllowGet);
        }
    }
}