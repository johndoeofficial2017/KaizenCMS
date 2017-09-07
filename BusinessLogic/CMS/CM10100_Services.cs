using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM10100Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM10100Repository _CM10100Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM10100Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM10100Repository = new CM10100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM10100> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("BatchID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClientID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClientName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BookingDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CreatedDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CreatedBy", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FileName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM10100Repository rep = new CM10100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM10100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM10100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM10100> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM10100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM10100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM10100Repository.GetWhereListWithPaging("CM10100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM10100> GetAllViewBYSQLQuery(string Filters, string ClientID,
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
                    SeachStr += Help.GetFilter("BatchID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClientID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClientName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BookingDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CreatedDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CreatedBy", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FileName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM10100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM10100Repository.GetListWithPaging(xx => xx.ClientID == ClientID.Trim(), PageSize, Page, ss => Member);
            else
            {
                SeachStr += " and ClientID='" + ClientID.Trim() + "'";
                result = _CM10100Repository.GetWhereListWithPaging("CM10100", PageSize, Page, SeachStr, Member, IsAscending);
            }
            return result;
        }

        public DataCollection<CM10100> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM10100Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM10100> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM10100Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM10100> result = tasks;
                return result;
            }

        }
        public DataCollection<CM10100> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM10100> result = null;
            var tasks = _CM10100Repository.GetListWithPaging(PageSize, Page, ss => new { ss.BatchID });
            result = tasks;
            return result;
        }
        public IList<CM10100> GetAll()
        {
            var tasks = _CM10100Repository.GetAll();
            IList<CM10100> result = tasks;
            return result;
        }

        public CM10100 GetSingle(string BatchID)
        {
            var tasks = _CM10100Repository.GetSingle(x => x.BatchID == BatchID);
            return tasks;
        }

        public KaizenResult AddCM10100(CM10100 newTask)
        {
            newTask.CreatedBy = UserContext.UserName;
            newTask.CreatedDate = DateTime.Now;
            var result = _CM10100Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM10100(IList<CM10100> newTask)
        {
            var result = _CM10100Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM10100 newTask)
        {
            var result = _CM10100Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM10100> newTask)
        {
            var result = _CM10100Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM10100 newTask)
        {
            var result = _CM10100Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM10100> newTask)
        {
            var result = _CM10100Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        public string GetNextBatchID()
        {
            Configuration.CM00000Services srv = new Configuration.CM00000Services(this.UserContext);
            Kaizen.Configuration.CM00000 classtemp = srv.GetAll().FirstOrDefault();
            string SequenceName = "CMS_BatchUpload_Sequence ";
            int KaizenID = -1;
            Kaizen.Admin.Repository.Sys00000Repository Repository = new Kaizen.Admin.Repository.Sys00000Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            List<Kaizen.Admin.Sys00000> AllSessions = Repository.GetList(xx => xx.SequenceName.Trim() == SequenceName.Trim()).ToList();
            Kaizen.Admin.Sys00000 temp = AllSessions.Find(xx => xx.UserName.Trim() == UserContext.UserName.Trim());
            //ExceptionUtility.LogData("CMS_BatchUpload_Sequence");
            //ExceptionUtility.LogData(AllSessions.Count.ToString());
            //foreach (Kaizen.Admin.Sys00000 row in AllSessions)
            //{
            //    ExceptionUtility.LogData(row.UserName.Trim());
            //}
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
                        _CM10100Repository.ExecuteSqlCommand(SQL);
                        KaizenID = row.KaizenID;
                        ExceptionUtility.LogData(SQL);
                        break;
                    }
                    bool notUse = true;
                    if(oKaizenSession.Screens != null)
                    {
                        foreach (Kaizen.Configuration.Screen scrn in oKaizenSession.Screens)
                        {
                            if (scrn == Kaizen.Configuration.Screen.CMS_TRX_CaseUploading)
                            {
                                notUse = false;
                                break;
                            }
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
                    ExceptionUtility.LogData(SQL);
                    KaizenID = (int)SequenceNumber.Value;
                    SQL = "insert into Sys00000(UserName,SequenceName,KaizenID) values (";
                    SQL += "'" + UserContext.UserName + "','" + SequenceName + "'," + SequenceNumber.ToString() + ");";
                    SQL += "update kaizen.dbo.CM00000 set BatchLastID =" + KaizenID.ToString();
                    Repository.ExecuteSqlCommand(SQL);
                }
                else
                    return "";
            }
            if (UserContext.Screens == null)
                UserContext.Screens = new List<Kaizen.Configuration.Screen>();
            Kaizen.Configuration.Screen tt = UserContext.Screens.Find(ww => ww == Kaizen.Configuration.Screen.CMS_TRX_CaseUploading);
            if (tt != Kaizen.Configuration.Screen.CMS_TRX_CaseUploading)
                UserContext.Screens.Add(Kaizen.Configuration.Screen.CMS_TRX_CaseUploading);

            int templenth = classtemp.BatchPrefixlengh - KaizenID.ToString().Length;
            string Str = string.Empty;
            for (byte i = 1; i <= templenth; i++)
            {
                Str = Str + "0";
            }
            string DOPrefix = string.Empty;
            if (!string.IsNullOrEmpty(classtemp.BatchPrefixValue))
                DOPrefix = classtemp.BatchPrefixValue.Trim();
            return DOPrefix + Str + KaizenID.ToString();
        }
    }
}
