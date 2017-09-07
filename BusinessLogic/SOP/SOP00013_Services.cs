using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
using System.Globalization;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP00013Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00013Repository _SOP00013Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00013Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00013Repository = new SOP00013Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00013> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP00013> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00013Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00013Repository.GetWhereListWithPaging("SOP00013", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP00013> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("StatementCycleID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("StatementCycleName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00013> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00013Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00013Repository.GetWhereListWithPaging("SOP00013", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP00013> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<SOP00013> L = null;
            var tasks = _SOP00013Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<SOP00013> result = tasks;
            return result;
        }
        public DataCollection<SOP00013> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00013> L = null;
            var tasks = _SOP00013Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                , xx => xx.StatementCycleID, null);
            DataCollection<SOP00013> result = tasks;
            return result;
        }

        public DataCollection<SOP00013> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00013> result = null;
            var tasks = _SOP00013Repository.GetListWithPaging(x => x.StatementCycleID.ToString().Contains(SearchTerm) ||
                x.StatementCycleName.Contains(SearchTerm)
                , PageSize, Page, ss => new { ss.StatementCycleID }, xx => xx.StatementCycleID, null);
            result = tasks;
            return result;
        }
        public DataCollection<SOP00013> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00013> result = null;
            var tasks = _SOP00013Repository.GetListWithPaging(PageSize, Page, ss => new { ss.StatementCycleID });
            result = tasks;
            return result;
        }


        public DataCollection<SOP00013> GetAllSOP00013(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00013> L = null;
            var tasks = _SOP00013Repository.GetListWithPaging(x => x.StatementCycleName.Contains(SearchTerm), PageSize, Page, ss => ss.StatementCycleID, null);

            DataCollection<SOP00013> result = tasks;
            return result;
        }
        public DataCollection<SOP00013> GetByStatementCycleID(int StatementCycleID, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00013> L = null;
            var tasks = _SOP00013Repository.GetListWithPaging(x => x.StatementCycleID == StatementCycleID, PageSize, Page, ss => ss.StatementCycleID, null);

            DataCollection<SOP00013> result = tasks;
            return result;
        }

        public IList<SOP00013> GetAll()
        {
            var tasks = _SOP00013Repository.GetAll();
            IList<SOP00013> result = tasks;
            return result;
        }
        public SOP00013 GetSingle(int StatementCycleID)
        {
            var tasks = _SOP00013Repository.GetSingle(x => x.StatementCycleID == StatementCycleID);
            return tasks;
        }

        public KaizenResult AddSOP00013(SOP00013 newTask)
        {
            var result = _SOP00013Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP00013(IList<SOP00013> newTask)
        {
            var result = _SOP00013Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP00013 newTask)
        {
            var result = _SOP00013Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP00013> newTask)
        {
            var result = _SOP00013Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP00013 newTask)
        {
            var result = _SOP00013Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP00013> newTask)
        {
            var result = _SOP00013Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
