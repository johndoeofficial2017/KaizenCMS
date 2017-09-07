using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00202Services
    {
        #region Infrastructure

        private CM00202Repository _CM00202Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00202Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00202Repository = new CM00202Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00202> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00202> result = null;
            result = _CM00202Repository.GetWhereListWithPaging("CM00202", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00202> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("BucketID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00202> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00202Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00202Repository.GetWhereListWithPaging("CM00202", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }


        public DataCollection<CM00202> GetAllCM00202(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<CM00202> L = null;
                var tasks = _CM00202Repository.GetListWithPaging(x => (x.UserName.Contains(SearchTerm)
                    ), PageSize, Page, ss => ss.AssigmentID, null);

                DataCollection<CM00202> result = tasks;
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
        public IList<CM00202> GetAll()
        {
            var tasks = _CM00202Repository.GetAll();
            IList<CM00202> result = tasks;
            return result;
        }

        public CM00202 GetSingle(string AssigmentID)
        {
            var tasks = _CM00202Repository.GetSingle(x => x.AssigmentID == AssigmentID);
            return tasks;
        }

        public KaizenResult AddCM00202(CM00202 newTask)
        {
            var result = _CM00202Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00202(CM00202 newTask, IList<CM00206> L)
        {
            foreach (CM00206 o in L)
            {
                o.AssigmentID = newTask.AssigmentID;
                o.AgentID = newTask.AgentID;
                o.AssignHistoryDate = newTask.AssignDate.Value;
            }
            newTask.UserName = UserContext.UserName;
            var result = _CM00202Repository.AddKaizenObject(newTask);
            if (result.Status)
            {
                CM00206Repository rep = new CM00206Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                result = rep.AddKaizenObject(L.ToArray());
                UserContext.Screens.Remove(Kaizen.Configuration.Screen.CMS_Trx_CasesAssignment);
            }
            return result;
        }

        public KaizenResult AddCM00202(IList<CM00202> newTask)
        {
            var result = _CM00202Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00202 newTask)
        {
            var result = _CM00202Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00202> newTask)
        {
            var result = _CM00202Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00202 newTask)
        {
            var result = _CM00202Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00202> newTask)
        {
            var result = _CM00202Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        public string GetNextTransactionID()
        {
            // CREATE SEQUENCE CMS_CasesAssignment_Sequence START WITH 1 INCREMENT BY 1
            string SequenceName = "CMS_CasesAssignment_Sequence";
            int KaizenID = 0;
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
                        _CM00202Repository.ExecuteSqlCommand(SQL);
                        KaizenID = row.KaizenID;
                        break;
                    }
                    bool notUse = true;
                    foreach (Kaizen.Configuration.Screen scrn in oKaizenSession.Screens)
                    {
                        if (scrn == Kaizen.Configuration.Screen.CMS_Trx_CasesAssignment)
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
                long? SequenceNumber = _CM00202Repository.GetSQLLong(SQL);
                if (SequenceNumber.HasValue)
                {
                    SQL = "insert into Sys00000(UserName,SequenceName,KaizenID) values (";
                    SQL += "'" + UserContext.UserName + "','" + SequenceName + "'," + SequenceNumber.ToString() + ");";
                    _CM00202Repository.ExecuteSqlCommand(SQL);
                    KaizenID = (int)SequenceNumber.Value;
                }
                else
                    return "";
            }
            if (UserContext.Screens == null)
                UserContext.Screens = new List<Kaizen.Configuration.Screen>();

            UserContext.Screens.Add(Kaizen.Configuration.Screen.CMS_Payment);
            //CM00000Repository reptemp = new CM00000Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            //IList<CM00000> obj = reptemp.GetAll();
            //CM00000 tempp = Master.CMSConfiguration;
            int templenth = 0;
            //if (tempp.RCTLenth.HasValue)
            //    templenth = tempp.RCTLenth.Value;
            //if (templenth > 0)
            //    templenth = templenth - KaizenID.ToString().Length;
            //else
            templenth =5- KaizenID.ToString().Length;
            string Str = string.Empty;
            for (byte i = 1; i <= templenth; i++)
            {
                Str = Str + "0";
            }
            return Str + KaizenID.ToString();
        }
    }
}
