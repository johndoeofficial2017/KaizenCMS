using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00008Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00008Repository _IV00008Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00008Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00008Repository = new IV00008Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00008> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00008> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00008Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00008Repository.GetWhereListWithPaging("IV00008", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00008> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<IV00008> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00008Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00008Repository.GetWhereListWithPaging("IV00008", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00008> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<IV00008> L = null;
            var tasks = _IV00008Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<IV00008> result = tasks;
            return result;
        }

        public DataCollection<IV00008> GetListWithPaging(string SearchTerm,string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                DataCollection<IV00008> result = null;
                var tasks = _IV00008Repository.GetListWithPaging(x => x.ItemTypeID.ToString().Contains(SearchTerm)
                    || x.ItemTypeName.Contains(SearchTerm)
                    , PageSize, Page, ss => ss.ItemTypeID, null);
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
        public DataCollection<IV00008> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<IV00008> result = null;
            var tasks = _IV00008Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ItemTypeID });
            result = tasks;
            return result;
        }

        public IV00008 GetSingle(int ItemTypeID)
        {
            try
            {
                var tasks = _IV00008Repository.GetSingle(x => x.ItemTypeID == ItemTypeID);
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

        public IList<IV00008> GetAll()
        {
            try
            {
                IList<IV00008> L = null;
                try
                {
                    var tasks = _IV00008Repository.GetAll();
                    IList<IV00008> result = tasks;
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

        public KaizenResult AddIV00008(IV00008 newTask)
        {
            var result = _IV00008Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00008(IList<IV00008> newTask)
        {
            var result = _IV00008Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00008 newTask)
        {
            var result = _IV00008Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00008> newTask)
        {
            var result = _IV00008Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00008 newTask)
        {
            var result = _IV00008Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00008> newTask)
        {
            var result = _IV00008Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
