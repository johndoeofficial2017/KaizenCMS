using Kaizen.BusinessLogic.Configuration;
using Kaizen.BusinessLogic.CMS;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaizen.CMS;
using System.IO;


namespace UIServer.Areas.LookUp.Controllers
{
    public class CMS_AgentLookUpController : Controller
    {
        // GET: LookUp/CMS_AgentLookUp
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetListPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00105Services invService = new CM00105Services(KaizenUser);
            DataCollection<CM00105> L = new DataCollection<CM00105>();
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
                SortMember = "AgentID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00105> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00105
               {
                   AgentID = o.AgentID,
                   CalendarID = o.CalendarID,
                   SignatureExtension = o.SignatureExtension,
                   //UserLevelID = o.UserLevelID,
                   //EmployeeID = o.EmployeeID,
                   SupervisorID = o.SupervisorID,
                   SuffixID = o.SuffixID,
                   MidName = o.MidName,
                   FirstName = o.FirstName,
                   LastName = o.LastName,
                   AgentGroup = o.AgentGroup,
                   Inactive = o.Inactive,
                   EmailAddress = o.EmailAddress,
                   DirectPhon = o.DirectPhon,
                   PhotoExtension = o.PhotoExtension,
                   UserCode = o.UserCode
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00105>(),
                    Total = 0
                };
            }
            return result;
        }

    }
}