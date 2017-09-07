using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00109Services
    {
        #region Infrastructure

        private IV00109Repository _IV00109Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00109Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00109Repository = new IV00109Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00109> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("VendorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VendorItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
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

            DataCollection<IV00109> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00109Repository.GetListWithPaging(ss => ss.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00109Repository.GetWhereListWithPaging("IV00109", PageSize, Page, SeachStr, Member, IsAscending);

            string str = string.Empty;
            if (result.Items != null)
            {
                foreach (var IV00109 in result.Items)
                {
                    if (IV00109 != null)
                    {
                        //if (IV00109.SalesAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.SalesAcc.ToString();
                        //    else
                        //        str += "," + IV00109.SalesAcc.ToString();
                        //}
                        //if (IV00109.SalesReturnAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.SalesReturnAcc.ToString();
                        //    else
                        //        str += "," + IV00109.SalesReturnAcc.ToString();
                        //}
                        //if (IV00109.MarkdownAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.MarkdownAcc.ToString();
                        //    else
                        //        str += "," + IV00109.MarkdownAcc.ToString();
                        //}

                        //if (IV00109.InventoryAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.InventoryAcc.ToString();
                        //    else
                        //        str += "," + IV00109.InventoryAcc.ToString();
                        //}
                        //if (IV00109.InventoryReturnAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.InventoryReturnAcc.ToString();
                        //    else
                        //        str += "," + IV00109.InventoryReturnAcc.ToString();
                        //}
                        //if (IV00109.InventoryOffsetAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.InventoryOffsetAcc.ToString();
                        //    else
                        //        str += "," + IV00109.InventoryOffsetAcc.ToString();
                        //}
                        //if (IV00109.TradeDiscountAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.TradeDiscountAcc.ToString();
                        //    else
                        //        str += "," + IV00109.TradeDiscountAcc.ToString();
                        //}
                        //if (IV00109.CostOfGoodsSold != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.CostOfGoodsSold.ToString();
                        //    else
                        //        str += "," + IV00109.CostOfGoodsSold.ToString();
                        //}
                        //if (IV00109.TaxAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.TaxAcc.ToString();
                        //    else
                        //        str += "," + IV00109.TaxAcc.ToString();
                        //}

                        Kaizen.Inventory.Repository.GL00100Repository oGL00100Repository = new GL00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                        string sql = "select * from GL00100 where AccountID in(" + str.Trim() + ")";
                        var tasks = oGL00100Repository.GetSQLData(sql);
                        DataCollection<GL00100> GL00100result = tasks;
                        if (GL00100result != null)
                        {
                            foreach (GL00100 o in GL00100result.Items)
                            {
                                //if (o.AccountID == IV00109.SalesAcc)
                                //    IV00109.SalesAccount = o;
                                //if (o.AccountID == IV00109.SalesReturnAcc)
                                //    IV00109.SalesReturnAccount = o;
                                //if (o.AccountID == IV00109.InventoryAcc)
                                //    IV00109.InventoryAccount = o;
                                //if (o.AccountID == IV00109.InventoryReturnAcc)
                                //    IV00109.InventoryReturnAccount = o;
                                //if (o.AccountID == IV00109.MarkdownAcc)
                                //    IV00109.MarkdownAccount = o;
                                //if (o.AccountID == IV00109.InventoryOffsetAcc)
                                //    IV00109.InventoryOffsetAccount = o;
                                //if (o.AccountID == IV00109.TradeDiscountAcc)
                                //    IV00109.TradeDiscountAccount = o;
                                //if (o.AccountID == IV00109.CostOfGoodsSold)
                                //    IV00109.CostOfGoodsSoldAccount = o;
                                //if (o.AccountID == IV00109.TaxAcc)
                                //    IV00109.TaxAccount = o;
                            }
                        }
                    }
                }
            }
            return result;
        }
        public DataCollection<IV00109> GetAllItemBYVendor(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string VendorID,
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
                    SeachStr += Help.GetFilter("VendorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VendorItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
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

            DataCollection<IV00109> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00109Repository.GetListWithPaging(ss => ss.VendorID.Trim() == VendorID.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00109Repository.GetWhereListWithPaging("IV00109", PageSize, Page, SeachStr, Member, IsAscending);

            string str = string.Empty;
            if (result.Items != null)
            {
                foreach (var IV00109 in result.Items)
                {
                    if (IV00109 != null)
                    {
                        //if (IV00109.SalesAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.SalesAcc.ToString();
                        //    else
                        //        str += "," + IV00109.SalesAcc.ToString();
                        //}
                        //if (IV00109.SalesReturnAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.SalesReturnAcc.ToString();
                        //    else
                        //        str += "," + IV00109.SalesReturnAcc.ToString();
                        //}
                        //if (IV00109.MarkdownAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.MarkdownAcc.ToString();
                        //    else
                        //        str += "," + IV00109.MarkdownAcc.ToString();
                        //}

                        //if (IV00109.InventoryAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.InventoryAcc.ToString();
                        //    else
                        //        str += "," + IV00109.InventoryAcc.ToString();
                        //}
                        //if (IV00109.InventoryReturnAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.InventoryReturnAcc.ToString();
                        //    else
                        //        str += "," + IV00109.InventoryReturnAcc.ToString();
                        //}
                        //if (IV00109.InventoryOffsetAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.InventoryOffsetAcc.ToString();
                        //    else
                        //        str += "," + IV00109.InventoryOffsetAcc.ToString();
                        //}
                        //if (IV00109.TradeDiscountAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.TradeDiscountAcc.ToString();
                        //    else
                        //        str += "," + IV00109.TradeDiscountAcc.ToString();
                        //}
                        //if (IV00109.CostOfGoodsSold != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.CostOfGoodsSold.ToString();
                        //    else
                        //        str += "," + IV00109.CostOfGoodsSold.ToString();
                        //}
                        //if (IV00109.TaxAcc != null)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        str += IV00109.TaxAcc.ToString();
                        //    else
                        //        str += "," + IV00109.TaxAcc.ToString();
                        //}

                        Kaizen.Inventory.Repository.GL00100Repository oGL00100Repository = new GL00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                        string sql = "select * from GL00100 where AccountID in(" + str.Trim() + ")";
                        var tasks = oGL00100Repository.GetSQLData(sql);
                        DataCollection<GL00100> GL00100result = tasks;
                        if (GL00100result != null)
                        {
                            foreach (GL00100 o in GL00100result.Items)
                            {
                                //if (o.AccountID == IV00109.SalesAcc)
                                //    IV00109.SalesAccount = o;
                                //if (o.AccountID == IV00109.SalesReturnAcc)
                                //    IV00109.SalesReturnAccount = o;
                                //if (o.AccountID == IV00109.InventoryAcc)
                                //    IV00109.InventoryAccount = o;
                                //if (o.AccountID == IV00109.InventoryReturnAcc)
                                //    IV00109.InventoryReturnAccount = o;
                                //if (o.AccountID == IV00109.MarkdownAcc)
                                //    IV00109.MarkdownAccount = o;
                                //if (o.AccountID == IV00109.InventoryOffsetAcc)
                                //    IV00109.InventoryOffsetAccount = o;
                                //if (o.AccountID == IV00109.TradeDiscountAcc)
                                //    IV00109.TradeDiscountAccount = o;
                                //if (o.AccountID == IV00109.CostOfGoodsSold)
                                //    IV00109.CostOfGoodsSoldAccount = o;
                                //if (o.AccountID == IV00109.TaxAcc)
                                //    IV00109.TaxAccount = o;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public DataCollection<IV00109> GetAllIV00109(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            var tasks = _IV00109Repository.GetListWithPaging(x => x.ItemID.Contains(SearchTerm), PageSize, Page, ss => ss.ItemID, null);
            DataCollection<IV00109> result = tasks;
            return result;
        }
        public DataCollection<IV00109> GetByItemID(string ItemID, int PageSize, int Page, string Member, string SortDirection)
        {
            var tasks = _IV00109Repository.GetListWithPaging(x => x.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => ss.ItemID, null);
            DataCollection<IV00109> result = tasks;
            return result;
        }
        public IV00109 GetSingle(string ItemID, string VendorID)
        {
            var tasks = _IV00109Repository.GetSingle(x => x.ItemID.Trim() == ItemID.Trim() && x.VendorID.Trim() == VendorID);
            IV00109 result = tasks;
            return result;
        }

        public IList<IV00109> GetAll()
        {
            var tasks = _IV00109Repository.GetAll();
            IList<IV00109> result = tasks;
            return result;
        }
        public IList<IV00109> GetAllByVendorItem(string ItemID)
        {
            IList<IV00109> IV00109 = _IV00109Repository.GetAll(ss => ss.ItemID.Trim() == ItemID.Trim());
            return IV00109;
        }
        public IList<IV00109> GetAllByVendor(string VendorID)
        {
            IList<IV00109> IV00109 = _IV00109Repository.GetAll(ss => ss.VendorID.Trim() == VendorID.Trim());
            return IV00109;
        }

        public KaizenResult AddIV00109(IV00109 newTask)
        {
            var result = _IV00109Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00109(IList<IV00109> newTask)
        {
            var result = _IV00109Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00109 newTask)
        {
            var result = _IV00109Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00109> newTask)
        {
            var result = _IV00109Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(IV00109 newTask)
        {
            var result = _IV00109Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00109> newTask)
        {
            var result = _IV00109Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
