using System.Collections.Generic;
using System.Linq;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
namespace Kaizen.BusinessLogic.CMS
{
    public class CM00234Services
    {
        #region Infrastructure

        private CM00234Repository _CM00234Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00234Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00234Repository = new CM00234Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00234> GetAllViewBYSQLQuery(string Filters,
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

            CM00234Repository rep = new CM00234Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00234> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00234", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00234> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00234> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00234Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00234Repository.GetWhereListWithPaging("CM00234", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<CM00235> GetAllCM00235(string Filters, string Searchcritaria,string MessageTRXID,
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
            DataCollection<CM00235> result = null;
            CM00235Repository req = new CM00235Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            result = req.GetWhereListWithPaging("CM00235", PageSize, Page, SeachStr, Member, IsAscending);
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

        public DataCollection<CM00234> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00234> L = null;
            var tasks = _CM00234Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00234> result = tasks;
            return result;
        }
        public DataCollection<CM00234> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00234> result = null;
            var tasks = _CM00234Repository.GetListWithPaging(PageSize, Page, ss => new { ss.MessageTRXID });
            result = tasks;
            return result;
        }
        public IList<CM00234> GetAll()
        {
            var tasks = _CM00234Repository.GetAll();
            IList<CM00234> result = tasks;
            return result;
        }
        public CM00234 GetSingle(string MessageTRXID)
        {
            var tasks = _CM00234Repository.GetSingle(x => x.MessageTRXID == MessageTRXID);
            return tasks;
        }
        public KaizenResult AddCM00234(CM00234 newTask, IList<CM00235> CM00235)
        {
            foreach(CM00235 o in CM00235)
            {
                o.MessageTRXID = newTask.MessageTRXID;
            }
            var result = _CM00234Repository.AddKaizenObject(newTask);
            if(result.Status)
            {
                CM00235Repository rep =new CM00235Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                result = rep.AddKaizenObject(CM00235.ToArray());
                UserContext.Screens.Remove(Kaizen.Configuration.Screen.CMS_Uti_Letter);
            }
            return result;
        }
        public KaizenResult AddCM00234(CM00234 newTask)
        {
            var result = _CM00234Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00234(IList<CM00234> newTask)
        {
            var result = _CM00234Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00234 newTask)
        {
            var result = _CM00234Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00234> newTask)
        {
            var result = _CM00234Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00234 newTask)
        {
            var result = _CM00234Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00234> newTask)
        {
            var result = _CM00234Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        public string GetNextLetterTransactionNumber()
        {
            // CREATE SEQUENCE CMSPayment_Sequence START WITH 1 INCREMENT BY 1
            string SequenceName = "CMS_Letter_Sequence";
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
                        _CM00234Repository.ExecuteSqlCommand(SQL);
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
                long? SequenceNumber = _CM00234Repository.GetSQLLong(SQL);
                if (SequenceNumber.HasValue)
                {
                    SQL = "insert into Sys00000(UserName,SequenceName,KaizenID) values (";
                    SQL += "'" + UserContext.UserName + "','" + SequenceName + "'," + SequenceNumber.ToString() + ");";
                    _CM00234Repository.ExecuteSqlCommand(SQL);
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
            //    Configuration.CM00000Services service = new Configuration.CM00000Services(UserContext);
            //    Master.CMSConfiguration = service.GetAll().FirstOrDefault();
            //}
            Kaizen.Configuration.CM00000 tempp = Master.GetCMSConfiguration(this.UserContext.CompanyID);
            int templenth = tempp.CasesLetterlengh.Value - KaizenID.ToString().Length;
            string Str = string.Empty;
            for (byte i = 1; i <= templenth; i++)
            {
                Str = Str + "0";
            }
            return tempp.CasesLetterPrefix.Trim() +  Str + KaizenID.ToString();
        }
    }
}



