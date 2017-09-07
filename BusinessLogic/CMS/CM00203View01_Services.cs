using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
namespace Kaizen.BusinessLogic.CMS
{
    public class CM00203View01Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00203View01Repository _CM00203View01Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00203View01Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00203View01Repository = new CM00203View01Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00203View01> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ClientID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClientName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseStatusID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseStatusname", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseStatusChild", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseStatusChildName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AgentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PTPAMT", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OSAmount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FinanceCharge", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PrincipleAmount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClaimAmount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseCount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00203View01Repository rep = new CM00203View01Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00203View01> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00203View01", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00203View01> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00203View01> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00203View01Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00203View01Repository.GetWhereListWithPaging("CM00203View01", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00203View01> GetAllViewBYSQLQuery(string Filters,
 string Searchcritaria, string AgentID,
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
            }
            DataCollection<CM00203View01> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00203View01Repository.GetListWithPaging(ss => ss.AgentID.Trim() == AgentID.Trim(), PageSize, Page, ss => Member);
            else
                result = _CM00203View01Repository.GetWhereListWithPaging("CM00203View01", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00203View01> GetAllViewBYSQLQuery(string Filters,
 string Searchcritaria, string ClientID, string CUSTCLAS,
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
            }
            DataCollection<CM00203View01> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                if (string.IsNullOrEmpty(CUSTCLAS))
                {
                    result = _CM00203View01Repository.GetListWithPaging(ss => ss.ClientID.Trim() == ClientID.Trim(), PageSize, Page, ss => Member);
                }
                else
                {
                    CM00110Repository _CM00110Repository = new CM00110Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    var list = _CM00110Repository.GetAll(ss => ss.CUSTCLAS == CUSTCLAS);
                    List<string> ClientIDs = new List<string>();
                    foreach (var item in list)
                    {
                        ClientIDs.Add(item.ClientID);
                    }
                    result = _CM00203View01Repository.GetListWithPaging(ss => ClientIDs.Contains(ss.ClientID), PageSize, Page, ss => Member);
                }
            }
            else
                result = _CM00203View01Repository.GetWhereListWithPaging("CM00203View01", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00203View01> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00203View01> L = null;
            var tasks = _CM00203View01Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00203View01> result = tasks;
            return result;
        }
        public DataCollection<CM00203View01> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00203View01> result = null;
            var tasks = _CM00203View01Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ClientID });
            result = tasks;
            return result;
        }
        public IList<CM00203View01> GetAll()
        {
            var tasks = _CM00203View01Repository.GetAll();
            IList<CM00203View01> result = tasks;
            return result;
        }
    }
}

