using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{ 
    public class CM10110Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM10110Repository _CM10110Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM10110Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM10110Repository = new CM10110Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM10110> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("AgentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PERIODID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TargetAmount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM10110Repository rep = new CM10110Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM10110> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM10110", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM10110> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM10110> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM10110Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM10110Repository.GetWhereListWithPaging("CM10110", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<CM10110> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM10110Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM10110> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM10110Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM10110> result = tasks;
                return result;
            }

        }
        public DataCollection<CM10110> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM10110> result = null;
            var tasks = _CM10110Repository.GetListWithPaging(PageSize, Page, ss => new { ss.AgentID });
            result = tasks;
            return result;
        }
        public IList<CM10110> GetAll()
        {
            var tasks = _CM10110Repository.GetAll();
            IList<CM10110> result = tasks;
            return result;
        }
        public IList<CM10110> GetAll(string AgentID, string YearCode)
        {
            var tasks = _CM10110Repository.GetAll(ss=>ss.YearCode == YearCode && ss.AgentID == AgentID);
            IList<CM10110> result = tasks;
            return result;
        }
        public CM10110[] GetByAgent(string AgentID)
        {
            var tasks = _CM10110Repository.GetList(xx => xx.AgentID == AgentID, ss => AgentID,
                ss => AgentID);
            List<CM10110> result = new List<CM10110>();
            CM00007Repository _CM00007Repository = new CM00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            foreach (var item in tasks)
            {
                CM00007 CM00007 = new CM00007();
                CM00007 = _CM00007Repository.GetSingle(xx => xx.PERIODID == item.PERIODID);
                CM10110 obj = new CM10110()
                {
                    AgentID = item.AgentID,
                    PERIODID = item.PERIODID,
                    //TargetAmount = item.TargetAmount,
                    CM00007 = CM00007
                };
                result.Add(obj);
            }
            return result.ToArray();
        }

        public CM10110 GetSingle(int PERIODID, string AgentID)
        {
            var tasks = _CM10110Repository.GetSingle(x => x.AgentID == AgentID && x.PERIODID == PERIODID);
            return tasks;
        }

        public KaizenResult AddCM10110(CM10110 newTask)
        {
            KaizenResult result = _CM10110Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM10110(IList<CM10110> newTask)
        {
            KaizenResult result = _CM10110Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM10110 newTask)
        {
            KaizenResult result = _CM10110Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM10110> newTask)
        {
            KaizenResult result = _CM10110Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM10110 newTask)
        {
            KaizenResult result = _CM10110Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM10110> newTask)
        {
            KaizenResult result = _CM10110Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        public IList<CM_60610> GetCurrentTarget(string AgentID)
        {
            CM00007Services srv = new CM00007Services(this.UserContext);
            CM00007 oCM00007 = srv.GetCurrentPeriod();
            CM_60610Repository rep = new CM_60610Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(ss => ss.AgentID == AgentID
            && ss.CurrentPeriod == oCM00007.PERIODID);
            IList<CM_60610> result = tasks;
            return result;
        }

    }
}
