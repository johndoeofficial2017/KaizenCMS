using Kaizen.Configuration.Repository;



namespace Kaizen.BusinessLogic.Configuration
{
    public class KaizenTasksServices
    {
        #region Infrastructure

        private KaizenTaskRepository _KaizenTaskRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public KaizenTasksServices(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _KaizenTaskRepository = new KaizenTaskRepository(UserContext.UserName, UserContext.Password);
        }
        #endregion

        //public List<Kaizen002> GetAll()
        //{
        //    var tasks = _KaizenTaskRepository.GetAll();
        //    List<Kaizen002> result = tasks.ToList();
        //    return result;
        //}
    }
}


