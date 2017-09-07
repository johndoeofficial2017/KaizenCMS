using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00017Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00017Repository _CM00017Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00017Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00017Repository = new CM00017Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00017> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00017> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00017Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00017Repository.GetWhereListWithPaging("CM00017", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00017> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("BucketID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00017> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00017Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00017Repository.GetWhereListWithPaging("CM00017", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }


        public DataCollection<CM00017> GetAllCM00017(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<CM00017> L = null;
                var tasks = _CM00017Repository.GetListWithPaging(x => (x.Lookup02Name.Contains(SearchTerm)
                    ), PageSize, Page, ss => ss.Lookup02, null);

                DataCollection<CM00017> result = tasks;
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
        public IList<CM00017> GetAll()
        {
            var tasks = _CM00017Repository.GetAll();
            IList<CM00017> result = tasks;
            return result;
        }

        public CM00017 GetSingle(int BucketID)
        {
            var tasks = _CM00017Repository.GetSingle(x => x.Lookup02 == BucketID);
            return tasks;
        }

        public KaizenResult AddCM00017(CM00017 newTask)
        {
            var result = _CM00017Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00017(IList<CM00017> newTask)
        {
            var result = _CM00017Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00017 newTask)
        {
            var result = _CM00017Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00017> newTask)
        {
            var result = _CM00017Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00017 newTask)
        {
            var result = _CM00017Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00017> newTask)
        {
            var result = _CM00017Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
