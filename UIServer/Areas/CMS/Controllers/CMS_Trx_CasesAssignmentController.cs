using System;
using System.Collections.Generic;
using System.Data;
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
    public class CMS_Trx_CasesAssignmentController : Controller
    {
        // GET: CMS_Trx_CasesAssignment
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult Create(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult FormView(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetSingle(string KaizenPublicKey, string AssigmentID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00202Services service = new CM00202Services(KaizenUser);
            CM00202 L = service.GetSingle(AssigmentID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveTransaction(CM00202 CM00202, IList<CM00206> CM00206, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00202Services service = new CM00202Services(KaizenUser);
            return Json(service.AddCM00202(CM00202, CM00206), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00202Services serv = new CM00202Services(KaizenUser);
            DataCollection<CM00202> L = new DataCollection<CM00202>();
            string filters = string.Empty;
            string SortMember = "AssigmentID";
            bool IsAscending = false;
            if (request.Sorts != null)
            {
                SortDescriptor sortobject = request.Sorts.FirstOrDefault();
                if (sortobject != null)
                {
                    SortMember = sortobject.Member;
                    if (sortobject.SortDirection == System.ComponentModel.ListSortDirection.Ascending)
                        IsAscending = true;
                    else
                        IsAscending = false;
                }
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
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00202> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00202
               {
                   AssigmentID = o.AssigmentID.Trim(),
                   AssignDate = o.AssignDate,
                   UserName = o.UserName,
                   AssignDescription = o.AssignDescription
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00100>(),
                    Total = 0
                };
            }
            return result;
        }

        public ActionResult GetDataListGridAssigmentID([DataSourceRequest]DataSourceRequest request,string AssigmentID ,
            string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00206Services serv = new CM00206Services(KaizenUser);
            DataCollection<CM00206> L = new DataCollection<CM00206>();
            string filters = string.Empty;
            string SortMember = "AssigmentID";
            if (request.Sorts != null)
            {
                SortDescriptor sortobject = request.Sorts.FirstOrDefault();
                if (sortobject != null)
                {
                    SortMember = sortobject.Member;
                }
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);
            SQLQuery += "AssigmentID='" + AssigmentID + "'";
            L = serv.GetWhereListWithPaging(SQLQuery,request.PageSize, request.Page);
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00206
               {
                   AssignLineID = o.AssignLineID,
                   CaseRef = o.CaseRef,
                   AgentID = o.AgentID
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00100>(),
                    Total = 0
                };
            }
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        public ActionResult GetNextTransactionID(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);
            CM00202Services srv = new CM00202Services(KaizenUser);
            return Json(srv.GetNextTransactionID(), JsonRequestBehavior.AllowGet);
        }
    }
}