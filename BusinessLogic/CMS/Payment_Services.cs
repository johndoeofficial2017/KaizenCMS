using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Kaizen.CMS;
using Kaizen.Configuration.Repository;
using Kaizen.BusinessLogic.Configuration;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00207Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00207Repository _CM00207Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00207Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00207Repository = new Kaizen.CMS.Repository.CM00207Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM10207> GetWhereListWithPaging(string Filters, string Searchcritaria
             , int TRXTypeID, string whereCondition, string viewSortCondition, int PageSize
            , int Page)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            if (!string.IsNullOrEmpty(SeachStr))
                SeachStr += " and ";
            SeachStr += "TRXTypeID=" + TRXTypeID.ToString();
            if (!string.IsNullOrEmpty(whereCondition))
                SeachStr += " and " + whereCondition;

            Kaizen.CMS.Repository.CM10207Repository rep = new Kaizen.CMS.Repository.CM10207Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM10207> result = null;
            result = rep.GetWhereListWithPaging("CM10207", PageSize, Page, SeachStr, viewSortCondition);
            return result;
        }
        public DataCollection<CM00207> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            Kaizen.CMS.Repository.CM00207Repository rep = new Kaizen.CMS.Repository.CM00207Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00207> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00207", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00207> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<CM00207> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00207Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00207Repository.GetWhereListWithPaging("CM00207", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00207> GetAllViewBYSQLQuery(string Filters,
    string Searchcritaria, string DebtorID,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            DataCollection<CM00207> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00207Repository.GetListWithPaging(ss=>ss.DebtorID == DebtorID, PageSize, Page, ss => Member);
            }
            else
            {
                SeachStr += " and DebtorID='" + DebtorID.Trim() + "'";
                result = _CM00207Repository.GetWhereListWithPaging("CM00207", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<CM00207> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00207> L = null;
            var tasks = _CM00207Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00207> result = tasks;
            return result;
        }
        public DataCollection<CM00207> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00207> result = null;
            var tasks = _CM00207Repository.GetListWithPaging(PageSize, Page, ss => new { ss.PaymentID });
            result = tasks;
            return result;
        }

        public IList<CM00207> GetAll()
        {
            try
            {
                IList<CM00207> L = null;
                try
                {
                    var tasks = _CM00207Repository.GetAll();
                    IList<CM00207> result = tasks;
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
        public List<Kaizen.Admin.Sys00020> GetAllPaymentMethod()
        {
            Kaizen.Admin.Repository.Sys00020Repository _Sys00020Repository = new Kaizen.Admin.Repository.Sys00020Repository(UserContext.CompanyID.Trim(), UserContext.UserName , UserContext.Password);
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
        public CM00207 GetSingle(string PaymentID)
        {
            try
            {
                var tasks = _CM00207Repository.GetSingle(x => x.PaymentID == PaymentID);
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

        public IList<CM00207> GetAllByCaseRef(string CaseRef)
        {
            var tasks = _CM00207Repository.GetAll();
            IList<CM00207> result = tasks;
            return result;
        }
        public KaizenResult AddCM00207(CM00207 newTask, List<CM00204> CM00204)
        {
            KaizenResult result = new KaizenResult();
            result.Status = false;
            result.Massage = "Failed to Save Data";
            using (TransactionScope txScope = new TransactionScope())
            {
                newTask.CreatedBy = this.UserContext.UserName;
                newTask.CreatedDate = DateTime.Now;
                newTask.CM00204 = CM00204;
                result = _CM00207Repository.AddKaizenObject(newTask);
                if (result.Status)
                {
                    //CM00204Repository rep = new CM00204Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    //result = rep.AddKaizenObject(CM00204.ToArray());

                    //CM00700Repository repstatus = new CM00700Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    //List<CM00700> sttuslist = repstatus.GetAll(s => s.IsPaymentApply).ToList();

                    //foreach (CM00700 ststus in sttuslist)
                    //{
                    //    string where = " where" + ststus.RuleCondition + ";";
                    //    string sql = "update CM00203 set CaseStatusID=" + ststus.CaseStatusID.ToString();
                    //    if (ststus.IsCloseReminder)
                    //        sql += ",ReminderDateTime=null,PTPAMT=0";
                    //    sql += where;

                    //    sql += "insert into CM10701(CaseStatusID,CaseRef,AgentID,CreatedDate,ChangeStatusSourceID,ReminderDateTime,CaseStatusComment,PTPAMT)";
                    //    sql += "select "+ ststus.CaseStatusID.ToString() + " ,CaseRef ,'" + newTask.AgentID.Trim()
                    //        + "',getdate(),4 ,ReminderDateTime , '" + ststus.CaseStatusname + "' ,"
                    //        + newTask.Amount.ToString() + " from CM00203 ";
                    //    sql += where;
                    //    _CM00207Repository.ExecuteSqlCommand(sql);
                    //}
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
            KaizenResult result = _CM00207Repository.RemoveKaizenObject(xx => xx.PaymentID.Trim() == PaymentID.Trim());
            return result;
        }

        public KaizenResult AddCM00207(CM00207 newTask)
        {
            newTask.CreatedDate = DateTime.Now;
            newTask.CreatedBy = this.UserContext.UserName;
            newTask.IsApproved = false;
            KaizenResult result = _CM00207Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00207(IList<CM00207> newTask)
        {
            foreach (var item in newTask)
            {
                item.CreatedDate = DateTime.Now;
                item.CreatedBy = this.UserContext.UserName;
            }
            KaizenResult result = _CM00207Repository.AddKaizenObject(newTask.ToArray());
            //if (result.Status)
            //{
            //    Configuration.CompanyacessServices src = new Configuration.CompanyacessServices(UserContext);
            //    src.DeleteNextPaymentID();
            //}
            return result;
        }
        public KaizenResult Update(CM00207 newTask)
        {
            KaizenResult result = _CM00207Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult UpdatePost(CM00207 newTask)
        {
            KaizenResult result = _CM00207Repository.UpdateKaizenObject(newTask);
            string sql = "exec CMS_PaymentPost '"+ newTask.PaymentID.Trim() + "'";
            if(_CM00207Repository.ExecuteSqlCommand(sql) == 0)
            {
                result.Status = false;
                result.Massage = "Failed to Save Data";
            }
            return result;
        }
        public KaizenResult PostSingle(string PaymentID)
        {
            KaizenResult result = new KaizenResult();
            string sql = "exec CMS_PaymentPostSingle '" + PaymentID.Trim() + "','"+this.UserContext.UserName+"'";
            sql += " exec CMS_PaymentPostSingleReconcile '" + PaymentID.Trim() + "'";
            if (_CM00207Repository.ExecuteSqlCommand(sql) == 0)
            {
                result.Status = false;
                result.Massage = "Failed to Save Data";
            }
            return result;
        }
        public KaizenResult Update(IList<CM00207> newTask)
        {
            KaizenResult result = _CM00207Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(CM00207 newTask)
        {
            KaizenResult result = _CM00207Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00207> newTask)
        {
            KaizenResult result = _CM00207Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        public string GetNextPaymentID()
        {
            // CREATE SEQUENCE CMS_Payment_Sequence START WITH 1 INCREMENT BY 1
            string SequenceName = "CMS_Payment_Sequence";
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
                long? SequenceNumber = _CM00207Repository.GetSQLLong(SQL);
                if (SequenceNumber.HasValue)
                {
                    SQL = "insert into Sys00000(UserName,SequenceName,KaizenID) values (";
                    SQL += "'" + UserContext.UserName + "','" + SequenceName + "'," + SequenceNumber.ToString() + ");";
                    _CM00207Repository.ExecuteSqlCommand(SQL);
                    KaizenID = (int)SequenceNumber.Value;
                }
                else
                    return "";
            }
            if (UserContext.Screens == null)
                UserContext.Screens = new List<Kaizen.Configuration.Screen>();

            UserContext.Screens.Add(Kaizen.Configuration.Screen.CMS_Payment);
            Kaizen.Configuration.CM00000 tempp = Master.GetCMSConfiguration(this.UserContext.CompanyID);
            if (tempp.RCTLenth.HasValue)
            {
                int templenth = tempp.RCTLenth.Value - KaizenID.ToString().Length;
                string Str = string.Empty;
                for (byte i = 1; i <= templenth; i++)
                {
                    Str = Str + "0";
                }
                string RCTPrefix = string.Empty;
                if (!string.IsNullOrEmpty(tempp.RCTPrefix))
                    RCTPrefix = tempp.RCTPrefix;
                return RCTPrefix + Str + KaizenID.ToString();
            }
            return KaizenID.ToString();
        }
        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _CM00207Repository.ExecuteSqlCommand(myQuery);
            return result;
        }
    }
}
