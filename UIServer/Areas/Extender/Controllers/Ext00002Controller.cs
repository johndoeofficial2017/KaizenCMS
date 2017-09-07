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
    public class Ext00002Controller : Controller
    {
        // GET: Extender/Ext00002
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
        public ActionResult DBSourceField(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        #region
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<Ext00002> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new Ext00002
               {
                   CompanyID=o.CompanyID,
                   ComTableName= o.ComTableName,
                   DataBaseSourceID= o.DataBaseSourceID,
                   IsAddNew= o.IsAddNew,
                   IsformOnly=o.IsformOnly,
                   IsGraph= o.IsGraph,
                   IsView= o.IsView,
                   MenueTypeID= o.MenueTypeID,
                   ModuleID= o.ModuleID,
                   SourceDisplay= o.SourceDisplay
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<Ext00002>(),
                    Total = 0
                };
            }
            return result;
        }
        public JsonResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            Ext00002Services serv = new Ext00002Services(KaizenUser);
            DataCollection<Ext00002> L = new DataCollection<Ext00002>();
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

        public ActionResult SaveData(Ext00002 Ext00002, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00002Services service = new Ext00002Services(KaizenUser);
            return Json(service.AddExt00002(Ext00002), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(Ext00002 Ext00002, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00002Services service = new Ext00002Services(KaizenUser);
            return Json(service.Update(Ext00002), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(Ext00002 Ext00002, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00002Services service = new Ext00002Services(KaizenUser);
            return Json(service.Delete(Ext00002), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, int DataBaseSourceID,string ComTableName)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00002Services service = new Ext00002Services(KaizenUser);
            Ext00002 L = service.GetSingle(DataBaseSourceID, ComTableName);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMenueTypes(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Ext00002Services service = new Ext00002Services(KaizenUser);
            IList<Kaizen001> L = service.GetMenueTypes();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDataBaseSourceList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Ext00002Services service = new Ext00002Services(KaizenUser);
            IList<Ext00001> L = service.GetDataBaseSourceList();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region DB Source Field
        public ActionResult GetSourceFields(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Ext00002Services service = new Ext00002Services(KaizenUser);
            IList<Kaizen00006> L = service.GetSourceFields();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetComTableNameList(int DataBaseSourceID,string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Ext00002Services service = new Ext00002Services(KaizenUser);
            IList<Ext00002> L = service.GetComTableNameList(DataBaseSourceID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetExt00003ByID(int DataBaseSourceID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Ext00002Services service = new Ext00002Services(KaizenUser);
            IList<Ext00003> L = service.GetExt00003(DataBaseSourceID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetExt00003(int DataBaseSourceID,string ComTableName,string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Ext00002Services service = new Ext00002Services(KaizenUser);
            IList<Ext00003> L = service.GetExt00003(DataBaseSourceID, ComTableName);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveSourceField(Ext00003 Ext00003, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00002Services service = new Ext00002Services(KaizenUser);
            return Json(service.SaveSourceField(Ext00003), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateSourceField(Ext00003 Ext00003, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00002Services service = new Ext00002Services(KaizenUser);
            return Json(service.UpdateSourceField(Ext00003), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSourceField(Ext00003 Ext00003, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00002Services service = new Ext00002Services(KaizenUser);
            return Json(service.DeleteSourceField(Ext00003), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Source Role Security
        public ActionResult DBSourceRoleSecurity(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult SaveSourceRole(Ext00004 Ext00004, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00002Services service = new Ext00002Services(KaizenUser);
            return Json(service.SaveSourceRole(Ext00004), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteSourceRole(Ext00004 Ext00004, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00002Services service = new Ext00002Services(KaizenUser);
            return Json(service.DeleteSourceRole(Ext00004), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSourceRoles(int DataBaseSourceID,string ComTableName, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Ext00002Services service = new Ext00002Services(KaizenUser);
            IList<Ext00004> L = service.GetSourceRoles(DataBaseSourceID, ComTableName);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Source User
        public ActionResult DBSourceUser(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult SaveSourceUser(Ext00005 Ext00005, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00002Services service = new Ext00002Services(KaizenUser);
            return Json(service.SaveSourceUser(Ext00005), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateSourceUser(Ext00005 Ext00005, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00002Services service = new Ext00002Services(KaizenUser);
            return Json(service.UpdateSourceUser(Ext00005), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteSourceUser(Ext00005 Ext00005, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Ext00002Services service = new Ext00002Services(KaizenUser);
            return Json(service.DeleteSourceUser(Ext00005), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSourceUsers(int DataBaseSourceID,string ComTableName, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Ext00002Services service = new Ext00002Services(KaizenUser);
            IList<Ext00005> L = service.GetSourceUsers(DataBaseSourceID,ComTableName);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}