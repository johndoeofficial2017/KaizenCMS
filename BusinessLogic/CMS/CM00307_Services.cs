using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Kaizen.CMS;
using Kaizen.Configuration.Repository;
using Kaizen.BusinessLogic.Configuration;
using Kaizen.CMS.Repository;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00307Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00307Repository _CM00307Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00307Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00307Repository = new Kaizen.CMS.Repository.CM00307Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00307> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            Kaizen.CMS.Repository.CM00307Repository rep = new Kaizen.CMS.Repository.CM00307Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00307> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00307", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00307> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CaseRef", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PaymentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Amount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PaymentMethodID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AgentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsApproved", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00307> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00307Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00307Repository.GetWhereListWithPaging("CM00307", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00307> GetAllViewBYSQLQuery(string Filters,
    string Searchcritaria, string CaseRef,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            DataCollection<CM00307> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00307Repository.GetListWithPaging(PageSize, Page, ss => Member);
            }
            else
            {
                SeachStr += " and CaseRef='" + CaseRef.Trim() + "'";
                result = _CM00307Repository.GetWhereListWithPaging("CM00307", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<CM00307> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00307> L = null;
            var tasks = _CM00307Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00307> result = tasks;
            return result;
        }
        public DataCollection<CM00307> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00307> result = null;
            var tasks = _CM00307Repository.GetListWithPaging(PageSize, Page, ss => new { ss.PaymentID });
            result = tasks;
            return result;
        }

        public IList<CM00307> GetAll()
        {
            var tasks = _CM00307Repository.GetAll();
            IList<CM00307> result = tasks;
            return result;
        }
        public List<Kaizen.Admin.Sys00020> GetAllPaymentMethod()
        {
            Kaizen.Admin.Repository.Sys00020Repository _Sys00020Repository = new Kaizen.Admin.Repository.Sys00020Repository(UserContext.CompanyID.Trim(), UserContext.UserName, UserContext.Password);
            var tasks = _Sys00020Repository.GetAll();
            List<Kaizen.Admin.Sys00020> result = tasks.ToList();
            return result;
        }
        public Kaizen.Admin.Sys00020 GetAllPaymentMethodByID(int PaymentMethodID)
        {
            Kaizen.Admin.Repository.Sys00020Repository _Sys00020Repository = new Kaizen.Admin.Repository.Sys00020Repository(UserContext.CompanyID.Trim(), UserContext.UserName, UserContext.Password);
            var tasks = _Sys00020Repository.GetSingle(ss => ss.PaymentMethodID == PaymentMethodID);
            return tasks;
        }
        public CM00307 GetSingle(string PaymentID)
        {
            var tasks = _CM00307Repository.GetSingle(x => x.PaymentID == PaymentID);
            return tasks;
        }
        public IList<CM00308> GetPaidCases(string PaymentID)
        {
            CM00308Repository _CM00308Repository = new CM00308Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            CM00203Repository _CM00203Repository = new CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM00308Repository.GetAll(ss => ss.PaymentID == PaymentID);
            foreach (var item in tasks)
            {
                CM00203 CM00203 = _CM00203Repository.GetSingle(ss=>ss.CaseRef == item.CaseRef);
                item.CurrencyCode = CM00203.CurrencyCode;
                item.ClaimAmount = CM00203.ClaimAmount;
            }
            return tasks;
        }

        public IList<CM00307> GetAllByCaseRef(string CaseRef)
        {
            var tasks = _CM00307Repository.GetAll();
            IList<CM00307> result = tasks;
            return result;
        }
        public KaizenResult AddCM00307(CM00307 newTask, List<CM00308> CM00308)
        {
            KaizenResult result = new KaizenResult();
            result.Status = false;
            result.Massage = "Failed to Save Data";
            using (TransactionScope txScope = new TransactionScope())
            {
                newTask.CreatedBy = this.UserContext.UserName;
                newTask.CreatedDate = DateTime.Now;
                newTask.CM00308 = CM00308;
                result = _CM00307Repository.AddKaizenObject(newTask);
                if (result.Status)
                {
                    CM00700Repository repstatus = new CM00700Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    List<CM00700> sttuslist = repstatus.GetAll(s => s.IsPaymentApply).ToList();

                    foreach (CM00700 ststus in sttuslist)
                    {
                        string where = " where " + ststus.RuleCondition + ";";
                        string sql = "update CM00203 set CaseStatusID=" + ststus.CaseStatusID.ToString();
                        if (ststus.IsCloseReminder)
                            sql += ",ReminderDateTime=null,PTPAMT=0";
                        sql += where;

                        sql += "insert into CM10701(CaseStatusID,CaseRef,AgentID,CreatedDate,ChangeStatusSourceID,ReminderDateTime,CaseStatusComment,PTPAMT)";
                        sql += "select " + ststus.CaseStatusID.ToString() + " ,CaseRef ,'" + newTask.AgentID.Trim()
                            + "',getdate(),4 ,ReminderDateTime , '" + ststus.CaseStatusname + "' ,"
                            + newTask.Amount.ToString() + " from CM00203 ";
                        sql += where;
                        _CM00307Repository.ExecuteSqlCommand(sql);
                    }
                    if (result.Status)
                        txScope.Complete();
                }
            }
            //if (result.Status)
            //{
            //    using (TransactionScope txScope = new TransactionScope())
            //    {
            //        Configuration.CompanyacessServices com = new Configuration.CompanyacessServices(this.UserContext);
            //        if (com.DeleteNextBookPaymentID(newTask.CheckbookCode.Trim()))
            //        {
            //            com.DeleteNextPaymentID();
            //            txScope.Complete();
            //            this.UserContext.Screens.Remove(Kaizen.Configuration.Screen.CMS_Payment);
            //        }
            //    }
            //}
            return result;
        }

        public KaizenResult Delete(string PaymentID)
        {
            KaizenResult result = _CM00307Repository.RemoveKaizenObject(xx => xx.PaymentID.Trim() == PaymentID.Trim());
            return result;
        }

        public KaizenResult AddCM00307(CM00307 newTask)
        {
            newTask.CreatedDate = DateTime.Now;
            newTask.CreatedBy = this.UserContext.UserName;
            KaizenResult result = _CM00307Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00307(IList<CM00307> newTask)
        {
            foreach (var item in newTask)
            {
                item.CreatedDate = DateTime.Now;
                item.CreatedBy = this.UserContext.UserName;
            }
            KaizenResult result = _CM00307Repository.AddKaizenObject(newTask.ToArray());
            //if (result.Status)
            //{
            //    Configuration.CompanyacessServices src = new Configuration.CompanyacessServices(UserContext);
            //    src.DeleteNextPaymentID();
            //}
            return result;
        }
        public KaizenResult Update(CM00307 newTask)
        {
            KaizenResult result = _CM00307Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00307> newTask)
        {
            KaizenResult result = _CM00307Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(CM00307 newTask)
        {
            KaizenResult result = _CM00307Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00307> newTask)
        {
            KaizenResult result = _CM00307Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        public string GetNextBookPaymentID(string CheckbookCode,int PaymentMethodID)
        {
            GL.GL00103Services srv = new GL.GL00103Services(this.UserContext);
            return srv.GetNextBookPaymentID(CheckbookCode.Trim(), PaymentMethodID);
        }
        public string GetNextPaymentID()
        {
            // CREATE SEQUENCE CMSPayment_Sequence START WITH 1 INCREMENT BY 1
            string SequenceName = "CMSPayment_Sequence";
            int KaizenID = 0;
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
                long? SequenceNumber = _CM00307Repository.GetSQLLong(SQL);
                if (SequenceNumber.HasValue)
                {
                    SQL = "insert into Sys00000(UserName,SequenceName,KaizenID) values (";
                    SQL += "'" + UserContext.UserName + "','" + SequenceName + "'," + SequenceNumber.ToString() + ");";
                    _CM00307Repository.ExecuteSqlCommand(SQL);
                    KaizenID = (int)SequenceNumber.Value;
                }
                else
                    return "";
            }
            if (UserContext.Screens == null)
                UserContext.Screens = new List<Kaizen.Configuration.Screen>();

            UserContext.Screens.Add(Kaizen.Configuration.Screen.CMS_Payment);
            CM00000Repository reptemp = new CM00000Repository(UserContext.UserName, UserContext.Password);
            IList<Kaizen.Configuration.CM00000> obj = reptemp.GetAll();
            Kaizen.Configuration.CM00000 tempp = obj.FirstOrDefault();
            int templenth = tempp.RCTLenth.Value - KaizenID.ToString().Length;
            string Str = string.Empty;
            for (byte i = 1; i <= templenth; i++)
            {
                Str = Str + "0";
            }
            return Str + KaizenID.ToString();
        }
        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _CM00307Repository.ExecuteSqlCommand(myQuery);
            return result;
        }
    }
}
