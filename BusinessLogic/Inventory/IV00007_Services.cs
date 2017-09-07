using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00007Services
    {
        #region Infrastructure

        private IV00007Repository _IV00007Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00007Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00007Repository = new IV00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00007> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00007> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00007Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00007Repository.GetWhereListWithPaging("IV00007", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00007> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ValuationMethodID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ValuationMethodName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00007> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00007Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00007Repository.GetWhereListWithPaging("IV00007", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public IList<IV00007> GetAll()
        {
            var tasks = _IV00007Repository.GetAll();
            IList<IV00007> result = tasks;
            return result;
        }

        public IV00007 GetSingle(byte ValuationMethodID)
        {
            var tasks = _IV00007Repository.GetSingle(x => x.ValuationMethodID == ValuationMethodID);
            return tasks;
        }

        public KaizenResult AddIV00007(IV00007 newTask)
        {
            var result = _IV00007Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00007(IList<IV00007> newTask)
        {
            var result = _IV00007Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00007 newTask)
        {
            var result = _IV00007Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00007> newTask)
        {
            var result = _IV00007Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00007 newTask)
        {
            var result = _IV00007Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00007> newTask)
        {
            var result = _IV00007Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
