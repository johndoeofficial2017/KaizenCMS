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
    public class SOP00003Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00003Repository _SOP00003Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00003Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00003Repository = new SOP00003Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00003> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP00003> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00003Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00003Repository.GetWhereListWithPaging("SOP00003", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP00003> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("Territory", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TerritoryName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00003> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00003Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00003Repository.GetWhereListWithPaging("SOP00003", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP00003> GetAllSOP00003(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00003> L = null;
            var tasks = _SOP00003Repository.GetListWithPaging(x => x.Territory.Contains(SearchTerm), PageSize, Page, ss => ss.Territory, null);

            DataCollection<SOP00003> result = tasks;
            return result;
        }
        public DataCollection<SOP00003> GetByTerritory(string Territory, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00003> L = null;
            var tasks = _SOP00003Repository.GetListWithPaging(x => x.Territory.Trim() == Territory.Trim(), PageSize, Page, ss => ss.Territory, null);

            DataCollection<SOP00003> result = tasks;
            return result;
        }

        public IList<SOP00003> GetAll()
        {
            var tasks = _SOP00003Repository.GetAll();
            IList<SOP00003> result = tasks;
            return result;
        }
        public SOP00003 GetSingle(string Territory)
        {
            var tasks = _SOP00003Repository.GetSingle(x => x.Territory.Trim() == Territory.Trim());
            return tasks;
        }

        public KaizenResult AddSOP00003(SOP00003 newTask)
        {
            var result = _SOP00003Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP00003(IList<SOP00003> newTask)
        {
            var result = _SOP00003Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP00003 newTask)
        {
            var result = _SOP00003Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP00003> newTask)
        {
            var result = _SOP00003Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP00003 newTask)
        {
            var result = _SOP00003Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP00003> newTask)
        {
            var result = _SOP00003Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
