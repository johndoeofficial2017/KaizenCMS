using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00004Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00004Repository _CM00004Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00004Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00004Repository = new CM00004Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00004> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TaskTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TaskTitle", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00004Repository rep = new CM00004Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00004> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00004", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00004> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00004> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00004Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00004Repository.GetWhereListWithPaging("CM00004", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<CM00004> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM00004Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00004> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM00004Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00004> result = tasks;
                return result;
            }

        }
        public DataCollection<CM00004> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00004> result = null;
            var tasks = _CM00004Repository.GetListWithPaging(PageSize, Page, ss => new { ss.TaskTypeID });
            result = tasks;
            return result;
        }
        public IList<CM00004> GetAll()
        {
            var tasks = _CM00004Repository.GetAll();
            IList<CM00004> result = tasks;
            return result;
        }

        public CM00004 GetSingle(string TaskTypeID)
        {
            var tasks = _CM00004Repository.GetSingle(x => x.TaskTypeID == TaskTypeID);
            return tasks;
        }

        public KaizenResult AddCM00004(CM00004 newTask)
        {
            var result = _CM00004Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00004(IList<CM00004> newTask)
        {
            var result = _CM00004Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00004 newTask)
        {
            var result = _CM00004Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00004> newTask)
        {
            var result = _CM00004Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00004 newTask)
        {
            var result = _CM00004Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00004> newTask)
        {
            var result = _CM00004Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
