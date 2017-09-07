using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00011Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00011Repository _GL00011Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00011Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00011Repository = new GL00011Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00011> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00011> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00011Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00011Repository.GetWhereListWithPaging("GL00011", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00011> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string CurrencyCode,
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
                    SeachStr += Help.GetFilter("ExchangeTableID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsMultiply", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            GL00011Repository rep = new GL00011Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<GL00011> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = rep.GetListWithPaging(ss => ss.CurrencyCode.Trim() == CurrencyCode.Trim(),
                    PageSize, Page, ss => Member);
            }
            else
            {
                SeachStr += " and CurrencyCode='" + CurrencyCode.Trim() + "'";
                result = rep.GetWhereListWithPaging("GL00011", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<GL00011> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ExchangeTableID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsMultiply", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            GL00011Repository rep = new GL00011Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<GL00011> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("GL00011", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00011> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<GL00011> L = null;
            var tasks = _GL00011Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<GL00011> result = tasks;
            return result;
        }
        public DataCollection<GL00011> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<GL00011> result = null;
            var tasks = _GL00011Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public DataCollection<GL00011> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<GL00011> L = null;
                var tasks = _GL00011Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<GL00011> result = tasks;
                return result;
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
        public DataCollection<GL00011> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection, string CurrencyCode)
        {
            DataCollection<GL00011> result = null;
            var tasks = _GL00011Repository.GetListWithPaging(ss => ss.CurrencyCode == CurrencyCode.Trim(), PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }
        public DataCollection<GL00011> GetListWithPagingByCurrencyCode(int PageSize, int Page, string Member, int SortDirection,string CurrencyCode)
        {
            DataCollection<GL00011> result = null;
            var tasks = _GL00011Repository.GetListWithPaging(ss => ss.CurrencyCode == CurrencyCode.Trim(), PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public DataCollection<GL00011> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00011> result = null;
            var tasks = _GL00011Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ExchangeTableID });
            result = tasks;
            return result;
        }


        public DataCollection<GL00011> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00011> result = null;
            _GL00011Repository = new GL00011Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _GL00011Repository.GetListWithPaging(SearchTerm, PageSize, Page, ss => new { ss.ExchangeTableID }, true);
            result = tasks;
            return result;
        }

        public IList<GL00011> GetAllByCurrencyCode(string CurrencyCode)
        {
            IList<GL00011> L = null;
            var tasks = _GL00011Repository.GetAll(x => x.CurrencyCode.Trim() == CurrencyCode.Trim());
            IList<GL00011> result = tasks;
            return result;
        }
        public GL00011 GetSingle(string CurrencyCode,string ExchangeTableID )
        {
            var tasks = _GL00011Repository.GetSingle(x => x.ExchangeTableID.Trim() == ExchangeTableID.Trim() && x.CurrencyCode.Trim() == CurrencyCode);
            return tasks;
        }

        public IList<GL00011> GetAllByCurrencyCodeWithExchangeTable(string CurrencyCode)
        {
            var tasks = _GL00011Repository.GetList(x => x.CurrencyCode.Trim() == CurrencyCode.Trim(), ss => new { ss.ExchangeTableID }, null, ss => ss.GL00009);
            return tasks;
        }

        public IList<GL00009> GetAllByExchangeTable()
        {
            GL00009Repository _GL00009RepositoryDataRepository = new GL00009Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _GL00009RepositoryDataRepository.GetAll();
            IList<GL00009> result = tasks;
            return result;
        }

        public KaizenResult AddGL00011(GL00011 newTask)
        {
            var result = _GL00011Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00011(IList<GL00011> newTask)
        {
            var result = _GL00011Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(GL00011 newTask)
        {
            var result = _GL00011Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00011> newTask)
        {
            var result = _GL00011Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00011 newTask)
        {
            var result = _GL00011Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00011> newTask)
        {
            var result = _GL00011Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }


    }
}
