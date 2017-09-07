using System.Collections.Generic;
using Kaizen.Configuration.Repository;
using Kaizen.Configuration;

namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00015Services
    {
        #region Infrastructure

        private Kaizen00015Repository _Kaizen00015Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Kaizen00015Services(KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
               
            }
            UserContext = _UserContext;
            _Kaizen00015Repository = new Kaizen00015Repository(UserContext.UserName, UserContext.Password);
            if (UserContext.Screens == null)
                UserContext.Screens = new List<Screen>();
        }
        #endregion
        public IList<Kaizen00015> GetAll(int ScreenID ,string TRXTypeID)
        {
            var tasks = _Kaizen00015Repository.GetAll(ss => ss.CompanyID == this.UserContext.CompanyID 
            && ss.TRXTypeID == TRXTypeID && ss.ScreenID == ScreenID);
            IList<Kaizen00015> result = tasks;
            return result;
        }
    }
}
