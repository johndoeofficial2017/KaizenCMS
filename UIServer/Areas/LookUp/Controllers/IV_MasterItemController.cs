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

namespace UIServer.Areas.LookUp.Controllers
{
    public class IV_MasterItemController : Controller
    {
        // GET: LookUp/IV_MasterItem
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

            L = invService.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
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
    }
}