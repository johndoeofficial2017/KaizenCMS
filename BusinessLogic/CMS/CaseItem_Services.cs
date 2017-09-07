using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00209Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00209Repository _CM00209Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00209Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00209Repository = new CM00209Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CMS_203View> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            Kaizen.CMS.Repository.CMS_203ViewRepository _CMS_203ViewRepository = new CMS_203ViewRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CMS_203ViewRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CMS_203View> result = tasks;
            return result;
        }
        public DataCollection<CMS_203View> GetAllBYSQLQueryView(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            Kaizen.CMS.Repository.CMS_203ViewRepository _CM_ClientRepository = new CMS_203ViewRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM_ClientRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CMS_203View> result = tasks;
            return result;
        }
        public DataCollection<CMS_203View> GetSQLData(string Searchcritaria, int ViewID, int PageSize, int Page, string Member, string SortDirection)
        {
            Page = Page - 1;
            DataCollection<CMS_203View> result = null;

            Kaizen.CMS.Repository.CMS_203ViewRepository _CM00100ViewRepository = new CMS_203ViewRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            string sql = "select * from CMS_203View " + Searchcritaria + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
            var tasks = _CM00100ViewRepository.GetSQLData(sql);
            if (tasks != null)
            {
                if (string.IsNullOrEmpty(Searchcritaria))
                    tasks.TotalItemCount = (int)_CM00100ViewRepository.Count();
                else
                    tasks.TotalItemCount = _CM00100ViewRepository.GetSQLINT("select count(ClientID) from CMS_203View " + Searchcritaria);
                tasks.TotalPageCount = (int)Math.Ceiling((double)tasks.TotalItemCount / (double)PageSize);
            }
            result = tasks;
            return result;
        }
        public IList<CM00209> GetAll()
        {
            var tasks = _CM00209Repository.GetAll();
            IList<CM00209> result = tasks;
            return result;
        }

        public DataCollection<CM00209> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member
    , string SortDirection)
        {
            DataCollection<CM00209> result = null;

            var tasks = _CM00209Repository.GetListWithPaging(SearchTerm,
                PageSize, Page, ss => ss.TransactionID, true);
            result = tasks;
            return result;
        }

        public DataCollection<CM00209> GetListWithPaging(string SearchTerm, string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                DataCollection<CM00209> result = null;

                if (string.IsNullOrEmpty(whereClause))
                {
                    //var tasks = _CM00209Repository.GetListWithPaging(PageSize, Page, ss => ss.CaseRef, null, xx => xx.CM00100, cc => cc.CM00201, xx => xx.CM00700);
                    result = null;
                }
                else
                {
                    // var tt = _CM00209Repository.Testing(whereClause);
                    var tasks = _CM00209Repository.GetListWithPaging(whereClause, PageSize, Page, ss => ss.TransactionID, null, xx => xx.CM00210);
                    result = tasks;
                }
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
        public DataCollection<CM00209> GetByDebtorID(string DebtorID, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<CM00209> L = null;
                //var tasks = _CM00209Repository.GetListWithPaging(x => (x.DebtorID.Contains(DebtorID)
                //    ), PageSize, Page, ss => ss.DebtorID, null, xx => xx.CM00100, cc => cc.CM00201, xx => xx.CM00700);

                DataCollection<CM00209> result = null;
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

        public CM00209 GetSingle(string TransactionID)
        {
            var tasks = _CM00209Repository.GetSingle(x => x.TransactionID.Trim() == TransactionID.Trim());
            return tasks;
        }

        public KaizenResult AddCM00209(CM00209 newTask)
        {
            var result = _CM00209Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00209(IList<CM00209> newTask)
        {
            var result = _CM00209Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00209 newTask)
        {
            var result = _CM00209Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00209> newTask)
        {
            var result = _CM00209Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00209 newTask)
        {
            var result = _CM00209Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00209> newTask)
        {
            var result = _CM00209Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
