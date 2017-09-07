using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00005Services
    {
        #region Infrastructure

        private Kaizen00005Repository _Kaizen00005Repository;
        private KaizenSession UserContext;
        public Kaizen00005Services(KaizenSession _Kaizen00005Context)
        {
            UserContext = _Kaizen00005Context;
            _Kaizen00005Repository = new Kaizen00005Repository(UserContext.UserName , UserContext.Password);
        }
        #endregion
        public DataCollection<Kaizen00005> GetAllBYSQLQuery(string Filters, string Searchcritaria, int PageSize, int Page, string Member, string SortDirection)
        {
            Kaizen00005Repository _Kaizen00005Repository = new Kaizen00005Repository(UserContext.UserName, UserContext.Password);
            DataCollection<Kaizen00005> result = null;
            if (!string.IsNullOrEmpty(Filters))
            {
                Page = Page - 1;
                if (!string.IsNullOrEmpty(Searchcritaria))
                {
                    Filters += " and TaskID like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and ClientName like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and ClientDescription like '%" + Searchcritaria.Trim() + "%'";
                }
                string sql = "select  * from Kaizen00005";
                sql += " where " + Filters + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
                var tasks = _Kaizen00005Repository.GetSQLData(sql);
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
                var tasks = _Kaizen00005Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            else
            {
                var tasks = _Kaizen00005Repository.GetListWithPaging(xx => xx.TaskDescription.Contains(Searchcritaria)
                    || xx.AsginFrom.Contains(Searchcritaria) || xx.TaskName.Contains(Searchcritaria)
                    , PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            return result;
        }
        public DataCollection<Kaizen00005> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Kaizen00005> result = null;
            var tasks = _Kaizen00005Repository.GetListWithPaging(PageSize, Page, ss => new { ss.TaskID });
            result = tasks;
            return result;
        }
        public DataCollection<Kaizen00005> GetTop6()
        {
            DataCollection<Kaizen00005> result = new DataCollection<Kaizen00005>();
            var tasks = _Kaizen00005Repository.GetAll();
            result.Items = tasks.ToList();

            return result;
        }
        public List<Kaizen00005> GetAll()
        {
            var tasks = _Kaizen00005Repository.GetAll();
            List<Kaizen00005> result = tasks.ToList();
            return result;
        }


    }
}
