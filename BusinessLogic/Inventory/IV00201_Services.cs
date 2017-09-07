using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00201Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00201Repository _IV00201Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00201Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00201Repository = new IV00201Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<IV00201> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00201> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00201Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00201Repository.GetWhereListWithPaging("IV00201", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00201> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TransactionID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TransactionTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BatchID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BatchSourceID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SiteID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ReasonID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00201> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00201Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00201Repository.GetWhereListWithPaging("IV00201", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00201> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<IV00201> L = null;
            var tasks = _IV00201Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<IV00201> result = tasks;
            return result;
        }
        public DataCollection<IV00201> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<IV00201> result = null;
            var tasks = _IV00201Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public DataCollection<IV00201> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<IV00201> L = null;
            var tasks = _IV00201Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<IV00201> result = tasks;
            return result;
        }

        public DataCollection<IV00201> GetListWithPaging(string SearchTerm, string whereClause, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                DataCollection<IV00201> result = null;
                var tasks = _IV00201Repository.GetListWithPaging(x => x.TransactionID.ToString().Contains(SearchTerm)
                    , PageSize, Page, ss => ss.TransactionID, null);
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
        public DataCollection<IV00201> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<IV00201> result = null;
            var tasks = _IV00201Repository.GetListWithPaging(PageSize, Page, ss => new { ss.TransactionID });
            result = tasks;
            return result;
        }

        public IV00201 GetSingle(int LineID)
        {
            var tasks = _IV00201Repository.GetSingle(x => x.LineID == LineID);
            return tasks;
        }
        public IList<IV_205Temp> GetLines(int TransactionTypeID,string TransactionID)
        {
            IV_205TempRepository rep = new IV_205TempRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(ss => ss.TransactionTypeID == TransactionTypeID && ss.TransactionID == TransactionID);
            IList<IV_205Temp> result = tasks;
            return result;
        }
        public IList<IV00201> GetAll()
        {
            var tasks = _IV00201Repository.GetAll();
            IList<IV00201> result = tasks;
            return result;
        }
        public IList<IV00201> GetByTransactionID(string TransactionID, int TransactionTypeID)
        {
            var tasks = _IV00201Repository.GetAll(ss => ss.TransactionID == TransactionID 
            && ss.TransactionTypeID == TransactionTypeID
            , ss => new { ss.LineID }, xx => xx.IV00202);
            List<IV00201> result = new List<IV00201>();
            foreach(IV00201 item in tasks)
            {
                List<IV00202> IV00202List = new List<IV00202>();
                foreach (IV00202 itemLot in item.IV00202)
                {
                    IV00202List.Add(new IV00202()
                    {
                        LotRowID = itemLot.LotRowID,
                        LineID = item.LineID,
                        ExpiryDate = itemLot.ExpiryDate,
                        BarCode = itemLot.BarCode,
                        LOTLineCode = itemLot.LOTLineCode,
                        ItemQuantity = itemLot.ItemQuantity
                    });
                }
                IV00201 tt = new IV00201()
                {
                    LineID = item.LineID,
                    TransactionID = item.TransactionID,
                    TransactionTypeID = item.TransactionTypeID,
                    ItemID = item.ItemID,
                    ItemName = item.ItemName,
                    UFMGroupID = item.UFMGroupID,
                    UFMID = item.UFMID,
                    BaseUnit = item.BaseUnit,
                    LotNumber = item.LotNumber,
                    SiteID = item.SiteID,
                    BinTrack = item.BinTrack,
                    ReasonID = item.ReasonID,
                    ItemDescription = item.ItemDescription,
                    ItemQuantity = item.ItemQuantity,
                    ItemUnitCost = item.ItemUnitCost
                };
                tt.IV00202 = IV00202List;
                result.Add(tt);
            }
            return result;
        }

        public KaizenResult AddIV00201(IV00201 newTask)
        {
            var result = _IV00201Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00201(IList<IV00201> newTask)
        {
            foreach(IV00201 temp in newTask)
            {
                foreach(IV00204 TEMP2014 in temp.IV00204)
                {
                    if(TEMP2014.IsBinGroup.HasValue && TEMP2014.IsBinGroup.Value)
                    {
                        
                    }
                    else
                    {
                        TEMP2014.IV00205 = null;

                    }
                }
            }
            var result = _IV00201Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00201 newTask)
        {
            var result = _IV00201Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00201> newTask)
        { 
            KaizenResult result = new KaizenResult();
            IV00202Repository rep = new IV00202Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IV00204Repository _IV00204Repository = new IV00204Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

            foreach (var item in newTask)
            {
                ICollection<IV00202> temp = item.IV00202;
                ICollection<IV00204> tempIV00204 = item.IV00204;

                item.IV00202 = null;
                item.IV00204 = null;

                result = _IV00201Repository.UpdateKaizenObject(item);
                //rep.ExecuteSqlCommand("delete IV00202 where LineID=" + item.LineID);
                _IV00204Repository.ExecuteSqlCommand("delete IV00204 where LineID=" + item.LineID);
                foreach (var LotLine in temp)
                {
                    LotLine.LineID = item.LineID;
                    rep.Add(LotLine);
                }
                foreach (var IV00204 in tempIV00204)
                {
                    IV00204.LineID = item.LineID;
                    if (IV00204.IsBinGroup.HasValue && IV00204.IsBinGroup.Value)
                    {
                        foreach (var IV00205 in IV00204.IV00205)
                        {
                            IV00205.LineID = IV00204.LineID;
                        }
                    }
                    else
                    {
                        IV00204.IV00205 = null;
                    }
                    _IV00204Repository.Add(IV00204);
                }
            }
            return result;
        }

        public KaizenResult Delete(IV00201 newTask)
        {
            var result = _IV00201Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00201> newTask)
        {
            var result = _IV00201Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Remove(string TransactionID)
        {
            var result = _IV00201Repository.RemoveKaizenObject(ss => ss.TransactionID == TransactionID);
            return result;
        }


        public IList<IV00019> GetAllIV00019()
        {
            try
            {
                IList<IV00019> L = null;
                try
                {
                    IV00019Repository r = new IV00019Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    var tasks = r.GetAll();
                    IList<IV00019> result = tasks;
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
        public IList<IV00018> GetAllIV00018()
        {
            try
            {
                IList<IV00018> L = null;
                try
                {
                    IV00018Repository r = new IV00018Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                    var tasks = r.GetAll();
                    IList<IV00018> result = tasks;
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

    }
}
