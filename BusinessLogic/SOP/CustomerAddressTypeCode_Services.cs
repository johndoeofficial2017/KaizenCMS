using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP00009Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00009Repository _SOP00009Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00009Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00009Repository = new SOP00009Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00009> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00009> L = null;
            var tasks = _SOP00009Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<SOP00009> result = tasks;
            return result;
        }
        public DataCollection<SOP00009> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00009> result = null;
            var tasks = _SOP00009Repository.GetListWithPaging(PageSize, Page, ss => new { ss.AddressTypeCode });
            result = tasks;
            return result;
        }
        public DataCollection<SOP00009> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00009> result = null;
            var tasks = _SOP00009Repository.GetListWithPaging(PageSize, Page, ss => new { ss.AddressTypeCode });
            result = tasks;
            return result;
        }

        public IList<SOP00009> GetAll()
        {
            var tasks = _SOP00009Repository.GetAll();
            return tasks;
        }
        public SOP00009 GetSingle(string AddressTypeCode)
        {
            var tasks = _SOP00009Repository.GetSingle(x => x.AddressTypeCode == AddressTypeCode);
            return tasks;
        }

        public bool AddSOP00009(SOP00009 newTask)
        {
            _SOP00009Repository.Add(newTask);
            return true;
        }
        public bool Update(SOP00009 UpdateSOP00009)
        {
            _SOP00009Repository.Update(UpdateSOP00009);
            return true;
        }
        public bool Delete(string AddressTypeCode)
        {
            _SOP00009Repository.ExecuteSqlCommand("delete SOP00009 where AddressTypeCode='" + AddressTypeCode.Trim() + "'");
            return true;
        }
    }
}
