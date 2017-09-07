using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00131Services
    {
        #region Infrastructure

        private IV00131Repository _IV00131Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00131Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00131Repository = new IV00131Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<IV00131> GetAllIV00131(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<IV00131> L = null;
                var tasks = _IV00131Repository.GetListWithPaging(x => x.ItemID.Contains(SearchTerm), PageSize, Page, ss => ss.ItemID, null);

                DataCollection<IV00131> result = tasks;
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
        public DataCollection<IV00131> GetByItemID(string ItemID, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<IV00131> L = null;
                var tasks = _IV00131Repository.GetListWithPaging(x => x.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => ss.ItemID, null);

                DataCollection<IV00131> result = tasks;
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
        public DataCollection<IV00131> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<IV00131> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00131Repository.GetListWithPaging(ss => ss.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00131Repository.GetWhereListWithPaging("IV00131", PageSize, Page, SeachStr, Member, IsAscending);
            
            return result;
        }

        public DataCollection<IV00131> GetAllSiteItemsViewBYSQLQuery(string Filters,
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

            DataCollection<IV00131> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00131Repository.GetListWithPaging(ss => ss.SiteID.Trim() == SiteID.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00131Repository.GetWhereListWithPaging("IV00131", PageSize, Page, SeachStr, Member, IsAscending);

            return result;
        }

        public DataCollection<IV00131> GetAllSiteItemBinsViewBYSQLQuery(string Filters,
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

            DataCollection<IV00131> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00131Repository.GetListWithPaging(ss => ss.SiteID.Trim() == SiteID.Trim() && ss.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00131Repository.GetWhereListWithPaging("IV00131", PageSize, Page, SeachStr, Member, IsAscending);

            return result;
        }

        public DataCollection<IV00131> GetAllSiteItemSubBinsViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string SiteID, string ItemID, string BinID,
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
                    SeachStr += Help.GetFilter("SubBinID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemQuantity", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00131> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00131Repository.GetListWithPaging(ss => ss.SiteID.Trim() == SiteID.Trim() && ss.ItemID.Trim() == ItemID.Trim() && ss.BinID.Trim() == BinID.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00131Repository.GetWhereListWithPaging("IV00131", PageSize, Page, SeachStr, Member, IsAscending);

            return result;
        }

        public IList<IV00131> GetAll()
        {
            var tasks = _IV00131Repository.GetAll();
            IList<IV00131> result = tasks;
            return result;
        }
        public IV00131 GetSingle(string ItemID, string SiteID)
        {
            var tasks = _IV00131Repository.GetSingle(x => x.ItemID.Trim() == ItemID.Trim() && x.SiteID.Trim() == SiteID);
            IV00131 result = tasks;
            return result;
        }
        public IList<IV00131> GetAllBySiteItem(string ItemID)
        {
            IList<IV00131> IV00131 = _IV00131Repository.GetAll(ss => ss.ItemID.Trim() == ItemID.Trim());
            return IV00131;
        }
        public IList<IV00131> GetAllBySite(string SiteID)
        {
            IList<IV00131> IV00131 = _IV00131Repository.GetAll(ss => ss.SiteID.Trim() == SiteID.Trim());
            return IV00131;
        }
        public IList<IV00131> GetAllByBinID(string BinID)
        {
            IList<IV00131> IV00131 = _IV00131Repository.GetAll(ss => ss.BinID.Trim() == BinID.Trim());
            return IV00131;
        }

        public KaizenResult AddIV00131(IV00131 newTask)
        {
            var result = _IV00131Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00131(IList<IV00131> newTask)
        {
            var result = _IV00131Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00131 newTask)
        {
            var result = _IV00131Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00131> newTask)
        {
            var result = _IV00131Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(IV00131 newTask)
        {
            var result = _IV00131Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00131> newTask)
        {
            var result = _IV00131Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
