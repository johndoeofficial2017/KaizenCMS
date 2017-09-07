using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP00100Services
    {
        #region Infrastructure

        private SOP00100Repository _SOP00100Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00100Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00100Repository = new SOP00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00100> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00100Repository.GetWhereListWithPaging("SOP00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP00100> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CUSTNMBR", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CUSTNAME", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ShortName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CUSTCLAS", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("StatusID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CustomerDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CPRCRNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmployerName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00100Repository.GetWhereListWithPaging("SOP00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP00100> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<SOP00100> L = null;
            var tasks = _SOP00100Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<SOP00100> result = tasks;
            return result;
        }
        public DataCollection<SOP00100> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<SOP00100> result = null;
            var tasks = _SOP00100Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }


        public DataCollection<SOP00100> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00100> L = null;
            var tasks = _SOP00100Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                , xx => xx.NationalityID, ss => ss.SOP00011);
            DataCollection<SOP00100> result = tasks;
            return result;
        }
        public DataCollection<SOP00100> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00100> result = null;
            var tasks = _SOP00100Repository.GetListWithPaging(x => x.CUSTNMBR.ToString().Contains(SearchTerm) ||
                x.CUSTNAME.Contains(SearchTerm)
                , PageSize, Page, ss => new { ss.CUSTNMBR }, xx => xx.NationalityID, ss => ss.SOP00011);
            result = tasks;
            return result;
        }
        public DataCollection<SOP00100> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00100> result = null;
            var tasks = _SOP00100Repository.GetListWithPaging(PageSize, Page, ss => new { ss.CUSTNMBR });
            result = tasks;
            return result;
        }

        public SOP00100 GetSingle(string CUSTNMBR)
        {
            var tasks = _SOP00100Repository.GetSingle(x => x.CUSTNMBR.Trim() == CUSTNMBR.Trim());
            return tasks;
        }
        public SOP00102 GetGL(string CUSTNMBR)
        {
            SOP00102Repository rep = new SOP00102Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetSingle(x => x.CUSTNMBR.Trim() == CUSTNMBR);
            return tasks;
        }

        public KaizenResult AddSOP00100(SOP00100 newTask)
        {
            newTask.EntryDate = DateTime.Now;
            newTask.UserCode = UserContext.UserName;
            var result = _SOP00100Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP00100(IList<SOP00100> newTask)
        {
            foreach (var item in newTask)
            {
                item.EntryDate = DateTime.Now;
                item.UserCode = UserContext.UserName;
            }
            var result = _SOP00100Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP00100 newTask)
        {
            var result = _SOP00100Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP00100> newTask)
        {
            var result = _SOP00100Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP00100 newTask)
        {
            var result = _SOP00100Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP00100> newTask)
        {
            var result = _SOP00100Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        //public KaizenResult AddSOP00100(SOP00100 newTask)
        //{
        //if (newTask.MainAddress != null)
        //{
        //    newTask.AddressTypeCode = newTask.MainAddress.AddressTypeCode;
        //    newTask.SOP00101.Add(newTask.MainAddress);
        //}
        //if (newTask.ShipAddress != null)
        //{
        //    if (newTask.ShipAddress.AddressTypeCode != newTask.MainAddress.AddressTypeCode)
        //        newTask.SOP00101.Add(newTask.ShipAddress);
        //    newTask.ShipTo = newTask.ShipAddress.AddressTypeCode;
        //}
        //if (newTask.BillAddress != null)
        //{
        //    if (newTask.BillAddress.AddressTypeCode != newTask.MainAddress.AddressTypeCode)
        //        newTask.SOP00101.Add(newTask.BillAddress);
        //    newTask.BillTo = newTask.BillAddress.AddressTypeCode;
        //}
        //if (newTask.StatementAddress != null)
        //{
        //    if (newTask.StatementAddress.AddressTypeCode != newTask.MainAddress.AddressTypeCode)
        //        newTask.SOP00101.Add(newTask.StatementAddress);
        //    newTask.StatementTo = newTask.StatementAddress.AddressTypeCode;
        //}
        //var result = _SOP00100Repository.AddKaizenObject(newTask);
        //Kaizen.BusinessLogic.Configuration.CompanyServices srv = new Configuration.CompanyServices(UserContext);

        //Kaizen.BusinessLogic.SOP.SOP00010Services srvdebtorClass = new SOP00010Services(UserContext);
        //srvdebtorClass.ExecuteSqlCommand("update SOP00010 set LastCustomerID = " + srv.GetNextCustomerKaizenID(newTask.CUSTCLAS.Trim())
        //    + " where CUSTCLAS = '" + newTask.CUSTCLAS.Trim() + "'");

        //srv.DeleteNextCustomerID(newTask.CUSTCLAS.Trim());
        //_SOP00100Repository.Add(newTask);
        //    return result;
        //}

    }
}
