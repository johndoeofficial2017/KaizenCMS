using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00012Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00012Repository _GL00012RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00012Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00012RepositoryDataRepository = new GL00012Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00012> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00012> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00012RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00012RepositoryDataRepository.GetWhereListWithPaging("GL00012", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00012> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ExchangeRateID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeTableID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("StartDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExpirationDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeRate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            GL00012Repository rep = new GL00012Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<GL00012> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("GL00012", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00012> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string CurrencyCode, string ExchangeTableID,
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
                    SeachStr += Help.GetFilter("ExchangeRateID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeTableID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("StartDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExpirationDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeRate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            GL00012Repository rep = new GL00012Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<GL00012> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = rep.GetListWithPaging(ss => ss.CurrencyCode == CurrencyCode && ss.ExchangeTableID == ExchangeTableID,
                    PageSize, Page, ss => Member);
            }
            else
            {
                SeachStr += " and CurrencyCode='" + CurrencyCode + "'";
                SeachStr += " and ExchangeTableID='" + ExchangeTableID + "'";
                result = rep.GetWhereListWithPaging("GL00012", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<GL00012> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<GL00012> L = null;
            var tasks = _GL00012RepositoryDataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<GL00012> result = tasks;
            return result;
        }
        public DataCollection<GL00012> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<GL00012> result = null;
            var tasks = _GL00012RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public DataCollection<GL00012> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection, string ExchangeTableID, string CurrencyCode)
        {
            DataCollection<GL00012> result = null;
            var tasks = _GL00012RepositoryDataRepository.GetListWithPaging(ss => ss.CurrencyCode == CurrencyCode.Trim()
                && ss.ExchangeTableID == ExchangeTableID.Trim(),
                PageSize, Page, ss => Member, null
                );
            result = tasks;
            return result;
        }
        public DataCollection<GL00012> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection, string ExchangeTableID, string CurrencyCode)
        {
            DataCollection<GL00012> result = null;
            var tasks = _GL00012RepositoryDataRepository.GetListWithPaging(ss => ss.CurrencyCode == CurrencyCode.Trim()
                && ss.ExchangeTableID == ExchangeTableID.Trim(),
                PageSize, Page, ss => Member, null
                );
            result = tasks;
            return result;
        }
        public IList<GL00012> GetAllByCurrencyAndExchangeTable(string ExchangeTableID, string CurrencyCode)
        {
            var tasks = _GL00012RepositoryDataRepository.GetAll(x => x.ExchangeTableID == ExchangeTableID 
            && x.CurrencyCode == CurrencyCode, ss => new { ss.ExchangeRateID });
            IList<GL00012> result = tasks;
            return result;
        }

        public IList<GL00012> GetAllByExchangeTable(string ExchangeTableID)
        {
            var tasks = _GL00012RepositoryDataRepository.GetAll(x => x.ExchangeTableID == ExchangeTableID);
            IList<GL00012> result = tasks;
            return result;
        }
        public GL00012 GetSingle(int ExchangeRateID)
        {
            try
            {
                var tasks = _GL00012RepositoryDataRepository.GetSingle(x => x.ExchangeRateID == ExchangeRateID);
                return tasks;
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
        public IList<GL00012> GetAll()
        {
            try
            {
                IList<GL00012> L = null;
                try
                {
                    var tasks = _GL00012RepositoryDataRepository.GetAll();
                    IList<GL00012> result = tasks;
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
        public DataCollection<GL00012> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<GL00012> L = null;
            var tasks = _GL00012RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<GL00012> result = tasks;
            return result;
        }
        public DataCollection<GL00012> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00012> result = null;
            var tasks = _GL00012RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.ExchangeRateID }, ss => ss.GL00011);
            result = tasks;
            return result;
        }

        public DataCollection<GL00012> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00012> result = null;
            _GL00012RepositoryDataRepository = new GL00012Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _GL00012RepositoryDataRepository.GetListWithPaging(SearchTerm, PageSize, Page, ss => new { ss.ExchangeRateID }, true);
            result = tasks;
            return result;
        }
        public KaizenResult AddGL00012(GL00012 newTask)
        {
            KaizenResult result = _GL00012RepositoryDataRepository.AddKaizenObject(newTask);
            return result;

        }
        public KaizenResult AddGL00012(IList<GL00012> newTask)
        {
            KaizenResult result = _GL00012RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;

        }

        public KaizenResult Update(GL00012 newTask)
        {
            KaizenResult result = _GL00012RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00012> newTask)
        {
            KaizenResult result = _GL00012RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;

        }

        public KaizenResult Delete(GL00012 newTask)
        {
            KaizenResult result = _GL00012RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00012> newTask)
        {
            KaizenResult result = _GL00012RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;

        }


    }
}
