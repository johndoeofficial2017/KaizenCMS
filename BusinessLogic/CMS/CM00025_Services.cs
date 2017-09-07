using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
namespace Kaizen.BusinessLogic.CMS
{
    public class CM00025Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00025Repository _CM00025Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00025Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00025Repository = new CM00025Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00025> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00025> L = null;
            var tasks = _CM00025Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00025> result = tasks;
            return result;
        }
        public DataCollection<CM00025> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00025> result = null;
            var tasks = _CM00025Repository.GetListWithPaging(PageSize, Page, ss => new { ss.TaskID });
            result = tasks;
            return result;
        }
        public IList<CM00025> GetAll()
        {
            var tasks = _CM00025Repository.GetAll();
            IList<CM00025> result = tasks;
            return result;
        }
        public IList<CM00025> GetAllByCaseStatusID(int CaseStatusID)
        {
            var tasks = _CM00025Repository.GetAll(xx=>xx.CaseStatusID== CaseStatusID);
            IList<CM00025> result = tasks;
            return result;
        }
        public CM00025 GetSingle(int TaskID)
        {
            var tasks = _CM00025Repository.GetSingle(x => x.TaskID == TaskID);
            return tasks;
        }

        public KaizenResult AddCM00025(CM00025 newTask)
        {
            var result = _CM00025Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00025(IList<CM00025> newTask)
        {
            var result = _CM00025Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00025 newTask)
        {
            var result = _CM00025Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00025> newTask)
        {
            var result = _CM00025Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00025 newTask)
        {
            var result = _CM00025Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00025> newTask)
        {
            var result = _CM00025Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}

