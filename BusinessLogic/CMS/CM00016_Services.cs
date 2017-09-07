using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00016Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00016Repository _CM00016Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00016Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00016Repository = new CM00016Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00016> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00016> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00016Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00016Repository.GetWhereListWithPaging("CM00016", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00016> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<CM00016> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00016Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00016Repository.GetWhereListWithPaging("CM00016", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }


        public IList<CM00016> GetAll()
        {
            var tasks = _CM00016Repository.GetAll();
            IList<CM00016> result = tasks;
            return result;
        }

        public CM00016 GetSingle(int Lookup01)
        {
            var tasks = _CM00016Repository.GetSingle(x => x.Lookup01 == Lookup01);
            return tasks;
        }

        public KaizenResult AddCM00016(CM00016 newTask)
        {
            var result = _CM00016Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00016(IList<CM00016> newTask)
        {
            var result = _CM00016Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00016 newTask)
        {
            var result = _CM00016Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00016> newTask)
        {
            var result = _CM00016Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00016 newTask)
        {
            var result = _CM00016Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00016> newTask)
        {
            var result = _CM00016Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
