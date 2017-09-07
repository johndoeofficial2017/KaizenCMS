using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00017Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00017Repository _IV00017Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00017Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00017Repository = new IV00017Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00017> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00017> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00017Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00017Repository.GetWhereListWithPaging("IV00017", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00017> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("BatchSourceID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BatchSourceName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00017> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00017Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00017Repository.GetWhereListWithPaging("IV00017", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00017> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<IV00017> L = null;
            var tasks = _IV00017Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<IV00017> result = tasks;
            return result;
        }

        public DataCollection<IV00017> GetListWithPaging(string SearchTerm,string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
                DataCollection<IV00017> result = null;
                var tasks = _IV00017Repository.GetListWithPaging(x => x.BatchSourceID.ToString().Contains(SearchTerm)
                    || x.BatchSourceName.Contains(SearchTerm)
                    , PageSize, Page, ss => ss.BatchSourceID, null);
                result = tasks;
                return result;
        }
        public DataCollection<IV00017> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<IV00017> result = null;
            var tasks = _IV00017Repository.GetListWithPaging(PageSize, Page, ss => new { ss.BatchSourceID });
            result = tasks;
            return result;
        }

        public IV00017 GetSingle(int BatchSourceID)
        {
                var tasks = _IV00017Repository.GetSingle(x => x.BatchSourceID == BatchSourceID);
                return tasks;
        }

        public IList<IV00017> GetAll()
        {
                    var tasks = _IV00017Repository.GetAll();
                    IList<IV00017> result = tasks;
                    return result;
        }

        public KaizenResult AddIV00017(IV00017 newTask)
        {
            var result = _IV00017Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00017(IList<IV00017> newTask)
        {
            var result = _IV00017Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00017 newTask)
        {
            var result = _IV00017Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00017> newTask)
        {
            var result = _IV00017Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00017 newTask)
        {
            var result = _IV00017Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00017> newTask)
        {
            var result = _IV00017Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
