using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00089Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00089Repository _CM00089Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00089Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00089Repository = new CM00089Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00089> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00089> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00089Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00089Repository.GetWhereListWithPaging("CM00089", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00089> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CheckbookCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00089> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00089Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00089Repository.GetWhereListWithPaging("CM00089", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }


        public DataCollection<CM00089> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM00089Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00089> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM00089Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00089> result = tasks;
                return result;
            }

        }
        public DataCollection<CM00089> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00089> result = null;
            var tasks = _CM00089Repository.GetListWithPaging(PageSize, Page, ss => new { ss.CheckbookCode });
            result = tasks;
            return result;
        }
        public IList<CM00089> GetAll()
        {
            try
            {
                IList<CM00089> L = null;
                try
                {
                    var tasks = _CM00089Repository.GetAll();
                    IList<CM00089> result = tasks;
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
        public KaizenResult AddCM00089(CM00089 newTask)
        {
            var result = _CM00089Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00089(IList<CM00089> newTask)
        {
            var result = _CM00089Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00089 newTask)
        {
            var result = _CM00089Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00089> newTask)
        {
            var result = _CM00089Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00089 newTask)
        {
            var result = _CM00089Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00089> newTask)
        {
            var result = _CM00089Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

       
        public IList<CM00088> GetPaymentTypes()
        {
            CM00088Repository rep = new CM00088Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var paymentTypes = rep.GetAll();
            IList<CM00088> result = paymentTypes;
            return result;
        }
        #region Account User
        public IList<CM00090> GetCM00090ByUser(string UserName)
        {
            CM00090Repository rep = new CM00090Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var users = rep.GetAll().Where(x=>x.UserName.Trim().Equals(UserName.Trim())).ToList();
            IList<CM00090> result = users;
            return result;
        }
        public KaizenResult AddCM00090(CM00090 newTask)
        {
            CM00090Repository rep = new CM00090Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult DeleteCM00090(CM00090 newTask)
        {
            CM00090Repository rep = new CM00090Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(newTask);
            return result;
        }
        #endregion

        #region Account Role
        public IList<CM00091> GetCM00091ByRole(int RoleID)
        {
            CM00091Repository rep = new CM00091Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var roles = rep.GetAll().Where(x => x.RoleID== RoleID).ToList();
            IList<CM00091> result = roles;
            return result;
        }
        public KaizenResult AddCM00091(CM00091 newTask)
        {
            CM00091Repository rep = new CM00091Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult DeleteCM00091(CM00091 newTask)
        {
            CM00091Repository rep = new CM00091Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(newTask);
            return result;
        }
        #endregion

        public CM00089 GetSingle(string CheckbookCode, string CurrencyCode, string CheckbookName)
        {
            CM00089Repository rep = new CM00089Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var result = rep.GetAll().Where(x=>x.CheckbookCode.Trim().Equals(CheckbookCode.Trim()) &&
                                                x.CurrencyCode.Trim().Equals(CurrencyCode.Trim()) && 
                                                x.CheckbookName.Trim().Equals(CheckbookName.Trim())).FirstOrDefault();
            return result;
        }
    }
}
