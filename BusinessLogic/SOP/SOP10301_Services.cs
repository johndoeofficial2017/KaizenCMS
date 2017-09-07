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
    public class SOP10301Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10301Repository _SOP10301Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10301Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10301Repository = new SOP10301Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP10301> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP10301> L = null;
            var tasks = _SOP10301Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<SOP10301> result = tasks;
            return result;
        }

        public DataCollection<SOP10301> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP10301> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10301Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10301Repository.GetWhereListWithPaging("SOP10301", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP10301> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<SOP10301> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10301Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10301Repository.GetWhereListWithPaging("SOP10301", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public IList<SOP10301> GetAll()
        {
            var tasks = _SOP10301Repository.GetAll();
            IList<SOP10301> result = tasks;
            return result;
        }
        public IList<SOP10301> GetBySOPNUMBE(string SOPNUMBE)
        {
            var tasks = _SOP10301Repository.GetAll(ss => ss.SOPNUMBE == SOPNUMBE);
            IList<SOP10301> result = tasks;
            return result;
        }

        public SOP10301 GetSingle(int LineID)
        {
            var tasks = _SOP10301Repository.GetSingle(x => x.LineID == LineID);
            return tasks;
        }

        public KaizenResult AddSOP10301(SOP10301 newTask)
        {
            var result = _SOP10301Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP10301(IList<SOP10301> newTask)
        {
            var result = _SOP10301Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP10301 newTask)
        {
            var result = _SOP10301Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP10301> newTask)
        {
            var result = _SOP10301Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP10301 newTask)
        {
            var result = _SOP10301Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP10301> newTask)
        {
            var result = _SOP10301Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
