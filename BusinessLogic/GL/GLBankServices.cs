using System;
using System.Collections.Generic;
using System.Linq;
using Kaizen.GL.Repository;
using Kaizen.GL;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00010Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00010Repository _GL00010DataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00010Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00010DataRepository = new GL00010Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00010> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00010> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00010DataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00010DataRepository.GetWhereListWithPaging("GL00010", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00010> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("BankCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BankName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            GL00010Repository rep = new GL00010Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<GL00010> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("GL00010", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00010> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<GL00010> L = null;
            var tasks = _GL00010DataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<GL00010> result = tasks;
            return result;
        }
        public DataCollection<GL00010> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<GL00010> result = null;
            var tasks = _GL00010DataRepository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<GL00010> GetAll()
        {
            try
            {
                IList<GL00010> L = null;
                try
                {
                    var tasks = _GL00010DataRepository.GetAll();
                    IList<GL00010> result = tasks;
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
        public DataCollection<GL00010> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<GL00010> L = null;
                var tasks = _GL00010DataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<GL00010> result = tasks;
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
        public DataCollection<GL00010> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00010> result = null;
            _GL00010DataRepository = new GL00010Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _GL00010DataRepository.GetListWithPaging(SearchTerm,
                PageSize, Page, ss => ss.BankCode, true);
            result = tasks;
            return result;
        }
        public DataCollection<GL00010> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00010> result = null;
            var tasks = _GL00010DataRepository.GetListWithPaging(PageSize, Page,ss => new { ss.BankCode });
            result = tasks;
            return result;
        }
        public GL00010 GetSingle(string BankCode)
        {
            var tasks = _GL00010DataRepository.GetSingle(x => x.BankCode == BankCode);
            return tasks;
        }

        public KaizenResult AddGL00010(GL00010 newTask)
        {
            var result = _GL00010DataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00010(IList<GL00010> newTask)
        {
            var result = _GL00010DataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00010 newTask)
        {
            var result = _GL00010DataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00010> newTask)
        {
            var result = _GL00010DataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00010 newTask)
        {
            var result = _GL00010DataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00010> newTask)
        {
            var result = _GL00010DataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
