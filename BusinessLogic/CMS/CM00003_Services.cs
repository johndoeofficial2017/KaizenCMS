using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
namespace Kaizen.BusinessLogic.CMS
{
    public class CM00003Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00003Repository _CM00003Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00003Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00003Repository = new CM00003Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00003> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("StatusActionTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("StatusActionTypeName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00003Repository rep = new CM00003Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00003> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00003", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00003> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00003> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00003Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00003Repository.GetWhereListWithPaging("CM00003", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00003> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00003> L = null;
            var tasks = _CM00003Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00003> result = tasks;
            return result;
        }
        public DataCollection<CM00003> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00003> result = null;
            var tasks = _CM00003Repository.GetListWithPaging(PageSize, Page, ss => new { ss.StatusActionTypeID });
            result = tasks;
            return result;
        }
        public IList<CM00003> GetAll()
        {
            var tasks = _CM00003Repository.GetAll();
            IList<CM00003> result = tasks;
            return result;
        }
        public CM00003 GetSingle(int StatusActionTypeID)
        {
            var tasks = _CM00003Repository.GetSingle(x => x.StatusActionTypeID == StatusActionTypeID);
            return tasks;
        }

        public KaizenResult AddCM00003(CM00003 newTask)
        {
            KaizenResult result = _CM00003Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00003(IList<CM00003> newTask)
        {
            KaizenResult result = _CM00003Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(CM00003 newTask)
        {
            KaizenResult result = _CM00003Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00003> newTask)
        {
            KaizenResult result = _CM00003Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(CM00003 newTask)
        {
            KaizenResult result = _CM00003Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00003> newTask)
        {
            KaizenResult result = _CM00003Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        // Status Group Roles
        public IList<CM00054> GetRolesByStatusActionType(int StatusActionTypeID)
        {
            CM00054Repository rep = new CM00054Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var actionTyperoles = rep.GetAll(xx => xx.StatusActionTypeID == StatusActionTypeID);
            IList<CM00054> result = actionTyperoles;
            return result;
        }

        public KaizenResult AddCM00054(CM00054 newTask)
        {
            CM00054Repository rep = new CM00054Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult DeleteCM00054(CM00054 newTask)
        {
            CM00054Repository rep = new CM00054Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(newTask);
            return result;
        }

        public IList<CM00053> GetStatusGroupByUser(string userName)
        {
            CM00053Repository rep = new CM00053Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(xx => xx.UserName == userName);
            IList<CM00053> result = tasks;
            return result;
        }

        public KaizenResult DeleteStatusGroupByUser(CM00053 statusGroupUser)
        {
            CM00053Repository rep = new CM00053Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var result = rep.DeleteKaizenObject(statusGroupUser);
            return result;
        }

        public KaizenResult AddCM00053(CM00053 stausGroupUser)
        {
            CM00053Repository rep = new CM00053Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(stausGroupUser);
            return result;
        }
    }
}

