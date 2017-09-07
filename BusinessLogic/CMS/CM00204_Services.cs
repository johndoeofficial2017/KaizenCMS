using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00204Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00204Repository _CM00204Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00204Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00204Repository = new CM00204Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00204> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("StatusHistoryID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TrxTypeName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00204Repository rep = new CM00204Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00204> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00204", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00204> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00204> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00204Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00204Repository.GetWhereListWithPaging("CM00204", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00204> GetAllViewBYSQLQuery(string Filters,
            string Searchcritaria, string CaseRef,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            DataCollection<CM00204> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00204Repository.GetListWithPaging(ss => ss.CaseRef.Trim() == CaseRef.Trim(),
                    PageSize, Page, ss => Member);
            }
            else
            {
                SeachStr += " and CaseRef='" + CaseRef.Trim() + "'";
                result = _CM00204Repository.GetWhereListWithPaging("CM00204", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }


        public DataCollection<CM00204> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM00204Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00204> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM00204Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00204> result = tasks;
                return result;
            }

        }
        public DataCollection<CM00204> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00204> result = null;
            var tasks = _CM00204Repository.GetListWithPaging(PageSize, Page, ss => new { ss.PaymentID });
            result = tasks;
            return result;
        }
        public IList<CM00204> GetAll()
        {
            var tasks = _CM00204Repository.GetAll();
            IList<CM00204> result = tasks;
            return result;
        }
        public IList<CM00204> GetAllByCase(string CaseRef)
        {
            var tasks = _CM00204Repository.GetAll(xx => xx.CaseRef == CaseRef);
            IList<CM00204> result = tasks;
            return result;
        }
        public IList<CM00204> GetPaidCases(string PaymentID)
        {
            var tasks = _CM00204Repository.GetAll(xx => xx.PaymentID == PaymentID);
            IList<CM00204> result = tasks;
            return result;
        }

        public CM00204 GetSingle(string PaymentID, string CaseRef)
        {
            var tasks = _CM00204Repository.GetSingle(x => x.PaymentID == PaymentID && x.CaseRef == CaseRef);
            return tasks;
        }

        public KaizenResult AddCM00204(CM00204 newTask)
        {
            KaizenResult result = _CM00204Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00204(IList<CM00204> newTask)
        {
            KaizenResult result = _CM00204Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00204 newTask)
        {
            KaizenResult result = _CM00204Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00204> newTask)
        {
            KaizenResult result = _CM00204Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00204 newTask)
        {
            KaizenResult result = _CM00204Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00204> newTask)
        {
            KaizenResult result = _CM00204Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
