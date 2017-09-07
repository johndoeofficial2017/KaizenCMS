using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00213Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00213Repository _CM00213RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00213Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00213RepositoryDataRepository = new CM00213Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00213> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TaskID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TaskTitle", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00213Repository rep = new CM00213Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00213> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00213", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
         
        public DataCollection<CM00213> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00213> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00213RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00213RepositoryDataRepository.GetWhereListWithPaging("CM00213", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00213> GetAllViewBYSQLQuery(string Filters,
            string Searchcritaria, string CaseRef,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            DataCollection<CM00213> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00213RepositoryDataRepository.GetListWithPaging(ss => ss.CaseRef.Trim() == CaseRef.Trim(),
                    PageSize, Page, ss => Member);
            }
            else
            {
                SeachStr += " and CaseRef='" + CaseRef.Trim() + "'";
                result = _CM00213RepositoryDataRepository.GetWhereListWithPaging("CM00213", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }


        public DataCollection<CM00213> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM00213RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00213> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM00213RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00213> result = tasks;
                return result;
            }

        }
        public DataCollection<CM00213> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00213> result = null;
            var tasks = _CM00213RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.TaskID });
            result = tasks;
            return result;
        }
        public DataCollection<CM00213> GetListWithPagingByTable(string sqlenquiry , int PageSize, int Page, string Member, bool SortDirection)
        {
            DataCollection<CM00213> result = null;
            var tasks = _CM00213RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.TaskID });
            result = tasks;
            return result;
        }
        public IList<CM00213> GetAll()
        {
            var tasks = _CM00213RepositoryDataRepository.GetAll();
            IList<CM00213> result = tasks;
            return result;
        }
        public IList<CM00213> GetAllByAgent(string AgentID)
        {
            var tasks = _CM00213RepositoryDataRepository.GetAll(xx => xx.AgentID == AgentID);
            IList<CM00213> result = tasks;
            return result;
        }

        public CM00213 GetSingle(int TaskID)
        {
            var tasks = _CM00213RepositoryDataRepository.GetSingle(x => x.TaskID == TaskID);
            return tasks;
        }

        public KaizenResult AddCM00213(CM00213 newTask)
        {
            KaizenResult result = _CM00213RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00213(IList<CM00213> newTask)
        {
            KaizenResult result = _CM00213RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(CM00213 newTask)
        {
            KaizenResult result = _CM00213RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(int TaskID)
        {
            KaizenResult result = _CM00213RepositoryDataRepository.RemoveKaizenObject(xx => xx.TaskID == TaskID);
            return result;
        }
    }
}
