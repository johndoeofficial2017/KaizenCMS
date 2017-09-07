using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00018Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00018Repository _IV00018Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00018Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00018Repository = new IV00018Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00018> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00018> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00018Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00018Repository.GetWhereListWithPaging("IV00018", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00018> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TransactionTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TransactionTypeName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00018> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00018Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00018Repository.GetWhereListWithPaging("IV00018", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00018> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<IV00018> L = null;
            var tasks = _IV00018Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<IV00018> result = tasks;
            return result;
        }

        public DataCollection<IV00018> GetListWithPaging(string SearchTerm,string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
                DataCollection<IV00018> result = null;
                var tasks = _IV00018Repository.GetListWithPaging(x => x.TransactionTypeID.ToString().Contains(SearchTerm)
                    || x.TransactionTypeName.Contains(SearchTerm)
                    , PageSize, Page, ss => ss.TransactionTypeID, null);
                result = tasks;
                return result;
        }
        public DataCollection<IV00018> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<IV00018> result = null;
            var tasks = _IV00018Repository.GetListWithPaging(PageSize, Page, ss => new { ss.TransactionTypeID });
            result = tasks;
            return result;
        }

        public IV00018 GetSingle(int TransactionTypeID)
        {
                var tasks = _IV00018Repository.GetSingle(x => x.TransactionTypeID == TransactionTypeID);
                return tasks;
        }

        public IList<IV00018> GetAll()
        {
                    var tasks = _IV00018Repository.GetAll();
                    IList<IV00018> result = tasks;
                    return result;
        }

        public KaizenResult AddIV00018(IV00018 newTask)
        {
            var result = _IV00018Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00018(IList<IV00018> newTask)
        {
            var result = _IV00018Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00018 newTask)
        {
            var result = _IV00018Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00018> newTask)
        {
            var result = _IV00018Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00018 newTask)
        {
            var result = _IV00018Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00018> newTask)
        {
            var result = _IV00018Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
