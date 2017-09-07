using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
using System.Globalization;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP00507Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00507Repository _SOP00507RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00507Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00507RepositoryDataRepository = new SOP00507Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<SOP00507> GetAllSOP00507(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<SOP00507> L = null;
                var tasks = _SOP00507RepositoryDataRepository.GetListWithPaging(x => x.SOPNUMBE.Contains(SearchTerm), PageSize, Page, ss => ss.AccountLine, null);

                DataCollection<SOP00507> result = tasks;
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
        public DataCollection<SOP00507> GetBySOPNUMBE(string SOPNUMBE, int SOPTYPE, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<SOP00507> L = null;
                var tasks = _SOP00507RepositoryDataRepository.GetListWithPaging(x => x.SOPNUMBE.Trim() == SOPNUMBE.Trim() && x.SOPTYPE == SOPTYPE, PageSize, Page, ss => ss.AccountLine, null);

                DataCollection<SOP00507> result = tasks;
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

        public IList<SOP00507> GetAll()
        {
            try
            {
                IList<SOP00507> L = null;
                try
                {
                    var tasks = _SOP00507RepositoryDataRepository.GetAll();
                    IList<SOP00507> result = tasks;
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
        public IList<SOP00507> GetListByCashReceiptLine(string SOPNUMBE, string SOPTypeSetupID, int SOPTYPE, string TrxDocumentID)
        {
            var tasks = _SOP00507RepositoryDataRepository.GetAll(ss => ss.SOPNUMBE.Trim() == SOPNUMBE.Trim() &&
                ss.SOPTYPE == SOPTYPE && ss.SOPTypeSetupID.Trim() == SOPTypeSetupID.Trim() && ss.TrxDocumentID.Trim() == TrxDocumentID.Trim(), ss => new { ss.AccountID });
            IList<SOP00507> result = tasks;
            return result;
        }
        public SOP00507 GetSingle(int AccountLine)
        {
            try
            {
                var tasks = _SOP00507RepositoryDataRepository.GetSingle(x => x.AccountLine == AccountLine);
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

        public IList<Sys00007> GetAllSys00007()
        {
            Sys00007Repository r = new Sys00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<Sys00007> result = tasks;
            return result;
        }

        public bool AddSOP00507(SOP00507 newTask)
        {
            try
            {
                _SOP00507RepositoryDataRepository.Add(newTask);

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
        public bool Update(SOP00507 UpdateSOP00507)
        {
            _SOP00507RepositoryDataRepository.Update(UpdateSOP00507);
            return true;
        }
        public bool Delete(IList<SOP00507> newTask)
        {
            if (newTask == null) return false;
            string str = string.Empty;
            foreach (var item in newTask)
            {
                str += item.AccountLine + ",";
            }
            str = str.Substring(0, str.Length - 1);
            _SOP00507RepositoryDataRepository.ExecuteSqlCommand("delete SOP00507 where AccountLine in(" + str + ");");
            return true;
        }
    }
}
