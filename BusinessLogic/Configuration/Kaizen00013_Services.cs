using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System.Collections.Generic;
using System.Linq;



namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00013Services
    {
        #region Infrastructure

        private Kaizen00013Repository _Kaizen00013Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Kaizen00013Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
               
            }
            UserContext = _UserContext;
            _Kaizen00013Repository = new Kaizen00013Repository(UserContext.UserName, UserContext.Password);
        }
        #endregion

        public List<Kaizen00013> GetDynamicTableField(int ScreenID, string kaizenTableName)
        {
            //var tasks = _Kaizen00013Repository.GetAll(x => x.ScreenID == ScreenID);
            //return tasks.ToList();
            return null;
        }
        public List<Kaizen00013> GetScreenFields(int ScreenID)
        {
            var tasks = _Kaizen00013Repository.GetAll(x => x.ScreenID == ScreenID);
            return tasks.ToList();
        }

        public KaizenResult AddKaizen00013(Kaizen00013 newTask)
        {
            var result = _Kaizen00013Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizen00013(IList<Kaizen00013> newTask)
        {
            var result = _Kaizen00013Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(Kaizen00013 newTask)
        {
            var result = _Kaizen00013Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Kaizen00013> newTask)
        {
            var result = _Kaizen00013Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Kaizen00013 newTask)
        {
            var result = _Kaizen00013Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Kaizen00013> newTask)
        {
            var result = _Kaizen00013Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
