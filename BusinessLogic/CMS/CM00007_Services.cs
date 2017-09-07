using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00007Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00007Repository _CM00007Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00007Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00007Repository = new CM00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00007> GetAllViewBYSQLQuery(string Filters,
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

            CM00007Repository rep = new CM00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00007> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00007", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00007> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00007> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00007Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00007Repository.GetWhereListWithPaging("CM00007", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<CM00007> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM00007Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00007> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM00007Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00007> result = tasks;
                return result;
            }

        }
        public DataCollection<CM00007> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00007> result = null;
            var tasks = _CM00007Repository.GetListWithPaging(PageSize, Page, ss => new { ss.CalendarID });
            result = tasks;
            return result;
        }
        public IList<CM00007> GetAll()
        {
            var tasks = _CM00007Repository.GetAll();
            IList<CM00007> result = tasks;
            return result;
        }
        public IList<CM00007> GetByCalendar(int CalendarID)
        {
            var tasks = _CM00007Repository.GetAll(xx => xx.CalendarID == CalendarID);
            IList<CM00007> result = tasks;
            return result;
        }
        public IList<CM00007> GetCalendarPeriods(int CalendarID, string YearCode)
        {
            var tasks = _CM00007Repository.GetAll(xx => xx.CalendarID == CalendarID && xx.YearCode == YearCode);
            IList<CM00007> result = tasks;
            return result;
        }

        public CM00007 GetSingle(int CalendarID, int PERIODID)
        {
            var tasks = _CM00007Repository.GetSingle(x => x.CalendarID == CalendarID && x.PERIODID == PERIODID);
            return tasks;
        }
        public CM00007 GetCurrentPeriod()
        {
            string SeachStr = "CONVERT(date, getdate()) between StartDate and EndDate";
            var result = _CM00007Repository.GetALL("CM00007", SeachStr, "StartDate", true);
            if(result !=null)
            {
                if (result.Count > 0)
                    return result.FirstOrDefault();
            }
            return null;
        }
        public KaizenResult AddCM00007(CM00007 newTask)
        {
            var result = _CM00007Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00007(IList<CM00007> newTask)
        {
            var result = _CM00007Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00007 newTask)
        {
            var result = _CM00007Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00007> newTask)
        {
            var result = _CM00007Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00007 newTask)
        {
            var result = _CM00007Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00007> newTask)
        {
            var result = _CM00007Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
