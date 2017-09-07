using Kaizen.BusinessLogic.Configuration;
using Kaizen.Configuration;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Controllers
{
    public class Admin_ScreenController : Controller
    {
        // GET: Admin_Screen
        public ActionResult Index(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult Create(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ScreenField(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult AddProperty(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult AddField(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult AddDropDownField(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ScreenView(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<Kaizen00010> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new Kaizen00010
               {
                   ScreenID = o.ScreenID,
                   ScreenName = o.ScreenName,
                   ScreenDescription = o.ScreenDescription,
                   HasSubScreen = o.HasSubScreen
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<Kaizen00010>(),
                    Total = 0
                };
            }
            return result;
        }

        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00010Services invService = new Kaizen00010Services(KaizenUser);
            DataCollection<Kaizen00010> L = new DataCollection<Kaizen00010>();
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
                SortMember = "ScreenID";
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

        public ActionResult SaveData(Kaizen00010 Kaizen00010, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00010Services service = new Kaizen00010Services(KaizenUser);
            return Json(service.AddKaizen00010(Kaizen00010), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(Kaizen00010 Kaizen00010, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00010Services service = new Kaizen00010Services(KaizenUser);
            return Json(service.Update(Kaizen00010), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(Kaizen00010 Kaizen00010, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00010Services service = new Kaizen00010Services(KaizenUser);
            return Json(service.Delete(Kaizen00010), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, int ScreenID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00010Services service = new Kaizen00010Services(KaizenUser);
            Kaizen00010 L = service.GetSingle(ScreenID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00010Services service = new Kaizen00010Services(KaizenUser);
            IList<Kaizen00010> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetScreensByModule(string KaizenPublicKey,int ModuleID)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00010Services service = new Kaizen00010Services(KaizenUser);
            IList<Kaizen00010> L = service.GetAllScreensByModule(ModuleID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region Screen Fields
        public ActionResult GetFieldsByScreen(string KaizenPublicKey, int ScreenID)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00025Services srv = new Kaizen00025Services(KaizenUser);
            List<Kaizen00025> list = srv.GetFieldsByScreen(ScreenID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveScreenFieldData(IList<Kaizen00025> Kaizen00025, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00025Services service = new Kaizen00025Services(KaizenUser);
            return Json(service.AddKaizen00025(Kaizen00025), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateScreenFieldData(IList<Kaizen00025> Kaizen00025, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00025Services service = new Kaizen00025Services(KaizenUser);
            return Json(service.Update(Kaizen00025), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteScreenFieldData(IList<Kaizen00025> Kaizen00025, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00025Services service = new Kaizen00025Services(KaizenUser);
            return Json(service.Delete(Kaizen00025), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Screen Field Grid View
        public ActionResult GetFieldsByView(string KaizenPublicKey, int ViewID)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00026Services srv = new Kaizen00026Services(KaizenUser);
            List<Kaizen00026> list = srv.GetFieldsByView(ViewID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveKaizen00026Data(IList<Kaizen00026> Kaizen00026, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00026Services service = new Kaizen00026Services(KaizenUser);
            return Json(service.AddKaizen00026(Kaizen00026), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateKaizen00026Data(IList<Kaizen00026> Kaizen00026, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00026Services service = new Kaizen00026Services(KaizenUser);
            return Json(service.Update(Kaizen00026), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteKaizen00026Data(IList<Kaizen00026> Kaizen00026, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00026Services service = new Kaizen00026Services(KaizenUser);
            return Json(service.Delete(Kaizen00026), JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}