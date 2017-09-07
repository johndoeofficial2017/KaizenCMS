using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Kaizen.Admin;
using Kaizen.BusinessLogic.Admin;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.BusinessLogic.CMS;
using Kaizen.CMS;
using Kaizen.BusinessLogic.Configuration;


namespace UIServer.Areas.CMS.Controllers
{
    public class CO_PartnerController : Controller
    {
        // GET: CMS/CO_Partner
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
            CM00008Services srv = new CM00008Services(KaizenUser);
            ViewData["AddressTypes"] = srv.GetAll();
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
        public ActionResult PopUpByClass(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, Kaizen.CMS.DataCollection<CM00130> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00130
               {
                   PartnerID = o.PartnerID,
                   PartnerName = o.PartnerName,
                   TotalCaseNumber = o.TotalCaseNumber
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00130>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetListPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
     , string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00130Services invService = new CM00130Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00130> L = new Kaizen.CMS.DataCollection<CM00130>();
            string filters = string.Empty;
            string SortMember = "PartnerID";
            bool IsAscending = true;
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

            L = invService.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetListPopUpByClassGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria, string CUSTCLAS)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00130Services invService = new CM00130Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00130> L = new Kaizen.CMS.DataCollection<CM00130>();
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
                SortMember = "ClientID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, CUSTCLAS
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00130Services serv = new CM00130Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00130> L = new Kaizen.CMS.DataCollection<CM00130>();
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
                SortMember = "ClientID";
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

        public ActionResult GetSingle(string KaizenPublicKey, string PartnerID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00130Services service = new CM00130Services(KaizenUser);
            CM00130 o = service.GetSingle(PartnerID);
            //CM00130 L = new CM00130()
            //{
            //    PartnerID = o.PartnerID,
            //    PartnerClassID = o.PartnerClassID,
            //    StatusID = o.StatusID,
            //    PhotoExtension = o.PhotoExtension,
            //    ParentClientID = o.ParentClientID,
            //    //VendorID = o.VendorID,
            //    AddressCode = o.AddressCode,
            //    BillAddressCode = o.BillAddressCode,
            //    ClaimAmount = o.ClaimAmount,
                
            //};
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateData(CM00130 CM00130, string KaizenPublicKey, bool PhotoChanged)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            int startindex;
            if (PhotoChanged)
            {
                if (!string.IsNullOrEmpty(CM00130.PhotoExtension))
                {
                    string PhotoPath = @"\\Photo\\KHCB\\PartnerPhotoTemp\\" + CM00130.PhotoExtension.Trim();
                    startindex = CM00130.PhotoExtension.LastIndexOf('.');
                    startindex += 1;
                    CM00130.PhotoExtension = CM00130.PhotoExtension.Substring(startindex, CM00130.PhotoExtension.Length - startindex);
                    if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                    {
                        string Destination = Server.MapPath(@"\\Photo\\KHCB\\PartnerPhotoTemp\\" + CM00130.PartnerID.Trim() + "." + CM00130.PhotoExtension);
                        if (System.IO.File.Exists(Destination))
                        {
                            System.IO.File.Delete(Destination);
                        }
                        System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                    }
                }
            }
            CM00130Services service = new CM00130Services(KaizenUser);
            return Json(service.Update(CM00130), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveData(CM00130 CM00130, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            int startindex;
            if (!string.IsNullOrEmpty(CM00130.PhotoExtension))
            {
                string PhotoPath = @"\\Photo\\KHCB\\PartnerPhotoTemp\\" + CM00130.PhotoExtension.Trim();
                startindex = CM00130.PhotoExtension.LastIndexOf('.');
                startindex += 1;
                CM00130.PhotoExtension = CM00130.PhotoExtension.Substring(startindex, CM00130.PhotoExtension.Length - startindex);
                if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                {
                    string Destination = Server.MapPath(@"\\Photo\\KHCB\\PartnerPhotoTemp\\" + CM00130.PartnerID.Trim() + "." + CM00130.PhotoExtension);
                    if (System.IO.File.Exists(Destination))
                    {
                        System.IO.File.Delete(Destination);
                    }
                    System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                }
            }
            CM00130Services service = new CM00130Services(KaizenUser);
            return Json(service.AddCM00130(CM00130), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(CM00130 CM00130, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CM00130Services service = new CM00130Services(KaizenUser);
            return Json(service.Delete(CM00130.PartnerID), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveImageTemp(IEnumerable<HttpPostedFileBase> attachments)
        {
            var fileName = "";
            foreach (var file in attachments)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/Photo/KHCB/PartnerPhotoTemp"), fileName);
                file.SaveAs(destinationPath);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveImagetemp(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Photo/KHCB/PartnerPhotoTemp"), fileName);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }


        #region Client Contact
        public ActionResult ClientContact(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen.BusinessLogic.Admin.Sys00006Services srv = new Kaizen.BusinessLogic.Admin.Sys00006Services(KaizenUser);
            ViewData["ContactTypes"] = srv.GetAll(1);
            return PartialView();
        }
        public ActionResult ClientContactPopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen.BusinessLogic.Admin.Sys00006Services srv = new Kaizen.BusinessLogic.Admin.Sys00006Services(KaizenUser);
            ViewData["ContactTypes"] = srv.GetAll(1);
            return PartialView();
        }

        private DataSourceResult ClientContactList([DataSourceRequest]DataSourceRequest request, Kaizen.CMS.DataCollection<CM00115> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00115
               {
                   ContactTypeID = o.ContactTypeID,
                   ContactPerson = o.ContactPerson,
                   PersonPosition = o.PersonPosition,
                   PersonEmailAdd = o.PersonEmailAdd,
                   OtherInfo = o.OtherInfo,
                   ClientID = o.ClientID,
                   Ext1 = o.Ext1,
                   Ext2 = o.Ext2,
                   MobileNo1 = o.MobileNo1,
                   MobileNo2 = o.MobileNo2,
                   Pnone01 = o.Pnone01,
                   Pnone02 = o.Pnone02
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00115>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetContactListGridWithClient([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string ClientID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00115Services serv = new CM00115Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00115> L = new Kaizen.CMS.DataCollection<CM00115>();
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

            if (!string.IsNullOrEmpty(ClientID))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, ClientID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = ClientContactList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveClientContact(CM00115 CM00115, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00115Services service = new CM00115Services(KaizenUser);
            return Json(service.AddCM00115(CM00115), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateClientContact(CM00115 CM00115, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00115Services service = new CM00115Services(KaizenUser);
            return Json(service.Update(CM00115), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteClientContact(CM00115 CM00115, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00115Services service = new CM00115Services(KaizenUser);
            return Json(service.Delete(CM00115), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Client Address
        public ActionResult ClientAddress(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00008Services srv = new CM00008Services(KaizenUser);
            ViewData["AddressTypes"] = srv.GetAll();
            return PartialView();
        }
        public ActionResult ClientAddressPopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ClientAddressTypeFillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00008Services service = new CM00008Services(KaizenUser);
            IList<CM00008> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        private DataSourceResult ClientAddressList([DataSourceRequest]DataSourceRequest request, Kaizen.CMS.DataCollection<CM00111> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00111
               {
                   AddressCode = o.AddressCode,
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
                   ClientID = o.ClientID,
                   Ext1 = o.Ext1,
                   Ext2 = o.Ext2,
                   Ext3 = o.Ext3,
                   Ext4 = o.Ext4
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00111>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetAddressListGridWithPartner([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string PartnerID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00111Services serv = new CM00111Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00111> L = new Kaizen.CMS.DataCollection<CM00111>();
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
                SortMember = "AddressCode";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(PartnerID))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, PartnerID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = ClientAddressList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveClientAddress(CM00111 CM00111, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00111Services service = new CM00111Services(KaizenUser);
            return Json(service.AddCM00111(CM00111), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveClientAddresses(IList<CM00111> CM00111, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00111Services service = new CM00111Services(KaizenUser);
            return Json(service.AddCM00111(CM00111), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateClientAddress(CM00111 CM00111, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00111Services service = new CM00111Services(KaizenUser);
            return Json(service.Update(CM00111), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeletePartnerAddress(CM00131 CM00131, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00130Services service = new CM00130Services(KaizenUser);
            return Json(service.DeletePartnerAddress(CM00131), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Client Document
        public ActionResult ClientDocument(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen.BusinessLogic.Admin.Sys00007Services srv = new Kaizen.BusinessLogic.Admin.Sys00007Services(KaizenUser);
            ViewData["DocumentTypes"] = srv.GetAll(1);
            return PartialView();
        }
        private DataSourceResult ClientDocumentList([DataSourceRequest]DataSourceRequest request, Kaizen.CMS.DataCollection<CM00112> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00112
               {
                   DocumentID = o.DocumentID,
                   ClientID = o.ClientID,
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
                    Data = new List<CM00112>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetDocumentListGridWithClient([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string ClientID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00112Services serv = new CM00112Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00112> L = new Kaizen.CMS.DataCollection<CM00112>();
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

            if (!string.IsNullOrEmpty(ClientID))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, ClientID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = ClientDocumentList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveClientDocument(CM00112 CM00112, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00112Services service = new CM00112Services(KaizenUser);
            if (!string.IsNullOrEmpty(CM00112.PhotoExtension))
            {
                string PhotoPath = Server.MapPath(@"\\Photo\\ClientDocumentsTemp\\" + CM00112.PhotoExtension.Trim());
                FileInfo TempFile_info = new FileInfo(PhotoPath);
                CM00112.PhotoExtension = TempFile_info.Extension;
                if (System.IO.File.Exists(PhotoPath))
                {
                    string Destination = Server.MapPath(@"\\Photo\\ClientDocuments\\" + CM00112.DocumentName + CM00112.PhotoExtension);
                    if (System.IO.File.Exists(Destination))
                    {
                        FileInfo file_info = new FileInfo(Destination);
                        string dirPath = file_info.DirectoryName;
                        string fileName = Path.GetFileNameWithoutExtension(file_info.Name);
                        string[] files = Directory.GetFiles(dirPath);
                        int count = files.Count(file => { return file.Contains(fileName); });
                        string newFileName = String.Format("{0} ({1})", fileName, count);
                        CM00112.DocumentName = newFileName;
                        Destination = Server.MapPath(@"\\Photo\\ClientDocuments\\" + CM00112.DocumentName + CM00112.PhotoExtension);
                    }
                    System.IO.File.Move(PhotoPath, Destination);
                }
            }
            return Json(service.AddCM00112(CM00112), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateClientDocument(CM00112 CM00112, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00112Services service = new CM00112Services(KaizenUser);
            return Json(service.Update(CM00112), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteClientDocument(CM00112 CM00112, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00112Services service = new CM00112Services(KaizenUser);
            string ServerPath = Server.MapPath(@"\\Photo\\ClientDocuments\\");
            return Json(service.Delete(CM00112, ServerPath), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveClientDocumentTemp(IEnumerable<HttpPostedFileBase> CMS_ClientDocumentattachments)
        {
            var fileName = "";
            foreach (var file in CMS_ClientDocumentattachments)
            {
                fileName = Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/Photo/ClientDocumentsTemp"), fileName);
                file.SaveAs(destinationPath);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveClientDocumentTemp(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Photo/ClientDocumentsTemp"), fileName);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }
        #endregion
        public string GetNextClient(string CUSTCLAS, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return "";
            if (KaizenPublicKey.Trim() == "undefined") return "";
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return "";
            if (!User.Identity.IsAuthenticated) return "";
            CM00130Services service = new CM00130Services(KaizenUser);
            return service.GetNextClient(CUSTCLAS);
        }

        #region - Create parent
        public ActionResult GetParentClassList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00130Services service = new CM00130Services(KaizenUser);
            IList<CM00002> L = service.GetParentClassList();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region - Partner Address
        public ActionResult GetParentAddressTypes(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00130Services service = new CM00130Services(KaizenUser);
            IList<CM00033> L = service.GetParentAddressTypes();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SavePartnerAddressData(CM00131 CM00131, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            
            CM00130Services service = new CM00130Services(KaizenUser);
            return Json(service.AddCM00131(CM00131), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdatePartnerAddressData(CM00131 CM00131, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");

            CM00130Services service = new CM00130Services(KaizenUser);
            return Json(service.UpdateCM00131(CM00131), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPartnerAddressData(int addressCodeType,string PartnerID,string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00130Services service = new CM00130Services(KaizenUser);
            IList<CM00131> L = service.GetPartnerAddressData(addressCodeType, PartnerID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPartnerAddressDataByAddressCodeList(int[] addressCodeTypeList, string PartnerID, string KaizenPublicKey)
        {
            //int[] arrList = addressCodeTypeList.Split(',').Select(x => int.Parse(x)).ToArray();
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00130Services service = new CM00130Services(KaizenUser);
            IList<CM00131> L = service.GetPartnerAddressData(addressCodeTypeList, PartnerID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPartnerAddressDataByPartnerID(string PartnerID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00130Services service = new CM00130Services(KaizenUser);
            IList<CM00131> L = service.GetPartnerAddressDataByPartnerID(PartnerID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetCountries(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Sys00013Services service = new Sys00013Services(KaizenUser);
            IList<Sys00013> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCities(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Sys00014Services service = new Sys00014Services(KaizenUser);
            IList<Sys00014> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}