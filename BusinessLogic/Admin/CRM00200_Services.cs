using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin.Repository;
using Kaizen.Admin;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using Kaizen.Configuration;
using Kaizen.CMS;
using Kaizen.CMS.Repository;
using System.Data;
using System.Data.SqlClient;
using Kaizen.CMS.DAL;
using System.Net.Http;
using System.Reflection;
//using System.Web.Script.Serialization;

namespace Kaizen.BusinessLogic.Admin
{
    public class CRM00200Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.CRM00200Repository _CRM00200RepositoryDataRepository;
        private CM00203Repository _CM00203RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CRM00200Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CRM00200RepositoryDataRepository = new CRM00200Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            _CM00203RepositoryDataRepository = new CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion


        public Kaizen.Admin.DataCollection<CRM00200> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<CRM00200> L = null;
            var tasks = _CRM00200RepositoryDataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            Kaizen.Admin.DataCollection<CRM00200> result = tasks;
            return result;
        }
        public Kaizen.Admin.DataCollection<CRM00200> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            Kaizen.Admin.DataCollection<CRM00200> result = null;
            var tasks = _CRM00200RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public Kaizen.Admin.DataCollection<CRM00200> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CRM00200> L = null;
            var tasks = _CRM00200RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            Kaizen.Admin.DataCollection<CRM00200> result = tasks;
            return result;
        }
        public Kaizen.Admin.DataCollection<CRM00200> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            Kaizen.Admin.DataCollection<CRM00200> result = null;
            var tasks = _CRM00200RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.TransactionID });
            result = tasks;
            return result;
        }
        public Kaizen.Admin.DataCollection<CRM00200> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            Kaizen.Admin.DataCollection<CRM00200> result = null;
            var tasks = _CRM00200RepositoryDataRepository.GetListWithPaging(SearchTerm,
                PageSize, Page, ss => ss.TransactionID, true);
            result = tasks;
            return result;
        }
        public IList<CRM00200> GetAll()
        {
            try
            {
                IList<CRM00200> L = null;
                try
                {
                    var tasks = _CRM00200RepositoryDataRepository.GetAll();
                    IList<CRM00200> result = tasks;
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
        public CRM00200 GetSingle(int TransactionID)
        {
            try
            {
                var tasks = _CRM00200RepositoryDataRepository.GetSingle(x => x.TransactionID == TransactionID);
                return tasks;
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
        public bool SendMail(int TransactionID, string filepath, string ColumnName, Kaizen00020 Kaizen00020)
        {
            var tasks = _CRM00200RepositoryDataRepository.GetSingle(x => x.TransactionID == TransactionID);
            CRM00202Repository _CRM00202Repository = new CRM00202Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            CRM00201Services srv = new CRM00201Services(UserContext);
            IList<CRM00201> Attachments = srv.GetAllByTransactionID(TransactionID);
            List<CRM00202> CRM00202List = new List<CRM00202>();

            #region Open Excel File & Get List of Email Column
            Application xlApp = new Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(filepath);
            Worksheet xlSheet = xlWorkbook.Sheets[1];
            Range xlRange = xlSheet.UsedRange;

            int numberOfRows = xlRange.Rows.Count;
            int numberOfCols = xlRange.Columns.Count;
            int columnsToRead = -1;
            for (int i = 1; i <= numberOfCols; i++)
            {
                if (xlRange.Cells[1, i].Value2 != null)
                {
                    if (xlRange.Cells[1, i].Value2.ToString().Equals(ColumnName))
                    {
                        columnsToRead = i;
                        break;
                    }
                }
            }
            // loop over each column number and add results to the list
            for (int r = 2; r <= numberOfRows; r++)
            {
                if (xlRange.Cells[r, columnsToRead].Value2 != null)
                {
                    string email = xlRange.Cells[r, columnsToRead].Value2.ToString();
                    CRM00202 CRM00202 = new CRM00202()
                    {
                        TransactionID = TransactionID,
                        ReceiverEmail = email
                    };
                    CRM00202List.Add(CRM00202);
                }
            }
            #endregion

            #region Configure SMTP Client & Email
            SmtpClient smtp = new SmtpClient(Kaizen00020.OutGoingProtocal, Convert.ToInt32(Kaizen00020.OutGoingPort));
            MailMessage mailMessage = new MailMessage();
            MailAddress from = new MailAddress(Kaizen00020.EmailID, Kaizen00020.EmailTitle);
            mailMessage.Subject = tasks.TransactionIName;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.Body = tasks.TransactionMessage;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;
            mailMessage.From = from;
            if (Kaizen00020.IsSSL == true)
            {
                smtp.EnableSsl = true;
            }
            smtp.UseDefaultCredentials = false;

            smtp.Credentials = new NetworkCredential(Kaizen00020.EmailID, Kaizen00020.EmailIPassword);
            foreach (var item in Attachments)
            {
                var destinationPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Photo/BulkEmail/"), item.DocumentName);
                mailMessage.Attachments.Add(new Attachment(destinationPath));
            }
            #endregion

            Object state = mailMessage;

            foreach (var item in CRM00202List)
            {
                MailAddress to = new MailAddress(item.ReceiverEmail);
                mailMessage.To.Clear();
                mailMessage.To.Add(to);
                smtp.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
                try
                {
                    smtp.SendAsync(mailMessage, state);
                    _CRM00202Repository.Add(item);
                }
                catch (Exception ex)
                {
                    List<CRM00202> EmailsNotSent = new List<CRM00202>();
                    EmailsNotSent.Add(item);
                }
            }
            return true;
        }
        void smtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MailMessage mail = e.UserState as MailMessage;

            if (!e.Cancelled && e.Error != null)
            {
                var Text = "Mail sent successfully";
            }
        }
        public bool AddCRM00200(CRM00200 newTask)
        {
            try
            {
                _CRM00200RepositoryDataRepository.Add(newTask);

                return true;


            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(CRM00200 UpdateCRM00200)
        {
            try
            {
                _CRM00200RepositoryDataRepository.Update(UpdateCRM00200);
                return true;


            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region Leads
        public List<CM00074> GetAllFieldsByTRXTypeID(int TRXTypeID)
        {
            CM00074Repository rep = new CM00074Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var tasks = rep.GetAll(ss => ss.TRXTypeID == TRXTypeID);
            IList<CM00074> result = tasks;
            IList<KaizenColumn> kaizenColumn = new List<KaizenColumn>();

            return result.OrderBy(x => x.FieldOrder).ToList();
        }
        public Kaizen.Admin.KaizenResult SaveLeadsData(int TRXTypeID, IList<KaizenColumn> KaizenColumn)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            Configuration.CompanyacessServices srv = new Configuration.CompanyacessServices(this.UserContext);
            
            Kaizen.Admin.KaizenResult result = new Kaizen.Admin.KaizenResult();
            using (var context = new CMSContext(this.UserContext.CompanyID, UserContext.UserName, UserContext.Password))
            {
                try
                {
                    #region 150 
                    List<string> colList = new List<string>();
                    List<object> valList = new List<object>();
                    int i = 0;

                    string query = "SELECT * FROM syscolumns WHERE id = OBJECT_ID('CRM00200') ";
                    System.Data.DataTable CRM00200Schema = this.ExecuteSqlCommandToDataTable(query);

                    List<string> CRM00200FieldNames = CRM00200Schema.AsEnumerable().Select(r => r.Field<string>("name")).ToList();

                    foreach (string field in CRM00200FieldNames)
                    {
                        KaizenColumn col = KaizenColumn.Where(c => c.FieldName.Trim().Equals(field.Trim())).FirstOrDefault();
                        if (col != null)
                        {
                            colList.Add(col.FieldName);
                            switch(col.FieldTypeID)
                            {
                                case 2:
                                case 3:
                                    valList.Add(string.IsNullOrEmpty(col.FieldValue) ? "NULL" : "'" + col.FieldValue + "'");
                                    break;
                                case 4:
                                    valList.Add(string.IsNullOrEmpty(col.FieldValue) ? 0 : Convert.ToBoolean(col.FieldValue) ? 1 : 0);
                                    break;
                                case 6:
                                    valList.Add(col.FieldValue==null?0: Convert.ToInt32(col.FieldValue));
                                    break;
                                default:
                                    valList.Add(null);
                                    break;
                            }
                        }
                    }
                    
                    string columns = string.Join(",", colList.ToArray());
                    string values = string.Join(",", valList.ToArray());
                    this.ExecuteSqlCommand(string.Format("INSERT INTO dbo.CRM00200({0},TRXTypeID) VALUES ({1},{2})", columns, values, TRXTypeID));

                    //using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(context.Database.Connection.ConnectionString, SqlBulkCopyOptions.KeepNulls & SqlBulkCopyOptions.Default))
                    //{
                    //    foreach (KaizenColumn collumn in KaizenColumn)
                    //    {
                    //        DataColumn FieldColumn = new DataColumn(collumn.FieldName);
                    //        if (collumn.FieldValue != "NULL")
                    //        {
                    //            FieldColumn.DefaultValue = collumn.FieldValue;
                    //            dt.Columns.Add(FieldColumn);
                    //            sqlBulkCopy.ColumnMappings.Add(collumn.FieldName, collumn.FieldName);
                    //        }
                    //    }
                    //    sqlBulkCopy.BatchSize = 10000;
                    //    sqlBulkCopy.DestinationTableName = "CRM00200";
                    //    sqlBulkCopy.WriteToServer(dt);
                    //}
                    #endregion,
                }
                catch (Exception ex)
                {
                    result.Status = false;
                    result.Massage = ex.Message + ex.InnerException.Message;
                    ExceptionUtility.LogException(ex, "UploadDebtorData");
                    return result;
                }
            }
            return result;
        }

        //public async Task<HttpResponseMessage> SaveLeadsData()
        //{
        //    var items = new List<KaizenColumn>();

        //    //var filesReadToProvider = await System.Web.HttpContext.Current.c Request.Content.ReadAsMultipartAsync();
        //    StreamReader reader = new StreamReader(HttpContext.Current.Request.InputStream);
        //    string requestFromPost = reader.ReadToEnd();

        //    var item = new KaizenColumn();

        //    IList<KaizenColumn> KaizenColumnList = new JavaScriptSerializer().Deserialize<IList<KaizenColumn>>(requestFromPost);

        //    //var fileStreams = new Dictionary<HttpContent, int>();
        //    //foreach (var stream in filesReadToProvider.Contents)
        //    //{
        //    //    var item = new KaizenColumn();
        //    //    //item.ElementId = int.Parse(stream.Headers.ContentDisposition.Name.Trim('\"'));
        //    //    //if filedata is file or not formdata!
        //    //    //if (!string.IsNullOrEmpty(stream.Headers.ContentDisposition.FileName))
        //    //    //{
        //    //    //    fileStreams.Add(stream, item.ElementId);
        //    //    //    item.Response = stream.Headers.ContentDisposition.FileName.Trim('\"');
        //    //    //}
        //    //    //else //formdata
        //    //    //{
        //    //        string jsonResponse = await stream.ReadAsStringAsync();
        //    //        var response = new JavaScriptSerializer().Deserialize<KaizenColumn>(jsonResponse);
        //    //        if (response != null) //this is empty file, not formdata.
        //    //        {
        //    //            item.FieldName = response.FieldName;
        //    //            item.FieldDisplay = response.FieldDisplay;
        //    //            item.FieldOrder = response.FieldOrder;
        //    //            item.FieldValue = response.FieldValue;
        //    //        }
        //    //    //}
        //    //    items.Add(item);
        //    //}

        //    //int submissionId = _submissionService.Create(formId, items);
        //    //if (submissionId != 0 && fileStreams.Count > 0)
        //    //{
        //    //    foreach (var stream_elementId in fileStreams)
        //    //    {
        //    //        var sp = new FileStreamProvider(stream_elementId.Key, _fileManager);
        //    //        await sp.Save(formId, submissionId, stream_elementId.Value);
        //    //    }
        //    //}

        //    return new HttpResponseMessage(HttpStatusCode.OK);
        //}
        //public DataTable ExecuteSqlCommandToDataTable(string myQuery)
        //{
        //    var result = _CRM00200RepositoryDataRepository.ExecuteSqlCommandToDataTable(myQuery);
        //    return result;
        //}


        #endregion
        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _CRM00200RepositoryDataRepository.ExecuteSqlCommand(myQuery);
            return result;
        }
        public System.Data.DataTable ExecuteSqlCommandToDataTable(string myQuery)
        {
            var result = _CM00203RepositoryDataRepository.ExecuteSqlCommandToDataTable(myQuery);
            return result;
        }
    }
}
