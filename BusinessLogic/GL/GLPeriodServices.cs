using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00003Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00003Repository _GL00003RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00003Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00003RepositoryDataRepository = new GL00003Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public IList<GL00003> GetAll()
        {
            try
            {
                IList<GL00003> L = null;
                try
                {
                    var tasks = _GL00003RepositoryDataRepository.GetAll();
                    IList<GL00003> result = tasks;
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
        public DataCollection<GL00003> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<GL00003> L = null;
                var tasks = _GL00003RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<GL00003> result = tasks;
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
        public DataCollection<GL00003> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {

                DataCollection<GL00003> result = null;

                var tasks = _GL00003RepositoryDataRepository.GetListWithPaging(x => x.YearCode.ToString().Contains(SearchTerm) ||
                    x.PeriodName.Contains(SearchTerm)
                    , PageSize, Page, ss => new { ss.YearCode });
                result = tasks;
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
        public DataCollection<GL00003> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00003> result = null;
            var tasks = _GL00003RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.PERIODID });
            result = tasks;
            return result;
        }
        public IList<GL00003> GetAllByYearCode(string YearCode)
        {
            IList<GL00003> result = null;
            var tasks = _GL00003RepositoryDataRepository.GetAll(x => x.YearCode == YearCode);
            result = tasks;
            return result;
        }
        public GL00003 GetSinglePeriod(DateTime TransactionDate,string YearCode)
        {
            var tasks = _GL00003RepositoryDataRepository.GetSingle(x => x.StartDate <= TransactionDate 
            && x.EndDate >= TransactionDate 
            && x.YearCode == YearCode);
            return tasks;
        }
        public GL00003 GetSinglePeriod(DateTime TransactionDate)
        {
            var tasks = _GL00003RepositoryDataRepository.GetSingle(x => x.StartDate <= TransactionDate
            && x.EndDate >= TransactionDate);
            return tasks;
        }
        public GL00003 GetSingle(int PERIODID)
        {
            var tasks = _GL00003RepositoryDataRepository.GetSingle(x => x.PERIODID == PERIODID);
            return tasks;
        }

        public KaizenResult AddGL00003(GL00003 newTask)
        {
            var result = _GL00003RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00003(IList<GL00003> newTask)
        {
            var result = _GL00003RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00003 newTask)
        {
            var result = _GL00003RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00003> newTask)
        {
            var result = _GL00003RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00003 newTask)
        {
            var result = _GL00003RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00003> newTask)
        {
            var result = _GL00003RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
