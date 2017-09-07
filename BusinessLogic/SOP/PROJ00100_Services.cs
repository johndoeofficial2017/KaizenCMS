using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
using System.Globalization;
namespace Kaizen.BusinessLogic.SOP
{
    public class PROJ00100Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.PROJ00100Repository _PROJ00100Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public PROJ00100Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _PROJ00100Repository = new PROJ00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<PROJ00100> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<PROJ00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _PROJ00100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _PROJ00100Repository.GetWhereListWithPaging("PROJ00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<PROJ00100> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ProjectID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ProjectName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ProjectDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<PROJ00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _PROJ00100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _PROJ00100Repository.GetWhereListWithPaging("PROJ00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<PROJ00100> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<PROJ00100> L = null;
            var tasks = _PROJ00100Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<PROJ00100> result = tasks;
            return result;
        }
        public DataCollection<PROJ00100> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<PROJ00100> result = null;
            var tasks = _PROJ00100Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public DataCollection<PROJ00100> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<PROJ00100> L = null;
                var tasks = _PROJ00100Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<PROJ00100> result = tasks;
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
        public DataCollection<PROJ00100> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<PROJ00100> result = null;
            var tasks = _PROJ00100Repository.GetListWithPaging(SearchTerm,
                PageSize, Page, ss => ss.ProjectID, true);
            result = tasks;
            return result;
        }
        public DataCollection<PROJ00100> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<PROJ00100> result = null;
            var tasks = _PROJ00100Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ProjectID });
            result = tasks;
            return result;
        }

        public DataCollection<PROJ00100> GetAllPROJ00100(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<PROJ00100> L = null;
                var tasks = _PROJ00100Repository.GetListWithPaging(x => x.ProjectID.Contains(SearchTerm), PageSize, Page, ss => ss.ProjectID, null);

                DataCollection<PROJ00100> result = tasks;
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
        public DataCollection<PROJ00100> GetByProjectID(string ProjectID, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<PROJ00100> L = null;
                var tasks = _PROJ00100Repository.GetListWithPaging(x => x.ProjectID.Trim() == ProjectID.Trim(), PageSize, Page, ss => ss.ProjectID, null);

                DataCollection<PROJ00100> result = tasks;
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

        public IList<PROJ00100> GetAll()
        {
            try
            {
                IList<PROJ00100> L = null;
                try
                {
                    var tasks = _PROJ00100Repository.GetAll();
                    IList<PROJ00100> result = tasks;
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
        public IList<PROJ00100> GetAllByProjectID(string ProjectID)
        {
            var tasks = _PROJ00100Repository.GetAll(ss => ss.ProjectID.Trim() == ProjectID.Trim()
                , ss => new { ss.ProjectID });
            IList<PROJ00100> result = tasks;
            return result;
        }
        public PROJ00100 GetSingle(string ProjectID)
        {
            try
            {
                var tasks = _PROJ00100Repository.GetSingle(x => x.ProjectID.Trim() == ProjectID.Trim());
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

        public KaizenResult AddPROJ00100(PROJ00100 newTask)
        {
            var result = _PROJ00100Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddPROJ00100(IList<PROJ00100> newTask)
        {
            var result = _PROJ00100Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(PROJ00100 newTask)
        {
            var result = _PROJ00100Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<PROJ00100> newTask)
        {
            var result = _PROJ00100Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(PROJ00100 newTask)
        {
            var result = _PROJ00100Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<PROJ00100> newTask)
        {
            var result = _PROJ00100Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
