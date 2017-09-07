using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin.Repository;
using Kaizen.Admin;

namespace Kaizen.BusinessLogic.Admin
{
    public class Sys00013Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.Sys00013Repository _Sys00013RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Sys00013Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Sys00013RepositoryDataRepository = new Sys00013Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<Sys00013> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CountryID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CountryName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            Sys00013Repository rep = new Sys00013Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<Sys00013> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("Sys00013", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Sys00013> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Sys00013> L = null;
            var tasks = _Sys00013RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<Sys00013> result = tasks;
            return result;
        }

        public DataCollection<Sys00013> GetListWithPaging(string SearchTerm, string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                DataCollection<Sys00013> result = null;
                var tasks = _Sys00013RepositoryDataRepository.GetListWithPaging(x => x.CountryID.ToString().Contains(SearchTerm)
                    || x.CountryName.Contains(SearchTerm)
                    , PageSize, Page, ss => ss.CountryID, null);
                result = tasks;
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
        public DataCollection<Sys00013> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Sys00013> result = null;
            var tasks = _Sys00013RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.CountryID });
            result = tasks;
            return result;
        }


        public DataCollection<Sys00013> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<Sys00013> L = null;
            var tasks = _Sys00013RepositoryDataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<Sys00013> result = tasks;
            return result;
        }
        public DataCollection<Sys00013> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<Sys00013> result = null;
            var tasks = _Sys00013RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<Sys00013> GetAll()
        {
            try
            {
                IList<Sys00013> L = null;
                try
                {
                    var tasks = _Sys00013RepositoryDataRepository.GetAll();
                    IList<Sys00013> result = tasks;
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

        public Sys00013 GetSingle(string CountryID)
        {
            try
            {
                var tasks = _Sys00013RepositoryDataRepository.GetSingle(x => x.CountryID == CountryID);
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

        public KaizenResult AddSys00013(Sys00013 newTask)
        {
            var result = _Sys00013RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(Sys00013 newTask)
        {
            var result = _Sys00013RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(Sys00013 newTask)
        {
            var result = _Sys00013RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
    }
}
