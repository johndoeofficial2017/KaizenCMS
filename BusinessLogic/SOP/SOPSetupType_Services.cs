using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP00001Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00001Repository _SOP00001Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00001Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00001Repository = new SOP00001Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00001> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP00001> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00001Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00001Repository.GetWhereListWithPaging("SOP00001", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP00001> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("SOPTypeSetupID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SOPTypeSetupName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SOPTYPE", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UseProspect", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsRepeatable", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsOverride", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00001> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00001Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00001Repository.GetWhereListWithPaging("SOP00001", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<SOP00001> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, int SOPTYPE,
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
                    SeachStr += Help.GetFilter("SOPTypeSetupID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SOPTypeSetupName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SOPTYPE", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UseProspect", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsRepeatable", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsOverride", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00001> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _SOP00001Repository.GetListWithPaging(ss => ss.SOPTYPE == SOPTYPE,
                     PageSize, Page, ss => Member);
            }
            else
            {
                SeachStr += " and SOPTYPE='" + SOPTYPE + "'";
                result = _SOP00001Repository.GetWhereListWithPaging("SOP00001", PageSize, Page, SeachStr, Member, IsAscending);
            }
            return result;
        }

        public DataCollection<SOP00001> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00001> L = null;
            var tasks = _SOP00001Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<SOP00001> result = tasks;
            return result;
        }
        public DataCollection<SOP00001> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00001> result = null;
            var tasks = _SOP00001Repository.GetListWithPaging(PageSize, Page, ss => new { ss.SOPTypeSetupID });
            result = tasks;
            return result;
        }
        public DataCollection<SOP00001> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00001> result = null;
            var tasks = _SOP00001Repository.GetListWithPaging(PageSize, Page, ss => new { ss.SOPTypeSetupID });
            result = tasks;
            return result;
        }
        public DataCollection<SOP00001> GetListWithPaging(int SOPTYPE, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00001> result = null;
            var tasks = _SOP00001Repository.GetListWithPaging(ss => ss.SOPTYPE == SOPTYPE, PageSize, Page, ss => new { ss.SOPTypeSetupID });
            result = tasks;
            return result;
        }


        public DataCollection<SOP00001> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<SOP00001> L = null;
            var tasks = _SOP00001Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<SOP00001> result = tasks;
            return result;
        }
        public DataCollection<SOP00001> GetListWithPaging(int SOPTYPE, int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<SOP00001> result = null;
            var tasks = _SOP00001Repository.GetListWithPaging(xx => xx.SOPTYPE == SOPTYPE, PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<SOP00001> GetAll()
        {
            var tasks = _SOP00001Repository.GetAll();
            IList<SOP00001> result = tasks;
            return result;
        }
        public IList<SOP00001> GetAllQuote()
        {
            var tasks = _SOP00001Repository.GetAll(xx => xx.SOPTYPE == 1);
            IList<SOP00001> result = tasks;
            return result;
        }
        public SOP00001 GetSingle(string SOPTypeSetupID)
        {
            var tasks = _SOP00001Repository.GetSingle(x => x.SOPTypeSetupID == SOPTypeSetupID);
            return tasks;
        }

        public KaizenResult AddSOP00001(SOP00001 newTask)
        {
            var result = _SOP00001Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP00001(IList<SOP00001> newTask)
        {
            var result = _SOP00001Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP00001 newTask)
        {
            var result = _SOP00001Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP00001> newTask)
        {
            var result = _SOP00001Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP00001 newTask)
        {
            var result = _SOP00001Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP00001> newTask)
        {
            var result = _SOP00001Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
