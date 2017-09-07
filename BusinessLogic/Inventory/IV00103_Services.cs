using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00103Services
    {
        #region Infrastructure

        private IV00103Repository _IV00103RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00103Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00103RepositoryDataRepository = new IV00103Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00103> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<IV00103> L = null;
            var tasks = _IV00103RepositoryDataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<IV00103> result = tasks;
            return result;
        }
        public DataCollection<IV00103> GetListByItemIDWithPaging(string ItemID, int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<IV00103> result = null;
            var tasks = _IV00103RepositoryDataRepository.GetListWithPaging(x => x.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public DataCollection<IV00103> GetAllIV00103(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<IV00103> L = null;
                var tasks = _IV00103RepositoryDataRepository.GetListWithPaging(x => x.ItemID.Contains(SearchTerm), PageSize, Page, ss => ss.ItemID, null);

                DataCollection<IV00103> result = tasks;
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
        public DataCollection<IV00103> GetByItemID(string ItemID, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<IV00103> L = null;
                var tasks = _IV00103RepositoryDataRepository.GetListWithPaging(x => x.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => ss.ItemID, null);

                DataCollection<IV00103> result = tasks;
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

        public IList<IV00103> GetAll()
        {
            try
            {
                IList<IV00103> L = null;
                try
                {
                    var tasks = _IV00103RepositoryDataRepository.GetAll();
                    IList<IV00103> result = tasks;
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
  
        public bool AddIV00103(IV00103 newTask)
        {
            try
            {
                _IV00103RepositoryDataRepository.Add(newTask);
               
                return true;


            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(IV00103 UpdateIV00103)
        {
            _IV00103RepositoryDataRepository.Update(UpdateIV00103);
            return true;
        }
        public bool Delete(IList<IV00103> newTask)
        {
            if (newTask == null) return false;
            foreach (var item in newTask)
            {
                _IV00103RepositoryDataRepository.ExecuteSqlCommand("delete IV00103 where ItemID ='" + item.ItemID + "' AND SiteID='" + item.SiteID+"'");
            } 
            return true;
        }

    }
}
