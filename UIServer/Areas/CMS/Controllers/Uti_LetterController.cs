using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.BusinessLogic.CMS;
using Kaizen.CMS;
using Kaizen.BusinessLogic.Configuration;
using System.IO;
using System.Transactions;
using System.Web;
using UIServer.Infrastructure.Token_Setup;

namespace UIServer.Areas.CMS.Controllers
{
    public class Uti_LetterController : Controller
    {
        // GET: Uti_Letter
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
        public ActionResult LetterView(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ViewReport(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetSingle(string KaizenPublicKey, string MessageTRXID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00234Services service = new CM00234Services(KaizenUser);
            CM00234 o = service.GetSingle(MessageTRXID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLetterViewGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria,
            string MessageTRXID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00234Services serv = new CM00234Services(KaizenUser);
            DataCollection<CM00235> L = new DataCollection<CM00235>();
            string filters = string.Empty;
            string SortMember = "TableID";
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

            L = serv.GetAllCM00235(SQLQuery, Searchcritaria, MessageTRXID
                , request.PageSize, request.Page, SortMember, IsAscending);
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00235
               {
                   MessageTRXID = o.MessageTRXID.Trim(),
                   TableID = o.TableID,
                   CaseRef = o.CaseRef,
                   IsSent = o.IsSent
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00235>(),
                    Total = 0
                };
            }

            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        public ActionResult SaveCaseDemandLetter(CM00234 CM00234, IList<CM00235> CM00235, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00234Services service = new CM00234Services(KaizenUser);
            CM00234.CreatedBy = KaizenUser.UserName;
            CM00234.CreatedDate = DateTime.Now;
            return Json(service.AddCM00234(CM00234, CM00235), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00234Services serv = new CM00234Services(KaizenUser);
            DataCollection<CM00234> L = new DataCollection<CM00234>();
            string filters = string.Empty;
            string SortMember = "MessageTRXID";
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

            L = serv.GetAllCM00234List(SQLQuery, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00234> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00234
               {
                   MessageTRXID = o.MessageTRXID.Trim(),
                   CreatedBy = o.CreatedBy,
                   CreatedDate = o.CreatedDate,
                   TrxComment = o.TrxComment,
                   TemplateID = o.TemplateID,
                   TemplateContant = o.TemplateContant,
                   AddressCode = o.AddressCode
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00234>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetNextTransactionID(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);
            CM00234Services srv = new CM00234Services(KaizenUser);
            return Json(srv.GetNextLetterTransactionNumber(), JsonRequestBehavior.AllowGet);
        }
    }
}