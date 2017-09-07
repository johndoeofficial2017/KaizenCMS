using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00005Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00005Repository _GL00005RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00005Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00005RepositoryDataRepository = new GL00005Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<GL00005> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00005> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00005RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00005RepositoryDataRepository.GetWhereListWithPaging("GL00005", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00005> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<GL00005> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _GL00005RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _GL00005RepositoryDataRepository.GetWhereListWithPaging("GL00005", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<GL00005> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<GL00005> L = null;
            var tasks = _GL00005RepositoryDataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<GL00005> result = tasks;
            return result;
        }
        public DataCollection<GL00005> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<GL00005> result = null;
            var tasks = _GL00005RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<GL00005> GetAll()
        {
            var tasks = _GL00005RepositoryDataRepository.GetAll();
            IList<GL00005> result = tasks;
            return result;
        }
        public DataCollection<GL00005> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<GL00005> L = null;
            var tasks = _GL00005RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<GL00005> result = tasks;
            return result;
        }
        public DataCollection<GL00005> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {

            DataCollection<GL00005> result = null;
            var tasks = _GL00005RepositoryDataRepository.GetListWithPaging(x => x.DocumentTypeID.ToString().Contains(SearchTerm) ||
                x.DocumentTypeName.Contains(SearchTerm)
                , PageSize, Page, ss => new { ss.DocumentTypeID });
            result = tasks;
            return result;
        }
        public DataCollection<GL00005> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00005> result = null;
            var tasks = _GL00005RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.DocumentTypeID });
            result = tasks;
            return result;
        }
        public GL00005 GetSingle(int DocumentTypeID)
        {
            var tasks = _GL00005RepositoryDataRepository.GetSingle(x => x.DocumentTypeID == DocumentTypeID);
            return tasks;
        }

        public KaizenResult AddGL00005(GL00005 newTask)
        {
            var result=_GL00005RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00005(IList<GL00005> newTask)
        {
            var result = _GL00005RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(GL00005 newTask)
        {
            var result = _GL00005RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00005> newTask)
        {
            var result = _GL00005RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00005 newTask)
        {
            var result = _GL00005RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00005> newTask)
        {
            var result = _GL00005RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
