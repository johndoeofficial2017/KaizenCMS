using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00012Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00012Repository _IV00012Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00012Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00012Repository = new IV00012Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00012> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00012> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00012Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00012Repository.GetWhereListWithPaging("IV00012", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00012> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("SiteID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SiteName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SiteDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BinTrack", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Address", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Phone01", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Phone02", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Phone03", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Phone04", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Phone05", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SiteManger", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00012> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00012Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00012Repository.GetWhereListWithPaging("IV00012", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00012> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("BinID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BinName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00012> result = null;
            if (!string.IsNullOrEmpty(SiteID))
                result = _IV00012Repository.GetListWithPaging(ss => ss.SiteID.Trim() == SiteID.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00012Repository.GetWhereListWithPaging("IV00012", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00012> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<IV00012> L = null;
            var tasks = _IV00012Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<IV00012> result = tasks;
            return result;
        }
        public DataCollection<IV00012> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<IV00012> result = null;
            var tasks = _IV00012Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<IV00012> GetAll()
        {
            var tasks = _IV00012Repository.GetAll();
            IList<IV00012> result = tasks;
            return result;
        }
        public IList<IV00012> GetBySiteID(string SiteID)
        {
            var tasks = _IV00012Repository.GetAll(ss => ss.SiteID.Trim() == SiteID.Trim());
            IList<IV00012> result = tasks;
            return result;
        }
        public IList<IV00012> GetBinGroupBySiteID(string SiteID)
        {
            var tasks = _IV00012Repository.GetAll(ss => ss.SiteID.Trim() == SiteID.Trim() && ss.IsBinGroup == true);
            IList<IV00012> result = tasks;
            return result;
        }

        public DataCollection<IV00012> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<IV00012> result = null;
            var tasks = _IV00012Repository.GetListWithPaging(PageSize, Page, ss => new { ss.BinID });
            result = tasks;
            return result;
        }
        public DataCollection<IV00012> GetListWithPaging(string SiteID, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                DataCollection<IV00012> result = null;
                var tasks = _IV00012Repository.GetListWithPaging(x => x.SiteID.Trim() == SiteID.Trim()
                    , PageSize, Page, ss => ss.SiteID);
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
        public IV00012 GetSingle(string BinID)
        {
            var tasks = _IV00012Repository.GetSingle(x => x.BinID == BinID);
            return tasks;
        }
        public IList<IV00012> GetAllBins(string SiteID)
        {
            var tasks = _IV00012Repository.GetAll(xx => xx.SiteID == SiteID);
            IList<IV00012> result = tasks;
            return result;
        }
        public KaizenResult AddIV00012(IV00012 newTask)
        {
            var result = _IV00012Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00012(IList<IV00012> newTask)
        {
            var result = _IV00012Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00012 newTask)
        {
            var result = _IV00012Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00012> newTask)
        {
            var result = _IV00012Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00012 newTask)
        {
            var result = _IV00012Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00012> newTask)
        {
            var result = _IV00012Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}



