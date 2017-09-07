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
    public class IV_LOTController : Controller
    {
        // GET: IV_LOT
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
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00010> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00010
               {
                   LotName = o.LotName,
                   LotNumber = o.LotNumber,
                   IsExpiryDate = o.IsExpiryDate
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00010>(),
                    Total = 0
                };
            }
            return result;
        }
        public JsonResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            IV00010Services serv = new IV00010Services(KaizenUser);
            DataCollection<IV00010> L = new DataCollection<IV00010>();
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
                SortMember = "LotNumber";
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

        public ActionResult SaveData(IV00010 IV00010, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00010Services service = new IV00010Services(KaizenUser);
            return Json(service.AddIV00010(IV00010), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(IV00010 IV00010, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00010Services service = new IV00010Services(KaizenUser);
            return Json(service.Update(IV00010), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(IV00010 IV00010, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00010Services service = new IV00010Services(KaizenUser);
            return Json(service.Delete(IV00010), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string LotNumber)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00010Services service = new IV00010Services(KaizenUser);
            IV00010 L = service.GetSingle(LotNumber);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00010Services service = new IV00010Services(KaizenUser);
            IList<IV00010> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region LOT Columns
        public ActionResult LotColumn(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult GetByLotNumber(string KaizenPublicKey, string LotNumber)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00013Services service = new IV00013Services(KaizenUser);
            IList<IV00013> L = service.GetAllLotColumn(LotNumber);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        private DataSourceResult LotColumnList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00013> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00013
               {
                   CollCode = o.CollCode,
                   CollName = o.CollName,
                   CollType = o.CollType,
                   LotNumber = o.LotNumber,
                   LookupFrom = o.LookupFrom,
                   LookupLotName = o.LookupLotName,
                   LookupLotNumber = o.LookupLotNumber
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00013>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetLotColumnListGridWithLot([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string LotNumber, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            IV00013Services serv = new IV00013Services(KaizenUser);
            DataCollection<IV00013> L = new DataCollection<IV00013>();
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
                SortMember = "CollCode";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(LotNumber))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, LotNumber
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = LotColumnList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveLotColumnData(IV00013 IV00013, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00013Services service = new IV00013Services(KaizenUser);
            return Json(service.AddIV00013(IV00013), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateLotColumnData(IV00013 IV00013, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00013Services service = new IV00013Services(KaizenUser);
            return Json(service.Update(IV00013), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteLotColumnData(IV00013 IV00013, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00013Services service = new IV00013Services(KaizenUser);
            return Json(service.Delete(IV00013), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}