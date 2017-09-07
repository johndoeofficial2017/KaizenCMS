using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00009Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00009Repository _GL00009RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00009Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00009RepositoryDataRepository = new GL00009Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public IList<GL00009> GetAll()
        {
            var tasks = _GL00009RepositoryDataRepository.GetAll();
            IList<GL00009> result = tasks;
            return result;
        }
        public DataCollection<GL00009> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<GL00009> L = null;
            var tasks = _GL00009RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<GL00009> result = tasks;
            return result;
        }
        public DataCollection<GL00009> GetListWithPaging(string SearchTerm, int PageSize, int Page
            , string Member, string SortDirection)
        {
            DataCollection<GL00009> result = null;
            var tasks = _GL00009RepositoryDataRepository.GetListWithPaging(x => x.ExchangeTableName.ToString().Contains(SearchTerm)
                , PageSize, Page, ss => new { ss.ExchangeTableID });
            result = tasks;
            return result;
        }
        public DataCollection<GL00009> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00009> result = null;
            var tasks = _GL00009RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.ExchangeTableID });
            result = tasks;
            return result;
        }
        public GL00009 GetSingle(string ExchangeTableID)
        {
            var tasks = _GL00009RepositoryDataRepository.GetSingle(x => x.ExchangeTableID.Trim() == ExchangeTableID.Trim());
            return tasks;
        }

        public KaizenResult AddGL00009(GL00009 newTask)
        {
            var result = _GL00009RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00009(IList<GL00009> newTask)
        {
            var result = _GL00009RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00009 newTask)
        {
            var result = _GL00009RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00009> newTask)
        {
            var result = _GL00009RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00009 newTask)
        {
            var result = _GL00009RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00009> newTask)
        {
            var result = _GL00009RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
