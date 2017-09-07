using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.SOP;
using Kaizen.BusinessLogic.SOP;
using Kaizen.BusinessLogic.Configuration;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System;

namespace UIServer.Areas.SOP.Controllers
{
    public class SOP_CustomerController : Controller
    {
        // GET: SOP_Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult MainGrid(string KaizenPublicKey, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00026Services oKaizen00011ColumnServices = new Kaizen00026Services(KaizenUser);
            List<Kaizen.Configuration.Kaizen00026> oKaizen00026ColumnList = oKaizen00011ColumnServices.GetFieldsByView(ViewID).ToList();
            List<Kaizen.Configuration.Kaizen00026> temp = oKaizen00026ColumnList.FindAll(ss => !ss.locked);
            if (temp.Count > 0)
                ViewBag.KaizenGridViewColumn = oKaizen00026ColumnList.OrderBy(x => x.ColumnOrder);
            else
                return PartialView("ErrorWindow");
            return PartialView();
        }
        public ActionResult ErrorWindow(string KaizenPublicKey)
        {
            return PartialView();
        }
        public ActionResult PopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<SOP00100> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new SOP00100
               {
                   CUSTNMBR = o.CUSTNMBR,
                   GroupID = o.GroupID,
                   StatementCycleID = o.StatementCycleID,
                   PriorityID = o.PriorityID,
                   CUSTNAME = o.CUSTNAME,
                   ShortName = o.ShortName,
                   CUSTCLAS = o.CUSTCLAS,
                   StatusID = o.StatusID,
                   IsHold = o.IsHold,
                   IsActive = o.IsActive,
                   CustomerDescription = string.IsNullOrEmpty(o.CustomerDescription) ? "" : o.CustomerDescription.Length > 15 ? o.CustomerDescription.Substring(0, 15) : o.CustomerDescription,
                   PhotoExtension = o.PhotoExtension,
                   ParentCustomer = o.ParentCustomer,
                   CPRCRNo = o.CPRCRNo,
                   EmployerName = o.EmployerName,
                   NationalityID = o.NationalityID,
                   ShipTo = o.ShipTo,
                   BillTo = o.BillTo,
                   StatementTo = o.StatementTo,
                   ContactTypeID=o.ContactTypeID,
                   PriceLevelCode=o.PriceLevelCode,
                   EntryDate=o.EntryDate,
                   UserCode=o.UserCode,
                   SalePersonID=o.SalePersonID,
                   AddressTypeCode=o.AddressTypeCode,
                   CurrencyCode=o.CurrencyCode
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<SOP00100>(),
                    Total = 0
                };
            }
            return result;
        }

        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            SOP00100Services invService = new SOP00100Services(KaizenUser);
            DataCollection<SOP00100> L = new DataCollection<SOP00100>();
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
                SortMember = "CUSTNMBR";
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
            SOP00100Services invService = new SOP00100Services(KaizenUser);
            DataCollection<SOP00100> L = new DataCollection<SOP00100>();
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
                SortMember = "CUSTNMBR";
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

        public ActionResult SaveData(SOP00100 SOP00100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00100Services service = new SOP00100Services(KaizenUser);
            int startindex;
            if (!string.IsNullOrEmpty(SOP00100.PhotoExtension))
            {
                string PhotoPath = @"\\Photo\\CustomerPhotoTemp\\" + SOP00100.PhotoExtension.Trim();
                startindex = SOP00100.PhotoExtension.LastIndexOf('.');
                startindex += 1;
                SOP00100.PhotoExtension = SOP00100.PhotoExtension.Substring(startindex, SOP00100.PhotoExtension.Length - startindex);
                if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                {
                    string Destination = Server.MapPath(@"\\Photo\\CustomerPhoto\\" + SOP00100.CUSTNMBR.Trim() + "." + SOP00100.PhotoExtension);
                    if (System.IO.File.Exists(Destination))
                    {
                        System.IO.File.Delete(Destination);
                    }
                    System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                }
            }
            return Json(service.AddSOP00100(SOP00100), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(SOP00100 SOP00100, string KaizenPublicKey,bool PhotoChanged)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00100Services service = new SOP00100Services(KaizenUser);
            int startindex;
            if (PhotoChanged)
            {
                if (!string.IsNullOrEmpty(SOP00100.PhotoExtension))
                {
                    string PhotoPath = @"\\Photo\\CustomerPhotoTemp\\" + SOP00100.PhotoExtension.Trim();
                    startindex = SOP00100.PhotoExtension.LastIndexOf('.');
                    startindex += 1;
                    SOP00100.PhotoExtension = SOP00100.PhotoExtension.Substring(startindex, SOP00100.PhotoExtension.Length - startindex);
                    if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                    {
                        string Destination = Server.MapPath(@"\\Photo\\CustomerPhoto\\" + SOP00100.CUSTNMBR.Trim() + "." + SOP00100.PhotoExtension);
                        if (System.IO.File.Exists(Destination))
                        {
                            System.IO.File.Delete(Destination);
                        }
                        System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                    }
                }
            }
            return Json(service.Update(SOP00100), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(SOP00100 SOP00100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00100Services service = new SOP00100Services(KaizenUser);
            return Json(service.Delete(SOP00100), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string CUSTNMBR)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00100Services service = new SOP00100Services(KaizenUser);
            return Json(service.GetSingle(CUSTNMBR), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveImageTemp(IEnumerable<HttpPostedFileBase> CustomerPhoto)
        {
            var fileName = "";
            foreach (var file in CustomerPhoto)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/Photo/CustomerPhotoTemp"), fileName);
                file.SaveAs(destinationPath);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveImagetemp(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Photo/CustomerPhotoTemp"), fileName);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }

        #region Customer Contact
        public ActionResult CustomerContact(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen.BusinessLogic.Admin.Sys00006Services srv = new Kaizen.BusinessLogic.Admin.Sys00006Services(KaizenUser);
            ViewData["ContactTypes"] = srv.GetAll(5);
            return PartialView();
        }
        public ActionResult CustomerContactPopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen.BusinessLogic.Admin.Sys00006Services srv = new Kaizen.BusinessLogic.Admin.Sys00006Services(KaizenUser);
            ViewData["ContactTypes"] = srv.GetAll(5);
            return PartialView();
        }

        private DataSourceResult CustomerContactList([DataSourceRequest]DataSourceRequest request, DataCollection<SOP00105> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new SOP00105
               {
                   ContactTypeID = o.ContactTypeID,
                   ContactPerson = o.ContactPerson,
                   PersonPosition = o.PersonPosition,
                   PersonEmailAdd = o.PersonEmailAdd,
                   OtherInfo = o.OtherInfo,
                   CUSTNMBR = o.CUSTNMBR
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<SOP00105>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetContactListGridWithCustomer([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string CUSTNMBR, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00105Services serv = new SOP00105Services(KaizenUser);
            DataCollection<SOP00105> L = new DataCollection<SOP00105>();
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
                SortMember = "ContactTypeID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(CUSTNMBR))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, CUSTNMBR
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = CustomerContactList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCustomerContact(SOP00105 SOP00105, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00105Services service = new SOP00105Services(KaizenUser);
            return Json(service.AddSOP00105(SOP00105), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCustomerContact(SOP00105 SOP00105, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00105Services service = new SOP00105Services(KaizenUser);
            return Json(service.Update(SOP00105), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCustomerContact(SOP00105 SOP00105, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00105Services service = new SOP00105Services(KaizenUser);
            return Json(service.Delete(SOP00105), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Customer Address
        public ActionResult CustomerAddress(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00009Services srv = new SOP00009Services(KaizenUser);
            ViewData["AddressTypes"] = srv.GetAll();
            return PartialView();
        }
        public ActionResult CustomerAddressPopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult CustomerAddressTypeFillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            SOP00009Services service = new SOP00009Services(KaizenUser);
            IList<SOP00009> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        private DataSourceResult CustomerAddressList([DataSourceRequest]DataSourceRequest request, DataCollection<SOP00101> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new SOP00101
               {
                   AddressTypeCode = o.AddressTypeCode,
                   Address1 = o.Address1,
                   Address2 = o.Address2,
                   Address3 = o.Address3,
                   Address4 = o.Address4,
                   AddressName = o.AddressName,
                   CityID = o.CityID,
                   CountryID = o.CountryID,
                   Email01 = o.Email01,
                   Email02 = o.Email02,
                   Email03 = o.Email03,
                   Email04 = o.Email04,
                   MobileNo1 = o.MobileNo1,
                   MobileNo2 = o.MobileNo2,
                   MobileNo3 = o.MobileNo3,
                   MobileNo4 = o.MobileNo4,
                   Other01 = o.Other01,
                   Other02 = o.Other02,
                   Other03 = o.Other03,
                   Other04 = o.Other04,
                   Pnone01 = o.Pnone01,
                   Pnone02 = o.Pnone02,
                   Pnone03 = o.Pnone03,
                   Pnone04 = o.Pnone04,
                   POBox = o.POBox,
                   WebSite = o.WebSite,
                   CUSTNMBR = o.CUSTNMBR
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<SOP00101>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetAddressListGridWithCustomer([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string CUSTNMBR, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00101Services serv = new SOP00101Services(KaizenUser);
            DataCollection<SOP00101> L = new DataCollection<SOP00101>();
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
                SortMember = "AddressTypeCode";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(CUSTNMBR))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, CUSTNMBR
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = CustomerAddressList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCustomerAddress(SOP00101 SOP00101, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00101Services service = new SOP00101Services(KaizenUser);
            return Json(service.AddSOP00101(SOP00101), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCustomerAddress(SOP00101 SOP00101, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00101Services service = new SOP00101Services(KaizenUser);
            return Json(service.Update(SOP00101), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCustomerAddress(SOP00101 SOP00101, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00101Services service = new SOP00101Services(KaizenUser);
            return Json(service.Delete(SOP00101), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Customer Document
        public ActionResult CustomerDocument(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen.BusinessLogic.Admin.Sys00007Services srv = new Kaizen.BusinessLogic.Admin.Sys00007Services(KaizenUser);
            ViewData["DocumentTypes"] = srv.GetAll(5);
            return PartialView();
        }
        private DataSourceResult CustomerDocumentList([DataSourceRequest]DataSourceRequest request, DataCollection<SOP00106> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new SOP00106
               {
                   DocumentID = o.DocumentID,
                   CUSTNMBR = o.CUSTNMBR,
                   DocumentDescription = o.DocumentDescription,
                   DocumentTypeID = o.DocumentTypeID,
                   DocumentName = o.DocumentName,
                   PhotoExtension = o.PhotoExtension
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<SOP00106>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetDocumentListGridWithCustomer([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string CUSTNMBR, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00106Services serv = new SOP00106Services(KaizenUser);
            DataCollection<SOP00106> L = new DataCollection<SOP00106>();
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
                SortMember = "DocumentID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(CUSTNMBR))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, CUSTNMBR
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = CustomerDocumentList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCustomerDocument(SOP00106 SOP00106, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00106Services service = new SOP00106Services(KaizenUser);
            if (!string.IsNullOrEmpty(SOP00106.PhotoExtension))
            {
                string PhotoPath = Server.MapPath(@"\\Photo\\CustomerDocumentsTemp\\" + SOP00106.PhotoExtension.Trim());
                FileInfo TempFile_info = new FileInfo(PhotoPath);
                SOP00106.PhotoExtension = TempFile_info.Extension;
                if (System.IO.File.Exists(PhotoPath))
                {
                    string Destination = Server.MapPath(@"\\Photo\\CustomerDocuments\\" + SOP00106.DocumentName + SOP00106.PhotoExtension);
                    if (System.IO.File.Exists(Destination))
                    {
                        FileInfo file_info = new FileInfo(Destination);
                        string dirPath = file_info.DirectoryName;
                        string fileName = Path.GetFileNameWithoutExtension(file_info.Name);
                        string[] files = Directory.GetFiles(dirPath);
                        int count = files.Count(file => { return file.Contains(fileName); });
                        string newFileName = String.Format("{0} ({1})", fileName, count);
                        SOP00106.DocumentName = newFileName;
                        Destination = Server.MapPath(@"\\Photo\\CustomerDocuments\\" + SOP00106.DocumentName + SOP00106.PhotoExtension);
                    }
                    System.IO.File.Move(PhotoPath, Destination);
                }
            }
            return Json(service.AddSOP00106(SOP00106), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCustomerDocument(SOP00106 SOP00106, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00106Services service = new SOP00106Services(KaizenUser);
            return Json(service.Update(SOP00106), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCustomerDocument(SOP00106 SOP00106, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            SOP00106Services service = new SOP00106Services(KaizenUser);
            string ServerPath = Server.MapPath(@"\\Photo\\CustomerDocuments\\");
            return Json(service.Delete(SOP00106, ServerPath), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCustomerDocumentTemp(IEnumerable<HttpPostedFileBase> CMS_CustomerDocumentattachments)
        {
            var fileName = "";
            foreach (var file in CMS_CustomerDocumentattachments)
            {
                fileName = Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/Photo/CustomerDocumentsTemp"), fileName);
                file.SaveAs(destinationPath);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveCustomerDocumentTemp(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Photo/CustomerDocumentsTemp"), fileName);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }
        #endregion

    }
}