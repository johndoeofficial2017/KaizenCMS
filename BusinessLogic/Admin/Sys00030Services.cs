using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin.Repository;
using Kaizen.Admin;

namespace Kaizen.BusinessLogic.Admin
{
    public class Sys00030Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.Sys00030Repository _Sys00030Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Sys00030Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Sys00030Repository = new Sys00030Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<Sys00030> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("NationalityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("NationalityName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }
            Sys00030Repository rep = new Sys00030Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<Sys00030> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("Sys00030", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Sys00030> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Sys00030> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Sys00030Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Sys00030Repository.GetWhereListWithPaging("Sys00030", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Sys00030> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Sys00030> L = null;
            var tasks = _Sys00030Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<Sys00030> result = tasks;
            return result;
        }
        public DataCollection<Sys00030> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Sys00030> result = null;
            var tasks = _Sys00030Repository.GetListWithPaging(PageSize, Page, ss => new { ss.NationalityID });
            result = tasks;
            return result;
        }

        public IList<Sys00030> GetAll()
        {
            var tasks = _Sys00030Repository.GetAll();
            IList<Sys00030> result = tasks;
            return result;
        }

        public Sys00030 GetSingle(string NationalityID)
        {
            var tasks = _Sys00030Repository.GetSingle(x => x.NationalityID == NationalityID);
            return tasks;
        }
  
        public KaizenResult AddSys00030(Sys00030 newTask)
        {
            var result=_Sys00030Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(Sys00030 newTask)
        {
            var result = _Sys00030Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(Sys00030 newTask)
        {
            var result = _Sys00030Repository.DeleteKaizenObject(newTask);
            return result;
        }
    }
}
