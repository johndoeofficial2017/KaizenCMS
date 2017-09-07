using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00210Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00210Repository _CM00210RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00210Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00210RepositoryDataRepository = new CM00210Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

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
        public IList<CM00210> GetAll()
        {
            var tasks = _CM00210RepositoryDataRepository.GetAll();
            IList<CM00210> result = tasks;
            return result;
        }
   
        public DataCollection<CM00210> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member
    , string SortDirection)
        {
            DataCollection<CM00210> result = null;

            var tasks = _CM00210RepositoryDataRepository.GetListWithPaging(SearchTerm,
                PageSize, Page, ss => ss.CaseRef, true);
            result = tasks;
            return result;
        }

        public DataCollection<CM00210> GetListWithPaging(string SearchTerm,string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                DataCollection<CM00210> result = null;

                if (string.IsNullOrEmpty(whereClause))
                {
                    //var tasks = _CM00210RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => ss.CaseRef, null, xx => xx.CM00100, cc => cc.CM00201, xx => xx.CM00700);
                    result = null;
                }
                else
                {
                   // var tt = _CM00210RepositoryDataRepository.Testing(whereClause);
                    var tasks = _CM00210RepositoryDataRepository.GetListWithPaging(whereClause, PageSize, Page, ss => ss.CaseRef, null, xx => xx.CM00209);
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
        public DataCollection<CM00210> GetByDebtorID(string DebtorID, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<CM00210> L = null;
                //var tasks = _CM00210RepositoryDataRepository.GetListWithPaging(x => (x.DebtorID.Contains(DebtorID)
                //    ), PageSize, Page, ss => ss.DebtorID, null, xx => xx.CM00100, cc => cc.CM00201, xx => xx.CM00700);

                DataCollection<CM00210> result = null ;
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

        public CM00210 GetSingle(string CaseRef)
        {
            try
            {
                var tasks = _CM00210RepositoryDataRepository.GetSingle(x => x.CaseRef.Trim() == CaseRef.Trim());
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
  
        public bool AddCM00210(CM00210 newTask)
        {
            _CM00210RepositoryDataRepository.Add(newTask);
            return true;
           
        }
        public bool Update(CM00210 UpdateCM00210)
        {
            try
            {
                _CM00210RepositoryDataRepository.Update(UpdateCM00210);
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
