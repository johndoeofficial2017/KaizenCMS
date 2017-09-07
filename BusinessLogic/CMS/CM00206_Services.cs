using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00206Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00206Repository _CM00206RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00206Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00206RepositoryDataRepository = new CM00206Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00206> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("AssignHistoryID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TrxTypeName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00206Repository rep = new CM00206Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00206> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00206", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00206> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00206> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00206RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00206RepositoryDataRepository.GetWhereListWithPaging("CM00206", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00206> GetAllViewBYSQLQuery(string Filters,
            string Searchcritaria, string CaseRef,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            DataCollection<CM00206> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00206RepositoryDataRepository.GetListWithPaging(ss => ss.CaseRef.Trim() == CaseRef.Trim(),
                    PageSize, Page, ss => Member);
            }
            else
            {
                SeachStr += " and CaseRef='" + CaseRef.Trim() + "'";
                result = _CM00206RepositoryDataRepository.GetWhereListWithPaging("CM00206", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<CM00206> GetWhereListWithPaging(string sqlenquiry, int PageSize, int Page)
        {
            var tasks = _CM00206RepositoryDataRepository.GetWhereListWithPaging("CM00206", PageSize, Page, sqlenquiry, "AssigmentID", true);
            DataCollection<CM00206> result = tasks;
            return result;
        }

        public DataCollection<CM00206> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM00206RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00206> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM00206RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00206> result = tasks;
                return result;
            }

        }
       
        public IList<CM00206> GetAll()
        {
            var tasks = _CM00206RepositoryDataRepository.GetAll();
            IList<CM00206> result = tasks;
            return result;
        }

        public CM00206 GetSingle(string AssignHistoryID)
        {
            var tasks = _CM00206RepositoryDataRepository.GetSingle(x => x.AssigmentID == AssignHistoryID);
            return tasks;
        }

        public KaizenResult AddCM00206(CM00206 newTask)
        {
            KaizenResult result = _CM00206RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00206(IList<CM00206> newTask)
        {
            KaizenResult result = _CM00206RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(CM00206 newTask)
        {
            KaizenResult result = _CM00206RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(string AssignHistoryID)
        {
            KaizenResult result = _CM00206RepositoryDataRepository.RemoveKaizenObject(xx => xx.AssigmentID == AssignHistoryID);
            return result;
        }
    }
}
