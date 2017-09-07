using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00100Services
    {
        #region Infrastructure

        private IV00100Repository _IV00100Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        private Kaizen.Configuration.Config00100 iInventoryConfig;

        private IV00150Repository _IV00150Repository;
        private IV00151Repository _IV00151Repository;
        private IV00152Repository _IV00152Repository;
        public Kaizen.Configuration.Config00100 InventoryConfig
        {
            get
            {
                return Master.GetInventoryConfig(this.UserContext.CompanyID);
            }
        }
        public IV00100Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00100Repository = new IV00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            _IV00150Repository = new IV00150Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            _IV00151Repository = new IV00151Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            _IV00152Repository = new IV00152Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00100> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00100> result = null;
            //if (string.IsNullOrEmpty(SeachStr))
            //    result = _IV00100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            //else
            result = _IV00100Repository.GetWhereListWithPaging("IV00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00151> GetAllViewBYSQLQueryIV00151(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00151> result = null;
            //if (string.IsNullOrEmpty(SeachStr))
            //    result = _IV00151Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            //else
            result = _IV00151Repository.GetWhereListWithPaging("IV00151", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00102Item> GetDataListGridBySite(string Filters, string FieldID, int FltrOperator
            , string SiteID,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (!string.IsNullOrEmpty(Filters))
                Filters = " and";

            SeachStr = " SiteID='" + SiteID + "'";
            IV00102ItemRepository rep = new IV00102ItemRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<IV00102Item> result = null;
            result = rep.GetWhereListWithPaging("IV00102Item", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00100> GetTop10BYItemID(string ItemID)
        {
            string SeachStr = string.Empty;
            SeachStr = "ItemID Like '" + ItemID.Trim() + "%'";
            var result = _IV00100Repository.GetWhereListWithPaging("IV00100", 10, 1, SeachStr, "ItemID", true);
            return result;
        }

        public DataCollection<IV00100> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BarCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
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

            DataCollection<IV00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00100Repository.GetWhereListWithPaging("IV00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00100> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, int ItemTypeID,
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
                    SeachStr += Help.GetFilter("ItemName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BarCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
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

            DataCollection<IV00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _IV00100Repository.GetListWithPaging(ss => ss.ItemTypeID == ItemTypeID,
                    PageSize, Page, ss => Member);
            }
            else
            {
                SeachStr += " and ItemTypeID='" + ItemTypeID + "'";
                result = _IV00100Repository.GetWhereListWithPaging("IV00100", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<IV00100> GetAllGroupItemsViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BarCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
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

            DataCollection<IV00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00100Repository.GetListWithPaging(ss => ss.ItemTypeID == 7, PageSize, Page, ss => Member);
            else
                result = _IV00100Repository.GetWhereListWithPaging("IV00100", PageSize, Page, SeachStr, Member, IsAscending);

            return result;
        }

        public DataCollection<IV00100> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<IV00100> L = null;
            var tasks = _IV00100Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<IV00100> result = tasks;
            return result;
        }
        public DataCollection<IV00100> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<IV00100> result = null;
            var tasks = _IV00100Repository.GetListWithPaging(PageSize, Page, ss => Member, null, ss => ss.IV00104);
            result = tasks;
            return result;
        }
        public DataCollection<IV00100> GetKitListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<IV00100> result = null;
            var tasks = _IV00100Repository.GetListWithPaging(ww => ww.ItemTypeID == 4, PageSize, Page, ss => new { ss.ItemID });
            result = tasks;
            return result;
        }
        public List<IV00100> GetAllbstituteItem(string ItemID)
        {
            string sql = "select IV00100.* from IV00100 inner join IV00105 on IV00100.ItemID = IV00105.SubstituteItem where IV00105.ItemID='" + ItemID.Trim() + "'";
            var tasks = _IV00100Repository.GetSQLData(sql);
            DataCollection<IV00100> result = tasks;
            return result.Items;
        }
        public List<IV00100> GetAllItemKit(string ItemID)
        {
            string sql = "select IV00100.* from IV00100 inner join IV00107 on IV00100.ItemID = IV00107.ItemKIT where IV00107.ItemID='" + ItemID.Trim() + "'";
            var tasks = _IV00100Repository.GetSQLData(sql);
            DataCollection<IV00100> result = tasks;
            return result.Items;
        }
        public IV00108 GetGLByCustomer(string ItemID, string CUSTNMBR)
        {
            Kaizen.Inventory.Repository.IV00108Repository _IV00108Repository = new IV00108Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IV00108 oIV00108 = _IV00108Repository.GetSingle(ss => ss.ItemID.Trim() == ItemID.Trim() && ss.CUSTNMBR.Trim() == CUSTNMBR.Trim());
            string str = string.Empty;
            if (oIV00108 != null)
            {
                if (oIV00108.SalesAcc.HasValue && oIV00108.SalesAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += oIV00108.SalesAcc.Value.ToString();
                    else
                        str += "," + oIV00108.SalesAcc.Value.ToString();
                }
                if (oIV00108.SalesReturnAcc.HasValue && oIV00108.SalesReturnAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += oIV00108.SalesReturnAcc.Value.ToString();
                    else
                        str += "," + oIV00108.SalesReturnAcc.Value.ToString();
                }
                if (oIV00108.MarkdownAcc.HasValue && oIV00108.MarkdownAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += oIV00108.MarkdownAcc.Value.ToString();
                    else
                        str += "," + oIV00108.MarkdownAcc.Value.ToString();
                }

                if (oIV00108.InventoryAcc.HasValue && oIV00108.InventoryAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += oIV00108.InventoryAcc.Value.ToString();
                    else
                        str += "," + oIV00108.InventoryAcc.Value.ToString();
                }
                if (oIV00108.InventoryReturnAcc.HasValue && oIV00108.InventoryReturnAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += oIV00108.InventoryReturnAcc.Value.ToString();
                    else
                        str += "," + oIV00108.InventoryReturnAcc.Value.ToString();
                }
                if (oIV00108.InventoryOffsetAcc.HasValue && oIV00108.InventoryOffsetAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += oIV00108.InventoryOffsetAcc.Value.ToString();
                    else
                        str += "," + oIV00108.InventoryOffsetAcc.Value.ToString();
                }
                if (oIV00108.FreightAcc.HasValue && oIV00108.FreightAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += oIV00108.FreightAcc.Value.ToString();
                    else
                        str += "," + oIV00108.FreightAcc.Value.ToString();
                }
                if (oIV00108.TradeDiscountAcc.HasValue && oIV00108.TradeDiscountAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += oIV00108.TradeDiscountAcc.Value.ToString();
                    else
                        str += "," + oIV00108.TradeDiscountAcc.Value.ToString();
                }
                if (oIV00108.CostOfGoodsSold.HasValue && oIV00108.CostOfGoodsSold != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += oIV00108.CostOfGoodsSold.Value.ToString();
                    else
                        str += "," + oIV00108.CostOfGoodsSold.Value.ToString();
                }
                if (oIV00108.TaxAcc.HasValue && oIV00108.TaxAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += oIV00108.TaxAcc.Value.ToString();
                    else
                        str += "," + oIV00108.TaxAcc.Value.ToString();
                }

                Kaizen.Inventory.Repository.GL00100Repository oGL00100Repository = new GL00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                string sql = "select * from GL00100 where AccountID in(" + str.Trim() + ")";
                var tasks = oGL00100Repository.GetSQLData(sql);
                DataCollection<GL00100> result = tasks;
                foreach (GL00100 o in result.Items)
                {
                    if (o.AccountID == oIV00108.SalesAcc)
                        oIV00108.SalesAccount = o;
                    if (o.AccountID == oIV00108.SalesReturnAcc)
                        oIV00108.SalesReturnAccount = o;
                    if (o.AccountID == oIV00108.InventoryAcc)
                        oIV00108.InventoryAccount = o;
                    if (o.AccountID == oIV00108.InventoryReturnAcc)
                        oIV00108.InventoryReturnAccount = o;
                    if (o.AccountID == oIV00108.MarkdownAcc)
                        oIV00108.MarkdownAccount = o;
                    if (o.AccountID == oIV00108.InventoryOffsetAcc)
                        oIV00108.InventoryOffsetAccount = o;
                    if (o.AccountID == oIV00108.FreightAcc)
                        oIV00108.FreightAccount = o;
                    if (o.AccountID == oIV00108.TradeDiscountAcc)
                        oIV00108.TradeDiscountAccount = o;
                    if (o.AccountID == oIV00108.CostOfGoodsSold)
                        oIV00108.CostOfGoodsSoldAccount = o;
                    if (o.AccountID == oIV00108.TaxAcc)
                        oIV00108.TaxAccount = o;
                }
            }

            return oIV00108;
        }

        public IV00102 GetGLBySite(string ItemID, string SiteID)
        {
            IV00102Repository _IV00102Repository = new IV00102Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IV00102 IV00102 = _IV00102Repository.GetSingle(ss => ss.ItemID.Trim() == ItemID.Trim() && ss.SiteID.Trim() == SiteID.Trim());
            string str = string.Empty;
            if (IV00102 != null)
            {
                if (IV00102.PurchaseID.HasValue && IV00102.PurchaseID != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += IV00102.PurchaseID.Value.ToString();
                    else
                        str += "," + IV00102.PurchaseID.Value.ToString();
                }
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
                GL00100Repository oGL00100Repository = new GL00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                string sql = "select * from GL00100 where AccountID in(" + str.Trim() + ")";
                var tasks = oGL00100Repository.GetSQLData(sql);
                DataCollection<GL00100> result = tasks;
                foreach (GL00100 o in result.Items)
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

            return IV00102;
        }
        public IV00109 GetGLByVendor(string ItemID, string VendorID)
        {
            Kaizen.Inventory.Repository.IV00109Repository _IV00109Repository = new IV00109Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IV00109 oIV00109 = _IV00109Repository.GetSingle(ss => ss.ItemID.Trim() == ItemID.Trim() && ss.VendorID.Trim() == VendorID.Trim());
            string str = string.Empty;
            if (oIV00109 != null)
            {
                //if (oIV00109.SalesAcc.HasValue && oIV00109.SalesAcc != null)
                //{
                //    if (string.IsNullOrEmpty(str))
                //        str += oIV00109.SalesAcc.Value.ToString();
                //    else
                //        str += "," + oIV00109.SalesAcc.Value.ToString();
                //}
                //if (oIV00109.SalesReturnAcc.HasValue && oIV00109.SalesReturnAcc != null)
                //{
                //    if (string.IsNullOrEmpty(str))
                //        str += oIV00109.SalesReturnAcc.Value.ToString();
                //    else
                //        str += "," + oIV00109.SalesReturnAcc.Value.ToString();
                //}
                //if (oIV00109.MarkdownAcc.HasValue && oIV00109.MarkdownAcc != null)
                //{
                //    if (string.IsNullOrEmpty(str))
                //        str += oIV00109.MarkdownAcc.Value.ToString();
                //    else
                //        str += "," + oIV00109.MarkdownAcc.Value.ToString();
                //}

                //if (oIV00109.InventoryAcc.HasValue && oIV00109.InventoryAcc != null)
                //{
                //    if (string.IsNullOrEmpty(str))
                //        str += oIV00109.InventoryAcc.Value.ToString();
                //    else
                //        str += "," + oIV00109.InventoryAcc.Value.ToString();
                //}
                //if (oIV00109.InventoryReturnAcc.HasValue && oIV00109.InventoryReturnAcc != null)
                //{
                //    if (string.IsNullOrEmpty(str))
                //        str += oIV00109.InventoryReturnAcc.Value.ToString();
                //    else
                //        str += "," + oIV00109.InventoryReturnAcc.Value.ToString();
                //}
                //if (oIV00109.InventoryOffsetAcc.HasValue && oIV00109.InventoryOffsetAcc != null)
                //{
                //    if (string.IsNullOrEmpty(str))
                //        str += oIV00109.InventoryOffsetAcc.Value.ToString();
                //    else
                //        str += "," + oIV00109.InventoryOffsetAcc.Value.ToString();
                //}
                //if (oIV00109.FreightAcc.HasValue && oIV00109.FreightAcc != null)
                //{
                //    if (string.IsNullOrEmpty(str))
                //        str += oIV00109.FreightAcc.Value.ToString();
                //    else
                //        str += "," + oIV00109.FreightAcc.Value.ToString();
                //}
                //if (oIV00109.TradeDiscountAcc.HasValue && oIV00109.TradeDiscountAcc != null)
                //{
                //    if (string.IsNullOrEmpty(str))
                //        str += oIV00109.TradeDiscountAcc.Value.ToString();
                //    else
                //        str += "," + oIV00109.TradeDiscountAcc.Value.ToString();
                //}
                //if (oIV00109.CostOfGoodsSold.HasValue && oIV00109.CostOfGoodsSold != null)
                //{
                //    if (string.IsNullOrEmpty(str))
                //        str += oIV00109.CostOfGoodsSold.Value.ToString();
                //    else
                //        str += "," + oIV00109.CostOfGoodsSold.Value.ToString();
                //}
                //if (oIV00109.TaxAcc.HasValue && oIV00109.TaxAcc != null)
                //{
                //    if (string.IsNullOrEmpty(str))
                //        str += oIV00109.TaxAcc.Value.ToString();
                //    else
                //        str += "," + oIV00109.TaxAcc.Value.ToString();
                //}

                //Kaizen.Inventory.Repository.GL00100Repository oGL00100Repository = new GL00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                //string sql = "select * from GL00100 where AccountID in(" + str.Trim() + ")";
                //var tasks = oGL00100Repository.GetSQLData(sql);
                //DataCollection<GL00100> result = tasks;
                //foreach (GL00100 o in result.Items)
                //{
                //    if (o.AccountID == oIV00109.SalesAcc)
                //        oIV00109.SalesAccount = o;
                //    if (o.AccountID == oIV00109.SalesReturnAcc)
                //        oIV00109.SalesReturnAccount = o;
                //    if (o.AccountID == oIV00109.InventoryAcc)
                //        oIV00109.InventoryAccount = o;
                //    if (o.AccountID == oIV00109.InventoryReturnAcc)
                //        oIV00109.InventoryReturnAccount = o;
                //    if (o.AccountID == oIV00109.MarkdownAcc)
                //        oIV00109.MarkdownAccount = o;
                //    if (o.AccountID == oIV00109.InventoryOffsetAcc)
                //        oIV00109.InventoryOffsetAccount = o;
                //    if (o.AccountID == oIV00109.FreightAcc)
                //        oIV00109.FreightAccount = o;
                //    if (o.AccountID == oIV00109.TradeDiscountAcc)
                //        oIV00109.TradeDiscountAccount = o;
                //    if (o.AccountID == oIV00109.CostOfGoodsSold)
                //        oIV00109.CostOfGoodsSoldAccount = o;
                //    if (o.AccountID == oIV00109.TaxAcc)
                //        oIV00109.TaxAccount = o;
                //}
            }

            return oIV00109;
        }
        public IV00100 GetGLPurchase(string ItemID, string SiteID)
        {
            IV00100 result = this.GetSingle(ItemID);
            if (string.IsNullOrEmpty(result.UFMPurchase))
            {
                result.UFMPurchase = result.UFMID.Trim();
                result.BaseUnitPurchase = result.BaseUnit;
            }
            IV00101Repository _IV00101Repository = new IV00101Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

            IV00101 IV00102result = null;
            IV00102Repository _IV00102Repository = new IV00102Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IV00102 IV00102 = _IV00102Repository.GetSingle(ss => ss.ItemID.Trim() == ItemID.Trim() && ss.SiteID.Trim() == SiteID.Trim());
            if (IV00102 == null)
            {
                IV00102result = _IV00101Repository.GetSingle(xx => xx.ItemID == result.ItemID.Trim());
                if (IV00102result != null)
                {
                    #region IV00101
                    result.PurchaseID = IV00102result.PurchaseID;
                    result.PurchaseName = IV00102result.PurchaseName;
                    result.PurchaseNumber = IV00102result.PurchaseNumber;
                    result.PurchaseTradDiscountID = IV00102result.PurchaseTradDiscountID;
                    result.PurchaseTradDiscountName = IV00102result.PurchaseTradDiscountName;
                    result.PurchaseTradDiscountNumber = IV00102result.PurchaseTradDiscountNumber;
                    result.PurchaseFreightID = IV00102result.PurchaseFreightID;
                    result.PurchaseFreightName = IV00102result.PurchaseFreightName;
                    result.PurchaseFreightNumber = IV00102result.PurchaseFreightNumber;
                    result.PurchaseTaxID = IV00102result.PurchaseTaxID;
                    result.PurchaseTaxName = IV00102result.PurchaseTaxName;
                    result.PurchaseTaxNumber = IV00102result.PurchaseTaxNumber;
                    result.PurchaseMisID = IV00102result.PurchaseTaxID;
                    result.PurchaseMisName = IV00102result.PurchaseMisName;
                    result.PurchaseMisNumber = IV00102result.PurchaseMisNumber;
                    #endregion
                }
                else
                {
                    #region  Setup IV000014
                    IV000014Services srv = new IV000014Services(this.UserContext);
                    IV000014 setGL = srv.GetAll().FirstOrDefault();
                    if (setGL != null)
                    {
                        #region
                        if (string.IsNullOrEmpty(result.PurchaseNumber))
                        {
                            result.PurchaseID = setGL.PurchaseID;
                            result.PurchaseName = setGL.PurchaseName;
                            result.PurchaseNumber = setGL.PurchaseNumber;
                        }
                        #endregion
                        //-----------PurchaseTradDiscountID
                        #region
                        if (string.IsNullOrEmpty(result.PurchaseTradDiscountNumber))
                        {
                            result.PurchaseTradDiscountID = setGL.PurchaseTradDiscountID;
                            result.PurchaseTradDiscountName = setGL.PurchaseTradDiscountName;
                            result.PurchaseTradDiscountNumber = setGL.PurchaseTradDiscountNumber;
                        }
                        #endregion
                        //-----------PurchaseFreightID
                        #region
                        if (string.IsNullOrEmpty(result.PurchaseFreightNumber))
                        {
                            result.PurchaseFreightID = setGL.PurchaseFreightID;
                            result.PurchaseFreightName = setGL.PurchaseFreightName;
                            result.PurchaseFreightNumber = setGL.PurchaseFreightNumber;
                        }
                        #endregion
                        //-----------PurchaseTaxID
                        #region 
                        if (string.IsNullOrEmpty(result.PurchaseTaxNumber))
                        {
                            result.PurchaseTaxID = setGL.PurchaseTaxID;
                            result.PurchaseTaxName = setGL.PurchaseTaxName;
                            result.PurchaseTaxNumber = setGL.PurchaseTaxNumber;
                        }
                        #endregion
                        //-----------PurchaseMisID
                        #region 
                        if (string.IsNullOrEmpty(result.PurchaseMisNumber))
                        {
                            result.PurchaseMisID = setGL.PurchaseMisID;
                            result.PurchaseMisName = setGL.PurchaseMisName;
                            result.PurchaseMisNumber = setGL.PurchaseMisNumber;
                        }
                        #endregion
                    }
                    #endregion
                }
            }
            else
            {
                #region
                result.PurchaseID = IV00102.PurchaseID;
                result.PurchaseName = IV00102.PurchaseName;
                result.PurchaseNumber = IV00102.PurchaseNumber;

                #endregion
                //-----------PurchaseTradDiscountID
                #region
                result.PurchaseTradDiscountID = IV00102.PurchaseTradDiscountID;
                result.PurchaseTradDiscountName = IV00102.PurchaseTradDiscountName;
                result.PurchaseTradDiscountNumber = IV00102.PurchaseTradDiscountNumber;

                #endregion
                //-----------PurchaseFreightID
                #region
                result.PurchaseFreightID = IV00102.PurchaseFreightID;
                result.PurchaseFreightName = IV00102.PurchaseFreightName;
                result.PurchaseFreightNumber = IV00102.PurchaseFreightNumber;
                #endregion
                //-----------PurchaseTaxID
                #region 
                result.PurchaseTaxID = IV00102.PurchaseTaxID;
                result.PurchaseTaxName = IV00102.PurchaseTaxName;
                result.PurchaseTaxNumber = IV00102.PurchaseTaxNumber;

                #endregion
                //-----------PurchaseMisID
                #region 
                result.PurchaseMisID = IV00102.PurchaseTaxID;
                result.PurchaseMisName = IV00102.PurchaseMisName;
                result.PurchaseMisNumber = IV00102.PurchaseMisNumber;

                #endregion
                if (string.IsNullOrEmpty(result.PurchaseNumber)
                || string.IsNullOrEmpty(result.PurchaseFreightNumber)
                || string.IsNullOrEmpty(result.PurchaseTaxNumber)
                || string.IsNullOrEmpty(result.PurchaseMisNumber))
                {
                    IV000014Services srv = new IV000014Services(this.UserContext);
                    IV000014 setGL = srv.GetAll().FirstOrDefault();
                    if (setGL != null)
                    {
                        #region
                        if (string.IsNullOrEmpty(result.PurchaseNumber))
                        {
                            result.PurchaseID = setGL.PurchaseID;
                            result.PurchaseName = setGL.PurchaseName;
                            result.PurchaseNumber = setGL.PurchaseNumber;
                        }
                        #endregion
                        //-----------PurchaseTradDiscountID
                        #region
                        if (string.IsNullOrEmpty(result.PurchaseTradDiscountNumber))
                        {
                            result.PurchaseTradDiscountID = setGL.PurchaseTradDiscountID;
                            result.PurchaseTradDiscountName = setGL.PurchaseTradDiscountName;
                            result.PurchaseTradDiscountNumber = setGL.PurchaseTradDiscountNumber;
                        }
                        #endregion
                        //-----------PurchaseFreightID
                        #region
                        if (string.IsNullOrEmpty(result.PurchaseFreightNumber))
                        {
                            result.PurchaseFreightID = setGL.PurchaseFreightID;
                            result.PurchaseFreightName = setGL.PurchaseFreightName;
                            result.PurchaseFreightNumber = setGL.PurchaseFreightNumber;
                        }
                        #endregion
                        //-----------PurchaseTaxID
                        #region 
                        if (string.IsNullOrEmpty(result.PurchaseTaxNumber))
                        {
                            result.PurchaseTaxID = setGL.PurchaseTaxID;
                            result.PurchaseTaxName = setGL.PurchaseTaxName;
                            result.PurchaseTaxNumber = setGL.PurchaseTaxNumber;
                        }
                        #endregion
                        //-----------PurchaseMisID
                        #region 
                        if (string.IsNullOrEmpty(result.PurchaseMisNumber))
                        {
                            result.PurchaseMisID = setGL.PurchaseMisID;
                            result.PurchaseMisName = setGL.PurchaseMisName;
                            result.PurchaseMisNumber = setGL.PurchaseMisNumber;
                        }
                        #endregion
                    }
                }
            }
            return result;
        }

        public IList<IV00102> GetAllByItemAndSite(string ItemID, string SiteID)
        {
            Kaizen.Inventory.Repository.IV00102Repository _IV00102Repository = new IV00102Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IList<IV00102> IV00102 = _IV00102Repository.GetAll(ss => ss.ItemID.Trim() == ItemID.Trim() && ss.SiteID.Trim() == SiteID.Trim());
            return IV00102;
        }
        public IList<IV00102> GetAllByItem(string ItemID)
        {
            Kaizen.Inventory.Repository.IV00102Repository _IV00102Repository = new IV00102Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IList<IV00102> IV00102 = _IV00102Repository.GetAll(ss => ss.ItemID.Trim() == ItemID.Trim());
            return IV00102;
        }
        public IList<IV00102> GetAllBySite(string SiteID)
        {
            Kaizen.Inventory.Repository.IV00102Repository _IV00102Repository = new IV00102Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IList<IV00102> IV00102 = _IV00102Repository.GetAll(ss => ss.SiteID.Trim() == SiteID.Trim());
            return IV00102;
        }

        public IList<IV00108> GetAllByItemAndCustomer(string ItemID, string CUSTNMBR)
        {
            Kaizen.Inventory.Repository.IV00108Repository _IV00108Repository = new IV00108Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IList<IV00108> IV00108 = _IV00108Repository.GetAll(ss => ss.ItemID.Trim() == ItemID.Trim() && ss.CUSTNMBR.Trim() == CUSTNMBR.Trim());
            return IV00108;
        }
        public IList<IV00108> GetAllByCustomerItem(string ItemID)
        {
            Kaizen.Inventory.Repository.IV00108Repository _IV00108Repository = new IV00108Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IList<IV00108> IV00108 = _IV00108Repository.GetAll(ss => ss.ItemID.Trim() == ItemID.Trim());
            return IV00108;
        }
        public IList<IV00108> GetAllByCustomer(string CUSTNMBR)
        {
            Kaizen.Inventory.Repository.IV00108Repository _IV00108Repository = new IV00108Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IList<IV00108> IV00108 = _IV00108Repository.GetAll(ss => ss.CUSTNMBR.Trim() == CUSTNMBR.Trim());
            return IV00108;
        }

        public IList<IV00109> GetAllByItemAndVendor(string ItemID, string VendorID)
        {
            Kaizen.Inventory.Repository.IV00109Repository _IV00109Repository = new IV00109Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IList<IV00109> IV00109 = _IV00109Repository.GetAll(ss => ss.ItemID.Trim() == ItemID.Trim() && ss.VendorID.Trim() == VendorID.Trim());
            return IV00109;
        }
        public IList<IV00109> GetAllByVendorItem(string ItemID)
        {
            Kaizen.Inventory.Repository.IV00109Repository _IV00109Repository = new IV00109Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IList<IV00109> IV00109 = _IV00109Repository.GetAll(ss => ss.ItemID.Trim() == ItemID.Trim());
            return IV00109;
        }
        public IList<IV00109> GetAllByVendor(string VendorID)
        {
            Kaizen.Inventory.Repository.IV00109Repository _IV00109Repository = new IV00109Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IList<IV00109> IV00109 = _IV00109Repository.GetAll(ss => ss.VendorID.Trim() == VendorID.Trim());
            return IV00109;
        }


        public IV00101 GetGL(string ItemID)
        {
            Kaizen.Inventory.Repository.IV00101Repository _IV00108Repository = new IV00101Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IV00101 IV00101 = _IV00108Repository.GetSingle(ss => ss.ItemID.Trim() == ItemID.Trim());
            string str = string.Empty;
            if (IV00101 != null)
            {
                if (IV00101.SalesAcc.HasValue && IV00101.SalesAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += IV00101.SalesAcc.Value.ToString();
                    else
                        str += "," + IV00101.SalesAcc.Value.ToString();
                }
                if (IV00101.SalesReturnAcc.HasValue && IV00101.SalesReturnAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += IV00101.SalesReturnAcc.Value.ToString();
                    else
                        str += "," + IV00101.SalesReturnAcc.Value.ToString();
                }
                if (IV00101.MarkdownAcc.HasValue && IV00101.MarkdownAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += IV00101.MarkdownAcc.Value.ToString();
                    else
                        str += "," + IV00101.MarkdownAcc.Value.ToString();
                }

                if (IV00101.InventoryAcc.HasValue && IV00101.InventoryAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += IV00101.InventoryAcc.Value.ToString();
                    else
                        str += "," + IV00101.InventoryAcc.Value.ToString();
                }
                if (IV00101.InventoryReturnAcc.HasValue && IV00101.InventoryReturnAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += IV00101.InventoryReturnAcc.Value.ToString();
                    else
                        str += "," + IV00101.InventoryReturnAcc.Value.ToString();
                }
                if (IV00101.InventoryOffsetAcc.HasValue && IV00101.InventoryOffsetAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += IV00101.InventoryOffsetAcc.Value.ToString();
                    else
                        str += "," + IV00101.InventoryOffsetAcc.Value.ToString();
                }
                if (IV00101.FreightAcc.HasValue && IV00101.FreightAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += IV00101.FreightAcc.Value.ToString();
                    else
                        str += "," + IV00101.FreightAcc.Value.ToString();
                }
                if (IV00101.TradeDiscountAcc.HasValue && IV00101.TradeDiscountAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += IV00101.TradeDiscountAcc.Value.ToString();
                    else
                        str += "," + IV00101.TradeDiscountAcc.Value.ToString();
                }
                if (IV00101.CostOfGoodsSold.HasValue && IV00101.CostOfGoodsSold != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += IV00101.CostOfGoodsSold.Value.ToString();
                    else
                        str += "," + IV00101.CostOfGoodsSold.Value.ToString();
                }
                if (IV00101.TaxAcc.HasValue && IV00101.TaxAcc != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += IV00101.TaxAcc.Value.ToString();
                    else
                        str += "," + IV00101.TaxAcc.Value.ToString();
                }

                Kaizen.Inventory.Repository.GL00100Repository oGL00100Repository = new GL00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                string sql = "select * from GL00100 where AccountID in(" + str.Trim() + ")";
                var tasks = oGL00100Repository.GetSQLData(sql);
                DataCollection<GL00100> result = tasks;
                foreach (GL00100 o in result.Items)
                {
                    if (o.AccountID == IV00101.SalesAcc)
                        IV00101.SalesAccount = o;
                    if (o.AccountID == IV00101.SalesReturnAcc)
                        IV00101.SalesReturnAccount = o;
                    if (o.AccountID == IV00101.InventoryAcc)
                        IV00101.InventoryAccount = o;
                    if (o.AccountID == IV00101.InventoryReturnAcc)
                        IV00101.InventoryReturnAccount = o;
                    if (o.AccountID == IV00101.MarkdownAcc)
                        IV00101.MarkdownAccount = o;
                    if (o.AccountID == IV00101.InventoryOffsetAcc)
                        IV00101.InventoryOffsetAccount = o;
                    if (o.AccountID == IV00101.FreightAcc)
                        IV00101.FreightAccount = o;
                    if (o.AccountID == IV00101.TradeDiscountAcc)
                        IV00101.TradeDiscountAccount = o;
                    if (o.AccountID == IV00101.CostOfGoodsSold)
                        IV00101.CostOfGoodsSoldAccount = o;
                    if (o.AccountID == IV00101.TaxAcc)
                        IV00101.TaxAccount = o;
                }
            }
            return IV00101;
        }
        public IList<IV00100> GetAll()
        {
            var tasks = _IV00100Repository.GetAll();
            IList<IV00100> result = tasks;
            return result;
        }
        public IList<IV00151> GetAllIV00151()
        {
            var tasks = _IV00151Repository.GetAll();
            IList<IV00151> result = tasks;
            return result;
        }

        public IList<IV00100> SearchForItems(string text)
        {
            var tasks = _IV00100Repository.GetAll(xx => xx.ItemID.Contains(text.Trim()) || xx.ShortDescription.Contains(text.Trim()));
            IList<IV00100> result = tasks;
            return result;
        }

        public DataCollection<IV00100> GetListWithPaging(string SearchTerm, string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                DataCollection<IV00100> result = null;

                var tasks = _IV00100Repository.GetListWithPaging(x => x.ClassID.ToString().Contains(SearchTerm) ||
                    x.ItemDescription.Contains(SearchTerm)
                    , PageSize, Page, ss => new { ss.ClassID }, null);
                result = tasks;
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
        public IList<IV00008> GetAllIV00008()
        {
            IV00008Repository r = new IV00008Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<IV00008> result = tasks;
            return result;
        }

        public IList<IV00007> GetAllIV00007()
        {
            IV00007Repository r = new IV00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<IV00007> result = tasks;
            return result;
        }
        public IList<IV00009> GetAllIV00009()
        {
            try
            {
                IList<IV00009> L = null;
                try
                {
                    IV00009Repository r = new IV00009Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    var tasks = r.GetAll();
                    IList<IV00009> result = tasks;
                    return result;
                }
                catch (Exception ex)
                {
                }
                return null;
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
        public IV00151 GetSingle(int WeekDayID, string SiteID, string ItemID)
        {
            var o = _IV00151Repository.GetSingle(x => x.WeekDayID == WeekDayID && x.ItemID.Equals(ItemID.Trim()) && x.SiteID.Equals(SiteID.Trim()), s => s.IV00152);
            IList<IV00152> IV00152List = new List<IV00152>();

            foreach (IV00152 obj in o.IV00152)
            {
                IV00152List.Add(new IV00152()
                {
                    CurrencyCode = obj.CurrencyCode.Trim(),
                    DecimalDigit = obj.DecimalDigit,
                    EndTimeTo = obj.EndTimeTo,
                    ExchangeRate = obj.ExchangeRate,
                    IsMultiply = obj.IsMultiply,
                    ItemID = obj.ItemID.Trim(),
                    LoyalityPoints = obj.LoyalityPoints,
                    PeriodID = obj.PeriodID,
                    PeriodName = obj.PeriodName,
                    SiteID = obj.SiteID.Trim(),
                    StartTimeFrom = obj.StartTimeFrom,
                    UnitMinutes = obj.UnitMinutes,
                    UnitPrice = obj.UnitPrice,
                    WeekDayID = obj.WeekDayID
                });
            }

            IV00151 tasks = new IV00151()
            {
                CurrencyCode = o.CurrencyCode.Trim(),
                DecimalDigit = o.DecimalDigit,
                EndTimeTo = o.EndTimeTo,
                ExchangeRate = o.ExchangeRate,
                IsMultiply = o.IsMultiply,
                ItemID = o.ItemID.Trim(),
                PeriodCount = o.PeriodCount,
                SiteID = o.SiteID.Trim(),
                StartTimeFrom = o.StartTimeFrom,
                WeekDayID = o.WeekDayID,
                IV00152 = IV00152List
            };
            return tasks;
        }
        public IV00100 GetSingle(string ItemID)
        {
            var o = _IV00100Repository.GetSingle(x => x.ItemID == ItemID, ss => ss.IV00101);
            IV00100 tasks = new IV00100()
            {
                ItemID = o.ItemID,
                ItemName = o.ItemName,
                ShortDescription = o.ShortDescription,
                GenericDescription = o.GenericDescription,
                ItemDescription = o.ItemDescription,
                PhotoExtension = o.PhotoExtension,
                BarCode = o.BarCode,
                IsinActive = o.IsinActive,
                IsHold = o.IsHold,
                WarrantyDays = o.WarrantyDays,
                ShelfLifeDays = o.ShelfLifeDays,
                ExpiryDays = o.ExpiryDays,
                UFMGroupID = o.UFMGroupID,
                UFMID = o.UFMID,
                DecimalDigitQuantity = o.DecimalDigitQuantity,
                PriceMethodCode = o.PriceMethodCode,
                ItemTypeID = o.ItemTypeID,
                ItemGroupID = o.ItemGroupID,
                ClassID = o.ClassID,
                ShippingWeight = o.ShippingWeight,
                ABCID = o.ABCID,
                UFMPurchase = string.IsNullOrEmpty(o.UFMPurchase) ? o.UFMID : o.UFMPurchase,
                BaseUnitPurchase = (o.BaseUnitPurchase.HasValue && o.BaseUnitPurchase.Value > 0) ? o.BaseUnitPurchase : o.BaseUnit,
                UnitCost = o.UnitCost,
                LastUnitCost = o.LastUnitCost,
                PurchaseQTY = o.PurchaseQTY,
                UFMSale = o.UFMSale,
                BaseUnitSale = o.BaseUnitSale,
                PriceLevelCode = o.PriceLevelCode,
                ValuationMethodID = o.ValuationMethodID,
                TrackTypeID = o.TrackTypeID,
                LotNumber = o.LotNumber,
                IsExpiryDate = o.IsExpiryDate,
                UnitPrice = o.UnitPrice,
                PromotedPrice = o.PromotedPrice,
                LastUnitPrice = o.LastUnitPrice,
                SaleQTY = o.SaleQTY,
                PrimaryVendor = o.PrimaryVendor,
                CountryID = o.CountryID,
                CreatedBy = o.CreatedBy,
                CreatedDate = o.CreatedDate,
                BaseUnit = o.BaseUnit,
                ItemParent = o.ItemParent,
                PurchaseFreightID = o.PurchaseFreightID,
                PurchaseFreightName = o.PurchaseFreightName,
                PurchaseFreightNumber = o.PurchaseFreightNumber,
                PurchaseID = o.PurchaseID,
                PurchaseName = o.PurchaseName,
                PurchaseNumber = o.PurchaseNumber,
                PurchaseMisID = o.PurchaseMisID,
                PurchaseMisName = o.PurchaseMisName,
                PurchaseMisNumber = o.PurchaseMisNumber,
                PurchaseTradDiscountID = o.PurchaseTradDiscountID,
                PurchaseTradDiscountName = o.PurchaseTradDiscountName,
                PurchaseTradDiscountNumber = o.PurchaseTradDiscountNumber,
                PurchaseTaxID = o.PurchaseTaxID,
                PurchaseTaxName = o.PurchaseTaxName,
                PurchaseTaxNumber = o.PurchaseTaxNumber,
                IV00101 = o.IV00101 == null ? null : new IV00101
                {
                    ItemID = o.IV00101.ItemID,
                    PurchaseID = o.IV00101.PurchaseID,
                    PurchaseName = o.IV00101.PurchaseName,
                    PurchaseNumber = o.IV00101.PurchaseNumber,
                    PurchaseTradDiscountID = o.IV00101.PurchaseTradDiscountID,
                    PurchaseTradDiscountName = o.IV00101.PurchaseTradDiscountName,
                    PurchaseTradDiscountNumber = o.IV00101.PurchaseTradDiscountNumber,
                    PurchaseFreightID = o.IV00101.PurchaseFreightID,
                    PurchaseFreightName = o.IV00101.PurchaseFreightName,
                    PurchaseFreightNumber = o.IV00101.PurchaseFreightNumber,
                    PurchaseTaxID = o.IV00101.PurchaseTaxID,
                    PurchaseTaxName = o.IV00101.PurchaseTaxName,
                    PurchaseTaxNumber = o.IV00101.PurchaseTaxNumber,
                    PurchaseMisID = o.IV00101.PurchaseMisID,
                    PurchaseMisName = o.IV00101.PurchaseMisName,
                    PurchaseMisNumber = o.IV00101.PurchaseMisNumber,
                    SalesAcc = o.IV00101.SalesAcc,
                    SalesReturnAcc = o.IV00101.SalesReturnAcc,
                    MarkdownAcc = o.IV00101.MarkdownAcc,
                    InventoryAcc = o.IV00101.InventoryAcc,
                    InventoryReturnAcc = o.IV00101.InventoryReturnAcc,
                    InventoryOffsetAcc = o.IV00101.InventoryOffsetAcc,
                    PurchasePriceVariance = o.IV00101.PurchasePriceVariance,
                    FreightAcc = o.IV00101.FreightAcc,
                    TradeDiscountAcc = o.IV00101.TradeDiscountAcc,
                    TaxAcc = o.IV00101.TaxAcc,
                    CostOfGoodsSold = o.IV00101.CostOfGoodsSold
                }
            };
            return tasks;
        }
        public IV00106 GetItemTrackSerial(string ItemID)
        {
            IV00106Repository rep = new IV00106Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetSingle(x => x.ItemID == ItemID);
            return tasks;
        }

        public KaizenResult AddIV00100(IV00100 newTask)
        {
            newTask.CreatedBy = UserContext.UserName;
            newTask.CreatedDate = DateTime.Now;
            var result = _IV00100Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IV00100 newTask)
        {
            var result = _IV00100Repository.UpdateKaizenObject(newTask);
            IV00101Repository _IV00101Repository = new IV00101Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IV00106Repository _IV00106Repository = new IV00106Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            if (newTask.IV00101 != null)
            {
                if (_IV00101Repository.GetSingle(cc => cc.ItemID == newTask.ItemID) != null)
                {
                    _IV00101Repository.UpdateKaizenObject(newTask.IV00101);
                }
                else
                {
                    newTask.IV00101.IV00100 = null;
                    _IV00101Repository.AddKaizenObject(newTask.IV00101);
                }
            }
            if (newTask.IV00106 != null)
            {
                if (_IV00106Repository.GetSingle(cc => cc.ItemID == newTask.ItemID) != null)
                {
                    _IV00106Repository.UpdateKaizenObject(newTask.IV00106);
                }
                else
                {
                    newTask.IV00106.IV00100 = null;
                    _IV00106Repository.AddKaizenObject(newTask.IV00106);
                }
            }
            return result;
        }

        public KaizenResult Delete(string ItemID)
        {
            var result = _IV00100Repository.RemoveKaizenObject(ss => ss.ItemID == ItemID);
            return result;
        }
        public string GetNextTransactionID(string ClassID)
        {
            IV00001Repository reptemp = new IV00001Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IV00001 obj = reptemp.GetSingle(ss => ss.ClassID == ClassID.Trim());
            if (obj == null) return "";
            if (obj.NextItemlenth.HasValue)
            {
                string SequenceName = "IV_Item_" + ClassID.Trim() + "_Sequence";
                int KaizenID = -1;
                Kaizen.Admin.Repository.Sys00000Repository Repository = new Kaizen.Admin.Repository.Sys00000Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                List<Kaizen.Admin.Sys00000> AllSessions = Repository.GetList(xx => xx.SequenceName.Trim() == SequenceName.Trim()).ToList();
                Kaizen.Admin.Sys00000 temp = AllSessions.Find(xx => xx.UserName.Trim() == UserContext.UserName.Trim());
                if (temp == null)
                {
                    string SQL = string.Empty;
                    foreach (Kaizen.Admin.Sys00000 row in AllSessions)
                    {
                        Kaizen.Configuration.KaizenSession oKaizenSession = Configuration.UserServices.GetUserSession(row.UserName, UserContext.CompanyID);
                        if (oKaizenSession == null)
                        {
                            SQL = "update Sys00000 set UserName = '" + UserContext.UserName.Trim() + "' where UserName ='" +
                                row.UserName.TrimStart() + "' and SequenceName ='" + SequenceName + "'";
                            this.ExecuteSqlCommand(SQL);
                            KaizenID = row.KaizenID;
                            break;
                        }
                        bool notUse = true;
                        foreach (Kaizen.Configuration.Screen scrn in oKaizenSession.Screens)
                        {
                            if (scrn == Kaizen.Configuration.Screen.IV_Item)
                            {
                                notUse = false;
                                break;
                            }
                        }
                        if (notUse)
                        {
                            KaizenID = row.KaizenID;
                            break;
                        }
                    }
                }
                else
                    KaizenID = temp.KaizenID;
                ////-----------New ID-----------------------------------------------------
                if (KaizenID == -1)
                {
                    string SQL = "SELECT NEXT VALUE FOR " + SequenceName;
                    long? SequenceNumber = Repository.GetSQLLong(SQL);
                    if (SequenceNumber.HasValue)
                    {
                        KaizenID = (int)SequenceNumber.Value;
                        SQL = "insert into Sys00000(UserName,SequenceName,KaizenID) values (";
                        SQL += "'" + UserContext.UserName + "','" + SequenceName + "'," + SequenceNumber.ToString() + ");";
                        SQL += "update IV00001 set NextItemNumber=" + KaizenID.ToString() + "  where ClassID ='" + ClassID.Trim() + "'";
                        Repository.ExecuteSqlCommand(SQL);
                    }
                    else
                        return "";
                }
                if (UserContext.Screens == null)
                    UserContext.Screens = new List<Kaizen.Configuration.Screen>();
                UserContext.Screens.Add(Kaizen.Configuration.Screen.IV_Item);


                int templenth = obj.NextItemlenth.Value - KaizenID.ToString().Length;
                string Str = string.Empty;
                for (byte i = 1; i <= templenth; i++)
                {
                    Str = Str + "0";
                }
                return obj.NextItemPrefix.Trim() + Str + KaizenID.ToString();
            }
            return "";
        }
        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _IV00100Repository.ExecuteSqlCommand(myQuery);
            return result;
        }

        public KaizenResult AddIV00150(IList<IV00150> newTask)
        {

            var result = _IV00150Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult UpdateIV00150(IList<IV00150> newTask)
        {

            var result = _IV00150Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public IList<IV00150> LoadIV00150UpdateData(string ItemID, string SiteID)
        {
            var result = _IV00150Repository.GetAll();
            return result;
        }

        #region Item Price Minutes
        public IList<IV00022> GetWeekDays()
        {
            IV00022Repository rep = new IV00022Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var weekDays = rep.GetAll();
            IList<IV00022> result = weekDays;
            return result;
        }
        public KaizenResult AddIV00151(IV00151 newTask)
        {

            IList<IV00152> IV00152List = new List<IV00152>();

            foreach (IV00152 obj in newTask.IV00152)
            {
                obj.IV00151 = null;
            }

            var result = _IV00151Repository.AddKaizenObject(newTask);
            //if (result.Status)
            //    result = _IV00152Repository.AddKaizenObject(newTask.IV00152.ToArray());

            return result;
        }

        public KaizenResult UpdateIV00151(IV00151 newTask)
        {

            IList<IV00152> IV00152List = new List<IV00152>();

            foreach (IV00152 obj in newTask.IV00152)
            {
                obj.IV00151 = null;
            }

            var result = _IV00151Repository.UpdateKaizenObject(newTask);
            if (result.Status)
                result = _IV00152Repository.UpdateKaizenObject(newTask.IV00152.ToArray());

            return result;
        }
        public KaizenResult DeleteIV00151(IV00151 newTask)
        {

            IList<IV00152> IV00152List = new List<IV00152>();

            var result = _IV00152Repository.DeleteKaizenObject(newTask.IV00152.ToArray());

            if (result.Status)
            {
                newTask.IV00152 = null;
                result = _IV00151Repository.DeleteKaizenObject(newTask);
            }

            return result;
        }
        #endregion
    }
}
