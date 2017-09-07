using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00200Services
    {
        #region Infrastructure

        private IV00200Repository _IV00200Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00200Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00200Repository = new IV00200Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<IV00200> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00200> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00200Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00200Repository.GetWhereListWithPaging("IV00200", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00200> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TransactionID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TransactionTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TransactionDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BatchID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BatchSourceID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SiteID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BinTrack", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ReasonID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TransactionNote", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00200> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00200Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00200Repository.GetWhereListWithPaging("IV00200", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00200> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<IV00200> L = null;
            var tasks = _IV00200Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<IV00200> result = tasks;
            return result;
        }
        public DataCollection<IV00200> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<IV00200> result = null;
            var tasks = _IV00200Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }


        public DataCollection<IV00200> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<IV00200> L = null;
            var tasks = _IV00200Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<IV00200> result = tasks;
            return result;
        }

        public DataCollection<IV00200> GetListWithPaging(string SearchTerm, string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<IV00200> result = null;
            var tasks = _IV00200Repository.GetListWithPaging(x => x.BatchID.ToString().Contains(SearchTerm)
                , PageSize, Page, ss => ss.BatchID, null);
            result = tasks;
            return result;
        }
        public DataCollection<IV00200> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<IV00200> result = null;
            var tasks = _IV00200Repository.GetListWithPaging(PageSize, Page, ss => new { ss.BatchID });
            result = tasks;
            return result;
        }

        public IV00200 GetSingle(string TransactionID, int TransactionTypeID)
        {
            var tasks = _IV00200Repository.GetSingle(x => x.TransactionID == TransactionID && x.TransactionTypeID == TransactionTypeID);
            return tasks;
        }

        public IList<IV00200> GetAll()
        {
            var tasks = _IV00200Repository.GetAll();
            IList<IV00200> result = tasks;
            return result;
        }

        public KaizenResult AddIV00200(IV00200 newTask)
        {
            var result = _IV00200Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00200(IList<IV00200> newTask)
        {
            var result = _IV00200Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00200 newTask)
        {
            IV00206Repository _IV00206Repository = new IV00206Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            ICollection<IV00206> tempIV00206 = newTask.IV00206;

            newTask.IV00206 = null;
            newTask.EntryDate = DateTime.Now;
            var result = _IV00200Repository.UpdateKaizenObject(newTask);
            if (result.Status)
            {
                _IV00206Repository.ExecuteSqlCommand("delete IV00206 where TransactionID='" + newTask.TransactionID + "' AND TransactionTypeID='"+ newTask.TransactionTypeID + "'");

                foreach (var item in tempIV00206)
                {
                    item.TransactionID = newTask.TransactionID;
                    item.TransactionTypeID = newTask.TransactionTypeID; 
                    item.ACTNUMBR = RemoveSpecialCharacters(item.ACTNUMBR.Trim());
                }
                _IV00206Repository.AddKaizenObject(tempIV00206.ToArray());
            }
            return result;
        }
        public KaizenResult Update(IList<IV00200> newTask)
        {
            var result = _IV00200Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00200 newTask)
        {
            var result = _IV00200Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00200> newTask)
        {
            var result = _IV00200Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }


        public IList<IV00017> GetAllIV00017()
        {
            IV00017Repository r = new IV00017Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<IV00017> result = tasks;
            return result;
        }
        public string GetNextTransactionID(int TransactionTypeID)
        {
            string SequenceName = string.Empty;
            switch (TransactionTypeID)
            {
                case 1:
                    SequenceName = "IV_Adjust_Increase_Sequence";
                    break;
                case 2:
                    SequenceName = "IV_Adjust_Decrease_Sequence";
                    break;
                case 3:
                    SequenceName = "IV_IncreaseVariance_Sequence";
                    break;
                case 4:
                    SequenceName = "IV_DecreaseVariance_Sequence";
                    break;
            }
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

                        this.ExecuteSqlCommand(SQL);
                        KaizenID = row.KaizenID;
                        break;
                    }
                    bool notUse = true;
                    foreach (Kaizen.Configuration.Screen scrn in oKaizenSession.Screens)
                    {
                        if (scrn == Kaizen.Configuration.Screen.GLTransaction)
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
                    switch (TransactionTypeID)
                    {
                        case 1:
                            SQL += "update IV000014 set LastNumberIncreaseAdjust =" + KaizenID.ToString();
                            break;
                        case 2:
                            SQL += "update IV000014 set LastNumberDecreaseAdjust =" + KaizenID.ToString();
                            break;
                        case 3:
                            SQL += "update IV000014 set LastNumberIncreaseVariance =" + KaizenID.ToString();
                            break;
                        case 4:
                            SQL += "update IV000014 set LastNumberDecreaseVariance =" + KaizenID.ToString();
                            break;
                    }
                    Repository.ExecuteSqlCommand(SQL);
                }
                else
                    return "";
            }
            if (UserContext.Screens == null)
                UserContext.Screens = new List<Kaizen.Configuration.Screen>();
            UserContext.Screens.Add(Kaizen.Configuration.Screen.IV_Adjust);

            IV000014Repository reptemp = new IV000014Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IList<IV000014> t = reptemp.GetAll();
            IV000014 obj = t.FirstOrDefault();
            int templenth = 0;
            string NextNumberPrefix = string.Empty;
            switch (TransactionTypeID)
            {
                case 1:
                    templenth = obj.LenthIncreaseAdjust - KaizenID.ToString().Length;
                    NextNumberPrefix = obj.PrefixIncreaseAdjust;
                    break;
                case 2:
                    templenth = obj.LenthDecreaseAdjust - KaizenID.ToString().Length;
                    NextNumberPrefix = obj.PrefixDecreaseAdjust;
                    break;
                case 3:
                    templenth = obj.LenthIncreaseVariance - KaizenID.ToString().Length;
                    NextNumberPrefix = obj.PrefixIncreaseVariance;
                    break;
                case 4:
                    templenth = obj.LenthDecreaseVariance - KaizenID.ToString().Length;
                    NextNumberPrefix = obj.PrefixDecreaseVariance;
                    break;
            }

            string Str = string.Empty;
            for (byte i = 1; i <= templenth; i++)
            {
                Str = Str + "0";
            }
            return NextNumberPrefix + Str + KaizenID.ToString();
        }
        public KaizenResult PostTransaction(int TransactionTypeID,string TransactionID)
        {
            IV00200 temp=   this.GetSingle(TransactionID, TransactionTypeID);
            KaizenResult oresult = new KaizenResult();
            GL.GL00003Services srv = new GL.GL00003Services(UserContext);
            Kaizen.GL.GL00003 Period = srv.GetSinglePeriod(temp.TransactionDate);
            if (Period == null)
            {
                oresult.Status = false;
                oresult.Massage = "Out Of Posting Date";
                return oresult;
            }
            string SQL = "insert into IV20200(TransactionID,TransactionTypeID,YearCode,PERIODID,DOCAMNT,TransactionDate,SiteID,ReasonID,TransactionNote,EntryDate)";
            SQL += "select TransactionID ,TransactionTypeID ,'" + Period.YearCode.Trim() + "'," +
                Period.PERIODID.ToString() + ", DOCAMNT,TransactionDate,SiteID,ReasonID,TransactionNote,EntryDate from IV00200";
            SQL += " where TransactionID='" + TransactionID.Trim() + "' and TransactionTypeID="+ TransactionTypeID.ToString() + ";";
            //SQL += "delete IV00200";
            //SQL += " where TransactionID='" + TransactionID.Trim() + "' and TransactionTypeID=" + TransactionTypeID.ToString() + ";";
            if (this.ExecuteSqlCommand(SQL) > 0)
            {
                oresult.Status = true;
                oresult.Massage = "Transaction has been Posted Successfully";
                return oresult;
            }
            oresult.Status = false;
            oresult.Massage = "Posting Fail";
            return oresult;
        }
        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _IV00200Repository.ExecuteSqlCommand(myQuery);
            return result;
        }
        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

    }
}
