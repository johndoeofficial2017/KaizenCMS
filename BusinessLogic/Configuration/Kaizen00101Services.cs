using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00101Services
    {
        #region Infrastructure

        private Kaizen00101Repository _Kaizen00101Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Kaizen00101Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
              
            }
            UserContext = _UserContext;
            _Kaizen00101Repository = new Kaizen00101Repository(UserContext.UserName, UserContext.Password);
        }
        #endregion
        public DataCollection<Kaizen00101> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Kaizen00101> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen00101Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Kaizen00101Repository.GetWhereListWithPaging("Kaizen00101", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00101> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CompanyID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ModuleID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ModuleName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<Kaizen00101> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen00101Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Kaizen00101Repository.GetWhereListWithPaging("Kaizen00101", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00101> GetAllBYSQLQuery(string Filters, string Searchcritaria, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Kaizen00101> result = null;
            if (!string.IsNullOrEmpty(Filters))
            {
                Page = Page - 1;
                if (!string.IsNullOrEmpty(Searchcritaria))
                {
                    Filters += " and ModuleID like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and ModuleName like '%" + Searchcritaria.Trim() + "%'";
                }
                string sql = "select  * from Kaizen00101 ";
                sql += " where " + Filters + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
                var tasks = _Kaizen00101Repository.GetSQLData(sql);
                if (tasks != null)
                {
                    tasks.TotalItemCount = tasks.Items.Count;
                    tasks.TotalPageCount = (int)Math.Ceiling((double)tasks.TotalItemCount / (double)PageSize);
                }
                result = tasks;
                return result;
            }
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                var tasks = _Kaizen00101Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            else
            {
                var tasks = _Kaizen00101Repository.GetListWithPaging(xx => xx.ModuleID.ToString().Contains(Searchcritaria)
                    , PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            return result;
        }

        public List<Kaizen00101> GetAll()
        {
            var tasks = _Kaizen00101Repository.GetAll();
            List<Kaizen00101> result = tasks.ToList();
            return result;
        }
        public Kaizen00101 GetSingle(int ModuleID)
        {
            var tasks = _Kaizen00101Repository.GetSingle(x => x.ModuleID == ModuleID);
            return tasks;
        }
        public List<Kaizen00101> GetAllByCompany(string CompanyID)
        {
            var tasks = _Kaizen00101Repository.GetAll(ss => ss.CompanyID == CompanyID);
            List<Kaizen00101> result = tasks.ToList();
            return result;
        }

        public KaizenResult AddKaizen00101(Kaizen00101 newTask)
        {
            var result = _Kaizen00101Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizen00101(IList<Kaizen00101> newTask)
        {
            var result = _Kaizen00101Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Kaizen00101 newTask)
        {
            var result = _Kaizen00101Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Kaizen00101> newTask)
        {
            var result = _Kaizen00101Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Kaizen00101 newTask)
        {
            var result = _Kaizen00101Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Kaizen00101> newTask)
        {
            var result = _Kaizen00101Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        //public int ExecuteSqlCommand(string myQuery)
        //{
        //    var result = _Kaizen00101Repository.ExecuteSqlCommand(myQuery);
        //    return result;
        //}
    }
}
