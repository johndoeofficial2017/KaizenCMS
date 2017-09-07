using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
namespace Kaizen.BusinessLogic.CMS
{
    public class CM00013Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00013Repository _CM00013Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00013Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00013Repository = new CM00013Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00013> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("BillingFrequencyID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BillingFrequencyName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00013Repository rep = new CM00013Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00013> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00013", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00013> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00013> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00013Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00013Repository.GetWhereListWithPaging("CM00013", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00013> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00013> L = null;
            var tasks = _CM00013Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00013> result = tasks;
            return result;
        }
        public DataCollection<CM00013> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00013> result = null;
            var tasks = _CM00013Repository.GetListWithPaging(PageSize, Page, ss => new { ss.BillingFrequencyID });
            result = tasks;
            return result;
        }
        public IList<CM00013> GetAll()
        {
            var tasks = _CM00013Repository.GetAll();
            IList<CM00013> result = tasks;
            return result;
        }
        public CM00013 GetSingle(int BillingFrequencyID)
        {
            var tasks = _CM00013Repository.GetSingle(x => x.BillingFrequencyID == BillingFrequencyID);
            return tasks;
        }

        public KaizenResult AddCM00013(CM00013 newTask)
        {
            var result = _CM00013Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00013(IList<CM00013> newTask)
        {
            var result = _CM00013Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00013 newTask)
        {
            var result = _CM00013Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00013> newTask)
        {
            var result = _CM00013Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00013 newTask)
        {
            var result = _CM00013Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00013> newTask)
        {
            var result = _CM00013Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}

