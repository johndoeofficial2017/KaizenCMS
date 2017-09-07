using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP00501Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00501Repository _SOP00501Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00501Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00501Repository = new SOP00501Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00501> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP00501> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00501Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00501Repository.GetWhereListWithPaging("SOP00501", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP00501> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("BatchID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BatchAmount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BatchDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BatchTRXcount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsTransactionDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PostingDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00501> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00501Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00501Repository.GetWhereListWithPaging("SOP00501", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<SOP00501> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<SOP00501> L = null;
            var tasks = _SOP00501Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<SOP00501> result = tasks;
            return result;
        }
        public DataCollection<SOP00501> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<SOP00501> result = null;
            var tasks = _SOP00501Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public DataCollection<SOP00501> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00501> L = null;
            var tasks = _SOP00501Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<SOP00501> result = tasks;
            return result;
        }
        public DataCollection<SOP00501> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00501> result = null;
            var tasks = _SOP00501Repository.GetListWithPaging(PageSize, Page, ss => new { ss.BatchID });
            result = tasks;
            return result;
        }
        public DataCollection<SOP00501> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00501> result = null;
            var tasks = _SOP00501Repository.GetListWithPaging(PageSize, Page, ss => new { ss.BatchID });
            result = tasks;
            return result;
        }

        public IList<SOP00501> GetAll()
        {
            try
            {
                IList<SOP00501> L = null;
                try
                {
                    var tasks = _SOP00501Repository.GetAll();
                    IList<SOP00501> result = tasks;
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
        public SOP00501 GetSingle(string BatchID)
        {
            var tasks = _SOP00501Repository.GetSingle(x => x.BatchID == BatchID);
            return tasks;
        }

        public KaizenResult AddSOP00501(SOP00501 newTask)
        {
            var result = _SOP00501Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP00501(IList<SOP00501> newTask)
        {
            var result = _SOP00501Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP00501 newTask)
        {
            var result = _SOP00501Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP00501> newTask)
        {
            var result = _SOP00501Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP00501 newTask)
        {
            var result = _SOP00501Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP00501> newTask)
        {
            var result = _SOP00501Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
