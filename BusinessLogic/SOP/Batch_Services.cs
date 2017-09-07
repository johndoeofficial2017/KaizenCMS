using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP00004Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00004Repository _SOP00004Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00004Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00004Repository = new SOP00004Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00004> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP00004> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00004Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00004Repository.GetWhereListWithPaging("SOP00004", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP00004> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("BatchID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BatchAmount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BatchDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BatchTRXcount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsTransactionDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PostingDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00004> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00004Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00004Repository.GetWhereListWithPaging("SOP00004", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<SOP00004> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<SOP00004> L = null;
            var tasks = _SOP00004Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<SOP00004> result = tasks;
            return result;
        }
        public DataCollection<SOP00004> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<SOP00004> result = null;
            var tasks = _SOP00004Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public DataCollection<SOP00004> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00004> L = null;
            var tasks = _SOP00004Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<SOP00004> result = tasks;
            return result;
        }
        public DataCollection<SOP00004> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00004> result = null;
            var tasks = _SOP00004Repository.GetListWithPaging(PageSize, Page, ss => new { ss.BatchID });
            result = tasks;
            return result;
        }
        public DataCollection<SOP00004> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00004> result = null;
            var tasks = _SOP00004Repository.GetListWithPaging(PageSize, Page, ss => new { ss.BatchID });
            result = tasks;
            return result;
        }

        public IList<SOP00004> GetAll()
        {
            var tasks = _SOP00004Repository.GetAll();
            IList<SOP00004> result = tasks;
            return result;
        }
        public SOP00004 GetSingle(string BatchID)
        {
            var tasks = _SOP00004Repository.GetSingle(x => x.BatchID == BatchID);
            return tasks;
        }

        public KaizenResult AddSOP00004(SOP00004 newTask)
        {
            var result = _SOP00004Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP00004(IList<SOP00004> newTask)
        {
            var result = _SOP00004Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP00004 newTask)
        {
            var result = _SOP00004Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP00004> newTask)
        {
            var result = _SOP00004Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP00004 newTask)
        {
            var result = _SOP00004Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP00004> newTask)
        {
            var result = _SOP00004Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
