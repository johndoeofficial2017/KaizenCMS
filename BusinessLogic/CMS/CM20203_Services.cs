using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Transactions;
using System.Data.SqlClient;
using Kaizen.CMS.DAL;
using System.Data;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM20203Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM20203Repository _CM20203Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM20203Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM20203Repository = new CM20203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM20203> GetAllSQLQueryByView(string Filters, string Searchcritaria,
 int ViewID, string YearCode, string PERIODID, int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            if (!string.IsNullOrEmpty(SeachStr))
                SeachStr += "and YearCode=" + YearCode;
            else
            {
                SeachStr += "YearCode='" + YearCode.Trim() + "'";
            }
           
            if ( !string.IsNullOrEmpty(PERIODID) && !string.IsNullOrEmpty(SeachStr))
                SeachStr += " and PERIODID=" + PERIODID;
            else
            {
                if (!string.IsNullOrEmpty(PERIODID))
                    SeachStr += " PERIODID=" + PERIODID;
            }
            Configuration.Kaizen00011Services service = new Configuration.Kaizen00011Services(UserContext);
            string ViewWhereCondition = service.GetViewWhereCondition(ViewID);
            if (!string.IsNullOrEmpty(ViewWhereCondition))
            {
                if (!string.IsNullOrEmpty(SeachStr))
                    SeachStr += " and ";
                SeachStr += ViewWhereCondition;
            }
            DataCollection<CM20203> result = null;
            result = _CM20203Repository.GetWhereListWithPaging("CM20203", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<CM20203> GetDataReminder(string Filters, string Searchcritaria, bool IsOverDue
            , DateTime FromReminder, DateTime ToReminder, string AgentID, int PageSize, int Page,
            string Member, bool IsAscending)
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
                    SeachStr = Filters + " and " + Searchcritaria;
            }
            CM20203Repository req = new CM20203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM20203> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                AgentID = AgentID.Trim();
                //if (IsOverDue)
                //    SeachStr += " DATEADD(dd, 0, DATEDIFF(dd, 0, ReminderDateTime)) <='" + ToReminder.ToString("yyyy-MM-dd") + "'";
                //else
                //{
                //    SeachStr += " DATEADD(dd, 0, DATEDIFF(dd, 0, ReminderDateTime)) >='" + FromReminder.ToString("yyyy-MM-dd") + "'";
                //    SeachStr += " and DATEADD(dd, 0, DATEDIFF(dd, 0, ReminderDateTime)) <='" + ToReminder.ToString("yyyy-MM-dd") + "'";
                //}
                //SeachStr += " and AgentID ='" + AgentID + "'";
                result = req.GetWhereListWithPaging("CM20203", PageSize, Page, SeachStr, Member, IsAscending);
            }
            else
            {
                if (IsOverDue)
                    SeachStr += "and DATEADD(dd, 0, DATEDIFF(dd, 0, ReminderDateTime)) <='" + ToReminder.ToString("yyyy-MM-dd") + "'";
                else
                {
                    SeachStr += " and DATEADD(dd, 0, DATEDIFF(dd, 0, ReminderDateTime)) >='" + FromReminder.ToString("yyyy-MM-dd") + "'";
                    SeachStr += " and DATEADD(dd, 0, DATEDIFF(dd, 0, ReminderDateTime)) <='" + ToReminder.ToString("yyyy-MM-dd") + "'";
                }
                SeachStr += " and AgentID ='" + AgentID + "'";
                result = req.GetWhereListWithPaging("CM20203", PageSize, Page, SeachStr, Member, IsAscending);
            }
            return result;
        }


        public DataCollection<CM20203> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CaseRef", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TRXTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContractID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClientID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DecimalDigit", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("JecketsID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("JecketsComment", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DebtorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ActionPlanID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseStatusID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseStatusComment", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ReminderDateTime", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AddressCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseAddess", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContactTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseAccountNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InvoiceNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClosingDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TransactionDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BookingDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Remarks", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OSAmount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClaimAmount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PrincipleAmount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AgentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AssignDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CreatedDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM20203Repository rep = new CM20203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM20203> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM20203", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<CM20203> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string CaseAccountNo,
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
                    SeachStr += Help.GetFilter("CaseRef", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TRXTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContractID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClientID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DecimalDigit", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("JecketsID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("JecketsComment", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DebtorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ActionPlanID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseStatusID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseStatusComment", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ReminderDateTime", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AddressCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseAddess", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContactTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseAccountNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InvoiceNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClosingDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TransactionDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BookingDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Remarks", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OSAmount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClaimAmount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PrincipleAmount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AgentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AssignDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CreatedDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM20203Repository rep = new CM20203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM20203> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetListWithPaging(ss => ss.CaseAccountNo.Trim() == CaseAccountNo.Trim(), PageSize, Page, ss => Member);
            else {
                SeachStr += " and CaseAccountNo='" + CaseAccountNo.Trim() + "'";
                result = rep.GetWhereListWithPaging("CM20203", PageSize, Page, SeachStr, Member, IsAscending);
            }
            return result;
        }

        public DataCollection<CM20203> GetAllViewBYSQLQuery(string WhereClause, int PageSize, int Page, string Member, ListSortDirection SortDirection)
        {
            CM20203Repository rep = new CM20203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM20203> result = null;
            if (SortDirection == ListSortDirection.Ascending)
                result = rep.GetWhereListWithPaging(WhereClause, Page, PageSize, s => Member, true);
            else
                result = rep.GetWhereListWithPaging(WhereClause, Page, PageSize, s => Member, false);
            return result;
        }

        public DataCollection<CM20203> GetAllByAgentID(string Filters, string Searchcritaria, string AgentID, int PageSize, int Page, string Member, string SortDirection)
        {
            Kaizen.CMS.Repository.CM20203Repository _CM20203Repository = new CM20203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM20203> result = null;
            if (!string.IsNullOrEmpty(Filters))
            {
                Page = Page - 1;
                if (!string.IsNullOrEmpty(Searchcritaria))
                {
                    Filters += " and CaseRef like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and CaseContractDetail like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and CaseAddess like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and CaseAccountNo like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and AgentID ='" + AgentID + "'";
                }
                string sql = "select  * from CM20203";
                sql += " where " + Filters + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
                var tasks = _CM20203Repository.GetSQLData(sql);
                if (tasks != null)
                {
                    tasks.TotalItemCount = tasks.Items.Count;
                    tasks.TotalPageCount = (int)Math.Ceiling((double)tasks.TotalItemCount / (double)PageSize);
                }
                result = tasks;
                return result;
            }
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                var tasks = _CM20203Repository.GetListWithPaging(ss => ss.AgentID == AgentID.Trim(), PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            else
            {
                var tasks = _CM20203Repository.GetListWithPaging(xx => xx.AgentID == AgentID && (
                     xx.CaseRef.Contains(Searchcritaria) || xx.CaseAddess.Contains(Searchcritaria) || xx.CaseAccountNo.Contains(Searchcritaria))
                    , PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            return result;
        }

        public DataCollection<CM20203> GetAllBYSQLQuery(string Filters, string Searchcritaria,
            int PageSize, int Page, string Member, bool IsAscending)
        {
            CM20203Repository _CM20203Repository = new CM20203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM20203> result = null;
            if (!string.IsNullOrEmpty(Filters))
            {
                if (!string.IsNullOrEmpty(Searchcritaria))
                {
                    Filters += " and CaseRef like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and CaseContractDetail like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and CaseAddess like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and CaseAccountNo like '%" + Searchcritaria.Trim() + "%'";
                }
                result = _CM20203Repository.GetWhereListWithPaging("CM20203", PageSize, Page, Filters,
                    Member, IsAscending);

                return result;
            }
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                var tasks = _CM20203Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => "asc");
                result = tasks;
            }
            else
            {
                var tasks = _CM20203Repository.GetListWithPaging(xx => xx.CaseRef.Contains(Searchcritaria) || xx.CaseAddess.Contains(Searchcritaria) || xx.CaseAccountNo.Contains(Searchcritaria)
                    , PageSize, Page, ss => Member, ss => "asc");
                result = tasks;
            }
            return result;
        }
        public DataCollection<CM20203> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<CM20203> L = null;
            var tasks = _CM20203Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<CM20203> result = tasks;
            return result;
        }
        public DataCollection<CM20203> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<CM20203> result = null;
            var tasks = _CM20203Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }


        public DataCollection<CM20203> GetSQLData(string Searchcritaria, int ViewID, int PageSize, int Page, string Member, string SortDirection)
        {
            Page = Page - 1;
            DataCollection<CM20203> result = null;

            CM20203Repository _CM00100ViewRepository = new CM20203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            string sql = "select * from CM20203 " + Searchcritaria + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
            var tasks = _CM00100ViewRepository.GetSQLData(sql);
            if (tasks != null)
            {
                if (string.IsNullOrEmpty(Searchcritaria))
                    tasks.TotalItemCount = (int)_CM00100ViewRepository.Count();
                else
                    tasks.TotalItemCount = _CM00100ViewRepository.GetSQLINT("select count(ClientID) from CM20203 " + Searchcritaria);
                tasks.TotalPageCount = (int)Math.Ceiling((double)tasks.TotalItemCount / (double)PageSize);
            }
            result = tasks;
            return result;
        }
        public IList<CM20203> GetAll()
        {
            List<CM20203> result = new List<CM20203>();
            var tasks = _CM20203Repository.GetAllWithPaging(2000, 1, ss => "ClientID", null);
            for (int i = 0; i <= 1000; i++)
            {
                CM20203 temp = ((DataCollection<CM20203>)tasks).Items[i];
                result.Add(temp);
            }
            //result = _CM20203Repository.GetAll().ToList();
            return result;
        }
        public IList<CM20203> GetAllByAgent(string AgentID)
        {
            var tasks = _CM20203Repository.GetAll().Take(10).ToArray();
            IList<CM20203> result = tasks;
            return result;
        }

        //public CM20203 GetSingle(int TrxID)
        //{
        //    var tasks = _CM20203Repository.GetSingle(x => x.TrxID == TrxID);
        //    return tasks;
        //}
        public CM20203 GetSingleByDebtorID(string CIFNumber)
        {
            var tasks = _CM20203Repository.GetSingle(x => x.CIFNumber.Trim() == CIFNumber.Trim());
            return tasks;
        }
        public IList<CM20203> GetAllByDebtorID(string CIFNumber)
        {
            var tasks = _CM20203Repository.GetAll(x => x.CIFNumber.Trim() == CIFNumber.Trim());
            return tasks;
        }
        public DataCollection<CM20203> GetTop10BYCaseRef(string CaseRef)
        {
            string SeachStr = string.Empty;
            SeachStr = "CaseRef Like '" + CaseRef.Trim() + "%'";
            var result = _CM20203Repository.GetWhereListWithPaging("CM20203", 10, 1, SeachStr, "CaseRef", true);
            return result;
        }

        public KaizenResult AddCM20203(CM20203 newTask)
        {
            //newTask.CreatedDate = DateTime.Now;
            var result = _CM20203Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM20203(IList<CM20203> newTask)
        {
            var result = _CM20203Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM20203 newTask)
        {
            var result = _CM20203Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM20203> newTask)
        {
            var result = _CM20203Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM20203 newTask)
        {
            var result = _CM20203Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM20203> newTask)
        {
            var result = _CM20203Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
