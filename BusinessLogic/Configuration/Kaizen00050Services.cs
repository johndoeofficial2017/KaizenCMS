using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00050Services
    {
        #region Infrastructure

        private Kaizen00050Repository _Kaizen00050Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Kaizen00050Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
              
            }
            UserContext = _UserContext;
            _Kaizen00050Repository = new Kaizen00050Repository(UserContext.UserName, UserContext.Password);
        }
        #endregion
        public DataCollection<Kaizen00050> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Kaizen00050> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen00050Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Kaizen00050Repository.GetWhereListWithPaging("Kaizen00050", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00050> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<Kaizen00050> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen00050Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Kaizen00050Repository.GetWhereListWithPaging("Kaizen00050", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00050> GetAllBYSQLQuery(string Filters, string Searchcritaria, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Kaizen00050> result = null;
            if (!string.IsNullOrEmpty(Filters))
            {
                Page = Page - 1;
                if (!string.IsNullOrEmpty(Searchcritaria))
                {
                    Filters += " and ModuleID like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and ModuleName like '%" + Searchcritaria.Trim() + "%'";
                }
                string sql = "select  * from Kaizen00050 ";
                sql += " where " + Filters + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
                var tasks = _Kaizen00050Repository.GetSQLData(sql);
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
                var tasks = _Kaizen00050Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            else
            {
                //var tasks = _Kaizen00050Repository.GetListWithPaging(xx => xx.ModuleID.ToString().Contains(Searchcritaria)
                //    , PageSize, Page, ss => Member, ss => SortDirection);
                //result = tasks;
            }
            return result;
        }

        public List<Kaizen00050> GetAll()
        {
            var tasks = _Kaizen00050Repository.GetAll();
            List<Kaizen00050> result = tasks.ToList();
            return result;
        }
        public Kaizen00050 GetSingle(int DashboardCode)
        {
            var tasks = _Kaizen00050Repository.GetSingle(x => x.DashboardCode == DashboardCode);
            return tasks;
        }
        public List<Kaizen00050> GetAllByCompany(string CompanyID)
        {
            var tasks = _Kaizen00050Repository.GetAll(ss => ss.CompanyID == CompanyID);
            List<Kaizen00050> result = tasks.ToList();
            return result;
        }
        public List<Kaizen00050> GetMyDashboard()
        {
            var tasks = _Kaizen00050Repository.GetList(ss => ss.CompanyID == UserContext.CompanyID &&
            ss.Kaizen00055.Any(tt => tt.UserName == UserContext.UserName), qq => qq.Kaizen00055);

            List<Kaizen00050> oresult = tasks.ToList();
            List<Kaizen00050> L = new List<Kaizen00050>();
            foreach(Kaizen00050 o in oresult)
            {
                L.Add(new Kaizen00050() {DashboardCode = o.DashboardCode,DashboardName = o.DashboardName  });
            }
            return L;
        }
        public KaizenResult AddKaizen00050(Kaizen00050 newTask)
        {
            var result = _Kaizen00050Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizen00050(IList<Kaizen00050> newTask)
        {
            var result = _Kaizen00050Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Kaizen00050 newTask)
        {
            var result = _Kaizen00050Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Kaizen00050> newTask)
        {
            var result = _Kaizen00050Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Kaizen00050 newTask)
        {
            var result = _Kaizen00050Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Kaizen00050> newTask)
        {
            var result = _Kaizen00050Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        #region Source Users
        public KaizenResult SaveDashboardUser(Kaizen00055 Kaizen00055)
        {
            Kaizen00055Repository rep = new Kaizen00055Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(Kaizen00055);
            return result;
        }
        public KaizenResult DeleteDashboardUser(Kaizen00055 Kaizen00055)
        {
            Kaizen00055Repository rep = new Kaizen00055Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(Kaizen00055);
            return result;
        }
        public IList<Kaizen00055> GetDashboardUsers(string UserName)
        {
            Kaizen00055Repository rep = new Kaizen00055Repository(UserContext.UserName, UserContext.Password);
            var sourceUsers = rep.GetAll().Where(x => x.UserName == UserName).ToList();
            IList<Kaizen00055> result = sourceUsers;
            return result;
        }
        #endregion

        #region Source Role Security
        public KaizenResult SaveDashboardRole(Kaizen00054 Kaizen00054)
        {
            Kaizen00054Repository rep = new Kaizen00054Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(Kaizen00054);
            return result;
        }
        public KaizenResult DeleteDashboardRole(Kaizen00054 Kaizen00054)
        {
            Kaizen00054Repository rep = new Kaizen00054Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(Kaizen00054);
            return result;
        }
        public IList<Kaizen00054> GetDashboardRoles(int RoleID)
        {
            Kaizen00054Repository rep = new Kaizen00054Repository(UserContext.UserName, UserContext.Password);
            var dashboardRoles = rep.GetAll().Where(x => x.RoleID == RoleID).ToList();
            IList<Kaizen00054> result = dashboardRoles;
            return result;
        }
        #endregion

        #region Dashboard Report
        public IList<Kaizen00051> GetReportTypes()
        {
            Kaizen00051Repository rep = new Kaizen00051Repository(UserContext.UserName, UserContext.Password);
            var dashboardReportTypes = rep.GetAll();
            IList<Kaizen00051> result = dashboardReportTypes;
            return result;
        }
        public IList<Kaizen00052> GetDashboardReports(int ReportTypeID)
        {
            Kaizen00052Repository rep = new Kaizen00052Repository(UserContext.UserName, UserContext.Password);
            var dashboardRoles = rep.GetAll().Where(x => x.ReportTypeID == ReportTypeID).ToList();
            IList<Kaizen00052> result = dashboardRoles;
            return result;
        }
        public IList<Kaizen00053> GetKaizen00053ForReportType(int ReportTypeID)
        {
            Kaizen00052Repository rep = new Kaizen00052Repository(UserContext.UserName, UserContext.Password);
            Kaizen00053Repository rep53 = new Kaizen00053Repository(UserContext.UserName, UserContext.Password);
            var dashboardReports = rep.GetAll().Where(x => x.ReportTypeID == ReportTypeID).ToList();

            IList<Kaizen00053> Kaizen00053List = new List<Kaizen00053>();

            foreach(Kaizen00052 obj in dashboardReports)
            {
                var obj53 = rep53.GetAll().Where(x => x.ReportID == obj.ReportID).FirstOrDefault();
                if(obj53!=null)
                Kaizen00053List.Add(obj53);
            }
            IList<Kaizen00053> result = Kaizen00053List;
            return result;
        }
        public KaizenResult AddDashboardReportData(IList<Kaizen00053> Kaizen00053List)
        {
            Kaizen00053Repository rep = new Kaizen00053Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(Kaizen00053List.ToArray());
            return result;
        }
        public KaizenResult UpdateDashboardReportData(IList<Kaizen00053> Kaizen00053List)
        {
            Kaizen00053Repository rep = new Kaizen00053Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(Kaizen00053List.ToArray());
            return result;
        }
        public KaizenResult DeleteDashboardReportData(IList<Kaizen00053> Kaizen00053List)
        {
            Kaizen00053Repository rep = new Kaizen00053Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(Kaizen00053List.ToArray());
            return result;
        }
        #endregion
    }
}
