using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM10701Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM10701Repository _CM10701Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM10701Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM10701Repository = new CM10701Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM10701> GetAllViewBYSQLQuery(string Filters,
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

            CM10701Repository rep = new CM10701Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM10701> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM10701", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM10701> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM10701> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM10701Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM10701Repository.GetWhereListWithPaging("CM10701", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM10701> GetAllViewBYSQLQuery(string Filters,
    string Searchcritaria, string CaseRef,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            DataCollection<CM10701> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM10701Repository.GetListWithPaging(ss => ss.CaseRef.Trim() == CaseRef.Trim(),
                    PageSize, Page, ss => Member);
            }
            else
            {
                SeachStr += " and CaseRef='" + CaseRef.Trim() + "'";
                result = _CM10701Repository.GetWhereListWithPaging("CM10701", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<CM10701> GetAllViewBYSQLQuery(string Filters,
            string Searchcritaria, string CaseRef, bool WithHistory,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            DataCollection<CM10701> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                if (WithHistory)
                {
                    CM20205Repository _CM20205Repository = new CM20205Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    DataCollection<CM20205> result2 = _CM20205Repository.GetWhereListWithPaging("CM20205", PageSize, Page, SeachStr, Member, IsAscending);

                    DataCollection<CM10701> result1 = _CM10701Repository.GetListWithPaging(ss => ss.CaseRef.Trim() == CaseRef.Trim(),
                        PageSize, Page, ss => Member);
                    result = result1;
                    if (result2 != null)
                    {
                        if (result2.Items != null)
                        {
                            foreach (var o in result2.Items)
                            {
                                result.Items.Add(new CM10701()
                                {
                                    StatusHistoryID = o.StatusHistoryID,
                                    CaseStatusID = o.CaseStatusID,
                                    CaseRef = CaseRef,
                                    //AgentID = o.AgentID,
                                    //CreatedDate = o.CreatedDate,
                                    ChangeStatusSourceID = 4,
                                    CaseStatusComment = o.CaseStatusComment,
                                    PTPAMT = o.PTPAMT,
                                    ReminderDateTime = o.ReminderDateTime
                                }
                                    );

                            }
                        }
                    }
                }
                else
                {
                    result = _CM10701Repository.GetListWithPaging(ss => ss.CaseRef.Trim() == CaseRef.Trim(),
                                       PageSize, Page, ss => Member);
                }
            }
            else
            {
                if (WithHistory)
                {
                    SeachStr += " and CaseRef='" + CaseRef.Trim() + "'";
                    CM20205Repository _CM20205Repository = new CM20205Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    DataCollection<CM20205> result2 = _CM20205Repository.GetWhereListWithPaging("CM20205", PageSize, Page, SeachStr, Member, IsAscending);
                    DataCollection<CM10701> result1 = _CM10701Repository.GetWhereListWithPaging("CM10701", PageSize, Page, SeachStr, Member, IsAscending);
                    result = result1;
                    if (result2 != null)
                    {
                        if (result2.Items != null)
                        {
                            foreach (var o in result2.Items)
                            {
                                result.Items.Add(new CM10701()
                                {
                                    StatusHistoryID = o.StatusHistoryID,
                                    CaseStatusID = o.CaseStatusID,
                                    CaseRef = CaseRef,
                                    //AgentID = o.AgentID,
                                    //CreatedDate = o.CreatedDate,
                                    ChangeStatusSourceID = 4,
                                    CaseStatusComment = o.CaseStatusComment,
                                    PTPAMT = o.PTPAMT,
                                    ReminderDateTime = o.ReminderDateTime
                                }
                                    );

                            }
                        }
                    }
                }
                else
                {
                    SeachStr += " and CaseRef='" + CaseRef.Trim() + "'";
                    result = _CM10701Repository.GetWhereListWithPaging("CM10701", PageSize, Page, SeachStr, Member, IsAscending);
                }
            }

            return result;
        }

        //public IList<CM10701View01> GetAllCM10701View01()
        //{
        //    List<CM10701View01> result = new List<CM10701View01>();
        //    CM10701View01Repository rep = new CM10701View01Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
        //    result = rep.GetAll().ToList();
        //    return result;
        //}
        public DataCollection<CM10701> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM10701Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM10701> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM10701Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM10701> result = tasks;
                return result;
            }

        }
        public DataCollection<CM10701> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM10701> result = null;
            var tasks = _CM10701Repository.GetListWithPaging(PageSize, Page, ss => new { ss.StatusHistoryID });
            result = tasks;
            return result;
        }
        public IList<CM10701> GetAll()
        {
            var tasks = _CM10701Repository.GetAll();
            IList<CM10701> result = tasks;
            return result;
        }
        public IList<CM10701> GetAllByAgent(string AgentID)
        {
            var tasks = _CM10701Repository.GetAll(xx => xx.AgentID == AgentID);
            IList<CM10701> result = tasks;
            return result;
        }

        public CM10701 GetSingle(int StatusHistoryID)
        {
            var tasks = _CM10701Repository.GetSingle(x => x.StatusHistoryID == StatusHistoryID);
            return tasks;
        }
        public KaizenResult AddCM10701(IList<CM00060> CM00060List, CM10701 newTask)
        {
            if (!string.IsNullOrEmpty(newTask.CaseStatusComment))
                newTask.CaseStatusComment = newTask.CaseStatusComment.Trim();

            string Fields = string.Empty;
            if (CM00060List != null)
            {
                foreach (CM00060 oCM00060 in CM00060List)
                {
                    switch (oCM00060.FieldTypeID)
                    {
                        case 1:
                            Fields += oCM00060.FieldCode.Trim() + ",";
                            Fields += oCM00060.FieldCode.Trim() + "ValueName,";
                            break;
                        case 2:
                            Fields += oCM00060.FieldCode.Trim() + ",";
                            break;
                        case 3:
                            Fields += oCM00060.FieldCode.Trim() + ",";
                            break;
                        case 4:
                            Fields += oCM00060.FieldCode.Trim() + ",";
                            break;
                    }
                }
                Fields = Fields.Substring(0, Fields.Length - 1);
                Fields = "insert into CM00700_" + newTask.CaseStatusID.ToString() +
                    "(ClaimAmount,CaseStatusID,CaseStatusChild,CaseStatusname,CaseStatusChildName"
                    + ",StatusScriptID,CaseRef,AgentID,CreatedDate,ChangeStatusSourceID"
                    + "," + Fields + ")";
                string Fieldsvlues = string.Empty;
                Fieldsvlues += "values(" + newTask.ClaimAmount.ToString() + "," 
                    + newTask.CaseStatusID.ToString();
                Fieldsvlues += "," +(string.IsNullOrEmpty(newTask.CaseStatusChild.ToString())? "null": newTask.CaseStatusChild.ToString());
                Fieldsvlues += ",'" + newTask.CaseStatusname + "'";
                Fieldsvlues += "," + (string.IsNullOrEmpty(newTask.CaseStatusChildName) ? "null" :"'"+ newTask.CaseStatusChildName + "'");
                Fieldsvlues += "," + (newTask.StatusScriptID.HasValue ? newTask.StatusScriptID.Value.ToString() : "null");
                Fieldsvlues += ",'" + newTask.CaseRef.Trim() + "'";
                Fieldsvlues += ",'" + newTask.AgentID.Trim() + "'";
                Fieldsvlues += ",getdate(),1,";
                foreach (CM00060 oCM00060 in CM00060List)
                {
                    switch (oCM00060.FieldTypeID)
                    {
                        case 1:
                            if (string.IsNullOrEmpty(oCM00060.FieldValue))
                            {
                                Fieldsvlues += "'','',";
                            }
                            else
                            {
                                Fieldsvlues += "'" + oCM00060.FieldValue.Trim() + "',";
                                Fieldsvlues += "'" + oCM00060.FieldName.Trim() + "',";
                            }
                            break;
                        case 2:
                            if (string.IsNullOrEmpty(oCM00060.FieldValue))
                                Fieldsvlues += "null,";
                            else
                                Fieldsvlues += "'" + oCM00060.FieldDateValue.ToString() + "',";
                            break;
                        case 3:
                            if (string.IsNullOrEmpty(oCM00060.FieldValue))
                                Fieldsvlues += "null,";
                            else
                                Fieldsvlues += "'" + oCM00060.FieldValue.Trim() + "',";
                            break;
                        case 4:
                            if (string.IsNullOrEmpty(oCM00060.FieldValue))
                                Fieldsvlues += "0,";
                            else
                                Fieldsvlues += "'" + oCM00060.FieldValue.Trim() + "',";
                            break;
                    }
                }
                Fieldsvlues = Fieldsvlues.Substring(0, Fieldsvlues.Length - 1);
                Fields = Fields + Fieldsvlues + ");";
            }

            string sql = "insert into CM10701(StatusActionTypeID,TRXTypeID,CurrencyCode,LastCaseStatusID,ClaimAmount,CaseStatusID,CaseStatusChild,CaseStatusname,CaseStatusChildName,StatusScriptID,CaseRef,AgentID,CreatedDate,ChangeStatusSourceID,ReminderDateTime,CaseStatusComment, PTPAMT)";
            sql += "values("+ newTask.StatusActionTypeID.ToString() + ","+ newTask.TRXTypeID.ToString() + ",'"+ newTask.CurrencyCode.Trim() + "',"
                + (string.IsNullOrEmpty(newTask.LastCaseStatusID.ToString()) ? "null" : newTask.LastCaseStatusID.ToString())
                + "," + newTask.ClaimAmount.ToString() + "," + newTask.CaseStatusID.ToString();
            sql += "," + (string.IsNullOrEmpty(newTask.CaseStatusChild.ToString()) ? "null" : newTask.CaseStatusChild.ToString());
            sql += ",'" + newTask.CaseStatusname + "'";
            sql += ",'" + newTask.CaseStatusChildName + "'";
            sql += "," + (newTask.StatusScriptID.HasValue ? newTask.StatusScriptID.Value.ToString() : "null");
            sql += ",'" + newTask.CaseRef.Trim() + "'";
            sql += ",'" + newTask.AgentID.Trim() + "'";
            sql += ",getdate(),1,";
            if (newTask.ReminderDateTime.HasValue)
                sql += "'" + newTask.ReminderDateTime.ToString() + "'";
            else
                sql += "null";
            sql += ",'" + newTask.CaseStatusComment;
            sql += "'," + (newTask.PTPAMT.HasValue ? newTask.PTPAMT.ToString() : "0");
            sql += ")";
            sql = Fields + sql;
            KaizenResult result = _CM10701Repository.ExecuteSqlCommandKaizenObject(sql);
            return result;
        }
        public KaizenResult AddCM10701(CM10701 newTask)
        {
            if (!string.IsNullOrEmpty(newTask.CaseStatusComment))
                newTask.CaseStatusComment = newTask.CaseStatusComment.Trim();
            string sql = "insert into CM10701(ClaimAmount,CaseStatusID,CaseStatusChild,CaseStatusname,CaseStatusChildName,StatusScriptID,CaseRef,AgentID,CreatedDate,ChangeStatusSourceID,ReminderDateTime,CaseStatusComment, PTPAMT)";
            sql += "values(" + newTask.ClaimAmount.ToString() + "," + newTask.CaseStatusID.ToString();
            sql += "," + newTask.CaseStatusChild.ToString();
            sql += ",'" + newTask.CaseStatusname + "'";
            sql += ",'" + newTask.CaseStatusChildName + "'";
            sql += ",'" + newTask.StatusScriptID + "'";
            sql += ",'" + newTask.CaseRef + "'";
            sql += ",'" + newTask.AgentID + "',getdate(),1";
            if (newTask.ReminderDateTime.HasValue)
                sql +="'"+ newTask.ReminderDateTime.ToString() +"'";
            else
                sql += "null";
            sql += ",'" + newTask.CaseStatusComment;
            sql += "'," + (newTask.PTPAMT.HasValue? newTask.PTPAMT.ToString():"0");
            //sql += "," + (newTask.Date01.HasValue ? "'" + newTask.Date01.ToString() + "'" : "null");
            //sql += "," + (newTask.Date02.HasValue ? "'" + newTask.Date02.ToString() + "'" : "null");
            //sql += "," + (newTask.Date03.HasValue ? "'" + newTask.Date03.ToString() + "'" : "null");
            //sql += "," + (newTask.Date04.HasValue ? "'" + newTask.Date04.ToString() + "'" : "null");
            //sql += "," + (newTask.Date05.HasValue ? "'" + newTask.Date05.ToString() + "'" : "null");
            ////sql += "','" + newTask.Date06.ToString();
            //sql += ",null";
            //sql += "," + newTask.AMT01.ToString();
            //sql += "," + newTask.AMT02.ToString();
            //sql += "," + newTask.AMT03.ToString();
            //sql += "," + newTask.AMT04.ToString();
            //sql += "," + newTask.AMT05.ToString();
            ////sql += "," + newTask.AMT06.ToString();
            //sql += ",0";
            //sql += ",'" + newTask.Lookup01;
            //sql += "','" + newTask.Lookup02;
            //sql += "','" + newTask.Lookup03;
            //sql += "','" + newTask.Lookup04;
            //sql += "','" + newTask.Lookup05;
            //sql += "','" + newTask.Lookup06;
            //sql += "','" + newTask.TxtField01;
            //sql += "','" + newTask.TxtField02;
            //sql += "','" + newTask.TxtField03;
            //sql += "','" + newTask.TxtField04;
            //sql += "','" + newTask.TxtField05;
            //sql += "','" + newTask.TxtField06;
            //sql += "','" + newTask.TxtBigField01;
            //sql += "','" + newTask.TxtBigField02;
            //sql += "','" + newTask.TxtBigField03;
            //sql += "','" + newTask.TxtBigField04;
            //sql += "','" + newTask.TxtBigField05;
            //sql += "','" + newTask.TxtBigField06;
            //sql += "'," + (newTask.IntField01.HasValue ? newTask.IntField01.ToString() : "0");
            //sql += "," + (newTask.IntField02.HasValue ? newTask.IntField02.ToString() : "0");
            //sql += "," + (newTask.IntField03.HasValue ? newTask.IntField03.ToString() : "0");
            //sql += "," + (newTask.IntField04.HasValue ? newTask.IntField04.ToString() : "0");
            //sql += "," + (newTask.IntField05.HasValue ? newTask.IntField05.ToString() : "0");
            //sql += "," + (newTask.IntField06.HasValue ? newTask.IntField06.ToString() : "0");

            //sql += "," + (newTask.CheckBoxField01.HasValue ? (newTask.CheckBoxField01.Value ? 1 : 0).ToString() : "null");
            //sql += "," + (newTask.CheckBoxField02.HasValue ? (newTask.CheckBoxField02.Value ? 1 : 0).ToString() : "null");
            //sql += "," + (newTask.CheckBoxField03.HasValue ? (newTask.CheckBoxField03.Value ? 1 : 0).ToString() : "null");
            //sql += "," + (newTask.CheckBoxField04.HasValue ? (newTask.CheckBoxField04.Value ? 1 : 0).ToString() : "null");
            //sql += "," + (newTask.CheckBoxField05.HasValue ? (newTask.CheckBoxField05.Value ? 1 : 0).ToString() : "null");
            //sql += "," + (newTask.CheckBoxField06.HasValue ? (newTask.CheckBoxField06.Value? 1:0).ToString() : "null");
            sql += ")";
            KaizenResult result = _CM10701Repository.ExecuteSqlCommandKaizenObject(sql);
            return result;
        }
        public KaizenResult Update(CM10701 newTask)
        {
            KaizenResult result = _CM10701Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(int StatusHistoryID)
        {
            KaizenResult result = _CM10701Repository.RemoveKaizenObject(xx => xx.StatusHistoryID == StatusHistoryID);
            return result;
        }
        
    }
}
