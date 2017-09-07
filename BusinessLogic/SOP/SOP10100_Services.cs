using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP10100Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10100Repository _SOP10100Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10100Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10100Repository = new SOP10100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP10100> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP10100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10100Repository.GetWhereListWithPaging("SOP10100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP10100> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("BatchID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CUSTNMBR", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CustomerPoNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP10100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10100Repository.GetWhereListWithPaging("SOP10100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<SOP10100> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<SOP10100> L = null;
            var tasks = _SOP10100Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<SOP10100> result = tasks;
            return result;
        }
        public DataCollection<SOP10100> GetListWithPaging(int SOPTYPE, int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<SOP10100> result = null;
            var tasks = _SOP10100Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }


        public DataCollection<SOP10100> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP10100> L = null;
            var tasks = _SOP10100Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<SOP10100> result = tasks;
            return result;
        }
        public DataCollection<SOP10100> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP10100> result = null;
            var tasks = _SOP10100Repository.GetListWithPaging(PageSize, Page, ss => new { ss.SOPNUMBE});
            result = tasks;
            return result;
        }
        public DataCollection<SOP10100> GetListWithPaging(int SOPTYPE,int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP10100> result = null;
            var tasks = _SOP10100Repository.GetListWithPaging(PageSize, Page, ss => new { ss.SOPNUMBE});
            result = tasks;
            return result;
        }

        public IList<SOP10100> GetCustomerInvoices(string CUSTNMBR,string CurrencyCode)
        {
            IList<SOP10100> result = null;
            var tasks = _SOP10100Repository.GetAll(ss => ss.CUSTNMBR == CUSTNMBR && ss.CurrencyCode == CurrencyCode);
            result = tasks;
            return result;
        }
        public IList<SOP10100> GetAll()
        {
            try
            {
                IList<SOP10100> L = null;
                try
                {
                    var tasks = _SOP10100Repository.GetAll();
                    IList<SOP10100> result = tasks;
                    return result;
                }
                catch (Exception ex)
                {
                }
                return null;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- GenderID: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return null;
        }
        public IList<SOP00000> GetAllSOP00000()
        {
            SOP00000Repository r = new SOP00000Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<SOP00000> result = tasks;
            return result;
        }

        public KaizenResult AddSOP10100(SOP10100 newTask)
        {
            var result = _SOP10100Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP10100(IList<SOP10100> newTask)
        {
            var result = _SOP10100Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP10100 newTask)
        {
            var result = _SOP10100Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP10100> newTask)
        {
            var result = _SOP10100Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP10100 newTask)
        {
            var result = _SOP10100Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP10100> newTask)
        {
            var result = _SOP10100Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
