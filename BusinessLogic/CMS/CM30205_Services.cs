using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM30205Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM30205Repository _CM30205Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM30205Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM30205Repository = new CM30205Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM30205> GetAllViewBYSQLQuery(string Filters,
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

            CM30205Repository rep = new CM30205Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM30205> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM30205", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM30205> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM30205> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM30205Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM30205Repository.GetWhereListWithPaging("CM30205", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM30205> GetAllViewBYSQLQuery(string Filters,
            string Searchcritaria, int TrxID,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            DataCollection<CM30205> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                //result = _CM30205Repository.GetListWithPaging(ss => ss.TrxID == TrxID,
                //    PageSize, Page, ss => Member);
            }
            else
            {
                SeachStr += " and TrxID='" + TrxID.ToString() + "'";
                result = _CM30205Repository.GetWhereListWithPaging("CM30205", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }


        public DataCollection<CM30205> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM30205Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM30205> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM30205Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM30205> result = tasks;
                return result;
            }

        }
        public DataCollection<CM30205> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM30205> result = null;
            var tasks = _CM30205Repository.GetListWithPaging(PageSize, Page, ss => new { ss.StatusHistoryID });
            result = tasks;
            return result;
        }
        public IList<CM30205> GetAll()
        {
            var tasks = _CM30205Repository.GetAll();
            IList<CM30205> result = tasks;
            return result;
        }
        //public IList<CM30205> GetAllByAgent(string AgentID)
        //{
        //    var tasks = _CM30205Repository.GetAll(xx=>xx.CaseRef== AgentID);
        //    IList<CM30205> result = tasks;
        //    return result;
        //}

        public CM30205 GetSingle(int StatusHistoryID)
        {
            var tasks = _CM30205Repository.GetSingle(x => x.StatusHistoryID == StatusHistoryID);
            return tasks;
        }

        public KaizenResult AddCM30205(CM30205 newTask)
        {
            KaizenResult result = _CM30205Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(CM30205 newTask)
        {
            KaizenResult result = _CM30205Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(int StatusHistoryID)
        {
            KaizenResult result = _CM30205Repository.RemoveKaizenObject(xx => xx.StatusHistoryID == StatusHistoryID);
            return result;
        }
    }
}
