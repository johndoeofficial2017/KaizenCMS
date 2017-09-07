using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.BusinessLogic.CMS;
using Kaizen.CMS;
using Kaizen.BusinessLogic.Configuration;
using System.IO;
using System.Web;
using Kaizen.BusinessLogic;

namespace UIServer.Areas.CMS.Controllers
{
    public class CMS_TRX_CaseUploadingController : Controller
    {
        // GET: CMS_TRX_CaseUploading 
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult DoAction(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult AdvancedAction(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        // -3
        public ActionResult UploadBach(string FileName, UploadedDataModel UploadedDataModel, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.UploadBach(FileName, UploadedDataModel.BatchID, UploadedDataModel.CurrencyCode
                , UploadedDataModel.DecimalDigit, UploadedDataModel.ExchangeTableID
                , UploadedDataModel.IsMultiply, UploadedDataModel.ExchangeRate, UploadedDataModel.ClientID
                , UploadedDataModel.ClientName, UploadedDataModel.ContractID, UploadedDataModel.ContractName
                , UploadedDataModel.CaseStatusID, UploadedDataModel.CaseStatusname
                , UploadedDataModel.CaseStatusChild, UploadedDataModel.CaseStatusChildName
                , UploadedDataModel.CaseStatusComment
                , UploadedDataModel.ReminderDateTime, UploadedDataModel.BookingDate, UploadedDataModel.ClosingDate
                , UploadedDataModel.AddressCode
                , UploadedDataModel.TRXTypeID), JsonRequestBehavior.AllowGet);
        }
        // -2
        public ActionResult UploadDebtorData(string TableName, IList<KaizenColumn> KaizenColumn, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            var destinationPath = Path.Combine(Server.MapPath("~/Files"), TableName);
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.UploadDebtorData(destinationPath, KaizenColumn), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCaseFields(string KaizenPublicKey, int TRXTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.GetAllByTRXTypeID(TRXTypeID), JsonRequestBehavior.AllowGet);
        }
        //11
        public ActionResult UploadCaseData(string TableName, IList<Kaizen.Configuration.Kaizen00015> KaizenColumn, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            var destinationPath = Path.Combine(Server.MapPath("~/Files"), TableName);
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.UploadCaseData(destinationPath, KaizenColumn), JsonRequestBehavior.AllowGet);
        }

