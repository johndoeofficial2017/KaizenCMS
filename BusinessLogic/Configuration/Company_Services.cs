using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using Kaizen.Configuration.Repository;
using Kaizen.Configuration;
using X2DataTable;
using Excel;

namespace Kaizen.BusinessLogic.Configuration
{
    public class CompanyacessServices
    {
        #region Infrastructure

        private CompanyRepository _CompanyRepository;
        private KaizenSession UserContext;
        public CompanyacessServices()
        {

        }
        public CompanyacessServices(KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CompanyRepository = new CompanyRepository(UserContext.UserName, UserContext.Password);
            if (UserContext.Screens == null)
                UserContext.Screens = new List<Screen>();
        }
        #endregion
        public DataCollection<Company> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Company> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CompanyRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CompanyRepository.GetWhereListWithPaging("Company", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<Company> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<Company> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CompanyRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CompanyRepository.GetWhereListWithPaging("Company", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }



        public DataCollection<Company> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<Company> L = null;
            var tasks = _CompanyRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<Company> result = tasks;
            return result;
        }
        public DataCollection<Company> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<Company> result = null;
            var tasks = _CompanyRepository.GetListWithPaging(PageSize, Page, ss => Member, null, null);
            result = tasks;
            return result;
        }

        public DataCollection<Company> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Company> L = null;
            var tasks = _CompanyRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                , xx => xx.CompanyID);
            DataCollection<Company> result = tasks;
            return result;
        }
        public DataCollection<Company> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Company> result = null;
            var tasks = _CompanyRepository.GetListWithPaging(x => x.CompanyName.ToString().Contains(SearchTerm) ||
                x.CompanyID.Contains(SearchTerm)
                , PageSize, Page, ss => new { ss.CompanyID });
            result = tasks;
            return result;
        }
        public DataCollection<Company> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Company> result = null;
            var tasks = _CompanyRepository.GetListWithPaging(PageSize, Page, ss => new { ss.CompanyID });
            result = tasks;
            return result;
        }
        public List<Company> GetAllCompanies()
        {
            CompanyRepository rep = new CompanyRepository();
            var tasks = rep.GetAllFromKaizen(ss => ss.CompanySegments);
            List<Company> result = tasks.ToList();
            return result;
        }
        public IList<Company> GetMyCompanies(string UserName)
        {
            CompanyRepository rep = new CompanyRepository();
            var tasks = rep.GetListFromKaizen(xx => xx.CompanyAccesses.Any(ss =>
            ss.UserName.Trim() == UserName));
            IList<Company> result = tasks;
            return result;
        }
        public IList<Kaizen004View> GetMyModules()
        {
            Kaizen004ViewRepository Repository = new Kaizen004ViewRepository(UserContext.UserName, UserContext.Password);
            var tasks = Repository.GetList(xx => xx.UserName.Trim() == this.UserContext.UserName.Trim()
            //&& xx.CompanyID == this.UserContext.CompanyID
            );
            IList<Kaizen004View> result = tasks;
            return result;
        }

        public List<KaizenSequence> GetSequences(string CompanyID, string SequenceName)
        {
            KaizenSequenceRepository Repository = new KaizenSequenceRepository(UserContext.UserName, UserContext.Password);
            var tasks = Repository.GetList(xx => xx.CompanyID == CompanyID && xx.SequenceName.Trim() == SequenceName.Trim());
            return tasks.ToList();
        }
        internal int? GetNextTransactionID(string SequenceName)
        {
            List<KaizenSequence> AllSessions = this.GetSequences(UserContext.CompanyID, SequenceName);
            KaizenSequence temp = AllSessions.Find(xx => xx.UserName.Trim() == UserContext.UserName.Trim());
            if (temp != null)
                return temp.KaizenID;

            string SQL = string.Empty;
            foreach (KaizenSequence row in AllSessions)
            {
                KaizenSession KaizenSession = UserServices.GetSessions(UserContext.CompanyID).Find(xx => xx.UserName == row.UserName);
                if (KaizenSession == null)
                    return row.KaizenID;
                foreach (Screen scrn in KaizenSession.Screens)
                {

                }
            }
            //-----------New ID-----------------------------------------------------
            SQL = "SELECT NEXT VALUE FOR " + SequenceName;
            KaizenSequenceRepository _KaizenSequenceRepository = new KaizenSequenceRepository(UserContext.UserName, UserContext.Password);
            long? SequenceNumber = _KaizenSequenceRepository.ExecuteSqlCommandInt(SQL);
            if (SequenceNumber.HasValue)
            {
                SQL = "insert into KaizenSequence(CompanyID,UserName,SequenceName,KaizenID) values (";
                SQL += "'" + UserContext.CompanyID.Trim() + "','" + UserContext.UserName + "','" + SequenceName + "'," + SequenceNumber.ToString() + ");";
                _KaizenSequenceRepository.ExecuteSqlCommand(SQL);
                return (int)SequenceNumber.Value;
            }
            else
                return (int?)SequenceNumber;
        }
        internal int? GetNextTransactionID(Screen currentScreen,
            string Cat = "")
        {
            string SequenceName = string.Empty;
            switch (currentScreen)
            {
                case Screen.CMS_Payment:
                    if (string.IsNullOrEmpty(Cat))
                        SequenceName = "PaymentID_" + this.UserContext.CompanyID.Trim() + "_Sequence";
                    else
                        SequenceName = "PaymentID_" + Cat.Trim() + "_" + this.UserContext.CompanyID.Trim() + "_Sequence";
                    break;
            }
            List<KaizenSequence> AllSessions = this.GetSequences(UserContext.CompanyID, SequenceName);
            KaizenSequence temp = AllSessions.Find(xx => xx.UserName.Trim() == UserContext.UserName.Trim());
            if (temp != null)
                return temp.KaizenID;

            string SQL = string.Empty;
            foreach (KaizenSequence row in AllSessions)
            {
                KaizenSession KaizenSession = UserServices.GetUserSession(row.UserName, UserContext.CompanyID);
                if (KaizenSession == null)
                {
                    SQL = "update KaizenSequence set UserName = '" + UserContext.UserName.Trim() + "' where UserName ='" +
                        row.UserName.TrimStart() + "' and CompanyID ='" + row.CompanyID.Trim() + "' and SequenceName ='" + SequenceName + "'";
                    this.ExecuteSqlCommand(SQL);
                    return row.KaizenID;
                }
                bool notUse = true;
                foreach (Screen scrn in KaizenSession.Screens)
                {
                    if (scrn == currentScreen)
                        notUse = false;
                }
                if (notUse)
                    return row.KaizenID;
            }

            //-----------New ID-----------------------------------------------------
            SQL = "SELECT NEXT VALUE FOR " + SequenceName;
            KaizenSequenceRepository _KaizenSequenceRepository = new KaizenSequenceRepository(UserContext.UserName, UserContext.Password);
            long? SequenceNumber = _KaizenSequenceRepository.ExecuteSqlCommandInt(SQL);
            if (SequenceNumber.HasValue)
            {
                SQL = "insert into KaizenSequence(CompanyID,UserName,SequenceName,KaizenID) values (";
                SQL += "'" + UserContext.CompanyID.Trim() + "','" + UserContext.UserName + "','" + SequenceName + "'," + SequenceNumber.ToString() + ");";
                _KaizenSequenceRepository.ExecuteSqlCommand(SQL);
                return (int)SequenceNumber.Value;
            }
            else
                return (int?)SequenceNumber;
        }
        #region GL Transaction Next ID
        //internal int GetNextGLIDKaizenID(string YearCode)
        //{
        //    string SequenceName = "FiscalPeriod_" + YearCode.Trim() + "_" + this.UserContext.CompanyID.Trim() + "_Sequence";
        //    int temp = (int)this.GetNextTransactionID(SequenceName);
        //    return temp;
        //}
        //internal void DeleteNexttNextGLID(string YearCode)
        //{
        //    string SequenceName = "FiscalPeriod_" + YearCode.Trim() + "_" + this.UserContext.CompanyID.Trim() + "_Sequence";
        //    KaizenSequenceRepository _KaizenSequenceRepository = new KaizenSequenceRepository(UserContext.UserName, UserContext.Password);
        //    string SQL = "delete KaizenSequence where CompanyID='" + this.UserContext.CompanyID.Trim()
        //        + "' and UserName='" + this.UserContext.UserName.Trim() + "' and SequenceName='" + SequenceName + "'";
        //    _KaizenSequenceRepository.ExecuteSqlCommand(SQL);
        //}
        //internal void SetUpNextGLID(string YearCode, int STARTWith)
        //{
        //    string SQL = "CREATE SEQUENCE FiscalPeriod_" + YearCode.ToString() + "_" + UserContext.CompanyID.Trim() + "_Sequence START WITH " + STARTWith.ToString() + " INCREMENT BY 1";
        //    KaizenSequenceRepository _KaizenSequenceRepository = new KaizenSequenceRepository(UserContext.UserName, UserContext.Password);
        //    _KaizenSequenceRepository.ExecuteSqlCommand(SQL);
        //}
        #endregion 
        #region CLient Next ID
        public string GetNextClientID(string CUSTCLAS)
        {
            //CMS.CM00021Services srv = new CMS.CM00021Services(this.UserContext);
            //Kaizen.CMS.CM00021 o = srv.GetSingle(CUSTCLAS.Trim());
            //if (o == null) return "";
            //if (!o.Prefixlengh.HasValue)
            //{
            //    return "";
            //}
            //int temp = this.GetNextClientKaizenID(CUSTCLAS);
            //this.UserContext.KaizenID = temp;
            //this.UserContext.CurrentScreen = Screen.Client;
            //int templenth = o.Prefixlengh.Value - temp.ToString().Length;
            //string Str = string.Empty;
            //for (byte i = 1; i <= templenth; i++)
            //{
            //    Str = Str + "0";
            //}
            //return o.PrefixValue + Str + temp.ToString();
            return "";
        }
        internal int GetNextClientKaizenID(string CUSTCLAS)
        {
            string SequenceName = "ClientID_" + CUSTCLAS.Trim() + "_" + this.UserContext.CompanyID.Trim() + "_Sequence";
            int temp = (int)this.GetNextTransactionID(SequenceName);
            return temp;
        }
        internal void DeleteNextClientID(string CUSTCLAS)
        {
            string SequenceName = "ClientID_" + CUSTCLAS.Trim() + "_" + this.UserContext.CompanyID.Trim() + "_Sequence";
            KaizenSequenceRepository _KaizenSequenceRepository = new KaizenSequenceRepository(UserContext.UserName, UserContext.Password);
            string SQL = "delete KaizenSequence where CompanyID='" + this.UserContext.CompanyID.Trim()
                + "' and UserName='" + this.UserContext.UserName.Trim() + "' and SequenceName='" + SequenceName + "'";
            _KaizenSequenceRepository.ExecuteSqlCommand(SQL);
        }
        internal void SetUpNextClientID(string CUSTCLAS)
        {
            string SQL = "CREATE SEQUENCE ClientID_" + CUSTCLAS.Trim() + "_" + UserContext.CompanyID.Trim() + "_Sequence START WITH 1 INCREMENT BY 1";
            KaizenSequenceRepository _KaizenSequenceRepository = new KaizenSequenceRepository(UserContext.UserName, UserContext.Password);
            _KaizenSequenceRepository.ExecuteSqlCommand(SQL);
        }
        #endregion 

        #region Debtor Next ID
        public string GetNextDebtorID(string CUSTCLAS)
        {
            //CMS.CM00010Services srv = new CMS.CM00010Services(this.UserContext);
            //Kaizen.CMS.CM00010 o = srv.GetSingle(CUSTCLAS.Trim());
            //if (o == null) return "";
            //if (!o.Prefixlengh.HasValue)
            //{
            //    return "";
            //}
            //int temp = this.GetNextDebtorKaizenID(CUSTCLAS);
            //this.UserContext.KaizenID = temp;
            //this.UserContext.CurrentScreen = Screen.Debtor;
            //int templenth = o.Prefixlengh.Value - temp.ToString().Length;
            //string Str = string.Empty;
            //for (byte i = 1; i <= templenth; i++)
            //{
            //    Str = Str + "0";
            //}
            //return o.PrefixValue + Str + temp.ToString();
            return "";
        }
        internal int GetNextDebtorKaizenID(string CUSTCLAS)
        {
            string SequenceName = "DebtorID_" + CUSTCLAS.Trim() + "_" + this.UserContext.CompanyID.Trim() + "_Sequence";
            int temp = (int)this.GetNextTransactionID(SequenceName);
            return temp;
        }
        internal void DeleteNextDebtorID(string CUSTCLAS)
        {
            string SequenceName = "DebtorID_" + CUSTCLAS.Trim() + "_" + this.UserContext.CompanyID.Trim() + "_Sequence";
            KaizenSequenceRepository _KaizenSequenceRepository = new KaizenSequenceRepository(UserContext.UserName, UserContext.Password);
            string SQL = "delete KaizenSequence where CompanyID='" + this.UserContext.CompanyID.Trim()
                + "' and UserName='" + this.UserContext.UserName.Trim() + "' and SequenceName='" + SequenceName + "'";
            _KaizenSequenceRepository.ExecuteSqlCommand(SQL);
        }
        internal void SetUpNextDebtorID(string CUSTCLAS)
        {
            string SQL = "CREATE SEQUENCE DebtorID_" + CUSTCLAS.Trim() + "_" + UserContext.CompanyID.Trim() + "_Sequence START WITH 1 INCREMENT BY 1";
            KaizenSequenceRepository _KaizenSequenceRepository = new KaizenSequenceRepository(UserContext.UserName, UserContext.Password);
            _KaizenSequenceRepository.ExecuteSqlCommand(SQL);
        }
        #endregion 

        #region Vendor Next ID
        public string GetNextVendorID(string ClassID)
        {
            //Purchase.POP00010Services srv = new Purchase.POP00010Services(this.UserContext);
            //Kaizen.Purchase.POP00010 o = srv.GetSingle(ClassID.Trim());
            //if (o == null) return "";
            //if (!o.Prefixlengh.HasValue)
            //{
            //    return "";
            //}
            //int temp = this.GetNextVendorKaizenID(ClassID);
            //this.UserContext.KaizenID = temp;
            //this.UserContext.CurrentScreen = Screen.Vendor;
            //int templenth = o.Prefixlengh.Value - temp.ToString().Length;
            //string Str = string.Empty;
            //for (byte i = 1; i <= templenth; i++)
            //{
            //    Str = Str + "0";
            //}
            //return o.PrefixValue + Str + temp.ToString();
            return "";
        }
        internal int GetNextVendorKaizenID(string ClassID)
        {
            string SequenceName = "VendorID_" + ClassID.Trim() + "_" + this.UserContext.CompanyID.Trim() + "_Sequence";
            int temp = (int)this.GetNextTransactionID(SequenceName);
            return temp;
        }
        internal void DeleteNextVendorID(string ClassID)
        {
            string SequenceName = "VendorID_" + ClassID.Trim() + "_" + this.UserContext.CompanyID.Trim() + "_Sequence";
            KaizenSequenceRepository _KaizenSequenceRepository = new KaizenSequenceRepository(UserContext.UserName, UserContext.Password);
            string SQL = "delete KaizenSequence where CompanyID='" + this.UserContext.CompanyID.Trim()
                + "' and UserName='" + this.UserContext.UserName.Trim() + "' and SequenceName='" + SequenceName + "'";
            _KaizenSequenceRepository.ExecuteSqlCommand(SQL);
        }
        internal void SetUpNextVendorID(string ClassID)
        {
            string SQL = "CREATE SEQUENCE VendorID_" + ClassID.Trim() + "_" + UserContext.CompanyID.Trim() + "_Sequence START WITH 1 INCREMENT BY 1";
            KaizenSequenceRepository _KaizenSequenceRepository = new KaizenSequenceRepository(UserContext.UserName, UserContext.Password);
            _KaizenSequenceRepository.ExecuteSqlCommand(SQL);
        }
        #endregion 
        #region Customer Next ID
        public string GetNextCustomerID(string CUSTCLAS)
        {
            //Kaizen.BusinessLogic.SOP.SOP00010Services srv = new SOP.SOP00010Services(this.UserContext);
            //Kaizen.SOP.SOP00010 o = srv.GetSingle(CUSTCLAS.Trim());
            //if (o == null) return "";
            //if (!o.Prefixlengh.HasValue)
            //{
            //    return "";
            //}
            //int temp = this.GetNextCustomerKaizenID(CUSTCLAS);
            //this.UserContext.KaizenID = temp;
            //this.UserContext.CurrentScreen = Screen.Customer;
            //int templenth = o.Prefixlengh.Value - temp.ToString().Length;
            //string Str = string.Empty;
            //for (byte i = 1; i <= templenth; i++)
            //{
            //    Str = Str + "0";
            //}
            //return o.PrefixValue + Str + temp.ToString();
            return "";
        }
        internal int GetNextCustomerKaizenID(string CUSTCLAS)
        {
            string SequenceName = "CUSTNMBR_" + CUSTCLAS.Trim() + "_" + this.UserContext.CompanyID.Trim() + "_Sequence";
            int temp = (int)this.GetNextTransactionID(SequenceName);
            return temp;
        }
        internal void DeleteNextCustomerID(string CUSTCLAS)
        {
            string SequenceName = "CUSTNMBR_" + CUSTCLAS.Trim() + "_" + this.UserContext.CompanyID.Trim() + "_Sequence";
            KaizenSequenceRepository _KaizenSequenceRepository = new KaizenSequenceRepository(UserContext.UserName, UserContext.Password);
            string SQL = "delete KaizenSequence where CompanyID='" + this.UserContext.CompanyID.Trim()
                + "' and UserName='" + this.UserContext.UserName.Trim() + "' and SequenceName='" + SequenceName + "'";
            _KaizenSequenceRepository.ExecuteSqlCommand(SQL);
        }
        internal void SetUpNextCustomerID(string CUSTCLAS)
        {
            string SQL = "CREATE SEQUENCE CUSTNMBR_" + CUSTCLAS.Trim() + "_" + UserContext.CompanyID.Trim() + "_Sequence START WITH 1 INCREMENT BY 1";
            KaizenSequenceRepository _KaizenSequenceRepository = new KaizenSequenceRepository(UserContext.UserName, UserContext.Password);
            _KaizenSequenceRepository.ExecuteSqlCommand(SQL);
        }
        #endregion 

        public DataCollection<Company> GetListWithPaging(string SearchTerm, string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Company> result = null;
            var tasks = _CompanyRepository.GetListWithPaging(x => x.CompanyID.Contains(SearchTerm) ||
                x.CompanyName.Contains(SearchTerm)
                , PageSize, Page, ss => new { ss.CompanyID }, null);
            result = tasks;
            return result;
        }
        public Company GetSingle(string CompanyID)
        {
            var tasks = _CompanyRepository.GetSingle(x => x.CompanyID == CompanyID);
            return tasks;
        }
        public KaizenResult AddCompany(Company newTask)
        {
            var result = _CompanyRepository.AddKaizenObject(newTask);
            Master.InstalledCompany.Add(newTask);
            //if (Master.IsHrInsalled())
            //{
            //    Config00001Repository _Config00001Repository = new Config00001Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            //    Config00001 o = new Config00001();
            //    o.CompanyID = newTask.CompanyID;
            //    o.IsAutoItemID = false;
            //    o.IsAutoItemIDByCat = false;
            //    _Config00001Repository.Add(o);
            //}
            return result;
        }
        internal void UpdateGLFormat(string CompanyID)
        {
            Company com = this.GetSingle(CompanyID);

            CompanySegmentServices srv = new CompanySegmentServices(this.UserContext);
            IList<CompanySegment> L = srv.GetByCompanyID(com.CompanyID.Trim());
            string GLFOrmat = string.Empty;
            foreach (CompanySegment o in L)
            {
                for (int i = 1; i <= o.SegmentLength; i++)
                    GLFOrmat += "0";
                GLFOrmat += com.SegmentMark;
            }
            if (!string.IsNullOrEmpty(GLFOrmat))
                GLFOrmat = GLFOrmat.Substring(0, GLFOrmat.Length - 1);
            this.ExecuteSqlCommand("update Company set GLFormat='" + GLFOrmat + "' where CompanyID='" + com.CompanyID.Trim() + "'");

        }
        public KaizenResult Update(Company UpdateCompany)
        {
            var result = _CompanyRepository.UpdateKaizenObject(UpdateCompany);
            return result;
        }
        public KaizenResult Delete(Company Company)
        {
            var result = _CompanyRepository.DeleteKaizenObject(Company);
            return result;
        }
        public List<ExcelColumns> ReadExcelColumnsWithoutInsert(string destinationPath, string fileName)
        {
            DataTable dt = ReadExcelData(destinationPath);
            List<ExcelColumns> lstExcelColumns = new List<ExcelColumns>();
            foreach (DataColumn column in dt.Columns)
            {
                ExcelColumns objExcelolumn = new ExcelColumns();
                objExcelolumn.ColumnName = column.ColumnName;
                objExcelolumn.Index = column.ColumnName.Trim();
                lstExcelColumns.Add(objExcelolumn);
            }
            return lstExcelColumns;
        }

        public List<ExcelColumns> ReadExcelColumns(string destinationPath, string fileName)
        {
            DataTable dt = ReadExcelSheet(destinationPath);
            List<ExcelColumns> lstExcelColumns = new List<ExcelColumns>();
            string sql = string.Empty;
            foreach (DataColumn column in dt.Columns)
            {
                if (Regex.IsMatch(column.ColumnName, @"^[A-Za-z]+$"))
                {
                    ExcelColumns objExcelolumn = new ExcelColumns();
                    objExcelolumn.ColumnName = column.ColumnName;
                    objExcelolumn.Index = column.ColumnName.Trim();
                    sql += column.ColumnName.Trim() + " [nvarchar](50) NULL,";
                    lstExcelColumns.Add(objExcelolumn);
                }
            }
            sql = sql.Substring(0, sql.Length - 1);

            sql = "CREATE TABLE [dbo].[KaizenTemp" + fileName.Trim() + "](" + sql + ")";
            ExecuteSqlCommand(sql);
            sql = string.Empty;
            string collum = string.Empty;
            foreach (DataRow row in dt.Rows)
            {
                string colname = string.Empty;
                string colValue = string.Empty;
                foreach (ExcelColumns col in lstExcelColumns)
                {
                    colValue += "'" + row[col.Index].ToString() + "',";
                    colname += col.Index + ",";
                }
                colValue = colValue.Substring(0, colValue.Length - 1);
                colname = colname.Substring(0, colname.Length - 1);
                sql += "INSERT INTO [dbo].[KaizenTemp" + fileName.Trim() + "] (" + colname + ")VALUES (" + colValue + ")";
                ExecuteSqlCommand(sql);
            }
            //using (SqlBulkCopy bulkCopy = new SqlBulkCopy(KaizenUser.ConnectionString))
            //{
            //    bulkCopy.DestinationTableName =
            //        "dbo.BulkCopyDemoMatchingColumns";

            //    try
            //    {
            //        // Write from the source to the destination.
            //        bulkCopy.WriteToServer(dt);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}
            return lstExcelColumns;
        }
        public DataTable ReadExcelSheet(string FilePath)
        {
            try
            {
                DataTable xlsDataTable = X2DataTable.X2DataTable.ReadToDataTable(
               InputFileType.EXCEL, true, FilePath, "Sheet1");
                return xlsDataTable;

                string conStr = "";
                switch (Path.GetExtension(FilePath))
                {
                    case ".xls":
                        conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1};IMEX=1'";
                        break;
                    case ".xlsx":
                        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1};IMEX=1'";
                        break;
                }
                conStr = String.Format(conStr, FilePath, true);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();
                return dt;
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "ReadExcelSheet");
            }
            return null;
        }
        public DataTable ReadExcelData(string FilePath)
        {
            try
            {
                FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader = null;
                switch (Path.GetExtension(FilePath))
                {
                    case ".xls":
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                        break;
                    case ".xlsx":
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        break;
                }
                excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();
                excelReader.Close();
                return result.Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "ReadExcelData");
            }
            return null;
        }

        public DataTable GetDataTable(string SQL, string CompanyID)
        {
            try
            {
                CompanyRepository rep = new CompanyRepository();
                DataTable table = rep.GetKaizenLookUpTable(SQL);
                return table;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _CompanyRepository.ExecuteSqlCommandInt(myQuery);
            return result;
        }
        public DataTable ExecuteSqlCommandToDataTable(string myQuery)
        {
            var result = _CompanyRepository.ExecuteSqlCommandToDataTable(myQuery);
            return result;
        }
    }
}
