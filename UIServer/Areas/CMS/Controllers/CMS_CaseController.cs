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
using Kaizen.Configuration;
using UIServer.Infrastructure.Token_Setup;
using System.Dynamic;

namespace UIServer.Areas.CMS.Controllers
{
    public class CMS_CaseController : Controller
    {
        // GET: CMS_Case
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult CaseGraph(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ManualPayment(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult CreateNew(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult CreateReadOnly(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult TasksIndex(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00004Services CM00004_serv = new CM00004Services(KaizenUser);
            CM00006Services CM00006_serv = new CM00006Services(KaizenUser);
            ViewData["TaskTypes"] = CM00004_serv.GetAll();
            ViewData["TaskPriorities"] = CM00006_serv.GetAll();

            return PartialView();
        }
        public ActionResult FillTaskTypesList(string KaizenPublicKey, string field)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00004Services CM00004_serv = new CM00004Services(KaizenUser);
            return Json(CM00004_serv.GetAll(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillTaskPrioritiesList(string KaizenPublicKey, string field)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00006Services CM00006_serv = new CM00006Services(KaizenUser);
            return Json(CM00006_serv.GetAll(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FiltersToString(IList<IFilterDescriptor> DataFilters)
        {
            var json = Json(Help.ApplyFilter(DataFilters[0]), JsonRequestBehavior.AllowGet);
            return json;
        }
        public ActionResult GetTaskDataListGrid([DataSourceRequest]DataSourceRequest request
          , DateTime FromReminder
          , DateTime ToReminder, string AgentID, bool IsOverDue, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00213Services invService = new CM00213Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00213> L = new Kaizen.CMS.DataCollection<CM00213>();
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
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);
            L = invService.GetListWithPagingByTable(SQLQuery, request.PageSize,
                request.Page, SortMember, IsAscending);
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00213
               {
                   TaskID = o.TaskID,
                   CaseRef = o.CaseRef,
                   TaskTypeID = o.TaskTypeID,
                   PriorityID = o.PriorityID,
                   TaskTitle = o.TaskTitle,
                   TaskDescription = o.TaskDescription,
                   TaskStartDate = o.TaskStartDate,
                   TaskEndDate = o.TaskEndDate,
                   AgentID = o.AgentID,
                   AssignDate = o.AssignDate,
                   TaskProgress = o.TaskProgress
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
            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult TaskView(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            return View();
        }
        public ActionResult CaseActionHistory(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00027Services CM00027_serv = new CM00027Services(KaizenUser);
            CM00700Services CM00700_serv = new CM00700Services(KaizenUser);
            ViewData["CaseStatusSources"] = CM00027_serv.GetAll();
            ViewData["CaseStatuses"] = CM00700_serv.GetAll();
            return PartialView();
        }

        public JsonResult GetReminderDataListGrid([DataSourceRequest]DataSourceRequest request
            , DateTime FromReminder, DateTime ToReminder, string AgentID, bool IsOverDue
            ,int CaseReminderType,int ViewID,string Searchcritaria, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);
            CM00203Services serv = new CM00203Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00203> L = new Kaizen.CMS.DataCollection<CM00203>();
            string filters = string.Empty;
            string SortMember = "CaseRef";
            if (request.Sorts != null)
            {
                SortDescriptor sortobject = request.Sorts.FirstOrDefault();
                if (sortobject != null)
                {
                    SortMember = sortobject.Member;
                    if (sortobject.SortDirection == System.ComponentModel.ListSortDirection.Ascending)
                        SortMember += " asc";
                    else
                        SortMember += " desc";
                }
                else
                    SortMember += " asc";
            }
            else
                SortMember += " asc";
            string SQLQuery = string.Empty;
            if (request.Filters != null)
            {
                if (request.Filters.Count > 0)
                    SQLQuery = Help.ApplyFilter(request.Filters[0]);
            }
            L = serv.GetDataReminder(SQLQuery, Searchcritaria,FromReminder, ToReminder,
                AgentID, IsOverDue, CaseReminderType, ViewID, request.PageSize, request.Page, SortMember);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult MainIndex(string KaizenPublicKey)
        //{
        //    Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
        //    if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
        //    return PartialView();
        //}
        public ActionResult Create(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            //CM00029Services CM00029_serv = new CM00029Services(KaizenUser);
            //ViewData["CaseTransactionTypes"] = CM00029_serv.GetAll();
            //CM00009Services srv = new CM00009Services(KaizenUser);
            //ViewData["AddressTypes"] = srv.GetAll();
            //Kaizen.BusinessLogic.Admin.Sys00007Services service = new Kaizen.BusinessLogic.Admin.Sys00007Services(KaizenUser);
            //ViewData["DocumentTypes"] = service.GetAll(6);
            return PartialView();
        }
        public ActionResult Reminder(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ReminderMainGrid(string KaizenPublicKey, string ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services oKaizen00011ColumnServices = new CM00029Services(KaizenUser);
            List<CM00075> ColumnList = oKaizen00011ColumnServices.GetAllFieldsByView(int.Parse(ViewID)).ToList();
            List<CM00075> temp = ColumnList.FindAll(ss => !ss.Islocked);
            if (temp.Count > 0)
                ViewBag.KaizenGridViewColumn = ColumnList.OrderBy(x => x.FieldOrder);
            else
                return PartialView("ErrorWindow");
            return PartialView();
        }
        public ActionResult MainGrid(string KaizenPublicKey, string ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services oKaizen00011ColumnServices = new CM00029Services(KaizenUser);
            List<CM00075> ColumnList = oKaizen00011ColumnServices.GetAllFieldsByView(int.Parse(ViewID)).ToList();
            List<CM00075> temp = ColumnList.FindAll(ss => !ss.Islocked);
            if (temp.Count > 0)
                ViewBag.KaizenGridViewColumn = ColumnList.OrderBy(x => x.FieldOrder);
            else
                return PartialView("ErrorWindow");
            return PartialView();
        }
        public ActionResult AssignedCases(string KaizenPublicKey, string ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00026Services oKaizen00011ColumnServices = new Kaizen00026Services(KaizenUser);
            List<Kaizen.Configuration.Kaizen00026> oKaizen00026ColumnList = oKaizen00011ColumnServices.GetFieldsByView(int.Parse(ViewID)).ToList();
            List<Kaizen.Configuration.Kaizen00026> temp = oKaizen00026ColumnList.FindAll(ss => !ss.locked);
            if (temp.Count > 0)
                ViewBag.KaizenGridViewColumn = oKaizen00026ColumnList.OrderBy(x => x.ColumnOrder);
            else
                return PartialView("ErrorWindow");
            return PartialView();
        }
        public ActionResult UnAssignedCases(string KaizenPublicKey, string ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00026Services oKaizen00011ColumnServices = new Kaizen00026Services(KaizenUser);
            List<Kaizen.Configuration.Kaizen00026> oKaizen00026ColumnList = oKaizen00011ColumnServices.GetFieldsByView(int.Parse(ViewID)).ToList();
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
        public ActionResult DebtorMainGrid(string KaizenPublicKey, int ViewID)
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
        public ActionResult DebtorIndex(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult PopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult PopUpByDebtor(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult CaseAction(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
           
            return PartialView();
        }
        public ActionResult CaseActionPlan(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult CaseTask(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00004Services CM00004_serv = new CM00004Services(KaizenUser);
            CM00006Services CM00006_serv = new CM00006Services(KaizenUser);
            ViewData["CaseTaskTypes"] = CM00004_serv.GetAll();
            ViewData["CaseTaskPriorities"] = CM00006_serv.GetAll();
            return PartialView();
        }
        public ActionResult CaseAssign(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult CaseAssignHistory(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult CaseBulkActions(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult PivotGrid(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
      
        public ActionResult UploadView(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult EmailTemplate(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        
        public ActionResult ClientCases(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult AgentCases(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult CaseHistory(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services CM00029_serv = new CM00029Services(KaizenUser);
            ViewData["CaseTransactionTypes"] = CM00029_serv.GetAll();
            CM00009Services srv = new CM00009Services(KaizenUser);
            ViewData["AddressTypes"] = srv.GetAll();
            Kaizen.BusinessLogic.Admin.Sys00007Services service = new Kaizen.BusinessLogic.Admin.Sys00007Services(KaizenUser);
            ViewData["DocumentTypes"] = service.GetAll(6);
            return PartialView();
        }
        public ActionResult CaseHistoryPopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult CaseDeletedPopUp(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult CaseDocuments(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen.BusinessLogic.Admin.Sys00007Services srv = new Kaizen.BusinessLogic.Admin.Sys00007Services(KaizenUser);
            ViewData["DocumentTypes"] = srv.GetAll(6);
            return PartialView();
        }
        public ActionResult CaseDemandLetter(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
       
        public ActionResult ActionManamgment(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ActionManamgmentSummery(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ActionManamgmentSummeryAdvance(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult CasePayment(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult ActionManamgmentSummeryAdvanceReport(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult ActionManamgmentSummeryData([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey,
           List<int> Statuses, List<string> Agents, List<string> Clients)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");

            CM10701Services serv = new CM10701Services(KaizenUser);
            //DataSourceResult result = serv.GetAllCM10701View01().ToDataSourceResult(request);
            var json = Json(null, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult ActionManamgmentMainGrid(int ViewID,string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services srv = new CM00700Services(KaizenUser);
            DataTable ColumnList = srv.GetStatusFieldsDT(ViewID);
            return PartialView(ColumnList);
        }
        public ActionResult GetActionManamgmentGrid([DataSourceRequest]DataSourceRequest request
           , int CaseStatusID, 
           string KaizenPublicKey, string Searchcritaria)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00700Services invService = new CM00700Services(KaizenUser);
            string filters = string.Empty;
            string SortMember = "StatusHistoryID";
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
            DataTable L  = invService.GetStatusFieldsData(CaseStatusID);
            var json = Json(L.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
            //return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetViewsByCaseStatusID(int CaseStatusID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00700Services invService = new CM00700Services(KaizenUser);


            return Json(invService.GetViewsByCaseStatusID(CaseStatusID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SendEmailData(string TRXTypeID , int ViewID, Kaizen00020 userEmailSetting, List<string> emailToList, string subject,string message, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services oKaizen00011ColumnServices = new CM00029Services(KaizenUser);
            List<CM00075> ColumnList = oKaizen00011ColumnServices.GetAllFieldsByView(ViewID).ToList();
            Kaizen.CMS.DataCollection<CM00203> L = new Kaizen.CMS.DataCollection<CM00203>();
            CM00203Services invService = new CM00203Services(KaizenUser);
            L = invService.GetAllBYSQLQuery(" TRXTypeID='" + TRXTypeID.Trim() + "'", "",1000,0, "CaseRef", true, -1);

            string htmlBody = GetEmailBodyForCaseType(ColumnList,L.Items);

            if (htmlBody == "")
                return Json("Unable to generate email content", JsonRequestBehavior.AllowGet);

            List<KaizenEmailAddress> toAddressList=new List<KaizenEmailAddress>();
            foreach (string emailTo in emailToList)
                toAddressList.Add(new KaizenEmailAddress() {Text = emailTo });

            htmlBody = htmlBody.Replace("%message%",message);

            return Json(KaizenComposeEmail.SendMail(subject ,
                htmlBody, userEmailSetting,
                toAddressList, null, null,
                null, SendOptions.Single, KaizenPublicKey), JsonRequestBehavior.AllowGet);
        }

        public string GetEmailBodyForCaseType(List<CM00075> columnList, List<CM00203> CM00203List)
        {
            string htmlBody = "";

            try
            {
                dynamic modelToPass = new ExpandoObject();
                modelToPass.CM00075List = columnList;
                modelToPass.CM00203List = CM00203List;

                htmlBody = RazorViewToString.RenderRazorViewToString(this, "~/Areas/CMS/Views/CMS_Case/EmailTemplateCaseType.cshtml", modelToPass);
            }
            catch (Exception ex)
            {
                htmlBody = "";
            }
            return htmlBody;
        }

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, Kaizen.CMS.DataCollection<CM00203> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00203
               {
                   CaseRef = o.CaseRef,
                   TRXTypeID = o.TRXTypeID,
                   ContractID = o.ContractID,
                   //ContractName = o.ContractName,
                   ClientID = o.ClientID,
                   ClientName = o.ClientName,
                   NationaltyClient = o.NationaltyClient,
                   NationltyCreditor = o.NationltyCreditor,
                   NationltyPartner = o.NationltyPartner,
                   NationaltyIDCIF = o.NationaltyIDCIF,
                   NationalityName = o.NationalityName,
                   CreditorID = o.CreditorID,
                   CreditorName = o.CreditorName,
                   PartnerID = o.PartnerID,
                   PartnerName = o.PartnerName,
                   PartnrAssinDate = o.PartnrAssinDate,
                   LegalID = o.LegalID,
                   LegalName = o.LegalName,
                   LegalAssignDate = o.LegalAssignDate,
                   BatchID = o.BatchID,
                   CurrencyCode = o.CurrencyCode,
                   DecimalDigit = o.DecimalDigit,
                   IsMultiply = o.IsMultiply,
                   ExchangeRate = o.ExchangeRate,
                   CIFNumber = o.CIFNumber,
                   CPRCRNo = o.CPRCRNo,
                   CIFName = o.CIFName,
                   PriorityID = o.PriorityID,
                   PriorityName = o.PriorityName,
                   NPA = o.NPA,
                   TaskComplated = o.TaskComplated,
                   ContactTypeID = o.ContactTypeID,
                   IsJoint = o.IsJoint,
                   CaseStatusID = o.CaseStatusID,
                   CaseStatusChild = o.CaseStatusChild,
                   CaseStatusname = o.CaseStatusname,
                   CaseStatusChildName = o.CaseStatusChildName,
                   CaseStatusComment = o.CaseStatusComment,
                   ReminderDateTime = o.ReminderDateTime,
                   PTPAMT = o.PTPAMT,
                   PTPCount = o.PTPCount,
                   PTPBroken = o.PTPBroken,
                   PTPkept = o.PTPkept,
                   LastCaseStatusID = o.LastCaseStatusID,
                   LastCasStatusChldID = o.LastCasStatusChldID,
                   LastCasStatusname = o.LastCasStatusname,
                   LastCasStatusChldNam = o.LastCasStatusChldNam,
                   LastCasStatusComment = o.LastCasStatusComment,
                   MidCasStatusID = o.MidCasStatusID,
                   MidCasStatusChld = o.MidCasStatusChld,
                   MidCasStatusnam = o.MidCasStatusnam,
                   MidCasStatusChldName = o.MidCasStatusChldName,
                   MidCasStatusComment = o.MidCasStatusComment,
                   CYCLEDAY = o.CYCLEDAY,
                   BucketPrev = o.BucketPrev,
                   BucketPrevName = o.BucketPrevName,
                   BucketID = o.BucketID,
                   BucketName = o.BucketName,
                   Lookup01Name =o.Lookup01Name,
                   Lookup02 = o.Lookup02,
                   Lookup02Name = o.Lookup02Name,
                   ProductID = o.ProductID,
                   ProductName = o.ProductName,
                   TxtField01 = o.TxtField01,
                   TxtField02 = o.TxtField02,
                   TxtField03 = o.TxtField03,
                   TxtField04 = o.TxtField04,
                   TxtField05 = o.TxtField05,
                   TxtField06 = o.TxtField06,
                   TxtField07 = o.TxtField07,
                   CheckBoxField02 = o.CheckBoxField02,
                   CheckBoxField01 = o.CheckBoxField01,
                   AMT01 = o.AMT01,
                   AMT02 = o.AMT02,
                   AMT03 = o.AMT03,
                   AMT04 = o.AMT04,
                   AMT05 = o.AMT05,
                   AMT06 = o.AMT06,
                   AMT07 = o.AMT07,
                   AMT08 = o.AMT08,
                   Date01 = o.Date01,
                   Date02 = o.Date02,
                   Date03 = o.Date03,
                   Date04 = o.Date04,
                   PrincipleAmount = o.PrincipleAmount,
                   MaturityAmount = o.MaturityAmount,
                   OutstandingAMT = o.OutstandingAMT,
                   OutStandingToday = o.OutStandingToday,
                   InstallmentAmount = o.InstallmentAmount,
                   ClaimAmount = o.ClaimAmount,
                   FinanceCharge = o.FinanceCharge,
                   WriteOffAMT = o.WriteOffAMT,
                   CreditLimit = o.CreditLimit,
                   LastPaymentAMTUpload = o.LastPaymentAMTUpload,
                   TotalLifeCollactedAMT = o.TotalLifeCollactedAMT,
                   CaseAccountNo = o.CaseAccountNo,
                   InvoiceNumber = o.InvoiceNumber,
                   OverDueDays = o.OverDueDays,
                   FirstDisburementDate = o.FirstDisburementDate,
                   MATURITY_DATE = o.MATURITY_DATE,
                   TransactionDate = o.TransactionDate,
                   OverDueDate = o.OverDueDate,
                   LastPaymentDateUplod = o.LastPaymentDateUplod,
                   ClosingDate = o.ClosingDate,
                   BookingDate = o.BookingDate,
                   FirstLifeOverDueDate = o.FirstLifeOverDueDate,
                   Remarks = string.IsNullOrEmpty(o.Remarks) ? "" : o.Remarks.Length > 15 ? o.Remarks.Substring(0, 15) : o.Remarks,
                   AgentID = string.IsNullOrEmpty(o.AgentID) ? "" : o.AgentID.Trim(),
                   AssignDate = o.AssignDate,
                   LastPaymentDate = o.LastPaymentDate,
                   LastCallactedAMT = o.LastCallactedAMT,
                   TotalCallactedAMT = o.TotalCallactedAMT,
                   LastPaymentBy = o.LastPaymentBy,
                   AddressCode = o.AddressCode,
                   CaseAddess = o.CaseAddess,
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
            return result;
        }
        private DataSourceResult CM00203View01List([DataSourceRequest]DataSourceRequest request, Kaizen.CMS.DataCollection<CM00203View01> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00203View01
               {
                   ClientID = o.ClientID,
                   ClientName = o.ClientName,
                   CaseStatusID = o.CaseStatusID,
                   CaseStatusname = o.CaseStatusname,
                   CaseStatusChild = o.CaseStatusChild,
                   CaseStatusChildName = o.CaseStatusChildName,
                   PTPAMT = o.PTPAMT,
                   //OSAmount = o.OSAmount,
                   FinanceCharge = o.FinanceCharge,
                   ClaimAmount = o.ClaimAmount,
                   PrincipleAmount = o.PrincipleAmount,
                   AgentID = o.AgentID.Trim(),
                   //CaseCount = o.CaseCount
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
            return result;
        }


        public ActionResult GetDataListPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00203Services serv = new CM00203Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00203> L = new Kaizen.CMS.DataCollection<CM00203>();
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
                SortMember = "CaseRef";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00203View01
               {
                   ClientID = o.ClientID,
                   ClientName = o.ClientName,
                   CaseStatusID = o.CaseStatusID,
                   CaseStatusname = o.CaseStatusname,
                   CaseStatusChild = o.CaseStatusChild,
                   CaseStatusChildName = o.CaseStatusChildName,
                   PTPAMT = o.PTPAMT,
                   //OSAmount = o.OSAmount,
                   FinanceCharge = o.FinanceCharge,
                   ClaimAmount = o.ClaimAmount,
                   PrincipleAmount = o.PrincipleAmount,
                   AgentID = o.AgentID.Trim()
                   // CaseCount = o.CaseCount
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00203View01>(),
                    Total = 0
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetListGridWithCIFNumber([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey, string CIFNumber)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00203Services invService = new CM00203Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00203> L = new Kaizen.CMS.DataCollection<CM00203>();
            string filters = string.Empty;
            string SortMember = "CaseRef";
            bool IsAscending = true;
            SortDescriptor sortobject = request.Sorts.FirstOrDefault();
            if (sortobject != null)
            {
                SortMember = sortobject.Member;
                if (sortobject.SortDirection == System.ComponentModel.ListSortDirection.Ascending)
                    IsAscending = true;
                else
                    IsAscending = false;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetListGridWithCIFNumber(SQLQuery, CIFNumber
                 , request.PageSize, request.Page, SortMember, IsAscending);

            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // MainGrid
        [HttpGet]
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request
            ,int ViewID,string KaizenPublicKey, string Searchcritaria)
        {
            if(ViewID == -1)
                return Json(new DataSourceResult(), JsonRequestBehavior.AllowGet);
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00203Services invService = new CM00203Services(KaizenUser);
            //Kaizen.CMS.DataCollection<CM00203> L = new Kaizen.CMS.DataCollection<CM00203>();
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
            string SQLQuery = string.Empty;
            if (request.Filters != null)
            {
                if (request.Filters.Count > 0)
                    SQLQuery = Help.ApplyFilter(request.Filters[0]);
            }
            //if (!string.IsNullOrEmpty(SQLQuery))
            //{
            //    SQLQuery += " and ";
            //}wael
            //SQLQuery += " TRXTypeID='" + TRXTypeID.Trim() + "'";
            KaizenDataTable L = invService.GetAllDataTable(SQLQuery, Searchcritaria, request.PageSize,
                request.Page, SortMember, IsAscending, ViewID);
            //DataSourceResult result = CaseList(request, L);
            //var json = Json(result, JsonRequestBehavior.AllowGet);
            //json.MaxJsonLength = int.MaxValue;
            //return json;
            //L.Items = L.Items.ToDataSourceResult(request);
            DataSourceResult tt = L.Items.ToDataSourceResult(request);
            tt.Total = L.TotalItemCount;
            //result.Items.TotalPageCount = L.TotalPageCount;
            var json = Json(tt, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
            //return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataListAssignGrid([DataSourceRequest]DataSourceRequest request,string TRXTypeID,
    string KaizenPublicKey, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00203Services invService = new CM00203Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00203> L = new Kaizen.CMS.DataCollection<CM00203>();
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
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);
            if (!string.IsNullOrEmpty(SQLQuery))
            {
                SQLQuery += " and ";
            }
            SQLQuery += " TRXTypeID='" + TRXTypeID.Trim() + "'";
            L = invService.GetAllBYSQLQuery(SQLQuery, Searchcritaria, request.PageSize,
                request.Page, SortMember, IsAscending,-1);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ttt([DataSourceRequest]DataSourceRequest request,
   string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00203Services invService = new CM00203Services(KaizenUser);
            List<CM00203> L = new List<CM00203>();
            DataSourceResult result = invService.GetAll().ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPivotGridData([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey,
            List<int> Statuses, List<string> Agents, List<string> Clients)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");

            CM00203Services serv = new CM00203Services(KaizenUser);
            DataSourceResult result = serv.GetAllCM00203View02().ToDataSourceResult(request);
            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetPivotTableData([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
            , string ClientID, string ContractID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");

            CM00203Services serv = new CM00203Services(KaizenUser);
            DataSourceResult result = serv.GetPivotTable(ClientID, ContractID).ToDataSourceResult(request);
            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAgentCasesData([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string Searchcritaria, string AgentID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00203View01Services invService = new CM00203View01Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00203View01> L = new Kaizen.CMS.DataCollection<CM00203View01>();
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
                SortMember = "AgentID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(AgentID))
            {
                L = invService.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria, AgentID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }
            else
            {
                L = invService.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria
               , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = CM00203View01List(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetClientCasesData([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
            , string Searchcritaria, string ClientID, string CUSTCLAS)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00203View01Services invService = new CM00203View01Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00203View01> L = new Kaizen.CMS.DataCollection<CM00203View01>();
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

            L = invService.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria, ClientID, CUSTCLAS
              , request.PageSize, request.Page, SortMember, IsAscending);

            DataSourceResult result = CM00203View01List(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, Kaizen.CMS.DataCollection<CM20203> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM20203
               {
                   CaseRef = o.CaseRef,
                   TRXTypeID = o.TRXTypeID,
                   //TrxTypeName = o.TrxTypeName,
                   ContractID = o.ContractID,
                   ContractName = o.ContractName,
                   ClientID = o.ClientID,
                   ClientName = o.ClientName,
                   CurrencyCode = o.CurrencyCode,
                   DecimalDigit = o.DecimalDigit,
                   ExchangeRate = o.ExchangeRate,
                   IsMultiply = o.IsMultiply,
                   CIFNumber = o.CIFNumber,
                   CIFName = o.CIFName,
                   BucketID = o.BucketID,
                   CaseStatusID = o.CaseStatusID,
                   CaseStatusname = o.CaseStatusname,
                   CaseStatusChild = o.CaseStatusChild,
                   CaseStatusChildName = o.CaseStatusChildName,
                   CaseStatusComment = o.CaseStatusComment,
                   ReminderDateTime = o.ReminderDateTime,
                   PTPAMT = o.PTPAMT,
                   CaseAddess = o.CaseAddess,
                   CaseAccountNo = o.CaseAccountNo,
                   InvoiceNumber = o.InvoiceNumber,
                   ClosingDate = o.ClosingDate,
                   TransactionDate = o.TransactionDate,
                   Remarks = string.IsNullOrEmpty(o.Remarks) ? "" : o.Remarks.Length > 15 ? o.Remarks.Substring(0, 15) : o.Remarks,
                   //OSAmount = o.OSAmount,
                   FinanceCharge = o.FinanceCharge,
                   ClaimAmount = o.ClaimAmount,
                   PrincipleAmount = o.PrincipleAmount,
                   //CreatedDate = o.CreatedDate,
                   AgentID = o.AgentID.Trim(),
                   AssignDate = o.AssignDate,
                   LastPaymentDate = o.LastPaymentDate,
                   LastCallactedAMT = o.LastCallactedAMT,
                   TotalCallactedAMT = o.TotalCallactedAMT,
                   LastPaymentBy = o.LastPaymentBy,
                   BatchID = o.BatchID
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM20203>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetDataListHistoryPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria, string CaseAccountNo)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM20203Services serv = new CM20203Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM20203> L = new Kaizen.CMS.DataCollection<CM20203>();
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
                SortMember = "CaseRef";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, CaseAccountNo
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, Kaizen.CMS.DataCollection<CM30203> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM30203
               {
                   CaseRef = o.CaseRef,
                   TRXTypeID = o.TRXTypeID,
                   //TrxTypeName = o.TrxTypeName,
                   ContractID = o.ContractID,
                   ContractName = o.ContractName,
                   ClientID = o.ClientID,
                   ClientName = o.ClientName,
                   CurrencyCode = o.CurrencyCode,
                   DecimalDigit = o.DecimalDigit,
                   ExchangeRate = o.ExchangeRate,
                   IsMultiply = o.IsMultiply,
                   CIFNumber = o.CIFNumber,
                   CIFName = o.CIFName,
                   BucketID = o.BucketID,
                   CaseStatusID = o.CaseStatusID,
                   CaseStatusname = o.CaseStatusname,
                   CaseStatusChild = o.CaseStatusChild,
                   CaseStatusChildName = o.CaseStatusChildName,
                   CaseStatusComment = o.CaseStatusComment,
                   ReminderDateTime = o.ReminderDateTime,
                   PTPAMT = o.PTPAMT,
                   CaseAddess = o.CaseAddess,
                   CaseAccountNo = o.CaseAccountNo,
                   InvoiceNumber = o.InvoiceNumber,
                   ClosingDate = o.ClosingDate,
                   TransactionDate = o.TransactionDate,
                   Remarks = string.IsNullOrEmpty(o.Remarks) ? "" : o.Remarks.Length > 15 ? o.Remarks.Substring(0, 15) : o.Remarks,
                   //OSAmount = o.OSAmount,
                   FinanceCharge = o.FinanceCharge,
                   ClaimAmount = o.ClaimAmount,
                   PrincipleAmount = o.PrincipleAmount,
                   AgentID = o.AgentID.Trim(),
                   AssignDate = o.AssignDate,
                   LastPaymentDate = o.LastPaymentDate,
                   LastCallactedAMT = o.LastCallactedAMT,
                   TotalCallactedAMT = o.TotalCallactedAMT,
                   LastPaymentBy = o.LastPaymentBy,
                   BatchID = o.BatchID,
                   //Address1 = o.Address1,
                   //Address2 = o.Address2,
                   //Email01 = o.Email01,
                   //Email02 = o.Email02,
                   //Phone01 = o.Phone01,
                   //Phone02 = o.Phone02,
                   //MobileNo1 = o.MobileNo1,
                   //MobileNo2 = o.MobileNo2,
                   //Ext1 = o.Ext1,
                   //Ext2 = o.Ext2,
                   //CountryID = o.CountryID,
                   //CityID = o.CityID,
                   //WebSite = o.WebSite,
                   //Other01 = o.Other01,
                   //Other02 = o.Other02,
                   //POBox = o.POBox,
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM30203>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetDataListDeletedPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria, string CaseRef)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM30203Services serv = new CM30203Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM30203> L = new Kaizen.CMS.DataCollection<CM30203>();
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
                SortMember = "CaseRef";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, CaseRef
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult SaveData(CM00203DatModel CM00203, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            CM00203 o = new Kaizen.CMS.CM00203();
            o.CaseRef = CM00203.CaseRef;
            o.TRXTypeID = CM00203.TRXTypeID;
            o.ContractID = CM00203.ContractID;
            //o.ContractName = CM00203.ContractName;
            o.ClientID = CM00203.ClientID;
            o.ClientName = CM00203.ClientName;
            o.BatchID = CM00203.BatchID;
            o.CurrencyCode = CM00203.CurrencyCode;
            o.DecimalDigit = CM00203.DecimalDigit;
            o.IsMultiply = CM00203.IsMultiply;
            o.ExchangeRate = CM00203.ExchangeRate;
            o.CIFNumber = CM00203.CIFNumber;
            o.CPRCRNo = CM00203.CPRCRNo;
            o.PriorityID = CM00203.PriorityID;
            o.CIFName = CM00203.CIFName;
            o.PriorityName = CM00203.PriorityName;
            o.NPA = CM00203.NPA;
            o.TaskComplated = CM00203.TaskComplated;
            o.ContactTypeID = CM00203.ContactTypeID;
            //o.MobileNo1 = CM00203.MobileNo1;
            //o.MobileNo2 = CM00203.MobileNo2;
            o.IsJoint = CM00203.IsJoint;
            //o.IsHasHistory = CM00203.IsHasHistory;
            //o.IsDeleted = CM00203.IsDeleted;
            o.CYCLEDAY = CM00203.CYCLEDAY;
            o.BucketPrev = CM00203.BucketPrev;
            o.BucketPrevName = CM00203.BucketPrevName;
            o.BucketID = CM00203.BucketID;
            o.BucketName = CM00203.BucketName;
            o.Lookup01 = CM00203.Lookup01;
            o.Lookup01Name = CM00203.Lookup01Name;
            o.Lookup02 = CM00203.Lookup02;
            o.Lookup02Name = CM00203.Lookup02Name;
            o.ProductID = CM00203.ProductID;
            o.ProductName = CM00203.ProductName;
            o.NationalityName  = CM00203.NationalityName;
            o.TxtField01 = CM00203.TxtField01;
            o.TxtField02 = CM00203.TxtField02;
            o.TxtField03 = CM00203.TxtField03;
            o.TxtField04 = CM00203.TxtField04;
            o.TxtField05 = CM00203.TxtField05;
            o.CheckBoxField02 = CM00203.CheckBoxField02;
            o.CheckBoxField01 = CM00203.CheckBoxField01;
            o.AMT01 = CM00203.AMT01;
            o.AMT02 = CM00203.AMT02;
            o.AMT03 = CM00203.AMT03;
            o.AMT04 = CM00203.AMT04;
            o.AMT05 = CM00203.AMT05;
            o.AMT06 = CM00203.AMT06;
            o.AMT07 = CM00203.AMT07;
            o.AMT08 = CM00203.AMT08;
            o.Date01 = CM00203.Date01;
            o.Date02 = CM00203.Date02;
            o.Date03 = CM00203.Date03;
            o.Date04 = CM00203.Date04;
            o.PrincipleAmount = CM00203.PrincipleAmount;
            o.MaturityAmount = CM00203.MaturityAmount;
            o.OutstandingAMT = CM00203.OutstandingAMT;
            o.OutStandingToday = CM00203.OutStandingToday;
            o.InstallmentAmount = CM00203.InstallmentAmount;
            o.ClaimAmount = CM00203.ClaimAmount;
            o.FinanceCharge = CM00203.FinanceCharge;
            o.WriteOffAMT = CM00203.WriteOffAMT;
            o.CreditLimit = CM00203.CreditLimit;
            o.LastPaymentAMTUpload = CM00203.LastPaymentAMTUpload;
            o.TotalLifeCollactedAMT = CM00203.TotalLifeCollactedAMT;
            o.CaseAccountNo = CM00203.CaseAccountNo;
            o.InvoiceNumber = CM00203.InvoiceNumber;
            o.OverDueDays = CM00203.OverDueDays;
            o.FirstDisburementDate = CM00203.FirstDisburementDate;
            o.MATURITY_DATE = CM00203.MATURITY_DATE;
            o.TransactionDate = CM00203.TransactionDate;
            o.OverDueDate = CM00203.OverDueDate;
            o.LastPaymentDateUplod = CM00203.LastPaymentDateUplod;
            o.ClosingDate = CM00203.ClosingDate;
            o.BookingDate = CM00203.BookingDate;
            o.FirstLifeOverDueDate = CM00203.FirstLifeOverDueDate;
            o.Remarks = CM00203.Remarks;
            //o.CreatedDate = CM00203.CreatedDate;
            o.AssignDate = CM00203.AssignDate;
            o.AgentID = CM00203.AgentID;
            o.AddressCode = CM00203.AddressCode;
            o.CaseAddess = CM00203.CaseAddess;
            o.CaseStatusID = CM00203.CaseStatusID;
            return Json(service.AddCM00203(o), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(CM00203DatModel CM00203, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            CM00203 o = new Kaizen.CMS.CM00203();
            o.CaseRef = CM00203.CaseRef;
            o.TRXTypeID = CM00203.TRXTypeID;
            o.ContractID = CM00203.ContractID;
            //o.ContractName = CM00203.ContractName;
            o.ClientID = CM00203.ClientID;
            o.ClientName = CM00203.ClientName;
            o.BatchID = CM00203.BatchID;
            o.CurrencyCode = CM00203.CurrencyCode;
            o.DecimalDigit = CM00203.DecimalDigit;
            o.IsMultiply = CM00203.IsMultiply;
            o.ExchangeRate = CM00203.ExchangeRate;
            o.CIFNumber = CM00203.CIFNumber;
            o.CPRCRNo = CM00203.CPRCRNo;
            o.PriorityID = CM00203.PriorityID;
            o.CIFName = CM00203.CIFName;
            o.PriorityName = CM00203.PriorityName;
            o.NPA = CM00203.NPA;
            o.TaskComplated = CM00203.TaskComplated;
            o.ContactTypeID = CM00203.ContactTypeID;
            //o.MobileNo1 = CM00203.MobileNo1;
            //o.MobileNo2 = CM00203.MobileNo2;
            o.IsJoint = CM00203.IsJoint;
            //o.IsHasHistory = CM00203.IsHasHistory;
            //o.IsDeleted = CM00203.IsDeleted;
            o.CYCLEDAY = CM00203.CYCLEDAY;
            o.BucketPrev = CM00203.BucketPrev;
            o.BucketPrevName = CM00203.BucketPrevName;
            o.BucketID = CM00203.BucketID;
            o.BucketName = CM00203.BucketName;
            o.Lookup01 = CM00203.Lookup01;
            o.Lookup01Name = CM00203.Lookup01Name;
            o.Lookup02 = CM00203.Lookup02;
            o.Lookup02Name = CM00203.Lookup02Name;
            o.ProductID = CM00203.ProductID;
            o.ProductName = CM00203.ProductName;
            //o.National = CM00203.NationalityCase;
            o.TxtField01 = CM00203.TxtField01;
            o.TxtField02 = CM00203.TxtField02;
            o.TxtField03 = CM00203.TxtField03;
            o.TxtField04 = CM00203.TxtField04;
            o.TxtField05 = CM00203.TxtField05;
            o.CheckBoxField02 = CM00203.CheckBoxField02;
            o.CheckBoxField01 = CM00203.CheckBoxField01;
            o.AMT01 = CM00203.AMT01;
            o.AMT02 = CM00203.AMT02;
            o.AMT03 = CM00203.AMT03;
            o.AMT04 = CM00203.AMT04;
            o.AMT05 = CM00203.AMT05;
            o.AMT06 = CM00203.AMT06;
            o.AMT07 = CM00203.AMT07;
            o.AMT08 = CM00203.AMT08;
            o.Date01 = CM00203.Date01;
            o.Date02 = CM00203.Date02;
            o.Date03 = CM00203.Date03;
            o.Date04 = CM00203.Date04;
            o.PrincipleAmount = CM00203.PrincipleAmount;
            o.MaturityAmount = CM00203.MaturityAmount;
            o.OutstandingAMT = CM00203.OutstandingAMT;
            o.OutStandingToday = CM00203.OutStandingToday;
            o.InstallmentAmount = CM00203.InstallmentAmount;
            o.ClaimAmount = CM00203.ClaimAmount;
            o.FinanceCharge = CM00203.FinanceCharge;
            o.WriteOffAMT = CM00203.WriteOffAMT;
            o.CreditLimit = CM00203.CreditLimit;
            o.LastPaymentAMTUpload = CM00203.LastPaymentAMTUpload;
            o.TotalLifeCollactedAMT = CM00203.TotalLifeCollactedAMT;
            o.CaseAccountNo = CM00203.CaseAccountNo;
            o.InvoiceNumber = CM00203.InvoiceNumber;
            o.OverDueDays = CM00203.OverDueDays;
            o.FirstDisburementDate = CM00203.FirstDisburementDate;
            o.MATURITY_DATE = CM00203.MATURITY_DATE;
            o.TransactionDate = CM00203.TransactionDate;
            o.OverDueDate = CM00203.OverDueDate;
            o.LastPaymentDateUplod = CM00203.LastPaymentDateUplod;
            o.ClosingDate = CM00203.ClosingDate;
            o.BookingDate = CM00203.BookingDate;
            o.FirstLifeOverDueDate = CM00203.FirstLifeOverDueDate;
            o.Remarks = CM00203.Remarks;
            o.AssignDate = CM00203.AssignDate;
            o.AgentID = CM00203.AgentID;
            o.AddressCode = CM00203.AddressCode;
            o.CaseAddess = CM00203.CaseAddess;
            o.CaseStatusID = CM00203.CaseStatusID;
            return Json(service.Update(o), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(CM00203 CM00203, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.DeleteCase(CM00203), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ArchiveData(CM00203 CM00203, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.ArchiveCase(CM00203), JsonRequestBehavior.AllowGet);
        }
      
        public ActionResult GetSingleTask(string KaizenPublicKey, int TaskID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00213Services service = new CM00213Services(KaizenUser);
            CM00213 L = service.GetSingle(TaskID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTaskActionList(string KaizenPublicKey, int TaskID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00214Services service = new CM00214Services(KaizenUser);
            IList<CM00214> L = service.GetAllByTaskID(TaskID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveTaskAction(CM00214 oCM00214, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00214Services service = new CM00214Services(KaizenUser);
            return Json(service.AddCM00214(oCM00214), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string CaseRef)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            CM00203 CM00203 = service.GetSingle(CaseRef);
            CM00203DatModel o = new CM00203DatModel();
            o.CaseRef = CM00203.CaseRef;
            //o.PastDueCount = CM00203.PastDueCount;
            o.TRXTypeID = CM00203.TRXTypeID;
            o.PstDueCountOpen = CM00203.PstDueCountOpen;
            o.PstDueCountClient = CM00203.PstDueCountClient;
            o.PstDueCountHstry = CM00203.PstDueCountHstry;
            o.ContractID = CM00203.ContractID;
            //o.ContractName = CM00203.ContractName;
            o.ClientID = CM00203.ClientID;
            o.ClientName = CM00203.ClientName;
            o.NationaltyClient = CM00203.NationaltyClient;
            o.NationltyCreditor = CM00203.NationltyCreditor;
            o.NationltyPartner = CM00203.NationltyPartner;
            o.NationaltyIDCIF = CM00203.NationaltyIDCIF;
            o.NationalityName = CM00203.NationalityName;
            o.EmployerName = CM00203.EmployerName;
            o.CreditorID = CM00203.CreditorID;
            o.CreditorName = CM00203.CreditorName;
            o.PartnerID = CM00203.PartnerID;
            o.PartnerName = CM00203.PartnerName;
            o.PartnrAssinDate = CM00203.PartnrAssinDate;
            o.LegalID = CM00203.LegalID;
            o.LegalName = CM00203.LegalName;
            o.LegalAssignDate = CM00203.LegalAssignDate;
            o.BatchID = CM00203.BatchID;
            o.CurrencyCode = CM00203.CurrencyCode;
            o.DecimalDigit = CM00203.DecimalDigit;
            o.IsMultiply = CM00203.IsMultiply;
            o.ExchangeRate = CM00203.ExchangeRate;
            o.CIFNumber = CM00203.CIFNumber;
            o.CPRCRNo = CM00203.CPRCRNo;
            o.CIFName = CM00203.CIFName;
            o.PriorityID = CM00203.PriorityID;
            o.PriorityName = CM00203.PriorityName;
            o.NPA = CM00203.NPA;
            o.TaskComplated = CM00203.TaskComplated;
            o.ContactTypeID = CM00203.ContactTypeID;
            o.IsJoint = CM00203.IsJoint;
            o.CaseStatusID = CM00203.CaseStatusID;
            o.CaseStatusChild = CM00203.CaseStatusChild;
            o.CaseStatusChildName = CM00203.CaseStatusChildName;
            o.CaseStatusname = CM00203.CaseStatusname;
            o.CaseStatusComment = CM00203.CaseStatusComment;
            o.ReminderDateTime = CM00203.ReminderDateTime;
            o.PTPAMT = CM00203.PTPAMT;
            o.PTPCount = CM00203.PTPCount;
            o.PTPkept = CM00203.PTPkept;
            o.PTPBroken = CM00203.PTPBroken;
            o.LastCaseStatusID = CM00203.LastCaseStatusID;
            o.LastCasStatusChldID = CM00203.LastCasStatusChldID;
            o.LastCasStatusname = CM00203.LastCasStatusname;
            o.LastCasStatusChldNam = CM00203.LastCasStatusChldNam;
            o.LastCasStatusComment = CM00203.LastCasStatusComment;
            o.MidCasStatusID = CM00203.MidCasStatusID;
            o.MidCasStatusChld = CM00203.MidCasStatusChld;
            o.MidCasStatusnam = CM00203.MidCasStatusnam;
            o.MidCasStatusChldName = CM00203.MidCasStatusChldName;
            o.MidCasStatusComment = CM00203.MidCasStatusComment;
            o.CYCLEDAY = CM00203.CYCLEDAY;
            o.BucketPrev = CM00203.BucketPrev;
            o.BucketPrevName = CM00203.BucketPrevName;
            o.BucketID = CM00203.BucketID;
            o.BucketName = CM00203.BucketName;
            o.Lookup01 = CM00203.Lookup01;
            o.Lookup01Name = CM00203.Lookup01Name;
            o.Lookup02 = CM00203.Lookup02;
            o.Lookup02Name = CM00203.Lookup02Name;
            o.ProductID = CM00203.ProductID;
            o.ProductName = CM00203.ProductName;
           
            o.TxtField01 = CM00203.TxtField01;
            o.TxtField02 = CM00203.TxtField02;
            o.TxtField03 = CM00203.TxtField03;
            o.TxtField04 = CM00203.TxtField04;
            o.TxtField05 = CM00203.TxtField05;
            o.TxtField06 = CM00203.TxtField06;
            o.TxtField07 = CM00203.TxtField07;
            o.CheckBoxField02 = CM00203.CheckBoxField02;
            o.CheckBoxField01 = CM00203.CheckBoxField01;
            o.CheckBoxField03 = CM00203.CheckBoxField03;
            o.AMT01 = CM00203.AMT01;
            o.AMT02 = CM00203.AMT02;
            o.AMT03 = CM00203.AMT03;
            o.AMT04 = CM00203.AMT04;
            o.AMT05 = CM00203.AMT05;
            o.AMT06 = CM00203.AMT06;
            o.AMT07 = CM00203.AMT07;
            o.AMT08 = CM00203.AMT08;
            o.AMT09 = CM00203.AMT09;
            o.AMT10 = CM00203.AMT10;
            o.Date01 = CM00203.Date01;
            o.Date02 = CM00203.Date02;
            o.Date03 = CM00203.Date03;
            o.Date04 = CM00203.Date04;
            o.PrincipleAmount = CM00203.PrincipleAmount;
            o.MaturityAmount = CM00203.MaturityAmount;
            o.OutstandingAMT = CM00203.OutstandingAMT;
            o.OutStandingToday = CM00203.OutStandingToday;
            o.InstallmentAmount = CM00203.InstallmentAmount;
            o.ClaimAmount = CM00203.ClaimAmount;
            o.FinanceCharge = CM00203.FinanceCharge;
            o.WriteOffAMT = CM00203.WriteOffAMT;
            o.CreditLimit = CM00203.CreditLimit;
            o.LastPaymentAMTUpload = CM00203.LastPaymentAMTUpload;
            o.TotalLifeCollactedAMT = CM00203.TotalLifeCollactedAMT;
            o.CaseAccountNo = CM00203.CaseAccountNo;
            o.InvoiceNumber = CM00203.InvoiceNumber;
            o.OverDueDays = CM00203.OverDueDays;
            o.FirstDisburementDate = CM00203.FirstDisburementDate;
            o.MATURITY_DATE = CM00203.MATURITY_DATE;
            o.TransactionDate = CM00203.TransactionDate;
            o.OverDueDate = CM00203.OverDueDate;
            o.LastPaymentDateUplod = CM00203.LastPaymentDateUplod;
            o.ClosingDate = CM00203.ClosingDate;
            o.BookingDate = CM00203.BookingDate;
            o.FirstLifeOverDueDate = CM00203.FirstLifeOverDueDate;
            o.Remarks = CM00203.Remarks;
            o.AssignDate = CM00203.AssignDate;
            o.AgentID = CM00203.AgentID;
            o.LastPaymentDate = CM00203.LastPaymentDate;
            o.LastCallactedAMT = CM00203.LastCallactedAMT;
            o.TotalCallactedAMT = CM00203.TotalCallactedAMT;
            o.CommissionRate = CM00203.CommissionRate;
            o.LastPaymentBy = CM00203.LastPaymentBy;
            o.MonthlySalary = CM00203.MonthlySalary;
            o.AddressCode = CM00203.AddressCode;
            o.CaseAddess = CM00203.CaseAddess;
            
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSingleByDebtor(string KaizenPublicKey, string DebtorID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            CM00203 o = service.GetSingleByDebtorID(DebtorID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllByDebtor(string KaizenPublicKey, string DebtorID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.GetAllByDebtorID(DebtorID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTop10BYCaseRef(string KaizenPublicKey, [DataSourceRequest]DataSourceRequest request, string CaseRef)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services invService = new CM00203Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00203> L = new Kaizen.CMS.DataCollection<CM00203>();
            if (CaseRef != null && !string.IsNullOrEmpty(CaseRef))
                L = invService.GetTop10BYCaseRef(CaseRef);
            else
                L = null;
            DataSourceResult result = CaseList(request, L);
            var r = Json(result.Data, JsonRequestBehavior.AllowGet);
            r.MaxJsonLength = int.MaxValue;
            return r;
        }
        public ActionResult FillDropDownList(string KaizenPublicKey, string field)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00203Services service = new CM00203Services(KaizenUser);
            IList<CM00203> L = service.GetAllByField(field);
            return Json(L, JsonRequestBehavior.AllowGet);
        }


        #region Case Payment History
        private DataSourceResult CasePaymentHistoryList([DataSourceRequest]DataSourceRequest request, Kaizen.CMS.DataCollection<CM00207> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM00207
                {
                    PaymentID = o.PaymentID,
                    DebtorID = o.DebtorID,
                    CheckbookCode = o.CheckbookCode,
                    CurrencyCode = o.CurrencyCode,
                    ExchangeTableID = o.ExchangeTableID,
                    IsMultiply = o.IsMultiply,
                    ExchangeRateID = o.ExchangeRateID,
                    ExchangeRate = o.ExchangeRate,
                    DecimalDigit = o.DecimalDigit,
                    Amount = o.Amount,
                    PaymentDate = o.PaymentDate,
                    Description = o.Description,
                    PaymentMethodID = o.PaymentMethodID,
                    AgentID = o.AgentID,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    PaymentNumber = o.PaymentNumber,
                    BankName = o.BankName,
                    BankAccount = o.BankAccount,
                    IsApproved = o.IsApproved,
                    VoidDate = o.VoidDate,
                    VoidSystemDate = o.VoidSystemDate,
                    BankCheckDate = o.BankCheckDate,
                    BankCode = o.BankCode,
                    //ReasonID = o.ReasonID,
                    VoidBy = o.VoidBy
                }
                );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00207>(),
                    Total = 0
                };
            }
            return result;
        }

        public JsonResult GetCasePaymentHistoryDataListGrid([DataSourceRequest]DataSourceRequest request,
            string Searchcritaria, string DebtorID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00207Services serv = new CM00207Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00207> L = new Kaizen.CMS.DataCollection<CM00207>();
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
                SortMember = "PaymentID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria, DebtorID
                                , request.PageSize, request.Page, SortMember, IsAscending);

            DataSourceResult result = CasePaymentHistoryList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Bulk Actions
        public ActionResult SaveBulkUploadActions(IEnumerable<HttpPostedFileBase> SaveBulkUploadUpdate)
        {
            var fileName = "";
            List<Kaizen.Configuration.ExcelColumns> lstExcelColumns = null;
            if (SaveBulkUploadUpdate != null)
            {
                foreach (var file in SaveBulkUploadUpdate)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                    var destinationPath = Path.Combine(Server.MapPath("~/Files"), fileName);
                    file.SaveAs(destinationPath);
                    Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(System.Web.HttpContext.Current.Request["KaizenPublicKey"]);
                    Kaizen.BusinessLogic.Configuration.CompanyacessServices srv = new Kaizen.BusinessLogic.Configuration.CompanyacessServices(KaizenUser);
                    lstExcelColumns = srv.ReadExcelColumnsWithoutInsert(destinationPath, fileName);
                }
            }
            TempTable o = new TempTable();
            o.TableName = fileName;
            o.ExcelColumns = lstExcelColumns;
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BulkDelete(IList<CM00203> CM00203, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.BulkDelete(CM00203), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveUploadedBulkDelete(
            IList<Kaizen.Configuration.Kaizen00001> KaizenColumn
            , string TableName, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            CompanyacessServices srv = new CompanyacessServices(KaizenUser);
            var destinationPath = Path.Combine(Server.MapPath("~/Files"), TableName);
            DataTable dt = srv.ReadExcelSheet(destinationPath);
            List<string> Ids = new List<string>();

            foreach (DataRow item in dt.Rows)
            {
                Ids.Add(item[KaizenColumn[0].FieldValue].ToString());
            }
            return Json(service.BulkDeleteWithCaseRef(Ids), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveUploadedBulkUpdate(
    IList<Kaizen.Configuration.Kaizen00001> KaizenColumn
    , string TableName, string Field, string Value, string Type, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            CompanyacessServices srv = new CompanyacessServices(KaizenUser);
            var destinationPath = Path.Combine(Server.MapPath("~/Files"), TableName);
            DataTable dt = srv.ReadExcelSheet(destinationPath);
            List<string> Ids = new List<string>();

            foreach (DataRow item in dt.Rows)
            {
                Ids.Add(item[KaizenColumn[0].FieldValue].ToString());
            }
            return Json(service.BulkUpdateWithCaseRef(Ids, Field, Value, Type), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Case Status History
        private DataSourceResult CaseStatusHistoryList([DataSourceRequest]DataSourceRequest request, Kaizen.CMS.DataCollection<CM10701> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM10701
                {
                    StatusHistoryID = o.StatusHistoryID,
                    CaseStatusID = o.CaseStatusID,
                    CaseStatusname =o.CaseStatusname,
                    CaseRef = o.CaseRef,
                    AgentID = o.AgentID,
                    CreatedDate = o.CreatedDate,
                    ChangeStatusSourceID = o.ChangeStatusSourceID,
                    CaseStatusComment = o.CaseStatusComment,
                    PTPAMT = o.PTPAMT,
                    ReminderDateTime = o.ReminderDateTime
                }
                );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM10701>(),
                    Total = 0
                };
            }
            return result;
        }

        public JsonResult GetCaseStatusHistoryDataListGrid([DataSourceRequest]DataSourceRequest request,
            string Searchcritaria, string CaseRef, bool WithHistory,int StatusActionTypeID,
            string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM10701Services serv = new CM10701Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM10701> L = new Kaizen.CMS.DataCollection<CM10701>();
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
                SortMember = "StatusHistoryID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(CaseRef))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria, CaseRef, WithHistory
                                , request.PageSize, request.Page, SortMember, IsAscending);
            }
            else
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria
                                , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = CaseStatusHistoryList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
         
        public ActionResult SaveCaseHistoryData(IList<CM00060> CM00060List,
          CM10701 CM10701, string KaizenPublicKey)
        { 
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CM10701Services serv = new CM10701Services(KaizenUser);
            return Json(serv.AddCM10701(CM00060List,CM10701), JsonRequestBehavior.AllowGet);
        }
         
        #endregion

        #region Case Assign History
        private DataSourceResult CaseAssignHistoryList([DataSourceRequest]DataSourceRequest request, Kaizen.CMS.DataCollection<CM00206> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM00206
                {
                    //AssignHistoryID = o.AssignHistoryID,
                    CaseRef = o.CaseRef,
                    AgentID = o.AgentID,
                    AssignHistoryDate = o.AssignHistoryDate,
                }
                );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00206>(),
                    Total = 0
                };
            }
            return result;
        }

        public JsonResult GetCaseAssignHistoryDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string CaseRef, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00206Services serv = new CM00206Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00206> L = new Kaizen.CMS.DataCollection<CM00206>();
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
                SortMember = "AssignHistoryID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(CaseRef))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria, CaseRef
                                , request.PageSize, request.Page, SortMember, IsAscending);
            }
            else
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria
                                , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = CaseAssignHistoryList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCaseAssignHistoryData(CM00203 CM00203, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            var result = service.Update(CM00203);
            if (result.Status)
            {
                CM00206Services serv = new CM00206Services(KaizenUser);
                CM00206 CM00206 = new CM00206();
                CM00206.CaseRef = CM00203.CaseRef;
                CM00206.AgentID = CM00203.AgentID;
                CM00206.AssignHistoryDate = CM00203.AssignDate.HasValue ? CM00203.AssignDate.Value : DateTime.Now ;
                return Json(serv.AddCM00206(CM00206), JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Case Task History
        private DataSourceResult CaseTaskHistoryList([DataSourceRequest]DataSourceRequest request, Kaizen.CMS.DataCollection<CM00213> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
                o => new CM00213
                {
                    TaskID = o.TaskID,
                    TaskTypeID = o.TaskTypeID,
                    PriorityID = o.PriorityID,
                    CaseRef = o.CaseRef,
                    TaskTitle = o.TaskTitle,
                    TaskDescription = o.TaskDescription,
                    TaskStartDate = o.TaskStartDate,
                    TaskEndDate = o.TaskEndDate,
                    AgentID = o.AgentID,
                    AssignDate = o.AssignDate,
                    TaskProgress = o.TaskProgress,
                }
                );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00213>(),
                    Total = 0
                };
            }
            return result;
        }

        public JsonResult GetCaseTaskHistoryDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string CaseRef, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00213Services serv = new CM00213Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00213> L = new Kaizen.CMS.DataCollection<CM00213>();
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
                SortMember = "TaskID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(CaseRef))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria, CaseRef
                                , request.PageSize, request.Page, SortMember, IsAscending);
            }
            else
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria
                                , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = CaseTaskHistoryList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCaseTaskData(CM00213 CM00213, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CM00213Services service = new CM00213Services(KaizenUser);
            CM00213.AssignDate = DateTime.Now;
            var result = service.AddCM00213(CM00213);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCaseTaskData(CM00213 CM00213, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CM00213Services service = new CM00213Services(KaizenUser);
            var result = service.Update(CM00213);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Case Event
        public ActionResult GetAllCasesReminders(string KaizenPublicKey, string AgentID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM10701Services service = new CM10701Services(KaizenUser);
            IList<CM10701> List = service.GetAllByAgent(AgentID);
            List<CM10701> CM10701List = new List<CM10701>();
            foreach (var item in List)
            {
                CM10701 obj = new CM10701()
                {
                    StatusHistoryID = item.StatusHistoryID,
                    CaseStatusID = item.CaseStatusID,
                    CaseRef = item.CaseRef,
                    AgentID = item.AgentID,
                    ReminderDateTime = item.ReminderDateTime,
                    CaseStatusComment = item.CaseStatusComment,
                    PTPAMT = item.PTPAMT,
                };
                CM10701List.Add(obj);
            }
            JsonResult result = Json(CM10701List);
            result.MaxJsonLength = int.MaxValue;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public ActionResult SaveCaseEventData(IList<CM00025> CM00025
            , string CaseRef
            , string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            CM10701Services serv = new CM10701Services(KaizenUser);
            CM00213Services CM00213_serv = new CM00213Services(KaizenUser);
            List<CM00213> CM00213List = new List<CM00213>();
            foreach (var item in CM00025)
            {
                CM00213 CM00213 = new CM00213()
                {
                    TaskIDSource = item.TaskID,
                    TaskTypeID = item.TaskTypeID,
                    PriorityID = item.PriorityID,
                    CaseRef = CaseRef,
                    TaskTitle = item.TaskTitle,
                    TaskDescription = item.TaskDescription,
                    TaskStartDate = item.TaskStartDate,
                    TaskEndDate = item.TaskEndDate,
                    AgentID = item.AgentID,
                    AssignDate = DateTime.Now,
                    TaskProgress = item.TaskProgress,
                };
                CM00213List.Add(CM00213);

            }
            return Json(CM00213_serv.AddCM00213(CM00213List), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Case Document
        public ActionResult CaseDocument(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Kaizen.BusinessLogic.Admin.Sys00007Services srv = new Kaizen.BusinessLogic.Admin.Sys00007Services(KaizenUser);
            ViewData["DocumentTypes"] = srv.GetAll(6);
            return PartialView();
        }
        private DataSourceResult CaseDocumentList([DataSourceRequest]DataSourceRequest request, Kaizen.CMS.DataCollection<CM00208> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00208
               {
                   DocumentID = o.DocumentID,
                   CaseRef = o.CaseRef,
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
        public ActionResult GetDocumentListGridWithCase([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string CaseRef, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00208Services serv = new CM00208Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00208> L = new Kaizen.CMS.DataCollection<CM00208>();
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

            if (!string.IsNullOrEmpty(CaseRef))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, CaseRef
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = CaseDocumentList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCaseDocument(CM00208 CM00208, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00208Services service = new CM00208Services(KaizenUser);
            if (!string.IsNullOrEmpty(CM00208.PhotoExtension))
            {
                string PhotoPath = Server.MapPath(@"\\Photo\\CaseDocumentsTemp\\" + CM00208.PhotoExtension.Trim());
                FileInfo TempFile_info = new FileInfo(PhotoPath);
                CM00208.PhotoExtension = TempFile_info.Extension;
                if (System.IO.File.Exists(PhotoPath))
                {
                    string Destination = Server.MapPath(@"\\Photo\\CaseDocuments\\" + CM00208.DocumentName + CM00208.PhotoExtension);
                    if (System.IO.File.Exists(Destination))
                    {
                        FileInfo file_info = new FileInfo(Destination);
                        string dirPath = file_info.DirectoryName;
                        string fileName = Path.GetFileNameWithoutExtension(file_info.Name);
                        string[] files = Directory.GetFiles(dirPath);
                        int count = files.Count(file => { return file.Contains(fileName); });
                        string newFileName = String.Format("{0} ({1})", fileName, count);
                        CM00208.DocumentName = newFileName;
                        Destination = Server.MapPath(@"\\Photo\\CaseDocuments\\" + CM00208.DocumentName + CM00208.PhotoExtension);
                    }
                    System.IO.File.Move(PhotoPath, Destination);
                }
            }
            return Json(service.AddCM00208(CM00208), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCaseDocument(CM00208 CM00208, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00208Services service = new CM00208Services(KaizenUser);
            return Json(service.Update(CM00208), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCaseDocument(CM00208 CM00208, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00208Services service = new CM00208Services(KaizenUser);
            string ServerPath = Server.MapPath(@"\\Photo\\CaseDocuments\\");
            return Json(service.Delete(CM00208, ServerPath), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCaseDocumentTemp(IEnumerable<HttpPostedFileBase> CMS_CaseDocumentattachments)
        {
            var fileName = "";
            foreach (var file in CMS_CaseDocumentattachments)
            {
                fileName = Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/Photo/CaseDocumentsTemp"), fileName);
                file.SaveAs(destinationPath);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveCaseDocumentTemp(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Photo/CaseDocumentsTemp"), fileName);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }
        #endregion

        public ActionResult SendCaseByEmail(CM00203 CM00203, List<KaizenEmailAddress> toAddressList,
    List<KaizenEmailAddress> CcAddressList, List<KaizenEmailAddress> BccAddressList,
    Kaizen.Configuration.Kaizen00020 userEmailSetting, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            string html = RazorViewToString.RenderRazorViewToString(this, "~/Views/CMS_Case/EmailTemplate.cshtml", CM00203);
            return Json(KaizenComposeEmail.SendMail("Details About Case Reference : #" + CM00203.CaseRef,
                html, userEmailSetting,
                toAddressList, CcAddressList, BccAddressList,
                null, SendOptions.Single, KaizenPublicKey), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendTemplateEmail(CM00203 CM00203, List<KaizenEmailAddress> toAddressList,
    List<KaizenEmailAddress> CcAddressList, List<KaizenEmailAddress> BccAddressList, string TemplateContant,
    Kaizen.Configuration.Kaizen00020 userEmailSetting, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            KaizenReplacer KR = new KaizenReplacer("[", "]");
            KR.Append(TemplateContant);
            KR.Replace("[CaseRef]", CM00203.CaseRef);
            KR.Replace("[CurrencyCode]", CM00203.CurrencyCode);
            KR.Replace("[TRXTypeID]", CM00203.TRXTypeID.ToString());
            KR.Replace("[ContractID]", CM00203.ContractID);
            KR.Replace("[ClientID]", CM00203.ClientID);
            KR.Replace("[ClientName]", CM00203.ClientName);
            KR.Replace("[BatchID]", CM00203.BatchID);
            KR.Replace("[DebtorID]", CM00203.CaseRef);
            KR.Replace("[DebtorName]", CM00203.CIFName);
            KR.Replace("[AddressCode]", CM00203.AddressCode);
            //KR.Replace("[AddressName]", CM00203.AddressName);
            //KR.Replace("[WebSite]", CM00203.WebSite);
            //KR.Replace("[CountryID]", CM00203.CountryID);
            //KR.Replace("[CityID]", CM00203.CityID);
            //KR.Replace("[Phone01]", CM00203.Phone01);
            //KR.Replace("[Phone02]", CM00203.Phone02);
            //KR.Replace("[Ext1]", CM00203.Ext1);
            //KR.Replace("[Ext2]", CM00203.Ext2);
            //KR.Replace("[MobileNo1]", CM00203.MobileNo1);
            //KR.Replace("[MobileNo2]", CM00203.MobileNo2);
            //KR.Replace("[POBox]", CM00203.POBox);
            //KR.Replace("[Other01]", CM00203.Other01);
            //KR.Replace("[Other02]", CM00203.Other02);
            //KR.Replace("[Address1]", CM00203.Address1);
            //KR.Replace("[Address2]", CM00203.Address2);
            //KR.Replace("[Email01]", CM00203.Email01);
            //KR.Replace("[Email02]", CM00203.Email02);
            KR.Replace("[BucketID]", CM00203.BucketID.ToString());
            KR.Replace("[CaseStatusID]", CM00203.CaseStatusID.ToString());
            KR.Replace("[CaseStatusname]", CM00203.CaseStatusname);
            KR.Replace("[CaseStatusChild]", CM00203.CaseStatusChild.ToString());
            KR.Replace("[CaseStatusChildName]", CM00203.CaseStatusChildName);
            KR.Replace("[CaseStatusComment]", CM00203.CaseStatusComment);
            KR.Replace("[ReminderDateTime]", CM00203.ReminderDateTime != null ? CM00203.ReminderDateTime.Value.ToString("dd/MM/yyyy hh:mm:ss tt") : "");
            KR.Replace("[PTPAMT]", CM00203.PTPAMT.ToString());
            KR.Replace("[ContactTypeID]", CM00203.ContactTypeID.ToString());
            KR.Replace("[CaseAccountNo]", CM00203.CaseAccountNo);
            KR.Replace("[InvoiceNumber]", CM00203.InvoiceNumber);
            KR.Replace("[ClosingDate]", CM00203.ClosingDate != null ? CM00203.ClosingDate.Value.ToString("dd/MM/yyyy") : "");
            KR.Replace("[TransactionDate]", CM00203.TransactionDate != null ? CM00203.TransactionDate.Value.ToString("dd/MM/yyyy") : "");
            //KR.Replace("[BookingDate]", CM00203.BookingDate != null ? CM00203.BookingDate.ToString("dd/MM/yyyy") : "");
            KR.Replace("[Remarks]", CM00203.Remarks);
            //KR.Replace("[OSAmount]", CM00203.OSAmount.ToString());
            KR.Replace("[FinanceCharge]", CM00203.FinanceCharge.ToString());
            KR.Replace("[ClaimAmount]", CM00203.ClaimAmount.ToString());
            KR.Replace("[PrincipleAmount]", CM00203.PrincipleAmount.ToString());
            KR.Replace("[AgentID]", CM00203.AgentID);
            //KR.Replace("[AssignDate]", CM00203.AssignDate.ToString("dd/MM/yyyy"));
            KR.Replace("[LastPaymentDate]", CM00203.LastPaymentDate != null ? CM00203.LastPaymentDate.Value.ToString("dd/MM/yyyy") : "");
            KR.Replace("[LastCallactedAMT]", CM00203.LastCallactedAMT.ToString());
            KR.Replace("[TotalCallactedAMT]", CM00203.TotalCallactedAMT.ToString());
            KR.Replace("[LastPaymentBy]", CM00203.LastPaymentBy);
            return Json(KaizenComposeEmail.SendMail("Case Reference : #" + CM00203.CaseRef,
                KR.ToString(), userEmailSetting,
                toAddressList, CcAddressList, BccAddressList,
                null, SendOptions.Single, KaizenPublicKey), JsonRequestBehavior.AllowGet);
        }


        public ActionResult CaseBuketReportView(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult CaseBuketReport(
            string KaizenPublicKey,string CaseAccountNo)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services serv = new CM00203Services(KaizenUser);
            return Json(serv.CaseBuketReport(CaseAccountNo), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CaseReconcile(string ClientID,string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00203Services service = new CM00203Services(KaizenUser);
            return Json(service.CaseReconcile(), JsonRequestBehavior.AllowGet);
        }


        ///-------------------------------------------------------
        public ActionResult GetMyViewsByTRXTypeID(int TRXTypeID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services oKaizen00011 = new CM00029Services(KaizenUser);
            List<CM00071> Views = oKaizen00011.GetViews(TRXTypeID, KaizenUser.UserName).ToList();
            return Json(Views, JsonRequestBehavior.AllowGet);
        }
    }
    public class CM00203DatModel
    {
        public string CaseRef { get; set; }
        public int TRXTypeID { get; set; }
        public Nullable<int> PstDueCountOpen { get; set; }
        public Nullable<int> PstDueCountClient { get; set; }
        public Nullable<int> PstDueCountHstry { get; set; }
        public string ContractID { get; set; }
        public string ContractName { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string NationaltyClient { get; set; }
        public string NationltyCreditor { get; set; }
        public string NationltyPartner { get; set; }
        public string NationaltyIDCIF { get; set; }
        public string NationalityName { get; set; }
        public string EmployerName { get; set; }
        public string CreditorID { get; set; }
        public string CreditorName { get; set; }
        public string PartnerID { get; set; }
        public string PartnerName { get; set; }
        public Nullable<System.DateTime> PartnrAssinDate { get; set; }
        public string LegalID { get; set; }
        public string LegalName { get; set; }
        public Nullable<System.DateTime> LegalAssignDate { get; set; }
        public string BatchID { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<byte> DecimalDigit { get; set; }
        public Nullable<bool> IsMultiply { get; set; }
        public Nullable<double> ExchangeRate { get; set; }
        public string CIFNumber { get; set; }
        public string CPRCRNo { get; set; }
        public string CIFName { get; set; }
        public Nullable<int> PriorityID { get; set; }
        public string PriorityName { get; set; }
        public Nullable<double> NPA { get; set; }
        public Nullable<double> TaskComplated { get; set; }
        public Nullable<int> ContactTypeID { get; set; }
        public Nullable<bool> IsJoint { get; set; }
        public int CaseStatusID { get; set; }
        public Nullable<int> CaseStatusChild { get; set; }
        public string CaseStatusname { get; set; }
        public string CaseStatusChildName { get; set; }
        public string CaseStatusComment { get; set; }
        public Nullable<System.DateTime> ReminderDateTime { get; set; }
        public Nullable<double> PTPAMT { get; set; }
        public Nullable<int> PTPCount { get; set; }
        public Nullable<int> PTPBroken { get; set; }
        public Nullable<int> PTPkept { get; set; }
        public Nullable<int> LastCaseStatusID { get; set; }
        public Nullable<int> LastCasStatusChldID { get; set; }
        public string LastCasStatusname { get; set; }
        public string LastCasStatusChldNam { get; set; }
        public string LastCasStatusComment { get; set; }
        public Nullable<int> MidCasStatusID { get; set; }
        public Nullable<int> MidCasStatusChld { get; set; }
        public string MidCasStatusnam { get; set; }
        public string MidCasStatusChldName { get; set; }
        public string MidCasStatusComment { get; set; }
        public Nullable<int> CYCLEDAY { get; set; }
        public Nullable<int> BucketPrev { get; set; }
        public string BucketPrevName { get; set; }
        public Nullable<int> BucketID { get; set; }
        public string BucketName { get; set; }
        public Nullable<int> Lookup01 { get; set; }
        public string Lookup01Name { get; set; }
        public Nullable<int> Lookup02 { get; set; }
        public string Lookup02Name { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string ProductName { get; set; }
        public string TxtField01 { get; set; }
        public string TxtField02 { get; set; }
        public string TxtField03 { get; set; }
        public string TxtField04 { get; set; }
        public string TxtField05 { get; set; }
        public string TxtField06 { get; set; }
        public string TxtField07 { get; set; }
        public Nullable<bool> CheckBoxField02 { get; set; }
        public Nullable<bool> CheckBoxField01 { get; set; }
        public Nullable<bool> CheckBoxField03 { get; set; }
        public Nullable<double> AMT01 { get; set; }
        public Nullable<double> AMT02 { get; set; }
        public Nullable<double> AMT03 { get; set; }
        public Nullable<double> AMT04 { get; set; }
        public Nullable<double> AMT05 { get; set; }
        public Nullable<double> AMT06 { get; set; }
        public Nullable<double> AMT07 { get; set; }
        public Nullable<double> AMT08 { get; set; }
        public Nullable<double> AMT09 { get; set; }
        public Nullable<double> AMT10 { get; set; }
        public Nullable<System.DateTime> Date01 { get; set; }
        public Nullable<System.DateTime> Date02 { get; set; }
        public Nullable<System.DateTime> Date03 { get; set; }
        public Nullable<System.DateTime> Date04 { get; set; }
        public Nullable<double> PrincipleAmount { get; set; }
        public Nullable<double> MaturityAmount { get; set; }
        public Nullable<double> OutstandingAMT { get; set; }
        public Nullable<double> OutStandingToday { get; set; }
        public Nullable<double> InstallmentAmount { get; set; }
        public double ClaimAmount { get; set; }
        public Nullable<double> FinanceCharge { get; set; }
        public Nullable<double> WriteOffAMT { get; set; }
        public Nullable<double> CreditLimit { get; set; }
        public Nullable<double> LastPaymentAMTUpload { get; set; }
        public Nullable<double> TotalLifeCollactedAMT { get; set; }
        public string CaseAccountNo { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<int> OverDueDays { get; set; }
        public Nullable<System.DateTime> FirstDisburementDate { get; set; }
        public Nullable<System.DateTime> MATURITY_DATE { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<System.DateTime> OverDueDate { get; set; }
        public Nullable<System.DateTime> LastPaymentDateUplod { get; set; }
        public Nullable<System.DateTime> ClosingDate { get; set; }
        public System.DateTime BookingDate { get; set; }
        public Nullable<System.DateTime> FirstLifeOverDueDate { get; set; }
        public string Remarks { get; set; }
        public string AgentID { get; set; }
        public Nullable<System.DateTime> AssignDate { get; set; }
        public Nullable<System.DateTime> LastPaymentDate { get; set; }
        public Nullable<double> LastCallactedAMT { get; set; }
        public Nullable<double> TotalCallactedAMT { get; set; }
        public Nullable<double> CommissionRate { get; set; }
        public string LastPaymentBy { get; set; }
        public Nullable<double> MonthlySalary { get; set; }
        public string AddressCode { get; set; }
        public string CaseAddess { get; set; }
        public string Phone01 { get; set; }
        public string Phone02 { get; set; }
        public string Phone03 { get; set; }
        public string Phone04 { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string MobileNo3 { get; set; }
        public string MobileNo4 { get; set; }
        public string POBox { get; set; }
        public string Other01 { get; set; }
        public string Other02 { get; set; }
        public string Other03 { get; set; }
        public string Other04 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Email01 { get; set; }
        public string Email02 { get; set; }
        public string Email03 { get; set; }
        public string Email04 { get; set; }
        public string FlatNo { get; set; }
        public string BuildingNo { get; set; }
        public string RoadName { get; set; }
        public string RoadNo { get; set; }
        public string BlockNo { get; set; }
        public string BlockName { get; set; }



        //------
        public double Balance
        {
            get
            {
                if (!FinanceCharge.HasValue)
                    FinanceCharge = 0;
                if (!WriteOffAMT.HasValue)
                    WriteOffAMT = 0;
                double tempClaimAmount = ClaimAmount + FinanceCharge.Value;
                tempClaimAmount = tempClaimAmount - WriteOffAMT.Value;
                return this.TotalCallactedAMT.HasValue ? tempClaimAmount - TotalCallactedAMT.Value : tempClaimAmount;
            }
        }
    }
}
