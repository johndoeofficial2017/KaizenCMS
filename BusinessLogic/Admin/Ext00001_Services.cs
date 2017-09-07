using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.Configuration;
using Kaizen.Configuration.Repository;

namespace Kaizen.BusinessLogic.Admin
{
    public class Ext00001Services
    {
        #region Infrastructure

        private Kaizen.Configuration.Repository.Ext00001Repository _Ext00001Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Ext00001Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Ext00001Repository = new Ext00001Repository(UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<Ext00001> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Ext00001> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Ext00001Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Ext00001Repository.GetWhereListWithPaging("Ext00001", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Ext00001> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<Ext00001> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _Ext00001Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _Ext00001Repository.GetWhereListWithPaging("Ext00001", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<Ext00001> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Ext00001> L = null;
            var tasks = _Ext00001Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<Ext00001> result = tasks;
            return result;
        }
        public DataCollection<Ext00001> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Ext00001> result = null;
            var tasks = _Ext00001Repository.GetListWithPaging(PageSize, Page, ss => new { ss.DataBaseSourceID });
            result = tasks;
            return result;
        }
        public IList<Ext00001> GetAll()
        {
            var tasks = _Ext00001Repository.GetAll();
            IList<Ext00001> result = tasks;
            return result;
        }
        public Ext00001 GetSingle(int DataBaseSourceID)
        {
            var tasks = _Ext00001Repository.GetSingle(x => x.DataBaseSourceID == DataBaseSourceID);
            return tasks;
        }
        public KaizenResult AddExt00001(Ext00001 newTask)
        {
            var result = _Ext00001Repository.AddKaizenObject(newTask);
            return result;
        }

        public KaizenResult AddExt00001(IList<Ext00001> newTask)
        {
            var result = _Ext00001Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(Ext00001 newTask)
        {
            var result = _Ext00001Repository.UpdateKaizenObject(newTask);
            return result;
        }

        public KaizenResult Update(IList<Ext00001> newTask)
        {
            var result = _Ext00001Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Ext00001 newTask)
        {
            var result = _Ext00001Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Ext00001> newTask)
        {
            var result = _Ext00001Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
