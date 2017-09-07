using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM20205Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM20205Repository _CM20205Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM20205Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM20205Repository = new CM20205Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM20205> GetAllViewBYSQLQuery(string Filters,
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

            CM20205Repository rep = new CM20205Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM20205> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM20205", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM20205> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM20205> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM20205Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM20205Repository.GetWhereListWithPaging("CM20205", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM20205> GetAllViewBYSQLQuery(string Filters,
            string Searchcritaria, int TrxID,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            DataCollection<CM20205> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                //result = _CM20205Repository.GetListWithPaging(ss => ss.TrxID == TrxID,
                //    PageSize, Page, ss => Member);
            }
            else
            {
                SeachStr += " and TrxID='" + TrxID.ToString() + "'";
                result = _CM20205Repository.GetWhereListWithPaging("CM20205", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }


        public DataCollection<CM20205> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM20205Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM20205> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM20205Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM20205> result = tasks;
                return result;
            }

        }
        public DataCollection<CM20205> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM20205> result = null;
            var tasks = _CM20205Repository.GetListWithPaging(PageSize, Page, ss => new { ss.StatusHistoryID });
            result = tasks;
            return result;
        }
        public IList<CM20205> GetAll()
        {
            var tasks = _CM20205Repository.GetAll();
            IList<CM20205> result = tasks;
            return result;
        }
        public IList<CM20205> GetAllByAgent(string AgentID)
        {
            var tasks = _CM20205Repository.GetAll(xx=>xx.CaseRef== AgentID);
            IList<CM20205> result = tasks;
            return result;
        }

        public CM20205 GetSingle(int StatusHistoryID)
        {
            var tasks = _CM20205Repository.GetSingle(x => x.StatusHistoryID == StatusHistoryID);
            return tasks;
        }

        public KaizenResult AddCM20205(CM20205 newTask)
        {
            KaizenResult result = _CM20205Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(CM20205 newTask)
        {
            KaizenResult result = _CM20205Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(int StatusHistoryID)
        {
            KaizenResult result = _CM20205Repository.RemoveKaizenObject(xx => xx.StatusHistoryID == StatusHistoryID);
            return result;
        }
    }
}
