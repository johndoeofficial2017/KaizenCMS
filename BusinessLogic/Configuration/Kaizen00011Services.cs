using Kaizen.CMS.DAL;
using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;



namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00011Services
    {
        #region Infrastructure

        private Kaizen00011Repository _Kaizen00011Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Kaizen00011Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Kaizen00011Repository = new Kaizen00011Repository(UserContext.UserName, UserContext.Password);
        }
        public Kaizen00011Services()
        {
            UserContext = new KaizenSession();
           
            _Kaizen00011Repository = new Kaizen00011Repository(UserContext.UserName, UserContext.Password);
        }
        #endregion
        public DataCollection<Kaizen00011> GetAllViewByCompany(string CompanyID, int? ModuleID,
            string Filters, string Searchcritaria, int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = "CompanyID='" + CompanyID.Trim() + "'";
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr += Filters;
            }
            else
            {
                Searchcritaria = Searchcritaria.Trim();
                SeachStr += " and (ViewName like '%" + Searchcritaria + "%'";
                SeachStr += " or ViewDescription like '%" + Searchcritaria + "%')";
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr += " and " + Filters;
            }
            string str = string.Empty;
            Kaizen00010Repository _Kaizen00010Repository = new Kaizen00010Repository(UserContext.UserName, UserContext.Password);
            if (ModuleID != null)
            {
                var ScreenList = _Kaizen00010Repository.GetAll(ss => ss.ModuleID == ModuleID);
                foreach (var item in ScreenList)
                {
                    if (string.IsNullOrEmpty(str))
                        str += item.ScreenID;
                    else
                        str += "," + item.ScreenID;
                }
                if (!string.IsNullOrEmpty(ModuleID.ToString()))
                    SeachStr += " and ScreenID in (" + str.Trim() + ")";
            }
            DataCollection<Kaizen00011> result = null;
            result = _Kaizen00011Repository.GetWhereListWithPaging("Kaizen00011", PageSize, Page, SeachStr, Member, IsAscending);

            return result;
        }

        public DataCollection<Kaizen00011> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = "";
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            else
            {
                Searchcritaria = Searchcritaria.Trim();
                SeachStr += " and (ViewName like '%" + Searchcritaria + "%'";
                SeachStr += " or ViewDescription like '%" + Searchcritaria + "%')";
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = " and " + Filters;
            }
            DataCollection<Kaizen00011> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen00011Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Kaizen00011Repository.GetWhereListWithPaging("Kaizen00011", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00011> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ViewID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ScreenID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CompanyID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ViewName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ViewDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsDefault", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsCustomView", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ControllerSource", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ViewWhereCondition", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ViewStateData", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<Kaizen00011> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen00011Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Kaizen00011Repository.GetWhereListWithPaging("Kaizen00011", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00011> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string CompanyID,
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
                    SeachStr += Help.GetFilter("ViewID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ScreenID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CompanyID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ViewName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ViewDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsDefault", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsCustomView", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ControllerSource", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ViewWhereCondition", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ViewStateData", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<Kaizen00011> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen00011Repository.GetListWithPaging(ss => ss.CompanyID.Trim() == CompanyID.Trim(), PageSize, Page, ss => Member);
            else
                result = _Kaizen00011Repository.GetWhereListWithPaging("Kaizen00011", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<Kaizen00011> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Kaizen00011> L = null;
            var tasks = _Kaizen00011Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<Kaizen00011> result = tasks;
            return result;
        }
        public DataCollection<Kaizen00011> GetListWithPaging(string CompanyID, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Kaizen00011> result = null;
            var tasks = _Kaizen00011Repository.GetListWithPaging(xx => xx.CompanyID == CompanyID, PageSize, Page, ss => new { ss.ViewID });
            result = tasks;
            return result;
        }

        public IList<Kaizen00011> GetMyGridViews(Screen ScreenID)
        {
            IList<Kaizen00011> result;
            var tasks = _Kaizen00011Repository.GetList(zz => zz.ScreenID == (int)ScreenID
                && zz.CompanyID.Trim() == this.UserContext.CompanyID.Trim()
                && zz.KaizenGridViewAccesses.Any(mm => mm.UserName == UserContext.UserName)
               , null, null
               , ss => ss.KaizenGridViewAccesses);
            result = tasks;
            return result;
        }

        public IList<Kaizen00011> GetScreenViews(int ScreenID)
        {
            IList<Kaizen00011> result;
            var tasks = _Kaizen00011Repository.GetList(zz => zz.ScreenID == ScreenID);
            result = tasks;
            return result;
        }
        public IList<Kaizen00011> GetScreenViews(int ScreenID, string CompanyID)
        {
            IList<Kaizen00011> result;
            var tasks = _Kaizen00011Repository.GetList(zz => zz.ScreenID == ScreenID && zz.CompanyID.Trim() == CompanyID.Trim());
            result = tasks;
            return result;
        }

        public IList<Kaizen00011> GetMyGridViewByGridID(string UserName, Screen ScreenID)
        {
            IList<Kaizen00011> result;
            var tasks = _Kaizen00011Repository.GetList(zz => zz.ScreenID == (int)ScreenID
                && zz.KaizenGridViewAccesses.Any(mm => mm.UserName == UserName)
               , null, null
               , ss => ss.KaizenGridViewAccesses);
            result = tasks;
            return result;
        }

        public Kaizen00011 GetOne(int ViewID)
        {
            Kaizen00011 result;
            var tasks = _Kaizen00011Repository.GetSingle(zz => zz.ViewID == ViewID);
            result = tasks;
            return result;
        }

        public bool SaveGrid(int ViewID, string obj)
        {
            try
            {
                Kaizen00011 temp = this.GetOne(ViewID);
                temp.ViewStateData = obj;
                _Kaizen00011Repository.Update(temp);
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IList<Kaizen00011> GetAll()
        {
            var tasks = _Kaizen00011Repository.GetAll();
            IList<Kaizen00011> result = tasks;
            return result;
        }
        public Kaizen00011 GetSingle(int ViewID)
        {
            var tasks = _Kaizen00011Repository.GetSingleFromKaizen(x => x.ViewID == ViewID);
            return tasks;
        }
        public string GetViewWhereCondition(int ViewID)
        {
            var tasks = _Kaizen00011Repository.GetSingleFromKaizen(x => x.ViewID == ViewID);
            return tasks.ViewWhereCondition;
        }
        public KaizenResult AddKaizen00011(Kaizen00011 newTask)
        {
            newTask.IsCustomView = true;
            newTask.ControllerSource = "MainGrid";
            var result = _Kaizen00011Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizen00011(IList<Kaizen00011> newTask)
        {
            var result = _Kaizen00011Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Kaizen00011 newTask)
        {
            var result = _Kaizen00011Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Kaizen00011> newTask)
        {
            var result = _Kaizen00011Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Kaizen00011 newTask)
        {
            var result = _Kaizen00011Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Kaizen00011> newTask)
        {
            var result = _Kaizen00011Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        //public int ExecuteSqlCommand(string myQuery)
        //{
        //    var result = _Kaizen00011Repository.ExecuteSqlCommand(myQuery);
        //    return result;
        //}

        public List<DataRow> FillDropDownList(string TableName)
        {
            string sql = "select * from " + TableName;
            var result = _Kaizen00011Repository.ExecuteSqlCommandToDataTable(sql);
            return result.AsEnumerable().ToList();
        }
        public DataCollection<object> LookUpData(string TableName,int PageSize,int Page)
        {
            string PK_Sql = "SELECT ColumnName = col.column_name FROM information_schema.table_constraints ";
            PK_Sql += "tc INNER JOIN information_schema.key_column_usage col ";
            PK_Sql += "ON col.Constraint_Name = tc.Constraint_Name AND col.Constraint_schema = tc.Constraint_schema ";
            PK_Sql += "WHERE tc.Constraint_Type = 'Primary Key' AND col.Table_name = '" + TableName + "'";

            Kaizen.CMS.Repository.CM00203Repository _CM00203Repository = new Kaizen.CMS.Repository.CM00203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var result = _CM00203Repository.ExecuteSqlCommandToDataTable(PK_Sql);
            string OrderBy = result.Rows[0].ItemArray[0].ToString();

            string sql = "select * from " + TableName + " ORDER BY "+ OrderBy;
                sql += " OFFSET " + (PageSize * (Page - 1)).ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
            DataCollection<object> ItemCollection = null;
            using (var db = new CMSContext(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password))
            {
                db.Database.Connection.ConnectionString = db.Database.Connection.ConnectionString.Replace("TWO", UserContext.CompanyID.Trim());
                ItemCollection = new DataCollection<object>();
                var temp = db.Database.SqlQuery<object>(sql);
                ItemCollection.Items = temp.ToList();
                ItemCollection.TotalItemCount = _CM00203Repository.GetSQLINT("select count(*) from " + TableName + " ");
                ItemCollection.TotalPageCount = (int)Math.Ceiling((double)ItemCollection.TotalItemCount / (double)PageSize);
            }
            return ItemCollection;
        }


    }
}


