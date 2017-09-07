using System;
using System.Collections.Generic;
using Kaizen.GL.Repository;
using System.Linq;
using Kaizen.GL;
using Kaizen.BusinessLogic.Configuration;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00200Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00200Repository _GL00200RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00200Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00200RepositoryDataRepository = new GL00200Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public static bool IsNumeric(object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }
        public DataCollection<GL00200> GetDataListGrid(string Filters, string Searchcritaria,
                 string YearCode, int PERIODID,int SelectdViewID,
                 int PageSize, int Page, string Member, bool IsAscending)
        {
            DataCollection<GL00200> result = null;
            string SeachStr = Filters;
            if (!string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr += " and ";
                if (IsNumeric(Searchcritaria))
                {
                    SeachStr += " TransactionID =" + Searchcritaria.Trim();
                }
                else
                {
                    SeachStr += " GLReference like '%" + Searchcritaria.Trim() + "%'";
                    SeachStr += " and TrxDescription like '%" + Searchcritaria.Trim() + "%'";
                }
            }
            if (string.IsNullOrEmpty(SeachStr))
            {
                if (YearCode != string.Empty && PERIODID != -1)
                    result = _GL00200RepositoryDataRepository.GetListWithPaging(x => x.YearCode == YearCode 
                    && x.PERIODID == PERIODID
                    , PageSize, Page, ss => Member);
                else if (PERIODID != -1)
                    result = _GL00200RepositoryDataRepository.GetListWithPaging(x => x.PERIODID == PERIODID, PageSize, Page, ss => new { ss.TransactionID });
                else
                    result = _GL00200RepositoryDataRepository.GetListWithPaging(x => x.YearCode == YearCode
                , PageSize, Page, ss => Member);

            }
            else
            {
                if (YearCode != string.Empty && PERIODID != -1)
                    SeachStr += " and YearCode =" + YearCode.ToString() + " and PERIODID=" + PERIODID;
                else if (YearCode != string.Empty)
                    SeachStr += " and YearCode =" + YearCode.ToString();
                else if (PERIODID != -1)
                    SeachStr += " and PERIODID=" + PERIODID;

                result = _GL00200RepositoryDataRepository.GetWhereListWithPaging("GL00200", PageSize, Page, SeachStr, Member, IsAscending);
            }
            return result;
        }
        public DataCollection<GL00200> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
            string YearCode, int PERIODID,
            int PageSize, int Page, string Member, bool IsAscending)
        {
            DataCollection<GL00200> result = null;
            string SeachStr = Filters;
            if (!string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr += " and ";
                SeachStr += " GLReference like '%" + Searchcritaria.Trim() + "%'";
                SeachStr += " and TrxDescription like '%" + Searchcritaria.Trim() + "%'";
            }
            if (string.IsNullOrEmpty(SeachStr))
            {
                if (YearCode != string.Empty && PERIODID != -1)
                    result = _GL00200RepositoryDataRepository.GetListWithPaging(x => x.YearCode == YearCode && x.PERIODID == PERIODID
                    , PageSize, Page, ss => new { ss.TransactionID });
                else if (YearCode != string.Empty)
                    result = _GL00200RepositoryDataRepository.GetListWithPaging(x => x.YearCode == YearCode, PageSize, Page, ss => new { ss.TransactionID });
                else if (PERIODID != -1)
                    result = _GL00200RepositoryDataRepository.GetListWithPaging(x => x.PERIODID == PERIODID, PageSize, Page, ss => new { ss.TransactionID });
                else
                    result = _GL00200RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                if (YearCode != string.Empty && PERIODID != -1)
                    SeachStr += " and YearCode =" + YearCode.ToString() + " and PERIODID=" + PERIODID;
                else if (YearCode != string.Empty)
                    SeachStr += " and YearCode =" + YearCode.ToString();
                else if (PERIODID != -1)
                    SeachStr += " and PERIODID=" + PERIODID;

                result = _GL00200RepositoryDataRepository.GetWhereListWithPaging("GL00200", PageSize, Page, SeachStr, Member, IsAscending);
            }
            return result;
        }

        public DataCollection<GL00200> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<GL00200> L = null;
            var tasks = _GL00200RepositoryDataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<GL00200> result = tasks;
            return result;
        }
        public DataCollection<GL00200> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<GL00200> result = null;
            var tasks = _GL00200RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<GL00200> GetAll()
        {
            var tasks = _GL00200RepositoryDataRepository.GetAll();
            IList<GL00200> result = tasks;
            return result;
        }
        public IList<GL00200> GetAllByYearCode(string YearCode)
        {
            var tasks = _GL00200RepositoryDataRepository.GetAll(xx => xx.YearCode == YearCode);
            IList<GL00200> result = tasks;
            return result;
        }

        public DataCollection<GL00200> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<GL00200> L = null;
            var tasks = _GL00200RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<GL00200> result = tasks;
            return result;
        }
        public DataCollection<GL00200> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00200> result = null;
            var tasks = _GL00200RepositoryDataRepository.GetListWithPaging(x => x.GLReference.ToString().Contains(SearchTerm)
                , PageSize, Page, ss => new { ss.TransactionID });
            result = tasks;
            return result;
        }
        public DataCollection<GL00200> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00200> result = null;
            var tasks = _GL00200RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.TransactionID });
            result = tasks;
            return result;
        }
        public GL00200 GetSingle(int TransactionID, int PERIODID)
        {
            var tasks = _GL00200RepositoryDataRepository.GetSingle(x => x.TransactionID == TransactionID && x.PERIODID == PERIODID);
            return tasks;
        }
        public string GetNextGLTransactionByDate(DateTime TransactionDate)
        {
            GL00002Repository rep = new GL00002Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetSingle(x => x.PeriodFrom <= TransactionDate && x.PeriodTo >= TransactionDate);
            if (tasks == null)
                return "";
            return this.GetNextTransactionID(tasks.YearCode);
        }
        public string GetNextTransactionID(string YearCode)
        {
            string SequenceName = "FiscalPeriod_" + YearCode.Trim() + "_Sequence";
            int KaizenID = -1;
            Kaizen.Admin.Repository.Sys00000Repository Repository = new Kaizen.Admin.Repository.Sys00000Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            List<Kaizen.Admin.Sys00000> AllSessions = Repository.GetList(xx => xx.SequenceName.Trim() == SequenceName.Trim()).ToList();
            Kaizen.Admin.Sys00000 temp = AllSessions.Find(xx => xx.UserName.Trim() == UserContext.UserName.Trim());
            if (temp == null)
            {
                string SQL = string.Empty;
                foreach (Kaizen.Admin.Sys00000 row in AllSessions)
                {
                    Kaizen.Configuration.KaizenSession oKaizenSession = UserServices.GetUserSession(row.UserName, UserContext.CompanyID);
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
                    SQL += "update GL00002 set NextGL =" + KaizenID.ToString() + " where YearCode='"+ YearCode.Trim() + "'";
                    Repository.ExecuteSqlCommand(SQL);
                   
                }
                else
                    return "";
            }
            if (UserContext.Screens == null)
                UserContext.Screens = new List<Kaizen.Configuration.Screen>();
            UserContext.Screens.Add(Kaizen.Configuration.Screen.GLTransaction);
            return KaizenID.ToString(); 
        }
        public KaizenResult PostTransaction(int TransactionID, int PERIODID)
        {
            KaizenResult oresult = new KaizenResult();
            string SQL = "INSERT INTO dbo.GL00700(TransactionID, PERIODID, YearCode, SourceID, GLReference, TransactionDate, CurrencyCode, DecimalDigit, ExchangeTableID, ExchangeRateID, ExchangeRate, IsMultiply, DOCAMT, BatchID, TrxDescription, UserNameTrx, EntryDateTrx, UserNamePost, EntryDatePost)";
            SQL += "select TransactionID, PERIODID, YearCode, SourceID, GLReference, TransactionDate, CurrencyCode, DecimalDigit, ExchangeTableID, ExchangeRateID, ExchangeRate, IsMultiply, DOCAMT, BatchID, TrxDescription, UserName, EntryDate,";
            SQL += "'" +this.UserContext.UserName.Trim() + "',getdate() FROM dbo.GL00200 where TransactionID="+ TransactionID.ToString() + " and PERIODID=" + PERIODID.ToString();
            if(_GL00200RepositoryDataRepository.ExecuteSqlCommand(SQL) > 0)
            {
                oresult.Status = true;
                oresult.Massage = "Transaction has been Posted Successfully";
                return oresult;
            }
            oresult.Status = false;
            oresult.Massage = "Posting Fail";
            return oresult;
        }
        public KaizenResult AddGL00200(GL00200 newTask)
        {
            newTask.UserName = this.UserContext.UserName;
            newTask.EntryDate = DateTime.Now; 
            GL00003Services oservice = new GL00003Services(this.UserContext);
            GL00003 oGL00003 = oservice.GetSinglePeriod(newTask.TransactionDate, newTask.YearCode);
            if (oGL00003 == null)
            {
                KaizenResult oresult = new KaizenResult();
                oresult.Status = false;
                oresult.Massage = "CLosed Period";
                return oresult;
            }
            if (oGL00003.IsClosed)
            {
                KaizenResult oresult = new KaizenResult();
                oresult.Status = false;
                oresult.Massage = "CLosed Period";
                return oresult;
            }
            newTask.PERIODID = oGL00003.PERIODID;
            var result = _GL00200RepositoryDataRepository.AddKaizenObject(newTask);
            result.Description = newTask.PERIODID.ToString();
            return result;
        }
        public KaizenResult AddGL00200(IList<GL00200> newTask)
        {
            var result = _GL00200RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            //if (result.Status)
            //{
            //    Configuration.CompanyacessServices com = new Configuration.CompanyacessServices(this.UserContext);
            //    GL00003Services oservice = new GL00003Services(this.UserContext);
            //    foreach (var item in newTask)
            //    {
            //        com.DeleteNexttNextGLID(oservice.GetSingle(item.PERIODID).YearCode);
            //    }
            //}
            return result;
        }
         
        public KaizenResult Update(GL00200 newTask)
        {
            var result = _GL00200RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00200> newTask)
        {
            var result = _GL00200RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(int TransactionID, int PERIODID)
        {
            KaizenResult oresult = new KaizenResult();
            string SQL = "INSERT INTO dbo.GL00600(TransactionID, PERIODID, YearCode, SourceID, GLReference, TransactionDate, CurrencyCode, DecimalDigit, ExchangeTableID, ExchangeRateID, ExchangeRate, IsMultiply, DOCAMT, BatchID, TrxDescription, UserNameTrx, EntryDateTrx, UserNameDelete, EntryDateDelete)";
            SQL += "select TransactionID, PERIODID, YearCode, SourceID, GLReference, TransactionDate, CurrencyCode, DecimalDigit, ExchangeTableID, ExchangeRateID, ExchangeRate, IsMultiply, DOCAMT, BatchID, TrxDescription, UserName, EntryDate,";
            SQL += "'" + this.UserContext.UserName.Trim() + "',getdate() FROM dbo.GL00200 where TransactionID=" + TransactionID.ToString() + " and PERIODID=" + PERIODID.ToString();
            if (_GL00200RepositoryDataRepository.ExecuteSqlCommand(SQL) > 0)
            {
                oresult.Status = true;
                oresult.Massage = "Transaction has been Posted Successfully";
                return oresult;
            }
            oresult.Status = false;
            oresult.Massage = "Posting Fail";
            return oresult;
        }
        public KaizenResult Delete(GL00200 newTask)
        {
            var result = _GL00200RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00200> newTask)
        {
            var result = _GL00200RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _GL00200RepositoryDataRepository.ExecuteSqlCommand(myQuery);
            return result;
        }
    }
}
