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
    public class CM00203Services
    {
        #region Infrastructure

        private CM00203Repository _CM00203Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00203Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00203Repository = new CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00203> GetListGridWithCIFNumber(string Filters, string CIFNumber, int PageSize, int Page,
           string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (!string.IsNullOrEmpty(Filters))
                SeachStr = Filters;
            if (!string.IsNullOrEmpty(Filters))
                SeachStr += " and ";
            SeachStr += " CIFNumber='"+ CIFNumber.Trim() + "'";
            CM00203Repository req = new CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00203> result = null;

            result = req.GetWhereListWithPaging("CM00203", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<CM00203> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00203> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00203Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00203Repository.GetWhereListWithPaging("CM00203", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00203> GetDataReminder(string Filters
            , string Searchcritaria
            , DateTime FromReminder, DateTime ToReminder, string AgentID, bool IsOverDue
            , int CaseReminderType, int ViewID, int PageSize, int Page,
            string Member)
        {
            CM00203Repository _CM00203Repository = new CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00203> result = null;
            if (!string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    Filters += "and ";
                Filters += " (CaseRef like '%" + Searchcritaria.Trim() + "%'";
                Filters += " or CIFNumber like '%" + Searchcritaria.Trim() + "%'";
                Filters += " or CPRCRNo like '%" + Searchcritaria.Trim() + "%'";
                Filters += " or CIFName like '%" + Searchcritaria.Trim() + "%'";
                Filters += " or CaseAccountNo like '%" + Searchcritaria.Trim() + "%')";
            }
            if (ViewID != -1)
            {
                CM00071Repository rep = new CM00071Repository(this.UserContext.CompanyID, this.UserContext.UserName, UserContext.Password);
                CM00071 CM00071List = rep.GetSingle(xx => xx.ViewID == ViewID);
                if (!string.IsNullOrEmpty(Filters))
                    Filters += "and ";
                Filters += "TRXTypeID=" + CM00071List.TRXTypeID.ToString();
                if (!string.IsNullOrEmpty(CM00071List.WhereCondition))
                    Filters += " and " + CM00071List.WhereCondition;
                if (!string.IsNullOrEmpty(CM00071List.WhereCustomCondition))
                    Filters += " and " + CM00071List.WhereCustomCondition;
            }
            AgentID = AgentID.Trim();
            if (CaseReminderType == 2)
            {
                if (IsOverDue)
                {
                    if (!string.IsNullOrEmpty(Filters))
                        Filters += "and ";
                    Filters += " DATEADD(dd, 0, DATEDIFF(dd, 0, ReminderDateTime)) <='" + ToReminder.ToString("yyyy-MM-dd") + "'";
                }
                else
                {
                    if (!string.IsNullOrEmpty(Filters))
                        Filters += "and ";
                    Filters += " DATEADD(dd, 0, DATEDIFF(dd, 0, ReminderDateTime)) >='" + FromReminder.ToString("yyyy-MM-dd") + "'";
                    Filters += " and DATEADD(dd, 0, DATEDIFF(dd, 0, ReminderDateTime)) <='" + ToReminder.ToString("yyyy-MM-dd") + "'";
                }
            }
            if (CaseReminderType == 1)
            {
                Filters += " and ReminderDateTime is null";
            }
            Filters += " and AgentID ='" + AgentID + "'";
            result = _CM00203Repository.GetWhereListWithPaging("CM00203", PageSize, Page, Filters, Member);
            return result;
        }
        public DataCollection<CM00203> GetAllViewBYSQLQuery(string Filters,
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

            CM00203Repository rep = new CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00203> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00203", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<CM00203> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string CIFNumber,
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

            CM00203Repository rep = new CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00203> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetListWithPaging(ss => ss.CIFNumber.Trim() == CIFNumber.Trim(), PageSize, Page, ss => Member);
            else
                result = rep.GetWhereListWithPaging("CM00203", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00203> GetAllViewBYSQLQuery(string WhereClause, int PageSize, int Page, string Member, ListSortDirection SortDirection)
        {
            CM00203Repository rep = new CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00203> result = null;
            if (SortDirection == ListSortDirection.Ascending)
                result = rep.GetWhereListWithPaging(WhereClause, Page, PageSize, s => Member, true);
            else
                result = rep.GetWhereListWithPaging(WhereClause, Page, PageSize, s => Member, false);
            return result;
        }

        public DataCollection<CM00203> GetAllByAgentID(string Filters, string Searchcritaria, string AgentID, int PageSize, int Page, string Member, string SortDirection)
        {
            Kaizen.CMS.Repository.CM00203Repository _CM00203Repository = new CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00203> result = null;
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
                string sql = "select  * from CM00203";
                sql += " where " + Filters + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
                var tasks = _CM00203Repository.GetSQLData(sql);
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
                var tasks = _CM00203Repository.GetListWithPaging(ss => ss.AgentID == AgentID.Trim(), PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            else
            {
                var tasks = _CM00203Repository.GetListWithPaging(xx => xx.AgentID == AgentID && (
                     xx.CaseRef.Contains(Searchcritaria) || xx.CaseAddess.Contains(Searchcritaria) || xx.CaseAccountNo.Contains(Searchcritaria))
                    , PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            return result;
        }
        public KaizenDataTable GetAllDataTable(string Filters, string Searchcritaria,
         int PageSize, int Page, string Member, bool IsAscending, int ViewID)
        {
            CM00203Repository _CM00203Repository = new CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            KaizenDataTable result = null;
            if (!string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    Filters += "and ";
                Filters += " (CaseRef like '%" + Searchcritaria.Trim() + "%'";
                Filters += " or CIFNumber like '%" + Searchcritaria.Trim() + "%'";
                Filters += " or CPRCRNo like '%" + Searchcritaria.Trim() + "%'";
                Filters += " or CIFName like '%" + Searchcritaria.Trim() + "%'";
                Filters += " or CaseAccountNo like '%" + Searchcritaria.Trim() + "%')";
            }
            if (ViewID != -1)
            {
                CM00071Repository rep = new CM00071Repository(this.UserContext.CompanyID, this.UserContext.UserName, UserContext.Password);
                CM00071 CM00071List = rep.GetSingle(xx => xx.ViewID == ViewID);
                if (!string.IsNullOrEmpty(Filters))
                    Filters += "and ";
                Filters += "TRXTypeID=" + CM00071List.TRXTypeID.ToString();
                if (!string.IsNullOrEmpty(CM00071List.WhereCondition))
                    Filters += " and " + CM00071List.WhereCondition;
                if (!string.IsNullOrEmpty(CM00071List.WhereCustomCondition))
                    Filters += " and " + CM00071List.WhereCustomCondition;
                string col = string.Empty;
                string colsummery = string.Empty;
                if (CM00071List.IsSummery)
                {
                    CM00029Services srv = new CM00029Services(this.UserContext);
                    var CM00082List = srv.GetCM00082(ViewID);
                    var CM00083List = srv.GetCM00083(ViewID);
                    foreach (CM00082 o in CM00082List)
                    {
                        col += o.FieldName.Trim() + ",";
                    }
                    foreach (CM00083 o in CM00083List)
                    {
                        if (o.SummeryTypeID == 1)
                            colsummery += "SUM(" + o.FieldName.Trim() + "),";
                    }
                    colsummery = col + colsummery.Substring(0, col.Length - 1);
                    result = _CM00203Repository.GetDTAggregateWithPaging(CM00071List.TableSource.Trim(), colsummery, col, PageSize, Page, Filters, Member, IsAscending);
                }
                else
                    result = _CM00203Repository.GetDTWithPaging(CM00071List.TableSource.Trim(), PageSize, Page, Filters, Member, IsAscending);
            }
            return result;
        }
        public DataCollection<CM00203> GetAllBYSQLQuery(string Filters, string Searchcritaria,
            int PageSize, int Page, string Member, bool IsAscending, int ViewID)
        {
            CM00203Repository _CM00203Repository = new CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00203> result = null;
            if (!string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    Filters += "and ";
                Filters += " (CaseRef like '%" + Searchcritaria.Trim() + "%'";
                Filters += " or CIFNumber like '%" + Searchcritaria.Trim() + "%'";
                Filters += " or CPRCRNo like '%" + Searchcritaria.Trim() + "%'";
                Filters += " or CIFName like '%" + Searchcritaria.Trim() + "%'";
                Filters += " or CaseAccountNo like '%" + Searchcritaria.Trim() + "%')";
            }
            if (ViewID !=-1)
            {
                CM00071Repository rep = new CM00071Repository(this.UserContext.CompanyID,this.UserContext.UserName,UserContext.Password);
                CM00071 CM00071List = rep.GetSingle(xx => xx.ViewID == ViewID);
                if (!string.IsNullOrEmpty(Filters))
                    Filters += "and ";
                Filters += "TRXTypeID=" + CM00071List.TRXTypeID.ToString();
                if (!string.IsNullOrEmpty(CM00071List.WhereCondition))
                    Filters += " and " + CM00071List.WhereCondition;
                if (!string.IsNullOrEmpty(CM00071List.WhereCustomCondition))
                    Filters += " and " + CM00071List.WhereCustomCondition;
                //if(CM00071List.IsPivotTable)
                //{
                //    CM00029Services srv = new CM00029Services(this.UserContext);
                //    var graphCategories = srv.GetGraphCategories(GraphID);
                //    var graphFields = srv.GetGraphFields(GraphID);
                //}
            }
            result = _CM00203Repository.GetWhereListWithPaging("CM00203", PageSize, Page, Filters,Member, IsAscending);
            return result;
        }
        public DataCollection<CM00203> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<CM00203> L = null;
            var tasks = _CM00203Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<CM00203> result = tasks;
            return result;
        }
        public DataCollection<CM00203> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<CM00203> result = null;
            var tasks = _CM00203Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }
        public IList<CM00203View02> GetAllCM00203View02()
        {
            List<CM00203View02> result = new List<CM00203View02>();
            CM00203View02Repository rep = new CM00203View02Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            result = rep.GetAll().ToList();
            return result;
        }

        public DataCollection<CM00203> GetSQLData(string Searchcritaria, int ViewID, int PageSize, int Page, string Member, string SortDirection)
        {
            Page = Page - 1;
            DataCollection<CM00203> result = null;

            CM00203Repository _CM00100ViewRepository = new CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            string sql = "select * from CM00203 " + Searchcritaria + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
            var tasks = _CM00100ViewRepository.GetSQLData(sql);
            if (tasks != null)
            {
                if (string.IsNullOrEmpty(Searchcritaria))
                    tasks.TotalItemCount = (int)_CM00100ViewRepository.Count();
                else
                    tasks.TotalItemCount = _CM00100ViewRepository.GetSQLINT("select count(ClientID) from CM00203 " + Searchcritaria);
                tasks.TotalPageCount = (int)Math.Ceiling((double)tasks.TotalItemCount / (double)PageSize);
            }
            result = tasks;
            return result;
        }
        public IList<CM00203> GetAll()
        {
            IList<CM00203> result = new List<CM00203>();
            //var tasks = _CM00203Repository.GetAllWithPaging(2000, 1, ss => "ClientID", null);
            //for (int i = 0; i <= 1000; i++)
            //{
            //    CM00203 temp = ((DataCollection<CM00203>)tasks).Items[i];
            //    result.Add(temp);
            //}
            result = _CM00203Repository.GetAll();
            return result;
        }
        public IList<CM00203> GetAllByCaseType(int TRXTypeID)
        {
            IList<CM00203> result = new List<CM00203>();
            //var tasks = _CM00203Repository.GetAllWithPaging(2000, 1, ss => "ClientID", null);
            //for (int i = 0; i <= 1000; i++)
            //{
            //    CM00203 temp = ((DataCollection<CM00203>)tasks).Items[i];
            //    result.Add(temp);
            //}
            result = _CM00203Repository.GetAll().Where(x=>x.TRXTypeID== TRXTypeID).ToList();
            return result;
        }
        public IList<CM00203> SearchCaseRefCM00203(string SearchText)
        {
            IList<CM00203> result = new List<CM00203>();
            //var tasks = _CM00203Repository.GetAllWithPaging(2000, 1, ss => "ClientID", null);
            //for (int i = 0; i <= 1000; i++)
            //{
            //    CM00203 temp = ((DataCollection<CM00203>)tasks).Items[i];
            //    result.Add(temp);
            //}

            result = _CM00203Repository.GetAll().Where(x => x.CaseRef.Trim().Contains(SearchText.Trim())).ToList();
            return result;
        }
        public IList<CM00203> GetAllByField(string field)
        {
            var tasks = _CM00203Repository.GetAll().Distinct(new CM00203Comparer(field)).ToArray();
            IList<CM00203> result = tasks;
            return result;
        }

        public IList<CM00203> GetAllByAgent(string AgentID)
        {
            var tasks = _CM00203Repository.GetAll().Take(10).ToArray();
            IList<CM00203> result = tasks;
            return result;
        }
        public IList<CM00203> GetPivotTable(string ClientID, string ContractID)
        {
            IList<CM00203> tasks;
            if (string.IsNullOrEmpty(ContractID))
                tasks = _CM00203Repository.GetAll(ss => ss.ClientID == ClientID);
            else
                tasks = _CM00203Repository.GetAll(ss => ss.ClientID == ClientID && ss.ContractID == ContractID);
            return tasks;
        }
        public DataCollection<CM00203> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member
    , string SortDirection)
        {
            DataCollection<CM00203> result = null;

            var tasks = _CM00203Repository.GetListWithPaging(SearchTerm,
                PageSize, Page, ss => ss.CaseRef, true);
            result = tasks;
            return result;
        }

        public DataCollection<CM00203> GetListWithPaging(string SearchTerm, string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00203> result = null;

            if (string.IsNullOrEmpty(whereClause))
            {
                //var tasks = _CM00203Repository.GetListWithPaging(PageSize, Page, ss => ss.CaseRef, null, xx => xx.CM00100, cc => cc.CM00201, xx => xx.CM00700);
                result = null;
            }
            else
            {
                // var tt = _CM00203Repository.Testing(whereClause);
                //var tasks = _CM00203Repository.GetListWithPaging(whereClause, PageSize, Page, ss => ss.CaseRef, null, xx => xx.CM00100, xx => xx.CM00700);
                //result = tasks;
            }
            return result;
        }
        public DataCollection<CM00203> GetByDebtorID(string DebtorID, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<CM00203> L = null;
                //var tasks = _CM00203Repository.GetListWithPaging(x => (x.DebtorID.Contains(DebtorID)
                //    ), PageSize, Page, ss => ss.DebtorID, null, xx => xx.CM00100, cc => cc.CM00201, xx => xx.CM00700);

                DataCollection<CM00203> result = null;
                return result;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- GenderID: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return null;
        }
       
        

        public CM00203 GetSingle(string CaseRef)
        {
            var tasks = _CM00203Repository.GetSingle(x => x.CaseRef.Trim() == CaseRef.Trim());
            return tasks;
        }
        public CM00203 GetSingleByDebtorID(string CIFNumber)
        {
            var tasks = _CM00203Repository.GetSingle(x => x.CIFNumber.Trim() == CIFNumber.Trim());
            return tasks;
        }
        public IList<CM00203> GetAllByDebtorID(string CIFNumber)
        {
            var tasks = _CM00203Repository.GetAll(x => x.CIFNumber.Trim() == CIFNumber.Trim());
            return tasks;
        }
        public DataCollection<CM00203> GetTop10BYCaseRef(string CaseRef)
        {
            string SeachStr = string.Empty;
            SeachStr = "CaseRef Like '" + CaseRef.Trim() + "%'";
            var result = _CM00203Repository.GetWhereListWithPaging("CM00203", 10, 1, SeachStr, "CaseRef", true);
            return result;
        }
        public DataCollection<CM00203> GetTop10BYCaseRef(int TRXTypeID, string CaseRef)
        {
            string SeachStr = string.Empty;
            SeachStr = "TRXTypeID = " + TRXTypeID + " and CaseRef Like '" + CaseRef.Trim() + "%'";
            var result = _CM00203Repository.GetWhereListWithPaging("CM00203", 10, 1, SeachStr, "CaseRef", true);
            return result;
        }
        public KaizenResult AddCM00203(CM00203 newTask)
        {
            //newTask.CreatedDate = DateTime.Now;
            var result = _CM00203Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00203(IList<CM00203> newTask)
        {
            var result = _CM00203Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00203 newTask)
        {
            //CM00203  tt = this.GetSingle(newTask.CaseRef.Trim());
            var result = _CM00203Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00203> newTask)
        {
            var result = _CM00203Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00203 newTask)
        {
            var result = _CM00203Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00203> newTask)
        {
            var result = _CM00203Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(string CaseRef)
        {
            KaizenResult result = _CM00203Repository.RemoveKaizenObject(xx => xx.CaseRef.Trim() == CaseRef.Trim());
            return result;
        }
        public KaizenResult BulkDelete(IList<CM00203> CM00203)
        {
            KaizenResult result = _CM00203Repository.DeleteKaizenObject(CM00203.ToArray());
            return result;
        }
        public KaizenResult BulkDeleteWithCaseRef(List<string> str)
        {
            var result = _CM00203Repository.ExecuteSqlCommandKaizenObject("delete from CM00203 where CaseRef in ('" + string.Join("','", str.ToArray()) + "')");
            return result;
        }
        public KaizenResult BulkUpdateWithCaseRef(List<string> str, string Field, string Value, string Type)
        {
            var result = new KaizenResult() { Status = true, Massage = "Data has been Updated Successfully.." };
            if (Type == "number")
                result = _CM00203Repository.ExecuteSqlCommandKaizenObject("Update CM00203 Set " + Field + "=" + int.Parse(Value) + " where CaseRef in ('" + string.Join("','", str.ToArray()) + "')");
            else if (Type == "date")
                result = _CM00203Repository.ExecuteSqlCommandKaizenObject("Update CM00203 Set " + Field + "=" + DateTime.Parse(Value) + " where CaseRef in ('" + string.Join("','", str.ToArray()) + "')");
            else
                result = _CM00203Repository.ExecuteSqlCommandKaizenObject("Update CM00203 Set " + Field + "='" + Value + "' where CaseRef in ('" + string.Join("','", str.ToArray()) + "')");
            return result;
        }

        public KaizenResult AssignToAgent(IList<CM00203> CM00203, string AgentID, DateTime AssignDate)
        {
            foreach (var item in CM00203)
            {
                item.AgentID = AgentID;
                item.AssignDate = AssignDate;
            }
            KaizenResult result = _CM00203Repository.UpdateKaizenObject(CM00203.ToArray());
            return result;
        }

        public KaizenResult UploadBach(string FileName,
          string BatchID, string CurrencyCode, byte DecimalDigit, string ExchangeTableID
  , bool IsMultiply, double ExchangeRate, string ClientID, string ClientName, string ContractID, string ContractName
            , int CaseStatusID, string CaseStatusname
  , int CaseStatusChild,string CaseStatusChildName, string CaseStatusComment, DateTime? ReminderDateTime
            , DateTime BookingDate, DateTime? ClosingDate
            ,string AddressCode
            ,int TRXTypeID)
        {
            KaizenResult result = new KaizenResult();
            result.Status = true;
            result.Massage = "Successfully Uploaded Data ";
            //return result;
            this.ExecuteSqlCommand("DELETE FROM CM00150 WHERE UserName ='" + UserContext.UserName.Trim() + "'");
            this.ExecuteSqlCommand("DELETE FROM CM00151 WHERE UserName ='" + UserContext.UserName.Trim() + "'");
            this.ExecuteSqlCommand("DELETE FROM CM00152 WHERE UserName ='" + UserContext.UserName.Trim() + "'");
            this.ExecuteSqlCommand("DELETE FROM CM00153 WHERE UserName ='" + UserContext.UserName.Trim() + "'");
            this.ExecuteSqlCommand("DELETE FROM CM00155 WHERE UserName ='" + UserContext.UserName.Trim() + "'");
            using (var context = new CMSContext(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password))
            {
                try
                {
                    string sql = "INSERT INTO dbo.CM00152(UserName,BatchID,TRXTypeID,ClientID,ClientName,ContractID,ContractName,CurrencyCode,DecimalDigit,IsMultiply, ExchangeRate, CaseStatusID, CaseStatusChild, CaseStatusname, CaseStatusChildName,CaseStatusComment, ReminderDateTime, BookingDate,ClosingDate,FileName, AddressCode) VALUES";
                    sql += "('" + UserContext.UserName.Trim();
                    sql += "','" + BatchID.Trim();
                    sql += "'," + TRXTypeID.ToString();
                    //sql += "','" + TrxTypeName.Trim();
                    sql += ",'" + ClientID.Trim();
                    sql += "','" + ClientName.Trim();
                    sql += "','" + ContractID;
                    sql += "','" + ContractName;
                    sql += "','" + CurrencyCode.Trim();
                    sql += "'," + DecimalDigit.ToString();
                    sql += ",'" + (IsMultiply ? "1" : "0");
                    sql += "','" + ExchangeRate.ToString();
                    sql += "','" + CaseStatusID;
                    sql += "','" + CaseStatusChild;
                    sql += "','" + CaseStatusname;
                    sql += "','" + CaseStatusChildName;
                    sql += "','" + CaseStatusComment;
                    if (ReminderDateTime.HasValue)
                        sql += "','" + ReminderDateTime.ToString() + "'";
                    else
                        sql += "',null";
                    sql += ",'" + BookingDate.ToString();
                    if (ClosingDate.HasValue)
                        sql += "','" + ClosingDate.ToString() + "'";
                    else
                        sql += "',NULL";
                    sql += ",'" + FileName + "'";
                    sql += ",'" + AddressCode.Trim() + "')";
                    this.ExecuteSqlCommand(sql);
                }
                catch (Exception ex)
                {
                    result.Status = false;
                    result.Massage = ex.Message;
                    ExceptionUtility.LogException(ex, "UploadDebtorData");
                    return result;
                }
            }
            return result;
        }

        public KaizenResult UploadDebtorData(string destinationPath, IList<KaizenColumn> KaizenColumn)
        {
            DataTable dt = new DataTable();
            Configuration.CompanyacessServices srv = new Configuration.CompanyacessServices(this.UserContext);
            dt = srv.ReadExcelData(destinationPath);
            DataColumn newColumn = new DataColumn("UserName", typeof(string));
            newColumn.DefaultValue = UserContext.UserName.ToString();
            dt.Columns.Add(newColumn);
            KaizenResult result = new KaizenResult();
            result.Status = true;
            result.Massage = "Successfully Uploaded Data ";
            //return result;
            this.ExecuteSqlCommand("DELETE FROM CM00150 WHERE UserName ='" + UserContext.UserName.Trim() + "'");
            this.ExecuteSqlCommand("DELETE FROM CM00151 WHERE UserName ='" + UserContext.UserName.Trim() + "'");
            using (var context = new CMSContext(this.UserContext.CompanyID, UserContext.UserName, UserContext.Password))
            {
                try
                {
                    #region 150 
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(context.Database.Connection.ConnectionString, SqlBulkCopyOptions.KeepNulls & SqlBulkCopyOptions.Default))
                    {
                        foreach (KaizenColumn collumn in KaizenColumn)
                        {
                            DataColumn FieldColumn = new DataColumn(collumn.FieldName, typeof(string));
                            if (collumn.FieldValue == "Fixed")
                            {
                                FieldColumn.DefaultValue = collumn.FixedValue;
                                dt.Columns.Add(FieldColumn);
                                sqlBulkCopy.ColumnMappings.Add(collumn.FieldName, collumn.FieldName);
                            }
                            else if (collumn.FieldValue != "NULL")
                            {
                                sqlBulkCopy.ColumnMappings.Add(collumn.FieldValue, collumn.FieldName);
                            }
                        }
                        sqlBulkCopy.ColumnMappings.Add("UserName", "UserName");
                        sqlBulkCopy.BatchSize = 10000;
                        sqlBulkCopy.DestinationTableName = "CM00150";
                        sqlBulkCopy.WriteToServer(dt);
                    }
                    #endregion,
                   
                    List<KaizenColumn> L = KaizenColumn.ToList().FindAll(ss => ss.FieldValue == "NULL");
                    foreach (KaizenColumn collumn in L)
                    {
                        string sql = "";
                        switch (collumn.FieldName)
                        {
                            case "NationalityID":
                                sql = "update CM00150 set NationalityID='" + Master.GetCMSConfiguration(this.UserContext.CompanyID).AgentNationalityIdUpload + "'";
                                sql += " where NationalityID is null";
                                break;
                            case "GenderID":
                                sql = "update CM00150 set GenderID='" + 3 + "'";
                                sql += " where GenderID is null";
                                break;
                            case "CUSTCLAS":
                                sql = "update CM00150 set CUSTCLAS='" + "Default" + "'";
                                sql += " where GenderID is null";
                                break;
                            //case "GroupID":
                            //    sql = "update CM00150 set GroupID='" + 2 + "'";
                            //    sql += " where GenderID is null";
                            //    break;
                        }
                        if (!string.IsNullOrEmpty(sql))
                            this.ExecuteSqlCommand(sql);
                    }
                    this.ExecuteSqlCommand("exec [dbo].[CMS_UploadDebtorTemp01] '" + UserContext.UserName.Trim() + "'");
                    this.ExecuteSqlCommand("exec [dbo].[CMS_UploadDebtorTemp02] '" + UserContext.UserName.Trim() + "'");
                }
                catch (Exception ex)
                {
                    result.Status = false;
                    result.Massage = ex.Message + ex.InnerException.Message;
                    ExceptionUtility.LogException(ex, "UploadDebtorData");
                    return result;
                }
            }
            return result;
        }
        public KaizenResult CaseCheckedhistryAll(bool IsJoint)
        {
            CM00150Repository _CM00150Repository = new CM00150Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            KaizenResult result = new KaizenResult();
            result.Status = true;
            result.Massage = "Successfully Upload Data";
            string sql = "update CM00155 set IsJoint=";
            if (IsJoint)
                sql += "1";
            else
                sql += "0";
            string strcase = string.Empty;
            if (this.ExecuteSqlCommand(sql) < 0)
            {
                result.Status = false;
                result.Massage = "Fail Data";
            }
            //else
            //{
            //    CM00152Repository rep = new CM00152Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password.Trim());
            //    CM00152 oCM00152 = rep.GetSingle(ss => ss.UserName == this.UserContext.UserName.Trim());
            //    return this.CaseReconcile(oCM00152.ClientID.Trim());
            //}
            return result;
        }
        public KaizenResult CaseCheckedhistry(string CaseRef, bool IsJoint)
        {
            CM00150Repository _CM00150Repository = new CM00150Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            KaizenResult result = new KaizenResult();
            result.Status = true;
            result.Massage = "Successfully Upload Data";
            string sql = "update CM00155 set IsJoint=";
            if (IsJoint)
                sql += "1";
            else
                sql += "0";
            sql += " where CaseRef='" + CaseRef.Trim() + "'";
            string strcase = string.Empty;
            if (this.ExecuteSqlCommand(sql) < 0)
            {
                result.Status = false;
                result.Massage = "Fail Data";
            }
            //else
            //{
            //    CM00152Repository rep = new CM00152Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password.Trim());
            //    CM00152 oCM00152 = rep.GetSingle(ss => ss.UserName == this.UserContext.UserName.Trim());
            //    return this.CaseReconcile(oCM00152.ClientID.Trim());
            //}
            return result;
        }
        public KaizenResult CaseDelete()
        {
            CM00150Repository _CM00150Repository = new CM00150Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            KaizenResult result = new KaizenResult();
            result.Status = true;
            result.Massage = "Successfully Upload Data";
            string sql = string.Empty;
            if (this.ExecuteSqlCommand("exec [dbo].[CMS_UploadCaseReconcile02] '" + UserContext.UserName.Trim() + "'") < 0)
            {
                result.Status = false;
                result.Massage = "Fail Data";
            }
            return result;
        }
        public KaizenResult CaseArchive()
        {
            CM00150Repository _CM00150Repository = new CM00150Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            KaizenResult result = new KaizenResult();
            result.Status = true;
            result.Massage = "Successfully Upload Data";
            string sql = string.Empty;
            if (this.ExecuteSqlCommand("exec [dbo].[CMS_UploadCaseReconcile01] '" + UserContext.UserName.Trim() + "'") < 0)
            {
                result.Status = false;
                result.Massage = "Fail Data";
            }
            return result;
        }
        public KaizenResult UpdateCasesStatus(CM10701 cM10701)
        {
            KaizenResult result = new KaizenResult();
            CM00155Repository rep = new CM00155Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            List<CM00155> L = rep.GetAll(x => x.IsJoint == true).ToList();
            string sql = "";
            foreach (CM00155 o in L)
            {
                sql += ",";
            }
            foreach (CM00155 o in L)
            {
                cM10701.CaseRef = o.CaseRef;
                cM10701.ChangeStatusSourceID = 3;
                cM10701.ClaimAmount = o.ClaimAmount;
                cM10701.OutstandingAMT = o.OutstandingAMT;
                cM10701.TotalCallactedAMT = o.TotalCallactedAMT;
                cM10701.AgentID = o.AgentID;
                cM10701.CreatedDate = DateTime.Now;
                cM10701.CurrencyCode = o.CurrencyCode;
                cM10701.DecimalDigit = o.DecimalDigit;
                cM10701.IsMultiply = o.IsMultiply;
                cM10701.ExchangeRate = o.ExchangeRate;
                cM10701.LastCaseStatusID = o.CaseStatusID;
                CM10701Services serv = new CM10701Services(this.UserContext);
                result = serv.AddCM10701(null, cM10701);
            }
            result = this.CaseReconcile();
            ExecuteSqlCommand("delete CM00155 where CM00155.IsJoint = 1 and CaseRef not in (select CaseRef from CM00203)");
            return result;
            ////CM00150Repository _CM00150Repository = new CM00150Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            ////result.Status = true;
            ////result.Massage = "Successfully Upload Data";
            ////string sql = string.Empty;
            ////sql = " update CM00155  set CaseStatusIDNew = '" + cM10701.CaseStatusID + "'  , CaseStatusnameNew = '" + cM10701.CaseStatusname
            ////    + "' , CaseStatusChildNew = '" + cM10701.CaseStatusChild + "'";
            ////sql += ", CaseStatusChildNameNew = '" + cM10701.CaseStatusChildName + "', CaseStatusCommentNew = '" + cM10701.CaseStatusComment + "'";
            ////if (cM10701.ReminderDateTime.HasValue)
            ////    sql += ",ReminderDateTimeNew = '" + cM10701.ReminderDateTime.ToString() + "'";
            ////else
            ////    sql += ",ReminderDateTimeNew =null";
            ////sql += ",PTPAMT=" + (cM10701.PTPAMT.HasValue ? cM10701.PTPAMT.ToString() : "0");
            ////sql += " where IsJoint=1";
            ////if (this.ExecuteSqlCommand(sql) < 0)
            ////{
            ////    result.Status = false;
            ////    result.Massage = "Fail Data";
            ////}
            //return result;
        }
        public KaizenResult UploadCaseData(string destinationPath, IList<Kaizen.Configuration.Kaizen00015> KaizenColumn)
        {
            DataTable dt = new DataTable();
            Configuration.CompanyacessServices srv = new Configuration.CompanyacessServices(this.UserContext);
            dt = srv.ReadExcelData(destinationPath);
            DataColumn newColumn = new DataColumn("UserName", typeof(System.String));
            newColumn.DefaultValue = UserContext.UserName.ToString();
            dt.Columns.Add(newColumn);
            KaizenResult result = new KaizenResult();
            result.Status = true;
            result.Massage = "Successfully Uploaded Data ";
            this.ExecuteSqlCommand("DELETE FROM CM00151 WHERE UserName ='" + UserContext.UserName.Trim() + "'");
            //return result;
            using (var context = new CMSContext(this.UserContext.CompanyID, UserContext.UserName, UserContext.Password))
            {
                try
                {
                    #region CM00151
                    using (SqlBulkCopy newsqlBulkCopy = new SqlBulkCopy(context.Database.Connection.ConnectionString, SqlBulkCopyOptions.KeepNulls & SqlBulkCopyOptions.Default))
                    {
                        newsqlBulkCopy.DestinationTableName = "dbo.CM00151";
                        newsqlBulkCopy.BatchSize = 10000;
                        foreach (Kaizen.Configuration.Kaizen00015 collumn in KaizenColumn)
                        {
                            if (collumn.FieldValue == "Fixed")
                            {
                                DataColumn FieldColumn = new DataColumn(collumn.FieldName, typeof(string));
                                FieldColumn.DefaultValue = collumn.FixedValue;
                                dt.Columns.Add(FieldColumn);
                                newsqlBulkCopy.ColumnMappings.Add(collumn.FieldName, collumn.FieldName);
                            }
                            else if (!string.IsNullOrEmpty(collumn.FieldValue) && collumn.FieldValue != "NULL")
                            {
                                newsqlBulkCopy.ColumnMappings.Add(collumn.FieldValue.Trim(), collumn.FieldName.Trim());
                            }
                        }
                        newsqlBulkCopy.ColumnMappings.Add("UserName", "UserName");
                        newsqlBulkCopy.WriteToServer(dt);
                    }
                    #endregion
                    this.ExecuteSqlCommand("exec [dbo].[CMS_UploadCaseTemp01] '" + UserContext.UserName.Trim() + "'");

                    
                    this.ExecuteSqlCommand("exec [dbo].[CMS_UploadCaseTemp02] '" + UserContext.UserName.Trim() + "'");
                }
                catch (Exception ex)
                {
                    result.Status = false;
                    result.Massage = ex.Message;
                    return result;
                }
            }

            return result;
        }

        public DataTable ExecuteSqlCommandToDataTable(string myQuery)
        {
            var result = _CM00203Repository.ExecuteSqlCommandToDataTable(myQuery);
            return result;
        }
        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _CM00203Repository.ExecuteSqlCommand(myQuery);
            return result;
        }

        #region Validate Debtor Data Upload 
        public DataCollection<CM00150> ValidateMissingDebtorIDData(int CurrentStep, int PageSize, int Page)
        {
            CM00150Repository rep = new CM00150Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00150> result = null;
            string Sql = string.Empty;
            switch (CurrentStep)
            {
                case 0:
                    Sql = "(EnglishFullName is null or EnglishFullName ='')";
                    break;
                case 1:
                    Sql = "((NationalityID is null or NationalityID = '') or ((NationalityID is not null or NationalityID <> '') and (NationalityID not in (select NationalityID from Sys00030))))";
                    break;
                case 2:
                    Sql = "(GenderID is not null or GenderID <>'') and (GenderID  not in (select GenderID from Sys00011))";
                    break;
                case 3: 
                    Sql = "(CUSTCLAS is not null or CUSTCLAS <> '') and CUSTCLAS not in (select CUSTCLAS from CM00010)";
                    break;
                case 4:
                    Sql = "(GroupID is not null or GroupID <> '') and GroupID not in (select GroupID from CM00011)";
                    break;
                case 5:
                    Sql = "(PriorityID is not null OR PriorityID <> '') and PriorityID not in (select PriorityID from CM00012)";
                    break;
                case 6:
                    Sql = "(DebtorStatusID is not null or DebtorStatusID <> '') and DebtorStatusID not in (select DebtorStatusID from CM00014)";
                    break;
                case 7:
                    Sql = "(CountryID is not null or CountryID <> '') and CountryID not in (select CountryID from Sys00013)";
                    break;
                case 8:
                    Sql = "(CityID is not null or CityID <> '') and CityID not in (select CityID from Sys00014)";
                    break;
            }
            Sql += " and  UserName ='" + this.UserContext.UserName.ToString() + "'";
            result = rep.GetWhereListWithPaging("CM00150", PageSize, Page, Sql, "DebtorID", false);
            return result;
        }
        public DataCollection<CM00151> GetCaseMissingData(int CurrentStep, int PageSize, int Page)
        {
            CM00151Repository rep = new CM00151Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00151> result = null;
            string Sql = string.Empty;
            switch (CurrentStep)
            {
                case 12:
                    Sql = "(AgentID is not null or AgentID <> '') and (AgentID  not in (select AgentID from CM00105))";
                    break;
                case 13:
                    Sql = "(CaseAccountNo is null or CaseAccountNo ='')";
                    break;
                case 14:
                    Sql = "(ClaimAmount is null or ClaimAmount ='' or ClaimAmount =0)";
                    break;
                case 15:
                    Sql = "(CIFNumber is null or CIFNumber ='')";
                    break;
                case 16:
                    Sql = "(CIFNumber is null or CIFNumber ='')";
                    break;
            }
            Sql += " and  UserName ='" + this.UserContext.UserName.ToString() + "'";
            result = rep.GetWhereListWithPaging("CM00151", PageSize, Page, Sql, "AgentID", false);
            return result;
        }
        public DataCollection<CM00151> ValidateCaseDuplicateForUploading(int PageSize, int Page, string Member, bool IsAscending)
        {
            CM00151Repository rep = new CM00151Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00151> result = null;
            result = rep.GetWhereListWithPaging("CM00151", PageSize, Page, " DuplicateRow>1 and UserName ='" + UserContext.UserName.Trim() + "'", Member, IsAscending);
            return result;
        }
        public DataCollection<CM00151> ValidateRepeatedCases(int PageSize, int Page, string Member, bool IsAscending)
        {
            CM00151Repository rep = new CM00151Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            this.ExecuteSqlCommand("exec [dbo].[CMS_UploadCaseTemp03] '" + UserContext.UserName.Trim() + "'");
            DataCollection<CM00151> result = null;
            string Filters = "UserName='" + UserContext.UserName.Trim() + "'";
            Filters += " and DuplicateRow =0";
            result = rep.GetWhereListWithPaging("CM00151", PageSize, Page, Filters, Member, IsAscending);
            return result;
        }
        public DataCollection<CM_UploadValidateNationalityID> GetAllCM_UploadValidateNationalityID(string Filters,
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
                    SeachStr += Help.GetFilter("DebtorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OldNationalityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("NewNationalityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("KaizenPublicKey", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM_UploadValidateNationalityIDRepository rep = new CM_UploadValidateNationalityIDRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM_UploadValidateNationalityID> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetListWithPaging(ss => ss.UserName == UserContext.UserName, PageSize, Page, ss => Member);
            else
                result = rep.GetWhereListWithPaging("CM_UploadValidateNationalityID", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<CM_UploadValidateMissingNationalityID> GetGridCM_UploadValidateMissingNationalityID(int PageSize, int Page)
        {
            CM_UploadValidateMissingNationalityIDRepository rep = new CM_UploadValidateMissingNationalityIDRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM_UploadValidateMissingNationalityID> result = null;
            result = rep.GetListWithPaging(ss => ss.UserName == UserContext.UserName, PageSize, Page, ss => ss.DebtorID);
            return result;
        }
        public DataCollection<CM_UploadValidateMissingNationalityID> GetCM_UploadValidateMissingNationalityID()
        {
            CM_UploadValidateMissingNationalityIDRepository rep = new CM_UploadValidateMissingNationalityIDRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM_UploadValidateMissingNationalityID> result = null;
            result = rep.GetListWithPaging(ss => ss.UserName == UserContext.UserName, 50, 1, ss => ss.DebtorID);
            return result;
        }

        public KaizenResult OverrideAll(int CurrentStep)
        {
            CM00150Repository _CM00150Repository = new CM00150Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            KaizenResult result = new KaizenResult();
            result.Status = true;
            result.Massage = "Successfully Upload Data";
            string sql = " update CM00100 ";
            switch (CurrentStep)
            {
                case 0:
                    sql += " set EnglishFullName = CM00150.EnglishFullName";
                    sql += " from CM00100 inner join CM00150 on CM00150.DebtorID = CM00100.DebtorID";
                    sql += " where UserName = '" + this.UserContext.UserName + "'";
                    break;
                case 1:
                    sql += "set VisaNo = null";
                    break;
                case 2:
                    sql += "set VisaExpiryDate = null";
                    break;
                case 3:
                    sql += "set PassportExpiryDate = null";
                    break;
                case 4:
                    sql += "set CPRExpiryDate = null";
                    break;
                case 5:
                    sql += "set SponsorName = null";
                    break;
                case 6:
                    sql += "set OccupationDescription1 = null";
                    break;
                case 7:
                    sql += "set EmployerName1 = null";
                    break;
                case 8:
                    sql += "set RoadNo = null";
                    break;
                case 9:
                    sql += "set ResidenceNo = null";
                    break;
                case 10:
                    sql += "set ContactNo = null";
                    break;
                case 11:
                    sql += "set NationalityID = null";
                    break;
                case 22:
                    sql = " update CM00203  set CaseStatusID = ''  , CaseStatusname = '' , CaseStatusChild = ''";
                    sql += ", CaseStatusChildName = '' from CM00203 where CaseRef not in (select CaseRef from CM00151 where UserName = '" + this.UserContext.UserName + "');";
                    break;
                case 23:
                    sql = "update CM00203 set ClaimAmount = CM00151.ClaimAmount ,Remarks = CM00151.Remarks ,AgentID = CM00151.AgentID ,InvoiceNumber = CM00151.InvoiceNumber from CM00203 inner join CM00151 on CM00151.CaseRef = CM00203.CaseRef ";
                    sql += " where CM00151.UserName='" + this.UserContext.UserName + "'";
                    break;
            }
            if (this.ExecuteSqlCommand(sql) >= 0)
            {
                result.Status = true;
            }
            else
            {
                result.Status = false;
                result.Massage = "Fail Data";
            }
            return result;
        }
        public KaizenResult ChangeOldCasesStatus(int CaseStatusID, string CaseStatusname
            , int CaseStatusChild, string CaseStatusChildName, string CaseStatusComment)
        {
            CM00150Repository _CM00150Repository = new CM00150Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            KaizenResult result = new KaizenResult();
            result.Status = true;
            result.Massage = "Successfully Upload Data";
            string sql = string.Empty;
            sql = " update CM00203  set CaseStatusID = '" + CaseStatusID + "'  , CaseStatusname = '" + CaseStatusname + "' , CaseStatusChild = '" + CaseStatusChild + "'";
            sql += ", CaseStatusChildName = '" + CaseStatusChildName + "', CaseStatusComment = '" + CaseStatusComment +
                "' from CM00203 inner join CM00152 on CM00152.ClientID = CM00203.ClientID and CM00152.UserName  = '" +
                 this.UserContext.UserName.Trim() +"' where CaseRef not in (select CaseRef from CM00151 where UserName = '" + this.UserContext.UserName + "' and CaseRef is not null)";
            sql += " and CM00152.UserName ='" + this.UserContext.UserName.Trim() +"'";
            if (this.ExecuteSqlCommand(sql) < 0)
            {
                result.Status = false;
                result.Massage = "Fail Data";
            }
            return result;
        }
        public KaizenResult UpdateCaseOptions(int CurrentStep, bool ClaimAmount, bool Remarks
    , bool AgentID, bool InvoiceNumber)
        {
            CM00150Repository _CM00150Repository = new CM00150Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            KaizenResult result = new KaizenResult();
            result.Status = true;
            result.Massage = "Successfully Upload Data";
            string sql = string.Empty;
            if (ClaimAmount || Remarks || AgentID || InvoiceNumber)
            {
                switch (CurrentStep)
                {
                    case 22:
                        if (ClaimAmount)
                        {
                            if (string.IsNullOrEmpty(sql))
                                sql += "update CM00203 set ClaimAmount = CM00151.ClaimAmount";
                            else
                                sql += ",ClaimAmount = CM00151.ClaimAmount";
                        }
                        if (Remarks)
                        {
                            if (string.IsNullOrEmpty(sql))
                                sql += "update CM00203 set Remarks = CM00151.Remarks";
                            else
                                sql += ",Remarks = CM00151.Remarks";

                        }
                        if (AgentID)
                        {
                            if (string.IsNullOrEmpty(sql))
                                sql += "update CM00203 set AgentID = CM00151.AgentID";
                            else
                                sql += ",AgentID = CM00151.AgentID";

                        }
                        if (InvoiceNumber)
                        {
                            if (string.IsNullOrEmpty(sql))
                                sql += "update CM00203 set InvoiceNumber = CM00151.InvoiceNumber";
                            else
                                sql += ",InvoiceNumber = CM00151.InvoiceNumber";

                        }
                        sql += " from CM00203 inner join CM00151 on CM00151.CaseRef = CM00203.CaseRef where CM00151.UserName='" + this.UserContext.UserName + "'";
                        break;
                }
                if (this.ExecuteSqlCommand(sql) >= 0)
                {
                    result.Status = true;
                }
                else
                {
                    result.Status = false;
                    result.Massage = "Fail Data";
                }
            }
            return result;
        }

        public KaizenResult DynamicCancel(CM_UploadValidate00 newTask, int CurrentStep)
        {
            CM00150Repository _CM00150Repository = new CM00150Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var CM00150 = _CM00150Repository.GetAll(ss => ss.DebtorID == newTask.DebtorID && ss.UserName == UserContext.UserName).FirstOrDefault();
            switch (CurrentStep)
            {
                case 5:
                    CM00150.EnglishFullName = null;
                    break;
                case 6:
                    CM00150.VisaNo = null;
                    break;
                case 7:
                    CM00150.VisaExpiryDate = null;
                    break;
                case 8:
                    CM00150.PassportExpiryDate = null;
                    break;
                case 9:
                    CM00150.CPRExpiryDate = null;
                    break;
                case 10:
                    CM00150.SponsorName = null;
                    break;
                case 11:
                    CM00150.OccupationDescription1 = null;
                    break;
                case 12:
                    CM00150.EmployerName1 = null;
                    break;
                //case 13:
                //    CM00150.RoadNo = null;
                //    break;
                //case 14:
                //    CM00150.ResidenceNo = null;
                //    break;
                //case 15:
                //    CM00150.ContactNo = null;
                //    break;
                case 16:
                    CM00150.NationalityID = null;
                    break;
            }
            var result = _CM00150Repository.UpdateKaizenObject(CM00150);
            return result;
        }
        public KaizenResult DynamicOverride(CM00151 newTask, int CurrentStep)
        {
            CM00203Repository _CM00100Repository = new CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var CM00100 = _CM00100Repository.GetSingle(ss => ss.CaseRef == newTask.CaseRef);
            CM00100.CIFNumber = newTask.CIFNumber;
            CM00100.ClaimAmount = newTask.ClaimAmount;
            //CM00100.CIFName = newTask.CIFName;
            CM00100.Remarks = newTask.Remarks;
            CM00100.AgentID = newTask.AgentID;
            CM00100.InvoiceNumber = newTask.InvoiceNumber;
            var result = _CM00100Repository.UpdateKaizenObject(CM00100);
            return result;
        }

        public KaizenResult CancelAllDuplicatedCases()
        {
            CM00150Repository _CM00150Repository = new CM00150Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            KaizenResult result = new KaizenResult();
            result.Status = true;
            result.Massage = "Successfully Upload Data";
            string sql = "DELETE w FROM CM00151 where CaseRef is not null";
            sql += " and UserName = '" + this.UserContext.UserName.Trim() + "'";
            if (this.ExecuteSqlCommand(sql) >= 0)
            {
                result.Status = true;
            }
            else
            {
                result.Status = false;
                result.Massage = "Fail Data";
            }
            return result;
        }
        public KaizenResult OverrideAllDuplicated()
        {
            CM00150Repository _CM00150Repository = new CM00150Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            KaizenResult result = new KaizenResult();
            result.Status = true;
            result.Massage = "Successfully Upload Data";
            string sql = "update CM00203 set  TrxTypeName = CM00151.TrxTypeName ,DebtorName = CM00151.DebtorName";
            sql += ", CaseStatusID = CM00151.CaseStatusID , CaseStatusChild = CM00151.CaseStatusChild";
            sql += ", CaseStatusname = CM00151.CaseStatusname, CaseStatusComment = CM00151.CaseStatusComment";
            sql += ", ClaimAmount = CM00151.ClaimAmount, PrincipleAmount = CM00151.PrincipleAmount";
            sql += ", AgentID = CM00151.AgentID FROM dbo.CM00203 INNER JOIN ";
            sql += "  dbo.CM00151 ON dbo.CM00151.CaseAccountNo = dbo.CM00203.CaseAccountNo";
            sql += " and dbo.CM00151.ClientID = dbo.CM00203.ClientID";
            sql += " and dbo.CM00151.ContractID = dbo.CM00203.ContractID";
            sql += " where CM00151.KaizenPublicKey = '" + this.UserContext.KaizenPublicKey.ToString() + "'";
            sql += ";delete CM00151 FROM dbo.CM00203 INNER JOIN ";
            sql += " dbo.CM00151 ON dbo.CM00151.CaseAccountNo = dbo.CM00203.CaseAccountNo";
            sql += " and dbo.CM00151.ClientID = dbo.CM00203.ClientID";
            sql += " and dbo.CM00151.ContractID = dbo.CM00203.ContractID";
            sql += " where CM00151.KaizenPublicKey = '" + this.UserContext.KaizenPublicKey.ToString() + "'";
            if (this.ExecuteSqlCommand(sql) >= 0)
            {
                result.Status = true;
            }
            else
            {
                result.Status = false;
                result.Massage = "Fail Data";
            }
            return result;
        }
        #endregion


        public DataCollection<CM_CaseNewForUploading> GetCM_CaseNewForUploading(string SQLQuery, int PageSize, int Page,
            string Member, bool IsAscending)
        {
            CM_CaseNewForUploadingRepository rep = new CM_CaseNewForUploadingRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM_CaseNewForUploading> result = null;
            result = rep.GetWhereListWithPaging("CM_CaseNewForUploading", PageSize, Page, SQLQuery, Member, IsAscending);
            return result;
        }
        public DataCollection<CM00155> ValidateOldCases(int PageSize, int Page, string Member, bool IsAscending, string Filter)
        {
            CM00155Repository rep = new CM00155Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00155> result = null;

            if (!string.IsNullOrEmpty(Filter))
                Filter += " and ";

            Filter += "dbo.CM00155.UserName ='" + UserContext.UserName.Trim() + "'";
            //Filters += " and CaseRef is not null";
            //result = rep.GetWhereListWithPaging("CM00151", PageSize, Page, Filters, Member, IsAscending);
            result = rep.GetWhereListWithPaging("CM00155", PageSize, Page, Filter, Member, IsAscending);
            return result;
        }
        public DataCollection<CM00151> ValidateCaseNew(int PageSize, int Page, string Member, bool IsAscending)
        {
            CM00151Repository rep = new CM00151Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00151> result = null;
            string Filters = "UserName='" + UserContext.UserName.Trim() + "'";
            Filters += " and DuplicateRow  =1 ";
            result = rep.GetWhereListWithPaging("CM00151", PageSize, Page, Filters, Member, IsAscending);
            return result;
        }
        public KaizenResult CancelCM_CaseNewForUploading(CM_CaseNewForUploading newTask)
        {
            CM00151Repository _CM00151Repository = new CM00151Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var CM00151 = _CM00151Repository.GetSingle(ss => ss.CaseRef == newTask.CaseRef && ss.UserName == UserContext.UserName);
            CM00151.CaseAccountNo = string.Empty;
            var result = _CM00151Repository.UpdateKaizenObject(CM00151);
            return result;
        }
        public KaizenResult CancelCM_CaseNewForUploading(IList<CM_CaseNewForUploading> newTask)
        {
            CM00151Repository _CM00151Repository = new CM00151Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            KaizenResult result = new KaizenResult();
            foreach (var item in newTask)
            {
                var CM00151 = _CM00151Repository.GetSingle(ss => ss.CaseRef == item.CaseRef && ss.UserName == UserContext.UserName);
                if (CM00151 != null)
                {
                    CM00151.CaseAccountNo = string.Empty;
                    result = _CM00151Repository.UpdateKaizenObject(CM00151);
                    if (!result.Status)
                        return result;
                }
            }
            return result;
        }

        public DataCollection<CM_CaseOldForUploading> GetCM_CaseOldForUploading(string Filters,
int PageSize, int Page, string Member, bool IsAscending)
        {
            CM_CaseOldForUploadingRepository rep = new CM_CaseOldForUploadingRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM_CaseOldForUploading> result = null;
            result = rep.GetWhereListWithPaging("CM_CaseOldForUploading", PageSize, Page, Filters, Member, IsAscending);
            return result;
        }
        public Boolean UploadingValidationOldCase(string ClientID)
        {
            CM_CaseOldForUploadingRepository rep19 = new CM_CaseOldForUploadingRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var result19 = rep19.GetListWithPaging(ss => ss.ClientID == ClientID, 5, 1, ss => ss.AgentID);
            if (result19 == null || result19.Items == null || result19.Items.Count == 0)
                return true;
            return false;
        }
        public string UploadingValidateDebtor(int CurrentStep)
        {
            CM_UploadValidate00Repository rep = new CM_UploadValidate00Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var result = rep.GetListWithPaging(ss => ss.UserName == UserContext.UserName && ss.AcctionStep == CurrentStep, 5, 1, ss => ss.DebtorID);
            if (result == null || result.Items == null || result.Items.Count == 0)
                return "OK";
            return result.Items.FirstOrDefault().ConditionVariable;
        }
        public Boolean UploadingValidation(int CurrentStep)
        {
            switch (CurrentStep)
            {
                case 0:
                    CM_UploadValidate00Repository _CM_UploadValidate00Repository = new CM_UploadValidate00Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    var result0 = _CM_UploadValidate00Repository.GetListWithPaging(ss => ss.UserName == UserContext.UserName, 5, 1, ss => ss.DebtorID);
                    if (result0 == null || result0.Items == null || result0.Items.Count == 0)
                        return true;
                    break;
                case 18:
                    CM_CaseMissingAgeUploadingRepository rep18 = new CM_CaseMissingAgeUploadingRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    var result18 = rep18.GetListWithPaging(ss => ss.KaizenPublicKey == UserContext.KaizenPublicKey, 5, 1, ss => ss.AgentID);
                    if (result18 == null || result18.Items == null || result18.Items.Count == 0)
                        return true;
                    break;
                case 19:
                    //CM_CaseDuplicateForUploadingRepository rep20 = new CM_CaseDuplicateForUploadingRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    //var result20 = rep20.GetListWithPaging(ss => ss.UserName == UserContext.UserName, 5, 1, ss => ss.AgentID);
                    //if (result20 == null || result20.Items == null || result20.Items.Count == 0)
                    //    return true;
                    break;
                case 20:
                    CM_CaseNewForUploadingRepository rep21 = new CM_CaseNewForUploadingRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    var result21 = rep21.GetListWithPaging(ss => ss.UserName == UserContext.UserName, 5, 1, ss => ss.AgentID);
                    if (result21 == null || result21.Items == null || result21.Items.Count == 0)
                        return true;
                    break;
                default:
                    CM_UploadValidate00Repository rep = new CM_UploadValidate00Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    var result = rep.GetListWithPaging(ss => ss.UserName == UserContext.UserName, 5, 1, ss => ss.DebtorID);
                    if (result == null || result.Items == null || result.Items.Count == 0)
                        return true;
                    break;
            }
            return false;
        }
        public KaizenResult PostDebtorData()
        {
            KaizenResult result = new KaizenResult();
            result.Status = false;
            result.Massage = "Fail Data";
            result.Status = true;
            string sql = "exec CMS_UploadDebtorPost01 '" + this.UserContext.UserName.ToString() + "'";
            if (this.ExecuteSqlCommand(sql) >= 0)
            {
                sql = "exec CMS_UploadDebtorPost02 '" + this.UserContext.UserName.ToString() + "'";
                if (this.ExecuteSqlCommand(sql) >= 0)
                {
                    sql = "exec CMS_UploadDebtorPost03 '" + this.UserContext.UserName.ToString() + "'";
                    if (this.ExecuteSqlCommand(sql) >= 0)
                    {
                        result.Status = true;
                        result.Massage = "Successfully Upload Data";
                    }
                }
            }
            return result;
        }
        public KaizenResult PostUploadCases()
        {
            KaizenResult result = new KaizenResult();
            try
            {
                result.Status = true;
                result.Massage = "Successfully Upload Data";
                string sql = "exec CMS_UploadCasePost01 '" + this.UserContext.UserName.Trim() + "'";
                if (this.ExecuteSqlCommand(sql) < 0)
                {
                    result.Status = false;
                    result.Massage = "Fail Data";
                    return result;
                }
                sql = "exec CMS_PostUploadCasesAfter '" + this.UserContext.UserName.Trim() + "'";
                if (this.ExecuteSqlCommand(sql) < 0)
                {
                    result.Status = false;
                    result.Massage = "Fail Data";
                }
            }
            catch (System.Exception ex)
            {
                result.Status = false;
                result.Massage = ex.Message;
            }
            return result;
        }
        public KaizenResult CaseReconcile()
        {
            KaizenResult result = new KaizenResult();
            try
            {
                result.Status = true;
                result.Massage = "Successfully Upload Data";
                string sql = "exec CMS_CaseTempReconcile01 ";
                if (this.ExecuteSqlCommand(sql) < 0)
                {
                    result.Status = false;
                    result.Massage = "Fail Data";
                    return result;
                }
                sql = "exec CMS_CaseTempReconcile02";
                if (this.ExecuteSqlCommand(sql) < 0)
                {
                    result.Status = false;
                    result.Massage = "Fail Data";
                }
                //sql = "exec CMS_CaseTempReconcile03";
                //if (this.ExecuteSqlCommand(sql) < 0)
                //{
                //    result.Status = false;
                //    result.Massage = "Fail Data";
                //}
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Massage = ex.Message;
            }
            return result;
        }
        //--------------------
        public KaizenResult ArchiveCase(CM00203 newTask)
        {
            CM20203Repository _CM20203Repository = new CM20203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            CM20203 CM20203 = new CM20203()
            {
                CaseRef = newTask.CaseRef,
                TRXTypeID = newTask.TRXTypeID,
                ContractID = newTask.ContractID,
                //ContractName = newTask.ContractName,
                ClientID = newTask.ClientID,
                ClientName = newTask.ClientName,
                BatchID = newTask.BatchID,
                CurrencyCode = newTask.CurrencyCode,
                //DecimalDigit = newTask.DecimalDigit,
                //IsMultiply = newTask.IsMultiply,
                //ExchangeRate = newTask.ExchangeRate,
                CIFNumber = newTask.CIFNumber,
                CIFName = newTask.CIFName,
                IsJoint = newTask.IsJoint,
                //WebSite = newTask.WebSite,
                //CountryID = newTask.CountryID,
                //CityID = newTask.CityID,
                //Phone01 = newTask.Phone01,
                //Phone02 = newTask.Phone02,
                //Ext1 = newTask.Ext1,
                //Ext2 = newTask.Ext2,
                //MobileNo1 = newTask.MobileNo1,
                //MobileNo2 = newTask.MobileNo2,
                //POBox = newTask.POBox,
                //Other01 = newTask.Other01,
                //Other02 = newTask.Other02,
                //Address1 = newTask.Address1,
                //Address2 = newTask.Address2,
                //Email01 = newTask.Email01,
                //Email02 = newTask.Email02,
                //BucketID = newTask.BucketID,
                CaseStatusID = newTask.CaseStatusID,
                CaseStatusChild = newTask.CaseStatusChild,
                CaseStatusname = newTask.CaseStatusname,
                CaseStatusChildName = newTask.CaseStatusChildName,
                CaseStatusComment = newTask.CaseStatusComment,
                ReminderDateTime = newTask.ReminderDateTime,
                PTPAMT = newTask.PTPAMT,
                CaseAddess = newTask.CaseAddess,
                CaseAccountNo = newTask.CaseAccountNo,
                InvoiceNumber = newTask.InvoiceNumber,
                ClosingDate = newTask.ClosingDate,
                TransactionDate = newTask.TransactionDate,
                Remarks = newTask.Remarks,
                //OSAmount = newTask.OSAmount,
                FinanceCharge = newTask.FinanceCharge,
                ClaimAmount = newTask.ClaimAmount,
                PrincipleAmount = newTask.PrincipleAmount,
                //CreatedDate = newTask.CreatedDate,
                AgentID = newTask.AgentID,
                //AssignDate = newTask.AssignDate,
                LastPaymentDate = newTask.LastPaymentDate,
                LastCallactedAMT = newTask.LastCallactedAMT,
                TotalCallactedAMT = newTask.TotalCallactedAMT,
                LastPaymentBy = newTask.LastPaymentBy,
            };

            var result = _CM20203Repository.AddKaizenObject(CM20203);
            if (result.Status)
            {
                #region Transfer Assign History
                CM00206Repository _CM00206Repository = new CM00206Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                CM20206Repository _CM20206Repository = new CM20206Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                IList<CM00206> CM00206List = _CM00206Repository.GetAll(ss => ss.CaseRef == newTask.CaseRef);
                List<CM20206> CM20206List = new List<CM20206>();
                if (CM00206List != null)
                {
                    foreach (var item in CM00206List.ToList())
                    {
                        CM20206 CM20206 = new CM20206()
                        {
                            //AssignHistoryID = item.AssignHistoryID,
                            ////TrxID = item.TrxID,
                            AgentID = item.AgentID,
                            AssignHistoryDate = item.AssignHistoryDate,
                        };
                        CM20206List.Add(CM20206);
                    }
                    _CM20206Repository.AddKaizenObject(CM20206List.ToArray());
                }
                #endregion

                #region Transfer Status History
                CM10701Repository _CM10701Repository = new CM10701Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                CM20205Repository _CM20205Repository = new CM20205Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                IList<CM10701> CM10701List = _CM10701Repository.GetAll(ss => ss.CaseRef == newTask.CaseRef);
                List<CM20205> CM20205List = new List<CM20205>();
                if (CM10701List != null)
                {
                    foreach (var item in CM20205List.ToList())
                    {
                        CM20205 CM20205 = new CM20205()
                        {
                            StatusHistoryID = item.StatusHistoryID,
                            CaseStatusID = item.CaseStatusID,
                            //TrxID = item.TrxID,
                            //AgentID = item.AgentID,
                            //CreatedDate = item.CreatedDate,
                            ReminderDateTime = item.ReminderDateTime,
                            CaseStatusComment = item.CaseStatusComment,
                            PTPAMT = item.PTPAMT
                        };
                        CM20205List.Add(CM20205);
                    }
                    _CM20205Repository.AddKaizenObject(CM20205List.ToArray());
                }
                #endregion

                _CM00203Repository.DeleteKaizenObject(newTask);

            }
            return result;
        }
        public KaizenResult DeleteCase(CM00203 newTask)
        {
            CM30203Repository _CM30203Repository = new CM30203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            CM30203 CM30203 = new CM30203()
            {
                CaseRef = newTask.CaseRef,
                TRXTypeID = newTask.TRXTypeID,
                ContractID = newTask.ContractID,
                //ContractName = newTask.ContractName,
                ClientID = newTask.ClientID,
                ClientName = newTask.ClientName,
                BatchID = newTask.BatchID,
                CurrencyCode = newTask.CurrencyCode,
                //DecimalDigit = newTask.DecimalDigit,
                //IsMultiply = newTask.IsMultiply,
                //ExchangeRate = newTask.ExchangeRate,
                CIFNumber = newTask.CIFNumber,
                CIFName = newTask.CIFName,
                IsJoint = newTask.IsJoint,
                //WebSite = newTask.WebSite,
                //CountryID = newTask.CountryID,
                //CityID = newTask.CityID,
                //Phone01 = newTask.Phone01,
                //Phone02 = newTask.Phone02,
                //Ext1 = newTask.Ext1,
                //Ext2 = newTask.Ext2,
                //MobileNo1 = newTask.MobileNo1,
                //MobileNo2 = newTask.MobileNo2,
                //POBox = newTask.POBox,
                //Other01 = newTask.Other01,
                //Other02 = newTask.Other02,
                //Address1 = newTask.Address1,
                //Address2 = newTask.Address2,
                //Email01 = newTask.Email01,
                //Email02 = newTask.Email02,
                //BucketID = newTask.BucketID,
                CaseStatusID = newTask.CaseStatusID,
                CaseStatusChild = newTask.CaseStatusChild,
                CaseStatusname = newTask.CaseStatusname,
                CaseStatusChildName = newTask.CaseStatusChildName,
                CaseStatusComment = newTask.CaseStatusComment,
                ReminderDateTime = newTask.ReminderDateTime,
                PTPAMT = newTask.PTPAMT,
                CaseAddess = newTask.CaseAddess,
                CaseAccountNo = newTask.CaseAccountNo,
                InvoiceNumber = newTask.InvoiceNumber,
                ClosingDate = newTask.ClosingDate,
                TransactionDate = newTask.TransactionDate,
                Remarks = newTask.Remarks,
                //OSAmount = newTask.OSAmount,
                FinanceCharge = newTask.FinanceCharge,
                ClaimAmount = newTask.ClaimAmount,
                PrincipleAmount = newTask.PrincipleAmount,
                //CreatedDate = newTask.CreatedDate,
                AgentID = newTask.AgentID,
                //AssignDate = newTask.AssignDate,
                LastPaymentDate = newTask.LastPaymentDate,
                LastCallactedAMT = newTask.LastCallactedAMT,
                TotalCallactedAMT = newTask.TotalCallactedAMT,
                LastPaymentBy = newTask.LastPaymentBy,
            };

            var result = _CM30203Repository.AddKaizenObject(CM30203);
            if (result.Status)
                _CM00203Repository.DeleteKaizenObject(newTask);
            return result;
        }

        public List<CMV00201> CaseBuketReport(string CaseAccountNo)
        {
            CMV00201Repository rep = new CMV00201Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password.Trim());
            List<CMV00201> L = rep.GetAll(ss => ss.CaseAccountNo == "1-8242707690").ToList();
            return L;
        }

        public CM00203 GetCM00203FromCaseRef(string CaseRef)
        {
            CM00203Repository rep = new CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password.Trim());
            CM00203 L = rep.GetAll(ss => ss.CaseRef.Trim().Equals(CaseRef.Trim())).FirstOrDefault();
            return L;
        }
    }
}
