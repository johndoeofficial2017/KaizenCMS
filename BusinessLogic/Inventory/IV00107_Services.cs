using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00107Services
    {
        #region Infrastructure

        private IV00107Repository _IV00107RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00107Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00107RepositoryDataRepository = new IV00107Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<IV00107> GetAllIV00107(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            var tasks = _IV00107RepositoryDataRepository.GetListWithPaging(x => x.ItemID.Contains(SearchTerm), PageSize, Page, ss => ss.ItemID, null);
            DataCollection<IV00107> result = tasks;
            return result;
        }
        public DataCollection<IV00107> GetByItemID(string ItemID, int PageSize, int Page, string Member, string SortDirection)
        {
            var tasks = _IV00107RepositoryDataRepository.GetListWithPaging(x => x.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => ss.ItemID, null);
            DataCollection<IV00107> result = tasks;
            return result;
        }

        public IList<IV00107> GetAll()
        {
            var tasks = _IV00107RepositoryDataRepository.GetAll();
            IList<IV00107> result = tasks;
            return result;
        }

        public IV00107 GetSingle(string ItemID)
        {
            var tasks = _IV00107RepositoryDataRepository.GetSingle(x => x.ItemID == ItemID);
            return tasks;
        }

        public KaizenResult AddIV00107(IV00107 newTask)
        {
            var result = _IV00107RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00107(IList<IV00107> newTask)
        {
            var result = _IV00107RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00107 newTask)
        {
            var result = _IV00107RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00107> newTask)
        {
            var result = _IV00107RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(IV00107 newTask)
        {
            var result = _IV00107RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00107> newTask)
        {
            var result = _IV00107RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
