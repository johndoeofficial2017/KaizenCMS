using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;
using Kaizen.BusinessLogic.Configuration;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00103Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00103Repository _GL00103RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00103Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00103RepositoryDataRepository = new GL00103Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00103> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00103> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00103RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00103RepositoryDataRepository.GetWhereListWithPaging("GL00103", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<GL00103> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CheckbookCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BankIBN", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BankCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BankAccount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ACTNUMBR", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CheckbookName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            GL00103Repository rep = new GL00103Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<GL00103> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("GL00103", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00103> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string CurrencyCode,
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
                    SeachStr += Help.GetFilter("CheckbookCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BankIBN", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BankCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BankAccount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ACTNUMBR", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CheckbookName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            GL00103Repository rep = new GL00103Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<GL00103> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = rep.GetListWithPaging(ss => ss.CurrencyCode.Trim() == CurrencyCode.Trim(),
                    PageSize, Page, ss => Member);
            }
            else
            {
                SeachStr += " and CurrencyCode='" + CurrencyCode.Trim() + "'";
                result = rep.GetWhereListWithPaging("GL00103", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }


        public DataCollection<GL00103> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<GL00103> L = null;
            var tasks = _GL00103RepositoryDataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<GL00103> result = tasks;
            return result;
        }
        public DataCollection<GL00103> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<GL00103> result = null;
            var tasks = _GL00103RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public DataCollection<GL00103> GetAllBYSQLQueryByCurrencyCode(string sqlenquiry, int PageSize, int Page, string Member, string CurrencyCode, int SortDirection)
        {
            IList<GL00103> L = null;
            var tasks = _GL00103RepositoryDataRepository.GetWhereListWithPaging(xx => xx.CurrencyCode == CurrencyCode, PageSize, Page, ss => Member);
            DataCollection<GL00103> result = tasks;
            return result;
        }
        public DataCollection<GL00103> GetListWithPagingByCurrencyCode(int PageSize, int Page, string Member, string CurrencyCode, int SortDirection)
        {
            DataCollection<GL00103> result = null;
            var tasks = _GL00103RepositoryDataRepository.GetListWithPaging(xx => xx.CurrencyCode == CurrencyCode, PageSize, Page, ss => Member);
            result = tasks;
            return result;
        }
        public IList<GL00103> GetAll()
        {
            try
            {
                IList<GL00103> L = null;
                try
                {
                    var tasks = _GL00103RepositoryDataRepository.GetAll();
                    IList<GL00103> result = tasks;
                    return result;
                }
                catch (Exception ex)
                {
                }
                return null;
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
        public DataCollection<GL00103> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<GL00103> L = null;
                var tasks = _GL00103RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<GL00103> result = tasks;
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
        public DataCollection<GL00103> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {

            DataCollection<GL00103> result = null;
            var tasks = _GL00103RepositoryDataRepository.GetListWithPaging(x => x.CheckbookCode.Contains(SearchTerm) ||
                x.CheckbookName.Contains(SearchTerm)
                , PageSize, Page, ss => new { ss.CheckbookCode });
            result = tasks;
            return result;
        }
        public DataCollection<GL00103> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00103> result = null;
            var tasks = _GL00103RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.CheckbookCode });
            result = tasks;
            return result;
        }
        public GL00103 GetSingle(string CheckbookCode)
        {
            var tasks = _GL00103RepositoryDataRepository.GetSingle(x => x.CheckbookCode == CheckbookCode);
            return tasks;
        }
        public GL00103 GetSingle(string CheckbookCode,string CurrencyCode)
        {
            var tasks = _GL00103RepositoryDataRepository.GetSingle(x => x.CheckbookCode == CheckbookCode && x.CurrencyCode == CurrencyCode);
            return tasks;
        }
        public KaizenResult AddGL00103(GL00103 newTask)
        {
            var result = _GL00103RepositoryDataRepository.AddKaizenObject(newTask);
            if(result.Status)
            {
                if (newTask.IsOneSerialNumberSale)
                {
                    this.ExecuteSqlCommand("RCTDOC_" + newTask.CheckbookCode.Trim() + "_" + newTask.CurrencyCode.Trim() + "_Sequence");
                }
                else
                {
                    string SequenceName = "RCTDOC_Cash_" + newTask.CheckbookCode.Trim() + "_" + newTask.CurrencyCode.Trim() + "_Sequence";
                    this.ExecuteSqlCommand(SequenceName);
                    SequenceName = "RCTDOC_Credit_" + newTask.CheckbookCode.Trim() + "_" + newTask.CurrencyCode.Trim() + "_Sequence";
                    this.ExecuteSqlCommand(SequenceName);
                    SequenceName = "RCTDOC_Check_" + newTask.CheckbookCode.Trim() + "_" + newTask.CurrencyCode.Trim() + "_Sequence";
                    this.ExecuteSqlCommand(SequenceName);
                }
            }
            return result;
        }
        public KaizenResult Update(GL00103 UpdateGL00103)
        {
            var result = _GL00103RepositoryDataRepository.UpdateKaizenObject(UpdateGL00103);
            return result;
        }
        public KaizenResult Delete(GL00103 newTask)
        {
            var result = _GL00103RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public string GetNextBookPaymentID(string CheckbookCode, int PaymentMethodID)
        {
            //CREATE SEQUENCE RCTDOC_Clien_BHD_Sequence START WITH 1 INCREMENT BY 1
            GL00103 obj = this.GetSingle(CheckbookCode);
            string SequenceName = string.Empty;
            int KaizenID = 0;
            if(obj == null)
            {
                return "0";
            }
            if (obj.IsOneSerialNumberSale)
                SequenceName = "RCTDOC_"+ obj.CheckbookCode.Trim() + "_" + obj.CurrencyCode.Trim() + "_Sequence";
            else
            {
                switch (PaymentMethodID)
                {
                    case 1:
                        SequenceName = "RCTDOC_Cash_" + obj.CheckbookCode.Trim() + "_" + obj.CurrencyCode.Trim() + "_Sequence";
                        break;
                    case 2:
                        SequenceName = "RCTDOC_Credit_" + obj.CheckbookCode.Trim() + "_" + obj.CurrencyCode.Trim() + "_Sequence";
                        break;
                    case 3:
                        SequenceName = "RCTDOC_Check_" + obj.CheckbookCode.Trim() + "_" + obj.CurrencyCode.Trim() + "_Sequence";
                        break;
                }
            }
            Kaizen.Admin.Repository.Sys00000Repository Repository = new Kaizen.Admin.Repository.Sys00000Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            List<Kaizen.Admin.Sys00000> AllSessions = Repository.GetList(xx => xx.SequenceName.Trim() == SequenceName.Trim()).ToList();
            Kaizen.Admin.Sys00000 temp = AllSessions.Find(xx => xx.UserName.Trim() == UserContext.UserName.Trim());
            if (temp == null)
            {
                #region 
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
                #endregion 
            }
            else
                KaizenID = temp.KaizenID;

            if (KaizenID == 0)
            {
                string SQL = "SELECT NEXT VALUE FOR " + SequenceName;
                long? SequenceNumber = _GL00103RepositoryDataRepository.GetSQLLong(SQL);
                if (SequenceNumber.HasValue)
                {
                    SQL = "insert into Sys00000(UserName,SequenceName,KaizenID) values (";
                    SQL += "'" + UserContext.UserName + "','" + SequenceName + "'," + SequenceNumber.ToString() + ");";
                    _GL00103RepositoryDataRepository.ExecuteSqlCommand(SQL);
                    KaizenID = (int)SequenceNumber.Value;
                }
                else
                    return "";
            }
            if (UserContext.Screens == null)
                UserContext.Screens = new List<Kaizen.Configuration.Screen>();
            UserContext.Screens.Add(Kaizen.Configuration.Screen.CMS_Payment);
            int templenth = obj.NextDepositLenth.Value - KaizenID.ToString().Length;
            string Str = string.Empty;
            for (byte i = 1; i <= templenth; i++)
            {
                Str = Str + "0";
            }
            return Str + KaizenID.ToString();
        }

        public string GetNextManualPaymentNumber(string CheckbookCode, string CurrencyCode, int PaymentMethodID)
        {
            GL00103 obj = this.GetSingle(CheckbookCode, CurrencyCode);
            string SequenceName = string.Empty;
            int KaizenID = 0;
            string PrefixSql = string.Empty;
            #region SequenceName
            switch (PaymentMethodID)
            {
                case 1:
                    PrefixSql = obj.PayCashPrefix;
                    if (obj.IsSystemPayCashNumber)
                    {
                        SequenceName = "Purchase_ManualPayment_Cash_Sequence";
                    }
                    else
                    {
                        SequenceName = "ManualPay_Cash_" + obj.CheckbookCode.Trim() + obj.CurrencyCode.Trim() + "_Sequence";
                    }
                    break;
                case 2:
                    SequenceName = "ManualPay_Credit_" + obj.CheckbookCode.Trim() + obj.CurrencyCode.Trim() + "_Sequence";
                    PrefixSql = obj.PayCreditPrefix;
                    break;
                case 3:
                    SequenceName = "ManualPay_Check_" + obj.CheckbookCode.Trim() + obj.CurrencyCode.Trim() + "_Sequence";
                    PrefixSql = obj.PayCheckPrefix;
                    break;
            }
            #endregion 

            Kaizen.Admin.Repository.Sys00000Repository Repository = new Kaizen.Admin.Repository.Sys00000Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            List<Kaizen.Admin.Sys00000> AllSessions = Repository.GetList(xx => xx.SequenceName.Trim() == SequenceName.Trim()).ToList();
            Kaizen.Admin.Sys00000 temp = AllSessions.Find(xx => xx.UserName.Trim() == UserContext.UserName.Trim());
            if (temp == null)
            {
                #region 
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
                #endregion 
            }
            else
                KaizenID = temp.KaizenID;

            if (KaizenID == 0)
            {
                string SQL = "SELECT NEXT VALUE FOR " + SequenceName;
                long? SequenceNumber = Repository.GetSQLLong(SQL);
                if (SequenceNumber.HasValue)
                {
                    KaizenID = (int)SequenceNumber.Value;
                    SQL = "insert into Sys00000(UserName,SequenceName,KaizenID) values (";
                    SQL += "'" + UserContext.UserName + "','" + SequenceName + "'," + SequenceNumber.ToString() + ");";
                    #region Update 
                    switch (PaymentMethodID)
                    {
                        case 1:
                            if (obj.IsSystemPayCashNumber)
                            {
                                SQL += "update POP000014 set PayCashLastNumber =" + KaizenID.ToString();
                            }
                            else
                            {
                                SQL += "update GL00103 set PayCashLastNumber =" + KaizenID.ToString();
                            }
                            break;
                        case 2:
                            SQL += "update GL00103 set PayCreditLastNumber =" + KaizenID.ToString();
                            break;
                        case 3:
                            SQL += "update GL00103 set PayCheckLastNumber =" + KaizenID.ToString();
                            break;
                    }
                    #endregion 

                    Repository.ExecuteSqlCommand(SQL);
                }
                else
                    return "";
            }
            if (UserContext.Screens == null)
                UserContext.Screens = new List<Kaizen.Configuration.Screen>();
            UserContext.Screens.Add(Kaizen.Configuration.Screen.CMS_Payment);
            int templenth = obj.PayCashLenth.Value - KaizenID.ToString().Length;
            string Str = string.Empty;
            for (byte i = 1; i <= templenth; i++)
            {
                Str = Str + "0";
            }
            return PrefixSql.Trim() + Str + KaizenID.ToString();
        }
        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _GL00103RepositoryDataRepository.ExecuteSqlCommand(myQuery);
            return result;
        }
    }
}
