using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin.Repository;
using Kaizen.Admin;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;


namespace Kaizen.BusinessLogic.Admin
{
    public class MS_00201Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.MS_00201Repository _MS_00201RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public MS_00201Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _MS_00201RepositoryDataRepository = new MS_00201Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<MS_00201> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<MS_00201> L = null;
            var tasks = _MS_00201RepositoryDataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<MS_00201> result = tasks;
            return result;
        }
        public DataCollection<MS_00201> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<MS_00201> result = null;
            var tasks = _MS_00201RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public DataCollection<MS_00201> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<MS_00201> L = null;
            var tasks = _MS_00201RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<MS_00201> result = tasks;
            return result;
        }
        public DataCollection<MS_00201> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<MS_00201> result = null;
            var tasks = _MS_00201RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.TrxID });
            result = tasks;
            return result;
        }
        public DataCollection<MS_00201> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<MS_00201> result = null;
            var tasks = _MS_00201RepositoryDataRepository.GetListWithPaging(SearchTerm,
                PageSize, Page, ss => ss.TrxID, true);
            result = tasks;
            return result;
        }
        public IList<MS_00201> GetAll()
        {
            try
            {
                IList<MS_00201> L = null;
                try
                {
                    var tasks = _MS_00201RepositoryDataRepository.GetAll();
                    IList<MS_00201> result = tasks;
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
        public MS_00201 GetSingle(int LineID)
        {
            try
            {
                var tasks = _MS_00201RepositoryDataRepository.GetSingle(x => x.LineID == LineID);
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
        public bool AddMS_00201(MS_00201 newTask)
        {
            try
            {
                _MS_00201RepositoryDataRepository.Add(newTask);

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
        public bool Update(MS_00201 UpdateMS_00201)
        {
            try
            {
                _MS_00201RepositoryDataRepository.Update(UpdateMS_00201);
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
