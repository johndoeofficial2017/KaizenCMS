using System;
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
    public class CMS_CaseStatusController : Controller
    {
        // GET: CMS_CaseStatus
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
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00700> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00700
               {
                   CaseStatusID = o.CaseStatusID,
                   CaseStatusname = o.CaseStatusname
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00700>(),
                    Total = 0
                };
            }
            return result;
        }
        public JsonResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00700Services serv = new CM00700Services(KaizenUser);
            DataCollection<CM00700> L = new DataCollection<CM00700>();
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
                SortMember = "CaseStatusID";
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

        public ActionResult SaveData(CM00700 CM00700, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.AddCM00700(CM00700), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(CM00700 CM00700, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.Update(CM00700), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(CM00700 CM00700, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.Delete(CM00700), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, int CaseStatusID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            CM00700 L = service.GetSingle(CaseStatusID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStatusChilds(string KaizenPublicKey, int CaseStatusParent)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00700> o = service.GetStatusChilds(CaseStatusParent);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetWorkableFields(string KaizenPublicKey, int CaseStatusID)
        { 
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00060> o = service.GetWorkableFields(CaseStatusID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllLookup(string KaizenPublicKey, int CaseStatusID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00061> o = service.GetAllLookup(CaseStatusID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllLookup02(string KaizenPublicKey, int CaseStatusID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00052> o = service.GetAllLookup02(CaseStatusID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetAllLookup03(string KaizenPublicKey, int CaseStatusID)
        //{
        //    Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
        //    CM00700Services service = new CM00700Services(KaizenUser);
        //    IList<CM00053> o = service.GetAllLookup03(CaseStatusID);
        //    return Json(o, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult GetAllLookup04(string KaizenPublicKey, int CaseStatusID)
        //{
        //    Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
        //    CM00700Services service = new CM00700Services(KaizenUser);
        //    IList<CM00054> o = service.GetAllLookup04(CaseStatusID);
        //    return Json(o, JsonRequestBehavior.AllowGet);
        //}
        
        //public ActionResult GetAllLookup06(string KaizenPublicKey, int CaseStatusID)
        //{
        //    Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
        //    CM00700Services service = new CM00700Services(KaizenUser);
        //    IList<CM00056> o = service.GetAllLookup06(CaseStatusID);
        //    return Json(o, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00700> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCaseCHilds(string KaizenPublicKey,int caseStatusID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00700> L = service.GetStatusChilds(caseStatusID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StatusHasChildDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00700> L = service.GetAllStatusHasChilds();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StatusActionTypeFillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00003Services service = new CM00003Services(KaizenUser);
            IList<CM00003> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StatusHasScriptsDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00700> L = service.GetAllStatusHasScripts();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StatusHasTasksDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00700> L = service.GetAllStatusHasTasks();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllLookupFields(string KaizenPublicKey, int CaseStatusID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00060> o = service.GetAllLookupFields(CaseStatusID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        #region Case Status Tasks
        public ActionResult StatusTask(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult SaveTask(IList<CM00025> CM00025, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00025Services service = new CM00025Services(KaizenUser);
            return Json(service.AddCM00025(CM00025), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateTask(IList<CM00025> CM00025, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00025Services service = new CM00025Services(KaizenUser);
            return Json(service.Update(CM00025), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteTask(IList<CM00025> CM00025, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00025Services service = new CM00025Services(KaizenUser);
            return Json(service.Delete(CM00025), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllByCaseStatusID(string KaizenPublicKey, int CaseStatusID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00025Services service = new CM00025Services(KaizenUser);
            IList<CM00025> o = service.GetAllByCaseStatusID(CaseStatusID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Case Status Scripts
        public ActionResult StatusScript(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult SaveScript(IList<CM00026> CM00026, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00026Services service = new CM00026Services(KaizenUser);
            return Json(service.AddCM00026(CM00026), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateScript(IList<CM00026> CM00026, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00026Services service = new CM00026Services(KaizenUser);
            return Json(service.Update(CM00026), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteScript(IList<CM00026> CM00026, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00026Services service = new CM00026Services(KaizenUser);
            return Json(service.Delete(CM00026), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllScriptsByCaseStatusID(string KaizenPublicKey, int CaseStatusID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00026Services service = new CM00026Services(KaizenUser);
            IList<CM00026> o = service.GetAllByCaseStatusID(CaseStatusID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Case Status Child
        public ActionResult StatusChild(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult UpdateChild(IList<CM00700> CM00700, int CaseStatusID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.UpdateChildStatus(CM00700, CaseStatusID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteStatusScript(CM00026 oCM00026, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.DeleteStatusScript(oCM00026), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteChild(IList<CM00700> CM00700, int CaseStatusID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.DeleteChildStatus(CM00700, CaseStatusID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllChildsByCaseStatusID(string KaizenPublicKey, int CaseStatusID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00026Services service = new CM00026Services(KaizenUser);
            IList<CM00026> o = service.GetAllByCaseStatusID(CaseStatusID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        private DataSourceResult CaseStatusChildList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00700> L, int? CaseStatusID)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00700
               {
                   IsSelected = CaseStatusID == null ? false : CaseStatusID == o.CaseStatusParent ? true : false,
                   CaseStatusID = o.CaseStatusID,
                   CaseStatusname = o.CaseStatusname,
                   CaseStatusParent = o.CaseStatusParent,
                   IsCloseReminder = o.IsCloseReminder,
                   IsHasChild = o.IsHasChild,
                   IsPaymentApply = o.IsPaymentApply,
                   IsPTP = o.IsPTP,
                   IsPTPRequired = o.IsPTPRequired,
                   IsScripting = o.IsScripting,
                   IsTaskList = o.IsTaskList,
                   RuleCondition = o.RuleCondition,
                   StatusActionTypeID = o.StatusActionTypeID
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00700>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetStatusHasChildListGrid([DataSourceRequest]DataSourceRequest request,
            string KaizenPublicKey, int? CaseStatusID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services invService = new CM00700Services(KaizenUser);
            DataCollection<CM00700> L = new DataCollection<CM00700>();
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
                SortMember = "CaseStatusID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetAllViewBYSQLQuery(SQLQuery, request.PageSize, request.Page, SortMember, IsAscending, CaseStatusID);
            DataSourceResult result = CaseStatusChildList(request, L, CaseStatusID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Case Status Role
        public ActionResult StatusRole(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult SaveRole(CM00052 CM00052, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.AddCM00052(CM00052), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteRole(CM00052 CM00052, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.DeleteCM00052(CM00052), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Status Users
        public ActionResult StatusUser(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetStatusByUser(string KaizenPublicKey, string userName)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00051> o = service.GetStatusByUser(userName);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteStatusByUser(CM00051 CM00051, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.DeleteStatusByUser(CM00051), JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddStatusByUser(CM00051 CM00051, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.AddCM00051(CM00051), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Status Field
        public ActionResult StatusFields(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetFieldsByCaseStatus(string KaizenPublicKey, int CaseStatusID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00060> o = service.GetFieldsByCaseStatus(CaseStatusID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveCaseStatusField(CM00060 CM00060, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.AddCM00060(CM00060), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateField(CM00060 CM00060, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.Update(CM00060), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteField(CM00060 CM00060, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.Delete(CM00060), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Status Views
        public ActionResult StatusViews(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetViewsByCaseStatus(string KaizenPublicKey, int CaseStatusID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00062> o = service.GetViewsByCaseStatus(CaseStatusID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveCaseStatusView(CM00062 CM00062, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.AddCM00062(CM00062), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateView(CM00062 CM00062, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.Update(CM00062), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteView(CM00062 CM00062, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.Delete(CM00062), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetViews(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00062> o = service.GetViews();
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Status Views Roles
        public ActionResult StatusViewsRoles(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetRolesByView(string KaizenPublicKey, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00065> o = service.GetRolesByView(ViewID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveViewRole(CM00065 CM00065, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.AddCM00065(CM00065), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteViewRole(CM00065 CM00065, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.DeleteCM00065(CM00065), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Status Views Users
        public ActionResult StatusViewsUsers(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult AddCaseStatusViewByUser(CM00064 CM00064, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.AddCM00064(CM00064), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCaseStatusViewUser(CM00064 CM00064, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.DeleteCaseStatusViewUser(CM00064), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCaseStatusViewsByUser(string KaizenPublicKey, string userName)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00064> o = service.GetCaseStatusViewsByUser(userName);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Status Views Fields
        public ActionResult StatusViewsFields(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetAllFieldsByView(string KaizenPublicKey, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00063> o = service.GetAllFieldsByView(ViewID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetAllFieldsByCaseStatus(string KaizenPublicKey, int CaseStatusID)
        //{
        //    Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
        //    CM00700Services service = new CM00700Services(KaizenUser);
        //    IList<CM00060> o = service.GetAllFieldsByCaseStatus(CaseStatusID);
        //    return Json(o, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult GetAllFieldsByCaseStatus(string KaizenPublicKey, int CaseStatusID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00060> o = service.GetAllFieldsByCaseStatus(CaseStatusID);
            List<CM00063> result = new List<CM00063>();
            foreach (CM00060 temp in o)
            {
                result.Add(new CM00063()
                {
                    CaseStatusID = temp.CaseStatusID,
                    FieldCode = temp.FieldCode,
                    FieldOrder = 0,
                    FieldTitle = temp.FieldName,
                    FieldTypeID = temp.FieldTypeID
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveViewsFields(CM00063 CM00063, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.AddCM00063(CM00063), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateViewsFields(IList<CM00063> CM00063List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.UpdateCM00063(CM00063List), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteViewsFields(CM00063 CM00063, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.DeleteCM00063(CM00063), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Status Lookup
        public ActionResult StatusLookup(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetFieldCodesByCaseStatus(string KaizenPublicKey, int CaseStatusID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00060> o = service.GetFieldCodesByCaseStatus(CaseStatusID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLookups(string KaizenPublicKey, int CaseStatusID,string fieldCode)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services service = new CM00700Services(KaizenUser);
            IList<CM00061> o = service.GetLookups(CaseStatusID,fieldCode);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddCM00061(CM00061 CM00061, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.AddCM00061(CM00061), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCM00061(CM00061 CM00061, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.UpdateCM00061(CM00061), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCM00061(CM00061 CM00061, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services service = new CM00700Services(KaizenUser);
            return Json(service.DeleteCM00061(CM00061), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}