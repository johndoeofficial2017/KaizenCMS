using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;


namespace Kaizen.BusinessLogic.CMS
{
    public class CM00021Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00021Repository _CM00021Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00021Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00021Repository = new CM00021Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00021> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00021> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00021Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00021Repository.GetWhereListWithPaging("CM00021", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00021> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<CM00021> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00021Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00021Repository.GetWhereListWithPaging("CM00021", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<CM00021> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00021> L = null;
            var tasks = _CM00021Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00021> result = tasks;
            return result;
        }
        public DataCollection<CM00021> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00021> result = null;
            var tasks = _CM00021Repository.GetListWithPaging(PageSize, Page, ss => new { ss.CUSTCLAS });
            result = tasks;
            return result;
        }

        public IList<CM00021> GetAll()
        {
            var tasks = _CM00021Repository.GetAll();
            IList<CM00021> result = tasks;
            return result;
        }

        public CM00021 GetSingle(string CUSTCLAS)
        {
            var tasks = _CM00021Repository.GetSingle(x => x.CUSTCLAS == CUSTCLAS);
            return tasks;
        }

        public KaizenResult AddCM00021(CM00021 newTask)
        {
            var result = _CM00021Repository.AddKaizenObject(newTask);
            Kaizen.BusinessLogic.Configuration.CompanyacessServices srv = new Configuration.CompanyacessServices(UserContext);
            srv.SetUpNextClientID(newTask.CUSTCLAS);
            return result;
        }
        public KaizenResult AddCM00021(IList<CM00021> newTask)
        {
            var result = _CM00021Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00021 newTask)
        {
            var result = _CM00021Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00021> newTask)
        {
            var result = _CM00021Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00021 newTask)
        {
            var result = _CM00021Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00021> newTask)
        {
            var result = _CM00021Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _CM00021Repository.ExecuteSqlCommand(myQuery);
            return result;
        }
    }
}
