using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00214Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00214Repository _CM00214RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00214Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00214RepositoryDataRepository = new CM00214Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00214> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ActionID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TaskTitle", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00214Repository rep = new CM00214Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00214> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00214", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00214> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00214> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00214RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00214RepositoryDataRepository.GetWhereListWithPaging("CM00214", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00214> GetAllViewBYSQLQuery(string Filters,
    string Searchcritaria, int TaskID,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            DataCollection<CM00214> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00214RepositoryDataRepository.GetListWithPaging(ss => ss.TaskID == TaskID,
                    PageSize, Page, ss => Member);
            }
            else
            {
                SeachStr += " and TaskID='" + TaskID + "'";
                result = _CM00214RepositoryDataRepository.GetWhereListWithPaging("CM00214", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<CM00214> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM00214RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00214> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM00214RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00214> result = tasks;
                return result;
            }

        }
        public DataCollection<CM00214> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00214> result = null;
            var tasks = _CM00214RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.ActionID });
            result = tasks;
            return result;
        }
        public IList<CM00214> GetAll()
        {
            var tasks = _CM00214RepositoryDataRepository.GetAll();
            IList<CM00214> result = tasks;
            return result;
        }
        public IList<CM00214> GetAllByTaskID(int TaskID)
        {
            var tasks = _CM00214RepositoryDataRepository.GetAll(xx => xx.TaskID == TaskID);
            IList<CM00214> result = tasks;
            return result;
        }

        public CM00214 GetSingle(int ActionID)
        {
            var tasks = _CM00214RepositoryDataRepository.GetSingle(x => x.ActionID == ActionID);
            return tasks;
        }

        public KaizenResult AddCM00214(CM00214 newTask)
        {
            KaizenResult result = _CM00214RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00214(IList<CM00214> newTask)
        {
            KaizenResult result = _CM00214RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(CM00214 newTask)
        {
            KaizenResult result = _CM00214RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(int ActionID)
        {
            KaizenResult result = _CM00214RepositoryDataRepository.RemoveKaizenObject(xx => xx.ActionID == ActionID);
            return result;
        }
    }
}
