using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;
using System.Globalization;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00202Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00202Repository _GL00202RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00202Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00202RepositoryDataRepository = new GL00202Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<GL00202> GetAllGL00202(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<GL00202> L = null;
                var tasks = _GL00202RepositoryDataRepository.GetListWithPaging(x => x.DocumentName.Contains(SearchTerm), PageSize, Page, ss => ss.DocumentID, null);

                DataCollection<GL00202> result = tasks;
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

        public IList<GL00202> GetAllByTransaction(int TransactionID, int PERIODID)
        {
            var tasks = _GL00202RepositoryDataRepository.GetAll(ss => ss.TransactionID == TransactionID &&
                ss.PERIODID == PERIODID, ss => new { ss.DocumentID });
            IList<GL00202> result = tasks;
            return result;
        }

        public IList<GL00202> GetAll()
        {
            try
            {
                IList<GL00202> L = null;
                try
                {
                    var tasks = _GL00202RepositoryDataRepository.GetAll();
                    IList<GL00202> result = tasks;
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
        public IList<GL00202> GetAllByTransactionID(int TransactionID, int PERIODID)
        {
            var tasks = _GL00202RepositoryDataRepository.GetAll(ss => ss.TransactionID == TransactionID &&
                ss.PERIODID == PERIODID, ss => new { ss.DocumentID });
            IList<GL00202> result = tasks;
            return result;
        }
        public GL00202 GetSingle(int DocumentID)
        {
            try
            {
                var tasks = _GL00202RepositoryDataRepository.GetSingle(x => x.DocumentID == DocumentID);
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

        public IList<GL00005> GetAllGL00005()
        {
            GL00005Repository r = new GL00005Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<GL00005> result = tasks;
            return result;
        }

        public KaizenResult AddGL00202(GL00202 newTask)
        {
            var result = _GL00202RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00202(IList<GL00202> newTask)
        {
            var result = _GL00202RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00202 newTask)
        {
            var result = _GL00202RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00202> newTask)
        {
            var result = _GL00202RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00202 newTask)
        {
            var result = _GL00202RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00202> newTask)
        {
            var result = _GL00202RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
