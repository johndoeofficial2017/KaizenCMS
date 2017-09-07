using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00027Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00027Repository _CM00027Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00027Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00027Repository = new CM00027Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public IList<CM00027> GetAll()
        {
            var tasks = _CM00027Repository.GetAll();
            IList<CM00027> result = tasks;
            return result;
        }

        public CM00027 GetSingle(int ChangeStatusSourceID)
        {
            var tasks = _CM00027Repository.GetSingle(x => x.ChangeStatusSourceID == ChangeStatusSourceID);
            return tasks;
        }

        public KaizenResult AddCM00027(CM00027 newTask)
        {
            var result = _CM00027Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00027(IList<CM00027> newTask)
        {
            var result = _CM00027Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00027 newTask)
        {
            var result = _CM00027Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00027> newTask)
        {
            var result = _CM00027Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00027 newTask)
        {
            var result = _CM00027Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00027> newTask)
        {
            var result = _CM00027Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
