using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00206Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00206Repository _IV00206Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00206Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00206Repository = new IV00206Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00206> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00206> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00206Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00206Repository.GetWhereListWithPaging("IV00206", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00206> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<IV00206> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00206Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00206Repository.GetWhereListWithPaging("IV00206", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00206> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<IV00206> L = null;
            var tasks = _IV00206Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<IV00206> result = tasks;
            return result;
        }       


        public IList<IV00206> GetAll()
        {
            var tasks = _IV00206Repository.GetAll();
            IList<IV00206> result = tasks;
            return result;
        }
        public IList<IV00206> GetByTransaction(string TransactionID,int TransactionTypeID)
        {
            var tasks = _IV00206Repository.GetAll(ss=>ss.TransactionID==TransactionID && ss.TransactionTypeID ==TransactionTypeID);
            IList<IV00206> result = tasks;
            return result;
        }

        public KaizenResult AddIV00206(IV00206 newTask)
        {
            var result = _IV00206Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00206(IList<IV00206> newTask)
        {
            var result = _IV00206Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00206 newTask)
        {
            var result = _IV00206Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00206> newTask)
        {
            var result = _IV00206Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00206 newTask)
        {
            var result = _IV00206Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00206> newTask)
        {
            var result = _IV00206Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