        #region 0 to 6 
        public ActionResult ValidateMissingDebtorIDData(string KaizenPublicKey, int CurrentStep)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            DataCollection<CM00150> res = service.ValidateMissingDebtorIDData(CurrentStep, 5, 0);
            if (res == null || res.Items == null || res.Items.Count == 0)
            {
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            else
            {
                string result = string.Empty;
                switch (CurrentStep)
                {
                    case 0:
                        result = "mising EnglishFullName";
                        break;
                    case 1:
                        result = "Nationality is missing";
                        break;
                    case 2:
                        result = "Gender is missing";
                        break;
                    case 3:
                        result = "CUSTCLAS not definded";
                        break;
                    case 4:
                        result = "GroupID is not found";
                        break;
                    case 5:
                        result = "PriorityID is not found";
                        break;
                    case 6:
                        result = "DebtorStatusID is not found";
                        break;
                }
                result += " please contact system administrator";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetCaseMissingDebtorData([DataSourceRequest]DataSourceRequest request, int CurrentStep, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00203Services serv = new CM00203Services(KaizenUser);
            DataCollection<CM00150> L = new DataCollection<CM00150>();

            L = serv.ValidateMissingDebtorIDData(CurrentStep, request.PageSize, request.Page);
            DataSourceResult result;
            int tempPage = request.Page;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM00150
                {
                    DebtorID = o.DebtorID,
                    EnglishFullName = o.EnglishFullName
                }
                );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00150>(),
                    Total = 0
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 17
        public ActionResult ValidateCaseDuplicateForUploading(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            DataCollection<CM00151> res = service.ValidateCaseDuplicateForUploading(5, 0, "TableID", true);
            if (res == null || res.Items == null || res.Items.Count == 0)
            {
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            else
            {
                string result = string.Empty;
                result += "Data Duplicated please contact system administrator";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        //public JsonResult GetCaseDuplicateForUploading([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey)
        //{
        //    Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
        //    if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

        //    CM00203Services serv = new CM00203Services(KaizenUser);
        //    DataCollection<CM00151> L = new DataCollection<CM00151>();

        //    L = serv.ValidateCaseDuplicateForUploading(request.PageSize, request.Page, "TableID", true);
        //    DataSourceResult result;
        //    int tempPage = request.Page;
        //    if (L != null && L.Items != null)
        //    {
        //        request.Page = 1;
        //        result = L.Items.ToDataSourceResult(request,
        //        o => new CMS_CaseDuplicateForUploading
        //        {
        //            TableID = o.TableID,
        //            CaseAccountNo = o.CaseAccountNo,
        //            CIFNumber = o.CIFNumber,
        //            Duplicate = o.DuplicateRow
        //        }
        //        );
        //        result.Total = L.TotalItemCount;
        //        request.Page = tempPage;
        //    }
        //    else
        //    {
        //        result = new DataSourceResult()
        //        {
        //            Data = new List<CMS_CaseDuplicateForUploading>(),
        //            Total = 0
        //        };
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        #endregion
        #region 8 to 16


        #endregion
        #region 12 to 16
        public ActionResult ValidateMissingDate(string KaizenPublicKey, int CurrentStep)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            DataCollection<CM00151> res = service.GetCaseMissingData(CurrentStep, 5, 0);
            if (res == null || res.Items == null || res.Items.Count == 0)
            {
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            else
            {
                string result = string.Empty;
                switch (CurrentStep)
                {
                    case 12:
                        result = "mising AgentID";
                        break;
                    case 13:
                        result = "mising CaseAccountNo";
                        break;
                    case 14:
                        result = "missing ClaimAmount";
                        break;
                    case 15:
                        result = "CIFNumber no defind";
                        break;
                    case 16:
                        result = "Database Validation";
                        break;
                }
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult GetCaseMissingData([DataSourceRequest]DataSourceRequest request, int CurrentStep, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00203Services serv = new CM00203Services(KaizenUser);
            DataCollection<CM00151> L = new DataCollection<CM00151>();

            L = serv.GetCaseMissingData(CurrentStep, request.PageSize, request.Page);
            DataSourceResult result;
            int tempPage = request.Page;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM00151
                {
                    CIFNumber = o.CIFNumber,
                    CaseAccountNo = o.CaseAccountNo,
                    ClaimAmount = o.ClaimAmount,
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
                    Data = new List<CM00151>(),
                    Total = 0
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 
        public ActionResult PostDebtorData(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.PostDebtorData(), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region CM_CaseOld 18
        public ActionResult ValidateCaseOld(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);

            DataCollection<CM00155> res = service.ValidateOldCases(5, 0, "CaseRef", true, "");
            if (res == null || res.Items == null || res.Items.Count == 0)
                return Json("OK", JsonRequestBehavior.AllowGet);
            return Json("CM_CaseDuplicate", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCaseOldForUploading([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00203Services serv = new CM00203Services(KaizenUser);
            DataCollection<CM00155> L = new DataCollection<CM00155>();
            string filters = string.Empty;
            string SortMember = "CaseRef";
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
            L = serv.ValidateOldCases(request.PageSize, request.Page, SortMember, IsAscending, "");
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM00203
                {
                    CaseRef = o.CaseRef,
                    CIFNumber = o.CIFNumber,
                    CIFName = o.CIFName,
                    CaseAccountNo = o.CaseAccountNo,
                    ClaimAmount = o.ClaimAmount
                }
                );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00203>(),
                    Total = 0
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCaseOldAdvancedUploading([DataSourceRequest]DataSourceRequest request
            , string CaseStatusID, string CaseStatusChild, string CaseAccountNo,
            string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00203Services serv = new CM00203Services(KaizenUser);
            DataCollection<CM00155> L = new DataCollection<CM00155>();
            string filters = string.Empty;
            string SortMember = "CaseRef";
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
            if (request.Filters.Count > 0)
                filters = Help.ApplyFilter(request.Filters[0]).Trim();
            if (!string.IsNullOrEmpty(CaseStatusID))
            {
                if (!string.IsNullOrEmpty(filters))
                    filters += " and";
                filters += " CaseStatusID=" + CaseStatusID;
            }
            if (!string.IsNullOrEmpty(CaseStatusChild))
            {
                if (!string.IsNullOrEmpty(filters))
                    filters += " and";
                filters += " CaseStatusID=" + CaseStatusID;
            }
            if (!string.IsNullOrEmpty(CaseAccountNo))
            {
                if (!string.IsNullOrEmpty(filters))
                    filters += " and";
                filters += " CaseAccountNo='" + CaseAccountNo + "'";
            }
            L = serv.ValidateOldCases(request.PageSize, request.Page, SortMember, IsAscending, filters);
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM00203
                {
                    CaseRef = o.CaseRef,
                    CIFNumber = o.CIFNumber,
                    CIFName = o.CIFName,
                    CaseAccountNo = o.CaseAccountNo,
                    ClaimAmount = o.ClaimAmount,
                    //BookingDate = o.BookingDate ,
                    AgentID = o.AgentID 
                    ,IsJoint = o.IsJoint 
                }
                );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00203>(),
                    Total = 0
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CaseCheckedhistryAll(bool IsJoint, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.CaseCheckedhistryAll( IsJoint), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CaseCheckedhistry(string CaseRef,bool IsJoint ,  string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.CaseCheckedhistry(CaseRef, IsJoint), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CaseArchive_Click(string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.CaseArchive(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CaseDelete_Click(string UserName, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.CaseDelete(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DoAdvancedAction_click(CM10701 oCM10701, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.UpdateCasesStatus(oCM10701), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CaseDuplicateForUploading
        public ActionResult ValidateRepeatedCases(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);

            DataCollection<CM00151> res = service.ValidateRepeatedCases(5, 0, "CIFNumber", true);
            if (res == null || res.Items == null || res.Items.Count == 0)
                return Json("OK", JsonRequestBehavior.AllowGet);
            return Json("CM_CaseDuplicate", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDuplicateCases([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00203Services serv = new CM00203Services(KaizenUser);
            DataCollection<CM00151> L = new DataCollection<CM00151>();
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
                SortMember = "CIFNumber";
                IsAscending = true;
            }
            L = serv.ValidateRepeatedCases(request.PageSize, request.Page, SortMember, IsAscending);
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM00151
                {
                    CaseRef = o.CaseRef,
                    CIFNumber = o.CIFNumber,
                    CaseAccountNo = o.CaseAccountNo,
                    //CIFName = o.CIFName,
                    Remarks = o.Remarks,
                    AgentID = o.AgentID,
                    InvoiceNumber = o.InvoiceNumber,
                    ClaimAmount = o.ClaimAmount
                }
                );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00151>(),
                    Total = 0
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region New Cases 
        public ActionResult ValidateCaseNew(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);

            DataCollection<CM00151> res = service.ValidateCaseNew(5, 0, "TableID", true);
            if (res == null || res.Items == null || res.Items.Count == 0)
                return Json("OK", JsonRequestBehavior.AllowGet);
            return Json("CM_CaseDuplicate", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCaseNew([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00203Services serv = new CM00203Services(KaizenUser);
            DataCollection<CM00151> L = new DataCollection<CM00151>();
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
                SortMember = "TableID";
                IsAscending = true;
            }
            L = serv.ValidateCaseNew(request.PageSize, request.Page, SortMember, IsAscending);
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM00151
                {
                    CIFNumber = o.CIFNumber,
                    //CIFName = o.CIFName,
                    CaseAccountNo = o.CaseAccountNo,
                    ClaimAmount = o.ClaimAmount
                }
                );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00151>(),
                    Total = 0
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult PostUploadCases(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.PostUploadCases(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DynamicOverride(CM00151 UploadedOBJECT, int CurrentStep, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.DynamicOverride(UploadedOBJECT, CurrentStep), JsonRequestBehavior.AllowGet);
        }
        public ActionResult OverrideAll(int CurrentStep, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.OverrideAll(CurrentStep), JsonRequestBehavior.AllowGet);
        }
        //-----------------------------------------------------
        public ActionResult ChangeOldCasesStatus(ChangeStatusDataModel ChangeStatusDataModel, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.ChangeOldCasesStatus(ChangeStatusDataModel.CaseStatusID
                , ChangeStatusDataModel.CaseStatusname, ChangeStatusDataModel.CaseStatusChild
                , ChangeStatusDataModel.CaseStatusChildName, ChangeStatusDataModel.CaseStatusComment), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCaseOptions(int CurrentStep, UpdateOptionsDataModel UpdateOptionsDataModel, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.UpdateCaseOptions(CurrentStep, UpdateOptionsDataModel.ClaimAmount
                , UpdateOptionsDataModel.Remarks, UpdateOptionsDataModel.AgentID
                , UpdateOptionsDataModel.InvoiceNumber), JsonRequestBehavior.AllowGet);
        }
      
        public ActionResult DebtorDetails(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00009Services srv = new CM00009Services(KaizenUser);
            ViewData["AddressTypes"] = srv.GetAll();
            return PartialView();
        }



        public ActionResult DynamicCancel(CM_UploadValidate00 UploadedOBJECT, int CurrentStep, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.DynamicCancel(UploadedOBJECT, CurrentStep), JsonRequestBehavior.AllowGet);
        }


        #region Upload Data
        public ActionResult SaveUploadFileTemp(IEnumerable<HttpPostedFileBase> CaseUploadFileAttachemnt)
        {
            var fileName = "";
            List<Kaizen.Configuration.ExcelColumns> lstExcelColumns = null;
            foreach (var file in CaseUploadFileAttachemnt)
            {
                try
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                    var destinationPath = Path.Combine(Server.MapPath("~/Files"), fileName);
                    file.SaveAs(destinationPath);
                    CompanyacessServices srv = new CompanyacessServices();
                    lstExcelColumns = srv.ReadExcelColumnsWithoutInsert(destinationPath, fileName);
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex, "SaveUploadFileTemp");
                }
            }
            TempTable o = new TempTable();
            o.TableName = fileName;
            o.ExcelColumns = lstExcelColumns;
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveUploadFiletemp(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Files"), fileName);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }


        //02 
        public ActionResult UploadingValidation(int CurrentStep, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.UploadingValidation(CurrentStep), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UploadingValidationOldCase(string ClientID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.UploadingValidationOldCase(ClientID), JsonRequestBehavior.AllowGet);
        }





        #region Debtor Validation
        private DataSourceResult CaseCM_UploadValidate00List([DataSourceRequest]DataSourceRequest request, DataCollection<CM_UploadValidate00> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM_UploadValidate00
                {
                    DebtorID = o.DebtorID,
                    OldValue = o.OldValue,
                    NewValue = o.NewValue,
                    UserName = o.UserName,
                    ConditionVariable = o.ConditionVariable,
                    AcctionStep = o.AcctionStep
                }
                );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM_UploadValidate00>(),
                    Total = 0
                };
            }
            return result;
        }




        #endregion






        #region CM_CaseNewForUploading
        private DataSourceResult CM_CaseNewForUploadingList([DataSourceRequest]DataSourceRequest request, DataCollection<CM_CaseNewForUploading> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM_CaseNewForUploading
                {
                    DebtorID = o.DebtorID,
                    CaseAccountNo = o.CaseAccountNo,
                    ClaimAmount = o.ClaimAmount,
                    AgentID = o.AgentID,
                    AssignDate = o.AssignDate,
                    BucketID = o.BucketID,
                    CaseAddess = o.CaseAddess,
                    CaseRef = o.CaseRef,
                    //CaseStatusChild = o.CaseStatusChild,
                    //CaseStatusComment = o.CaseStatusComment,
                    //CaseStatusID = o.CaseStatusID,
                    //CaseStatusname = o.CaseStatusname,
                    //ClientID = o.ClientID,
                    //ContractID = o.ContractID,
                    //CurrencyCode = o.CurrencyCode,
                    //DecimalDigit = o.DecimalDigit,
                    //ExchangeRate = o.ExchangeRate,
                    //InvoiceNumber = o.InvoiceNumber,
                    //IsMultiply = o.IsMultiply,
                    LastCallactedAMT = o.LastCallactedAMT,
                    LastPaymentBy = o.LastPaymentBy,
                    LastPaymentDate = o.LastPaymentDate,
                    OSAmount = o.OSAmount,
                    PrincipleAmount = o.PrincipleAmount,
                    Remarks = o.Remarks,
                    //ReminderDateTime = o.ReminderDateTime,
                    //TotalCallactedAMT = o.TotalCallactedAMT,
                    //TRXTypeID = o.TRXTypeID
                }
                );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM_UploadValidateResidenceNo>(),
                    Total = 0
                };
            }
            return result;
        }

        public ActionResult CancelCM_CaseNewForUploading(CM_CaseNewForUploading UploadedOBJECT, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.CancelCM_CaseNewForUploading(UploadedOBJECT), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CancelAllCM_CaseNewForUploading(IList<CM_CaseNewForUploading> UploadedOBJECT, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.CancelCM_CaseNewForUploading(UploadedOBJECT), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CM_CaseOldForUploading
        private DataSourceResult CM_CaseOldForUploadingList([DataSourceRequest]DataSourceRequest request, DataCollection<CM_CaseOldForUploading> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM_CaseOldForUploading
                {
                    DebtorID = o.DebtorID,
                    CaseAccountNo = o.CaseAccountNo,
                    ClaimAmount = o.ClaimAmount,
                    AgentID = o.AgentID,
                    CaseRef = o.CaseRef,
                    DebtorName = o.DebtorName,
                    CaseStatusname = o.CaseStatusname,
                    Remarks = o.Remarks
                }
                );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM_UploadValidateResidenceNo>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetCM_CaseOldForUploadingValidate(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            DataCollection<CM_CaseOldForUploading> L = new DataCollection<CM_CaseOldForUploading>();
            L = service.GetCM_CaseOldForUploading("", 10, 0, "CaseAccountNo", true);
            return Json(L == null ? null : L.Items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCM_CaseOldForUploading([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00203Services serv = new CM00203Services(KaizenUser);
            DataCollection<CM_CaseOldForUploading> L = new DataCollection<CM_CaseOldForUploading>();
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
                SortMember = "DebtorID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = serv.GetCM_CaseOldForUploading(SQLQuery
               , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CM_CaseOldForUploadingList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion




        #endregion


    }
    public class UploadedDataModel
    {
        public string BatchID { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string ContractID { get; set; }
        public string ContractName { get; set; }
        public string CurrencyCode { get; set; }
        public byte DecimalDigit { get; set; }
        public string ExchangeTableID { get; set; }
        public bool IsMultiply { get; set; }
        public double ExchangeRate { get; set; }
        public int TRXTypeID { get; set; }
        public string TrxTypeName { get; set; }
        public int CaseStatusID { get; set; }
        public int CaseStatusChild { get; set; }
        public string CaseStatusname { get; set; }
        public string CaseStatusChildName { get; set; }
        public string CaseStatusComment { get; set; }
        public DateTime? ReminderDateTime { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string AddressCode { get; set; }
    }
    public class ChangeStatusDataModel
    {
        public int CaseStatusID { get; set; }
        public string CaseStatusname { get; set; }
        public int CaseStatusChild { get; set; }
        public string CaseStatusChildName { get; set; }
        public string CaseStatusComment { get; set; }

    }
    public class UpdateOptionsDataModel
    {
        public bool ClaimAmount { get; set; }
        public bool Remarks { get; set; }
        public bool AgentID { get; set; }
        public bool InvoiceNumber { get; set; }

    }
    public class SelectedCaseDataModel
    {
        public string CaseRef { get; set; }
    }
}