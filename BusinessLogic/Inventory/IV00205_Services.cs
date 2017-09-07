using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00205Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00205Repository _IV00205Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00205Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00205Repository = new IV00205Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00205> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00205> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00205Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00205Repository.GetWhereListWithPaging("IV00205", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00205> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("BatchDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00205> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00205Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00205Repository.GetWhereListWithPaging("IV00205", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00205> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<IV00205> L = null;
            var tasks = _IV00205Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<IV00205> result = tasks;
            return result;
        }

        public DataCollection<IV00205> GetListWithPaging(string SearchTerm, string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
            //DataCollection<IV00205> result = null;
            //var tasks = _IV00205Repository.GetListWithPaging(x => x.BatchID.ToString().Contains(SearchTerm)
            //    || x.BatchDescription.Contains(SearchTerm)
            //    , PageSize, Page, ss => ss.BatchID, null);
            //result = tasks;
            return null;
        }
       


        public IList<IV00205> GetAll()
        {
            var tasks = _IV00205Repository.GetAll();
            IList<IV00205> result = tasks;
            return result;
        }

        public KaizenResult AddIV00205(IV00205 newTask)
        {
            var result = _IV00205Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00205(IList<IV00205> newTask)
        {
            var result = _IV00205Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00205 newTask)
        {
            var result = _IV00205Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00205> newTask)
        {
            var result = _IV00205Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00205 newTask)
        {
            var result = _IV00205Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00205> newTask)
        {
            var result = _IV00205Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
