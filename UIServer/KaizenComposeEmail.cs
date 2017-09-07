using Kaizen.BusinessLogic.Configuration;
using Kaizen.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace UIServer
{
    public enum SendOptions
    {
        Single = 1,
        Bulk = 2,
        SeperateReciever = 3
    }

    public class KaizenComposeEmail
    {
        public List<KaizenEmailAddress> ToAddressList { set; get; }
        public List<KaizenEmailAddress> CcAddressList { set; get; }
        public List<KaizenEmailAddress> BccAddressList { set; get; }
        public ICollection<Attachment> Attachments { set; get; }

        public static KaizenEmailResult SendMail(
            string Subject,
            string htmlmailBody, Kaizen00020 userEmailSetting,
            List<KaizenEmailAddress> ToAddressList,
            List<KaizenEmailAddress> CcAddressList,
            List<KaizenEmailAddress> BccAddressList,
            ICollection<Attachment> Attachments,
            SendOptions option,
            string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            KaizenEmailResult result = new KaizenEmailResult();

            #region Configure SMTP Client & Email
            SmtpClient smtp = new SmtpClient(userEmailSetting.OutGoingProtocal, userEmailSetting.OutGoingPort.Value);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(userEmailSetting.EmailID.Trim(), userEmailSetting.EmailIPassword.Trim());

            if (userEmailSetting.IsSSL.Value)
            {
                smtp.Port = userEmailSetting.OutGoingPort.Value;
                smtp.EnableSsl = true;
                smtp.Host = userEmailSetting.OutGoingProtocal;
            }
            #endregion

            #region Single Email Message
            if (option == SendOptions.Single)
            {
                MailMessage singleMessage = new MailMessage();
                MailAddress defaultFrom = new MailAddress(userEmailSetting.EmailID, userEmailSetting.EmailTitle);
                singleMessage.Subject = Subject;
                singleMessage.SubjectEncoding = Encoding.UTF8;
                singleMessage.Body = htmlmailBody;
                singleMessage.BodyEncoding = Encoding.UTF8;
                singleMessage.IsBodyHtml = true;
                singleMessage.Priority = MailPriority.High;
                singleMessage.From = defaultFrom;


                #region CC Emails Part 

                if (CcAddressList != null)
                {
                    foreach (var ccEmail in CcAddressList)
                    {
                        singleMessage.CC.Add(ccEmail.Text);
                    }
                }

                #endregion

                #region BCC Emails Part 

                if (BccAddressList != null)
                {
                    foreach (var bccEmail in BccAddressList)
                    {
                        singleMessage.Bcc.Add(bccEmail.Text);
                    }
                }

                #endregion

                #region Email Attachment Part 

                if (Attachments != null)
                {
                    foreach (var file in Attachments)
                    {
                        singleMessage.Attachments.Add(file);
                    }
                }

                #endregion

                foreach (var toEmail in ToAddressList)
                {
                    singleMessage.To.Add(toEmail.Text);
                    result.Status = true;
                    result.Massage = "Email Sent Successfully ";
                }
                try
                {
                    smtp.Send(singleMessage);
                }
                catch (Exception ex)
                {
                    result.FailedEmailList = ToAddressList;
                    result.Status = false;
                    result.Massage = "Email Fail to Send Due to:\n " + ex.InnerException == null ? ex.Message: ex.InnerException.Message;
                }

            }
            #endregion

            #region Bulk Email Message
            else if (option == SendOptions.Bulk)
            {
                ThreadPool.QueueUserWorkItem(t =>
                {
                    foreach (var toEmail in ToAddressList)
                    {
                        MailMessage singleMessage = new MailMessage();
                        MailAddress defaultFrom = new MailAddress(userEmailSetting.EmailID, userEmailSetting.EmailTitle);
                        singleMessage.Subject = Subject;
                        singleMessage.SubjectEncoding = Encoding.UTF8;
                        singleMessage.Body = htmlmailBody;
                        singleMessage.BodyEncoding = Encoding.UTF8;
                        singleMessage.IsBodyHtml = true;
                        singleMessage.Priority = MailPriority.High;
                        singleMessage.From = defaultFrom;

                        #region CC Emails Part 

                        if (CcAddressList != null)
                        {
                            foreach (var ccEmail in CcAddressList)
                            {
                                singleMessage.CC.Add(ccEmail.Text);
                            }
                        }

                        #endregion

                        #region BCC Emails Part 

                        if (BccAddressList != null)
                        {
                            foreach (var bccEmail in BccAddressList)
                            {
                                singleMessage.Bcc.Add(bccEmail.Text);
                            }
                        }

                        #endregion

                        #region Email Attachment Part 

                        if (Attachments != null)
                        {
                            foreach (var file in Attachments)
                            {
                                singleMessage.Attachments.Add(file);
                            }
                        }

                        #endregion

                        singleMessage.To.Clear();
                        singleMessage.To.Add(toEmail.Text);
                        try
                        {
                            smtp.SendMailAsync(singleMessage);
                        }
                        catch (Exception ex)
                        {
                            result.FailedEmailList.Add(toEmail);
                        }
                        // Set the method that is called back when the send operation ends.
                        smtp.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                        // The userState can be any object that allows your callback 
                        // method to identify this send operation.
                        smtp.SendAsync(singleMessage, singleMessage);
                    }
                });
                foreach (var toEmail in ToAddressList)
                {
                    MailMessage singleMessage = new MailMessage();
                    MailAddress defaultFrom = new MailAddress(userEmailSetting.EmailID, userEmailSetting.EmailTitle);
                    singleMessage.Subject = Subject;
                    singleMessage.SubjectEncoding = Encoding.UTF8;
                    singleMessage.Body = htmlmailBody;
                    singleMessage.BodyEncoding = Encoding.UTF8;
                    singleMessage.IsBodyHtml = true;
                    singleMessage.Priority = MailPriority.High;
                    singleMessage.From = defaultFrom;


                    #region CC Emails Part 

                    if (CcAddressList != null)
                    {
                        foreach (var ccEmail in CcAddressList)
                        {
                            singleMessage.CC.Add(ccEmail.Text);
                        }
                    }

                    #endregion

                    #region BCC Emails Part 

                    if (BccAddressList != null)
                    {
                        foreach (var bccEmail in BccAddressList)
                        {
                            singleMessage.Bcc.Add(bccEmail.Text);
                        }
                    }

                    #endregion

                    #region Email Attachment Part 

                    if (Attachments != null)
                    {
                        foreach (var file in Attachments)
                        {
                            singleMessage.Attachments.Add(file);
                        }
                    }

                    #endregion

                    singleMessage.To.Clear();
                    singleMessage.To.Add(toEmail.Text);
                    try
                    {
                        smtp.SendMailAsync(singleMessage);
                    }
                    catch (Exception ex)
                    {
                        result.FailedEmailList.Add(toEmail);
                    }
                }

                result.Status = true;
                result.Massage = "Email Sent Successfully ";
            }
            #endregion

            #region Seperate Reciever
            else if (option == SendOptions.SeperateReciever)
            {
                MailMessage singleMessage = new MailMessage();
                MailAddress defaultFrom = new MailAddress(userEmailSetting.EmailID, userEmailSetting.EmailTitle);
                singleMessage.Subject = Subject;
                singleMessage.SubjectEncoding = Encoding.UTF8;
                singleMessage.Body = htmlmailBody;
                singleMessage.BodyEncoding = Encoding.UTF8;
                singleMessage.IsBodyHtml = true;
                singleMessage.Priority = MailPriority.High;
                singleMessage.From = defaultFrom;


                #region CC Emails Part 

                if (CcAddressList != null)
                {
                    foreach (var ccEmail in CcAddressList)
                    {
                        singleMessage.CC.Add(ccEmail.Text);
                    }
                }

                #endregion

                #region BCC Emails Part 

                if (BccAddressList != null)
                {
                    foreach (var bccEmail in BccAddressList)
                    {
                        singleMessage.Bcc.Add(bccEmail.Text);
                    }
                }

                #endregion

                #region Email Attachment Part 

                if (Attachments != null)
                {
                    foreach (var file in Attachments)
                    {
                        singleMessage.Attachments.Add(file);
                    }
                }

                #endregion

                foreach (var toEmail in ToAddressList)
                {
                    singleMessage.To.Clear();
                    singleMessage.To.Add(toEmail.Text);
                    try
                    {
                        smtp.Send(singleMessage);
                    }
                    catch (Exception ex)
                    {
                        result.FailedEmailList.Add(toEmail);
                    }
                }
                result.Status = true;
                result.Massage = "Email Sent Successfully ";
            }
            #endregion

            return result;
        }
        private static void SendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            KaizenEmailResult result = new KaizenEmailResult();
            // Get the message we sent
            MailMessage msg = (MailMessage)e.UserState;

            if (e.Cancelled)
            {
                // prompt user with "send cancelled" message 
                result.Status = true;
                result.Massage = "Email Cancelled Successfully ";
            }
            if (e.Error != null)
            {
                // prompt user with error message 
                //result.FailedEmailList.Add();
            }
            else
            {
                // prompt user with message sent!
                // as we have the message object we can also display who the message
                // was sent to etc 
                result.Status = true;
                result.Massage = "Email Sent Successfully ";
            }

            // finally dispose of the message
            if (msg != null)
                msg.Dispose();
            if (!e.Cancelled && e.Error != null)
            {
                result.Status = true;
                result.Massage = "Email Sent Successfully ";
            }
        }
    }
    public class KaizenEmailResult
    {
        public bool Status
        {
            set;
            get;
        }
        public string Massage
        {
            set;
            get;
        }
        public object Data
        {
            set;
            get;
        }
        public List<KaizenEmailAddress> FailedEmailList
        {
            set;
            get;
        }
    }
    public class KaizenEmailAddress
    {
        public string Text
        {
            set;
            get;
        }
    }

}
