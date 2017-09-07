using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
namespace Kaizen.BusinessLogic.CMS
{
    public class CM00018Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00018Repository _CM00018Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00018Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00018Repository = new CM00018Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00018> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("PaymentBaseID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PaymentBaseName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00018Repository rep = new CM00018Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00018> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00018", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00018> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00018> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00018Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00018Repository.GetWhereListWithPaging("CM00018", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00018> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00018> L = null;
            var tasks = _CM00018Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00018> result = tasks;
            return result;
        }
        public DataCollection<CM00018> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00018> result = null;
            var tasks = _CM00018Repository.GetListWithPaging(PageSize, Page, ss => new { ss.PaymentBaseID });
            result = tasks;
            return result;
        }
        public IList<CM00018> GetAll()
        {
            var tasks = _CM00018Repository.GetAll();
            IList<CM00018> result = tasks;
            return result;
        }
        public CM00018 GetSingle(int PaymentBaseID)
        {
            var tasks = _CM00018Repository.GetSingle(x => x.PaymentBaseID == PaymentBaseID);
            return tasks;
        }

        public KaizenResult AddCM00018(CM00018 newTask)
        {
            var result = _CM00018Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00018(IList<CM00018> newTask)
        {
            var result = _CM00018Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00018 newTask)
        {
            var result = _CM00018Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00018> newTask)
        {
            var result = _CM00018Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00018 newTask)
        {
            var result = _CM00018Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00018> newTask)
        {
            var result = _CM00018Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}

