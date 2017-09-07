using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00303Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00303Repository _GL00303RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00303Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00303RepositoryDataRepository = new GL00303Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00303> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<GL00303> L = null;
            var tasks = _GL00303RepositoryDataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<GL00303> result = tasks;
            return result;
        }
        public DataCollection<GL00303> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<GL00303> result = null;
            var tasks = _GL00303RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<GL00303> GetAll()
        {
            var tasks = _GL00303RepositoryDataRepository.GetAll();
            IList<GL00303> result = tasks;
            return result;
        }
        public DataCollection<GL00303> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<GL00303> L = null;
            var tasks = _GL00303RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<GL00303> result = tasks;
            return result;
        }
        public DataCollection<GL00303> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00303> result = null;
            var tasks = _GL00303RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.TrxLine });
            result = tasks;
            return result;
        }
        public List<GL00303> GetAllByTransactionID(int? TransactionID, int YearCode)
        {
            //List<GL00303> result = new List<GL00303>();
            //var tasks = _GL00303RepositoryDataRepository.GetAll(x => x.TransactionID == TransactionID
            //, ss => new { ss.PERIODID }, ee => ee.GL00003);
            //if (tasks.Count == 0)
            //{
            //    GL00003Services oGL00003Services = new GL00003Services(UserContext);
            //    IList<GL00003> L = oGL00003Services.GetAllByYearCode(YearCode);
            //    foreach (GL00003 obj in L)
            //    {
            //        result.Add(new GL00303() { PERIODID = obj.PERIODID, GL00003 = obj });
            //    }
            //}
            //else
            //{
            //    foreach (var item in tasks)
            //    {
            //        GL00003 _GL00003 = new GL00003()
            //        {
            //            PERIODID = item.GL00003.PERIODID,
            //            YearCode = item.GL00003.YearCode,
            //            PeriodName = item.GL00003.PeriodName,
            //            StartDate = item.GL00003.StartDate,
            //            EndDate = item.GL00003.EndDate,
            //            IsClosed = item.GL00003.IsClosed,
            //            IsSalesClosed = item.GL00003.IsSalesClosed,
            //            IsPurchaseClosed = item.GL00003.IsPurchaseClosed,
            //            IsInventoryClosed = item.GL00003.IsInventoryClosed,
            //            PayrollClosed = item.GL00003.PayrollClosed,
            //            IsManufacturingClosed = item.GL00003.IsManufacturingClosed,
            //            IsExpenseManagementClosed = item.GL00003.IsExpenseManagementClosed,
            //            IsPOSClosed = item.GL00003.IsPOSClosed,
            //            IsBankClosed = item.GL00003.IsBankClosed
            //        };
            //        GL00303 obj = new GL00303()
            //        {
            //            TrxLine = item.TrxLine,
            //            TransactionID = item.TransactionID,
            //            PERIODID = item.PERIODID,
            //            AMT = item.AMT,
            //            GL00003 = _GL00003
            //        };
            //        result.Add(obj);
            //    }
            //}
            //return result;
            return null;
        }

        public KaizenResult AddGL00303(GL00303 newTask)
        {
            var result = _GL00303RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00303(IList<GL00303> newTask)
        {
            var result = _GL00303RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00303 newTask)
        {
            var result = _GL00303RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00303> newTask)
        {
            var result = _GL00303RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00303 newTask)
        {
            var result = _GL00303RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00303> newTask)
        {
            var result = _GL00303RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
