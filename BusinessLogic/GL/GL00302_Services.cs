using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00302Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00302Repository _GL00302RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00302Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00302RepositoryDataRepository = new GL00302Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00302> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00302> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00302RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00302RepositoryDataRepository.GetWhereListWithPaging("GL00302", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00302> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<GL00302> L = null;
            var tasks = _GL00302RepositoryDataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<GL00302> result = tasks;
            return result;
        }
        public DataCollection<GL00302> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<GL00302> result = null;
            var tasks = _GL00302RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<GL00302> GetAll()
        {
            IList<GL00302> L = null;
            var tasks = _GL00302RepositoryDataRepository.GetAll();
            IList<GL00302> result = tasks;
            return result;
        }
        public DataCollection<GL00302> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TransactionID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ACTNUMBR", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BudgetID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("YearCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TransactionName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TrxDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TrxDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<GL00302> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _GL00302RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _GL00302RepositoryDataRepository.GetWhereListWithPaging("GL00302", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<GL00302> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {

            DataCollection<GL00302> result = null;
            var tasks = _GL00302RepositoryDataRepository.GetListWithPaging(x => x.BudgetID.Contains(SearchTerm) ||
                x.ACTNUMBR.Contains(SearchTerm) || x.AccountID.ToString().Contains(SearchTerm), PageSize, Page, ss => new { ss.BudgetID });
            result = tasks;
            return result;
        }
        public DataCollection<GL00302> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00302> result = null;
            var tasks = _GL00302RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.BudgetID });
            result = tasks;
            return result;
        }

        public GL00302 GetSingle(int TransactionID)
        {
            var tasks = _GL00302RepositoryDataRepository.GetSingle(x => x.TransactionID == TransactionID);
            return tasks;
        }

        public KaizenResult AddGL00302(GL00302 newTask)
        {
            var result=_GL00302RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00302(IList<GL00302> newTask)
        {
            var result = _GL00302RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00302 newTask)
        {
            var result = _GL00302RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00302> newTask)
        {
            var result = _GL00302RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00302 newTask)
        {
            var result = _GL00302RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00302> newTask)
        {
            var result = _GL00302RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
