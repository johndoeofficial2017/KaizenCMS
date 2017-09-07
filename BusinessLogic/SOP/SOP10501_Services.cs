using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP10501Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10501Repository _SOP10501Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10501Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10501Repository = new SOP10501Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<SOP10501> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP10501> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10501Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10501Repository.GetWhereListWithPaging("SOP10501", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP10501> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("LineID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SOPNUMBE", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ProjectID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PhotoExtension", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DecimalDigitQuantity", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CustomerItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ShipAddressTypeCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ShipAddressName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Pnone01", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Pnone02", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MobileNo1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MobileNo2", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("POBox", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Other01", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Other02", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Address1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Email01", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Email02", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UFMGroupID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UFMID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ShippingID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ReqShipDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ShipDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SiteID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BinTrack", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VendorItemID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VendorItemName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VendorShortDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LineDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PriceLevelCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("QuantityOrder", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("QuantityCancel", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Territory", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SalePersonID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TaxAMT", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Markdown", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Freight", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Miscellaneous", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TradeDiscount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Commission", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VendorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CountryID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UnitPrice", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UnitCost", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP10501> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10501Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10501Repository.GetWhereListWithPaging("SOP10501", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public IList<SOP10501> GetListBySOPNUMBE(string SOPNUMBE)
        {
            SOP10502Repository rep = new SOP10502Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            SOP10506Repository _SOP10506Repository = new SOP10506Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

            var tasks = _SOP10501Repository.GetAll(ss => ss.SOPNUMBE == SOPNUMBE);
            foreach (var item in tasks)
            {
                var list = rep.GetAll(ss => ss.SOPNUMBE == item.SOPNUMBE && ss.ItemLineIndex == item.ItemLineIndex);
                var SOP10506 = _SOP10506Repository.GetSingle(ss => ss.SOPNUMBE == item.SOPNUMBE && ss.ItemLineIndex == item.ItemLineIndex);
                item.SOP10502 = list;
                item.SOP10506 = SOP10506;
            }
            return tasks;
        }

        public IList<SOP10501> GetAll()
        {
            var tasks = _SOP10501Repository.GetAll();
            IList<SOP10501> result = tasks;
            return result;
        }
        public SOP10501 GetSingle(string SOPNUMBE, string SOPTypeSetupID)
        {
            var tasks = _SOP10501Repository.GetSingle(x => x.SOPNUMBE == SOPNUMBE);
            return tasks;
        }

        public KaizenResult AddSOP10501(SOP10501 newTask)
        {
            var result = _SOP10501Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP10501(IList<SOP10501> newTask)
        {
            foreach (var item in newTask)
            {
                if (item.SOP10502 != null)
                {
                    foreach (var LotLine in item.SOP10502)
                    {
                        LotLine.SOPNUMBE = item.SOPNUMBE;
                        LotLine.ItemLineIndex = item.ItemLineIndex;
                    }
                }
                if (item.SOP10504 != null)
                {
                    foreach (var bin in item.SOP10504)
                    {
                        bin.SOPNUMBE = item.SOPNUMBE;
                        bin.ItemLineIndex = item.ItemLineIndex;
                    }
                }
            }
            var result = _SOP10501Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP10501 newTask)
        {
            var result = _SOP10501Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP10501> newTask)
        {
            KaizenResult result = new KaizenResult();
            SOP10502Repository rep = new SOP10502Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            SOP10504Repository _SOP10504Repository = new SOP10504Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            SOP10506Repository _SOP10506Repository = new SOP10506Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

            foreach (var item in newTask)
            {
                ICollection<SOP10502> temp = item.SOP10502;
                ICollection<SOP10504> tempSOP10504 = item.SOP10504;

                item.SOP10502 = null;
                item.SOP10504 = null;

                result = _SOP10501Repository.UpdateKaizenObject(item);
                if (item.SOP10506 != null)
                {
                    if (!string.IsNullOrEmpty(item.SOP10506.ShipAddressTypeCode))
                    {
                        var SOP10506 = _SOP10506Repository.GetSingle(ss => ss.SOPNUMBE == item.SOPNUMBE && ss.ItemLineIndex == item.ItemLineIndex);
                        if (SOP10506 == null)
                        {
                            item.SOP10506.SOPNUMBE = item.SOPNUMBE;
                            item.SOP10506.ItemLineIndex = item.ItemLineIndex;
                            _SOP10506Repository.AddKaizenObject(item.SOP10506);
                        }
                        else
                            _SOP10506Repository.UpdateKaizenObject(item.SOP10506);
                    }
                }

                rep.RemoveKaizenObject(ss => ss.SOPNUMBE == item.SOPNUMBE && ss.ItemLineIndex == item.ItemLineIndex);
                _SOP10504Repository.RemoveKaizenObject(ss => ss.SOPNUMBE == item.SOPNUMBE && ss.ItemLineIndex == item.ItemLineIndex);
                foreach (var LotLine in temp)
                {
                    LotLine.SOPNUMBE = item.SOPNUMBE;
                    LotLine.ItemLineIndex = item.ItemLineIndex;
                    rep.Add(LotLine);
                }
                foreach (var SOP10504 in tempSOP10504)
                {
                    SOP10504.SOPNUMBE = item.SOPNUMBE;
                    SOP10504.ItemLineIndex = item.ItemLineIndex;
                    _SOP10504Repository.Add(SOP10504);
                }
            }
            return result;
        }

        public KaizenResult Delete(SOP10501 newTask)
        {
            var result = _SOP10501Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP10501> newTask)
        {
            var result = _SOP10501Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(string SOPNUMBE, int ItemLineIndex)
        {
            var result = _SOP10501Repository.RemoveKaizenObject(ss => ss.SOPNUMBE == SOPNUMBE && ss.ItemLineIndex == ItemLineIndex);
            return result;
        }

    }
}
