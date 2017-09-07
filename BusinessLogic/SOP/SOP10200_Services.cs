using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP10200Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10200Repository _SOP10200Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10200Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10200Repository = new SOP10200Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP10200> GetAllViewBYSQLQuery(string SOPTypeSetupID, string Filters,
 string FieldID, int FltrOperator, string Searchcritaria,
 int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            else
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = " and ";
                if (FieldID == "-1")
                {
                    SeachStr += Help.GetFilter("SOPNUMBE", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BatchID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CUSTNMBR", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CustomerPoNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            SOP10200Repository rep = new SOP10200Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<SOP10200> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetListWithPaging(ss => ss.SOPTypeSetupID == SOPTypeSetupID, PageSize, Page, ss => Member);
            else
                result = rep.GetWhereListWithPaging("SOP10200", PageSize, Page, Searchcritaria, Member, IsAscending);
            return result;
        }

        public DataCollection<SOP10200> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
          int PageSize, int Page, string Member, bool IsAscending, string SOPTypeSetupID, int TransactionType)
        {
            DataCollection<SOP10200> result = null;
            string SeachStr = Filters;
            if (!string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr += " and ";
                SeachStr += " ItemID like '%" + Searchcritaria.Trim() + "%'";
                SeachStr += " and ClassID like '%" + Searchcritaria.Trim() + "%'";
                SeachStr += " and ShortDescription like '%" + Searchcritaria.Trim() + "%'";
                SeachStr += " and GenericDescription like '%" + Searchcritaria.Trim() + "%'";
                SeachStr += " and ItemDescription like '%" + Searchcritaria.Trim() + "%'";
            }
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _SOP10200Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _SOP10200Repository.GetWhereListWithPaging("SOP10200", PageSize, Page, SeachStr, Member, IsAscending);
            }
            return result;
        }
        public DataCollection<SOP10200> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<SOP10200> L = null;
            var tasks = _SOP10200Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<SOP10200> result = tasks;
            return result;
        }
        public DataCollection<SOP10200> GetListWithPaging(int SOPTYPE, int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<SOP10200> result = null;
            var tasks = _SOP10200Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }


        public DataCollection<SOP10200> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP10200> L = null;
            var tasks = _SOP10200Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<SOP10200> result = tasks;
            return result;
        }
        public DataCollection<SOP10200> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP10200> result = null;
            var tasks = _SOP10200Repository.GetListWithPaging(PageSize, Page, ss => new { ss.SOPNUMBE, ss.SOPTypeSetupID });
            result = tasks;
            return result;
        }
        public DataCollection<SOP10200> GetListWithPaging(int SOPTYPE, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP10200> result = null;
            var tasks = _SOP10200Repository.GetListWithPaging(PageSize, Page, ss => new { ss.SOPNUMBE, ss.SOPTypeSetupID });
            result = tasks;
            return result;
        }

        public IList<SOP10200> GetListByCUSTNMBR(string CUSTNMBR)
        {
            IList<SOP10200> result = null;
            var tasks = _SOP10200Repository.GetAll(ss => ss.CUSTNMBR == CUSTNMBR);
            result = tasks;
            return result;
        }

        public IList<SOP10200> GetAll()
        {
            try
            {
                IList<SOP10200> L = null;
                try
                {
                    var tasks = _SOP10200Repository.GetAll();
                    IList<SOP10200> result = tasks;
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
        public SOP10200 GetSingle(string SOPNUMBE, string SOPTypeSetupID)
        {
            var tasks = _SOP10200Repository.GetSingle(x => x.SOPNUMBE == SOPNUMBE && x.SOPTypeSetupID == SOPTypeSetupID);
            return tasks;
        }
        public IList<SOP00000> GetAllSOP00000()
        {
            SOP00000Repository r = new SOP00000Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<SOP00000> result = tasks;
            return result;
        }
        public IList<SOP00024> GetAllSOP00024()
        {
            SOP00024Repository r = new SOP00024Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<SOP00024> result = tasks;
            return result;
        }

        public bool AddSOP10200(SOP10200 newTask)
        {
            _SOP10200Repository.Add(newTask);
            return true;
        }
        public bool Update(SOP10200 UpdateSOP10200)
        {

            try
            {
                _SOP10200Repository.Update(UpdateSOP10200);
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
        public bool Delete(string SOPNUMBE)
        {
            try
            {
                _SOP10200Repository.ExecuteSqlCommand("delete SOP10200 where SOPNUMBE='" + SOPNUMBE + "'");
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
