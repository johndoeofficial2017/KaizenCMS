using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00102Services
    {
        #region Infrastructure

        private IV00102Repository _IV00102Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00102Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00102Repository = new IV00102Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<IV00102> GetAllIV00102(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<IV00102> L = null;
                var tasks = _IV00102Repository.GetListWithPaging(x => x.ItemID.Contains(SearchTerm), PageSize, Page, ss => ss.ItemID, null);

                DataCollection<IV00102> result = tasks;
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
        public DataCollection<IV00102> GetByItemID(string ItemID, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<IV00102> L = null;
                var tasks = _IV00102Repository.GetListWithPaging(x => x.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => ss.ItemID, null);

                DataCollection<IV00102> result = tasks;
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
        public DataCollection<IV00102> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("PrimaryVendor", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ShortDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GenericDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BarCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CostOfGoodsSold", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SalesAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SalesReturnAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MarkdownAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InventoryAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InventoryReturnAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InventoryOffsetAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FreightAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TradeDiscountAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TaxAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00102> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00102Repository.GetListWithPaging(ss => ss.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00102Repository.GetWhereListWithPaging("IV00102", PageSize, Page, SeachStr, Member, IsAscending);

            string str = string.Empty;
            foreach (var IV00102 in result.Items)
            {
                if (IV00102 != null)
                {
                    if (IV00102.SalesAcc.HasValue && IV00102.SalesAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.SalesAcc.Value.ToString();
                        else
                            str += "," + IV00102.SalesAcc.Value.ToString();
                    }
                    if (IV00102.SalesReturnAcc.HasValue && IV00102.SalesReturnAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.SalesReturnAcc.Value.ToString();
                        else
                            str += "," + IV00102.SalesReturnAcc.Value.ToString();
                    }
                    if (IV00102.MarkdownAcc.HasValue && IV00102.MarkdownAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.MarkdownAcc.Value.ToString();
                        else
                            str += "," + IV00102.MarkdownAcc.Value.ToString();
                    }

                    if (IV00102.InventoryAcc.HasValue && IV00102.InventoryAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.InventoryAcc.Value.ToString();
                        else
                            str += "," + IV00102.InventoryAcc.Value.ToString();
                    }
                    if (IV00102.InventoryReturnAcc.HasValue && IV00102.InventoryReturnAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.InventoryReturnAcc.Value.ToString();
                        else
                            str += "," + IV00102.InventoryReturnAcc.Value.ToString();
                    }
                    if (IV00102.InventoryOffsetAcc.HasValue && IV00102.InventoryOffsetAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.InventoryOffsetAcc.Value.ToString();
                        else
                            str += "," + IV00102.InventoryOffsetAcc.Value.ToString();
                    }
                    if (IV00102.FreightAcc.HasValue && IV00102.FreightAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.FreightAcc.Value.ToString();
                        else
                            str += "," + IV00102.FreightAcc.Value.ToString();
                    }
                    if (IV00102.TradeDiscountAcc.HasValue && IV00102.TradeDiscountAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.TradeDiscountAcc.Value.ToString();
                        else
                            str += "," + IV00102.TradeDiscountAcc.Value.ToString();
                    }
                    if (IV00102.CostOfGoodsSold.HasValue && IV00102.CostOfGoodsSold != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.CostOfGoodsSold.Value.ToString();
                        else
                            str += "," + IV00102.CostOfGoodsSold.Value.ToString();
                    }
                    if (IV00102.TaxAcc.HasValue && IV00102.TaxAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.TaxAcc.Value.ToString();
                        else
                            str += "," + IV00102.TaxAcc.Value.ToString();
                    }

                    Kaizen.Inventory.Repository.GL00100Repository oGL00100Repository = new GL00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    string sql = "select * from GL00100 where AccountID in(" + str.Trim() + ")";
                    var tasks = oGL00100Repository.GetSQLData(sql);
                    DataCollection<GL00100> GL00100result = tasks;
                    if (GL00100result != null)
                    {
                        foreach (GL00100 o in GL00100result.Items)
                        {
                            if (o.AccountID == IV00102.SalesAcc)
                                IV00102.SalesAccount = o;
                            if (o.AccountID == IV00102.SalesReturnAcc)
                                IV00102.SalesReturnAccount = o;
                            if (o.AccountID == IV00102.InventoryAcc)
                                IV00102.InventoryAccount = o;
                            if (o.AccountID == IV00102.InventoryReturnAcc)
                                IV00102.InventoryReturnAccount = o;
                            if (o.AccountID == IV00102.MarkdownAcc)
                                IV00102.MarkdownAccount = o;
                            if (o.AccountID == IV00102.InventoryOffsetAcc)
                                IV00102.InventoryOffsetAccount = o;
                            if (o.AccountID == IV00102.FreightAcc)
                                IV00102.FreightAccount = o;
                            if (o.AccountID == IV00102.TradeDiscountAcc)
                                IV00102.TradeDiscountAccount = o;
                            if (o.AccountID == IV00102.CostOfGoodsSold)
                                IV00102.CostOfGoodsSoldAccount = o;
                            if (o.AccountID == IV00102.TaxAcc)
                                IV00102.TaxAccount = o;
                        }
                    }
                }
            }
            return result;
        }

        public DataCollection<IV00102> GetAllSiteItemsViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("PrimaryVendor", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ShortDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GenericDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BarCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CostOfGoodsSold", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SalesAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SalesReturnAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MarkdownAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InventoryAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InventoryReturnAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InventoryOffsetAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FreightAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TradeDiscountAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TaxAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00102> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00102Repository.GetListWithPaging(ss => ss.SiteID.Trim() == SiteID.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00102Repository.GetWhereListWithPaging("IV00102", PageSize, Page, SeachStr, Member, IsAscending);

            string str = string.Empty;
            foreach (var IV00102 in result.Items)
            {
                if (IV00102 != null)
                {
                    if (IV00102.SalesAcc.HasValue && IV00102.SalesAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.SalesAcc.Value.ToString();
                        else
                            str += "," + IV00102.SalesAcc.Value.ToString();
                    }
                    if (IV00102.SalesReturnAcc.HasValue && IV00102.SalesReturnAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.SalesReturnAcc.Value.ToString();
                        else
                            str += "," + IV00102.SalesReturnAcc.Value.ToString();
                    }
                    if (IV00102.MarkdownAcc.HasValue && IV00102.MarkdownAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.MarkdownAcc.Value.ToString();
                        else
                            str += "," + IV00102.MarkdownAcc.Value.ToString();
                    }

                    if (IV00102.InventoryAcc.HasValue && IV00102.InventoryAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.InventoryAcc.Value.ToString();
                        else
                            str += "," + IV00102.InventoryAcc.Value.ToString();
                    }
                    if (IV00102.InventoryReturnAcc.HasValue && IV00102.InventoryReturnAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.InventoryReturnAcc.Value.ToString();
                        else
                            str += "," + IV00102.InventoryReturnAcc.Value.ToString();
                    }
                    if (IV00102.InventoryOffsetAcc.HasValue && IV00102.InventoryOffsetAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.InventoryOffsetAcc.Value.ToString();
                        else
                            str += "," + IV00102.InventoryOffsetAcc.Value.ToString();
                    }
                    if (IV00102.FreightAcc.HasValue && IV00102.FreightAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.FreightAcc.Value.ToString();
                        else
                            str += "," + IV00102.FreightAcc.Value.ToString();
                    }
                    if (IV00102.TradeDiscountAcc.HasValue && IV00102.TradeDiscountAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.TradeDiscountAcc.Value.ToString();
                        else
                            str += "," + IV00102.TradeDiscountAcc.Value.ToString();
                    }
                    if (IV00102.CostOfGoodsSold.HasValue && IV00102.CostOfGoodsSold != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.CostOfGoodsSold.Value.ToString();
                        else
                            str += "," + IV00102.CostOfGoodsSold.Value.ToString();
                    }
                    if (IV00102.TaxAcc.HasValue && IV00102.TaxAcc != null)
                    {
                        if (string.IsNullOrEmpty(str))
                            str += IV00102.TaxAcc.Value.ToString();
                        else
                            str += "," + IV00102.TaxAcc.Value.ToString();
                    }

                    Kaizen.Inventory.Repository.GL00100Repository oGL00100Repository = new GL00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    string sql = "select * from GL00100 where AccountID in(" + str.Trim() + ")";
                    var tasks = oGL00100Repository.GetSQLData(sql);
                    DataCollection<GL00100> GL00100result = tasks;
                    if (GL00100result != null)
                    {
                        foreach (GL00100 o in GL00100result.Items)
                        {
                            if (o.AccountID == IV00102.SalesAcc)
                                IV00102.SalesAccount = o;
                            if (o.AccountID == IV00102.SalesReturnAcc)
                                IV00102.SalesReturnAccount = o;
                            if (o.AccountID == IV00102.InventoryAcc)
                                IV00102.InventoryAccount = o;
                            if (o.AccountID == IV00102.InventoryReturnAcc)
                                IV00102.InventoryReturnAccount = o;
                            if (o.AccountID == IV00102.MarkdownAcc)
                                IV00102.MarkdownAccount = o;
                            if (o.AccountID == IV00102.InventoryOffsetAcc)
                                IV00102.InventoryOffsetAccount = o;
                            if (o.AccountID == IV00102.FreightAcc)
                                IV00102.FreightAccount = o;
                            if (o.AccountID == IV00102.TradeDiscountAcc)
                                IV00102.TradeDiscountAccount = o;
                            if (o.AccountID == IV00102.CostOfGoodsSold)
                                IV00102.CostOfGoodsSoldAccount = o;
                            if (o.AccountID == IV00102.TaxAcc)
                                IV00102.TaxAccount = o;
                        }
                    }
                }
            }
            return result;
        }

        public IList<IV00102> GetAll()
        {
            var tasks = _IV00102Repository.GetAll();
            IList<IV00102> result = tasks;
            return result;
        }
        public IV00102 GetSingle(string ItemID, string SiteID)
        {
            var tasks = _IV00102Repository.GetSingle(x => x.ItemID.Trim() == ItemID.Trim() && x.SiteID.Trim() == SiteID);
            IV00102 result = tasks;
            return result;
        }
        public IList<IV00102> GetAllBySiteItem(string ItemID)
        {
            IList<IV00102> IV00102 = _IV00102Repository.GetAll(ss => ss.ItemID.Trim() == ItemID.Trim());
            return IV00102;
        }
        public IList<IV00102> GetAllBySite(string SiteID)
        {
            IList<IV00102> IV00102 = _IV00102Repository.GetAll(ss => ss.SiteID.Trim() == SiteID.Trim());
            return IV00102;
        }

        public KaizenResult AddIV00102(IV00102 newTask)
        {
            var result = _IV00102Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00102(IList<IV00102> newTask)
        {
            var result = _IV00102Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00102 newTask)
        {
            var result = _IV00102Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00102> newTask)
        {
            var result = _IV00102Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(IV00102 newTask)
        {
            var result = _IV00102Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00102> newTask)
        {
            var result = _IV00102Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
