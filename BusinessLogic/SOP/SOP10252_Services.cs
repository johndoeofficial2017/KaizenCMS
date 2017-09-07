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
    public class SOP10252Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10252Repository _SOP10252RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10252Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10252RepositoryDataRepository = new SOP10252Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<SOP10252> GetAllSOP10252(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP10252> L = null;
            var tasks = _SOP10252RepositoryDataRepository.GetListWithPaging(x => x.SOPNUMBE.Contains(SearchTerm), PageSize, Page, ss => ss.DocumentID, null);
            DataCollection<SOP10252> result = tasks;
            return result;
        }
        public DataCollection<SOP10252> GetBySOPNUMBE(string SOPNUMBE, int SOPTYPE, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP10252> L = null;
            var tasks = _SOP10252RepositoryDataRepository.GetListWithPaging(x => x.SOPNUMBE.Trim() == SOPNUMBE.Trim(), PageSize, Page, ss => ss.DocumentID, null);
            DataCollection<SOP10252> result = tasks;
            return result;
        }

        public IList<SOP10252> GetAll()
        {
            var tasks = _SOP10252RepositoryDataRepository.GetAll();
            IList<SOP10252> result = tasks;
            return result;
        }
        public IList<SOP10252> GetAllBySOPNUMBE(string SOPNUMBE, string SOPTypeSetupID)
        {
            var tasks = _SOP10252RepositoryDataRepository.GetAll(ss => ss.SOPNUMBE.Trim() == SOPNUMBE.Trim()
            && ss.SOPTypeSetupID.Trim() == SOPTypeSetupID.Trim()
              , ss => new { ss.DocumentID });
            IList<SOP10252> result = tasks;
            return result;
        }
        public SOP10252 GetSingle(int DocumentID)
        {
            var tasks = _SOP10252RepositoryDataRepository.GetSingle(x => x.DocumentID == DocumentID);
            return tasks;
        }

        public IList<Sys00007> GetAllSys00007()
        {
            Sys00007Repository r = new Sys00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<Sys00007> result = tasks;
            return result;
        }

        public bool AddSOP10252(SOP10252 newTask)
        {
            _SOP10252RepositoryDataRepository.Add(newTask);
            return true;
        }
        public bool Update(SOP10252 UpdateSOP10252)
        {
            _SOP10252RepositoryDataRepository.Update(UpdateSOP10252);
            return true;
        }
        public bool Delete(IList<SOP10252> newTask, string ServerPath)
        {
            if (newTask == null) return false;
            string str = string.Empty;
            foreach (var item in newTask)
            {
                str += item.DocumentID + ",";
            }
            str = str.Substring(0, str.Length - 1);
            int sucess = _SOP10252RepositoryDataRepository.ExecuteSqlCommand("delete SOP10252 where DocumentID in(" + str + ");");
            if (sucess > 0)
            {
                foreach (var item in newTask)
                {
                    string Destination = ServerPath + item.DocumentName.Trim() + item.PhotoExtension.Trim();
                    if (System.IO.File.Exists(Destination))
                    {
                        System.IO.File.Delete(Destination);
                    }
                }
            }
            return true;
        }

        public bool Delete(IList<SOP10252> newTask)
        {
            if (newTask == null) return false;
            string str = string.Empty;
            foreach (var item in newTask)
            {
                str += item.DocumentID + ",";
            }
            str = str.Substring(0, str.Length - 1);
            _SOP10252RepositoryDataRepository.ExecuteSqlCommand("delete SOP10252 where DocumentID in(" + str + ");");
            return true;
        }
    }
}
