using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00700Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00700Repository _CM00700Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00700Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
             UserContext = _UserContext;
            _CM00700Repository = new CM00700Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
        }
        #endregion
        public DataCollection<CM00700> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00700> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00700Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00700Repository.GetWhereListWithPaging("CM00700", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00700> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            else
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = " and ";
                if (FieldID == "-1")
                {
                    SeachStr += Help.GetFilter("CaseStatusID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseStatusname", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00700> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00700Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00700Repository.GetWhereListWithPaging("CM00700", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<CM00700> GetAllViewBYSQLQuery(string Filters,
            int PageSize, int Page, string Member, bool IsAscending, int? CaseStatusID)
        {
            string SeachStr = string.Empty;
            if (!string.IsNullOrEmpty(Filters))
                SeachStr = Filters;

            DataCollection<CM00700> result = null;
            if (CaseStatusID != null)
            {
                result = _CM00700Repository.GetListWithPaging(xx => xx.CaseStatusParent == null || xx.CaseStatusParent == CaseStatusID, PageSize, Page, ss => Member);
            }
            else
            {
                result = _CM00700Repository.GetListWithPaging(xx => xx.CaseStatusParent == null, PageSize, Page, s => Member);
            }
            return result;
        }


        public DataCollection<CM00700> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00700> L = null;
            var tasks = _CM00700Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00700> result = tasks;
            return result;
        }
        public DataCollection<CM00700> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00700> result = null;
            var tasks = _CM00700Repository.GetListWithPaging(PageSize, Page, ss => new { ss.CaseStatusID });
            result = tasks;
            return result;
        }

        public IList<CM00700> GetAll()
        {
            var tasks = _CM00700Repository.GetAll();
            IList<CM00700> result = tasks;
            return result;
        }
        public IList<CM00700> GetAllStatusHasScripts()
        {
            var tasks = _CM00700Repository.GetAll(x => x.IsScripting == true);
            IList<CM00700> result = tasks;
            return result;
        }
        public IList<CM00700> GetAllStatusHasTasks()
        {
            var tasks = _CM00700Repository.GetAll(x => x.IsTaskList == true);
            IList<CM00700> result = tasks;
            return result;
        }
        public IList<CM00700> StatusParentDropDownList()
        {
            var tasks = _CM00700Repository.GetAll(x => x.CaseStatusParent == null);
            IList<CM00700> result = tasks;
            return result;
        }
        public IList<CM00700> GetAllStatusHasChilds()
        {
            var tasks = _CM00700Repository.GetAll(x => x.IsHasChild == true);
            IList<CM00700> result = tasks;
            return result;
        }
        public IList<CM00700> GetStatusChilds(int CaseStatusParent)
        {
            var tasks = _CM00700Repository.GetAll(xx => xx.CaseStatusParent == CaseStatusParent);
            IList<CM00700> result = tasks;
            return result;
        }
        public IList<CM00060> GetWorkableFields(int CaseStatusID)
        {
            CM00060Repository rep = new CM00060Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(xx => xx.CaseStatusID == CaseStatusID && xx.FieldTypeID != 5);
            IList<CM00060> result = tasks;
            return result;
        }
        public IList<CM00060> GetAllLookupFields(int CaseStatusID)
        {
            CM00060Repository rep = new CM00060Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(xx => xx.CaseStatusID == CaseStatusID && xx.FieldTypeID == 1);
            IList<CM00060> result = tasks;
            return result;
        }
        public IList<CM00062> GetViewsByCaseStatusID(int CaseStatusID)
        {
            CM00062Repository rep = new CM00062Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(xx => xx.CaseStatusID == CaseStatusID);
            IList<CM00062> result = tasks;
            return result;
        }
        public System.Data.DataTable GetStatusFieldsDT(int ViewID)
        {
            CM00060Repository rep = new CM00060Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            //var tasks = rep.GetAll(xx => xx.CaseStatusID == CaseStatusID);
            string sql = "select * from CM00063 where ViewID=" + ViewID.ToString();
            System.Data.DataTable dt = rep.ExecuteSqlCommandToDataTable(sql);
            System.Data.DataRow workRow = dt.NewRow();
            workRow["ViewID"] = ViewID.ToString();
            workRow["CaseStatusID"] = dt.Rows[0]["CaseStatusID"].ToString();
            workRow["FieldCode"] = "CaseRef";
            workRow["FieldOrder"] = "-1";
            workRow["FieldTypeID"] = "-1";
            //workRow["FieldName"] = "CaseRef";
            dt.Rows.Add(workRow);
            System.Data.DataRow workRow2 = dt.NewRow();
            workRow2["ViewID"] = ViewID.ToString();
            workRow2["CaseStatusID"] = dt.Rows[0]["CaseStatusID"].ToString();
            workRow2["FieldCode"] = "ClaimAmount";
            workRow2["FieldOrder"] = "-2";
            workRow2["FieldTypeID"] = "-2";
            //workRow2["FieldName"] = "ClaimAmount";
            dt.Rows.Add(workRow2);
            System.Data.DataRow workRow3 = dt.NewRow();
            workRow3["ViewID"] = ViewID.ToString();
            workRow3["CaseStatusID"] = dt.Rows[0]["CaseStatusID"].ToString();
            workRow3["FieldCode"] = "OutstandingAMT";
            workRow3["FieldOrder"] = "-3";
            workRow3["FieldTypeID"] = "-3";
            dt.Rows.Add(workRow3);
            System.Data.DataRow workRow4 = dt.NewRow();
            workRow4["ViewID"] = ViewID.ToString();
            workRow4["CaseStatusID"] = dt.Rows[0]["CaseStatusID"].ToString();
            workRow4["FieldCode"] = "TotalCallactedAMT";
            workRow4["FieldOrder"] = "-4";
            workRow4["FieldTypeID"] = "-4";
            dt.Rows.Add(workRow4);
            return dt;
        }
        public System.Data.DataTable GetStatusFieldsData(int CaseStatusID)
        {
            CM00060Repository rep = new CM00060Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            //var tasks = rep.GetAll(xx => xx.CaseStatusID == CaseStatusID);
            string sql = "select * from CM00700_" + CaseStatusID.ToString();
            System.Data.DataTable dt = rep.ExecuteSqlCommandToDataTable(sql);
            //IList<CM00060> result = tasks;
            return dt;
        }
        public IList<CM00061> GetAllLookup(int CaseStatusID)
        {
            CM00061Repository rep = new CM00061Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(xx => xx.CaseStatusID == CaseStatusID);
            IList<CM00061> result = tasks;
            return result;
        }
        public IList<CM00052> GetAllLookup02(int CaseStatusID)
        {
            CM00052Repository rep = new CM00052Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(xx => xx.CaseStatusID == CaseStatusID);
            IList<CM00052> result = tasks;
            return result;
        }
        //public IList<CM00053> GetAllLookup03(int CaseStatusID)
        //{
        //    CM00053Repository rep = new CM00053Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
        //    var tasks = rep.GetAll(xx => xx.CaseStatusID == CaseStatusID);
        //    IList<CM00053> result = tasks;
        //    return result;
        //}
        //public IList<CM00054> GetAllLookup04(int CaseStatusID)
        //{
        //    CM00054Repository rep = new CM00054Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
        //    var tasks = rep.GetAll(xx => xx.CaseStatusID == CaseStatusID);
        //    IList<CM00054> result = tasks;
        //    return result;
        //}
        //public IList<CM00055> GetAllLookup05(int ProductID)
        //{
        //    CM00055Repository rep = new CM00055Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
        //    var tasks = rep.GetAll(xx => xx.ProductID == ProductID);
        //    IList<CM00055> result = tasks;
        //    return result;
        //}
        //public IList<CM00056> GetAllLookup06(int CaseStatusID)
        //{
        //    CM00056Repository rep = new CM00056Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
        //    var tasks = rep.GetAll(xx => xx.CaseStatusID == CaseStatusID);
        //    IList<CM00056> result = tasks;
        //    return result;
        //}
        public KaizenResult DeleteStatusScript(CM00026 newTask)
        {
            CM00026Services srv = new CM00026Services(this.UserContext);
            return srv.Delete(newTask);
        }
        public KaizenResult DeleteChildStatus(IList<CM00700> newTask, int CaseStatusID)
        {
            foreach (var item in newTask)
            {
                item.CaseStatusParent = null;
            }
            KaizenResult result = _CM00700Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult UpdateChildStatus(IList<CM00700> newTask, int CaseStatusID)
        {
            foreach (var item in newTask)
            {
                item.CaseStatusParent = CaseStatusID;
            }
            KaizenResult result = _CM00700Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public CM00700 GetSingle(int CaseStatusID)
        {
            var tasks = _CM00700Repository.GetSingle(x => x.CaseStatusID == CaseStatusID);
            return tasks;
        }

        public KaizenResult AddCM00700(CM00700 newTask)
        {
            var result = _CM00700Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00700(IList<CM00700> newTask)
        {
            var result = _CM00700Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00700 newTask)
        {
            var result = _CM00700Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00700> newTask)
        {
            var result = _CM00700Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00700 newTask)
        {
            var result = _CM00700Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00700> newTask)
        {
            var result = _CM00700Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        //--------------------------------------- CM00052
        public KaizenResult AddCM00052(CM00052 newTask)
        {
            CM00052Repository rep = new CM00052Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult DeleteCM00052(CM00052 newTask)
        {
            CM00052Repository rep = new CM00052Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(newTask);
            return result;
        }


        public IList<CM00051> GetStatusByUser(string userName)
        {
            CM00051Repository rep = new CM00051Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(xx => xx.UserName == userName);
            IList<CM00051> result = tasks;
            return result;
        }

        public KaizenResult DeleteStatusByUser(CM00051 statusUser)
        {
            CM00051Repository rep = new CM00051Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var result = rep.DeleteKaizenObject(statusUser);
            return result;
        }

        public KaizenResult AddCM00051(CM00051 newStausUser)
        {
            CM00051Repository rep = new CM00051Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(newStausUser);
            return result;
        }

        #region Status Field
        public IList<CM00060> GetFieldsByCaseStatus(int caseStatusID)
        {
            CM00060Repository rep = new CM00060Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseStatusFields = rep.GetAll(xx => xx.CaseStatusID == caseStatusID);
            IList<CM00060> result = caseStatusFields;
            return result;
        }
        public KaizenResult AddCM00060(CM00060 caseStatusField)
        {
            CM00060Repository rep = new CM00060Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(caseStatusField);
            return result;
        }

        public KaizenResult Update(CM00060 caseStatusField)
        {
            CM00060Repository rep = new CM00060Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            KaizenResult result = rep.UpdateKaizenObject(caseStatusField);
            return result;
        }

        public KaizenResult Delete(CM00060 caseStatusField)
        {
            CM00060Repository rep = new CM00060Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(caseStatusField);
            return result;
        }
        #endregion

        #region Status View
        public IList<CM00062> GetViewsByCaseStatus(int CaseStatusID)
        {
            CM00062Repository rep = new CM00062Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseStatusFields = rep.GetAll(xx => xx.CaseStatusID == CaseStatusID);
            IList<CM00062> result = caseStatusFields;
            return result;
        }
        public KaizenResult Update(CM00062 viewStaus)
        {
            CM00062Repository rep = new CM00062Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            KaizenResult result = rep.UpdateKaizenObject(viewStaus);
            return result;
        }
        public KaizenResult Delete(CM00062 viewStaus)
        {
            CM00062Repository rep = new CM00062Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(viewStaus);
            return result;
        }
        public KaizenResult AddCM00062(CM00062 caseStatusView)
        {
            CM00062Repository rep = new CM00062Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(caseStatusView);
            return result;
        }
        #endregion

        #region Status View Roles
        public IList<CM00062> GetViews()
        {
            CM00062Repository rep = new CM00062Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseStatusFields = rep.GetAll();
            IList<CM00062> result = caseStatusFields;
            return result;
        }
        public IList<CM00065> GetRolesByView(int viewID)
        {
            CM00065Repository rep = new CM00065Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var rolesByViews = rep.GetAll(xx => xx.ViewID == viewID);
            IList<CM00065> result = rolesByViews;
            return result;
        }
        public KaizenResult AddCM00065(CM00065 viewRole)
        {
            CM00065Repository rep = new CM00065Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(viewRole);
            return result;
        }
        public KaizenResult DeleteCM00065(CM00065 viewRole)
        {
            CM00065Repository rep = new CM00065Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(viewRole);
            return result;
        }
        #endregion

        #region Status View Users
        public KaizenResult AddCM00064(CM00064 viewUser)
        {
            CM00064Repository rep = new CM00064Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(viewUser);
            return result;
        }
        public KaizenResult DeleteCaseStatusViewUser(CM00064 caseStatusViewUser)
        {
            CM00064Repository rep = new CM00064Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var result = rep.DeleteKaizenObject(caseStatusViewUser);
            return result;
        }
        public IList<CM00064> GetCaseStatusViewsByUser(string userName)
        {
            CM00064Repository rep = new CM00064Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(xx => xx.UserName == userName);
            IList<CM00064> result = tasks;
            return result;
        }
        #endregion

        #region Status Views Fields
        public IList<CM00063> GetAllFieldsByView(int viewID)
        {
            CM00063Repository rep = new CM00063Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var viewsFields = rep.GetAll(xx => xx.ViewID == viewID);
            IList<CM00063> result = viewsFields;
            return result;
        }
        public IList<CM00060> GetAllFieldsByCaseStatus(int CaseStatusID)
        {
            CM00060Repository rep = new CM00060Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseTypeViewsFields = rep.GetAll(xx => xx.CaseStatusID == CaseStatusID);
            IList<CM00060> result = caseTypeViewsFields;
            return result;
        }
        public KaizenResult AddCM00063(CM00063 viewField)
        {
            CM00063Repository rep = new CM00063Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(viewField);
            return result;
        }
        public KaizenResult UpdateCM00063(IList<CM00063> viewFieldList)
        {
            CM00063Repository rep = new CM00063Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(viewFieldList.ToArray());
            return result;
        }
        public KaizenResult DeleteCM00063(CM00063 viewField)
        {
            CM00063Repository rep = new CM00063Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(viewField);
            return result;
        }
        #endregion

        #region Status Lookups
        public IList<CM00060> GetFieldCodesByCaseStatus(int CaseStatusID)
        {
            CM00060Repository rep = new CM00060Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var fieldsByStatus = rep.GetAll(xx => xx.CaseStatusID == CaseStatusID);
            IList<CM00060> result = fieldsByStatus;
            return result;
        }
        public IList<CM00061> GetLookups(int CaseStatusID,string fieldCode)
        {
            CM00061Repository rep = new CM00061Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var lookups = rep.GetAll(xx => xx.CaseStatusID == CaseStatusID && xx.FieldCode==fieldCode);
            IList<CM00061> result = lookups;
            return result;
        }
        public KaizenResult AddCM00061(CM00061 lookup)
        {
            CM00061Repository rep = new CM00061Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(lookup);
            return result;
        }
        public KaizenResult UpdateCM00061(CM00061 lookup)
        {
            CM00061Repository rep = new CM00061Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(lookup);
            return result;
        }
        public KaizenResult DeleteCM00061(CM00061 lookup)
        {
            CM00061Repository rep = new CM00061Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(lookup);
            return result;
        }
        #endregion
    }
}
