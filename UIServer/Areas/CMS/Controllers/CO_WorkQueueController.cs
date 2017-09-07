using Kaizen.BusinessLogic.Configuration;
using Kaizen.BusinessLogic.CMS;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaizen.CMS;
using System.IO;

namespace UIServer.Areas.CMS.Controllers
{
    public class CO_WorkQueueController : Controller
    {
        // GET: CMS/CO_WorkQueue
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
        public ActionResult MainGrid(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        //public ActionResult PopUp(string KaizenPublicKey)
        //{
        //    Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
        //    if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
        //    return PartialView();
        //}
        public ActionResult OrgnizationChartPage(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00105> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00105
               {
                   AgentID = o.AgentID,
                   CalendarID = o.CalendarID,
                   SignatureExtension = o.SignatureExtension,
                   //UserLevelID = o.UserLevelID,
                   //EmployeeID = o.EmployeeID,
                   SupervisorID = o.SupervisorID,
                   SuffixID = o.SuffixID,
                   MidName = o.MidName,
                   FirstName = o.FirstName,
                   LastName = o.LastName,
                   AgentGroup = o.AgentGroup,
                   Inactive = o.Inactive,
                   EmailAddress = o.EmailAddress,
                   DirectPhon = o.DirectPhon,
                   PhotoExtension = o.PhotoExtension,
                   UserCode = o.UserCode
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00105>(),
                    Total = 0
                };
            }
            return result;
        }
        public JsonResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00105Services serv = new CM00105Services(KaizenUser);
            DataCollection<CM00105> L = new DataCollection<CM00105>();
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

            L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        public ActionResult GetListPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00105Services invService = new CM00105Services(KaizenUser);
            DataCollection<CM00105> L = new DataCollection<CM00105>();
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

