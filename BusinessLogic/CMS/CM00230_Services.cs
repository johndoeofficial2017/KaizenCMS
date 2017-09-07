using System.Collections.Generic;
using System.Linq;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
namespace Kaizen.BusinessLogic.CMS
{
    public class CM00230Services
    {
        #region Infrastructure

        private CM00230Repository _CM00230Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00230Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00230Repository = new CM00230Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00230> GetAllViewBYSQLQuery(string Filters,
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

            CM00230Repository rep = new CM00230Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00230> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00230", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00230> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00230> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00230Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00230Repository.GetWhereListWithPaging("CM00230", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<CM00231> GetAllCM00231(string Filters, string Searchcritaria,string MessageTRXID,
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
            DataCollection<CM00231> result = null;
            CM00231Repository req = new CM00231Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            result = req.GetWhereListWithPaging("CM00231", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00234> GetAllCM00234List(string Filters, string Searchcritaria,int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00234> result = null;
            CM00234Repository req = new CM00234Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            result = req.GetWhereListWithPaging("CM00234", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00230> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00230> L = null;
            var tasks = _CM00230Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00230> result = tasks;
            return result;
        }
        public DataCollection<CM00230> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00230> result = null;
            var tasks = _CM00230Repository.GetListWithPaging(PageSize, Page, ss => new { ss.MessageTRXID });
            result = tasks;
            return result;
        }
        public IList<CM00230> GetAll()
        {
            var tasks = _CM00230Repository.GetAll();
            IList<CM00230> result = tasks;
            return result;
        }
        public CM00230 GetSingle(string MessageTRXID)
        {
            var tasks = _CM00230Repository.GetSingle(x => x.MessageTRXID == MessageTRXID);
            return tasks;
        }
        public KaizenResult AddCM00230(CM00230 newTask, IList<CM00231> CM00231)
        {
            foreach(CM00231 o in CM00231)
            {
                o.MessageTRXID = newTask.MessageTRXID;
            }
            var result = _CM00230Repository.AddKaizenObject(newTask);
            if(result.Status)
            {
                CM00231Repository rep =new CM00231Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                result = rep.AddKaizenObject(CM00231.ToArray());
            }
            return result;
        }
        public KaizenResult AddCM00230(CM00230 newTask)
        {
            var result = _CM00230Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00230(IList<CM00230> newTask)
        {
            var result = _CM00230Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00230 newTask)
        {
            var result = _CM00230Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00230> newTask)
        {
            var result = _CM00230Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00230 newTask)
        {
            var result = _CM00230Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00230> newTask)
        {
            var result = _CM00230Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        public string GetNextLetterTransactionNumber()
        {
            // CREATE SEQUENCE CMSPayment_Sequence START WITH 1 INCREMENT BY 1
            string SequenceName = "CMSLetter_Sequence";
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
                        _CM00230Repository.ExecuteSqlCommand(SQL);
                        KaizenID = row.KaizenID;
                        break;
                    }
                    bool notUse = true;
                    foreach (Kaizen.Configuration.Screen scrn in oKaizenSession.Screens)
                    {
                        if (scrn == Kaizen.Configuration.Screen.CMS_Payment)
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
                long? SequenceNumber = _CM00230Repository.GetSQLLong(SQL);
                if (SequenceNumber.HasValue)
                {
                    SQL = "insert into Sys00000(UserName,SequenceName,KaizenID) values (";
                    SQL += "'" + UserContext.UserName + "','" + SequenceName + "'," + SequenceNumber.ToString() + ");";
                    _CM00230Repository.ExecuteSqlCommand(SQL);
                    KaizenID = (int)SequenceNumber.Value;
                }
                else
                    return "";
            }
            if (UserContext.Screens == null)
                UserContext.Screens = new List<Kaizen.Configuration.Screen>();

            UserContext.Screens.Add(Kaizen.Configuration.Screen.CMS_Uti_Letter);
            //CM00000Repository reptemp = new CM00000Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            //IList<CM00000> obj = reptemp.GetAll();
            //CM00000 tempp = obj.FirstOrDefault();
            //if (Master.CMSConfiguration == null)
            //{
            //    CM00000Services service = new CM00000Services(UserContext);
            //    Master.CMSConfiguration = service.GetAll().FirstOrDefault();
            //}
            //CM00000 tempp = Master.CMSConfiguration;
            int templenth = Master.GetCMSConfiguration(this.UserContext.CompanyID).CasesLetterlengh.Value - KaizenID.ToString().Length;
            string Str = string.Empty;
            for (byte i = 1; i <= templenth; i++)
            {
                Str = Str + "0";
            }
            return Master.GetCMSConfiguration(this.UserContext.CompanyID).CasesLetterPrefix.Trim() +  Str + KaizenID.ToString();
        }
    }
}



