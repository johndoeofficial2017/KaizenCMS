using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00033Services
    {     
        #region Infrastructure

        private CM00033Repository _CM00033Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00033Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00033Repository = new CM00033Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00033> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00033> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00033Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00033Repository.GetWhereListWithPaging("CM00033", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<CM00033> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("AddressCodeType", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AddressTypeName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00033> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00033Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00033Repository.GetWhereListWithPaging("CM00033", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00033> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00033> L = null;
            var tasks = _CM00033Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00033> result = tasks;
            return result;
        }
        public DataCollection<CM00033> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00033> result = null;
            var tasks = _CM00033Repository.GetListWithPaging(PageSize, Page, ss => new { ss.AddressCodeType });
            result = tasks;
            return result;
        }

        public IList<CM00033> GetAll()
        {
            try
            {
                IList<CM00033> L = null;
                try
                {
                    var tasks = _CM00033Repository.GetAll();
                    IList<CM00033> result = tasks;
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

        public CM00033 GetSingle(int AddressCodeType)
        {
            var tasks = _CM00033Repository.GetSingle(x => x.AddressCodeType == AddressCodeType);
            return tasks;
        }

        public KaizenResult AddCM00033(CM00033 newTask)
        {
            var result = _CM00033Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00033(IList<CM00033> newTask)
        {
            var result = _CM00033Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00033 newTask)
        {
            var result = _CM00033Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00033> newTask)
        {
            var result = _CM00033Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00033 newTask)
        {
            var result = _CM00033Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00033> newTask)
        {
            var result = _CM00033Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