            L = invService.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult SaveData(CM00105 CM00105, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            int startindex;
            if (!string.IsNullOrEmpty(CM00105.PhotoExtension))
            {
                string PhotoPath = @"\\Photo\\AgentPhotoTemp\\" + CM00105.PhotoExtension.Trim();
                startindex = CM00105.PhotoExtension.LastIndexOf('.');
                startindex += 1;
                CM00105.PhotoExtension = CM00105.PhotoExtension.Substring(startindex, CM00105.PhotoExtension.Length - startindex);
                if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                {
                    string Destination = Server.MapPath(@"\\Photo\\AgentPhoto\\" + CM00105.AgentID.Trim() + "." + CM00105.PhotoExtension);
                    if (System.IO.File.Exists(Destination))
                    {
                        System.IO.File.Delete(Destination);
                    }
                    System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                }
            }
            if (!string.IsNullOrEmpty(CM00105.SignatureExtension))
            {
                string PhotoPath = @"\\Photo\\AgentSignatureTemp\\" + CM00105.SignatureExtension.Trim();
                startindex = CM00105.SignatureExtension.LastIndexOf('.');
                startindex += 1;
                CM00105.SignatureExtension = CM00105.SignatureExtension.Substring(startindex, CM00105.SignatureExtension.Length - startindex);
                if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                {
                    string Destination = Server.MapPath(@"\\Photo\\AgentSignature\\" + CM00105.AgentID.Trim() + "." + CM00105.SignatureExtension);
                    if (System.IO.File.Exists(Destination))
                    {
                        System.IO.File.Delete(Destination);
                    }
                    System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                }
            }
            CM00105Services service = new CM00105Services(KaizenUser);
            return Json(service.AddCM00105(CM00105), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(CM00105 CM00105, string KaizenPublicKey, bool PhotoChanged, bool SignatureChanged)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            int startindex;
            if (PhotoChanged)
            {
                if (!string.IsNullOrEmpty(CM00105.PhotoExtension))
                {
                    string PhotoPath = @"\\Photo\\AgentPhotoTemp\\" + CM00105.PhotoExtension.Trim();
                    startindex = CM00105.PhotoExtension.LastIndexOf('.');
                    startindex += 1;
                    CM00105.PhotoExtension = CM00105.PhotoExtension.Substring(startindex, CM00105.PhotoExtension.Length - startindex);
                    if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                    {
                        string Destination = Server.MapPath(@"\\Photo\\AgentPhoto\\" + CM00105.AgentID.Trim() + "." +
                            CM00105.PhotoExtension);
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
                if (!string.IsNullOrEmpty(CM00105.SignatureExtension))
                {
                    string PhotoPath = @"\\Photo\\AgentSignatureTemp\\" + CM00105.SignatureExtension.Trim();
                    startindex = CM00105.SignatureExtension.LastIndexOf('.');
                    startindex += 1;
                    CM00105.SignatureExtension = CM00105.SignatureExtension.Substring(startindex, CM00105.SignatureExtension.Length - startindex);
                    if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                    {
                        string Destination = Server.MapPath(@"\\Photo\\AgentSignature\\" + CM00105.AgentID.Trim() + "." +
                            CM00105.SignatureExtension);
                        if (System.IO.File.Exists(Destination))
                        {
                            System.IO.File.Delete(Destination);
                        }
                        System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                    }
                }
            }
            CM00105Services service = new CM00105Services(KaizenUser);
            return Json(service.Update(CM00105), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(CM00105 CM00105, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00105Services service = new CM00105Services(KaizenUser);
            return Json(service.Delete(CM00105), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, string AgentID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00105Services service = new CM00105Services(KaizenUser);
            CM00105 L = service.GetSingle(AgentID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get List of Agents By Supervisor -- Used in CMS_Reminder
        /// By - Mahmoud Gamal
        /// </summary>
        /// <param name="KaizenPublicKey"></param>
        /// <param name="AgentID"></param>
        /// <returns></returns>
        public ActionResult GetMyListBySupervisor(string KaizenPublicKey, DateTime StartDate, DateTime EndDate)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00105Services service = new CM00105Services(KaizenUser);
            List<KCM00203_Result> o = service.GetAgentListBySupervisor(StartDate, EndDate);
            KCM00203_Result currentAgent = new KCM00203_Result();
            currentAgent.AgentID = KaizenUser.UserName;
            currentAgent.SupervisorID = KaizenUser.UserName;
            currentAgent.Level = 0;
            o.Add(currentAgent);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrgAgent(string KaizenPublicKey, string AgentGroup)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00105Services srv = new CM00105Services(KaizenUser);
            List<CM00105> defaultagent = new List<CM00105>();
            if (string.IsNullOrEmpty(AgentGroup))
            {
                defaultagent = srv.GetAll().ToList();
            }
            else
            {
                defaultagent = srv.GetAllByGroup(AgentGroup).ToList();
            }
            List<CM00105> result = new List<CM00105>();
            foreach (CM00105 o in defaultagent)
            {
                if (string.IsNullOrEmpty(o.SupervisorID))
                {
                    result.Add(o);
                    continue;
                }
                CM00105 parent = defaultagent.Find(ss => ss.AgentID.Trim() == o.SupervisorID.Trim());
                if (parent != null)
                {
                    if (parent.CM00105Children == null) parent.CM00105Children = new List<CM00105>();
                    parent.CM00105Children.Add(o);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveImageTemp(IEnumerable<HttpPostedFileBase> attachments)
        {
            var fileName = "";
            foreach (var file in attachments)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/Photo/AgentPhotoTemp"), fileName);
                file.SaveAs(destinationPath);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveImagetemp(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Photo/AgentPhotoTemp"), fileName);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }

        public ActionResult SaveImageSignatureTemp(IEnumerable<HttpPostedFileBase> SignatureAttachment)
        {
            var fileName = "";
            foreach (var file in SignatureAttachment)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/Photo/AgentSignatureTemp"), fileName);
                file.SaveAs(destinationPath);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveImageSignaturetemp(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Photo/AgentSignatureTemp"), fileName);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }
        public ActionResult GetAgentPeriods(int CalendarID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00007Services service = new CM00007Services(KaizenUser);
            return Json(service.GetByCalendar(CalendarID), JsonRequestBehavior.AllowGet);
        }
        #region Agent Client Target
        public ActionResult TargetByClient(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetAgentTargetByCLient(string AgentID, string ClientID, string YearCode, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00105Services service = new CM00105Services(KaizenUser);
            return Json(service.GetAgentTargetByCLient(ClientID, AgentID, YearCode), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveClientTarget(IList<CM00212> CM00212, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00212Services service = new CM00212Services(KaizenUser);
            return Json(service.AddCM00212(CM00212), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Agent Target
        public ActionResult TargetBYLayer(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetAgentTargetsInfo(string AgentID, string TargetID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00107Services service = new CM00107Services(KaizenUser);
            return Json(service.GetAgentTarget(AgentID, TargetID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveTargetInfo(IList<CM00107> CM00107, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00105Services service = new CM00105Services(KaizenUser);
            return Json(service.AddCM00107(CM00107), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Agent Cycle Target
        public ActionResult CycleTarget(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult GetCyclePeriodsInfo(int BucketID, string AgentID, string YearCode, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00215Services service = new CM00215Services(KaizenUser);
            return Json(service.GetCyclePeriodsInfo(BucketID, AgentID, YearCode), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAgentCyclePeriodTargetsInfo(string AgentID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM10110Services service = new CM10110Services(KaizenUser);
            return Json(service.GetByAgent(AgentID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SavePeriodCycleTargetInfo(IList<CM00215> CM10110, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00215Services service = new CM00215Services(KaizenUser);
            return Json(service.AddCM00215(CM10110), JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Agent Period Target
        public ActionResult TargetByPeriod(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetTargetByAgentID(string YearCode, string AgentID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM10110Services service = new CM10110Services(KaizenUser);
            return Json(service.GetAll(AgentID, YearCode), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SavePeriodTargetInfo(IList<CM10110> CM10110, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00105Services service = new CM00105Services(KaizenUser);
            return Json(service.AddCM10110(CM10110), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Agent Assignment Conditions
        public ActionResult AgentCondition(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        private DataSourceResult AgentConditionList([DataSourceRequest]DataSourceRequest request, DataCollection<CM00109> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00109
               {
                   //ConditionID = o.ConditionID,
                   //AgentID = o.AgentID,
                   //FieldName = o.FieldName,
                   //FieldCondition = o.FieldCondition,
                   //FieldValue = o.FieldValue,
                   //FieldOperatorAnd = o.FieldOperatorAnd
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
        public ActionResult GetConditionListGridWithAgent([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string AgentID, string Searchcritaria)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00109Services serv = new CM00109Services(KaizenUser);
            DataCollection<CM00109> L = new DataCollection<CM00109>();
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

            if (!string.IsNullOrEmpty(AgentID))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, AgentID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = AgentConditionList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAgentConditions(string AgentID, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00109Services service = new CM00109Services(KaizenUser);
            return Json(service.GetByAgentID(AgentID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveAgentCondition(CM00109 CM00109, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00109Services service = new CM00109Services(KaizenUser);
            return Json(service.AddCM00109(CM00109), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateAgentCondition(CM00109 CM00109, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00109Services service = new CM00109Services(KaizenUser);
            return Json(service.Update(CM00109), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteAgentCondition(CM00109 CM00109, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00109Services service = new CM00109Services(KaizenUser);
            return Json(service.Delete(CM00109), JsonRequestBehavior.AllowGet);
        }

        #endregion

        public string GetNextAgent(string CUSTCLAS, string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return "";
            if (KaizenPublicKey.Trim() == "undefined") return "";
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return "";
            if (!User.Identity.IsAuthenticated) return "";
            CM00105Services service = new CM00105Services(KaizenUser);
            return service.GetNextAgent(CUSTCLAS);
        }
    }
}