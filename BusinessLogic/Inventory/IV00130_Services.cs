using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00130Services
    {
        #region Infrastructure

        private IV00130Repository _IV00130Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00130Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00130Repository = new IV00130Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<IV00130> GetAllIV00130(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<IV00130> L = null;
                var tasks = _IV00130Repository.GetListWithPaging(x => x.ItemID.Contains(SearchTerm), PageSize, Page, ss => ss.ItemID, null);

                DataCollection<IV00130> result = tasks;
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
        public DataCollection<IV00130> GetByItemID(string ItemID, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<IV00130> L = null;
                var tasks = _IV00130Repository.GetListWithPaging(x => x.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => ss.ItemID, null);

                DataCollection<IV00130> result = tasks;
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

        public DataCollection<IV00130> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria,
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
                    SeachStr += Help.GetFilter("SiteID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BinID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BinName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsBinGroup", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemQuantity", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00130> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00130Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00130Repository.GetWhereListWithPaging("IV00130", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00130> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("SiteID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BinID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BinName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsBinGroup", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemQuantity", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00130> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00130Repository.GetListWithPaging(ss => ss.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00130Repository.GetWhereListWithPaging("IV00130", PageSize, Page, SeachStr, Member, IsAscending);
            
            return result;
        }

        public DataCollection<IV00130> GetAllSiteItemsViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("SiteID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BinID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BinName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsBinGroup", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemQuantity", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00130> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00130Repository.GetListWithPaging(ss => ss.SiteID.Trim() == SiteID.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00130Repository.GetWhereListWithPaging("IV00130", PageSize, Page, SeachStr, Member, IsAscending);

            return result;
        }

        public DataCollection<IV00130> GetAllSiteItemBinsViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string SiteID, string ItemID,
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
                    SeachStr += Help.GetFilter("SiteID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BinID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BinName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsBinGroup", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemQuantity", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00130> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00130Repository.GetListWithPaging(ss => ss.SiteID.Trim() == SiteID.Trim() && ss.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00130Repository.GetWhereListWithPaging("IV00130", PageSize, Page, SeachStr, Member, IsAscending);

            return result;
        }

        public IList<IV00130> GetAll()
        {
            var tasks = _IV00130Repository.GetAll();
            IList<IV00130> result = tasks;
            return result;
        }
        public IV00130 GetSingle(string ItemID, string SiteID)
        {
            var tasks = _IV00130Repository.GetSingle(x => x.ItemID.Trim() == ItemID.Trim() && x.SiteID.Trim() == SiteID);
            IV00130 result = tasks;
            return result;
        }
        public IList<IV00130> GetAllBySiteItem(string ItemID)
        {
            IList<IV00130> IV00130 = _IV00130Repository.GetAll(ss => ss.ItemID.Trim() == ItemID.Trim());
            return IV00130;
        }
        public IList<IV00130> GetAllBySite(string SiteID)
        {
            IList<IV00130> IV00130 = _IV00130Repository.GetAll(ss => ss.SiteID.Trim() == SiteID.Trim());
            return IV00130;
        }

        public KaizenResult AddIV00130(IV00130 newTask)
        {
            var result = _IV00130Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00130(IList<IV00130> newTask)
        {
            var result = _IV00130Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00130 newTask)
        {
            var result = _IV00130Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00130> newTask)
        {
            var result = _IV00130Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(IV00130 newTask)
        {
            var result = _IV00130Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00130> newTask)
        {
            var result = _IV00130Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
