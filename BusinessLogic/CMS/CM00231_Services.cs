using System.Collections.Generic;
using System.Linq;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
namespace Kaizen.BusinessLogic.CMS
{
    public class CM00231Services
    {
        #region Infrastructure

        private CM00231Repository _CM00231Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00231Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00231Repository = new CM00231Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00231> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("MessageTRXID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TableID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseRef", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsSent", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00231> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00231Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00231Repository.GetWhereListWithPaging("CM00231", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00231> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00231> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00231Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00231Repository.GetWhereListWithPaging("CM00231", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00231> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00231> L = null;
            var tasks = _CM00231Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00231> result = tasks;
            return result;
        }
        public DataCollection<CM00231> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00231> result = null;
            var tasks = _CM00231Repository.GetListWithPaging(PageSize, Page, ss => new { ss.MessageTRXID });
            result = tasks;
            return result;
        }
        public IList<CM00231> GetAll()
        {
            var tasks = _CM00231Repository.GetAll();
            IList<CM00231> result = tasks;
            return result;
        }
        public CM00231 GetSingle(int TableID)
        {
            var tasks = _CM00231Repository.GetSingle(x => x.TableID == TableID);
            return tasks;
        }

        public KaizenResult AddCM00231(CM00231 newTask)
        {
            var result = _CM00231Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00231(IList<CM00231> newTask)
        {
            var result = _CM00231Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00231 newTask)
        {
            var result = _CM00231Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00231> newTask)
        {
            var result = _CM00231Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00231 newTask)
        {
            var result = _CM00231Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00231> newTask)
        {
            var result = _CM00231Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}

