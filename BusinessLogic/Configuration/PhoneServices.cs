using System.Collections.Generic;
using Kaizen.Configuration.Repository;
using Kaizen.Configuration;

namespace Kaizen.BusinessLogic.Configuration
{
    public class PhoneServices
    {
        #region Infrastructure

        private Kaizen00100Repository _Kaizen00100Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public PhoneServices(KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
                
            }
            UserContext = _UserContext;
            _Kaizen00100Repository = new Kaizen00100Repository(UserContext.UserName, UserContext.Password);
            if (UserContext.Screens == null)
                UserContext.Screens = new List<Screen>();
        }
        #endregion
        public Kaizen00100 GetSingle(int TransactionID)
        {
            var tasks = _Kaizen00100Repository.GetSingle(x => x.TransactionID == TransactionID);
            return tasks;
        }
        public KaizenResult AddKaizen00100(Kaizen00100 newTask)
        {
            var result = _Kaizen00100Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(Kaizen00100 UpdateKaizen00100)
        {
            var result = _Kaizen00100Repository.UpdateKaizenObject(UpdateKaizen00100);
            return result;
        }
        public KaizenResult Delete(Kaizen00100 Kaizen00100)
        {
            var result = _Kaizen00100Repository.DeleteKaizenObject(Kaizen00100);
            return result;
        }
       
    }
}
