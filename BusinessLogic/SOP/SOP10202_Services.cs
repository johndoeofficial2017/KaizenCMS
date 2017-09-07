using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
using System.Globalization;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP10202Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10202Repository _SOP10202RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10202Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10202RepositoryDataRepository = new SOP10202Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<SOP10202> GetAllSOP10202(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP10202> L = null;
            var tasks = _SOP10202RepositoryDataRepository.GetListWithPaging(x => x.SOPNUMBE.Contains(SearchTerm), PageSize, Page, ss => ss.ItemCategoryID, null);
            DataCollection<SOP10202> result = tasks;
            return result;
        }
        public DataCollection<SOP10202> GetBySOPNUMBE(string SOPNUMBE, int SOPTYPE, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP10202> L = null;
            var tasks = _SOP10202RepositoryDataRepository.GetListWithPaging(x => x.SOPNUMBE.Trim() == SOPNUMBE.Trim() && x.SOPTYPE == SOPTYPE, PageSize, Page, ss => ss.ItemCategoryID, null);

            DataCollection<SOP10202> result = tasks;
            return result;
        }

        public IList<SOP10202> GetAll()
        {
            var tasks = _SOP10202RepositoryDataRepository.GetAll();
            IList<SOP10202> result = tasks;
            return result;
        }
        public IList<SOP10202> GetAllBySOPNUMBE(string SOPNUMBE, string SOPTypeSetupID)
        {
            var tasks = _SOP10202RepositoryDataRepository.GetAll(ss => ss.SOPNUMBE.Trim() == SOPNUMBE.Trim() &&
                ss.SOPTypeSetupID == SOPTypeSetupID, ss => new { ss.ItemCategoryID });
            IList<SOP10202> result = tasks;
            return result;
        }
        public SOP10202 GetSingle(int ItemCategoryID)
        {
            var tasks = _SOP10202RepositoryDataRepository.GetSingle(x => x.ItemCategoryID == ItemCategoryID);
            return tasks;
        }

        public IList<Sys00007> GetAllSys00007()
        {
            Sys00007Repository r = new Sys00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<Sys00007> result = tasks;
            return result;
        }

        public bool AddSOP10202(IList<SOP10202> newTask)
        {
            _SOP10202RepositoryDataRepository.Add(newTask.ToArray());
            return true;
        }
        public bool Update(IList<SOP10202> UpdateSOP10202)
        {
            _SOP10202RepositoryDataRepository.Update(UpdateSOP10202.ToArray());
            return true;
        }
        public bool Delete(IList<SOP10202> newTask)
        {
            if (newTask == null) return false;
            string str = string.Empty;
            foreach (var item in newTask)
            {
                str += item.ItemCategoryID + ",";
            }
            str = str.Substring(0, str.Length - 1);
            _SOP10202RepositoryDataRepository.ExecuteSqlCommand("delete SOP10202 where ItemCategoryID in(" + str + ");");
            return true;
        }
    }
}
