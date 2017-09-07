using System.Collections.Generic;
using System.Linq;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
namespace Kaizen.BusinessLogic.CMS
{
    public class CM00232Services
    {
        #region Infrastructure

        private CM00232Repository _CM00232Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00232Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00232Repository = new CM00232Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00232> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("MessageTRXID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CreatedBy", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CreatedDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TrxComment", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TemplateID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00232Repository rep = new CM00232Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00232> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00232", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00232> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00232> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00232Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00232Repository.GetWhereListWithPaging("CM00232", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<CM00233> GetAllCM00233(string Filters, string Searchcritaria,string MessageTRXID,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            if(!string.IsNullOrEmpty(SeachStr))
            {
                SeachStr += "and ";
            }
            SeachStr += " MessageTRXID='" + MessageTRXID.Trim() + "'";
            DataCollection<CM00233> result = null;
            CM00233Repository req = new CM00233Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            result = req.GetWhereListWithPaging("CM00233", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00232> GetAllCM00232List(string Filters, string Searchcritaria,int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00232> result = null;
            CM00232Repository req = new CM00232Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            result = req.GetWhereListWithPaging("CM00232", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00232> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00232> L = null;
            var tasks = _CM00232Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00232> result = tasks;
            return result;
        }
        public DataCollection<CM00232> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00232> result = null;
            var tasks = _CM00232Repository.GetListWithPaging(PageSize, Page, ss => new { ss.MessageTRXID });
            result = tasks;
            return result;
        }
        public IList<CM00232> GetAll()
        {
            var tasks = _CM00232Repository.GetAll();
            IList<CM00232> result = tasks;
            return result;
        }
        public CM00232 GetSingle(string MessageTRXID)
        {
            var tasks = _CM00232Repository.GetSingle(x => x.MessageTRXID == MessageTRXID);
            return tasks;
        }
        public KaizenResult AddCM00232(CM00232 newTask, IList<CM00233> CM00231)
        {
            foreach(CM00233 o in CM00231)
            {
                o.MessageTRXID = newTask.MessageTRXID;
                o.IsSent = false;
            }
            var result = _CM00232Repository.AddKaizenObject(newTask);
            if(result.Status)
            {
                CM00233Repository rep =new CM00233Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                result = rep.AddKaizenObject(CM00231.ToArray());
                UserContext.Screens.Remove(Kaizen.Configuration.Screen.CMS_Uti_SMS);
            }
            string sql = "update CM00233 set MobileNo1 = CM00104.MobileNo1";
            sql += ",MobileNo2 = CM00104.MobileNo2";
            sql += ",MobileNo3 = CM00104.MobileNo3";
            sql += ",MobileNo4 = CM00104.MobileNo4";
            sql += " from CM00203 inner join CM00233 on CM00233.CaseRef = CM00203.CaseRef";
            sql += " inner join CM00104 on CM00104.DebtorID = CM00203.CIFNumber ";
            sql += " where CM00104.AddressCode = '"+ newTask.AddressCode.Trim() + "'";
            sql += " and CM00233.MessageTRXID='" + newTask.MessageTRXID + "'";
            result = _CM00232Repository.ExecuteSqlCommandKaizenObject(sql);
            return result;
        }
        public KaizenResult AddCM00232(CM00232 newTask)
        {
            var result = _CM00232Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00232(IList<CM00232> newTask)
        {
            var result = _CM00232Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00232 newTask)
        {
            var result = _CM00232Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00232> newTask)
        {
            var result = _CM00232Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00232 newTask)
        {
            var result = _CM00232Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00232> newTask)
        {
            var result = _CM00232Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        public string GetNextLetterTransactionNumber()
        {
            // CREATE SEQUENCE CMSPayment_Sequence START WITH 1 INCREMENT BY 1
            string SequenceName = "CMS_SMS_Sequence";
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
                        _CM00232Repository.ExecuteSqlCommand(SQL);
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
                long? SequenceNumber = _CM00232Repository.GetSQLLong(SQL);
                if (SequenceNumber.HasValue)
                {
                    SQL = "insert into Sys00000(UserName,SequenceName,KaizenID) values (";
                    SQL += "'" + UserContext.UserName + "','" + SequenceName + "'," + SequenceNumber.ToString() + ");";
                    _CM00232Repository.ExecuteSqlCommand(SQL);
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
            int templenth = KaizenID.ToString().Length;
            if (Master.GetCMSConfiguration(this.UserContext.CompanyID).SMSLetterlengh.HasValue)
                templenth = Master.GetCMSConfiguration(this.UserContext.CompanyID).SMSLetterlengh.Value - KaizenID.ToString().Length;
            string Str = string.Empty;
            for (byte i = 1; i <= templenth; i++)
            {
                Str = Str + "0";
            }
            if (string.IsNullOrEmpty(Master.GetCMSConfiguration(this.UserContext.CompanyID).SMSLetterPrefix))
                Master.GetCMSConfiguration(this.UserContext.CompanyID).SMSLetterPrefix = "";
            return Master.GetCMSConfiguration(this.UserContext.CompanyID).SMSLetterPrefix.Trim() +  Str + KaizenID.ToString();
        }
    }
}



