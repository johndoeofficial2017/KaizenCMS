using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin.Repository;
using Kaizen.Admin;


namespace Kaizen.BusinessLogic.Admin
{
    public class CRM00201Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.CRM00201Repository _CRM00201RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CRM00201Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CRM00201RepositoryDataRepository = new CRM00201Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CRM00201> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CRM00201> L = null;
            var tasks = _CRM00201RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CRM00201> result = tasks;
            return result;
        }
        public DataCollection<CRM00201> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CRM00201> result = null;
            var tasks = _CRM00201RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.DocumentID });
            result = tasks;
            return result;
        }

        public IList<CRM00201> GetAll()
        {
            try
            {
                IList<CRM00201> L = null;
                try
                {
                    var tasks = _CRM00201RepositoryDataRepository.GetAll();
                    IList<CRM00201> result = tasks;
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

        public IList<CRM00201> GetAllByTransactionID(int TransactionID)
        {
            try
            {
                try
                {
                    var tasks = _CRM00201RepositoryDataRepository.GetList(xx => xx.TransactionID == TransactionID);
                    IList<CRM00201> result = tasks;
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

        public CRM00201 GetSingle(Guid DocumentID)
        {
            try
            {
                var tasks = _CRM00201RepositoryDataRepository.GetSingle(x => x.DocumentID == DocumentID);
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

        public bool AddCRM00201(CRM00201 newTask)
        {
            try
            {
                _CRM00201RepositoryDataRepository.Add(newTask);

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
        public bool Update(CRM00201 UpdateCRM00201)
        {
            try
            {
                _CRM00201RepositoryDataRepository.Update(UpdateCRM00201);
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
        public bool Delete(IList<CRM00201> newTask)
        {
            if (newTask == null) return false;
            string str = string.Empty;
            foreach (var item in newTask)
            {
                str +="'"+ item.DocumentID + "',";
            }
            str = str.Substring(0, str.Length - 1);
            _CRM00201RepositoryDataRepository.ExecuteSqlCommand("delete CRM00201 where DocumentID in(" + str + ");");
            return true;
        }

    }
}
