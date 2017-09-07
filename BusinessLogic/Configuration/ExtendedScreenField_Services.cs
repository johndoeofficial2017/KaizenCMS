using System.Collections.Generic;
using System.Linq;

using Kaizen.Configuration.Repository;
using Kaizen.Configuration;

namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00008Services
    {
        #region Infrastructure

        private Kaizen00008Repository _Kaizen00008Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Kaizen00008Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
            }
            UserContext = _UserContext;
            _Kaizen00008Repository = new Kaizen00008Repository(UserContext.UserName, UserContext.Password);
        }
        #endregion

        public List<Kaizen00008> GetAllByScreen(int EXT_ScreenID)
        {
            var tasks = _Kaizen00008Repository.GetAll(x => x.EXT_ScreenID == EXT_ScreenID);
            return tasks.ToList();
        }

        public KaizenResult AddKaizen00008(Kaizen00008 newTask)
        {
            var result = _Kaizen00008Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizen00008(IList<Kaizen00008> newTask)
        {
            var result = _Kaizen00008Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Kaizen00008 newTask)
        {
            var result = _Kaizen00008Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Kaizen00008> newTask)
        {
            var result = _Kaizen00008Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Kaizen00008 newTask)
        {
            var result = _Kaizen00008Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Kaizen00008> newTask)
        {
            var result = _Kaizen00008Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        public IList<Kaizen00006> GetAllKaizen00006()
        {
            Kaizen00006Repository r = new Kaizen00006Repository(UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<Kaizen00006> result = tasks;
            return result;
        }

    }
}
