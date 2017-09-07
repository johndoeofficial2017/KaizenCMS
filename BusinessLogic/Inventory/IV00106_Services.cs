using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00106Services
    {
        #region Infrastructure

        private IV00106Repository _IV00106Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00106Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00106Repository = new IV00106Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<IV00106> GetAllIV00106(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            var tasks = _IV00106Repository.GetListWithPaging(x => x.ItemID.Contains(SearchTerm), PageSize, Page, ss => ss.ItemID, null);
            DataCollection<IV00106> result = tasks;
            return result;
        }
        public DataCollection<IV00106> GetByItemID(string ItemID, int PageSize, int Page, string Member, string SortDirection)
        {
            var tasks = _IV00106Repository.GetListWithPaging(x => x.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => ss.ItemID, null);
            DataCollection<IV00106> result = tasks;
            return result;
        }

        public IList<IV00106> GetAll()
        {
            var tasks = _IV00106Repository.GetAll();
            IList<IV00106> result = tasks;
            return result;
        }
        public IV00106 GetSingle(string ItemID)
        {
            var tasks = _IV00106Repository.GetSingle(x => x.ItemID.Trim() == ItemID.Trim());
            IV00106 result = tasks;
            return result;
        }

        public KaizenResult AddIV00106(IV00106 newTask)
        {
            var result = _IV00106Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00106(IList<IV00106> newTask)
        {
            var result = _IV00106Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00106 newTask)
        {
            var result = _IV00106Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00106> newTask)
        {
            var result = _IV00106Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(IV00106 newTask)
        {
            var result = _IV00106Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00106> newTask)
        {
            var result = _IV00106Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
