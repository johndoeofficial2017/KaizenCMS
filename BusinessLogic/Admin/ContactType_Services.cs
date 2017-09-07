using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin.Repository;
using Kaizen.Admin;
namespace Kaizen.BusinessLogic.Admin
{
    public class Sys00006Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.Sys00006Repository _Sys00006Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Sys00006Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Sys00006Repository = new Sys00006Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<Sys00006> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ContactTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContactTypeName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<Sys00006> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Sys00006Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Sys00006Repository.GetWhereListWithPaging("Sys00006", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<Sys00006> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Sys00006> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Sys00006Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Sys00006Repository.GetWhereListWithPaging("Sys00006", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Sys00006> GetAllViewBYSQLQuery(string Filters,int ContactSourceID, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Sys00006> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Sys00006Repository.GetListWithPaging(ss => ss.ContactSourceID == ContactSourceID, PageSize, Page, ss => Member);
            else
                result = _Sys00006Repository.GetWhereListWithPaging("Sys00006", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Sys00006> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Sys00006> L = null;
            var tasks = _Sys00006Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<Sys00006> result = tasks;
            return result;
        }
        public DataCollection<Sys00006> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Sys00006> result = null;
            var tasks = _Sys00006Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ContactTypeID });
            result = tasks;
            return result;
        }
        public DataCollection<Sys00006> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Sys00006> result = null;
            var tasks = _Sys00006Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ContactTypeID });
            result = tasks;
            return result;
        }

        public IList<Sys00006> GetAllBySource(int ContactSourceID)
        {
            var tasks = _Sys00006Repository.GetAll(ss => ss.ContactSourceID == ContactSourceID);
            return tasks;
        }
        public IList<Kaizen.SOP.Sys00006> GetAllSys00006(int ContactSourceID)
        {
            Kaizen.SOP.Repository.Sys00006Repository rep = new Kaizen.SOP.Repository.Sys00006Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(ss => ss.ContactSourceID == ContactSourceID);
            IList<Kaizen.SOP.Sys00006> result = tasks;
            return result;
        }

        public IList<Sys00006> GetAll(int ContactSourceID)
        {
            var tasks = _Sys00006Repository.GetAll(ss => ss.ContactSourceID == ContactSourceID);
            return tasks;
        }

        public Sys00006 GetSingle(int ContactTypeID)
        {
            var tasks = _Sys00006Repository.GetSingle(x => x.ContactTypeID == ContactTypeID);
            return tasks;
        }

        public KaizenResult AddSys00006(Sys00006 newTask)
        {
            KaizenResult result = _Sys00006Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(Sys00006 newTask)
        {
            KaizenResult result = _Sys00006Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(Sys00006 newTask)
        {
            KaizenResult result = _Sys00006Repository.DeleteKaizenObject(newTask);
            return result;
        }
    }
}
