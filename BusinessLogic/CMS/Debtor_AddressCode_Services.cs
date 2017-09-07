using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;


namespace Kaizen.BusinessLogic.CMS
{
    public class CM00009Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00009Repository _CM00009Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00009Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00009Repository = new CM00009Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00009> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00009> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00009Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00009Repository.GetWhereListWithPaging("CM00009", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<CM00009> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("AddressCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AddressName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00009> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00009Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00009Repository.GetWhereListWithPaging("CM00009", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00009> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00009> L = null;
            var tasks = _CM00009Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<CM00009> result = tasks;
            return result;
        }
        public DataCollection<CM00009> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00009> result = null;
            var tasks = _CM00009Repository.GetListWithPaging(PageSize, Page, ss => new { ss.AddressCode });
            result = tasks;
            return result;
        }
        public IList<CM00009> GetAll()
        {
            try
            {
                IList<CM00009> L = null;
                try
                {
                    var tasks = _CM00009Repository.GetAll();
                    IList<CM00009> result = tasks;
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
        public CM00009 GetSingle(string AddressCode)
        {
            var tasks = _CM00009Repository.GetSingle(x => x.AddressCode == AddressCode);
            return tasks;
        }

        public KaizenResult AddCM00009(CM00009 newTask)
        {
            var result = _CM00009Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00009(IList<CM00009> newTask)
        {
            var result = _CM00009Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00009 newTask)
        {
            var result = _CM00009Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00009> newTask)
        {
            var result = _CM00009Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00009 newTask)
        {
            var result = _CM00009Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00009> newTask)
        {
            var result = _CM00009Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        #region Debtor Address Roles
        public IList<CM00032> GetRolesByAddressCode(string AddressCode)
        {
            CM00032Repository rep = new CM00032Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var rolesByAddressCode = rep.GetAll(xx => xx.AddressCode == AddressCode);
            IList<CM00032> result = rolesByAddressCode;
            return result;
        }
        public KaizenResult AddCM00032(CM00032 addressRole)
        {
            CM00032Repository rep = new CM00032Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(addressRole);
            return result;
        }
        public KaizenResult DeleteCM00032(CM00032 addressRole)
        {
            CM00032Repository rep = new CM00032Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(addressRole);
            return result;
        }
        #endregion

        #region Debtor Address User
        public IList<CM00031> GetDebtorAddressByUser(string userName)
        {
            CM00031Repository rep = new CM00031Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(xx => xx.UserName == userName);
            IList<CM00031> result = tasks;
            return result;
        }
        public KaizenResult AddCM00031(CM00031 addressUser)
        {
            CM00031Repository rep = new CM00031Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(addressUser);
            return result;
        }
        public KaizenResult DeleteCM00031(CM00031 addressUser)
        {
            CM00031Repository rep = new CM00031Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(addressUser);
            return result;
        }
        #endregion
    }
}
