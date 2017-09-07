using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00005Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00005Repository _IV00005Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00005Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00005Repository = new IV00005Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00005> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00005> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00005Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00005Repository.GetWhereListWithPaging("IV00005", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00005> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<IV00005> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00005Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00005Repository.GetWhereListWithPaging("IV00005", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00005> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<IV00005> L = null;
            var tasks = _IV00005Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<IV00005> result = tasks;
            return result;
        }

        public DataCollection<IV00005> GetListWithPaging(string SearchTerm, string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                DataCollection<IV00005> result = null;
                var tasks = _IV00005Repository.GetListWithPaging(x => x.PriceGroupID.ToString().Contains(SearchTerm)
                    || x.PriceGroupName.Contains(SearchTerm)
                    , PageSize, Page, ss => ss.PriceGroupID, null);
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
        public DataCollection<IV00005> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<IV00005> result = null;
            var tasks = _IV00005Repository.GetListWithPaging(PageSize, Page, ss => new { ss.PriceGroupID });
            result = tasks;
            return result;
        }

        public IV00005 GetSingle(int PriceGroupID)
        {
            try
            {
                var tasks = _IV00005Repository.GetSingle(x => x.PriceGroupID == PriceGroupID);
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

        public IList<IV00005> GetAll()
        {
            try
            {
                IList<IV00005> L = null;
                try
                {
                    var tasks = _IV00005Repository.GetAll();
                    IList<IV00005> result = tasks;
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

        public KaizenResult AddIV00005(IV00005 newTask)
        {
            var result = _IV00005Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00005(IList<IV00005> newTask)
        {
            var result = _IV00005Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00005 newTask)
        {
            var result = _IV00005Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00005> newTask)
        {
            var result = _IV00005Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00005 newTask)
        {
            var result = _IV00005Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00005> newTask)
        {
            var result = _IV00005Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
