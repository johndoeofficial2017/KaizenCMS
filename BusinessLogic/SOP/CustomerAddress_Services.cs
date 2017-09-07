using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP00101Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00101Repository _SOP00101Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00101Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00101Repository = new SOP00101Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00101> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("AddressTypeCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CUSTNMBR", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AddressName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("WebSite", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CountryID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00101> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00101Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00101Repository.GetWhereListWithPaging("SOP00101", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<SOP00101> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP00101> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00101Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00101Repository.GetWhereListWithPaging("SOP00101", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<SOP00101> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("AddressTypeCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CUSTNMBR", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AddressName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("WebSite", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CountryID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00101> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00101Repository.GetListWithPaging(ss => ss.CUSTNMBR.Trim() == CUSTNMBR.Trim(), PageSize, Page, ss => Member);
            else
                result = _SOP00101Repository.GetWhereListWithPaging("SOP00101", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<SOP00101> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<SOP00101> L = null;
            var tasks = _SOP00101Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<SOP00101> result = tasks;
            return result;
        }
        public DataCollection<SOP00101> GetListWithPaging(string CUSTNMBR, int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<SOP00101> result = null;
            var tasks = _SOP00101Repository.GetListWithPaging(xx => xx.CUSTNMBR.Trim() == CUSTNMBR.Trim(), PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }


        public DataCollection<SOP00101> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00101> L = null;
            var tasks = _SOP00101Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<SOP00101> result = tasks;
            return result;
        }
        public DataCollection<SOP00101> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00101> result = null;
            var tasks = _SOP00101Repository.GetListWithPaging(PageSize, Page, ss => new { ss.AddressTypeCode });
            result = tasks;
            return result;
        }
        public DataCollection<SOP00101> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00101> result = null;
            var tasks = _SOP00101Repository.GetListWithPaging(PageSize, Page, ss => new { ss.AddressTypeCode });
            result = tasks;
            return result;
        }

        public IList<SOP00101> GetAll()
        {
            try
            {
                IList<SOP00101> L = null;
                try
                {
                    var tasks = _SOP00101Repository.GetAll();
                    IList<SOP00101> result = tasks;
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
                        System.Diagnostics.Debug.WriteLine("- AddressTypeCode: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return null;
        }
        public IList<SOP00101> GetByCUSTNMBR(string CUSTNMBR)
        {
            var tasks = _SOP00101Repository.GetAll(x => x.CUSTNMBR == CUSTNMBR.Trim());
            IList<SOP00101> result = tasks;
            return result;
        }

        public SOP00101 GetSingle(string AddressTypeCode, string CUSTNMBR)
        {
            var tasks = _SOP00101Repository.GetSingle(x => x.AddressTypeCode == AddressTypeCode, x => x.CUSTNMBR == CUSTNMBR);
            return tasks; 
        }

        public KaizenResult AddSOP00101(SOP00101 newTask)
        {
            var result = _SOP00101Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP00101(IList<SOP00101> newTask)
        {
            var result = _SOP00101Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP00101 newTask)
        {
            var result = _SOP00101Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP00101> newTask)
        {
            var result = _SOP00101Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(SOP00101 newTask)
        {
            var result = _SOP00101Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP00101> newTask)
        {
            var result = _SOP00101Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(string AddressTypeCode, string CUSTNMBR)
        {
            var result = _SOP00101Repository.RemoveKaizenObject(ss => ss.AddressTypeCode == AddressTypeCode && ss.CUSTNMBR == CUSTNMBR);
            return result;
        }
    }
}
