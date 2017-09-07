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
    public class SOP10104Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10104Repository _SOP10104RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10104Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10104RepositoryDataRepository = new SOP10104Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<SOP10104> GetAllSOP10104(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<SOP10104> L = null;
                var tasks = _SOP10104RepositoryDataRepository.GetListWithPaging( PageSize, Page, ss => ss.BinID, null);

                DataCollection<SOP10104> result = tasks;
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
        public IList<SOP10104> GetAllBySOPNUMBE(int LineID)
        {
            var tasks = _SOP10104RepositoryDataRepository.GetAll(ss => ss.LineID == LineID
              , ss => new { ss.BinID });
            IList<SOP10104> result = tasks;
            return result;
        }
        public IList<Sys00007> GetAllSys00007()
        {
            Sys00007Repository r = new Sys00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<Sys00007> result = tasks;
            return result;
        }

        public bool AddSOP10104(SOP10104 newTask)
        {
            try
            {
                _SOP10104RepositoryDataRepository.Add(newTask);

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
        public bool Update(SOP10104 UpdateSOP10104)
        {
            _SOP10104RepositoryDataRepository.Update(UpdateSOP10104);
            return true;
        }
    }
}
