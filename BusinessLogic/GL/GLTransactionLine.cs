using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00201Services
    {
             #region Infrastructure

        private GL00201Repository _GL00201RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00201Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00201RepositoryDataRepository = new GL00201Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<GL00201> GetByTransactionID(int TransactionID, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<GL00201> L = null;
                var tasks = _GL00201RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => ss.LineID, null);
                DataCollection<GL00201> result = tasks;
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

        public IList<GL00201> GetAll()
        {
            try
            {
                IList<GL00201> L = null;
                try
                {
                    var tasks = _GL00201RepositoryDataRepository.GetAll();
                    IList<GL00201> result = tasks;
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
        public IList<GL00201> GetByTransactionID(int TransactionID,int PERIODID)
        {
            var tasks = _GL00201RepositoryDataRepository.GetAll(xx => xx.TransactionID == TransactionID && xx.PERIODID == PERIODID);
            IList<GL00201> result = tasks;
            return result;
        }

        public GL00201 GetSingle(int TransactionID)
        {
            var tasks = _GL00201RepositoryDataRepository.GetSingle(x => x.TransactionID == TransactionID);
            return tasks;
        }

        public KaizenResult AddGL00201(GL00201 newTask)
        {
            var result = _GL00201RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00201(IList<GL00201> newTask)
        {
            var result = _GL00201RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00201 newTask)
        {
            var result = _GL00201RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00201> newTask)
        {
            var result = _GL00201RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00201 newTask)
        {
            var result = _GL00201RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00201> newTask)
        {
            var result = _GL00201RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
