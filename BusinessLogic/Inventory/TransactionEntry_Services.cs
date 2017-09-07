using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class TransactionEntryServices
    {
        #region Infrastructure

        private IV00201Repository _IV00201RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public TransactionEntryServices(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00201RepositoryDataRepository = new IV00201Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
       
        public DataCollection<IV00201> GetAllIV00201(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<IV00201> L = null;
            var tasks = _IV00201RepositoryDataRepository.GetListWithPaging(x => x.TransactionID.Contains(SearchTerm)
                , PageSize, Page, ss => ss.TransactionID, null);
            DataCollection<IV00201> result = tasks;
            return result;
        }
        public IList<IV00201> GetAll()
        {
            var tasks = _IV00201RepositoryDataRepository.GetAll();
            IList<IV00201> result = tasks;
            return result;
        }
  
        public bool AddIV00201(IV00201 newTask)
        {
            _IV00201RepositoryDataRepository.Add(newTask);
            return true;

        }
        public bool Update(IV00201 UpdateIV00201)
        {
            _IV00201RepositoryDataRepository.Update(UpdateIV00201);
            return true;
        }     
    }
}
