using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00107Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00107Repository _CM00107Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00107Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00107Repository = new CM00107Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00107> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("BudgetAmount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AchieveAmount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00107Repository rep = new CM00107Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00107> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00107", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00107> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00107> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00107Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00107Repository.GetWhereListWithPaging("CM00107", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<CM00107> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM00107Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00107> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM00107Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00107> result = tasks;
                return result;
            }

        }
        public DataCollection<CM00107> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00107> result = null;
            var tasks = _CM00107Repository.GetListWithPaging(PageSize, Page, ss => new { ss.AgentID });
            result = tasks;
            return result;
        }
        public IList<CM00107> GetAll()
        {
            var tasks = _CM00107Repository.GetAll();
            IList<CM00107> result = tasks;
            return result;
        }
        public IList<CM00107> GetByAgent(string AgentID)
        {
            var tasks = _CM00107Repository.GetAll(xx => xx.AgentID == AgentID);
            IList<CM00107> result = tasks;
            return result;
        }
        public CM00107 GetSingle(int PERIODID, string AgentID)
        {
            var tasks = _CM00107Repository.GetSingle(x => x.AgentID == AgentID && x.PERIODID == PERIODID);
            return tasks;
        }
        public CM00107[] GetAgentTarget(string AgentID, string TargetID)
        {
            var tasks = _CM00107Repository.GetList(xx => xx.AgentID == AgentID && xx.TargetID == TargetID, ss => AgentID,
                ss => AgentID);
            //List<CM00107> result = new List<CM00107>();
            //CM00007Repository _CM00007Repository = new CM00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            //foreach (var item in tasks)
            //{
            //    CM00007 CM00007 = new CM00007();
            //    CM00007 = _CM00007Repository.GetSingle(xx => xx.PERIODID == item.PERIODID);
            //    CM00107 obj = new CM00107()
            //    {
            //        AgentID = item.AgentID,
            //        PERIODID = item.PERIODID,
            //        TargetID=item.TargetID,
            //        //TargetAmount = item.TargetAmount
            //    };
            //    result.Add(obj);
            //}
            return tasks.ToArray();
        }

        public KaizenResult AddCM00107(CM00107 newTask)
        {
            KaizenResult result = _CM00107Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00107(IList<CM00107> newTask)
        {
            KaizenResult result = _CM00107Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(CM00107 newTask)
        {
            KaizenResult result = _CM00107Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00107> newTask)
        {
            KaizenResult result = _CM00107Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00107 newTask)
        {
            KaizenResult result = _CM00107Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00107> newTask)
        {
            KaizenResult result = _CM00107Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
