using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00006Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00006Repository _CM00006Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00006Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00006Repository = new CM00006Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00006> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("PriorityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PriorityName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00006Repository rep = new CM00006Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00006> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00006", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00006> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00006> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00006Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00006Repository.GetWhereListWithPaging("CM00006", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<CM00006> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM00006Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00006> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM00006Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00006> result = tasks;
                return result;
            }

        }
        public DataCollection<CM00006> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00006> result = null;
            var tasks = _CM00006Repository.GetListWithPaging(PageSize, Page, ss => new { ss.PriorityID });
            result = tasks;
            return result;
        }
        public IList<CM00006> GetAll()
        {
            var tasks = _CM00006Repository.GetAll();
            IList<CM00006> result = tasks;
            return result;
        }

        public CM00006 GetSingle(string PriorityID)
        {
            var tasks = _CM00006Repository.GetSingle(x => x.PriorityID == PriorityID);
            return tasks;
        }

        public KaizenResult AddCM00006(CM00006 newTask)
        {
            var result = _CM00006Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00006(IList<CM00006> newTask)
        {
            var result = _CM00006Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00006 newTask)
        {
            var result = _CM00006Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00006> newTask)
        {
            var result = _CM00006Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00006 newTask)
        {
            var result = _CM00006Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00006> newTask)
        {
            var result = _CM00006Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
