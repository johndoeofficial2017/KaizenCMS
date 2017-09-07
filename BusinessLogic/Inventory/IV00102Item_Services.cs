using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00102ItemServices
    {
        #region Infrastructure

        private IV00102ItemRepository _IV00102ItemRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00102ItemServices(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00102ItemRepository = new IV00102ItemRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<IV00102Item> GetAllIV00102Item(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<IV00102Item> L = null;
                var tasks = _IV00102ItemRepository.GetListWithPaging(x => x.ItemID.Contains(SearchTerm), PageSize, Page, ss => ss.ItemID, null);

                DataCollection<IV00102Item> result = tasks;
                return result;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- GenderID: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return null;
        }
        public DataCollection<IV00102Item> GetByItemID(string ItemID, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<IV00102Item> L = null;
                var tasks = _IV00102ItemRepository.GetListWithPaging(x => x.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => ss.ItemID, null);

                DataCollection<IV00102Item> result = tasks;
                return result;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- GenderID: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return null;
        }
        public DataCollection<IV00102Item> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string ItemID,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            else
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = " and ";
                if (FieldID == "-1")
                {
                    SeachStr += Help.GetFilter("ItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ShortDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GenericDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UFMGroupID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UFMPurchase", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UFMSale", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UnitPrice", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UnitCost", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsinActive", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00102Item> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00102ItemRepository.GetListWithPaging(ss => ss.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00102ItemRepository.GetWhereListWithPaging("IV00102Item", PageSize, Page, SeachStr, Member, IsAscending);
            
            return result;
        }

        public DataCollection<IV00102Item> GetAllSiteItemsViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string SiteID,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            else
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = " and ";
                if (FieldID == "-1")
                {
                    SeachStr += Help.GetFilter("ItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ShortDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GenericDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UFMGroupID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UFMPurchase", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UFMSale", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UnitPrice", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UnitCost", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsinActive", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00102Item> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00102ItemRepository.GetListWithPaging(ss => ss.SiteID.Trim() == SiteID.Trim() || ss.SiteID == null || ss.SiteID == "", PageSize, Page, ss => Member);
            else
                result = _IV00102ItemRepository.GetWhereListWithPaging("IV00102Item", PageSize, Page, SeachStr, Member, IsAscending);
            
            return result;
        }

        public IList<IV00102Item> GetAll()
        {
            var tasks = _IV00102ItemRepository.GetAll();
            IList<IV00102Item> result = tasks;
            return result;
        }
        public IV00102Item GetSingle(string ItemID, string SiteID)
        {
            var tasks = _IV00102ItemRepository.GetSingle(x => x.ItemID.Trim() == ItemID.Trim() && x.SiteID.Trim() == SiteID);
            IV00102Item result = tasks;
            return result;
        }
        public IList<IV00102Item> GetAllBySiteItem(string ItemID)
        {
            IList<IV00102Item> IV00102Item = _IV00102ItemRepository.GetAll(ss => ss.ItemID.Trim() == ItemID.Trim());
            return IV00102Item;
        }
        public IList<IV00102Item> GetAllBySite(string SiteID)
        {
            IList<IV00102Item> IV00102Item = _IV00102ItemRepository.GetAll(ss => ss.SiteID.Trim() == SiteID.Trim());
            return IV00102Item;
        }

        public KaizenResult AddIV00102Item(IV00102Item newTask)
        {
            var result = _IV00102ItemRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00102Item(IList<IV00102Item> newTask)
        {
            var result = _IV00102ItemRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00102Item newTask)
        {
            var result = _IV00102ItemRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00102Item> newTask)
        {
            var result = _IV00102ItemRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(IV00102Item newTask)
        {
            var result = _IV00102ItemRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00102Item> newTask)
        {
            var result = _IV00102ItemRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
