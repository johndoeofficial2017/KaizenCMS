using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00035Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00035Repository _CM00035Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00035Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00035Repository = new CM00035Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00035> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00035> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00035Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00035Repository.GetWhereListWithPaging("CM00035", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00035> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<CM00035> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00035Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00035Repository.GetWhereListWithPaging("CM00035", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<CM00035> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00035> L = null;
            var tasks = _CM00035Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00035> result = tasks;
            return result;
        }
        public DataCollection<CM00035> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00035> result = null;
            var tasks = _CM00035Repository.GetListWithPaging(PageSize, Page, ss => new { ss.TemplateID });
            result = tasks;
            return result;
        }
        public IList<CM00035> GetAll()
        {
            var tasks = _CM00035Repository.GetAll();
            IList<CM00035> result = tasks;
            return result;
        }
        public CM00035 GetSingle(int TemplateID)
        {
            var tasks = _CM00035Repository.GetSingle(x => x.TemplateID == TemplateID);
            return tasks;
        }

        public KaizenResult AddCM00035(CM00035 newTask)
        {
            var result = _CM00035Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00035(IList<CM00035> newTask)
        {
            var result = _CM00035Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00035 newTask)
        {
            var result = _CM00035Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00035> newTask)
        {
            var result = _CM00035Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00035 newTask)
        {
            var result = _CM00035Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00035> newTask)
        {
            var result = _CM00035Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
