using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
namespace Kaizen.BusinessLogic.CMS
{
    public class CM00026Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00026Repository _CM00026Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00026Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00026Repository = new CM00026Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00026> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00026> L = null;
            var tasks = _CM00026Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00026> result = tasks;
            return result;
        }
        public DataCollection<CM00026> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00026> result = null;
            var tasks = _CM00026Repository.GetListWithPaging(PageSize, Page, ss => new { ss.StatusScriptID });
            result = tasks;
            return result;
        }
        public IList<CM00026> GetAll()
        {
            var tasks = _CM00026Repository.GetAll();
            IList<CM00026> result = tasks;
            return result;
        }
        public IList<CM00026> GetAllByCaseStatusID(int CaseStatusID)
        {
            var tasks = _CM00026Repository.GetAll(xx=>xx.CaseStatusID == CaseStatusID);
            IList<CM00026> result = tasks;
            return result;
        }
        public CM00026 GetSingle(int StatusScriptID)
        {
            var tasks = _CM00026Repository.GetSingle(x => x.StatusScriptID == StatusScriptID);
            return tasks;
        }

        public KaizenResult AddCM00026(CM00026 newTask)
        {
            var result = _CM00026Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00026(IList<CM00026> newTask)
        {
            var result = _CM00026Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00026 newTask)
        {
            var result = _CM00026Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00026> newTask)
        {
            var result = _CM00026Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00026 newTask)
        {
            var result = _CM00026Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00026> newTask)
        {
            var result = _CM00026Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}

