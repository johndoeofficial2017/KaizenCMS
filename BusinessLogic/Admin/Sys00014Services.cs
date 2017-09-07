using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin.Repository;
using Kaizen.Admin;

namespace Kaizen.BusinessLogic.Admin
{
    public class Sys00014Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.Sys00014Repository _Sys00014RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Sys00014Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Sys00014RepositoryDataRepository = new Sys00014Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<Sys00014> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Sys00014> L = null;
            var tasks = _Sys00014RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<Sys00014> result = tasks;
            return result;
        }
        public DataCollection<Sys00014> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Sys00014> result = null;
            var tasks = _Sys00014RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.CityID });
            result = tasks;
            return result;
        }

        public IList<Sys00014> GetAll()
        {
            var tasks = _Sys00014RepositoryDataRepository.GetAll();
            IList<Sys00014> result = tasks;
            return result;
        }

        public Sys00014 GetSingle(string CityID)
        {
            var tasks = _Sys00014RepositoryDataRepository.GetSingle(x => x.CityID == CityID);
            return tasks;
        }
  
        public KaizenResult AddSys00014(Sys00014 newTask)
        {
            var result=_Sys00014RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(Sys00014 newTask)
        {
            var result = _Sys00014RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(Sys00014 newTask)
        {
            var result = _Sys00014RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
    }
}
