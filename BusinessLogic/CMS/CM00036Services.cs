using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00036Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00036Repository _CM00036Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00036Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00036Repository = new CM00036Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00036> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00036> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00036Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00036Repository.GetWhereListWithPaging("CM00036", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00036> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<CM00036> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00036Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00036Repository.GetWhereListWithPaging("CM00036", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<CM00036> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00036> L = null;
            var tasks = _CM00036Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00036> result = tasks;
            return result;
        }
        public DataCollection<CM00036> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00036> result = null;
            var tasks = _CM00036Repository.GetListWithPaging(PageSize, Page, ss => new { ss.TemplateID });
            result = tasks;
            return result;
        }
        public IList<CM00036> GetAll()
        {
            var tasks = _CM00036Repository.GetAll();
            IList<CM00036> result = tasks;
            return result;
        }
        public CM00036 GetSingle(int TemplateID)
        {
            var tasks = _CM00036Repository.GetSingle(x => x.TemplateID == TemplateID);
            return tasks;
        }

        public KaizenResult AddCM00036(CM00036 newTask)
        {
            var result = _CM00036Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00036(IList<CM00036> newTask)
        {
            var result = _CM00036Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00036 newTask)
        {
            var result = _CM00036Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00036> newTask)
        {
            var result = _CM00036Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00036 newTask)
        {
            var result = _CM00036Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00036> newTask)
        {
            var result = _CM00036Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
