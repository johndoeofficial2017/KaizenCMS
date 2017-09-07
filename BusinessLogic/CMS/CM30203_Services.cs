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
    public class CM30203Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM30203Repository _CM30203Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM30203Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM30203Repository = new CM30203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM30203> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM30203> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM30203Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM30203Repository.GetWhereListWithPaging("CM30203", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM30203> GetDataReminder(string Filters, string Searchcritaria, bool IsOverDue
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
            CM30203Repository req = new CM30203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM30203> result = null;
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
                result = req.GetWhereListWithPaging("CM30203", PageSize, Page, SeachStr, Member, IsAscending);
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
                result = req.GetWhereListWithPaging("CM30203", PageSize, Page, SeachStr, Member, IsAscending);
            }
            return result;
        }


        public DataCollection<CM30203> GetAllViewBYSQLQuery(string Filters,
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

            CM30203Repository rep = new CM30203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM30203> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM30203", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<CM30203> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string CaseRef,
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

            CM30203Repository rep = new CM30203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM30203> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetListWithPaging(ss => ss.CaseRef.Trim() == CaseRef.Trim(), PageSize, Page, ss => Member);
            else
            {
                SeachStr += " and CaseRef='" + CaseRef.Trim() + "'";
                result = rep.GetWhereListWithPaging("CM30203", PageSize, Page, SeachStr, Member, IsAscending);
            }
            return result;
        }

        public DataCollection<CM30203> GetAllViewBYSQLQuery(string WhereClause, int PageSize, int Page, string Member, ListSortDirection SortDirection)
        {
            CM30203Repository rep = new CM30203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM30203> result = null;
            if (SortDirection == ListSortDirection.Ascending)
                result = rep.GetWhereListWithPaging(WhereClause, Page, PageSize, s => Member, true);
            else
                result = rep.GetWhereListWithPaging(WhereClause, Page, PageSize, s => Member, false);
            return result;
        }

        public DataCollection<CM30203> GetAllByAgentID(string Filters, string Searchcritaria, string AgentID, int PageSize, int Page, string Member, string SortDirection)
        {
            Kaizen.CMS.Repository.CM30203Repository _CM30203Repository = new CM30203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM30203> result = null;
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
                string sql = "select  * from CM30203";
                sql += " where " + Filters + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
                var tasks = _CM30203Repository.GetSQLData(sql);
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
                var tasks = _CM30203Repository.GetListWithPaging(ss => ss.AgentID == AgentID.Trim(), PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            else
            {
                var tasks = _CM30203Repository.GetListWithPaging(xx => xx.AgentID == AgentID && (
                     xx.CaseRef.Contains(Searchcritaria) || xx.CaseAddess.Contains(Searchcritaria) || xx.CaseAccountNo.Contains(Searchcritaria))
                    , PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            return result;
        }

        public DataCollection<CM30203> GetAllBYSQLQuery(string Filters, string Searchcritaria,
            int PageSize, int Page, string Member, bool IsAscending)
        {
            CM30203Repository _CM30203Repository = new CM30203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM30203> result = null;
            if (!string.IsNullOrEmpty(Filters))
            {
                if (!string.IsNullOrEmpty(Searchcritaria))
                {
                    Filters += " and CaseRef like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and CaseContractDetail like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and CaseAddess like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and CaseAccountNo like '%" + Searchcritaria.Trim() + "%'";
                }
                result = _CM30203Repository.GetWhereListWithPaging("CM30203", PageSize, Page, Filters,
                    Member, IsAscending);

                return result;
            }
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                var tasks = _CM30203Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => "asc");
                result = tasks;
            }
            else
            {
                var tasks = _CM30203Repository.GetListWithPaging(xx => xx.CaseRef.Contains(Searchcritaria) || xx.CaseAddess.Contains(Searchcritaria) || xx.CaseAccountNo.Contains(Searchcritaria)
                    , PageSize, Page, ss => Member, ss => "asc");
                result = tasks;
            }
            return result;
        }
        public DataCollection<CM30203> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<CM30203> L = null;
            var tasks = _CM30203Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<CM30203> result = tasks;
            return result;
        }
        public DataCollection<CM30203> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<CM30203> result = null;
            var tasks = _CM30203Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }


        public DataCollection<CM30203> GetSQLData(string Searchcritaria, int ViewID, int PageSize, int Page, string Member, string SortDirection)
        {
            Page = Page - 1;
            DataCollection<CM30203> result = null;

            CM30203Repository _CM00100ViewRepository = new CM30203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            string sql = "select * from CM30203 " + Searchcritaria + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
            var tasks = _CM00100ViewRepository.GetSQLData(sql);
            if (tasks != null)
            {
                if (string.IsNullOrEmpty(Searchcritaria))
                    tasks.TotalItemCount = (int)_CM00100ViewRepository.Count();
                else
                    tasks.TotalItemCount = _CM00100ViewRepository.GetSQLINT("select count(ClientID) from CM30203 " + Searchcritaria);
                tasks.TotalPageCount = (int)Math.Ceiling((double)tasks.TotalItemCount / (double)PageSize);
            }
            result = tasks;
            return result;
        }
        public IList<CM30203> GetAll()
        {
            List<CM30203> result = new List<CM30203>();
            var tasks = _CM30203Repository.GetAllWithPaging(2000, 1, ss => "ClientID", null);
            for (int i = 0; i <= 1000; i++)
            {
                CM30203 temp = ((DataCollection<CM30203>)tasks).Items[i];
                result.Add(temp);
            }
            //result = _CM30203Repository.GetAll().ToList();
            return result;
        }
        public IList<CM30203> GetAllByAgent(string AgentID)
        {
            var tasks = _CM30203Repository.GetAll().Take(10).ToArray();
            IList<CM30203> result = tasks;
            return result;
        }

        //public CM30203 GetSingle(int TrxID)
        //{
        //    var tasks = _CM30203Repository.GetSingle(x => x.TrxID == TrxID);
        //    return tasks;
        //}
        public CM30203 GetSingleByDebtorID(string CIFNumber)
        {
            var tasks = _CM30203Repository.GetSingle(x => x.CIFNumber.Trim() == CIFNumber.Trim());
            return tasks;
        }
        public IList<CM30203> GetAllByDebtorID(string CIFNumber)
        {
            var tasks = _CM30203Repository.GetAll(x => x.CIFNumber.Trim() == CIFNumber.Trim());
            return tasks;
        }
        public DataCollection<CM30203> GetTop10BYCaseRef(string CaseRef)
        {
            string SeachStr = string.Empty;
            SeachStr = "CaseRef Like '" + CaseRef.Trim() + "%'";
            var result = _CM30203Repository.GetWhereListWithPaging("CM30203", 10, 1, SeachStr, "CaseRef", true);
            return result;
        }

        public KaizenResult AddCM30203(CM30203 newTask)
        {
            //newTask.CreatedDate = DateTime.Now;
            var result = _CM30203Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM30203(IList<CM30203> newTask)
        {
            var result = _CM30203Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM30203 newTask)
        {
            var result = _CM30203Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM30203> newTask)
        {
            var result = _CM30203Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM30203 newTask)
        {
            var result = _CM30203Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM30203> newTask)
        {
            var result = _CM30203Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
