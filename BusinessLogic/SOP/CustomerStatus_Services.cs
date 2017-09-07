using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP00014Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00014Repository _SOP00014Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00014Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00014Repository = new SOP00014Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00014> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP00014> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00014Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00014Repository.GetWhereListWithPaging("SOP00014", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP00014> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<SOP00014> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00014Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00014Repository.GetWhereListWithPaging("SOP00014", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<SOP00014> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00014> L = null;
            var tasks = _SOP00014Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<SOP00014> result = tasks;
            return result;
        }
        public DataCollection<SOP00014> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00014> result = null;
            var tasks = _SOP00014Repository.GetListWithPaging(PageSize, Page, ss => new { ss.StatusID });
            result = tasks;
            return result;
        }
        public DataCollection<SOP00014> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00014> result = null;
            var tasks = _SOP00014Repository.GetListWithPaging(PageSize, Page, ss => new { ss.StatusID });
            result = tasks;
            return result;
        }

        public IList<SOP00014> GetAll()
        {
            try
            {
                IList<SOP00014> L = null;
                try
                {
                    var tasks = _SOP00014Repository.GetAll();
                    IList<SOP00014> result = tasks;
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
        public SOP00014 GetSingle(int StatusID)
        {
            var tasks = _SOP00014Repository.GetSingle(x => x.StatusID == StatusID);
            return tasks;
        }

        public KaizenResult AddSOP00014(SOP00014 newTask)
        {
            var result = _SOP00014Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP00014(IList<SOP00014> newTask)
        {
            var result = _SOP00014Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP00014 newTask)
        {
            var result = _SOP00014Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP00014> newTask)
        {
            var result = _SOP00014Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP00014 newTask)
        {
            var result = _SOP00014Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP00014> newTask)
        {
            var result = _SOP00014Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
