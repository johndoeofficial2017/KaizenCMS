using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.SOP;
using Kaizen.BusinessLogic.SOP;
using Kaizen.BusinessLogic.Configuration;
using System.Collections.Generic;

namespace UIServer.Areas.SOP.Controllers
{
    public class SOP_Trx_DeliveryOrderController : Controller
    {
        // GET: SOP_Trx_DeliveryOrder
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

        #region Grid Actions
        public ActionResult MainGrid(string KaizenPublicKey, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00026Services oKaizen00011ColumnServices = new Kaizen00026Services(KaizenUser);
            List<Kaizen.Configuration.Kaizen00026> oKaizen00026ColumnList = oKaizen00011ColumnServices.GetFieldsByView(ViewID).ToList();
            ViewBag.KaizenGridViewLockedColumn = oKaizen00026ColumnList.FindAll(ss => ss.locked).OrderBy(x => x.ColumnOrder);
            List<Kaizen.Configuration.Kaizen00026> temp = oKaizen00026ColumnList.FindAll(ss => !ss.locked);
            if (temp.Count > 0)
                ViewBag.KaizenGridViewColumn = oKaizen00026ColumnList.OrderBy(x => x.ColumnOrder);
            else
                return PartialView("ErrorWindow");
            return PartialView();
        }
        public ActionResult ErrorWindow(string KaizenPublicKey, int ViewID)
        {
            return PartialView();
        }

        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<SOP10500> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new SOP10500
               {
                   SOPNUMBE = o.SOPNUMBE,
                   SOPNUMBEORG = o.SOPNUMBEORG,
                   BatchID = o.BatchID,
                   ProjectID = o.ProjectID,
                   PaymentTermID = o.PaymentTermID,
                   DOCDATE = o.DOCDATE,
                   DOCAMNT = o.DOCAMNT,
                   DOCAMNTCollected = o.DOCAMNTCollected,
                   CUSTNMBR = o.CUSTNMBR,
                   CUSTNAME = o.CUSTNAME,
                   BillAddressTypeCode = o.BillAddressTypeCode,
                   BillAddressName = o.BillAddressName,
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
                   CustomerPoNumber = o.CustomerPoNumber,
                   PriceLevelCode = o.PriceLevelCode,
                   ReqShipDate = o.ReqShipDate,
                   ShipDate = o.ShipDate,
                   SiteID = o.SiteID,
                   BinTrack = o.BinTrack,
                   CurrencyCode = o.CurrencyCode,
                   ExchangeTableID = o.ExchangeTableID,
                   ExchangeRate = o.ExchangeRate,
                   IsMultiply = o.IsMultiply,
                   DecimalDigit = o.DecimalDigit,
                   Territory = o.Territory,
                   SalePersonID = o.SalePersonID,
                   TaxAMT = o.TaxAMT,
                   Markdown = o.Markdown,
                   Freight = o.Freight,
                   Miscellaneous = o.Miscellaneous,
                   TradeDiscount = o.TradeDiscount,
                   TrxComments = o.TrxComments
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<SOP10500>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            SOP10500Services invService = new SOP10500Services(KaizenUser);
            DataCollection<SOP10500> L = new DataCollection<SOP10500>();
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

        #endregion

        public ActionResult SaveData(SOP10500 SOP10500, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10500Services service = new SOP10500Services(KaizenUser);
            return Json(service.AddSOP10500(SOP10500), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(SOP10500 SOP10500, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10500Services service = new SOP10500Services(KaizenUser);
            return Json(service.Update(SOP10500), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(SOP10500 SOP10500, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10500Services service = new SOP10500Services(KaizenUser);
            return Json(service.Delete(SOP10500), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string SOPNUMBE)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10500Services service = new SOP10500Services(KaizenUser);
            SOP10500 L = service.GetSingle(SOPNUMBE);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region Transaction Lines
        public ActionResult GetLinesByTransactionID(string KaizenPublicKey, string SOPNUMBE)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10501Services service = new SOP10501Services(KaizenUser);
            IList<SOP10501> o = service.GetListBySOPNUMBE(SOPNUMBE);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveLineData(List<SOP10501> SOP10501, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10501Services service = new SOP10501Services(KaizenUser);
            return Json(service.AddSOP10501(SOP10501), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateLineData(List<SOP10501> SOP10501, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10501Services service = new SOP10501Services(KaizenUser);
            return Json(service.Update(SOP10501), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteLineData(IList<SOP10501> SOP10501, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10501Services service = new SOP10501Services(KaizenUser);
            return Json(service.Delete(SOP10501), JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveLine(string SOPNUMBE,int ItemLineIndex, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10501Services service = new SOP10501Services(KaizenUser);
            return Json(service.Delete(SOPNUMBE, ItemLineIndex), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Line Bin Quantity
        public ActionResult GetLineBinsByLineID(string KaizenPublicKey, int LineID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10504Services service = new SOP10504Services(KaizenUser);
            IList<SOP10504> o = service.GetListByLineID(LineID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateLineBinData(List<SOP10504> SOP10504, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10504Services service = new SOP10504Services(KaizenUser);
            return Json(service.Update(SOP10504), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteLineBinData(IList<SOP10504> SOP10504, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10504Services service = new SOP10504Services(KaizenUser);
            return Json(service.Delete(SOP10504), JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}