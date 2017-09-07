using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin.Repository;
using Kaizen.Admin;

namespace Kaizen.BusinessLogic.Admin
{
    public class MS_00002Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.MS_00002Repository _MS_00002Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public MS_00002Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _MS_00002Repository = new MS_00002Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<MS_00002> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<MS_00002> L = null;
            var tasks = _MS_00002Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<MS_00002> result = tasks;
            return result;
        }
        public DataCollection<MS_00002> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<MS_00002> result = null;
            var tasks = _MS_00002Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public DataCollection<MS_00002> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<MS_00002> L = null;
            var tasks = _MS_00002Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<MS_00002> result = tasks;
            return result;
        }
        public DataCollection<MS_00002> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<MS_00002> result = null;
            var tasks = _MS_00002Repository.GetListWithPaging(PageSize, Page, ss => new { ss.MsgTemplateID });
            result = tasks;
            return result;
        }
        public DataCollection<MS_00002> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<MS_00002> result = null;
            var tasks = _MS_00002Repository.GetListWithPaging(SearchTerm,
                PageSize, Page, ss => ss.MsgTemplateID, true);
            result = tasks;
            return result;
        }

        public IList<MS_00002> GetAll()
        {
            try
            {
                IList<MS_00002> L = null;
                try
                {
                    var tasks = _MS_00002Repository.GetAll();
                    IList<MS_00002> result = tasks;
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

        public MS_00002 GetSingle(int MsgTemplateID)
        {
            try
            {
                var tasks = _MS_00002Repository.GetSingle(x => x.MsgTemplateID == MsgTemplateID);
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
  
        public bool AddMS_00002(MS_00002 newTask)
        {
            try
            {
                _MS_00002Repository.Add(newTask);
               
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
        public bool Update(MS_00002 UpdateMS_00002)
        {
            try
            {
                _MS_00002Repository.Update(UpdateMS_00002);
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

        public IList<MS_00001> GetAllMS_00001()
        {
            MS_00001Repository r = new MS_00001Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<MS_00001> result = tasks;
            return result;
        }
        public IList<MS_00000> GetAllMS_00000()
        {
            try
            {
                IList<MS_00000> L = null;
                try
                {
                    MS_00000Repository r = new MS_00000Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    var tasks = r.GetAll();
                    IList<MS_00000> result = tasks;
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

    }
}
