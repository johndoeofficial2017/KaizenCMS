using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;

namespace Kaizen.BusinessLogic.SOP
{
    public class SOP00200Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00200Repository _SOP00200RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00200Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00200RepositoryDataRepository = new SOP00200Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00200> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<SOP00200> L = null;
                var tasks = _SOP00200RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                    , xx => xx.SOP00201);
                DataCollection<SOP00200> result = tasks;
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
        public DataCollection<SOP00200> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                DataCollection<SOP00200> result = null;

                var tasks = _SOP00200RepositoryDataRepository.GetListWithPaging(x => x.AddressCode.ToString().Contains(SearchTerm) ||
                    x.DocDescription.Contains(SearchTerm)
                    , PageSize, Page, ss => new { ss.TerritoryID }, null);
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
        public DataCollection<SOP00200> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00200> result = null;
            var tasks = _SOP00200RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.AddressCode });
            result = tasks;
            return result;
        }
        public SOP00200 GetSingle(string DocNumber)
        {
            try
            {
                var tasks = _SOP00200RepositoryDataRepository.GetSingle(x => x.DocNumber.Trim() == DocNumber);
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
  
        public bool AddSOP00200(SOP00200 newTask)
        {
            _SOP00200RepositoryDataRepository.Add(newTask);

            return true;
        }
        public bool Update(SOP00200 UpdateSOP00200)
        {
            try
            {
                _SOP00200RepositoryDataRepository.Update(UpdateSOP00200);
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
        public bool Delete(string  ItemID)
        {
            try
            {
                _SOP00200RepositoryDataRepository.ExecuteSqlCommand("delete SOP00200 where ItemID='" + ItemID.Trim() + "'");
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
     
    }
}
