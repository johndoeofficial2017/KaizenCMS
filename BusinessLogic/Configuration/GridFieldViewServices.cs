using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System.Collections.Generic;
using System.Linq;



namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00026Services
    {
        #region Infrastructure

        private Kaizen00026Repository _Kaizen00026Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Kaizen00026Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
                
            }
            UserContext = _UserContext;
            _Kaizen00026Repository = new Kaizen00026Repository(UserContext.UserName, UserContext.Password);
        }
        public Kaizen00026Services()
        {
            UserContext = new KaizenSession();
       
            _Kaizen00026Repository = new Kaizen00026Repository(UserContext.UserName, UserContext.Password);
        }
        #endregion
        public DataCollection<Kaizen00026> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Kaizen00026> L = null;
            var tasks = _Kaizen00026Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<Kaizen00026> result = tasks;
            return result;
        }
        public DataCollection<Kaizen00026> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Kaizen00026> result = null;
            //var tasks = _Kaizen00026Repository.GetListWithPaging(PageSize, Page, ss => new { ss.FieldID });
            //result = tasks;
            return null ;
        }
        public List<Kaizen00026> GetAll()
        {
            var tasks = _Kaizen00026Repository.GetAll();
            List<Kaizen00026> result = tasks.ToList();
            return result;
        }
       
        public List<Kaizen00026> GetFieldsByView(int ViewID)
        {
            var tasks = _Kaizen00026Repository.GetListFromKaizen(x => x.ViewID == ViewID);
            return tasks.ToList();
        }

        public KaizenResult AddKaizen00026(Kaizen00026 newTask)
        {
            var result = _Kaizen00026Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizen00026(IList<Kaizen00026> newTask)
        {
            var result = _Kaizen00026Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Kaizen00026 newTask)
        {
            var result = _Kaizen00026Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Kaizen00026> newTask)
        {
            var result = _Kaizen00026Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Kaizen00026 newTask)
        {
            var result = _Kaizen00026Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Kaizen00026> newTask)
        {
            var result = _Kaizen00026Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
