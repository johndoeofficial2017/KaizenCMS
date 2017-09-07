using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00020Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00020Repository _GL00020Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00020Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00020Repository = new GL00020Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00020> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00020> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00020Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00020Repository.GetWhereListWithPaging("GL00020", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00020> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CategoryID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CategoryName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            GL00020Repository rep = new GL00020Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<GL00020> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("GL00020", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public IList<GL00020> GetAll()
        {
            var tasks = _GL00020Repository.GetAll();
            IList<GL00020> result = tasks;
            return result;
        }
        public int GetIDByName(string CategoryName)
        {
            var tasks = _GL00020Repository.GetAll(xx => xx.CategoryName.Trim() == CategoryName.Trim()).FirstOrDefault();
            return tasks.CategoryID;
        }
        public DataCollection<GL00020> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<GL00020> L = null;
                var tasks = _GL00020Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<GL00020> result = tasks;
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
        public DataCollection<GL00020> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {

                DataCollection<GL00020> result = null;

                var tasks = _GL00020Repository.GetListWithPaging(x => x.CategoryName.ToString().Contains(SearchTerm)
                    , PageSize, Page, ss => new { ss.CategoryID });
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
        public DataCollection<GL00020> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00020> result = null;
            var tasks = _GL00020Repository.GetListWithPaging(PageSize, Page, ss => new { ss.CategoryID });
            result = tasks;
            return result;
        }
        public GL00020 GetSingle(int AccountID)
        {
            var tasks = _GL00020Repository.GetSingle(x => x.CategoryID == AccountID);
            return tasks;
        }

        public KaizenResult AddGL00020(GL00020 newTask)
        {
            var result = _GL00020Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00020(IList<GL00020> newTask)
        {
            var result = _GL00020Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00020 newTask)
        {
            var result = _GL00020Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00020> newTask)
        {
            var result = _GL00020Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00020 newTask)
        {
            var result = _GL00020Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00020> newTask)
        {
            var result = _GL00020Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
