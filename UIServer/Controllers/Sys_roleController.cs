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
    public class Sys_roleController : Controller
    {
        // GET: Sys_role
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

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<Kaizen00030> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new Kaizen00030
               {
                   RoleID = o.RoleID,
                   RoleName = o.RoleName,
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<Kaizen00030>(),
                    Total = 0
                };
            }
            return result;
        }

        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00030Services invService = new Kaizen00030Services(KaizenUser);
            DataCollection<Kaizen00030> L = new DataCollection<Kaizen00030>();
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
                SortMember = "RoleID";
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

        public ActionResult SaveData(Kaizen00030 Kaizen00030, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00030Services service = new Kaizen00030Services(KaizenUser);
            return Json(service.AddKaizen00030(Kaizen00030), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(Kaizen00030 Kaizen00030, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00030Services service = new Kaizen00030Services(KaizenUser);
            return Json(service.Update(Kaizen00030), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(Kaizen00030 Kaizen00030, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00030Services service = new Kaizen00030Services(KaizenUser);
            return Json(service.Delete(Kaizen00030), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, int RoleID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00030Services service = new Kaizen00030Services(KaizenUser);
            Kaizen00030 L = service.GetSingle(RoleID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00030Services service = new Kaizen00030Services(KaizenUser);
            IList<Kaizen00030> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        #region Role Security
        public ActionResult RoleSecurity(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult GetTaskAccessByModule(string KaizenPublicKey, int RoleID, int ModuleID, string CompanyID)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen004Services srv = new Kaizen004Services(KaizenUser);
            IList<Kaizen004> L = srv.GetRoleTasksByModuleID(RoleID, ModuleID, CompanyID);
            List<Kaizen004> o = new List<Kaizen004>();
            foreach (var item in L)
            {
                o.Add(new Kaizen004()
                {
                    TaskID = item.TaskID,
                    ModuleID = item.ModuleID,
                    RoleID = item.RoleID,
                    CompanyID = item.CompanyID,
                    IsCustomPage = item.IsCustomPage,
                    MenueOrder = item.MenueOrder,
                    MenueTypeID = item.MenueTypeID,
                    MenuName = item.MenuName,
                    ScreenPath = item.ScreenPath
                });
            }
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetModuleTasks(string KaizenPublicKey, int ModuleID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen002Services srv = new Kaizen002Services(KaizenUser);
            IList<Kaizen002> L = srv.GetAllByModuleID(ModuleID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveRoleSecurityData(IList<Kaizen004> Kaizen004, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen004Services service = new Kaizen004Services(KaizenUser);
            return Json(service.AddKaizen004(Kaizen004), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateRoleSecurityData(IList<Kaizen004> Kaizen004, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen004Services service = new Kaizen004Services(KaizenUser);
            return Json(service.Update(Kaizen004), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteRoleSecurityData(IList<Kaizen004> Kaizen004, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen004Services service = new Kaizen004Services(KaizenUser);
            return Json(service.Delete(Kaizen004), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region View Role Security
        public ActionResult ViewRoleSecurity(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult GetViewAccessByRole(string KaizenPublicKey, int RoleID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00030Services service = new Kaizen00030Services(KaizenUser);
            Kaizen00030 L = service.GetSingleWithViews(RoleID);
            return Json(L.Kaizen00011, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveViewRole(int RoleID,int ViewID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00030Services service = new Kaizen00030Services(KaizenUser);
            return Json(service.SaveViewRole(RoleID, ViewID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteViewRole(int RoleID, int ViewID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00030Services service = new Kaizen00030Services(KaizenUser);
            return Json(service.DeleteViewRole(RoleID, ViewID), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Role Company
        public ActionResult RoleCompany(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetRolesByCompany(string KaizenPublicKey, string CompanyID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00030Services service = new Kaizen00030Services(KaizenUser);
            IList<Kaizen006> o = service.GetRolesByCompany(CompanyID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCompanyRole(Kaizen006 Kaizen006, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00030Services service = new Kaizen00030Services(KaizenUser);
            return Json(service.AddKaizen006(Kaizen006), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCompanyRole(Kaizen006 Kaizen006, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00030Services service = new Kaizen00030Services(KaizenUser);
            return Json(service.DeleteKaizen006(Kaizen006), JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}