using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.Inventory;
using Kaizen.BusinessLogic.Configuration;
using Kaizen.BusinessLogic.Inventory;

namespace UIServer.Areas.IVConfiguration.Controllers
{
    public class ItemClassController : Controller
    {
        // GET: IVConfiguration/ItemClass
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
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<IV00001> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new IV00001
               {
                   ClassID = o.ClassID,
                   GroupName = o.GroupName
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<IV00001>(),
                    Total = 0
                };
            }
            return result;
        }
        public JsonResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            IV00001Services serv = new IV00001Services(KaizenUser);
            DataCollection<IV00001> L = new DataCollection<IV00001>();
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
                SortMember = "ClassID";
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
        public ActionResult SaveData(IV00001 IV00001, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00001Services service = new IV00001Services(KaizenUser);
            return Json(service.AddIV00001(IV00001), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(IV00001 IV00001, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00001Services service = new IV00001Services(KaizenUser);
            return Json(service.Update(IV00001), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(IV00001 IV00001, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00001Services service = new IV00001Services(KaizenUser);
            return Json(service.Delete(IV00001), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string ClassID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            IV00001Services service = new IV00001Services(KaizenUser);
            IV00001 L = service.GetSingle(ClassID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00001Services service = new IV00001Services(KaizenUser);
            IList<IV00001> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region--> Week Price IV00030
        public ActionResult WeekPrice(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult LoadIV00001ClassDetails(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00001Services service = new IV00001Services(KaizenUser);
            IList<IV00001> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadIV00022WeekDetails(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00022Services service = new IV00022Services(KaizenUser);
            IList<IV00022> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveWeekData(string KaizenPublicKey,IList<IV00030> IV00030List)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00001Services service = new IV00001Services(KaizenUser);
            var L = service.AddIV00030(IV00030List);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateWeekData(string KaizenPublicKey, IList<IV00030> IV00030List)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00001Services service = new IV00001Services(KaizenUser);
            var L = service.UpdateIV00030(IV00030List);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllIV00030byClassID(string KaizenPublicKey, string ClassID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            IV00001Services service = new IV00001Services(KaizenUser);
            var L = service.GetAllIV00030byClassID(ClassID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}