using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Configuration;
using Kaizen.Configuration.Repository;

namespace Kaizen.BusinessLogic.Configuration
{
    public class CM00000Services
    {
        #region Infrastructure

        private CM00000Repository _CM00000Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00000Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00000Repository = new CM00000Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        public CM00000Services()
        {
            _CM00000Repository = new CM00000Repository();
        }
        #endregion
        public DataCollection<CM00000> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("SetupID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("RCTPrefix", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("RCTLenth", (KaizenFilterOperator)FltrOperator, Searchcritaria);

                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00000Repository rep = new CM00000Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00000> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00000", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00000> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00000> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00000Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00000Repository.GetWhereListWithPaging("CM00000", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<CM00000> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM00000Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00000> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM00000Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00000> result = tasks;
                return result;
            }

        }
        public DataCollection<CM00000> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00000> result = null;
            var tasks = _CM00000Repository.GetListWithPaging(PageSize, Page, ss => new { ss.CompanyID });
            result = tasks;
            return result;
        }
        public IList<CM00000> GetAll()
        {
            var tasks = _CM00000Repository.GetAllFromKaizen();
            IList<CM00000> result = tasks;
            return result;
        }

        public CM00000 GetSingle(string CompanyID)
        {
            var tasks = _CM00000Repository.GetSingleFromKaizen(x => x.CompanyID == CompanyID);
            return tasks;
        }

        public KaizenResult AddCM00000(CM00000 newTask)
        {
            var result = _CM00000Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00000(IList<CM00000> newTask)
        {
            var result = _CM00000Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00000 newTask)
        {
            var result = _CM00000Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00000> newTask)
        {
            var result = _CM00000Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00000 newTask)
        {
            var result = _CM00000Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00000> newTask)
        {
            var result = _CM00000Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

       
    }
}
