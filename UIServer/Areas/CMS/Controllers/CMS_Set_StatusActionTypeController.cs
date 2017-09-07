﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.BusinessLogic.CMS;
using Kaizen.CMS;
using Kaizen.BusinessLogic.Configuration;

namespace UIServer.Areas.CMS.Controllers
{
    public class CMS_Set_StatusActionTypeController : Controller
    {
        // GET: CMS_Set_StatusActionType
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
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00003> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00003
               {
                   StatusActionTypeID = o.StatusActionTypeID,
                   StatusActionTypeName = o.StatusActionTypeName
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00003>(),
                    Total = 0
                };
            }
            return result;
        }
        public JsonResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00003Services serv = new CM00003Services(KaizenUser);
            DataCollection<CM00003> L = new DataCollection<CM00003>();
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
                SortMember = "StatusActionTypeID";
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

        public ActionResult SaveData(CM00003 CM00003, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00003Services service = new CM00003Services(KaizenUser);
            return Json(service.AddCM00003(CM00003), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(CM00003 CM00003, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00003Services service = new CM00003Services(KaizenUser);
            return Json(service.Update(CM00003), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(CM00003 CM00003, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00003Services service = new CM00003Services(KaizenUser);
            return Json(service.Delete(CM00003), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, int StatusActionTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00003Services service = new CM00003Services(KaizenUser);
            CM00003 L = service.GetSingle(StatusActionTypeID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00003Services service = new CM00003Services(KaizenUser);
            IList<CM00003> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region Status Group Role
        public ActionResult StatusGroupRole(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult GetRolesByStatusActionType(string KaizenPublicKey, int StatusActionTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00003Services service = new CM00003Services(KaizenUser);
            IList<CM00054> o = service.GetRolesByStatusActionType(StatusActionTypeID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveRole(CM00054 CM00054, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00003Services service = new CM00003Services(KaizenUser);
            return Json(service.AddCM00054(CM00054), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteRole(CM00054 CM00054, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00003Services service = new CM00003Services(KaizenUser);
            return Json(service.DeleteCM00054(CM00054), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Status Group User
        public ActionResult StatusGroupUser(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetStatusGroupByUser(string KaizenPublicKey, string userName)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00003Services service = new CM00003Services(KaizenUser);
            IList<CM00053> o = service.GetStatusGroupByUser(userName);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteStatusGroupByUser(CM00053 CM00053, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00003Services service = new CM00003Services(KaizenUser);
            return Json(service.DeleteStatusGroupByUser(CM00053), JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddStatusGroupByUser(CM00053 CM00053, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00003Services service = new CM00003Services(KaizenUser);
            return Json(service.AddCM00053(CM00053), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}