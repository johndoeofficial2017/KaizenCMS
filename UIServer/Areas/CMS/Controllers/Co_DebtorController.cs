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
    public class Co_DebtorController : Controller
    {
        // GET: Co_Debtor
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
            //CM00009Services srv = new CM00009Services(KaizenUser);
            //ViewData["AddressTypes"] = srv.GetAll();
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
        public ActionResult SendEmails(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult UploadDebtor(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult DebtorDetails(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00009Services srv = new CM00009Services(KaizenUser);
            ViewData["AddressTypes"] = srv.GetAll();
            return PartialView();
        }
        public ActionResult AddDebtorDetails(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult JointDebtorDetails(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00100> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00100
               {
                   DebtorID = o.DebtorID.Trim(),
                   GenderID = o.GenderID,
                   BirthDate = o.BirthDate,
                   PLACE_OF_BIRTH = o.PLACE_OF_BIRTH,
                   COUNTRY_OF_BIRTH = o.COUNTRY_OF_BIRTH,
                   MaritalID = o.MaritalID,
                   DebtorDescription = o.DebtorDescription,
                   FirstNameArabic = o.FirstNameArabic,
                   FirstNameEnglish = o.FirstNameEnglish,
                   EnglishFullName = o.EnglishFullName,
                   ArabicFullName = o.ArabicFullName,
                   SHORT_NAME_ARAB = o.SHORT_NAME_ARAB,
                   SHORT_NAME_ENG = o.SHORT_NAME_ENG,
                   MiddleName1English = o.MiddleName1English,
                   MiddleName2English = o.MiddleName2English,
                   MiddleName3English = o.MiddleName3English,
                   MiddleName4English = o.MiddleName4English,
                   MiddleName1Arabic = o.MiddleName1Arabic,
                   MiddleName2Arabic = o.MiddleName2Arabic,
                   MiddleName3Arabic = o.MiddleName3Arabic,
                   MOTHER_FIRST_NAME = o.MOTHER_FIRST_NAME,
                   MOTHER_LAST_NAME = o.MOTHER_LAST_NAME,
                   ContactTypeID = o.ContactTypeID,
                   AddressCode = o.AddressCode,
                   AddressEnglish = o.AddressEnglish,
                   AddressArabic = o.AddressArabic,
                   ResidenceNo = o.ResidenceNo,
                   GovernorateNo = o.GovernorateNo,
                   GovernorateNameEnglish = o.GovernorateNameEnglish,
                   EmployerFlag1 = o.EmployerFlag1,
                   EmployerName1 = o.EmployerName1,
                   EmployerNo1 = o.EmployerNo1,
                   EmployerNameArabic = o.EmployerNameArabic,
                   EmploymentFlag = o.EmploymentFlag,
                   LatestEducationDegree = o.LatestEducationDegree,
                   EmploymentNameEnglish = o.EmploymentNameEnglish,
                   LaborForceParticipation = o.LaborForceParticipation,

                   DebtorStatusID = o.DebtorStatusID,
                   OccupationDescription1 = o.OccupationDescription1,
                   OccupationEnglish = o.OccupationEnglish,
                   SponsorCPRNoorUnitNo = o.SponsorCPRNoorUnitNo,
                   SponsorFlag = o.SponsorFlag,
                   SponsorName = o.SponsorName,
                   SponserId = o.SponserId,
                   SponserNameEnglish = o.SponserNameEnglish,
                   FingerprintCode = o.FingerprintCode,
                   IdNumber = o.IdNumber,
                   ParentDebtor = o.ParentDebtor,
                   ReligionID = o.ReligionID,
                   CUSTCLAS = o.CUSTCLAS,
                   GroupID = o.GroupID,
                   PriorityID = o.PriorityID,
                   IsHold = o.IsHold,
                   IsActive = o.IsActive,
                   PhotoExtension = o.PhotoExtension,
                   SignatureExtension = o.SignatureExtension,
                   CPRExpiryDate = o.CPRExpiryDate,
                   CPRIssueDate = o.CPRIssueDate,
                   CPRCRNo = o.CPRCRNo,
                   PassportExpiryDate = o.PassportExpiryDate,
                   PassportIssueDate = o.PassportIssueDate,
                   PassportNumber = o.PassportNumber,
                   PassportSequnceNo = o.PassportSequnceNo,
                   PassportType = o.PassportType,
                   CPRSerialNumber = o.CPRSerialNumber,
                   VisaNo = o.VisaNo,
                   VisaExpiryDate = o.VisaExpiryDate,
                   VisaType = o.VisaType,
                   ResidentPermitNo = o.ResidentPermitNo,
                   ResidentPermitExpiryDate = o.ResidentPermitExpiryDate,
                   TypeOfResident = o.TypeOfResident,
                   NationalityID = o.NationalityID,
                   NO_OF_DEPENDENTS = o.NO_OF_DEPENDENTS,
                   MonthlySalary = o.MonthlySalary,
                   AMT01 = o.AMT01,
                   AMT02 = o.AMT02,
                   AMT03 = o.AMT03,
                   TxtField01 = o.TxtField01,
                   TxtField02 = o.TxtField02,
                   TxtField03 = o.TxtField03,
                   TxtField04 = o.TxtField04,
                   CheckBoxField01 = o.CheckBoxField01,
                   CheckBoxField02 = o.CheckBoxField02,
                   CheckBoxField03 = o.CheckBoxField03,
                   CheckBoxField04 = o.CheckBoxField04,
                   Date01 = o.Date01,
                   Date02 = o.Date02,
                   Date03 = o.Date03,
                   Date04 = o.Date04
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
        public ActionResult GetListPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
     , string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00100Services invService = new CM00100Services(KaizenUser);
            DataCollection<CM00100> L = new DataCollection<CM00100>();
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

            L = invService.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //Main Grid
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, int viewID,string ViewWhereCondition,string ViewSortCondition , string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(new DataSourceResult(), JsonRequestBehavior.AllowGet);
            CM00100Services serv = new CM00100Services(KaizenUser);
            DataCollection<CM00100> L = new DataCollection<CM00100>();
            string filters = string.Empty;
            string SortMember = "DebtorID";
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
            //if (!string.IsNullOrEmpty(SQLQuery) && !string.IsNullOrEmpty(AddressCode))
            //    SQLQuery += " and ";
            //if (!string.IsNullOrEmpty(AddressCode) && AddressCode != "KaizenSystem")
            //    SQLQuery = "AddressCode='" + AddressCode.Trim() + "'";

            L = serv.GetWhereListWithPaging(SQLQuery, Searchcritaria, ViewWhereCondition, ViewSortCondition, request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetListReminderPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00100Services invService = new CM00100Services(KaizenUser);
            DataCollection<CM00100> L = new DataCollection<CM00100>();
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

            L = invService.SearchDebtorData(SQLQuery, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetChildListGridWithDebtorID([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string DebtorID, string Searchcritaria, string FieldID = "-1", int FltrOperator = 8)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00100Services serv = new CM00100Services(KaizenUser);
            DataCollection<CM00100> L = new DataCollection<CM00100>();
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

            if (!string.IsNullOrEmpty(DebtorID))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, DebtorID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult GetSingle(string KaizenPublicKey, string DebtorID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00100Services service = new CM00100Services(KaizenUser);
            CM00100 o = service.GetSingle(DebtorID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTop10BYDebtorID(string KaizenPublicKey, [DataSourceRequest]DataSourceRequest request, string DebtorID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00100Services invService = new CM00100Services(KaizenUser);
            DataCollection<CM00100> L = new DataCollection<CM00100>();
            if (DebtorID != null && !string.IsNullOrEmpty(DebtorID))
                L = invService.GetTop10BYDebtorID(DebtorID);
            else
                L = null;
            DataSourceResult result = CaseList(request, L);
            var r = Json(result.Data, JsonRequestBehavior.AllowGet);
            r.MaxJsonLength = int.MaxValue;
            return r;
        }

        public ActionResult UpdateData(CM00100 CM00100, string KaizenPublicKey, bool PhotoChanged, bool SignatureChanged)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            int startindex;
            if (PhotoChanged)
            {
                if (!string.IsNullOrEmpty(CM00100.PhotoExtension))
                {
                    string PhotoPath = @"\\Photo\\DebtorPhotoTemp\\" + CM00100.PhotoExtension.Trim();
                    startindex = CM00100.PhotoExtension.LastIndexOf('.');
                    startindex += 1;
                    CM00100.PhotoExtension = CM00100.PhotoExtension.Substring(startindex, CM00100.PhotoExtension.Length - startindex);
                    if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                    {
                        string Destination = Server.MapPath(@"\\Photo\\DebtorPhoto\\" + CM00100.DebtorID.Trim() + "." + CM00100.PhotoExtension);
                        if (System.IO.File.Exists(Destination))
                        {
                            System.IO.File.Delete(Destination);
                        }
                        System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                    }
                }
            }
            if (SignatureChanged)
            {
                if (!string.IsNullOrEmpty(CM00100.SignatureExtension))
                {
                    string SignaturePath = @"\\Photo\\DebtorSignatureTemp\\" + CM00100.SignatureExtension.Trim();
                    startindex = CM00100.SignatureExtension.LastIndexOf('.');
                    startindex += 1;
                    CM00100.SignatureExtension = CM00100.SignatureExtension.Substring(startindex, CM00100.SignatureExtension.Length - startindex);
                    if (System.IO.File.Exists(Server.MapPath(SignaturePath)))
                    {
                        string Destination = Server.MapPath(@"\\Photo\\DebtorSignature\\" + CM00100.DebtorID.Trim() + "." + CM00100.SignatureExtension);
                        if (System.IO.File.Exists(Destination))
                        {
                            System.IO.File.Delete(Destination);
                        }
                        System.IO.File.Move(Server.MapPath(SignaturePath), Destination);
                    }
                }
            }
            CM00100Services service = new CM00100Services(KaizenUser);
            return Json(service.Update(CM00100), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveData(CM00100 CM00100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            int startindex;
            if (!string.IsNullOrEmpty(CM00100.PhotoExtension))
            {
                string PhotoPath = @"\\Photo\\DebtorPhotoTemp\\" + CM00100.PhotoExtension.Trim();
                startindex = CM00100.PhotoExtension.LastIndexOf('.');
                startindex += 1;
                CM00100.PhotoExtension = CM00100.PhotoExtension.Substring(startindex, CM00100.PhotoExtension.Length - startindex);
                if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                {
                    string Destination = Server.MapPath(@"\\Photo\\DebtorPhoto\\" + CM00100.DebtorID.Trim() + "." + CM00100.PhotoExtension);
                    if (System.IO.File.Exists(Destination))
                    {
                        System.IO.File.Delete(Destination);
                    }
                    System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                }
            }
            if (!string.IsNullOrEmpty(CM00100.SignatureExtension))
            {
                string SignaturePath = @"\\Photo\\DebtorSignatureTemp\\" + CM00100.SignatureExtension.Trim();
                startindex = CM00100.SignatureExtension.LastIndexOf('.');
                startindex += 1;
                CM00100.SignatureExtension = CM00100.SignatureExtension.Substring(startindex, CM00100.SignatureExtension.Length - startindex);
                if (System.IO.File.Exists(Server.MapPath(SignaturePath)))
                {
                    string Destination = Server.MapPath(@"\\Photo\\DebtorSignature\\" + CM00100.DebtorID.Trim() + "." + CM00100.SignatureExtension);
                    if (System.IO.File.Exists(Destination))
                    {
                        System.IO.File.Delete(Destination);
                    }
                    System.IO.File.Move(Server.MapPath(SignaturePath), Destination);
                }
            }
            CM00100Services service = new CM00100Services(KaizenUser);
            return Json(service.AddCM00100(CM00100), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveDataJointAccount(string CIFNumber,string DebtorID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00100Services service = new CM00100Services(KaizenUser);
            return Json(service.AddCM10201(CIFNumber, DebtorID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteJointAccount(string CIFNumber, string DebtorID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00100Services service = new CM00100Services(KaizenUser);
            return Json(service.DeleteCM10201(CIFNumber, DebtorID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetJointAccount(string CIFNumber, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00100Services serv = new CM00100Services(KaizenUser);
            DataCollection<CM00104> L = new DataCollection<CM00104>();
            return Json(serv.GetAllCM10201ByCIFNumber(CIFNumber), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteData(CM00100 CM00100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00100Services service = new CM00100Services(KaizenUser);
            return Json(service.Delete(CM00100.DebtorID), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveImageTemp(IEnumerable<HttpPostedFileBase> attachments)
        {
            var fileName = "";
            foreach (var file in attachments)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/Photo/DebtorPhotoTemp"), fileName);
                file.SaveAs(destinationPath);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveImagetemp(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Photo/DebtorPhotoTemp"), fileName);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }

        public ActionResult SaveImageSignatureTemp(IEnumerable<HttpPostedFileBase> Co_Debtor_SignatureAttachment)
        {
            var fileName = "";
            foreach (var file in Co_Debtor_SignatureAttachment)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/Photo/DebtorSignatureTemp"), fileName);
                file.SaveAs(destinationPath);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveImageSignaturetemp(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Photo/DebtorSignatureTemp"), fileName);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }

        public ActionResult SaveUploadFileTemp(IEnumerable<HttpPostedFileBase> DebtorUploadFileAttachemnt)
        {
            var fileName = "";
            List<Kaizen.Configuration.ExcelColumns> lstExcelColumns = null;
            foreach (var file in DebtorUploadFileAttachemnt)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/Files"), fileName);
                file.SaveAs(destinationPath);
                Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(System.Web.HttpContext.Current.Request["KaizenPublicKey"]);
                CompanyacessServices srv = new CompanyacessServices(KaizenUser);
                lstExcelColumns = srv.ReadExcelColumnsWithoutInsert(destinationPath, fileName);
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

        #region Debtor Contact
        public ActionResult DebtorContact(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen.BusinessLogic.Admin.Sys00006Services srv = new Kaizen.BusinessLogic.Admin.Sys00006Services(KaizenUser);
            ViewData["ContactTypes"] = srv.GetAll(2);
            return PartialView();
        }
        private DataSourceResult DebtorContactList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00106> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00106
               {
                   ContactTypeID = o.ContactTypeID,
                   ContactPerson = o.ContactPerson,
                   PersonPosition = o.PersonPosition,
                   PersonEmailAdd = o.PersonEmailAdd,
                   OtherInfo = o.OtherInfo,
                   DebtorID = o.DebtorID,
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
                    Data = new List<CM00106>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetContactListGridWithDebtor([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string DebtorID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00106Services serv = new CM00106Services(KaizenUser);
            DataCollection<CM00106> L = new DataCollection<CM00106>();
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

            if (!string.IsNullOrEmpty(DebtorID))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, DebtorID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = DebtorContactList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveDebtorContact(CM00106 CM00106, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00106Services service = new CM00106Services(KaizenUser);
            return Json(service.AddCM00106(CM00106), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateDebtorContact(CM00106 CM00106, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00106Services service = new CM00106Services(KaizenUser);
            return Json(service.Update(CM00106), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteDebtorContact(CM00106 CM00106, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00106Services service = new CM00106Services(KaizenUser);
            return Json(service.Delete(CM00106), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDebtorContactSingle(string KaizenPublicKey, string DebtorID, int ContactTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00106Services service = new CM00106Services(KaizenUser);
            CM00106 Contact = service.GetSingle(DebtorID, ContactTypeID);
            return Json(Contact, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Debtor Address
        public ActionResult DebtorAddress(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00009Services srv = new CM00009Services(KaizenUser);
            ViewData["AddressTypes"] = srv.GetAll();
            return PartialView();
        }
        public ActionResult DebtorAddressPopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
       

        private DataSourceResult DebtorAddressList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00104> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00104
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
                   Phone01 = o.Phone01,
                   Phone02 = o.Phone02,
                   Phone03 = o.Phone03,
                   Phone04 = o.Phone04,
                   POBox = o.POBox,
                   WebSite = o.WebSite,
                   DebtorID = o.DebtorID,
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
                    Data = new List<CM00104>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetAddressListGridWithDebtor([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string DebtorID, string Searchcritaria, string FieldID = "-1", int FltrOperator = 8)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00104Services serv = new CM00104Services(KaizenUser);
            DataCollection<CM00104> L = new DataCollection<CM00104>();
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

            if (!string.IsNullOrEmpty(DebtorID))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, DebtorID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = DebtorAddressList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetListGridWithAddressCode([DataSourceRequest]DataSourceRequest request, string AddressCode, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00104Services serv = new CM00104Services(KaizenUser);
            DataCollection<CM00104> L = new DataCollection<CM00104>();
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

            L = serv.GetAllViewBYSQLQueryWithAddressCode(SQLQuery, AddressCode
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = DebtorAddressList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //Call from Main Case CMS_CaseController JS
        public ActionResult SaveDebtorAddress(CM00104 oCM00104, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00104Services service = new CM00104Services(KaizenUser);
            return Json(service.AddCM00104(oCM00104), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveDebtorAddresses(IList<CM00104> CM00104, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00104Services service = new CM00104Services(KaizenUser);
            return Json(service.AddCM00104(CM00104), JsonRequestBehavior.AllowGet);
        }
        //Call from Main Case CMS_CaseController JS
        public ActionResult UpdateDebtorAddress(CM00104 oCM00104, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00104Services service = new CM00104Services(KaizenUser);
            return Json(service.Update(oCM00104), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteDebtorAddress(CM00104 CM00104, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00104Services service = new CM00104Services(KaizenUser);
            return Json(service.Delete(CM00104), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDebtorAddressSingle(string KaizenPublicKey, string DebtorID, string AddressCode)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00104Services srv = new CM00104Services(KaizenUser);
            CM00104 Address = srv.GetSingle(AddressCode, DebtorID);
            return Json(Address, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Debtor Document
        public ActionResult DebtorDocument(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen.BusinessLogic.Admin.Sys00007Services srv = new Kaizen.BusinessLogic.Admin.Sys00007Services(KaizenUser);
            ViewData["DocumentTypes"] = srv.GetAll(2);
            return PartialView();
        }
        private DataSourceResult DebtorDocumentList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00101> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00101
               {
                   DocumentID = o.DocumentID,
                   DebtorID = o.DebtorID,
                   DocumentDescription = o.DocumentDescription,
                   DocumentTypeID = o.DocumentTypeID,
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
                    Data = new List<CM00101>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetDocumentListGridWithDebtor([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string DebtorID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00101Services serv = new CM00101Services(KaizenUser);
            DataCollection<CM00101> L = new DataCollection<CM00101>();
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
                SortMember = "DocumentCode";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(DebtorID))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, DebtorID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = DebtorDocumentList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveDebtorDocument(CM00101 CM00101, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00101Services service = new CM00101Services(KaizenUser);
            if (!string.IsNullOrEmpty(CM00101.PhotoExtension))
            {
                string PhotoPath = Server.MapPath(@"\\Photo\\DebtorDocumentsTemp\\" + CM00101.PhotoExtension.Trim());
                FileInfo TempFile_info = new FileInfo(PhotoPath);
                CM00101.PhotoExtension = TempFile_info.Extension;
                if (System.IO.File.Exists(PhotoPath))
                {
                    string Destination = Server.MapPath(@"\\Photo\\DebtorDocuments\\" + CM00101.DocumentName + CM00101.PhotoExtension);
                    if (System.IO.File.Exists(Destination))
                    {
                        FileInfo file_info = new FileInfo(Destination);
                        string dirPath = file_info.DirectoryName;
                        string fileName = Path.GetFileNameWithoutExtension(file_info.Name);
                        string[] files = Directory.GetFiles(dirPath);
                        int count = files.Count(file => { return file.Contains(fileName); });
                        string newFileName = String.Format("{0} ({1})", fileName, count);
                        CM00101.DocumentName = newFileName;
                        Destination = Server.MapPath(@"\\Photo\\DebtorDocuments\\" + CM00101.DocumentName + CM00101.PhotoExtension);
                    }
                    System.IO.File.Move(PhotoPath, Destination);
                }
            }
            return Json(service.AddCM00101(CM00101), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateDebtorDocument(CM00101 CM00101, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00101Services service = new CM00101Services(KaizenUser);
            return Json(service.Update(CM00101), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteDebtorDocument(CM00101 CM00101, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00101Services service = new CM00101Services(KaizenUser);
            string ServerPath = Server.MapPath(@"\\Photo\\DebtorDocuments\\");
            return Json(service.Delete(CM00101, ServerPath), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveDebtorDocumentTemp(IEnumerable<HttpPostedFileBase> Co_DebtorDocumentattachments)
        {
            var fileName = "";
            foreach (var file in Co_DebtorDocumentattachments)
            {
                fileName = Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/Photo/DebtorDocumentsTemp"), fileName);
                file.SaveAs(destinationPath);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveDebtorDocumentTemp(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Photo/DebtorDocumentsTemp"), fileName);
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