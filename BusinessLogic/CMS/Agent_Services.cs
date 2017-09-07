using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS;
using Kaizen.CMS.Repository;
using Kaizen.HumanResources.Repository;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00105Services
    {
        #region Infrastructure

        private Kaizen.HumanResources.Repository.CM00105Repository _CM00105Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00105Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00105Repository = new CM00105Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00105> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00105> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00105Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00105Repository.GetWhereListWithPaging("CM00105", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00105> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("AgentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AgentGroup", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UserLevelID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmployeeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SupervisorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SuffixID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MidName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FirstName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LastName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InactiveEmployee", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmailAddress", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DirectPhon", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UserCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00105Repository rep = new CM00105Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00105> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00105", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public List<KCM00203_Result> GetAgentListBySupervisor(DateTime StartDate, DateTime EndDate)
        {
            var tasks = _CM00105Repository.GetAgentListBySupervisor(this.UserContext.UserName.Trim()
                , StartDate, EndDate);
            List<KCM00203_Result> result = tasks;
            return result;
        }
        public DataCollection<CM00105> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<CM00105> L = null;
            var tasks = _CM00105Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<CM00105> result = tasks;
            return result;
        }
        public DataCollection<CM00105> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<CM00105> result = null;
            var tasks = _CM00105Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public DataCollection<CM00105> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00105> L = null;
            var tasks = _CM00105Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00105> result = tasks;
            return result;
        }
        public DataCollection<CM00105> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00105> result = null;
            var tasks = _CM00105Repository.GetListWithPaging(PageSize, Page, ss => new { ss.AgentID });
            result = tasks;
            return result;
        }
        public IList<CM00212> GetAgentTargetByCLient(string ClientID, string AgentID, string YearCode)
        {
            Kaizen.CMS.Repository.CM00212Repository rep = new Kaizen.CMS.Repository.CM00212Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(ss => ss.ClientID == ClientID && ss.AgentID == AgentID && ss.YearCode == YearCode);
            IList<CM00212> result = tasks;
            return result;
        }
        public CM00105 GetSingle(string AgentID)
        {
            Kaizen.HumanResources.Repository.CM00105Repository _CM00105Repository = new CM00105Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM00105Repository.GetSingle(x => x.AgentID == AgentID);
            return tasks;
        }
        public CM00105 GetSingleByUserCode(string UserCode)
        {
            Kaizen.HumanResources.Repository.CM00105Repository _CM00105Repository = new CM00105Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM00105Repository.GetSingle(x => x.UserCode == UserCode);
            return tasks;
        }
        public DataCollection<CM00105> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00105> result = null;
            Kaizen.HumanResources.Repository.CM00105Repository _CM00105Repository = new CM00105Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM00105Repository.GetListWithPaging(SearchTerm,
                PageSize, Page, ss => ss.AgentID, true);
            result = tasks;
            return result;
        }

        public IList<CM00105> GetAll()
        {
            var tasks = _CM00105Repository.GetAll();
            IList<CM00105> result = tasks;
            return result;
        }
        public IList<CM00105> GetAllByGroup(string AgentGroup)
        {
            var tasks = _CM00105Repository.GetAll(xx => xx.AgentGroup == AgentGroup);
            IList<CM00105> result = tasks;
            return result;
        }

        public KaizenResult AddCM00105(CM00105 newTask)
        {
            var result = _CM00105Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00105(IList<CM00105> newTask)
        {
            var result = _CM00105Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00105 newTask)
        {
            var result = _CM00105Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00105> newTask)
        {
            var result = _CM00105Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00105 newTask)
        {
            var result = _CM00105Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00105> newTask)
        {
            var result = _CM00105Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        public string GetNextAgent(string AgentGroup)
        { 
            if (!Master.GetCMSConfiguration(this.UserContext.CompanyID).IsAgentSerial)
                return string.Empty;
            CM00023Services srv = new CM00023Services(this.UserContext);
            CM00023 classtemp = srv.GetSingle(AgentGroup);
            string SequenceName = "CMS_Agent_" + classtemp.AgentGroup.Trim() + "_Sequence";
            int KaizenID = -1;
            Kaizen.Admin.Repository.Sys00000Repository Repository = new Kaizen.Admin.Repository.Sys00000Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            List<Kaizen.Admin.Sys00000> AllSessions = Repository.GetList(xx => xx.SequenceName.Trim() == SequenceName.Trim()).ToList();
            Kaizen.Admin.Sys00000 temp = AllSessions.Find(xx => xx.UserName.Trim() == UserContext.UserName.Trim());
            if (temp == null)
            {
                string SQL = string.Empty;
                foreach (Kaizen.Admin.Sys00000 row in AllSessions)
                {
                    Kaizen.Configuration.KaizenSession oKaizenSession = Configuration.UserServices.GetUserSession(row.UserName, UserContext.CompanyID);
                    if (oKaizenSession == null)
                    {
                        SQL = "update Sys00000 set UserName = '" + UserContext.UserName.Trim() + "' where UserName ='" +
                            row.UserName.TrimStart() + "' and SequenceName ='" + SequenceName + "'";
                        _CM00105Repository.ExecuteSqlCommand(SQL);
                        KaizenID = row.KaizenID;
                        break;
                    }
                    bool notUse = true;
                    foreach (Kaizen.Configuration.Screen scrn in oKaizenSession.Screens)
                    {
                        if (scrn == Kaizen.Configuration.Screen.Agent)
                        {
                            notUse = false;
                            break;
                        }
                    }
                    if (notUse)
                    {
                        KaizenID = row.KaizenID;
                        break;
                    }
                }
            }
            else
                KaizenID = temp.KaizenID;
            ////-----------New ID-----------------------------------------------------
            if (KaizenID == -1)
            {
                string SQL = "SELECT NEXT VALUE FOR " + SequenceName;
                long? SequenceNumber = Repository.GetSQLLong(SQL);
                if (SequenceNumber.HasValue)
                {
                    KaizenID = (int)SequenceNumber.Value;
                    SQL = "insert into Sys00000(UserName,SequenceName,KaizenID) values (";
                    SQL += "'" + UserContext.UserName + "','" + SequenceName + "'," + SequenceNumber.ToString() + ");";
                    SQL += "update CM00023 set NextDocumentNumber =" + KaizenID.ToString() + " where AgentGroup='" + classtemp.AgentGroup.Trim() + "'";
                    Repository.ExecuteSqlCommand(SQL);
                }
                else
                    return "";
            }
            if (UserContext.Screens == null)
                UserContext.Screens = new List<Kaizen.Configuration.Screen>();
            UserContext.Screens.Add(Kaizen.Configuration.Screen.Agent);
            int templenth = 0;
            if (!classtemp.NextNumberlenth.HasValue)
                templenth = KaizenID.ToString().Length;
            else
                templenth = classtemp.NextNumberlenth.Value - KaizenID.ToString().Length;
            string Str = string.Empty;
            for (byte i = 1; i <= templenth; i++)
            {
                Str = Str + "0";
            }
            string DOPrefix = string.Empty;
            if (!string.IsNullOrEmpty(classtemp.NextNumberPrefix))
                DOPrefix = classtemp.NextNumberPrefix.Trim();
            return DOPrefix + Str + KaizenID.ToString();
        }
        public KaizenResult AddCM10110(IList<CM10110> newTask)
        {
            CM10110 first = newTask.FirstOrDefault();
            CM10110Services srv = new CM10110Services(this.UserContext);
            IList<CM10110> ss = srv.GetAll(first.AgentID, first.YearCode);
            if (ss.Count == 0)
            {
                foreach (CM10110 row in newTask)
                {
                    CM10110 o = new CM10110();
                    o.YearCode = row.YearCode;
                    o.AgentID = row.AgentID;
                    o.PERIODID = row.PERIODID;
                    srv.AddCM10110(o);
                }
            }
            KaizenResult result = null;
            Kaizen.CMS.Repository.CM10110Repository rep = new Kaizen.CMS.Repository.CM10110Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            result = rep.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult AddCM00107(IList<CM00107> newTask)
        {
            CM00107 first = newTask.FirstOrDefault();
            CM10110Services srv = new CM10110Services(this.UserContext);
            IList<CM10110> ss = srv.GetAll(first.AgentID, first.YearCode);
            if (ss.Count == 0)
            {
                foreach (CM00107 row in newTask)
                {
                    CM10110 o = new CM10110();
                    o.YearCode = row.YearCode;
                    o.AgentID = row.AgentID;
                    o.PERIODID = row.PERIODID;
                    srv.AddCM10110(o);
                }
            }
            KaizenResult result = null;
            Kaizen.CMS.Repository.CM00107Repository rep = new Kaizen.CMS.Repository.CM00107Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            switch (first.Status)
            {
                case 0:

                    result = rep.AddKaizenObject(newTask.ToArray());
                    break;
                case 1:
                    result = rep.UpdateKaizenObject(newTask.ToArray());
                    break;
            }
            return result;
        }

        #region Agent Case Status
        public IList<CM00203> GetAgentCaseStatus()
        {
            CM00203Repository rep = new CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var agentCaseStatus = rep.GetAll().Where(x=>x.ReminderDateTime!=null).ToList();
            foreach (CM00203 obj in agentCaseStatus)
            {
                obj.ClosingDate = obj.ReminderDateTime.Value.AddMinutes(1);
            }
            IList<CM00203> result = agentCaseStatus;
            return result;
        }

        public IList<CM00105> GetAllAgents()
        {
            CM00105Repository rep = new CM00105Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var agents = rep.GetAll();
            IList<CM00105> result = agents;
            return result;
        }

        #endregion

        #region Target Assignment
        public KaizenResult AddCM00109(CM00109 CM00109)
        {
            CM00109Repository rep = new CM00109Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(CM00109);
            return result;
        }
        public KaizenResult DeleteCM00109(CM00109 CM00109)
        {
            CM00109Repository rep = new CM00109Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(CM00109);
            return result;
        }
        #endregion
    }
} 
