using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00030Services
    {
        #region Infrastructure

        private Kaizen00030Repository _Kaizen00030Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Kaizen00030Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
                
            }
            UserContext = _UserContext;
            _Kaizen00030Repository = new Kaizen00030Repository(UserContext.UserName, UserContext.Password);
        }
        #endregion
        public DataCollection<Kaizen00030> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Kaizen00030> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen00030Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Kaizen00030Repository.GetWhereListWithPaging("Kaizen00030", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00030> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("RoleID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("RoleName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<Kaizen00030> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen00030Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Kaizen00030Repository.GetWhereListWithPaging("Kaizen00030", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00030> GetAllBYSQLQuery(string Filters, string Searchcritaria, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Kaizen00030> result = null;
            if (!string.IsNullOrEmpty(Filters))
            {
                Page = Page - 1;
                if (!string.IsNullOrEmpty(Searchcritaria))
                {
                    Filters += " and RoleID like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and RoleName like '%" + Searchcritaria.Trim() + "%'";
                }
                string sql = "select  * from Kaizen00030 ";
                sql += " where " + Filters + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
                var tasks = _Kaizen00030Repository.GetSQLData(sql);
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
                var tasks = _Kaizen00030Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            else
            {
                var tasks = _Kaizen00030Repository.GetListWithPaging(xx => xx.RoleID.ToString().Contains(Searchcritaria)
                    || xx.RoleName.Contains(Searchcritaria), PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            return result;
        }

        public List<Kaizen00030> GetAll()
        {
            var tasks = _Kaizen00030Repository.GetAll();
            List<Kaizen00030> result = tasks.ToList();
            return result;
        }
        public Kaizen00030 GetSingle(int RoleID)
        {
            var tasks = _Kaizen00030Repository.GetSingle(x => x.RoleID == RoleID);
            return tasks;
        }
        public Kaizen00030 GetSingleWithViews(int RoleID)
        {
            var tasks = _Kaizen00030Repository.GetSingle(x => x.RoleID == RoleID, ss => ss.Kaizen00011);
            foreach (var item in tasks.Kaizen00011)
            {
                item.Kaizen00030 = null;
            }
            return tasks;
        }

        public KaizenResult AddKaizen00030(Kaizen00030 newTask)
        {
            var result = _Kaizen00030Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizen00030(IList<Kaizen00030> newTask)
        {
            var result = _Kaizen00030Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Kaizen00030 newTask)
        {
            var result = _Kaizen00030Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Kaizen00030> newTask)
        {
            var result = _Kaizen00030Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Kaizen00030 newTask)
        {
            var result = _Kaizen00030Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Kaizen00030> newTask)
        {
            var result = _Kaizen00030Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _Kaizen00030Repository.ExecuteSqlCommandInt(myQuery);
            return result;
        }

        public KaizenResult SaveViewRole(int RoleID, int ViewID)
        {
            KaizenResult result = new KaizenResult();
            result = _Kaizen00030Repository.ExecuteSqlCommand("insert into Kaizen005 (RoleID, ViewID) VALUES ("+ RoleID+","+ ViewID+")");
            return result;
        }
        public KaizenResult DeleteViewRole(int RoleID, int ViewID)
        {
            KaizenResult result = new KaizenResult();
            result = _Kaizen00030Repository.ExecuteSqlCommand("delete from Kaizen005 where RoleID='" + RoleID + "' AND ViewID='" + ViewID + "'");
            return result;
        }

        #region Role Company
        public IList<Kaizen006> GetRolesByCompany(string CompanyID)
        {
            Kaizen006Repository rep = new Kaizen006Repository(UserContext.UserName, UserContext.Password);
            var rolesByCompany = rep.GetAll(xx => xx.CompanyID == CompanyID);
            IList<Kaizen006> result = rolesByCompany;
            return result;
        }
        public KaizenResult AddKaizen006(Kaizen006 companyRole)
        {
            Kaizen006Repository rep = new Kaizen006Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(companyRole);
            return result;
        }
        public KaizenResult DeleteKaizen006(Kaizen006 companyRole)
        {
            Kaizen006Repository rep = new Kaizen006Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(companyRole);
            return result;
        }
        #endregion

    }
}
