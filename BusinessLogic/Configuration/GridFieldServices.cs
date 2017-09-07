using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System.Collections.Generic;
using System.Linq;



namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00025Services
    {
        #region Infrastructure

        private Kaizen00025Repository _Kaizen00025Repository;
        private KaizenSession UserContext;
        public Kaizen00025Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
                
            }
            UserContext = _UserContext;
            _Kaizen00025Repository = new Kaizen00025Repository(UserContext.UserName, UserContext.Password);
        }
        #endregion
        public DataCollection<Kaizen00025> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Kaizen00025> L = null;
            var tasks = _Kaizen00025Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<Kaizen00025> result = tasks;
            return result;
        }
        public DataCollection<Kaizen00025> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Kaizen00025> result = null;
            var tasks = _Kaizen00025Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ScreenID });
            result = tasks;
            return result;
        }
        public List<Kaizen00025> GetAll()
        {
            var tasks = _Kaizen00025Repository.GetAll();
            List<Kaizen00025> result = tasks.ToList();
            return result;
        }

        public List<Kaizen00025> GetFieldsByScreen(int ScreenID)
        {
            var tasks = _Kaizen00025Repository.GetList(x => x.ScreenID == ScreenID);
            return tasks.ToList();
        }

        public KaizenResult AddKaizen00025(Kaizen00025 newTask)
        {
            var result = _Kaizen00025Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizen00025(IList<Kaizen00025> newTask)
        {
            var result = _Kaizen00025Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Kaizen00025 newTask)
        {
            var result = _Kaizen00025Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Kaizen00025> newTask)
        {
            var result = _Kaizen00025Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Kaizen00025 newTask)
        {
            var result = _Kaizen00025Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Kaizen00025> newTask)
        {
            var result = _Kaizen00025Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
