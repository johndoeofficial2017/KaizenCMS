using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System.Collections.Generic;
using System.Linq;



namespace Kaizen.BusinessLogic.Configuration
{
    public class KaizenGridViewAccessServices
    {
        #region Infrastructure

        private KaizenGridViewAccessRepository _KaizenGridViewAccessRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public KaizenGridViewAccessServices(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _KaizenGridViewAccessRepository = new KaizenGridViewAccessRepository(UserContext.UserName, UserContext.Password);
        }
        #endregion

        public List<KaizenGridViewAccess> GetUserViews(string UserName, int ScreenID)
        {
            var tasks = _KaizenGridViewAccessRepository.GetList(xx => xx.Kaizen00011.ScreenID == ScreenID && xx.UserName == UserName, xx => xx.Kaizen00011);
            List<KaizenGridViewAccess> result = tasks.ToList();
            return result;
        }

        public KaizenResult AddKaizenGridViewAccess(KaizenGridViewAccess newTask)
        {
            var result = _KaizenGridViewAccessRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizenGridViewAccess(IList<KaizenGridViewAccess> newTask)
        {
            var result = _KaizenGridViewAccessRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(KaizenGridViewAccess newTask)
        {
            var result = _KaizenGridViewAccessRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<KaizenGridViewAccess> newTask)
        {
            var result = _KaizenGridViewAccessRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(KaizenGridViewAccess newTask)
        {
            var result = _KaizenGridViewAccessRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<KaizenGridViewAccess> newTask)
        {
            var result = _KaizenGridViewAccessRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
