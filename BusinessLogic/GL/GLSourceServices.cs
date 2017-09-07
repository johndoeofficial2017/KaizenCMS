using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00021Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00021Repository _GL00021DataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00021Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00021DataRepository = new GL00021Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00021> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00021> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00021DataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00021DataRepository.GetWhereListWithPaging("GL00021", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00021> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("SourceID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SourceName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            GL00021Repository rep = new GL00021Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<GL00021> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("GL00021", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00021> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<GL00021> L = null;
            var tasks = _GL00021DataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<GL00021> result = tasks;
            return result;
        }
        public DataCollection<GL00021> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<GL00021> result = null;
            var tasks = _GL00021DataRepository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<GL00021> GetAll()
        {
            try
            {
                IList<GL00021> L = null;
                try
                {
                    var tasks = _GL00021DataRepository.GetAll();
                    IList<GL00021> result = tasks;
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
        public DataCollection<GL00021> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<GL00021> L = null;
                var tasks = _GL00021DataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<GL00021> result = tasks;
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
        public DataCollection<GL00021> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00021> result = null;
            _GL00021DataRepository = new GL00021Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _GL00021DataRepository.GetListWithPaging(SearchTerm,
                PageSize, Page, ss => ss.SourceID, true);
            result = tasks;
            return result;
        }
        public DataCollection<GL00021> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00021> result = null;
            var tasks = _GL00021DataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.SourceID });
            result = tasks;
            return result;
        }
        public GL00021 GetSingle(string SourceID)
        {
            var tasks = _GL00021DataRepository.GetSingle(x => x.SourceID == SourceID);
            return tasks;
        }

        public KaizenResult AddGL00021(GL00021 newTask)
        {
            var result = _GL00021DataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00021(IList<GL00021> newTask)
        {
            var result = _GL00021DataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00021 newTask)
        {
            var result = _GL00021DataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00021> newTask)
        {
            var result = _GL00021DataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00021 newTask)
        {
            var result = _GL00021DataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00021> newTask)
        {
            var result = _GL00021DataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
