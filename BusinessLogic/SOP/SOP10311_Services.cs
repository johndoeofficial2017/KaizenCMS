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
    public class SOP10311Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10311Repository _SOP10311Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10311Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10311Repository = new SOP10311Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP10311> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP10311> L = null;
            var tasks = _SOP10311Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<SOP10311> result = tasks;
            return result;
        }

        public DataCollection<SOP10311> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP10311> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10311Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10311Repository.GetWhereListWithPaging("SOP10311", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP10311> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("SOPNUMBE", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InvoiceOTY", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UnitCost", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP10311> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10311Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10311Repository.GetWhereListWithPaging("SOP10311", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public IList<SOP10311> GetAll()
        {
            var tasks = _SOP10311Repository.GetAll();
            IList<SOP10311> result = tasks;
            return result;
        }
        public IList<SOP10311> GetByTrxNumber(string TrxNumber)
        {
            var tasks = _SOP10311Repository.GetAll(ss => ss.TrxNumber == TrxNumber);
            IList<SOP10311> result = tasks;
            return result;
        }

        public SOP10311 GetSingle(int LineID)
        {
            var tasks = _SOP10311Repository.GetSingle(x => x.LineID == LineID);
            return tasks;
        }

        public KaizenResult AddSOP10311(SOP10311 newTask)
        {
            var result = _SOP10311Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP10311(IList<SOP10311> newTask)
        {
            var result = _SOP10311Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP10311 newTask)
        {
            var result = _SOP10311Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP10311> newTask)
        {
            var result = _SOP10311Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP10311 newTask)
        {
            var result = _SOP10311Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP10311> newTask)
        {
            var result = _SOP10311Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
