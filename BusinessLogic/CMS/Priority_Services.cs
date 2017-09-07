using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
namespace Kaizen.BusinessLogic.CMS
{
    public class CM00012Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00012Repository _CM00012Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00012Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00012Repository = new CM00012Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00012> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00012> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00012Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00012Repository.GetWhereListWithPaging("CM00012", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00012> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ActionTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00012> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00012Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00012Repository.GetWhereListWithPaging("CM00012", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<CM00012> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00012> L = null;
            var tasks = _CM00012Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00012> result = tasks;
            return result;
        }
        public DataCollection<CM00012> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00012> result = null;
            var tasks = _CM00012Repository.GetListWithPaging(PageSize, Page, ss => new { ss.PriorityID });
            result = tasks;
            return result;
        }
        public IList<CM00012> GetAll()
        {
            try
            {
                IList<CM00012> L = null;
                try
                {
                    var tasks = _CM00012Repository.GetAll();
                    IList<CM00012> result = tasks;
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
        public CM00012 GetSingle(int PriorityID)
        {
            try
            {
                var tasks = _CM00012Repository.GetSingle(x => x.PriorityID == PriorityID);
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

        public KaizenResult AddCM00012(CM00012 newTask)
        {
            KaizenResult result = _CM00012Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00012(IList<CM00012> newTask)
        {
            KaizenResult result = _CM00012Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00012 newTask)
        {
            KaizenResult result = _CM00012Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00012> newTask)
        {
            KaizenResult result = _CM00012Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00012 newTask)
        {
            KaizenResult result = _CM00012Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00012> newTask)
        {
            KaizenResult result = _CM00012Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}

