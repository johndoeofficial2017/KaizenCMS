using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class Sys00007Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.Sys00007Repository _Sys00007Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Sys00007Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Sys00007Repository = new Sys00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<Sys00007> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Sys00007> L = null;
            var tasks = _Sys00007Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<Sys00007> result = tasks;
            return result;
        }
        public DataCollection<Sys00007> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Sys00007> result = null;
            var tasks = _Sys00007Repository.GetListWithPaging(PageSize, Page, ss => new { ss.DocumentTypeID });
            result = tasks;
            return result;
        }
        public DataCollection<Sys00007> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Sys00007> result = null;
            var tasks = _Sys00007Repository.GetListWithPaging(PageSize, Page, ss => new { ss.DocumentTypeID });
            result = tasks;
            return result;
        }

        public IList<Sys00007> GetAll()
        {
            var tasks = _Sys00007Repository.GetAll();
            IList<Sys00007> result = tasks;
            return result;
        }
        public IList<Sys00005> GetAllSys00005()
        {
            Sys00005Repository rep = new Sys00005Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll();
            IList<Sys00005> result = tasks;
            return result;
        }
        public Sys00007 GetSingle(int DocumentTypeID)
        {
            var tasks = _Sys00007Repository.GetSingle(x => x.DocumentTypeID == DocumentTypeID);
            return tasks;
        }

        public bool AddSys00007(Sys00007 newTask)
        {
            _Sys00007Repository.Add(newTask);
            return true;
        }
        public bool Update(Sys00007 UpdateSys00007)
        {
            try
            {
                _Sys00007Repository.Update(UpdateSys00007);
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
        public bool Delete(int DocumentTypeID)
        {
            try
            {
                _Sys00007Repository.ExecuteSqlCommand("delete Sys00007 where DocumentTypeID='" + DocumentTypeID + "'");
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
