using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00108Services
    {
        #region Infrastructure

        private IV00108Repository _IV00108Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00108Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00108Repository = new IV00108Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00108> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CUSTNMBR", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CustomerItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
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

            DataCollection<IV00108> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00108Repository.GetListWithPaging(ss => ss.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00108Repository.GetWhereListWithPaging("IV00108", PageSize, Page, SeachStr, Member, IsAscending);

            string str = string.Empty;
            if (result.Items != null)
            {
                foreach (var IV00108 in result.Items)
                {
                    if (IV00108 != null)
                    {
                        if (IV00108.SalesAcc.HasValue && IV00108.SalesAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.SalesAcc.Value.ToString();
                            else
                                str += "," + IV00108.SalesAcc.Value.ToString();
                        }
                        if (IV00108.SalesReturnAcc.HasValue && IV00108.SalesReturnAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.SalesReturnAcc.Value.ToString();
                            else
                                str += "," + IV00108.SalesReturnAcc.Value.ToString();
                        }
                        if (IV00108.MarkdownAcc.HasValue && IV00108.MarkdownAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.MarkdownAcc.Value.ToString();
                            else
                                str += "," + IV00108.MarkdownAcc.Value.ToString();
                        }

                        if (IV00108.InventoryAcc.HasValue && IV00108.InventoryAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.InventoryAcc.Value.ToString();
                            else
                                str += "," + IV00108.InventoryAcc.Value.ToString();
                        }
                        if (IV00108.InventoryReturnAcc.HasValue && IV00108.InventoryReturnAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.InventoryReturnAcc.Value.ToString();
                            else
                                str += "," + IV00108.InventoryReturnAcc.Value.ToString();
                        }
                        if (IV00108.InventoryOffsetAcc.HasValue && IV00108.InventoryOffsetAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.InventoryOffsetAcc.Value.ToString();
                            else
                                str += "," + IV00108.InventoryOffsetAcc.Value.ToString();
                        }
                        if (IV00108.FreightAcc.HasValue && IV00108.FreightAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.FreightAcc.Value.ToString();
                            else
                                str += "," + IV00108.FreightAcc.Value.ToString();
                        }
                        if (IV00108.TradeDiscountAcc.HasValue && IV00108.TradeDiscountAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.TradeDiscountAcc.Value.ToString();
                            else
                                str += "," + IV00108.TradeDiscountAcc.Value.ToString();
                        }
                        if (IV00108.CostOfGoodsSold.HasValue && IV00108.CostOfGoodsSold != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.CostOfGoodsSold.Value.ToString();
                            else
                                str += "," + IV00108.CostOfGoodsSold.Value.ToString();
                        }
                        if (IV00108.TaxAcc.HasValue && IV00108.TaxAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.TaxAcc.Value.ToString();
                            else
                                str += "," + IV00108.TaxAcc.Value.ToString();
                        }

                        Kaizen.Inventory.Repository.GL00100Repository oGL00100Repository = new GL00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                        string sql = "select * from GL00100 where AccountID in(" + str.Trim() + ")";
                        var tasks = oGL00100Repository.GetSQLData(sql);
                        DataCollection<GL00100> GL00100result = tasks;
                        if (GL00100result != null)
                        {
                            foreach (GL00100 o in GL00100result.Items)
                            {
                                if (o.AccountID == IV00108.SalesAcc)
                                    IV00108.SalesAccount = o;
                                if (o.AccountID == IV00108.SalesReturnAcc)
                                    IV00108.SalesReturnAccount = o;
                                if (o.AccountID == IV00108.InventoryAcc)
                                    IV00108.InventoryAccount = o;
                                if (o.AccountID == IV00108.InventoryReturnAcc)
                                    IV00108.InventoryReturnAccount = o;
                                if (o.AccountID == IV00108.MarkdownAcc)
                                    IV00108.MarkdownAccount = o;
                                if (o.AccountID == IV00108.InventoryOffsetAcc)
                                    IV00108.InventoryOffsetAccount = o;
                                if (o.AccountID == IV00108.FreightAcc)
                                    IV00108.FreightAccount = o;
                                if (o.AccountID == IV00108.TradeDiscountAcc)
                                    IV00108.TradeDiscountAccount = o;
                                if (o.AccountID == IV00108.CostOfGoodsSold)
                                    IV00108.CostOfGoodsSoldAccount = o;
                                if (o.AccountID == IV00108.TaxAcc)
                                    IV00108.TaxAccount = o;
                            }
                        }
                    }
                }
            }
            return result;
        }
        public DataCollection<IV00108> GetAllItemBYCustomer(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string CUSTNMBR,
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
                    SeachStr += Help.GetFilter("CUSTNMBR", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CustomerItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
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
                    SeachStr += Help.GetFilter("TradeDiscountAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TaxAcc", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00108> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00108Repository.GetListWithPaging(ss => ss.CUSTNMBR.Trim() == CUSTNMBR.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00108Repository.GetWhereListWithPaging("IV00108", PageSize, Page, SeachStr, Member, IsAscending);

            string str = string.Empty;
            if (result.Items != null)
            {
                foreach (var IV00108 in result.Items)
                {
                    if (IV00108 != null)
                    {
                        if (IV00108.SalesAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.SalesAcc.ToString();
                            else
                                str += "," + IV00108.SalesAcc.ToString();
                        }
                        if (IV00108.SalesReturnAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.SalesReturnAcc.ToString();
                            else
                                str += "," + IV00108.SalesReturnAcc.ToString();
                        }
                        if (IV00108.MarkdownAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.MarkdownAcc.ToString();
                            else
                                str += "," + IV00108.MarkdownAcc.ToString();
                        }

                        if (IV00108.InventoryAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.InventoryAcc.ToString();
                            else
                                str += "," + IV00108.InventoryAcc.ToString();
                        }
                        if (IV00108.InventoryReturnAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.InventoryReturnAcc.ToString();
                            else
                                str += "," + IV00108.InventoryReturnAcc.ToString();
                        }
                        if (IV00108.InventoryOffsetAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.InventoryOffsetAcc.ToString();
                            else
                                str += "," + IV00108.InventoryOffsetAcc.ToString();
                        }
                        if (IV00108.TradeDiscountAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.TradeDiscountAcc.ToString();
                            else
                                str += "," + IV00108.TradeDiscountAcc.ToString();
                        }
                        if (IV00108.CostOfGoodsSold != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.CostOfGoodsSold.ToString();
                            else
                                str += "," + IV00108.CostOfGoodsSold.ToString();
                        }
                        if (IV00108.TaxAcc != null)
                        {
                            if (string.IsNullOrEmpty(str))
                                str += IV00108.TaxAcc.ToString();
                            else
                                str += "," + IV00108.TaxAcc.ToString();
                        }

                        Kaizen.Inventory.Repository.GL00100Repository oGL00100Repository = new GL00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                        string sql = "select * from GL00100 where AccountID in(" + str.Trim() + ")";
                        var tasks = oGL00100Repository.GetSQLData(sql);
                        DataCollection<GL00100> GL00100result = tasks;
                        if (GL00100result != null)
                        {
                            foreach (GL00100 o in GL00100result.Items)
                            {
                                if (o.AccountID == IV00108.SalesAcc)
                                    IV00108.SalesAccount = o;
                                if (o.AccountID == IV00108.SalesReturnAcc)
                                    IV00108.SalesReturnAccount = o;
                                if (o.AccountID == IV00108.InventoryAcc)
                                    IV00108.InventoryAccount = o;
                                if (o.AccountID == IV00108.InventoryReturnAcc)
                                    IV00108.InventoryReturnAccount = o;
                                if (o.AccountID == IV00108.MarkdownAcc)
                                    IV00108.MarkdownAccount = o;
                                if (o.AccountID == IV00108.InventoryOffsetAcc)
                                    IV00108.InventoryOffsetAccount = o;
                                if (o.AccountID == IV00108.TradeDiscountAcc)
                                    IV00108.TradeDiscountAccount = o;
                                if (o.AccountID == IV00108.CostOfGoodsSold)
                                    IV00108.CostOfGoodsSoldAccount = o;
                                if (o.AccountID == IV00108.TaxAcc)
                                    IV00108.TaxAccount = o;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public DataCollection<IV00108> GetAllIV00108(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<IV00108> L = null;
                var tasks = _IV00108Repository.GetListWithPaging(x => x.ItemID.Contains(SearchTerm), PageSize, Page, ss => ss.ItemID, null);

                DataCollection<IV00108> result = tasks;
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
        public DataCollection<IV00108> GetByItemID(string ItemID, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<IV00108> L = null;
                var tasks = _IV00108Repository.GetListWithPaging(x => x.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => ss.ItemID, null);

                DataCollection<IV00108> result = tasks;
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
        public IV00108 GetSingle(string ItemID, string CUSTNMBR)
        {
            var tasks = _IV00108Repository.GetSingle(x => x.ItemID.Trim() == ItemID.Trim() && x.CUSTNMBR.Trim() == CUSTNMBR);
            IV00108 result = tasks;
            return result;
        }

        public IList<IV00108> GetAll()
        {
            var tasks = _IV00108Repository.GetAll();
            IList<IV00108> result = tasks;
            return result;
        }
        public IList<IV00108> GetAllByCustomerItem(string ItemID)
        {
            IList<IV00108> IV00108 = _IV00108Repository.GetAll(ss => ss.ItemID.Trim() == ItemID.Trim());
            return IV00108;
        }
        public IList<IV00108> GetAllByCustomer(string CUSTNMBR)
        {
            IList<IV00108> IV00108 = _IV00108Repository.GetAll(ss => ss.CUSTNMBR.Trim() == CUSTNMBR.Trim());
            return IV00108;
        }

        public KaizenResult AddIV00108(IV00108 newTask)
        {
            var result = _IV00108Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00108(IList<IV00108> newTask)
        {
            var result = _IV00108Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00108 newTask)
        {
            var result = _IV00108Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00108> newTask)
        {
            var result = _IV00108Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(IV00108 newTask)
        {
            var result = _IV00108Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00108> newTask)
        {
            var result = _IV00108Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
