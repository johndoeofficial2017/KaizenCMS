using Kaizen.BusinessLogic.Configuration;
using Kaizen.Configuration;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Kendo.Mvc.UI;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using System.Data;
using Newtonsoft.Json;
using System;
using System.Runtime.Remoting;
using System.Dynamic;

namespace UIServer.Controllers
{
    public class Sys_ViewController : Controller
    {
        // GET: Sys_View
        public ActionResult Index(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult Create(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ViewAccess(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ViewMangement(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ScreenMangement(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult DynamicReport(string KaizenPublicKey)
        {
            //KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            //if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<Kaizen00011> L, string CompanyID)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new Kaizen00011
               {
                   ViewID = o.ViewID,
                   ScreenID = o.ScreenID,
                   CompanyID = o.CompanyID,
                   ViewName = o.ViewName,
                   ViewDescription = o.ViewDescription,
                   IsDefault = o.IsDefault,
                   ViewWhereCondition = o.ViewWhereCondition,
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<Kaizen00011>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string Searchcritaria, string CompanyID,int? ModuleID)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00011Services invService = new Kaizen00011Services(KaizenUser);
            DataCollection<Kaizen00011> L = new DataCollection<Kaizen00011>();
            string filters = string.Empty;
            string SortMember = "ViewID";
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
            L = invService.GetAllViewByCompany(CompanyID,ModuleID, SQLQuery, Searchcritaria, request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L, KaizenUser.CompanyID.Trim());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult SaveData(Kaizen00011 Kaizen00011, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00011Services service = new Kaizen00011Services(KaizenUser);
            return Json(service.AddKaizen00011(Kaizen00011), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(Kaizen00011 Kaizen00011, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00011Services service = new Kaizen00011Services(KaizenUser);
            return Json(service.Update(Kaizen00011), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(Kaizen00011 Kaizen00011, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00011Services service = new Kaizen00011Services(KaizenUser);
            return Json(service.Delete(Kaizen00011), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00011Services service = new Kaizen00011Services(KaizenUser);
            Kaizen00011 L = service.GetSingle(ViewID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00011Services service = new Kaizen00011Services(KaizenUser);
            IList<Kaizen00011> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Getting Views by ScreenID 
        /// </summary>
        /// <param name="KaizenPublicKey"></param>
        /// <param name="ScreenID">ID of Screen</param>
        /// <remarks>Mahmoud Gamal - 03 April 2016</remarks>
        /// <returns>Views List</returns>
        /// wael >> Calling from GL_TRX_GLEntryController
        public ActionResult GetViewsByScreen(int ScreenID ,string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00011Services oKaizen00011 = new Kaizen00011Services(KaizenUser);
            List<Kaizen00011> Views = oKaizen00011.GetMyGridViews((Screen)ScreenID).ToList();
            List<Kaizen00011> ViewResult = new List<Kaizen00011>();
            foreach (Kaizen00011 o in Views)
            {
                Kaizen00011 newobj = new Kaizen00011()
                {
                    ViewID = o.ViewID,
                    ViewName = o.ViewName,
                    ControllerSource = o.ControllerSource,
                    //CompanyID = o.CompanyID,
                    ViewSortCondition = o.ViewSortCondition,
                    IsDefault = o.IsDefault,
                    ViewWhereCondition = o.ViewWhereCondition,
                    //ViewStateData = o.ViewStateData
                };
                ViewResult.Add(newobj);
            }
            return Json(ViewResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetColumnsByView(int ViewID, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00026Services oKaizen00011ColumnServices = new Kaizen00026Services(KaizenUser);
            List<Kaizen00026> result = oKaizen00011ColumnServices.GetFieldsByView(ViewID).OrderBy(x => x.ColumnOrder).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Getting Views by ScreenID and CompanyID
        /// Used in : SysUser 
        /// </summary>
        /// <param name="KaizenPublicKey"></param>
        /// <param name="ScreenID">ID of Screen</param>
        /// <param name="CompanyID">ID of Company</param>
        /// <remarks>Mahmoud Gamal - 03 April 2016</remarks>
        /// <returns>Views List</returns>
        public ActionResult GetScreenViews(string KaizenPublicKey, int ScreenID, string CompanyID)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00011Services srv = new Kaizen00011Services(KaizenUser);
            IList<Kaizen00011> L = srv.GetScreenViews(ScreenID, CompanyID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadGrid(int ViewID)
        {
            Kaizen00011Services oKaizen00011ColumnServices = new Kaizen00011Services(null);
            Kaizen00011 o = oKaizen00011ColumnServices.GetOne(ViewID);
            string SqlData = o.ViewStateData;//.Replace("Kaizen", "'");
            if (string.IsNullOrEmpty(SqlData))
            {
                KaizenJson KaizenJsonresult = new KaizenJson();
                KaizenJsonresult.Status = false;
                KaizenJsonresult.Massage = "Fail";
                return Json(KaizenJsonresult, JsonRequestBehavior.AllowGet);
            }
            return Json(SqlData, JsonRequestBehavior.AllowGet);
            //return Json(Session["data"], JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateScreenField(Kaizen00013 Kaizen00013, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00013Services serv = new Kaizen00013Services(KaizenUser);
            return Json(serv.Update(Kaizen00013), JsonRequestBehavior.AllowGet);
        }


        #region View Conditions
        public ActionResult ViewCondition(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetScreenConditionFields(string KaizenPublicKey, int ScreenID)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00013Services _Kaizen00011Services = new Kaizen00013Services(KaizenUser);
            List<Kaizen00013> o = _Kaizen00011Services.GetScreenFields(ScreenID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DynamicFillDropDownList(string KaizenPublicKey ,string kaizenTableName)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            DataSourceResult result = new DataSourceResult();
            switch (kaizenTableName)
            {
                case "CM00029":
                    Kaizen.BusinessLogic.CMS.CM00029Services srv = new Kaizen.BusinessLogic.CMS.CM00029Services(KaizenUser);
                    return Json(srv.GetAll(), JsonRequestBehavior.AllowGet);
                    break;
                case "CM00015":
                    Kaizen.BusinessLogic.CMS.CM00015Services srv2 = new Kaizen.BusinessLogic.CMS.CM00015Services(KaizenUser);
                    return Json(srv2.GetAll(), JsonRequestBehavior.AllowGet);
                    break;
                case "Sys00013":
                    Kaizen.BusinessLogic.Admin.Sys00013Services srv3 = new Kaizen.BusinessLogic.Admin.Sys00013Services(KaizenUser);
                    return Json(srv3.GetAll(), JsonRequestBehavior.AllowGet);
                    break;
                case "Sys00014":
                    Kaizen.BusinessLogic.Admin.Sys00014Services srv4 = new Kaizen.BusinessLogic.Admin.Sys00014Services(KaizenUser);
                    return Json(srv4.GetAll(), JsonRequestBehavior.AllowGet);
                    break;
                case "CM00700":
                    Kaizen.BusinessLogic.CMS.CM00700Services srv5 = new Kaizen.BusinessLogic.CMS.CM00700Services(KaizenUser);
                    return Json(srv5.GetAll(), JsonRequestBehavior.AllowGet);
                    break;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private DataSourceResult ViewConditionList([DataSourceRequest]DataSourceRequest request, DataCollection<Kaizen00014> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new Kaizen00014
               {
                   ConditionID = o.ConditionID,
                   ViewID = o.ViewID,
                   FieldName = o.FieldName,
                   FieldCondition = o.FieldCondition,
                   FieldValue = o.FieldValue,
                   FieldOperatorAnd = o.FieldOperatorAnd
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<Kaizen00014>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetConditionListGridWithView([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, int ViewID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00014Services serv = new Kaizen00014Services(KaizenUser);
            DataCollection<Kaizen00014> L = new DataCollection<Kaizen00014>();
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
                SortMember = "ConditionID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(ViewID.ToString()))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, ViewID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = ViewConditionList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveViewCondition(Kaizen00014 Kaizen00014, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00014Services serv = new Kaizen00014Services(KaizenUser);
            return Json(serv.AddKaizen00014(Kaizen00014), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateViewCondition(Kaizen00014 Kaizen00014, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00014Services serv = new Kaizen00014Services(KaizenUser);
            return Json(serv.Update(Kaizen00014), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteViewCondition(Kaizen00014 Kaizen00014, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen00014Services serv = new Kaizen00014Services(KaizenUser);
            return Json(serv.Delete(Kaizen00014), JsonRequestBehavior.AllowGet);
        }

        private DataSourceResult DynamicList([DataSourceRequest]DataSourceRequest request, DataCollection<dynamic> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request);
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<dynamic>(),
                    Total = 0
                };
            }
            return result;
        }

        public ActionResult LookUpDataGrid([DataSourceRequest]DataSourceRequest request, string kaizenTableName, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            DataSourceResult result = new DataSourceResult();
            switch (kaizenTableName)
            {
                case "CM00203":
                    Kaizen.BusinessLogic.CMS.CM00203Services srv = new Kaizen.BusinessLogic.CMS.CM00203Services(KaizenUser);
                    Kaizen.CMS.DataCollection<Kaizen.CMS.CM00203> CM00203 = srv.GetAllBYSQLQuery("", request.PageSize, request.Page, "CaseRef", 1);
                    result = CM00203.Items.ToDataSourceResult(request,
                      o => new Ttt
                      {
                          FieldID = o.CaseRef,
                          FieldName = o.CaseAccountNo,
                          FieldDescription = o.CIFName,
                      }
                      );
                    break;
                case "CM00200":
                    Kaizen.BusinessLogic.CMS.CM00200Services srv2 = new Kaizen.BusinessLogic.CMS.CM00200Services(KaizenUser);
                    Kaizen.CMS.DataCollection<Kaizen.CMS.CM00200> CM00200 = srv2.GetAllBYSQLQuery("", request.PageSize, request.Page, "ContractID", 1);
                    result = CM00200.Items.ToDataSourceResult(request,
                      o => new Ttt
                      {
                          FieldID = o.ContractID,
                          FieldName = o.ContractName,
                          FieldDescription = o.ClientID,
                      }
                      );
                    break;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }

    public class Ttt
    {
        public string FieldID { get; set; }
        public string FieldName { get; set; }
        public string FieldDescription { get; set; }

    }
}