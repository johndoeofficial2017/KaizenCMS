using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin.Repository;
using Kaizen.Admin;
namespace Kaizen.BusinessLogic.Admin
{
    public class Sys00007Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.Sys00007Repository _Sys00007Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Sys00007Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Sys00007Repository = new Sys00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<Sys00007> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("DocumentTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DocumentTypeName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<Sys00007> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Sys00007Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Sys00007Repository.GetWhereListWithPaging("Sys00007", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<Sys00007> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Sys00007> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Sys00007Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Sys00007Repository.GetWhereListWithPaging("Sys00007", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<Sys00007> GetAllViewBYSQLQuery(string Filters, int ContactSourceID, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Sys00007> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Sys00007Repository.GetListWithPaging(ss => ss.ContactSourceID == ContactSourceID, PageSize, Page, ss => Member);
            else
                result = _Sys00007Repository.GetWhereListWithPaging("Sys00007", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Sys00007> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Sys00007> L = null;
            var tasks = _Sys00007Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<Sys00007> result = tasks;
            return result;
        }
        public DataCollection<Sys00007> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Sys00007> result = null;
            var tasks = _Sys00007Repository.GetListWithPaging(PageSize, Page, ss => new { ss.DocumentTypeID });
            result = tasks;
            return result;
        }
        public DataCollection<Sys00007> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Sys00007> result = null;
            var tasks = _Sys00007Repository.GetListWithPaging(PageSize, Page, ss => new { ss.DocumentTypeID });
            result = tasks;
            return result;
        }

        public IList<Sys00007> GetAll(int ContactSourceID)
        {
            
            var tasks = _Sys00007Repository.GetAll(ss => ss.ContactSourceID == ContactSourceID);
            return tasks;
        }
        public Sys00007 GetSingle(int DocumentTypeID)
        {
            var tasks = _Sys00007Repository.GetSingle(x => x.DocumentTypeID == DocumentTypeID);
            return tasks;
        }

        public KaizenResult AddSys00007(Sys00007 newTask)
        {
            KaizenResult result = _Sys00007Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(Sys00007 newTask)
        {
            KaizenResult result = _Sys00007Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(Sys00007 newTask)
        {
            KaizenResult result = _Sys00007Repository.DeleteKaizenObject(newTask);
            return result;
        }
    }
}
