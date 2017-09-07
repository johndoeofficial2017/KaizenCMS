using Kaizen.BusinessLogic.Configuration;
using Kaizen.Configuration;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace UIServer.Controllers
{
    public class Sys_CompanyController : Controller
    {
        [Authorize]
        public ActionResult Index(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        public ActionResult Create(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<Company> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new Company
               {
                   CompanyID = o.CompanyID,
                   CompanyName = o.CompanyName,
                   SegmentMark = o.SegmentMark,
                   CurrentDate = o.CurrentDate,
                   CurrencyCode = o.CurrencyCode,
                   DecimalDigit = o.DecimalDigit,
                   ExchangeTableID = o.ExchangeTableID,
                   IsMultiply = o.IsMultiply,
                   ExchangeRateID = o.ExchangeRateID,
                   ExchangeRate = o.ExchangeRate
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<Company>(),
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
            CompanyacessServices invService = new CompanyacessServices(KaizenUser);
            DataCollection<Company> L = new DataCollection<Company>();
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
                SortMember = "CompanyID";
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
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CompanyacessServices serv = new CompanyacessServices(KaizenUser);
            DataCollection<Company> L = new DataCollection<Company>();
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
                SortMember = "CompanyID";
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

        public ActionResult SaveData(Company Company, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanyacessServices service = new CompanyacessServices(KaizenUser);
            Company.CurrentDate = DateTime.Now;
            return Json(service.AddCompany(Company), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(Company Company, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanyacessServices service = new CompanyacessServices(KaizenUser);
            return Json(service.Update(Company), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(Company Company, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanyacessServices service = new CompanyacessServices(KaizenUser);
            return Json(service.Delete(Company), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string CompanyID)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanyacessServices service = new CompanyacessServices(KaizenUser);
            return Json(service.GetSingle(CompanyID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            //CompanyacessServices service = new CompanyacessServices(null);
            IList<Company> L = Kaizen.BusinessLogic.Master.InstalledCompany;
            List<Company> result = new List<Company>();
            foreach (Company o in L)
            {
                result.Add(new Company()
                {
                    CompanyID = o.CompanyID,
                    CompanyName = o.CompanyName,
                    CurrencyCode = o.CurrencyCode,
                    ExchangeTableID = o.ExchangeTableID,
                    SegmentMark = o.SegmentMark
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUserCompanies(string KaizenPublicKey, string UserName)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CompanyacessServices service = new CompanyacessServices(KaizenUser);
            return Json(service.GetMyCompanies(UserName), JsonRequestBehavior.AllowGet);
        }

        #region Company Segment
        public ActionResult CompanySegment(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        private DataSourceResult CompanySegmentList([DataSourceRequest]DataSourceRequest request, DataCollection<CompanySegment> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CompanySegment
               {
                   SegmentID = o.SegmentID,
                   SegmentName = o.SegmentName,
                   SegmentLength = o.SegmentLength,
                   CompanyID = o.CompanyID,
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CompanySegment>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetSegmentListGridWithCompany([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string CompanyID)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanySegmentServices serv = new CompanySegmentServices(KaizenUser);
            DataCollection<CompanySegment> L = new DataCollection<CompanySegment>();
            string filters = string.Empty;
            string SortMember;
            bool IsAscending;
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
                else
                {
                    SortMember = "SegmentID";
                    IsAscending = true;
                }
            }
            else
            {
                SortMember = "SegmentID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            SQLQuery += "CompanyID='" + CompanyID + "'";

            L = serv.GetAllViewBYSQLQuery(SQLQuery, request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CompanySegmentList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCompanySegment(CompanySegment CompanySegment, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);

            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanySegmentServices service = new CompanySegmentServices(KaizenUser);
            return Json(service.AddCompanySegment(CompanySegment), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCompanySegment(CompanySegment CompanySegment, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanySegmentServices service = new CompanySegmentServices(KaizenUser);
            return Json(service.Update(CompanySegment), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCompanySegment(CompanySegment CompanySegment, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanySegmentServices service = new CompanySegmentServices(KaizenUser);
            return Json(service.Delete(CompanySegment), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Company Module
        public ActionResult CompanyModule(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult GetModuleAccessByCompany(string KaizenPublicKey, string CompanyID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00101Services service = new Kaizen00101Services(KaizenUser);
            IList<Kaizen00101> L = service.GetAllByCompany(CompanyID);
            List<Kaizen000> result = new List<Kaizen000>();
            foreach (var item in L)
            {
                if (item.ModuleID == 1)
                {
                    result.Add(new Kaizen000()
                    {
                        ModuleID = 1,
                        ModuleName = "Financial"
                    });
                }
                else if (item.ModuleID == 2)
                {
                    result.Add(new Kaizen000()
                    {
                        ModuleID = 2,
                        ModuleName = "FixedAssets"
                    });
                }
                else if (item.ModuleID == 3)
                {
                    result.Add(new Kaizen000()
                    {
                        ModuleID = 3,
                        ModuleName = "Purchase"
                    });
                }
                else if (item.ModuleID == 4)
                {
                    result.Add(new Kaizen000()
                    {
                        ModuleID = 4,
                        ModuleName = "Sales"
                    });
                }
                else if (item.ModuleID == 5)
                {
                    result.Add(new Kaizen000()
                    {
                        ModuleID = 5,
                        ModuleName = "Inventory"
                    });
                }
                else if (item.ModuleID == 6)
                {
                    result.Add(new Kaizen000()
                    {
                        ModuleID = 6,
                        ModuleName = "Project"
                    });
                }
                else if (item.ModuleID == 7)
                {
                    result.Add(new Kaizen000()
                    {
                        ModuleID = 7,
                        ModuleName = "HumanResources"
                    });
                }
                else if (item.ModuleID == 8)
                {
                    result.Add(new Kaizen000()
                    {
                        ModuleID = 8,
                        ModuleName = "CRM"
                    });
                }
                else if (item.ModuleID == 9)
                {
                    result.Add(new Kaizen000()
                    {
                        ModuleID = 9,
                        ModuleName = "CollectionManagementSystem"
                    });
                }
                else if (item.ModuleID == 10)
                {
                    result.Add(new Kaizen000()
                    {
                        ModuleID = 10,
                        ModuleName = "Administrator"
                    });
                }
                else if (item.ModuleID == 11)
                {
                    result.Add(new Kaizen000()
                    {
                        ModuleID = 11,
                        ModuleName = "Medical"
                    });
                }
                else if (item.ModuleID == 12)
                {
                    result.Add(new Kaizen000()
                    {
                        ModuleID = 12,
                        ModuleName = "PBX"
                    });
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCompanyModule(Kaizen00101 Kaizen00101, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00101Services service = new Kaizen00101Services(KaizenUser);
            return Json(service.AddKaizen00101(Kaizen00101), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCompanyModule(Kaizen00101 Kaizen00101, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00101Services service = new Kaizen00101Services(KaizenUser);
            return Json(service.Delete(Kaizen00101), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Company SMS Account
        public ActionResult CompanySMSAccount(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetSMSAccountByCompany(string KaizenPublicKey, string CompanyID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CompanyAccessServices service = new CompanyAccessServices(KaizenUser);
            IList<Kaizen00040> o = service.GetSMSAccountByCompany(CompanyID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveCompanySMSAccount(Kaizen00040 Kaizen00040, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CompanyAccessServices service = new CompanyAccessServices(KaizenUser);
            return Json(service.AddKaizen00040(Kaizen00040), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateSMSAccount(Kaizen00040 Kaizen00040, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CompanyAccessServices service = new CompanyAccessServices(KaizenUser);
            return Json(service.UpdateKaizen00040(Kaizen00040), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSMSAccount(Kaizen00040 Kaizen00040, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CompanyAccessServices service = new CompanyAccessServices(KaizenUser);
            return Json(service.DeleteKaizen00040(Kaizen00040), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Company Role

        #endregion

    }
}