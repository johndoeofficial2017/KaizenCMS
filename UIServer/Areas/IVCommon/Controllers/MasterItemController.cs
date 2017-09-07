using Kaizen.BusinessLogic.Configuration;
using Kaizen.BusinessLogic.Inventory;
using Kaizen.Inventory;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace UIServer.Areas.IVCommon.Controllers
{
    public class MasterItemController : Controller
    {
        // GET: IVCommon/MasterItem
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
        public ActionResult MainGrid(string KaizenPublicKey, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00026Services oKaizen00011ColumnServices = new Kaizen00026Services(KaizenUser);
            List<Kaizen.Configuration.Kaizen00026> oKaizen00026ColumnList = oKaizen00011ColumnServices.GetFieldsByView(ViewID).ToList();
            List<Kaizen.Configuration.Kaizen00026> temp = oKaizen00026ColumnList.FindAll(ss => !ss.locked);
            if (temp.Count > 0)
                ViewBag.KaizenGridViewColumn = oKaizen00026ColumnList.OrderBy(x => x.ColumnOrder);
            else
                return PartialView("ErrorWindow");
            return PartialView();
        }
        public ActionResult MainGridIV00151(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00100Services service = new IV00100Services(KaizenUser);
            List<IV00151> IV00151ColumnList = service.GetAllIV00151().ToList();
            ViewBag.KaizenGridViewColumnIV00151 = IV00151ColumnList.OrderBy(x => x.WeekDayID);
            return PartialView();
        }
        public ActionResult ErrorWindow(string KaizenPublicKey)
        {
            return PartialView();
        }
        public ActionResult PopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ItemViewPopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult KitPopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ItemGroupPopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00100> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00100
               {
                   ItemID = o.ItemID,
                   ItemName = o.ItemName,
                   ShortDescription = o.ShortDescription,
                   GenericDescription = o.GenericDescription,
                   ItemDescription = o.ItemDescription,
                   PhotoExtension = o.PhotoExtension,
                   BarCode = o.BarCode,
                   IsinActive = o.IsinActive,
                   IsHold = o.IsHold,
                   WarrantyDays = o.WarrantyDays,
                   ShelfLifeDays = o.ShelfLifeDays,
                   ExpiryDays = o.ExpiryDays,
                   UFMGroupID = o.UFMGroupID,
                   UFMID = o.UFMID,
                   DecimalDigitQuantity = o.DecimalDigitQuantity,
                   PriceMethodCode = o.PriceMethodCode,
                   ItemTypeID = o.ItemTypeID,
                   ItemGroupID = o.ItemGroupID,
                   ClassID = o.ClassID,
                   ShippingWeight = o.ShippingWeight,
                   ABCID = o.ABCID,
                   UFMPurchase = string.IsNullOrEmpty(o.UFMPurchase) ? o.UFMID : o.UFMPurchase,
                   BaseUnitPurchase = (o.BaseUnitPurchase.HasValue && o.BaseUnitPurchase.Value > 0) ? o.BaseUnitPurchase : o.BaseUnit,
                   UnitCost = o.UnitCost,
                   LastUnitCost = o.LastUnitCost,
                   PurchaseQTY = o.PurchaseQTY,
                   UFMSale = o.UFMSale,
                   BaseUnitSale = o.BaseUnitSale,
                   PriceLevelCode = o.PriceLevelCode,
                   ValuationMethodID = o.ValuationMethodID,
                   TrackTypeID = o.TrackTypeID,
                   LotNumber = o.LotNumber,
                   IsExpiryDate = o.IsExpiryDate,
                   UnitPrice = o.UnitPrice,
                   PromotedPrice = o.PromotedPrice,
                   LastUnitPrice = o.LastUnitPrice,
                   SaleQTY = o.SaleQTY,
                   PrimaryVendor = o.PrimaryVendor,
                   CountryID = o.CountryID,
                   CreatedBy = o.CreatedBy,
                   CreatedDate = o.CreatedDate,
                   BaseUnit = o.BaseUnit,
                   ItemParent = o.ItemParent,
                   PurchaseFreightID = o.PurchaseFreightID,
                   PurchaseFreightName = o.PurchaseFreightName,
                   PurchaseFreightNumber = o.PurchaseFreightNumber,
                   PurchaseID = o.PurchaseID,
                   PurchaseName = o.PurchaseName,
                   PurchaseNumber = o.PurchaseNumber,
                   PurchaseMisID = o.PurchaseMisID,
                   PurchaseMisName = o.PurchaseMisName,
                   PurchaseMisNumber = o.PurchaseMisNumber,
                   PurchaseTradDiscountID = o.PurchaseTradDiscountID,
                   PurchaseTradDiscountName = o.PurchaseTradDiscountName,
                   PurchaseTradDiscountNumber = o.PurchaseTradDiscountNumber,
                   PurchaseTaxID = o.PurchaseTaxID,
                   PurchaseTaxName = o.PurchaseTaxName,
                   PurchaseTaxNumber = o.PurchaseTaxNumber,
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00100>(),
                    Total = 0
                };
            }
            return result;
        }
        private DataSourceResult CaseListIV00151([DataSourceRequest]DataSourceRequest request, DataCollection<IV00151> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00151
               {
                   CurrencyCode = o.CurrencyCode,
                   DecimalDigit = o.DecimalDigit,
                   EndTimeTo = o.EndTimeTo,
                   ExchangeRate = o.ExchangeRate,
                   IsMultiply = o.IsMultiply,
                   ItemID = o.ItemID,
                   PeriodCount = o.PeriodCount,
                   SiteID = o.SiteID,
                   StartTimeFrom = o.StartTimeFrom,
                   WeekDayID = o.WeekDayID,
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00151>(),
                    Total = 0
                };
            }
            return result;
        }

        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00100Services invService = new IV00100Services(KaizenUser);
            DataCollection<IV00100> L = new DataCollection<IV00100>();
            string filters = string.Empty;
            string SortMember = "ItemID";
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
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetDataListGridIV00151([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00100Services invService = new IV00100Services(KaizenUser);
            DataCollection<IV00151> L = new DataCollection<IV00151>();
            string filters = string.Empty;
            string SortMember = "WeekDayID";
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

            L = invService.GetAllViewBYSQLQueryIV00151(SQLQuery, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseListIV00151(request, L);
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetKitItemListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00100Services invService = new IV00100Services(KaizenUser);
            DataCollection<IV00100> L = new DataCollection<IV00100>();
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
                SortMember = "ItemID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, 4, request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00102Item> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00102Item
               {
                   ItemID = o.ItemID,
                   ItemName = o.ItemName,
                   ShortDescription = o.ShortDescription,
                   GenericDescription = o.GenericDescription,
                   ItemDescription = o.ItemDescription,
                   PhotoExtension = o.PhotoExtension,
                   BarCode = o.BarCode,
                   IsinActive = o.IsinActive,
                   IsHold = o.IsHold,
                   SiteID = o.SiteID,
                   IsExpiryDate = o.IsExpiryDate,
                   BaseUnit = o.BaseUnit,
                   UFMGroupID = o.UFMGroupID,
                   UFMID = o.UFMID,
                   DecimalDigitQuantity = o.DecimalDigitQuantity,
                   UFMPurchase = string.IsNullOrEmpty(o.UFMPurchase) ? o.UFMID : o.UFMPurchase,
                   BaseUnitPurchase = (o.BaseUnitPurchase.HasValue && o.BaseUnitPurchase.Value > 0) ? o.BaseUnitPurchase : o.BaseUnit,
                   UnitCost = o.UnitCost,
                   LastUnitCost = o.LastUnitCost,
                   UFMSale = o.UFMSale,
                   BaseUnitSale = o.BaseUnitSale,
                   LotNumber = o.LotNumber,
                   UnitPrice = o.UnitPrice,
                   PromotedPrice = o.PromotedPrice
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00102Item>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetListItemViewPopUpGridWithSite([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string SiteID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00102ItemServices invService = new IV00102ItemServices(KaizenUser);
            DataCollection<IV00102Item> L = new DataCollection<IV00102Item>();
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
                SortMember = "ItemID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetAllSiteItemsViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, SiteID
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetListItemGroupPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00100Services invService = new IV00100Services(KaizenUser);
            DataCollection<IV00100> L = new DataCollection<IV00100>();
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
                SortMember = "ItemID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetAllGroupItemsViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult SaveData(IV00100 IV00100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00100Services service = new IV00100Services(KaizenUser);
            int startindex;
            if (!string.IsNullOrEmpty(IV00100.PhotoExtension))
            {
                string PhotoPath = @"\\Photo\\ItemPhotoTemp\\" + IV00100.PhotoExtension.Trim();
                startindex = IV00100.PhotoExtension.LastIndexOf('.');
                startindex += 1;
                IV00100.PhotoExtension = IV00100.PhotoExtension.Substring(startindex, IV00100.PhotoExtension.Length - startindex);
                if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                {
                    string Destination = Server.MapPath(@"\\Photo\\ItemPhoto\\" + IV00100.ItemID.Trim() + "." + IV00100.PhotoExtension);
                    if (System.IO.File.Exists(Destination))
                    {
                        System.IO.File.Delete(Destination);
                    }
                    System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                }
            }
            return Json(service.AddIV00100(IV00100), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(IV00100 IV00100, bool PhotoChanged, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00100Services service = new IV00100Services(KaizenUser);
            int startindex;
            if (PhotoChanged)
            {
                if (!string.IsNullOrEmpty(IV00100.PhotoExtension))
                {
                    string PhotoPath = @"\\Photo\\ItemPhotoTemp\\" + IV00100.PhotoExtension.Trim();
                    startindex = IV00100.PhotoExtension.LastIndexOf('.');
                    startindex += 1;
                    IV00100.PhotoExtension = IV00100.PhotoExtension.Substring(startindex, IV00100.PhotoExtension.Length - startindex);
                    if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                    {
                        string Destination = Server.MapPath(@"\\Photo\\ItemPhoto\\" + IV00100.ItemID.Trim() + "." + IV00100.PhotoExtension);
                        if (System.IO.File.Exists(Destination))
                        {
                            System.IO.File.Delete(Destination);
                        }
                        System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                    }
                }
            }
            return Json(service.Update(IV00100), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(IV00100 IV00100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00100Services service = new IV00100Services(KaizenUser);
            return Json(service.Delete(IV00100.ItemID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteItemPhoto(IV00100 IV00100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00100Services service = new IV00100Services(KaizenUser);
            string fileName = IV00100.ItemID.Trim() + "." + IV00100.PhotoExtension.Trim();
            var physicalPath = Path.Combine(Server.MapPath("~/Photo/ItemPhoto"), fileName);
            if (System.IO.File.Exists(physicalPath))
            {
                System.IO.File.Delete(physicalPath);
            }
            IV00100.PhotoExtension = null;
            return Json(service.Update(IV00100), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSingleIV00151(string KaizenPublicKey, int WeekDayID, string SiteID,string ItemID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00100Services service = new IV00100Services(KaizenUser);
            IV00151 L = service.GetSingle(WeekDayID, SiteID, ItemID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSingle(string KaizenPublicKey, string ItemID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00100Services service = new IV00100Services(KaizenUser);
            IV00100 L = service.GetSingle(ItemID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTop10BYItemID(string KaizenPublicKey, [DataSourceRequest]DataSourceRequest request, string ItemID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00100Services invService = new IV00100Services(KaizenUser);
            DataCollection<IV00100> L = new DataCollection<IV00100>();
            if (ItemID != null && !string.IsNullOrEmpty(ItemID))
                L = invService.GetTop10BYItemID(ItemID);
            else
                L = null;
            DataSourceResult result = CaseList(request, L);
            var r = Json(result.Data, JsonRequestBehavior.AllowGet);
            r.MaxJsonLength = int.MaxValue;
            return r;
        }
        public ActionResult GetItemTrackSerial(string KaizenPublicKey, string ItemID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00100Services service = new IV00100Services(KaizenUser);
            IV00106 L = service.GetItemTrackSerial(ItemID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveImageTemp(IEnumerable<HttpPostedFileBase> attachments)
        {
            var fileName = "";
            foreach (var file in attachments)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/Photo/ItemPhotoTemp"), fileName);
                file.SaveAs(destinationPath);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveImagetemp(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Photo/ItemPhotoTemp"), fileName);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }
        public ActionResult FillABCDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00100Services service = new IV00100Services(KaizenUser);
            IList<IV00009> L = service.GetAllIV00009();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region Item Track Serial
        public ActionResult SaveTrackSerialData(IV00106 IV00106, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00106Services service = new IV00106Services(KaizenUser);
            return Json(service.AddIV00106(IV00106), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateTrackSerialData(IV00106 IV00106, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00106Services service = new IV00106Services(KaizenUser);
            return Json(service.Update(IV00106), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteTrackSerialData(IV00106 IV00106, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00106Services service = new IV00106Services(KaizenUser);
            return Json(service.Delete(IV00106), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Item KIT
        public ActionResult ItemKit(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetKitByItemID(string KaizenPublicKey, string ItemID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00100Services service = new IV00100Services(KaizenUser);
            IList<IV00100> o = service.GetAllItemKit(ItemID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveItemKitData(IList<IV00107> IV00107, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00107Services service = new IV00107Services(KaizenUser);
            return Json(service.AddIV00107(IV00107), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateItemKitData(IList<IV00107> IV00107, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00107Services service = new IV00107Services(KaizenUser);
            return Json(service.Update(IV00107), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteItemKitData(IList<IV00107> IV00107, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00107Services service = new IV00107Services(KaizenUser);
            return Json(service.Delete(IV00107), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Item Substitute
        public ActionResult ItemSubstitute(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetSubstituteByItemID(string KaizenPublicKey, string ItemID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00100Services service = new IV00100Services(KaizenUser);
            IList<IV00100> o = service.GetAllbstituteItem(ItemID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveItemSubstituteData(IList<IV00105> IV00105, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00105Services service = new IV00105Services(KaizenUser);
            return Json(service.AddIV00105(IV00105), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateItemSubstituteData(IList<IV00105> IV00105, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00105Services service = new IV00105Services(KaizenUser);
            return Json(service.Update(IV00105), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteItemSubstituteData(IList<IV00105> IV00105, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00105Services service = new IV00105Services(KaizenUser);
            return Json(service.Delete(IV00105), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Item Customer
        public ActionResult ItemCustomer(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ItemCustomerPopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        private DataSourceResult ItemCustomerList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00108> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00108
               {
                   ItemID = o.ItemID,
                   CUSTNMBR = o.CUSTNMBR,
                   ItemName = o.ItemName,
                   CustomerItemID = o.CustomerItemID,
                   ShortDescription = o.ShortDescription,
                   GenericDescription = o.GenericDescription,
                   ItemDescription = o.ItemDescription,
                   BarCode = o.BarCode,
                   CostOfGoodsSold = o.CostOfGoodsSold,
                   CostOfGoodsSoldAccount = o.CostOfGoodsSoldAccount,
                   SalesAcc = o.SalesAcc,
                   SalesAccount = o.SalesAccount,
                   SalesReturnAcc = o.SalesReturnAcc,
                   SalesReturnAccount = o.SalesReturnAccount,
                   MarkdownAcc = o.MarkdownAcc,
                   MarkdownAccount = o.MarkdownAccount,
                   InventoryAcc = o.InventoryAcc,
                   InventoryAccount = o.InventoryAccount,
                   InventoryReturnAcc = o.InventoryReturnAcc,
                   InventoryReturnAccount = o.InventoryReturnAccount,
                   InventoryOffsetAcc = o.InventoryOffsetAcc,
                   InventoryOffsetAccount = o.InventoryOffsetAccount,
                   FreightAcc = o.FreightAcc,
                   FreightAccount = o.FreightAccount,
                   TradeDiscountAcc = o.TradeDiscountAcc,
                   TradeDiscountAccount = o.TradeDiscountAccount,
                   TaxAcc = o.TaxAcc,
                   TaxAccount = o.TaxAccount,
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00108>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetCustomerListGridWithItem([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string ItemID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            IV00108Services serv = new IV00108Services(KaizenUser);
            DataCollection<IV00108> L = new DataCollection<IV00108>();
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
                SortMember = "CUSTNMBR";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(ItemID))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, ItemID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = ItemCustomerList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemListGridWithCustomer([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string CUSTNMBR, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            IV00108Services serv = new IV00108Services(KaizenUser);
            DataCollection<IV00108> L = new DataCollection<IV00108>();
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
                SortMember = "ItemID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(CUSTNMBR))
            {
                L = serv.GetAllItemBYCustomer(SQLQuery, FieldID, FltrOperator, Searchcritaria, CUSTNMBR
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = ItemCustomerList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingleItemCustomer(string KaizenPublicKey, string ItemID, string CUSTNMBR)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00108Services service = new IV00108Services(KaizenUser);
            IV00108 o = service.GetSingle(ItemID, CUSTNMBR);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetByCustomerItem(string KaizenPublicKey, string ItemID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00108Services service = new IV00108Services(KaizenUser);
            IList<IV00108> o = service.GetAllByCustomerItem(ItemID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetByCustomer(string KaizenPublicKey, string CUSTNMBR)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00108Services service = new IV00108Services(KaizenUser);
            IList<IV00108> o = service.GetAllByCustomer(CUSTNMBR);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveItemCustomerData(IV00108 IV00108, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00108Services service = new IV00108Services(KaizenUser);
            return Json(service.AddIV00108(IV00108), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateItemCustomerData(IV00108 IV00108, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00108Services service = new IV00108Services(KaizenUser);
            return Json(service.Update(IV00108), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteItemCustomerData(IV00108 IV00108, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00108Services service = new IV00108Services(KaizenUser);
            return Json(service.Delete(IV00108), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Item Vendor
        public ActionResult ItemVendor(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ItemVendorPopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        private DataSourceResult ItemVendorList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00109> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00109
               {
                   ItemID = o.ItemID,
                   ItemName = o.ItemName,
                   //DecimalDigitQuantity = o.DecimalDigitQuantity,
                   VendorID = o.VendorID,
                   VendorItemID = o.VendorItemID,
                   ShortDescription = o.ShortDescription,
                   GenericDescription = o.GenericDescription,
                   ItemDescription = o.ItemDescription,
                   VendorBarCode = o.VendorBarCode,
                   //CostOfGoodsSold = o.CostOfGoodsSold,
                   //CostOfGoodsSoldAccount = o.CostOfGoodsSoldAccount,
                   //SalesAcc = o.SalesAcc,
                   //SalesAccount = o.SalesAccount,
                   //SalesReturnAcc = o.SalesReturnAcc,
                   //SalesReturnAccount = o.SalesReturnAccount,
                   //MarkdownAcc = o.MarkdownAcc,
                   //MarkdownAccount = o.MarkdownAccount,
                   //InventoryAcc = o.InventoryAcc,
                   //InventoryAccount = o.InventoryAccount,
                   //InventoryReturnAcc = o.InventoryReturnAcc,
                   //InventoryReturnAccount = o.InventoryReturnAccount,
                   //InventoryOffsetAcc = o.InventoryOffsetAcc,
                   //InventoryOffsetAccount = o.InventoryOffsetAccount,
                   //BaseUnitPurchase = o.BaseUnitPurchase,
                   //LastUnitCost = o.LastUnitCost,
                   //PurchaseQTY = o.PurchaseQTY,
                   //UFMGroupID = o.UFMGroupID,
                   //UFMPurchase = o.UFMPurchase,
                   //FreightAccount = o.FreightAccount,
                   //TradeDiscountAcc = o.TradeDiscountAcc,
                   //TradeDiscountAccount = o.TradeDiscountAccount,
                   //TaxAcc = o.TaxAcc,
                   TaxAccount = o.TaxAccount,
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00109>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetVendorListGridWithItem([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string ItemID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            IV00109Services serv = new IV00109Services(KaizenUser);
            DataCollection<IV00109> L = new DataCollection<IV00109>();
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
                SortMember = "VendorID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(ItemID))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, ItemID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = ItemVendorList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemListGridWithVendor([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string VendorID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            IV00109Services serv = new IV00109Services(KaizenUser);
            DataCollection<IV00109> L = new DataCollection<IV00109>();
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
                SortMember = "ItemID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(VendorID))
            {
                L = serv.GetAllItemBYVendor(SQLQuery, FieldID, FltrOperator, Searchcritaria, VendorID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = ItemVendorList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingleItemVendor(string KaizenPublicKey, string ItemID, string VendorID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00109Services service = new IV00109Services(KaizenUser);
            IV00109 o = service.GetSingle(ItemID, VendorID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetByVendorItem(string KaizenPublicKey, string ItemID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00109Services service = new IV00109Services(KaizenUser);
            IList<IV00109> o = service.GetAllByVendorItem(ItemID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetByVendor(string KaizenPublicKey, string VendorID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00109Services service = new IV00109Services(KaizenUser);
            IList<IV00109> o = service.GetAllByVendor(VendorID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveItemVendorData(IV00109 IV00109, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00109Services service = new IV00109Services(KaizenUser);
            return Json(service.AddIV00109(IV00109), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateItemVendorData(IV00109 IV00109, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00109Services service = new IV00109Services(KaizenUser);
            return Json(service.Update(IV00109), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteItemVendorData(IV00109 IV00109, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00109Services service = new IV00109Services(KaizenUser);
            return Json(service.Delete(IV00109), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Item Site
        public ActionResult ItemSite(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        private DataSourceResult ItemSiteList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00102> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00102
               {
                   ItemID = o.ItemID,
                   ItemName = o.ItemName,
                   SiteID = o.SiteID,
                   BinTrack = o.BinTrack,
                   PrimaryVendor = o.PrimaryVendor,
                   ShortDescription = o.ShortDescription,
                   GenericDescription = o.GenericDescription,
                   ItemDescription = o.ItemDescription,
                   BarCode = o.BarCode,
                   PurchaseID = o.PurchaseID,
                   PurchaseName = o.PurchaseName,
                   PurchaseNumber = o.PurchaseNumber,
                   CostOfGoodsSold = o.CostOfGoodsSold,
                   CostOfGoodsSoldAccount = o.CostOfGoodsSoldAccount,
                   SalesAcc = o.SalesAcc,
                   SalesAccount = o.SalesAccount,
                   SalesReturnAcc = o.SalesReturnAcc,
                   SalesReturnAccount = o.SalesReturnAccount,
                   MarkdownAcc = o.MarkdownAcc,
                   MarkdownAccount = o.MarkdownAccount,
                   InventoryAcc = o.InventoryAcc,
                   InventoryAccount = o.InventoryAccount,
                   InventoryReturnAcc = o.InventoryReturnAcc,
                   InventoryReturnAccount = o.InventoryReturnAccount,
                   InventoryOffsetAcc = o.InventoryOffsetAcc,
                   InventoryOffsetAccount = o.InventoryOffsetAccount,
                   FreightAcc = o.FreightAcc,
                   FreightAccount = o.FreightAccount,
                   TradeDiscountAcc = o.TradeDiscountAcc,
                   TradeDiscountAccount = o.TradeDiscountAccount,
                   TaxAcc = o.TaxAcc,
                   TaxAccount = o.TaxAccount,
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
        public ActionResult GetSiteListGridWithItem([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string ItemID, string Searchcritaria)
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

            if (!string.IsNullOrEmpty(ItemID))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, ItemID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = ItemSiteList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingleItemSite(string KaizenPublicKey, string ItemID, string SiteID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00102Services service = new IV00102Services(KaizenUser);
            IV00102 o = service.GetSingle(ItemID, SiteID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBySiteItem(string KaizenPublicKey, string ItemID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00102Services service = new IV00102Services(KaizenUser);
            IList<IV00102> o = service.GetAllBySiteItem(ItemID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBySite(string KaizenPublicKey, string SiteID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00102Services service = new IV00102Services(KaizenUser);
            IList<IV00102> o = service.GetAllBySite(SiteID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveItemSiteData(IV00102 IV00102, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00102Services service = new IV00102Services(KaizenUser);
            return Json(service.AddIV00102(IV00102), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateItemSiteData(IV00102 IV00102, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00102Services service = new IV00102Services(KaizenUser);
            return Json(service.Update(IV00102), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteItemSiteData(IV00102 IV00102, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00102Services service = new IV00102Services(KaizenUser);
            return Json(service.Delete(IV00102), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Item Account
        public ActionResult ItemAccount(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult GetSingleItemAccount(string KaizenPublicKey, string ItemID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00101Services service = new IV00101Services(KaizenUser);
            IV00101 o = service.GetGL(ItemID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveItemAccountData(IV00101 IV00101, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00101Services service = new IV00101Services(KaizenUser);
            return Json(service.AddIV00101(IV00101), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateItemAccountData(IV00101 IV00101, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00101Services service = new IV00101Services(KaizenUser);
            return Json(service.Update(IV00101), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteItemAccountData(IV00101 IV00101, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00101Services service = new IV00101Services(KaizenUser);
            return Json(service.Delete(IV00101), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Item Price
        public ActionResult ItemPrice(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        private DataSourceResult ItemPriceList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00006> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00006
               {
                   ItemID = o.ItemID,
                   PriceLineID = o.PriceLineID,
                   PriceLevelCode = o.PriceLevelCode,
                   PriceGroupID = o.PriceGroupID,
                   UFMID = o.UFMID,
                   UFMGroupID = o.UFMGroupID,
                   CurrencyCode = o.CurrencyCode,
                   DecimalDigit = o.DecimalDigit,
                   QuantityFrom = o.QuantityFrom,
                   QuantityTo = o.QuantityTo,
                   DecimalDigitQuantity = o.DecimalDigitQuantity,
                   PriceValue = o.PriceValue
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00006>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetPriceListGridWithItem([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string ItemID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            IV00006Services serv = new IV00006Services(KaizenUser);
            DataCollection<IV00006> L = new DataCollection<IV00006>();
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
                SortMember = "PriceLineID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(ItemID))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, ItemID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = ItemPriceList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingleItemPrice(string KaizenPublicKey, int PriceLineID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00006Services service = new IV00006Services(KaizenUser);
            IV00006 o = service.GetSingle(PriceLineID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveItemPriceData(IV00006 IV00006, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00006Services service = new IV00006Services(KaizenUser);
            return Json(service.AddIV00006(IV00006), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateItemPriceData(IV00006 IV00006, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00006Services service = new IV00006Services(KaizenUser);
            return Json(service.Update(IV00006), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteItemPriceData(IV00006 IV00006, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00006Services service = new IV00006Services(KaizenUser);
            return Json(service.Delete(IV00006), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region LOT View
        public ActionResult LotView(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        private DataSourceResult CaseItemsList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00140> L)
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
                   ItemQuantity = o.ItemQuantity,
                   LotRowID = o.LotRowID,
                   LotSource = o.LotSource
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
        public ActionResult GetDataListItemsGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
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

        #endregion
        public string GetNextItemID(string ClassID, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return "";
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return "";
            if (!User.Identity.IsAuthenticated) return "";
            IV00100Services service = new IV00100Services(KaizenUser);

            return service.GetNextTransactionID(ClassID);
        }
        public ActionResult GetGLPurchase(string ItemID, string SiteID, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return null;
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return null;
            if (!User.Identity.IsAuthenticated) return null;
            IV00100Services service = new IV00100Services(KaizenUser);
            return Json(service.GetGLPurchase(ItemID, SiteID), JsonRequestBehavior.AllowGet);
        }

        #region--> Week Price | IV00150
        public ActionResult WeekPrice(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult SaveWeekData(string KaizenPublicKey, IList<IV00150> IV00150List)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00100Services service = new IV00100Services(KaizenUser);
            var L = service.AddIV00150(IV00150List);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateWeekData(string KaizenPublicKey, IList<IV00150> IV00150List)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00100Services service = new IV00100Services(KaizenUser);
            var L = service.UpdateIV00150(IV00150List);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadIV00150UpdateData(string KaizenPublicKey, string SiteID, string ItemID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00100Services service = new IV00100Services(KaizenUser);
            var L = service.LoadIV00150UpdateData(SiteID, ItemID);
            var data = L.Where(m => m.ItemID == ItemID && m.SiteID == SiteID).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Price Minutes
        public ActionResult PriceMinutes(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult CreatePriceMinutes(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetWeekDays(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00100Services service = new IV00100Services(KaizenUser);
            IList<IV00022> L = service.GetWeekDays();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveDataPriceMinutes(IV00151 IV00151, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00100Services service = new IV00100Services(KaizenUser);
            
            return Json(service.AddIV00151(IV00151), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateDataPriceMinutes(IV00151 IV00151, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00100Services service = new IV00100Services(KaizenUser);

            return Json(service.UpdateIV00151(IV00151), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteDataPriceMinutes(IV00151 IV00151, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00100Services service = new IV00100Services(KaizenUser);

            return Json(service.DeleteIV00151(IV00151), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}