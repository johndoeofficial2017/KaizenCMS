using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00023Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00023Repository _CM00023Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00023Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00023Repository = new CM00023Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00023> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00023> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00023Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00023Repository.GetWhereListWithPaging("CM00023", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00023> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ActionTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00023> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00023Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00023Repository.GetWhereListWithPaging("CM00023", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<CM00023> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00023> L = null;
            var tasks = _CM00023Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00023> result = tasks;
            return result;
        }
        public DataCollection<CM00023> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00023> result = null;
            var tasks = _CM00023Repository.GetListWithPaging(PageSize, Page, ss => new { ss.AgentGroup });
            result = tasks;
            return result;
        }
        public IList<CM00023> GetAll()
        {
            var tasks = _CM00023Repository.GetAll();
            IList<CM00023> result = tasks;
            return result;
        }
        public CM00023 GetSingle(string AgentGroup)
        {
            var tasks = _CM00023Repository.GetSingle(x => x.AgentGroup == AgentGroup);
            return tasks;
        }
        public KaizenResult AddCM00023(CM00023 newTask)
        {
            var result = _CM00023Repository.AddKaizenObject(newTask);
           
            if (Master.GetCMSConfiguration(this.UserContext.CompanyID).IsAgentSerial)
                _CM00023Repository.ExecuteSqlCommand("CREATE SEQUENCE CMS_Agent_" + newTask.AgentGroup.Trim() + "_Sequence START WITH 1 INCREMENT BY 1");
            return result;
        }
        //public KaizenResult AddCM00023(IList<CM00023> newTask)
        //{
        //    var result = _CM00023Repository.AddKaizenObject(newTask.ToArray());
        //    return result;
        //}

        public KaizenResult Update(CM00023 newTask)
        {
            var result = _CM00023Repository.UpdateKaizenObject(newTask);
            return result;
        }
        //public KaizenResult Update(IList<CM00023> newTask)
        //{
        //    var result = _CM00023Repository.UpdateKaizenObject(newTask.ToArray());
        //    return result;
        //}

        public KaizenResult Delete(CM00023 newTask)
        {
            var result = _CM00023Repository.DeleteKaizenObject(newTask);
            if (Master.GetCMSConfiguration(this.UserContext.CompanyID).IsAgentSerial)
                _CM00023Repository.ExecuteSqlCommand("drop SEQUENCE CMS_Agent_" + newTask.AgentGroup.Trim() + "_Sequence");
            return result;
        }
        //public KaizenResult Delete(IList<CM00023> newTask)
        //{
        //    var result = _CM00023Repository.DeleteKaizenObject(newTask.ToArray());
        //    return result;
        //}

    }
}
