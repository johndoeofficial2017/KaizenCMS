using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00022Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00022Repository _IV00022Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00022Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00022Repository = new IV00022Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
          
        public IList<IV00022> GetAll()
        { 
            var tasks = _IV00022Repository.GetAll();
            IList<IV00022> result = tasks;
            return result;
        }
        public IList<IV00022> GetByWeekDayID(int WeekDayID)
        {
            var tasks = _IV00022Repository.GetAll(ss=>ss.WeekDayID == WeekDayID);
            IList<IV00022> result = tasks;
            return result;
        }

        public IV00022 GetSingle( int WeekDayID)
        {
            var tasks = _IV00022Repository.GetSingle(x => x.WeekDayID == WeekDayID);
            return tasks;
        }
       
        public KaizenResult AddIV00022(IV00022 newTask)
        {
            var result = _IV00022Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00022(IList<IV00022> newTask)
        {
            var result = _IV00022Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00022 newTask)
        {
            var result = _IV00022Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00022> newTask)
        {
            var result = _IV00022Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00022 newTask)
        {
            var result = _IV00022Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00022> newTask)
        {
            var result = _IV00022Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}



