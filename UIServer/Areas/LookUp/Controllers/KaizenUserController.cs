using System;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Kaizen.BusinessLogic.Configuration;
using Kaizen.Configuration;
using UIServer.Models;
using System.IO;
using System.Web;


namespace UIServer.Areas.LookUp.Controllers
{
    public class KaizenUserController : Controller
    {
        // GET: LookUp/KaizenUser
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<User> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new User 
               { 
                   UserName = o.UserName,
                   FirstName = string.IsNullOrEmpty(o.FirstName) ? "" : o.FirstName.Length > 15 ? o.FirstName.Substring(0, 15) : o.FirstName,
                   LastName = string.IsNullOrEmpty(o.LastName) ? "" : o.LastName.Length > 15 ? o.LastName.Substring(0, 15) : o.LastName,
                   //FullName = string.IsNullOrEmpty(o.FullName) ? "" : o.FullName.Length > 15 ? o.FullName.Substring(0, 15) : o.FullName,
                   IsDisabled = o.IsDisabled,
                   PhotoExtension = o.PhotoExtension,
                   IsCustomer = o.IsCustomer,
                   IsVendor = o.IsVendor,
                   IsDebtor = o.IsDebtor,
                   IsPartner = o.IsPartner,
                   IsAgent = o.IsAgent,
                   IsClient = o.IsClient,
                   IsEmployee = o.IsEmployee,
                   LastLogin = o.LastLogin,
                   IsPasswordchange = o.IsPasswordchange
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<User>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetListPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            UserServices invService = new UserServices(KaizenUser);
            DataCollection<User> L = new DataCollection<User>();
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
                SortMember = "UserName";
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

    }
}