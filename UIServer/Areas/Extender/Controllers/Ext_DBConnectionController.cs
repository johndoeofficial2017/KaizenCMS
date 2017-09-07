using Kaizen.BusinessLogic.Admin;
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

namespace UIServer.Areas.Extender.Controllers
{
    public class Ext_DBConnectionController : Controller
    {
        // GET: Extender/Ext_DBConnection
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        #region
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<Ext00001> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new Ext00001
               {
                   CompanyName=o.CompanyName,
                   CompanyPassword=o.CompanyPassword,
                   CompanyUserName=o.CompanyUserName,
                   DataBaseSourceID=o.DataBaseSourceID,
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<Ext00001>(),
                    Total = 0
                };
            }
            return result;
        }
        public JsonResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            Ext00001Services serv = new Ext00001Services(KaizenUser);
            DataCollection<Ext00001> L = new DataCollection<Ext00001>();
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
                SortMember = "DataBaseSourceID";
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

        public ActionResult SaveData(Ext00001 Ext00001, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00001Services service = new Ext00001Services(KaizenUser);
            return Json(service.AddExt00001(Ext00001), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(Ext00001 Ext00001, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00001Services service = new Ext00001Services(KaizenUser);
            return Json(service.Update(Ext00001), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(Ext00001 Ext00001, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00001Services service = new Ext00001Services(KaizenUser);
            return Json(service.Delete(Ext00001), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, int DataBaseSourceID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00001Services service = new Ext00001Services(KaizenUser);
            Ext00001 L = service.GetSingle(DataBaseSourceID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
    }
}