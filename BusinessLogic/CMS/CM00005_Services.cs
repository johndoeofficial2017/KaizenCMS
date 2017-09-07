using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00005Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00005Repository _CM00005Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00005Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00005Repository = new CM00005Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00005> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CalendarID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TaskTitle", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00005Repository rep = new CM00005Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00005> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00005", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00005> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00005> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00005Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00005Repository.GetWhereListWithPaging("CM00005", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<CM00005> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM00005Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00005> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM00005Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00005> result = tasks;
                return result;
            }

        }
        public DataCollection<CM00005> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00005> result = null;
            var tasks = _CM00005Repository.GetListWithPaging(PageSize, Page, ss => new { ss.CalendarID });
            result = tasks;
            return result;
        }
        public IList<CM00005> GetAll()
        {
            var tasks = _CM00005Repository.GetAll();
            IList<CM00005> result = tasks;
            return result;
        }

        public CM00005 GetSingle(int CalendarID)
        {
            var tasks = _CM00005Repository.GetSingle(x => x.CalendarID == CalendarID);
            return tasks;
        }

        public KaizenResult AddCM00005(CM00005 newTask)
        {
            var result = _CM00005Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00005(IList<CM00005> newTask)
        {
            var result = _CM00005Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00005 newTask)
        {
            var result = _CM00005Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00005> newTask)
        {
            var result = _CM00005Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00005 newTask)
        {
            var result = _CM00005Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00005> newTask)
        {
            var result = _CM00005Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
