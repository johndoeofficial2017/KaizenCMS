using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00101Services
    {
        #region Infrastructure

        private IV00101Repository _IV00101Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00101Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00101Repository = new IV00101Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<IV00101> GetAllIV00101(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<IV00101> L = null;
                var tasks = _IV00101Repository.GetListWithPaging(x => x.ItemID.Contains(SearchTerm), PageSize, Page, ss => ss.ItemID, null);

                DataCollection<IV00101> result = tasks;
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
        public DataCollection<IV00101> GetByItemID(string ItemID, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<IV00101> L = null;
                var tasks = _IV00101Repository.GetListWithPaging(x => x.ItemID.Trim() == ItemID.Trim(), PageSize, Page, ss => ss.ItemID, null);

                DataCollection<IV00101> result = tasks;
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

        public IList<IV00101> GetAll()
        {
            try
            {
                IList<IV00101> L = null;
                try
                {
                    var tasks = _IV00101Repository.GetAll();
                    IList<IV00101> result = tasks;
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
        public IV00101 GetSingle(string ItemID)
        {
            var tasks = _IV00101Repository.GetSingle(xx => xx.ItemID == ItemID);
            IV00101 result = tasks;
            return result;
        }
        public IV00101 GetGL(string ItemID)
        {
            IV00101 IV00101 = _IV00101Repository.GetSingle(ss => ss.ItemID.Trim() == ItemID.Trim());
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
                if (result != null)
                {
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
            }
            return IV00101;
        }

        public KaizenResult AddIV00101(IV00101 newTask)
        {
            var result = _IV00101Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00101(IList<IV00101> newTask)
        {
            var result = _IV00101Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00101 newTask)
        {
            var result = _IV00101Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00101> newTask)
        {
            var result = _IV00101Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(IV00101 newTask)
        {
            var result = _IV00101Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00101> newTask)
        {
            var result = _IV00101Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
