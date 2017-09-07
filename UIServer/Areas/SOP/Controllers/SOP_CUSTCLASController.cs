using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kaizen.BusinessLogic.Purchase;
using Kaizen.SOP;
using Kaizen.BusinessLogic.Configuration;
using Kendo.Mvc.UI;
using Kaizen.BusinessLogic.SOP;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;

namespace UIServer.Areas.SOP.Controllers
{
    public class SOP_CUSTCLASController : Controller
    {
        // GET: SOP_CUSTCLAS
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult Create(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult PopUp(string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "SysUser");

            return PartialView();
        }

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<SOP00010> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new SOP00010
               {
                   CUSTCLAS = o.CUSTCLAS,
                   CUSTCLASNAME = o.CUSTCLASNAME,
                   LastCustomerID = o.LastCustomerID,
                   Prefixlengh = o.Prefixlengh,
                   PrefixValue = o.PrefixValue
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<SOP00010>(),
                    Total = 0
                };
            }
            return result;
        }

        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            SOP00010Services invService = new SOP00010Services(KaizenUser);
            DataCollection<SOP00010> L = new DataCollection<SOP00010>();
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
                SortMember = "LastCustomerID";
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


        public ActionResult SaveData(SOP00010 SOP00010, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00010Services service = new SOP00010Services(KaizenUser);
            return Json(service.AddSOP00010(SOP00010), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(SOP00010 SOP00010, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00010Services service = new SOP00010Services(KaizenUser);
            return Json(service.Update(SOP00010), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(SOP00010 SOP00010, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00010Services service = new SOP00010Services(KaizenUser);
            return Json(service.Delete(SOP00010), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string CUSTCLAS)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00010Services service = new SOP00010Services(KaizenUser);
            SOP00010 L = service.GetSingle(CUSTCLAS);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            SOP00010Services service = new SOP00010Services(KaizenUser);
            IList<SOP00010> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
    }
}