using System.Collections.Generic;
using System.Linq;
using Kaizen.Configuration.Repository;
using Kaizen.Configuration;
namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00014Services
    {
        #region Infrastructure

        private Kaizen00014Repository _Kaizen00014Repository;
        private KaizenSession UserContext;
        public Kaizen00014Services(KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Kaizen00014Repository = new Kaizen00014Repository(UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<Kaizen00014> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ConditionID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ViewID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldValue", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldCondition", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldOperatorAnd", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            Kaizen00014Repository rep = new Kaizen00014Repository(UserContext.UserName, UserContext.Password);
            DataCollection<Kaizen00014> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("Kaizen00014", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00014> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Kaizen00014> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen00014Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Kaizen00014Repository.GetWhereListWithPaging("Kaizen00014", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00014> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, int ViewID,
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
                    SeachStr += Help.GetFilter("ConditionID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ViewID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldValue", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldCondition", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FieldOperatorAnd", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<Kaizen00014> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen00014Repository.GetListWithPaging(ss => ss.ViewID == ViewID, PageSize, Page, ss => Member);
            else
                result = _Kaizen00014Repository.GetWhereListWithPaging("Kaizen00014", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00014> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Kaizen00014> L = null;
            var tasks = _Kaizen00014Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<Kaizen00014> result = tasks;
            return result;
        }
        public DataCollection<Kaizen00014> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Kaizen00014> result = null;
            var tasks = _Kaizen00014Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ConditionID });
            result = tasks;
            return result;
        }
        public IList<Kaizen00014> GetAll()
        {
            var tasks = _Kaizen00014Repository.GetAll();
            IList<Kaizen00014> result = tasks;
            return result;
        }
        public IList<Kaizen00014> GetByViewID(int ViewID)
        {
            var tasks = _Kaizen00014Repository.GetAll(ss => ss.ViewID == ViewID);
            IList<Kaizen00014> result = tasks;
            return result;
        }

        public Kaizen00014 GetSingle(int ConditionID)
        {
            var tasks = _Kaizen00014Repository.GetSingle(x => x.ConditionID == ConditionID);
            return tasks;
        }

        public KaizenResult AddKaizen00014(Kaizen00014 newTask)
        {
            var result = _Kaizen00014Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizen00014(IList<Kaizen00014> newTask)
        {
            var result = _Kaizen00014Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(Kaizen00014 newTask)
        {
            var result = _Kaizen00014Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Kaizen00014> newTask)
        {
            var result = _Kaizen00014Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Kaizen00014 newTask)
        {
            var result = _Kaizen00014Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Kaizen00014> newTask)
        {
            var result = _Kaizen00014Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}

