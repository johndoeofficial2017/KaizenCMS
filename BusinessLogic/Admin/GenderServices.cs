using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin.Repository;
using Kaizen.Admin;

namespace Kaizen.BusinessLogic.Admin
{
    public class Sys00011Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.Sys00011Repository _Sys00011Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Sys00011Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Sys00011Repository = new Sys00011Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<Sys00011> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("GenderID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GenderName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<Sys00011> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Sys00011Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Sys00011Repository.GetWhereListWithPaging("Sys00011", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<Sys00011> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Sys00011> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Sys00011Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Sys00011Repository.GetWhereListWithPaging("Sys00011", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Sys00011> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Sys00011> L = null;
            var tasks = _Sys00011Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<Sys00011> result = tasks;
            return result;
        }
        public IList<Sys00011> GetAll()
        {
            var tasks = _Sys00011Repository.GetAll();
            IList<Sys00011> result = tasks;
            return result;
        }

        public Sys00011 GetSingle(int GenderID)
        {
            var tasks = _Sys00011Repository.GetSingle(x => x.GenderID == GenderID);
            return tasks;
        }

        public KaizenResult AddSys00011(Sys00011 newTask)
        {
            var result = _Sys00011Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSys00011(IList<Sys00011> newTask)
        {
            var result = _Sys00011Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Sys00011 newTask)
        {
            var result = _Sys00011Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Sys00011> newTask)
        {
            var result = _Sys00011Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Sys00011 newTask)
        {
            var result = _Sys00011Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Sys00011> newTask)
        {
            var result = _Sys00011Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
