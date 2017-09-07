using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00004Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00004Repository _IV00004Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00004Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00004Repository = new IV00004Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00004> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00004> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00004Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00004Repository.GetWhereListWithPaging("IV00004", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00004> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<IV00004> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00004Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00004Repository.GetWhereListWithPaging("IV00004", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00004> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<IV00004> L = null;
            var tasks = _IV00004Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<IV00004> result = tasks;
            return result;
        }

        public DataCollection<IV00004> GetListWithPaging(string SearchTerm,string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                DataCollection<IV00004> result = null;
                var tasks = _IV00004Repository.GetListWithPaging(x => x.PriceLevelCode.ToString().Contains(SearchTerm)
                    || x.PriceLevelName.Contains(SearchTerm)
                    , PageSize, Page, ss => ss.PriceLevelCode, null);
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
        public DataCollection<IV00004> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<IV00004> result = null;
            var tasks = _IV00004Repository.GetListWithPaging(PageSize, Page, ss => new { ss.PriceLevelCode });
            result = tasks;
            return result;
        }

        public IV00004 GetSingle(int PriceLevelCode)
        {
            try
            {
                var tasks = _IV00004Repository.GetSingle(x => x.PriceLevelCode == PriceLevelCode);
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

        public IList<IV00004> GetAll()
        {
            try
            {
                IList<IV00004> L = null;
                try
                {
                    var tasks = _IV00004Repository.GetAll();
                    IList<IV00004> result = tasks;
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

        public KaizenResult AddIV00004(IV00004 newTask)
        {
            var result = _IV00004Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00004(IList<IV00004> newTask)
        {
            var result = _IV00004Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00004 newTask)
        {
            var result = _IV00004Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00004> newTask)
        {
            var result = _IV00004Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00004 newTask)
        {
            var result = _IV00004Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00004> newTask)
        {
            var result = _IV00004Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
