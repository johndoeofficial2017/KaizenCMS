using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using Kaizen.BusinessLogic.Admin;
using Kaizen.Admin;
using Kendo.Mvc;
using System.Linq;
using Kaizen.BusinessLogic.CMS;
using Kaizen.BusinessLogic.Configuration;
using System.Web;
using System.IO;

namespace UIServer.Areas.Tools.Controllers
{
    public class SysTaskController : Controller
    {
        // GET: SysTask
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);

            CM00004Services CM00004_serv = new CM00004Services(KaizenUser);
            CM00006Services CM00006_serv = new CM00006Services(KaizenUser);
            ViewData["TaskTypes"] = CM00004_serv.GetAll();
            ViewData["TaskPriorities"] = CM00006_serv.GetAll();

            return View();
        }
        public ActionResult Create(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult TodayTasks(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            return View();
        }
        public ActionResult TasksList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            return View();
        }

        public ActionResult TaskView(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            return View();
        }


        #region Grid Data
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<Sys00100> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new Sys00100
               {
                   TaskID = o.TaskID,
                   TaskTypeID = o.TaskTypeID,
                   PriorityID = o.PriorityID,
                   TaskTitle = o.TaskTitle,
                   TaskDescription = o.TaskDescription,
                   TaskStartDate = o.TaskStartDate,
                   TaskEndDate = o.TaskEndDate,
                   UserAsginFrom = o.UserAsginFrom,
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
                    Data = new List<Sys00100>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetListGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            Sys00100Services invService = new Sys00100Services(KaizenUser);
            DataCollection<Sys00100> L = new DataCollection<Sys00100>();
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

            L = invService.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            Sys00100Services serv = new Sys00100Services(KaizenUser);
            DataCollection<Sys00100> L = new DataCollection<Sys00100>();
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

            L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #endregion

        // Danish COntroller 
        // 26 March 2016 
        // Calling from Main Controller
        public ActionResult GetSingle(string KaizenPublicKey, int TaskID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Sys00100Services service = new Sys00100Services(KaizenUser);
            Sys00101Services service00101 = new Sys00101Services(KaizenUser);
            Sys00100 L = service.GetSingle(TaskID);
            L.Sys00101 = service00101.GetByTaskID(TaskID);

            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllAgentTasks(string KaizenPublicKey, string AgentID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            Sys00100Services service = new Sys00100Services(KaizenUser);
            IList<Sys00100> List = service.GetTasksAssignedToUser(AgentID);
            List<Sys00100> Sys00100List = new List<Sys00100>();
            foreach (var item in List)
            {
                Sys00100 obj = new Sys00100()
                {
                    TaskID = item.TaskID,
                    TaskTypeID = item.TaskTypeID,
                    PriorityID = item.PriorityID,
                    TaskTitle = item.TaskTitle,
                    TaskDescription = item.TaskDescription,
                    TaskStartDate = item.TaskStartDate,
                    TaskEndDate = item.TaskEndDate,
                    UserAsginFrom = item.UserAsginFrom,
                    AssignDate = item.AssignDate,
                    TaskProgress = item.TaskProgress
                };
                Sys00100List.Add(obj);
            }
            JsonResult result = Json(Sys00100List);
            result.MaxJsonLength = int.MaxValue;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult GetTodayTasks(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            Sys00100Services service = new Sys00100Services(KaizenUser);
            IList<Sys00100> List = service.GetTodayTasksAssignedToUser(KaizenUser.UserName);
            List<Sys00100> Sys00100List = new List<Sys00100>();
            foreach (var item in List)
            {
                Sys00100 obj = new Sys00100()
                {
                    TaskID = item.TaskID,
                    TaskTypeID = item.TaskTypeID,
                    PriorityID = item.PriorityID,
                    TaskTitle = item.TaskTitle,
                    TaskDescription = item.TaskDescription,
                    TaskStartDate = item.TaskStartDate,
                    TaskEndDate = item.TaskEndDate,
                    UserAsginFrom = item.UserAsginFrom,
                    AssignDate = item.AssignDate,
                    TaskProgress = item.TaskProgress
                };
                Sys00100List.Add(obj);
            }
            JsonResult result = Json(Sys00100List);
            result.MaxJsonLength = int.MaxValue;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult SaveTaskAction(Sys00101 Sys00101, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            Sys00101Services service = new Sys00101Services(KaizenUser);
            Sys00101.UserName = KaizenUser.UserName;
            Sys00101.PhotoExtension = "";
            return Json(service.AddSys00101(Sys00101), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllTaskActions(string KaizenPublicKey, int TaskID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            Sys00101Services service = new Sys00101Services(KaizenUser);
            IList<Sys00101> List = service.GetByTaskID(TaskID);
            JsonResult result = Json(List);
            result.MaxJsonLength = int.MaxValue;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public ActionResult SaveData(Sys00100 Sys00100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Sys00100.UserAsginFrom = KaizenUser.UserName;
            Sys00100Services service = new Sys00100Services(KaizenUser);
            return Json(service.AddSys00100(Sys00100), JsonRequestBehavior.AllowGet);
        }

        #region Task Attachment
        public ActionResult SaveUploadFileTemp(IEnumerable<HttpPostedFileBase> TaskAttachemnt)
        {
            var DocumentID = "";
            string photoExtension = "";

            foreach (var file in TaskAttachemnt)
            {
                try
                {
                    photoExtension = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1);
                    DocumentID = Guid.NewGuid().ToString();
                    var filePath = DocumentID + "." + photoExtension;
                    var destinationPath = Path.Combine(Server.MapPath("~/TaskFiles"), filePath);
                    file.SaveAs(destinationPath);
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex, "SaveUploadFileTemp");
                }
            }

            return Json(new { DocumentID = DocumentID, PhotoExtension = photoExtension }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveUploadFiletemp(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/TaskFiles"), fileName);
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