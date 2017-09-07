using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System.Collections.Generic;
using System.Linq;



namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00001Services
    {
        #region Infrastructure

        private Kaizen00001Repository _Kaizen00001Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Kaizen00001Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
            }
            UserContext = _UserContext;
            _Kaizen00001Repository = new Kaizen00001Repository(UserContext.UserName, UserContext.Password);
        }
        #endregion

        public List<Kaizen00001> GetAllByScreen(int ScreenID)
        {
            var tasks = _Kaizen00001Repository.GetAll(x => x.ScreenID == ScreenID);
            return tasks.ToList();
        }

        public KaizenResult AddKaizen00001(Kaizen00001 newTask)
        {
            var result = _Kaizen00001Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizen00001(IList<Kaizen00001> newTask)
        {
            var result = _Kaizen00001Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Kaizen00001 newTask)
        {
            var result = _Kaizen00001Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Kaizen00001> newTask)
        {
            var result = _Kaizen00001Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Kaizen00001 newTask)
        {
            var result = _Kaizen00001Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Kaizen00001> newTask)
        {
            var result = _Kaizen00001Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
