using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System.Collections.Generic;
using System.Linq;



namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00003Services
    {
        #region Infrastructure

        private Kaizen00003Repository _Kaizen00003Repository;
        private KaizenSession UserContext;
        public Kaizen00003Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
            }
            UserContext = _UserContext;
            _Kaizen00003Repository = new Kaizen00003Repository(UserContext.UserName, UserContext.Password);
        }
        #endregion

        public List<Kaizen00003> GetDynamicTableField(int ScreenID, string kaizenTableName)
        {
            //var tasks = _Kaizen00003Repository.GetAll(x => x.ScreenID == ScreenID);
            //return tasks.ToList();
            return null;
        }

        public List<Kaizen00003> GetByScreenFieldAndValue(int ScreenID, int FieldID, string FieldValue)
        {
            var tasks = _Kaizen00003Repository.GetAll(x => x.ScreenID == ScreenID && x.FieldID == FieldID && x.FieldValue == FieldValue.Trim());
            return tasks.ToList();
        }
        public List<Kaizen00003> GetByScreenAndField(int ScreenID, int FieldID)
        {
            var tasks = _Kaizen00003Repository.GetAll(x => x.ScreenID == ScreenID && x.FieldID == FieldID);
            return tasks.ToList();
        }
        public List<Kaizen00003> GetAllDropDownFields()
        {
            var tasks = _Kaizen00003Repository.GetAll();
            return tasks.ToList();
        }

        public KaizenResult AddKaizen00003(Kaizen00003 newTask)
        {
            var result = _Kaizen00003Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizen00003(IList<Kaizen00003> newTask)
        {
            var result = _Kaizen00003Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Kaizen00003 newTask)
        {
            var result = _Kaizen00003Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Kaizen00003> newTask)
        {
            var result = _Kaizen00003Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Kaizen00003 newTask)
        {
            var result = _Kaizen00003Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Kaizen00003> newTask)
        {
            var result = _Kaizen00003Repository.DeleteKaizenObject(newTask.ToArray());
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
