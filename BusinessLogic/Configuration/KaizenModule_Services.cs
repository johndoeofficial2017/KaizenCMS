using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System.Collections.Generic;



namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen000Services
    {
        #region Infrastructure

        private Kaizen000Repository _Kaizen000Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Kaizen000Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Kaizen000Repository = new Kaizen000Repository(UserContext.UserName, UserContext.Password);
        }
        public Kaizen000Services()
        {
            _Kaizen000Repository = new Kaizen000Repository(null,null);
        }
        #endregion
        public IList<Kaizen000> GetListFromKaizen()
        {
            var tasks = _Kaizen000Repository.GetListFromKaizen();
            IList<Kaizen000> result = tasks;
            return result;
        }
        public IList<Kaizen000> GetAllModules()
        {
            var tasks = _Kaizen000Repository.GetAll();
            IList<Kaizen000> result = tasks;
            return result;
        }

    }
}
