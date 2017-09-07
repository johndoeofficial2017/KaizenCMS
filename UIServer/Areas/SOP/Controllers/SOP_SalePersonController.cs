using Kaizen.BusinessLogic.Configuration;
using Kaizen.BusinessLogic.SOP;
using Kaizen.SOP;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Areas.SOP.Controllers
{
    public class SOP_SalePersonController : Controller
    {
        // GET: SOP_SalePerson
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
        public ActionResult PopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<SOP00110> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new SOP00110
               {
                   SalePersonID = o.SalePersonID,
                   UserCode = o.UserCode,
                   EmployeeID = o.EmployeeID,
                   SupervisorID = o.SupervisorID,
                   SalePersonTypeID = o.SalePersonTypeID,
                   MidName = o.MidName,
                   FirstName = o.FirstName,
                   PhonExtension = o.PhonExtension,
                   LastName = o.LastName,
                   Inactive = o.Inactive,
                   EmailAddress = o.EmailAddress,
                   DirectPhon = o.DirectPhon
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<SOP00110>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            SOP00110Services invService = new SOP00110Services(KaizenUser);
            DataCollection<SOP00110> L = new DataCollection<SOP00110>();
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
                SortMember = "SalePersonID";
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
        public ActionResult GetListPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00110Services invService = new SOP00110Services(KaizenUser);
            DataCollection<SOP00110> L = new DataCollection<SOP00110>();
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
                SortMember = "SalePersonID";
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
        #endregion

        public ActionResult SaveData(SOP00110 SOP00110, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00110Services service = new SOP00110Services(KaizenUser);
            int startindex;
            if (!string.IsNullOrEmpty(SOP00110.PhonExtension))
            {
                string PhotoPath = @"\\Photo\\SalesPersonPhotoTemp\\" + SOP00110.PhonExtension.Trim();
                startindex = SOP00110.PhonExtension.LastIndexOf('.');
                startindex += 1;
                SOP00110.PhonExtension = SOP00110.PhonExtension.Substring(startindex, SOP00110.PhonExtension.Length - startindex);
                if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                {
                    string Destination = Server.MapPath(@"\\Photo\\SalesPersonPhoto\\" + SOP00110.SalePersonID.Trim() + "." + SOP00110.PhonExtension);
                    if (System.IO.File.Exists(Destination))
                    {
                        System.IO.File.Delete(Destination);
                    }
                    System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                }
            }
            return Json(service.AddSOP00110(SOP00110), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(SOP00110 SOP00110, string KaizenPublicKey, bool PhotoChanged)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00110Services service = new SOP00110Services(KaizenUser);
            int startindex;
            if (PhotoChanged)
            {
                if (!string.IsNullOrEmpty(SOP00110.PhonExtension))
                {
                    string PhotoPath = @"\\Photo\\SalesPersonPhotoTemp\\" + SOP00110.PhonExtension.Trim();
                    startindex = SOP00110.PhonExtension.LastIndexOf('.');
                    startindex += 1;
                    SOP00110.PhonExtension = SOP00110.PhonExtension.Substring(startindex, SOP00110.PhonExtension.Length - startindex);
                    if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                    {
                        string Destination = Server.MapPath(@"\\Photo\\SalesPersonPhoto\\" + SOP00110.SalePersonID.Trim() + "." + SOP00110.PhonExtension);
                        if (System.IO.File.Exists(Destination))
                        {
                            System.IO.File.Delete(Destination);
                        }
                        System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                    }
                }
            }
            return Json(service.Update(SOP00110), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(SOP00110 SOP00110, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00110Services service = new SOP00110Services(KaizenUser);
            return Json(service.Delete(SOP00110), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string SalePersonID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00110Services service = new SOP00110Services(KaizenUser);
            return Json(service.GetSingle(SalePersonID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveImageTemp(IEnumerable<HttpPostedFileBase> attachments)
        {
            var fileName = "";
            foreach (var file in attachments)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/Photo/SalesPersonPhotoTemp"), fileName);
                file.SaveAs(destinationPath);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveImagetemp(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Photo/SalesPersonPhotoTemp"), fileName);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }

    }
}