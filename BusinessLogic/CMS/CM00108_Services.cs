using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00108Services
    {
        #region Infrastructure

        private CM00108Repository _CM00108Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00108Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00108Repository = new CM00108Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00108> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00108> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00108Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00108Repository.GetWhereListWithPaging("CM00108", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00108> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TargetID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TargetName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00108> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00108Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00108Repository.GetWhereListWithPaging("CM00108", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<CM00108> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00108> L = null;
            var tasks = _CM00108Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00108> result = tasks;
            return result;
        }
        public DataCollection<CM00108> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00108> result = null;
            var tasks = _CM00108Repository.GetListWithPaging(PageSize, Page, ss => new { ss.TargetID });
            result = tasks;
            return result;
        }

        public IList<CM00108> GetAll()
        {
            var tasks = _CM00108Repository.GetAll();
            IList<CM00108> result = tasks;
            return result;
        }
        public IList<CM00058> GetAllCM00058()
        {
            CM00058Repository rep = new CM00058Repository(this.UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll();
            IList<CM00058> result = tasks;
            return result;
        }
        public CM00108 GetSingle(string TargetID)
        {
            var tasks = _CM00108Repository.GetSingle(x => x.TargetID == TargetID);
            return tasks;
        }

        public KaizenResult AddCM00108(CM00108 newTask)
        {
            var result = _CM00108Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00108(IList<CM00108> newTask)
        {
            var result = _CM00108Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00108 newTask)
        {
            var result = _CM00108Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00108> newTask)
        {
            var result = _CM00108Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00108 newTask)
        {
            var result = _CM00108Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00108> newTask)
        {
            var result = _CM00108Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
