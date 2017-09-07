using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;

namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00001Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00001Repository _IV00001Repository;
        private Kaizen.Inventory.Repository.IV00030Repository _IV00030Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00001Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00001Repository = new IV00001Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            _IV00030Repository = new IV00030Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00001> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00001> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00001Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00001Repository.GetWhereListWithPaging("IV00001", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00001> GetAllViewBYSQLQuery(string Filters,
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

            IV00001Repository rep = new IV00001Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<IV00001> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("IV00001", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00001> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<IV00001> L = null;
            var tasks = _IV00001Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<IV00001> result = tasks;
            return result;
        }

        public DataCollection<IV00001> GetListWithPaging(string SearchTerm,string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                DataCollection<IV00001> result = null;
                var tasks = _IV00001Repository.GetListWithPaging(x => x.ClassID.ToString().Contains(SearchTerm)
                    || x.GroupName.Contains(SearchTerm)
                    , PageSize, Page, ss => ss.ClassID, null);
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
        public DataCollection<IV00001> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<IV00001> result = null;
            var tasks = _IV00001Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ClassID });
            result = tasks;
            return result;
        }

        public IV00001 GetSingle(string ClassID)
        {
            try
            {
                var tasks = _IV00001Repository.GetSingle(x => x.ClassID == ClassID);
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

        public IList<IV00001> GetAll()
        {
            try
            {
                IList<IV00001> L = null;
                try
                {
                    var tasks = _IV00001Repository.GetAll();
                    IList<IV00001> result = tasks;
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

        public KaizenResult AddIV00001(IV00001 newTask)
        {
            var result = _IV00001Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00001(IList<IV00001> newTask)
        {
            var result = _IV00001Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00001 newTask)
        {
            var result = _IV00001Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00001> newTask)
        {
            var result = _IV00001Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00001 newTask)
        {
            var result = _IV00001Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00001> newTask)
        {
            var result = _IV00001Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _IV00001Repository.ExecuteSqlCommand(myQuery);
            return result;
        }


        #region--> IV00030
        public KaizenResult AddIV00030(IList<IV00030> newTask)
        {
            var result = _IV00030Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult UpdateIV00030(IList<IV00030> newTask)
        {
            var result = _IV00030Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public IList<IV00030> GetAllIV00030byClassID(string ClassID)
        {
            var result = _IV00030Repository.GetAll(m=>m.ClassID.Trim().Equals(ClassID.Trim()));
            return result;
        }
        #endregion
    }
}
