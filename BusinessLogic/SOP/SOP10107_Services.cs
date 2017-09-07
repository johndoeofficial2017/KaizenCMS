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
    public class SOP10107Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10107Repository _SOP10107RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10107Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10107RepositoryDataRepository = new SOP10107Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public IList<SOP10107> GetAll()
        {
            try
            {
                IList<SOP10107> L = null;
                try
                {
                    var tasks = _SOP10107RepositoryDataRepository.GetAll();
                    IList<SOP10107> result = tasks;
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
        public IList<SOP10107> GetAllByInvoice(string SOPNUMBE, string SOPTypeSetupID, int SOPTYPE)
        {
            var tasks = _SOP10107RepositoryDataRepository.GetAll(ss => ss.SOPNUMBE.Trim() == SOPNUMBE.Trim());
            IList<SOP10107> result = tasks;
            return result;
        }
        public IList<Sys00007> GetAllSys00007()
        {
            Sys00007Repository r = new Sys00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<Sys00007> result = tasks;
            return result;
        }

        public bool AddSOP10107(SOP10107 newTask)
        {
            try
            {
                _SOP10107RepositoryDataRepository.Add(newTask);

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
        public bool Update(SOP10107 UpdateSOP10107)
        {
            _SOP10107RepositoryDataRepository.Update(UpdateSOP10107);
            return true;
        }
    }
}
