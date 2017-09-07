using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP00008Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00008Repository _SOP00008Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00008Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00008Repository = new SOP00008Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00008> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP00008> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00008Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00008Repository.GetWhereListWithPaging("SOP00008", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP00008> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ProjectID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ProjectName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ProjectDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00008> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00008Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00008Repository.GetWhereListWithPaging("SOP00008", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<SOP00008> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00008> L = null;
            var tasks = _SOP00008Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<SOP00008> result = tasks;
            return result;
        }
        public DataCollection<SOP00008> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00008> result = null;
            var tasks = _SOP00008Repository.GetListWithPaging(PageSize, Page, ss => new { ss.SalePersonTypeID });
            result = tasks;
            return result;
        }
        public DataCollection<SOP00008> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<SOP00008> result = null;
            var tasks = _SOP00008Repository.GetListWithPaging(PageSize, Page, ss => new { ss.SalePersonTypeID });
            result = tasks;
            return result;
        }

        public IList<SOP00008> GetAll()
        {
            try
            {
                IList<SOP00008> L = null;
                try
                {
                    var tasks = _SOP00008Repository.GetAll();
                    IList<SOP00008> result = tasks;
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
        public SOP00008 GetSingle(int SalePersonTypeID)
        {
            var tasks = _SOP00008Repository.GetSingle(x => x.SalePersonTypeID == SalePersonTypeID);
            return tasks;
        }

        public KaizenResult AddSOP00008(SOP00008 newTask)
        {
            var result = _SOP00008Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP00008(IList<SOP00008> newTask)
        {
            var result = _SOP00008Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP00008 newTask)
        {
            var result = _SOP00008Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP00008> newTask)
        {
            var result = _SOP00008Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP00008 newTask)
        {
            var result = _SOP00008Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP00008> newTask)
        {
            var result = _SOP00008Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
