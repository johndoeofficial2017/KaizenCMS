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
    public class Ext00002Services
    {
        #region Infrastructure

        private Kaizen.Configuration.Repository.Ext00002Repository _Ext00002Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Ext00002Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Ext00002Repository = new Ext00002Repository(UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<Ext00002> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Ext00002> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Ext00002Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Ext00002Repository.GetWhereListWithPaging("Ext00002", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Ext00002> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<Ext00002> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _Ext00002Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _Ext00002Repository.GetWhereListWithPaging("Ext00002", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<Ext00002> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Ext00002> L = null;
            var tasks = _Ext00002Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<Ext00002> result = tasks;
            return result;
        }
        public DataCollection<Ext00002> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Ext00002> result = null;
            var tasks = _Ext00002Repository.GetListWithPaging(PageSize, Page, ss => new { ss.DataBaseSourceID });
            result = tasks;
            return result;
        }
        public IList<Ext00002> GetAll()
        {
            var tasks = _Ext00002Repository.GetAll();
            IList<Ext00002> result = tasks;
            return result;
        }
        public Ext00002 GetSingle(int DataBaseSourceID,string ComTableName)
        {
            var tasks = _Ext00002Repository.GetSingle(x => x.DataBaseSourceID == DataBaseSourceID && x.ComTableName.Trim().Equals(ComTableName.Trim()));
            return tasks;
        }
        public KaizenResult AddExt00002(Ext00002 newTask)
        {
            var result = _Ext00002Repository.AddKaizenObject(newTask);
            return result;
        }

        public KaizenResult AddExt00002(IList<Ext00002> newTask)
        {
            var result = _Ext00002Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(Ext00002 newTask)
        {
            var result = _Ext00002Repository.UpdateKaizenObject(newTask);
            return result;
        }

        public KaizenResult Update(IList<Ext00002> newTask)
        {
            var result = _Ext00002Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Ext00002 newTask)
        {
            var result = _Ext00002Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Ext00002> newTask)
        {
            var result = _Ext00002Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        public IList<DT00001> GetViewTypes()
        {
            DT00001Repository rep = new DT00001Repository(UserContext.UserName, UserContext.Password);
            var viewTypes = rep.GetAll();
            IList<DT00001> result = viewTypes;
            return result;
        }
        public IList<Kaizen001> GetMenueTypes()
        {
            Kaizen001Repository rep = new Kaizen001Repository(UserContext.UserName, UserContext.Password);
            var menueTypes = rep.GetAll();
            IList<Kaizen001> result = menueTypes;
            return result;
        }

        public IList<Ext00001> GetDataBaseSourceList()
        {
            Ext00001Repository rep = new Ext00001Repository(UserContext.UserName, UserContext.Password);
            var databaseSourceList = rep.GetAll();
            IList<Ext00001> result = databaseSourceList;
            return result;
        }

        #region Source Users
        public KaizenResult SaveSourceUser(Ext00005 Ext00005)
        {
            Ext00005Repository rep = new Ext00005Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(Ext00005);
            return result;
        }
        public KaizenResult UpdateSourceUser(Ext00005 Ext00005)
        {
            Ext00005Repository rep = new Ext00005Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(Ext00005);
            return result;
        }
        public KaizenResult DeleteSourceUser(Ext00005 Ext00005)
        {
            Ext00005Repository rep = new Ext00005Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(Ext00005);
            return result;
        }
        public IList<Ext00005> GetSourceUsers(int DataBaseSourceID,string ComTableName)
        {
            Ext00005Repository rep = new Ext00005Repository(UserContext.UserName, UserContext.Password);
            var sourceUsers = rep.GetAll().Where(x => x.DataBaseSourceID == DataBaseSourceID && x.ComTableName.Trim().Equals(ComTableName.Trim())).ToList();
            IList<Ext00005> result = sourceUsers;
            return result;
        }
        #endregion

        #region Source Role Security
        public KaizenResult SaveSourceRole(Ext00004 Ext00004)
        {
            Ext00004Repository rep = new Ext00004Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(Ext00004);
            return result;
        }
        public KaizenResult DeleteSourceRole(Ext00004 Ext00004)
        {
            Ext00004Repository rep = new Ext00004Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(Ext00004);
            return result;
        }
        public IList<Ext00004> GetSourceRoles(int DataBaseSourceID,string ComTableName)
        {
            Ext00004Repository rep = new Ext00004Repository(UserContext.UserName, UserContext.Password);
            var sourceRoles = rep.GetAll().Where(x => x.DataBaseSourceID == DataBaseSourceID&&x.ComTableName.Trim().Equals(ComTableName.Trim())).ToList();
            IList<Ext00004> result = sourceRoles;
            return result;
        }

        #endregion

        #region source Field
        public IList<Kaizen00006> GetSourceFields()
        {
            Kaizen00006Repository rep = new Kaizen00006Repository(UserContext.UserName, UserContext.Password);
            var sourceFields = rep.GetAll();
            IList<Kaizen00006> result = sourceFields;
            return result;
        }
        public IList<Ext00002> GetComTableNameList(int DataBaseSourceID)
        {
            Ext00002Repository rep = new Ext00002Repository(UserContext.UserName, UserContext.Password);
            var ComTableNameList = rep.GetAll().Where(x=>x.DataBaseSourceID== DataBaseSourceID).ToList();
            IList<Ext00002> result = ComTableNameList;
            return result;
        }
        public KaizenResult SaveSourceField(Ext00003 sourceField)
        {
            Ext00003Repository rep = new Ext00003Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(sourceField);
            return result;
        }

        public KaizenResult UpdateSourceField(Ext00003 sourceField)
        {
            Ext00003Repository rep = new Ext00003Repository(this.UserContext.UserName, this.UserContext.Password);
            KaizenResult result = rep.UpdateKaizenObject(sourceField);
            return result;
        }

        public KaizenResult DeleteSourceField(Ext00003 sourceField)
        {
            Ext00003Repository rep = new Ext00003Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(sourceField);
            return result;
        }
        public IList<Ext00003> GetExt00003(int DataBaseSourceID)
        {
            Ext00003Repository rep = new Ext00003Repository(UserContext.UserName, UserContext.Password);
            var Ext00003List = rep.GetAll().Where(x => x.DataBaseSourceID == DataBaseSourceID).ToList();
            IList<Ext00003> result = Ext00003List;
            return result;
        }
        public IList<Ext00003> GetExt00003(int DataBaseSourceID,string ComTableName)
        {
            Ext00003Repository rep = new Ext00003Repository(UserContext.UserName, UserContext.Password);
            var Ext00003List = rep.GetAll().Where(x=>x.DataBaseSourceID == DataBaseSourceID && x.ComTableName == ComTableName).ToList();
            IList<Ext00003> result = Ext00003List;
            return result;
        }
        #endregion
    }
}
