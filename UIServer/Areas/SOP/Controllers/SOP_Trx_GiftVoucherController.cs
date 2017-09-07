using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kaizen.BusinessLogic.Configuration;
using Kendo.Mvc.UI;
using Kaizen.SOP;
using Kendo.Mvc.Extensions;
using Kaizen.BusinessLogic.SOP;
using Kendo.Mvc;
using System;

namespace UIServer.Areas.SOP.Controllers
{
    public class SOP_Trx_GiftVoucherController : Controller
    {
        // GET: SOP_Trx_GiftVoucher
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
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<SOP10305> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new SOP10305
               {
                   VoucherTrxNumber = o.VoucherTrxNumber,
                   VoucherAMT = o.VoucherAMT,
                   VoucherCount = o.VoucherCount,
                   VoucherStartDate = o.VoucherStartDate,
                   VoucherEndDate = o.VoucherEndDate,
                   UserName = o.UserName,
                   EntryDate = o.EntryDate,
                   BarcodPrefix = o.BarcodPrefix,
                   BarcodLenth = o.BarcodLenth,
                   BarcodStartNumber = o.BarcodStartNumber,
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
                    Data = new List<SOP10305>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            SOP10305Services invService = new SOP10305Services(KaizenUser);
            DataCollection<SOP10305> L = new DataCollection<SOP10305>();
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
                SortMember = "VoucherTrxNumber";
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

        public ActionResult SaveData(SOP10305 SOP10305, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10305Services service = new SOP10305Services(KaizenUser);
            SOP10305.UserName = KaizenUser.UserName;
            SOP10305.EntryDate = DateTime.Now;
            return Json(service.AddSOP10305(SOP10305), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(SOP10305 SOP10305, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10305Services service = new SOP10305Services(KaizenUser);
            return Json(service.Update(SOP10305), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(SOP10305 SOP10305, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10305Services service = new SOP10305Services(KaizenUser);
            return Json(service.Delete(SOP10305), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string VoucherTrxNumber)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP10305Services service = new SOP10305Services(KaizenUser);
            SOP10305 L = service.GetSingle(VoucherTrxNumber);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
    }
}