using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
namespace Kaizen.BusinessLogic.CMS
{
    public class CM00109Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00109Repository _CM00109Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00109Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00109Repository = new CM00109Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00109> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ConditionID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AgentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldValue", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldCondition", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldOperatorAnd", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00109Repository rep = new CM00109Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00109> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00109", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00109> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00109> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00109Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00109Repository.GetWhereListWithPaging("CM00109", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00109> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string AgentID,
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
                    SeachStr += Help.GetFilter("ConditionID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AgentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldValue", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldCondition", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldOperatorAnd", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00109> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00109Repository.GetListWithPaging(ss => ss.AgentID.Trim() == AgentID.Trim(), PageSize, Page, ss => Member);
            else
                result = _CM00109Repository.GetWhereListWithPaging("CM00109", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00109> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00109> L = null;
            var tasks = _CM00109Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00109> result = tasks;
            return result;
        }
        //public DataCollection<CM00109> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        //{
        //    DataCollection<CM00109> result = null;
        //    var tasks = _CM00109Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ConditionID });
        //    result = tasks;
        //    return result;
        //}
        public IList<CM00109> GetAll()
        {
            var tasks = _CM00109Repository.GetAll();
            IList<CM00109> result = tasks;
            return result;
        }
        public IList<CM00109> GetByAgentID(string AgentID)
        {
            var tasks = _CM00109Repository.GetAll(ss => ss.AgentID.Trim() == AgentID.Trim());
            IList<CM00109> result = tasks;
            return result;
        }

        //public CM00109 GetSingle(int ConditionID)
        //{
        //    var tasks = _CM00109Repository.GetSingle(x => x.AgentID == ConditionID);
        //    return tasks;
        //}

        public KaizenResult AddCM00109(CM00109 newTask)
        {
            var result = _CM00109Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00109(IList<CM00109> newTask)
        {
            var result = _CM00109Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00109 newTask)
        {
            var result = _CM00109Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00109> newTask)
        {
            var result = _CM00109Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00109 newTask)
        {
            var result = _CM00109Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00109> newTask)
        {
            var result = _CM00109Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}

