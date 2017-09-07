using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00300Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00300Repository _GL00300RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00300Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00300RepositoryDataRepository = new GL00300Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00300> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00300> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00300RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00300RepositoryDataRepository.GetWhereListWithPaging("GL00300", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00300> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<GL00300> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _GL00300RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _GL00300RepositoryDataRepository.GetWhereListWithPaging("GL00300", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }


        public DataCollection<GL00300> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<GL00300> L = null;
            var tasks = _GL00300RepositoryDataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<GL00300> result = tasks;
            return result;
        }
        public DataCollection<GL00300> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<GL00300> result = null;
            var tasks = _GL00300RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<GL00300> GetAll()
        {
            var tasks = _GL00300RepositoryDataRepository.GetAll();
            IList<GL00300> result = tasks;
            return result;
        }
        public DataCollection<GL00300> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<GL00300> L = null;
            var tasks = _GL00300RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<GL00300> result = tasks;
            return result;
        }
        public DataCollection<GL00300> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {

            DataCollection<GL00300> result = null;
            var tasks = _GL00300RepositoryDataRepository.GetListWithPaging(x => x.BudgetID.Contains(SearchTerm) ||
                x.BudgetName.Contains(SearchTerm)
                , PageSize, Page, ss => new { ss.BudgetID });
            result = tasks;
            return result;
        }
        public DataCollection<GL00300> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00300> result = null;
            var tasks = _GL00300RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.BudgetID });
            result = tasks;
            return result;
        }
        public GL00300 GetSingle(string BudgetID)
        {
            var tasks = _GL00300RepositoryDataRepository.GetSingle(x => x.BudgetID == BudgetID);
            return tasks;
        }

        public KaizenResult AddGL00300(GL00300 newTask)
        {
            var result=_GL00300RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00300(IList<GL00300> newTask)
        {
            var result = _GL00300RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(GL00300 newTask)
        {
            var result = _GL00300RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00300> newTask)
        {
            var result = _GL00300RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(GL00300 newTask)
        {
            var result = _GL00300RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00300> newTask)
        {
            var result = _GL00300RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
