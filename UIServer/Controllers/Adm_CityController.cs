﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.Admin;
using Kaizen.BusinessLogic.Admin;
using Kaizen.BusinessLogic.Configuration;

namespace UIServer.Controllers
{
    public class Adm_CityController : Controller
    {
        // GET: Adm_City
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
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<Sys00014> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new Sys00014
               {

                   CityID = o.CityID,
                   CityName = o.CityName
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<Sys00014>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Sys00014Services invService = new Sys00014Services(KaizenUser);
            DataCollection<Sys00014> L = new DataCollection<Sys00014>();
            string filters = string.Empty;
            string SortMember;
            string SortDirection;
            SortDescriptor sortobject = request.Sorts.FirstOrDefault();
            if (sortobject != null)
            {
                SortMember = sortobject.Member;
                SortDirection = sortobject.SortDirection.ToString();
            }
            else
            {
                SortMember = "CityID";
                SortDirection = "asc";
            }
            if (request.Filters.Count > 0)
            {
                string SQLQuery = Help.ApplyFilter(request.Filters[0]);
                L = invService.GetAllBYSQLQuery(SQLQuery, request.PageSize, request.Page, SortMember, SortDirection);
            }
            else
            {
                if (string.IsNullOrEmpty(Searchcritaria))
                    L = invService.GetListWithPaging(request.PageSize, request.Page, SortMember, SortDirection);
                else
                    L = invService.GetAllBYSQLQuery(Searchcritaria, request.PageSize, request.Page, SortMember, SortDirection);
            }
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult SaveData(Sys00014 Sys00014, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Sys00014Services service = new Sys00014Services(KaizenUser);
            return Json(service.AddSys00014(Sys00014), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(Sys00014 Sys00014, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Sys00014Services service = new Sys00014Services(KaizenUser);
            return Json(service.Update(Sys00014), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(Sys00014 Sys00014, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Sys00014Services service = new Sys00014Services(KaizenUser);
            return Json(service.Delete(Sys00014), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string CityID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Sys00014Services service = new Sys00014Services(KaizenUser);
            Sys00014 L = service.GetSingle(CityID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Sys00014Services service = new Sys00014Services(KaizenUser);
            IList<Sys00014> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

    }
}