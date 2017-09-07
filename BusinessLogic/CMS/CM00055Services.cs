using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00055Services
    {
        #region Infrastructure

        private CM00055Repository _CM00055Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00055Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00055Repository = new CM00055Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00055> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00055> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00055Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00055Repository.GetWhereListWithPaging("CM00055", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00055> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<CM00055> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00055Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00055Repository.GetWhereListWithPaging("CM00055", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<CM00055> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00055> L = null;
            var tasks = _CM00055Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00055> result = tasks;
            return result;
        }
        public DataCollection<CM00055> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00055> result = null;
            var tasks = _CM00055Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ProductID });
            result = tasks;
            return result;
        }
        public IList<CM00055> GetAll(int tRXTypeID)
        {
            var tasks = _CM00055Repository.GetAll(ss=>ss.TRXTypeID== tRXTypeID);
            IList<CM00055> result = tasks;
            return result;
        }
        public IList<CM00055> GetAll()
        {
            var tasks = _CM00055Repository.GetAll();
            IList<CM00055> result = tasks;
            return result;
        }
        public CM00055 GetSingle(int ProductID)
        {
            var tasks = _CM00055Repository.GetSingle(x => x.ProductID == ProductID);
            return tasks;
        }

        public KaizenResult AddCM00055(CM00055 newTask)
        {
            KaizenResult result = _CM00055Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00055(IList<CM00055> newTask)
        {
            KaizenResult result = _CM00055Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00055 newTask)
        {
            KaizenResult result = _CM00055Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00055> newTask)
        {
            KaizenResult result = _CM00055Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00055 newTask)
        {
            KaizenResult result = _CM00055Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00055> newTask)
        {
            KaizenResult result = _CM00055Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
