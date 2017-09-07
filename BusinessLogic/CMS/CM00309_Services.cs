using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
namespace Kaizen.BusinessLogic.CMS
{
    public class CM00309Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00309Repository _CM00309Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00309Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00309Repository = new CM00309Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00309> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ReasonID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ReasonName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00309Repository rep = new CM00309Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00309> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00309", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00309> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00309> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00309Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00309Repository.GetWhereListWithPaging("CM00309", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00309> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00309> L = null;
            var tasks = _CM00309Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00309> result = tasks;
            return result;
        }
        public DataCollection<CM00309> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00309> result = null;
            var tasks = _CM00309Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ReasonID });
            result = tasks;
            return result;
        }
        public IList<CM00309> GetAll()
        {
            var tasks = _CM00309Repository.GetAll();
            IList<CM00309> result = tasks;
            return result;
        }
        public CM00309 GetSingle(int ReasonID)
        {
            var tasks = _CM00309Repository.GetSingle(x => x.ReasonID == ReasonID);
            return tasks;
        }

        public KaizenResult AddCM00309(CM00309 newTask)
        {
            var result = _CM00309Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00309(IList<CM00309> newTask)
        {
            var result = _CM00309Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00309 newTask)
        {
            var result = _CM00309Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00309> newTask)
        {
            var result = _CM00309Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00309 newTask)
        {
            var result = _CM00309Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00309> newTask)
        {
            var result = _CM00309Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}

