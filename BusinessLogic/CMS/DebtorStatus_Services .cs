using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00014Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00014Repository _CM00014Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00014Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00014Repository = new CM00014Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00014> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00014> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00014Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00014Repository.GetWhereListWithPaging("CM00014", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00014> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<CM00014> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00014Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00014Repository.GetWhereListWithPaging("CM00014", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<CM00014> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00014> L = null;
            var tasks = _CM00014Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00014> result = tasks;
            return result;
        }
        public DataCollection<CM00014> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00014> result = null;
            var tasks = _CM00014Repository.GetListWithPaging(PageSize, Page, ss => new { ss.DebtorStatusID });
            result = tasks;
            return result;
        }
        public IList<CM00014> GetAll()
        {
            try
            {
                IList<CM00014> L = null;
                try
                {
                    var tasks = _CM00014Repository.GetAll();
                    IList<CM00014> result = tasks;
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
        public CM00014 GetSingle(int DebtorStatusID)
        {
            try
            {
                var tasks = _CM00014Repository.GetSingle(x => x.DebtorStatusID == DebtorStatusID);
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

        public KaizenResult AddCM00014(CM00014 newTask)
        {
            KaizenResult result = _CM00014Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00014(IList<CM00014> newTask)
        {
            KaizenResult result = _CM00014Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00014 newTask)
        {
            KaizenResult result = _CM00014Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00014> newTask)
        {
            KaizenResult result = _CM00014Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00014 newTask)
        {
            KaizenResult result = _CM00014Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00014> newTask)
        {
            KaizenResult result = _CM00014Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
