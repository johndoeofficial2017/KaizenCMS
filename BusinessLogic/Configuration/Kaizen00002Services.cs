using System.Collections.Generic;
using System.Linq;

using Kaizen.Configuration;
using Kaizen.Configuration.Repository;

namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00002Services
    {
        #region Infrastructure

        private Kaizen00002Repository _Kaizen00002Repository;
        private KaizenSession UserContext;
        public Kaizen00002Services(KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
            }
            UserContext = _UserContext;
            _Kaizen00002Repository = new Kaizen00002Repository(UserContext.UserName, UserContext.Password);
        }
        #endregion
        public DataCollection<Kaizen00002> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Kaizen00002> L = null;
            var tasks = _Kaizen00002Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<Kaizen00002> result = tasks;
            return result;
        }
        public DataCollection<Kaizen00002> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Kaizen00002> result = null;
            var tasks = _Kaizen00002Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ScreenID });
            result = tasks;
            return result;
        }
        public List<Kaizen00002> GetAll()
        {
            var tasks = _Kaizen00002Repository.GetAll();
            List<Kaizen00002> result = tasks.ToList();
            return result;
        }

        public List<Kaizen00002> GetByScreenAndField(int ScreenID, int FieldID)
        {
            var tasks = _Kaizen00002Repository.GetList(x => x.ScreenID == ScreenID && x.FieldID == FieldID);
            return tasks.ToList();
        }

        public bool AddKaizen00002(Kaizen00002 newTask)
        {
            _Kaizen00002Repository.Add(newTask);
            return true;
        }
        public bool Update(Kaizen00002 UpdateKaizen00002)
        {
            _Kaizen00002Repository.Update(UpdateKaizen00002);
            return true;
        }
        public bool Delete(IList<Kaizen00002> newTask)
        {
            if (newTask == null) return false;
            string str = string.Empty;
            foreach (var item in newTask)
            {
                _Kaizen00002Repository.ExecuteSqlCommand("delete Kaizen00002 where FieldID ='" + item.FieldID.ToString() + "' AND ScreenID ='" + item.ScreenID + "' AND FieldValue ='" + item.FieldValue.Trim() + "'");
            }
            return true;
        }

    }
}
