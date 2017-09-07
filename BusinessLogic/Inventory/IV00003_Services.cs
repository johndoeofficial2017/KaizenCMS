using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00003Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00003Repository _IV00003Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00003Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00003Repository = new IV00003Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00003> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00003> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00003Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00003Repository.GetWhereListWithPaging("IV00003", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00003> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ClassID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GroupName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00003> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00003Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00003Repository.GetWhereListWithPaging("IV00003", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00003> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string UFMGroupID,
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
                    SeachStr += Help.GetFilter("UFMID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BaesUnit", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00003> result = null;
            if (!string.IsNullOrEmpty(UFMGroupID))
                result = _IV00003Repository.GetListWithPaging(ss => ss.UFMGroupID.Trim() == UFMGroupID.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00003Repository.GetWhereListWithPaging("IV00003", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00003> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<IV00003> L = null;
            var tasks = _IV00003Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<IV00003> result = tasks;
            return result;
        }

        public DataCollection<IV00003> GetListWithPaging(string SearchTerm, string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                DataCollection<IV00003> result = null;
                var tasks = _IV00003Repository.GetListWithPaging(x => x.UFMID.ToString().Contains(SearchTerm)
                    , PageSize, Page, ss => ss.UFMID, null);
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
        public DataCollection<IV00003> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<IV00003> result = null;
            var tasks = _IV00003Repository.GetListWithPaging(PageSize, Page, ss => new { ss.UFMID });
            result = tasks;
            return result;
        }

        public IV00003 GetSingle(string UFMID)
        {
            try
            {
                var tasks = _IV00003Repository.GetSingle(x => x.UFMID == UFMID);
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

        public IList<IV00003> GetAll()
        {
            try
            {
                IList<IV00003> L = null;
                try
                {
                    var tasks = _IV00003Repository.GetAll();
                    IList<IV00003> result = tasks;
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
        public IList<IV00003> GetAllByUFMGroupID(string UFMGroupID)
        {
            try
            {
                IList<IV00003> L = null;
                try
                {
                    var tasks = _IV00003Repository.GetAll(x => x.UFMGroupID == UFMGroupID);
                    IList<IV00003> result = tasks;
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

        public KaizenResult AddIV00003(IV00003 newTask)
        {
            var result = _IV00003Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00003(IList<IV00003> newTask)
        {
            var result = _IV00003Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00003 newTask)
        {
            var result = _IV00003Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00003> newTask)
        {
            var result = _IV00003Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00003 newTask)
        {
            var result = _IV00003Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00003> newTask)
        {
            var result = _IV00003Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
