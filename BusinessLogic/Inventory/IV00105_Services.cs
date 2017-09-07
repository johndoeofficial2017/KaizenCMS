using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00105Services
    {
        #region Infrastructure

        private IV00105Repository _IV00105RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00105Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00105RepositoryDataRepository = new IV00105Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<IV00105> GetAllIV00105(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            var tasks = _IV00105RepositoryDataRepository.GetListWithPaging(x => x.ItemID.Contains(SearchTerm), PageSize, Page, ss => ss.ItemID, null);

            DataCollection<IV00105> result = tasks;
            return result;
        }
        public DataCollection<IV00105> GetByItemID(string ItemID, int PageSize, int Page, string Member, string SortDirection)
        {
            var tasks = _IV00105RepositoryDataRepository.GetListWithPaging(x => x.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => ss.ItemID, null);
            DataCollection<IV00105> result = tasks;
            return result;
        }

        public IList<IV00105> GetAll()
        {
            var tasks = _IV00105RepositoryDataRepository.GetAll();
            IList<IV00105> result = tasks;
            return result;
        }

        public IV00105 GetSingle(string ItemID)
        {
            var tasks = _IV00105RepositoryDataRepository.GetSingle(x => x.ItemID == ItemID);
            return tasks;
        }

        public KaizenResult AddIV00105(IV00105 newTask)
        {
            var result=_IV00105RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00105(IList<IV00105> newTask)
        {
            var result = _IV00105RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00105 newTask)
        {
            var result = _IV00105RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00105> newTask)
        {
            var result = _IV00105RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(IV00105 newTask)
        {
            var result = _IV00105RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00105> newTask)
        {
            var result = _IV00105RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
