using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00301Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00301Repository _GL00301RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00301Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00301RepositoryDataRepository = new GL00301Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00301> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<GL00301> L = null;
            var tasks = _GL00301RepositoryDataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<GL00301> result = tasks;
            return result;
        }
        public DataCollection<GL00301> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<GL00301> result = null;
            var tasks = _GL00301RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<GL00301> GetAll()
        {
            try
            {
                IList<GL00301> L = null;
                try
                {
                    var tasks = _GL00301RepositoryDataRepository.GetAll();
                    IList<GL00301> result = tasks;
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
        public DataCollection<GL00301> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<GL00301> L = null;
                var tasks = _GL00301RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<GL00301> result = tasks;
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
        public DataCollection<GL00301> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {

            DataCollection<GL00301> result = null;
            var tasks = _GL00301RepositoryDataRepository.GetListWithPaging(x => x.BudgetID.Contains(SearchTerm) ||
                x.ACTNUMBR.Contains(SearchTerm) || x.AccountID.ToString().Contains(SearchTerm) || x.AMT.ToString().Contains(SearchTerm)
                , PageSize, Page, ss => new { ss.BudgetID });
            result = tasks;
            return result;
        }
        public DataCollection<GL00301> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00301> result = null;
            var tasks = _GL00301RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.BudgetID });
            result = tasks;
            return result;
        }
        public IList<GL00301> GetByAccountIDAndBudgetID(int AccountID, string BudgetID)
        {
            var tasks = _GL00301RepositoryDataRepository.GetAll(x => x.BudgetID == BudgetID && x.AccountID == AccountID);
            return tasks;
        }
        public List<GL00301> GetAllByYearPeriod(int AccountID, string BudgetID, string YearCode)
        {
            List<GL00301> result = new List<GL00301>();
            var tasks = _GL00301RepositoryDataRepository.GetAll(x => x.BudgetID == BudgetID
            && x.AccountID == AccountID && x.GL00003.YearCode == YearCode, ss => new { ss.PERIODID }, ee => ee.GL00003);
            if (tasks.Count == 0)
            {
                GL00003Services oGL00003Services = new GL00003Services(UserContext);
                IList<GL00003> L = oGL00003Services.GetAllByYearCode(YearCode);
                foreach (GL00003 obj in L)
                {
                    result.Add(new GL00301() { PERIODID = obj.PERIODID, GL00003 = obj });
                }
            }
            else
            {
                foreach (var item in tasks)
                {
                    GL00003 _GL00003 = new GL00003()
                    {
                        PERIODID = item.GL00003.PERIODID,
                        YearCode = item.GL00003.YearCode,
                        PeriodName = item.GL00003.PeriodName,
                        StartDate = item.GL00003.StartDate,
                        EndDate = item.GL00003.EndDate,
                        IsClosed = item.GL00003.IsClosed,
                        IsSalesClosed = item.GL00003.IsSalesClosed,
                        IsPurchaseClosed = item.GL00003.IsPurchaseClosed,
                        IsInventoryClosed = item.GL00003.IsInventoryClosed,
                        PayrollClosed = item.GL00003.PayrollClosed,
                        IsManufacturingClosed = item.GL00003.IsManufacturingClosed,
                        IsExpenseManagementClosed = item.GL00003.IsExpenseManagementClosed,
                        IsPOSClosed = item.GL00003.IsPOSClosed,
                        IsBankClosed = item.GL00003.IsBankClosed
                    };
                    GL00301 obj = new GL00301()
                    {
                        AccountID = item.AccountID,
                        ACTNUMBR = item.ACTNUMBR,
                        BudgetID = item.BudgetID,
                        PERIODID = item.PERIODID,
                        AMT = item.AMT,
                        GL00003 = _GL00003
                    };
                    result.Add(obj);
                }
            }
            return result;
        }

        public KaizenResult AddGL00301(GL00301 newTask)
        {
            var result = _GL00301RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00301(IList<GL00301> newTask)
        {
            var result = _GL00301RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00301 newTask)
        {
            var result = _GL00301RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00301> newTask)
        {
            var result = _GL00301RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00301 newTask)
        {
            var result = _GL00301RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00301> newTask)
        {
            var result = _GL00301RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
