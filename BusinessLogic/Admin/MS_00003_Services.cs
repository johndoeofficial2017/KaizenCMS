using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin.Repository;
using Kaizen.Admin;

namespace Kaizen.BusinessLogic.Admin
{
    public class MS_00003Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.MS_00003Repository _MS_00003RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public MS_00003Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _MS_00003RepositoryDataRepository = new MS_00003Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<MS_00003> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<MS_00003> L = null;
            var tasks = _MS_00003RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<MS_00003> result = tasks;
            return result;
        }
        public DataCollection<MS_00003> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<MS_00003> result = null;
            var tasks = _MS_00003RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.DocumentID });
            result = tasks;
            return result;
        }

        public IList<MS_00003> GetAll()
        {
            try
            {
                IList<MS_00003> L = null;
                try
                {
                    var tasks = _MS_00003RepositoryDataRepository.GetAll();
                    IList<MS_00003> result = tasks;
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

        public IList<MS_00003> GetAllByMsgTemplateID(int MsgTemplateID)
        {
            try
            {
                try
                {
                    var tasks = _MS_00003RepositoryDataRepository.GetList(xx => xx.MsgTemplateID == MsgTemplateID);
                    IList<MS_00003> result = tasks;
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

        public MS_00003 GetSingle(string DocumentID)
        {
            try
            {
                var tasks = _MS_00003RepositoryDataRepository.GetSingle(x => x.DocumentID.Trim() == DocumentID.Trim());
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

        public bool AddMS_00003(MS_00003 newTask)
        {
            try
            {
                _MS_00003RepositoryDataRepository.Add(newTask);

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
        public bool Update(MS_00003 UpdateMS_00003)
        {
            try
            {
                _MS_00003RepositoryDataRepository.Update(UpdateMS_00003);
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
        public bool Delete(IList<MS_00003> newTask)
        {
            if (newTask == null) return false;
            string str = string.Empty;
            foreach (var item in newTask)
            {
                str +="'"+ item.DocumentID + "',";
            }
            str = str.Substring(0, str.Length - 1);
            _MS_00003RepositoryDataRepository.ExecuteSqlCommand("delete MS_00003 where DocumentID in(" + str + ");");
            return true;
        }

    }
}
