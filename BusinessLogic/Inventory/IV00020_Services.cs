using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00020Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00020Repository _IV00020Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00020Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00020Repository = new IV00020Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00020> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00020> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00020Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00020Repository.GetWhereListWithPaging("IV00020", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00020> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ItemGroupID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemGroupName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00020> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00020Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00020Repository.GetWhereListWithPaging("IV00020", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00020> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<IV00020> L = null;
            var tasks = _IV00020Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<IV00020> result = tasks;
            return result;
        }

        public DataCollection<IV00020> GetListWithPaging(string SearchTerm, string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<IV00020> result = null;
            var tasks = _IV00020Repository.GetListWithPaging(x => x.ItemGroupID.ToString().Contains(SearchTerm)
                || x.ItemGroupName.Contains(SearchTerm)
                , PageSize, Page, ss => ss.ItemGroupID, null);
            result = tasks;
            return result;
        }
        public DataCollection<IV00020> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<IV00020> result = null;
            var tasks = _IV00020Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ItemGroupID });
            result = tasks;
            return result;
        }

        public IV00020 GetSingle(string ItemGroupID)
        {
            var tasks = _IV00020Repository.GetSingle(x => x.ItemGroupID == ItemGroupID);
            return tasks;
        }

        public IList<IV00020> GetAll()
        {
            var tasks = _IV00020Repository.GetAll();
            IList<IV00020> result = tasks;
            return result;
        }

        public KaizenResult AddIV00020(IV00020 newTask)
        {
            var result = _IV00020Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00020(IList<IV00020> newTask)
        {
            var result = _IV00020Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00020 newTask)
        {
            var result = _IV00020Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00020> newTask)
        {
            var result = _IV00020Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00020 newTask)
        {
            var result = _IV00020Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00020> newTask)
        {
            var result = _IV00020Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
