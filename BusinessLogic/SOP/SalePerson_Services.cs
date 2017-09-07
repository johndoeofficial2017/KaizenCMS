using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP00110Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00110Repository _SOP00110Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00110Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00110Repository = new SOP00110Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00110> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP00110> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00110Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00110Repository.GetWhereListWithPaging("SOP00110", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP00110> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("SalePersonID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SalePersonTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FirstName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MidName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LastName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Inactive", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmailAddress", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DirectPhon", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmployeeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SupervisorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UserCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00110> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00110Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00110Repository.GetWhereListWithPaging("SOP00110", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<SOP00110> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<SOP00110> L = null;
            var tasks = _SOP00110Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<SOP00110> result = tasks;
            return result;
        }
        public DataCollection<SOP00110> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<SOP00110> result = null;
            var tasks = _SOP00110Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public DataCollection<SOP00110> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00110> L = null;
            var tasks = _SOP00110Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<SOP00110> result = tasks;
            return result;
        }
        public DataCollection<SOP00110> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00110> result = null;
            var tasks = _SOP00110Repository.GetListWithPaging(PageSize, Page, ss => new { ss.SalePersonID });
            result = tasks;
            return result;
        }
      
        public SOP00110 GetSingle(string SalePersonID)
        {
            var tasks = _SOP00110Repository.GetSingle(x => x.SalePersonID == SalePersonID);
            return tasks;
        }
        public SOP00110 GetSingleByUserCode(string UserCode)
        {
            var tasks = _SOP00110Repository.GetSingle(x => x.UserCode == UserCode);
            return tasks;
        }
        public DataCollection<SOP00110> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00110> result = null;
            Kaizen.SOP.Repository.SOP00110Repository _SOP00110Repository = new SOP00110Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _SOP00110Repository.GetListWithPaging(SearchTerm,
                PageSize, Page, ss => ss.SalePersonID, true);
            result = tasks;
            return result;
        }

        public IList<SOP00110> GetAll()
        {
            var tasks = _SOP00110Repository.GetAll();
            IList<SOP00110> result = tasks;
            return result;
        }

        public KaizenResult AddSOP00110(SOP00110 newTask)
        {
            var result = _SOP00110Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP00110(IList<SOP00110> newTask)
        {
            var result = _SOP00110Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP00110 newTask)
        {
            var result = _SOP00110Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP00110> newTask)
        {
            var result = _SOP00110Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP00110 newTask)
        {
            var result = _SOP00110Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP00110> newTask)
        {
            var result = _SOP00110Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
