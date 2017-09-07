using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;
using System.Data.SqlClient;
using Kaizen.GL.DAL;
using System.Data;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00100Services
    {
        #region Infrastructure

        private GL00100Repository _GL00100Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00100Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00100Repository = new GL00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00100> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00100Repository.GetWhereListWithPaging("GL00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00100> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("AccountID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ACTNUMBR", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountAlias", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsPL", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsDebit", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InSales", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InPurchase", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InInventory", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InPayroll", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InManufacturing", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InExpenseManagement", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InPOS", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Inbank", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsAllowAccountEntry", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Inactive", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<GL00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00100Repository.GetWhereListWithPaging("GL00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00100> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<GL00100> L = null;
            var tasks = _GL00100Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<GL00100> result = tasks;
            return result;
        }

        public DataCollection<GL00100> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<GL00100> result = null;
            var tasks = _GL00100Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<GL00100> GetAll()
        {
            var tasks = _GL00100Repository.GetAll();
            IList<GL00100> result = tasks;
            return result;
        }
        public DataCollection<GL00100> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<GL00100> L = null;
            var tasks = _GL00100Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<GL00100> result = tasks;
            return result;
        }
        public DataCollection<GL00100> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00100> result = null;

            var tasks = _GL00100Repository.GetListWithPaging(x => x.AccountName.ToString().Contains(SearchTerm) ||
                x.ACTNUMBR.Contains(SearchTerm)
                , PageSize, Page, ss => new { ss.AccountID });
            result = tasks;
            return result;
        }
        public DataCollection<GL00100> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00100> result = null;
            var tasks = _GL00100Repository.GetListWithPaging(PageSize, Page, ss => new { ss.AccountID }
                , ss => ss.GL00020);
            result = tasks;
            return result;
        }
        public GL00100 GetSingle(int AccountID)
        {
            var tasks = _GL00100Repository.GetSingle(x => x.AccountID == AccountID);
            return tasks;
        }

        public GL00100 GetSingleByACTNUMBR(string ACTNUMBR)
        {
            string SegmentMark = Master.InstalledCompany.Find(ss => ss.CompanyID.Trim() == this.UserContext.CompanyID.Trim()).SegmentMark;
            var tasks = _GL00100Repository.GetSingle(x => x.ACTNUMBR == ACTNUMBR.Replace(SegmentMark.Trim(), ""));
            return tasks;
        }

        public KaizenResult AddGL00100(GL00100 newTask)
        {
            var result = _GL00100Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00100(IList<GL00100> newTask)
        {
            var result = _GL00100Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00100 UpdateGL00100)
        {
            var result = _GL00100Repository.UpdateKaizenObject(UpdateGL00100);
            return result;
        }
        public KaizenResult Update(IList<GL00100> UpdateGL00100)
        {
            var result = _GL00100Repository.UpdateKaizenObject(UpdateGL00100.ToArray());
            return result;
        }

        public KaizenResult Delete(int AccountID)
        {
            var result = _GL00100Repository.RemoveKaizenObject(xx => xx.AccountID == AccountID);
            return result;
        }
        public KaizenResult Delete(GL00100 newTask)
        {
            var result = _GL00100Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00100> newTask)
        {
            var result = _GL00100Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Upload(IList<Kaizen.Configuration.Kaizen00001> KaizenColumn, string destinationPath)
        {
            Configuration.CompanyacessServices srv = new Configuration.CompanyacessServices(UserContext);

            using (var context = new GLContext(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password))
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(context.Database.Connection.ConnectionString, SqlBulkCopyOptions.KeepNulls & SqlBulkCopyOptions.Default))
                {
                    foreach (Kaizen.Configuration.Kaizen00001 collumn in KaizenColumn)
                    {
                        bulkCopy.ColumnMappings.Add(collumn.FieldValue, collumn.FieldName);
                    }
                    bulkCopy.BatchSize = 10000;
                    bulkCopy.DestinationTableName = "GL00100";
                    KaizenResult result = new KaizenResult();
                    GL00020Services GL00020_srv = new GL00020Services(UserContext);
                    try
                    {
                        DataTable dt = srv.ReadExcelSheet(destinationPath);
                        foreach (dynamic item in bulkCopy.ColumnMappings)
                        {
                            if (item.DestinationColumn == "ACTNUMBR")
                            {
                                string str = item.SourceColumn;
                                foreach (DataRow row in dt.Rows)
                                {
                                    row[str] = RemoveSpecialCharacters(row[str].ToString());
                                }
                            }
                            if (item.DestinationColumn == "CategoryID")
                            {
                                string str = item.SourceColumn;
                                foreach (DataRow row in dt.Rows)
                                {
                                    row[str] = GL00020_srv.GetIDByName(row[str].ToString());
                                }
                            }
                        }

                        bulkCopy.WriteToServer(dt.CreateDataReader());
                        result.Status = true;
                        result.Massage = "Data has been Uploaded Successfully";
                        return result;
                    }
                    catch (Exception ex)
                    {
                        result.Status = false;
                        result.Massage = "Failed to Upload Data";
                        return result;
                    }
                }
            }
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
