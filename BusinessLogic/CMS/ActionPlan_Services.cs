using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00015Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00015Repository _CM00015Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00015Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00015Repository = new CM00015Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00015> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            CM00081Repository swws = new CM00081Repository(this.UserContext.CompanyID ,this.UserContext.UserName,this.UserContext.Password);
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00015> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00015Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00015Repository.GetWhereListWithPaging("CM00015", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00015> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<CM00015> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00015Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00015Repository.GetWhereListWithPaging("CM00015", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }


        public DataCollection<CM00015> GetAllCM00015(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<CM00015> L = null;
                var tasks = _CM00015Repository.GetListWithPaging(x => (x.BucketName.Contains(SearchTerm)
                    ), PageSize, Page, ss => ss.BucketID, null);

                DataCollection<CM00015> result = tasks;
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
        public IList<CM00015> GetAll()
        {
            var tasks = _CM00015Repository.GetAll();
            IList<CM00015> result = tasks;
            return result;
        }

        public CM00015 GetSingle(int BucketID)
        {
            var tasks = _CM00015Repository.GetSingle(x => x.BucketID == BucketID);
            return tasks;
        }

        public KaizenResult AddCM00015(CM00015 newTask)
        {
            var result = _CM00015Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00015(IList<CM00015> newTask)
        {
            var result = _CM00015Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00015 newTask)
        {
            var result = _CM00015Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00015> newTask)
        {
            var result = _CM00015Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00015 newTask)
        {
            var result = _CM00015Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00015> newTask)
        {
            var result = _CM00015Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
