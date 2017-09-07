using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00004Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00004Repository _GL00004Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00004Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00004Repository = new GL00004Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00004> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00004> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00004Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00004Repository.GetWhereListWithPaging("GL00004", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00004> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("BatchID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BatchName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            GL00004Repository rep = new GL00004Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<GL00004> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("GL00004", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00004> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<GL00004> L = null;
            var tasks = _GL00004Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<GL00004> result = tasks;
            return result;
        }
        public DataCollection<GL00004> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<GL00004> result = null;
            var tasks = _GL00004Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<GL00004> GetAll()
        {
            try
            {
                IList<GL00004> L = null;
                try
                {
                    var tasks = _GL00004Repository.GetAll();
                    IList<GL00004> result = tasks;
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
        public DataCollection<GL00004> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<GL00004> L = null;
                var tasks = _GL00004Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<GL00004> result = tasks;
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

        public DataCollection<GL00004> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00004> result = null;
            _GL00004Repository = new GL00004Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _GL00004Repository.GetListWithPaging(SearchTerm,
                PageSize, Page, ss => ss.BatchID, true);
            result = tasks;
            return result;
        }
        public DataCollection<GL00004> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00004> result = null;
            var tasks = _GL00004Repository.GetListWithPaging(PageSize, Page, ss => new { ss.BatchID });
            result = tasks;
            return result;
        }
        public GL00004 GetSingle(string BatchID)
        {
            var tasks = _GL00004Repository.GetSingle(x => x.BatchID == BatchID);
            return tasks;
        }

        public KaizenResult AddGL00004(GL00004 newTask)
        {
            var result = _GL00004Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00004(IList<GL00004> newTask)
        {
            var result = _GL00004Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00004 newTask)
        {
            var result = _GL00004Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00004> newTask)
        {
            var result = _GL00004Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00004 newTask)
        {
            var result = _GL00004Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00004> newTask)
        {
            var result = _GL00004Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
