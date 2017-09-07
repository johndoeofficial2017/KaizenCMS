using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using Kaizen.Configuration;
using Kaizen.Admin;
using Kaizen.Admin.Repository;

namespace Kaizen.BusinessLogic.Admin
{
    public class MS_00200Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.MS_00200Repository _MS_00200RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public MS_00200Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _MS_00200RepositoryDataRepository = new MS_00200Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public Kaizen.Admin.DataCollection<MS_00200> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<MS_00200> L = null;
            var tasks = _MS_00200RepositoryDataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            Kaizen.Admin.DataCollection<MS_00200> result = tasks;
            return result;
        }
        public Kaizen.Admin.DataCollection<MS_00200> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            Kaizen.Admin.DataCollection<MS_00200> result = null;
            var tasks = _MS_00200RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public Kaizen.Admin.DataCollection<MS_00200> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<MS_00200> L = null;
            var tasks = _MS_00200RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            Kaizen.Admin.DataCollection<MS_00200> result = tasks;
            return result;
        }
        public Kaizen.Admin.DataCollection<MS_00200> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            Kaizen.Admin.DataCollection<MS_00200> result = null;
            var tasks = _MS_00200RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.TrxID });
            result = tasks;
            return result;
        }
        public Kaizen.Admin.DataCollection<MS_00200> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            Kaizen.Admin.DataCollection<MS_00200> result = null;
            var tasks = _MS_00200RepositoryDataRepository.GetListWithPaging(SearchTerm,
                PageSize, Page, ss => ss.TrxID, true);
            result = tasks;
            return result;
        }
        public IList<MS_00200> GetAll()
        {
            try
            {
                IList<MS_00200> L = null;
                try
                {
                    var tasks = _MS_00200RepositoryDataRepository.GetAll();
                    IList<MS_00200> result = tasks;
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
        public MS_00200 GetSingle(string TrxID)
        {
            try
            {
                var tasks = _MS_00200RepositoryDataRepository.GetSingle(x => x.TrxID.Trim() == TrxID.Trim());
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
        public bool SendMail(string TrxID, string filepath, string ColumnName, Kaizen00020 Kaizen00020)
        {
            var tasks = _MS_00200RepositoryDataRepository.GetSingle(x => x.TrxID.Trim() == TrxID.Trim());
            MS_00201Repository _MS_00201Repository = new MS_00201Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            MS_00003Services srv = new MS_00003Services(UserContext);
            IList<MS_00003> Attachments = srv.GetAllByMsgTemplateID(tasks.MsgTemplateID);
            List<MS_00201> MS_00201List = new List<MS_00201>();

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
                    MS_00201 MS_00201 = new MS_00201()
                    {
                        TrxID = TrxID,
                        ReceiverEmail = email
                    };
                    MS_00201List.Add(MS_00201);
                }
            }
            #endregion

            #region Configure SMTP Client & Email
            SmtpClient smtp = new SmtpClient(Kaizen00020.OutGoingProtocal, Convert.ToInt32(Kaizen00020.OutGoingPort));
            MailMessage mailMessage = new MailMessage();
            MailAddress from = new MailAddress(Kaizen00020.EmailID, Kaizen00020.EmailTitle);
            mailMessage.Subject = tasks.MsgTemplateName;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.Body = tasks.MsgTemplateContant;
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
                var destinationPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Photo/BulkEmail/"), item.DocumentID);
                mailMessage.Attachments.Add(new Attachment(destinationPath));
            }
            #endregion


            foreach (var item in MS_00201List)
            {
                MailAddress to = new MailAddress(item.ReceiverEmail);
                mailMessage.To.Clear();
                mailMessage.To.Add(to);
                try
                {
                    smtp.Send(mailMessage);
                    _MS_00201Repository.Add(item);
                }
                catch (Exception ex)
                {
                    List<MS_00201> EmailsNotSent = new List<MS_00201>();
                    EmailsNotSent.Add(item);
                }
            }
            return true;
        }

        //public bool SendMail(string TrxID, string filepath, string ColumnName, Kaizen00020 Kaizen00020)
        //{
        //    var tasks = _MS_00200RepositoryDataRepository.GetSingle(x => x.TrxID.Trim() == TrxID.Trim());
        //    MS_00201Repository _MS_00201Repository = new MS_00201Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
        //    MS_00003Services srv = new MS_00003Services(UserContext);
        //    IList<MS_00003> Attachments = srv.GetAllByMsgTemplateID(tasks.MsgTemplateID);
        //    List<MS_00201> MS_00201List = new List<MS_00201>();

        //    #region Open Excel File & Get List of Email Column
        //    Application xlApp = new Application();
        //    Workbook xlWorkbook = xlApp.Workbooks.Open(filepath);
        //    Worksheet xlSheet = xlWorkbook.Sheets[1];
        //    Range xlRange = xlSheet.UsedRange;

        //    int numberOfRows = xlRange.Rows.Count;
        //    int numberOfCols = xlRange.Columns.Count;
        //    int columnsToRead = -1;
        //    for (int i = 1; i <= numberOfCols; i++)
        //    {
        //        if (xlRange.Cells[1, i].Value2 != null)
        //        {
        //            if (xlRange.Cells[1, i].Value2.ToString().Equals(ColumnName))
        //            {
        //                columnsToRead = i;
        //                break;
        //            }
        //        }
        //    }
        //    // loop over each column number and add results to the list
        //    for (int r = 2; r <= numberOfRows; r++)
        //    {
        //        if (xlRange.Cells[r, columnsToRead].Value2 != null)
        //        {
        //            string email = xlRange.Cells[r, columnsToRead].Value2.ToString();
        //            MS_00201 MS_00201 = new MS_00201()
        //            {
        //                TrxID = TrxID,
        //                ReceiverEmail = email
        //            };
        //            MS_00201List.Add(MS_00201);
        //        }
        //    }
        //    #endregion

        //    #region Configure SMTP Client & Email
        //    SmtpClient smtp = new SmtpClient(Kaizen00020.OutGoingProtocal, Convert.ToInt32(Kaizen00020.OutGoingPort));
        //    MailMessage mailMessage = new MailMessage();
        //    MailAddress from = new MailAddress(Kaizen00020.EmailID, Kaizen00020.EmailTitle);
        //    mailMessage.Subject = tasks.MsgTemplateName;
        //    mailMessage.SubjectEncoding = Encoding.UTF8;
        //    mailMessage.Body = tasks.MsgTemplateContant;
        //    mailMessage.BodyEncoding = Encoding.UTF8;
        //    mailMessage.IsBodyHtml = true;
        //    mailMessage.Priority = MailPriority.Normal;
        //    mailMessage.From = from;
        //    if (Kaizen00020.IsSSL == true)
        //    {
        //        smtp.EnableSsl = true;
        //    }
        //    smtp.UseDefaultCredentials = false;

        //    smtp.Credentials = new NetworkCredential(Kaizen00020.EmailID, Kaizen00020.EmailIPassword);
        //    foreach (var item in Attachments)
        //    {
        //        var destinationPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Photo/BulkEmail/"), item.DocumentID);
        //        mailMessage.Attachments.Add(new Attachment(destinationPath));
        //    }
        //    #endregion


        //    foreach (var item in MS_00201List)
        //    {
        //        MailAddress to = new MailAddress(item.ReceiverEmail);
        //        mailMessage.To.Clear();
        //        mailMessage.To.Add(to);
        //        try
        //        {
        //            smtp.Send(mailMessage);
        //            _MS_00201Repository.Add(item);
        //        }
        //        catch (Exception ex)
        //        {
        //            List<MS_00201> EmailsNotSent = new List<MS_00201>();
        //            EmailsNotSent.Add(item);
        //        }
        //    }
        //    return true;
        //}
        public bool AddMS_00200(MS_00200 newTask)
        {
            try
            {
                _MS_00200RepositoryDataRepository.Add(newTask);

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
        public bool Update(MS_00200 UpdateMS_00200)
        {
            try
            {
                _MS_00200RepositoryDataRepository.Update(UpdateMS_00200);
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

    }
}
