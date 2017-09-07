using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00212Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00212Repository _CM00212Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00212Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00212Repository = new CM00212Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00212> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TaskID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TaskTitle", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00212Repository rep = new CM00212Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00212> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00212", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00212> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00212> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00212Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00212Repository.GetWhereListWithPaging("CM00212", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00212> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM00212Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00212> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM00212Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00212> result = tasks;
                return result;
            }

        }
        public IList<CM00212> GetAll()
        {
            var tasks = _CM00212Repository.GetAll();
            IList<CM00212> result = tasks;
            return result;
        }
        public IList<CM00212> GetAgentTarget(string AgentID, string ClientID)
        {
            var tasks = _CM00212Repository.GetAll(xx => xx.AgentID == AgentID
            && xx.ClientID == ClientID);
            return tasks;
        }
        public List<CM00007> GetAgentClientTargets(string AgentID, string ClientID, string YearCode, int CalendarID)
        {
            CM00007Repository rep = new CM00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            List<CM00007> CM00007list = rep.GetAll(xx => xx.CalendarID == CalendarID
           && xx.YearCode == YearCode).ToList();
            string sql = "select * from CM10110 where PERIODID in(";
            foreach (var item in CM00007list)
            {
                sql += item.PERIODID.ToString() + ",";
            }
            sql += sql.Substring(sql.Length - 2, 1);
            sql += ")";
            CM10110Repository rep10110 = new CM10110Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var data = rep10110.GetSQLData(sql);
            List<CM10110> CM10110lIst = data == null ? null : data.Items;
            // Loop 
            sql = sql.Replace("CM10110", "CM00212");
            sql += "and AgentID='" + AgentID.Trim() + "'";
            sql += "and ClientID <>'" + ClientID.Trim() + "'";
            var data2 = _CM00212Repository.GetSQLData(sql);
            List<CM00212> result212 = data2 == null ? null : data2.Items;

            foreach (CM00007 row in CM00007list)
            {
                if (CM10110lIst != null)
                {
                    var temp = CM10110lIst.Find(ss => ss.PERIODID == row.PERIODID);
                    //row.TotalTarget += temp.TargetAmount;
                }
                if (result212 != null)
                {
                    var temp2 = result212.FindAll(ss => ss.PERIODID == row.PERIODID);
                    foreach (CM00212 tt in temp2)
                    {
                        row.BalanceTarget += tt.TargetAmount.Value ;
                    }
                    row.BalanceTarget = row.TotalTarget - row.BalanceTarget;
                }

            }
            return CM00007list;
        }
        public CM00212[] GetAgentBalanceAmount(string AgentID, int PERIODID, string ClientID)
        {
            CM10110Repository _CM10110Repository = new CM10110Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

            var tasks = _CM00212Repository.GetAll(xx => xx.AgentID == AgentID
            && xx.PERIODID == PERIODID && xx.ClientID != ClientID, ss => ClientID);
            var AgentTargets = _CM10110Repository.GetAll(xx => xx.AgentID == AgentID, ss => AgentID);
            List<CM00212> result = new List<CM00212>();
            double Balance = 0;
            foreach (var item in tasks)
            {
                Balance += item.TargetAmount.Value;
            }
            return result.ToArray();
        }

        public KaizenResult AddCM00212(CM00212 newTask)
        {
            KaizenResult result = _CM00212Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00212(IList<CM00212> newTask)
        {
            CM00212 first = newTask.FirstOrDefault();
            CM10110Services srv = new CM10110Services(this.UserContext);
            IList<CM10110> ss = srv.GetAll(first.AgentID, first.YearCode);
            if (ss.Count == 0)
            {
                foreach (CM00212 row in newTask)
                {
                    CM10110 o = new CM10110();
                    o.YearCode = row.YearCode;
                    o.AgentID = row.AgentID;
                    o.PERIODID = row.PERIODID;
                    srv.AddCM10110(o);
                }
            }
            KaizenResult result = null;
            switch (first.Status)
            {
                case 0:
                    result = _CM00212Repository.AddKaizenObject(newTask.ToArray());
                    break;
                case 1:
                    result = _CM00212Repository.UpdateKaizenObject(newTask.ToArray());
                    break;
            }
            return result;

        }
        public KaizenResult Update(CM00212 newTask)
        {
            KaizenResult result = _CM00212Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00212> newTask)
        {
            KaizenResult result = _CM00212Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(IList<CM00212> newTask)
        {
            KaizenResult result = _CM00212Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
