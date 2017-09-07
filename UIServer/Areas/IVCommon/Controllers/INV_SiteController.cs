using Kaizen.BusinessLogic.Configuration;
using Kaizen.BusinessLogic.Inventory;
using Kaizen.Inventory;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Areas.IVCommon.Controllers
{
    public class INV_SiteController : Controller
    {
        // GET: INV_Site
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
        //public ActionResult PopUp(string KaizenPublicKey)
        //{
        //    Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
        //    if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
        //    return PartialView();
        //}
        public ActionResult PopUpByItem(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }


        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00011> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00011
               {
                   SiteID = o.SiteID,
                   SiteName = o.SiteName,
                   Address = o.Address,
                   BinTrack = o.BinTrack,
                   Phone01 = o.Phone01,
                   Phone02 = o.Phone02,
                   Phone03 = o.Phone03,
                   Phone04 = o.Phone04,
                   Phone05 = o.Phone05,
                   SiteManger = o.SiteManger,
                   SiteDescription = o.SiteDescription

               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00011>(),
                    Total = 0
                };
            }
            return result;
        }

        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00011Services invService = new IV00011Services(KaizenUser);
            DataCollection<IV00011> L = new DataCollection<IV00011>();
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
                SortMember = "SiteID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
     
        public ActionResult GetListPopUpGridByItem([DataSourceRequest]DataSourceRequest request, string ItemID, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00011Services invService = new IV00011Services(KaizenUser);
            DataCollection<IV00011> L = new DataCollection<IV00011>();
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
                SortMember = "SiteID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, ItemID
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult SaveData(IV00011 IV00011, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00011Services service = new IV00011Services(KaizenUser);
            return Json(service.AddIV00011(IV00011), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(IV00011 IV00011, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00011Services service = new IV00011Services(KaizenUser);
            return Json(service.Update(IV00011), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(IV00011 IV00011, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00011Services service = new IV00011Services(KaizenUser);
            return Json(service.Delete(IV00011), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string SiteID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00011Services service = new IV00011Services(KaizenUser);
            IV00011 L = service.GetSingle(SiteID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region Site Bin Track
        public ActionResult SetupBin(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        private DataSourceResult BinCaseList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00012> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00012
               {
                   SiteID = o.SiteID,
                   BinID = o.BinID,
                   BinName = o.BinName
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00012>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetBinListGridWithSite([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string SiteID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            IV00012Services serv = new IV00012Services(KaizenUser);
            DataCollection<IV00012> L = new DataCollection<IV00012>();
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

            if (!string.IsNullOrEmpty(SiteID))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, SiteID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = BinCaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveBinData(IV00012 IV00012, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00012Services service = new IV00012Services(KaizenUser);
            return Json(service.AddIV00012(IV00012), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateBinData(IV00012 IV00012, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00012Services service = new IV00012Services(KaizenUser);
            return Json(service.Update(IV00012), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteBinData(IV00012 IV00012, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00012Services service = new IV00012Services(KaizenUser);
            return Json(service.Delete(IV00012), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBinSingle(string KaizenPublicKey, string BinID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00012Services service = new IV00012Services(KaizenUser);
            IV00012 L = service.GetSingle(BinID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBinBySiteID(string KaizenPublicKey, string SiteID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00012Services service = new IV00012Services(KaizenUser);
            IList<IV00012> L = service.GetBySiteID(SiteID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBinGroupBySiteID(string KaizenPublicKey, string SiteID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00012Services service = new IV00012Services(KaizenUser);
            IList<IV00012> L = service.GetBinGroupBySiteID(SiteID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Site Sub Bin Track
        public ActionResult SetupSubBin(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        private DataSourceResult SubBinCaseList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00021> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00021
               {
                   SubBinID = o.SubBinID,
                   BinID = o.BinID,
                   SubBinName = o.SubBinName
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00021>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetSubBinListGridWithBin([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string BinID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            IV00021Services serv = new IV00021Services(KaizenUser);
            DataCollection<IV00021> L = new DataCollection<IV00021>();
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
                SortMember = "SubBinID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(BinID.ToString()))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, BinID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = SubBinCaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveSubBinData(IV00021 IV00021, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00021Services service = new IV00021Services(KaizenUser);
            return Json(service.AddIV00021(IV00021), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateSubBinData(IV00021 IV00021, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00021Services service = new IV00021Services(KaizenUser);
            return Json(service.Update(IV00021), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteSubBinData(IV00021 IV00021, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00021Services service = new IV00021Services(KaizenUser);
            return Json(service.Delete(IV00021), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubBinSingle(string KaizenPublicKey, string SubBinID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00021Services service = new IV00021Services(KaizenUser);
            IV00021 L = service.GetSingle(SubBinID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSubBinByBinID(string KaizenPublicKey, string BinID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00021Services service = new IV00021Services(KaizenUser);
            IList<IV00021> L = service.GetByBinID(BinID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Site Items
        public ActionResult SiteItems(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        private DataSourceResult ItemCaseList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00102> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00102
               {
                   SiteID = o.SiteID,
                   ItemID = o.ItemID,
                   ItemName = o.ItemName,
                   PrimaryVendor = o.PrimaryVendor,
                   ShortDescription = o.ShortDescription,
                   GenericDescription = o.GenericDescription,
                   ItemDescription = o.ItemDescription,
                   BarCode = o.BarCode,
                   BinTrack = o.BinTrack,
                   CostOfGoodsSold = o.CostOfGoodsSold,
                   SalesAcc = o.SalesAcc,
                   SalesReturnAcc = o.SalesReturnAcc,
                   MarkdownAcc = o.MarkdownAcc,
                   InventoryAcc = o.InventoryAcc,
                   InventoryReturnAcc = o.InventoryReturnAcc,
                   InventoryOffsetAcc = o.InventoryOffsetAcc,
                   FreightAcc = o.FreightAcc,
                   TradeDiscountAcc = o.TradeDiscountAcc,
                   TaxAcc = o.TaxAcc,
                   Inactive = o.Inactive
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00102>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetItemsListGridWithSite([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string SiteID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            IV00102Services serv = new IV00102Services(KaizenUser);
            DataCollection<IV00102> L = new DataCollection<IV00102>();
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
                SortMember = "SiteID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(SiteID))
            {
                L = serv.GetAllSiteItemsViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, SiteID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = ItemCaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Site Item Bins
        public ActionResult SiteItemBins(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        private DataSourceResult ItemBinCaseList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00130> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00130
               {
                   SiteID = o.SiteID,
                   ItemID = o.ItemID,
                   BinID = o.BinID,
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
        public ActionResult GetBinsListGridWithSiteItem([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string SiteID, string ItemID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            IV00130Services serv = new IV00130Services(KaizenUser);
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
                SortMember = "SiteID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(SiteID) && !string.IsNullOrEmpty(ItemID))
            {
                L = serv.GetAllSiteItemBinsViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, SiteID
                  , ItemID, request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = ItemBinCaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Site Item SubBins
        public ActionResult SiteItemSubBins(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        private DataSourceResult ItemSubBinCaseList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00131> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00131
               {
                   SiteID = o.SiteID,
                   ItemID = o.ItemID,
                   BinID = o.BinID,
                   SubBinID = o.SubBinID,
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
                    Data = new List<IV00131>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetSubBinsListGridWithSiteItem([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string SiteID, string ItemID, string BinID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            IV00131Services serv = new IV00131Services(KaizenUser);
            DataCollection<IV00131> L = new DataCollection<IV00131>();
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
                SortMember = "SiteID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(SiteID) && !string.IsNullOrEmpty(ItemID) && !string.IsNullOrEmpty(BinID))
            {
                L = serv.GetAllSiteItemSubBinsViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, SiteID
                  , ItemID, BinID, request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = ItemSubBinCaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}