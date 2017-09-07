using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00220Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00220Repository _CM00220Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00220Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00220Repository = new CM00220Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00220> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TrxID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AgentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AgentIDTO", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("StartDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EndDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TrxComments", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00220Repository rep = new CM00220Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00220> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00220", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00220> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00220> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00220Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00220Repository.GetWhereListWithPaging("CM00220", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00220> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM00220Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00220> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM00220Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00220> result = tasks;
                return result;
            }

        }
        public DataCollection<CM00220> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00220> result = null;
            var tasks = _CM00220Repository.GetListWithPaging(PageSize, Page, ss => new { ss.TrxID });
            result = tasks;
            return result;
        }
        public IList<CM00220> GetAll()
        {
            var tasks = _CM00220Repository.GetAll();
            IList<CM00220> result = tasks;
            return result;
        }

        public CM00220 GetSingle(string TrxID)
        {
            var tasks = _CM00220Repository.GetSingle(x => x.TrxID == TrxID);
            return tasks;
        }

        public KaizenResult AddCM00220(CM00220 newTask)
        {
            KaizenResult result = _CM00220Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00220(IList<CM00220> newTask)
        {
            KaizenResult result = _CM00220Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(CM00220 newTask)
        {
            KaizenResult result = _CM00220Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(string TrxID)
        {
            KaizenResult result = _CM00220Repository.RemoveKaizenObject(xx => xx.TrxID == TrxID);
            return result;
        }
        public KaizenResult Delete(CM00220 newTask)
        {
            KaizenResult result = _CM00220Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public string GetNextTransactionID()
        {
            // CREATE SEQUENCE CMS_AgentReplacement_Sequence START WITH 1 INCREMENT BY 1
            string SequenceName = "CMS_AgentReplacement_Sequence";
            int KaizenID = 0;
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
                        _CM00220Repository.ExecuteSqlCommand(SQL);
                        KaizenID = row.KaizenID;
                        break;
                    }
                    bool notUse = true;
                    foreach (Kaizen.Configuration.Screen scrn in oKaizenSession.Screens)
                    {
                        if (scrn == Kaizen.Configuration.Screen.CMS_Uti_SMS)
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
            if (KaizenID == 0)
            {
                string SQL = "SELECT NEXT VALUE FOR " + SequenceName;
                long? SequenceNumber = _CM00220Repository.GetSQLLong(SQL);
                if (SequenceNumber.HasValue)
                {
                    SQL = "insert into Sys00000(UserName,SequenceName,KaizenID) values (";
                    SQL += "'" + UserContext.UserName + "','" + SequenceName + "'," + SequenceNumber.ToString() + ");";
                    _CM00220Repository.ExecuteSqlCommand(SQL);
                    KaizenID = (int)SequenceNumber.Value;
                }
                else
                    return "";
            }
            if (UserContext.Screens == null)
                UserContext.Screens = new List<Kaizen.Configuration.Screen>();

            UserContext.Screens.Add(Kaizen.Configuration.Screen.CMS_Uti_SMS);
            //CM00000Repository reptemp = new CM00000Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            //IList<CM00000> obj = reptemp.GetAll();
            //CM00000 tempp = obj.FirstOrDefault();
            //if (Master.CMSConfiguration == null)
            //{
            //    CM00000Services service = new CM00000Services(UserContext);
            //    Master.CMSConfiguration = service.GetAll().FirstOrDefault();
            //}
            //CM00000 tempp = Master.CMSConfiguration;
            int templenth = 5;// KaizenID.ToString().Length;
            if (Master.GetCMSConfiguration(this.UserContext.CompanyID).SMSLetterlengh.HasValue)
                templenth = Master.GetCMSConfiguration(this.UserContext.CompanyID).SMSLetterlengh.Value - KaizenID.ToString().Length;
            string Str = string.Empty;
            for (byte i = 1; i <= templenth; i++)
            {
                Str = Str + "0";
            }
            if (string.IsNullOrEmpty(Master.GetCMSConfiguration(this.UserContext.CompanyID).SMSLetterPrefix))
                Master.GetCMSConfiguration(this.UserContext.CompanyID).SMSLetterPrefix = "";
            return  Str + KaizenID.ToString();
        }
    }
}
