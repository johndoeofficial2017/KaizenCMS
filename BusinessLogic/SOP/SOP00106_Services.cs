using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
using System.Globalization;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP00106Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00106Repository _SOP00106Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00106Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00106Repository = new SOP00106Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00106> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("DocumentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DocumentTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DocumentName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DocumentDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PhotoExtension", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00106> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00106Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00106Repository.GetWhereListWithPaging("SOP00106", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<SOP00106> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP00106> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00106Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00106Repository.GetWhereListWithPaging("SOP00106", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<SOP00106> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CUSTNMBR", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DocumentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DocumentTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DocumentName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DocumentDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PhotoExtension", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00106> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00106Repository.GetListWithPaging(ss => ss.CUSTNMBR.Trim() == CUSTNMBR.Trim(), PageSize, Page, ss => Member);
            else
                result = _SOP00106Repository.GetWhereListWithPaging("SOP00106", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<SOP00106> GetAllSOP00106(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<SOP00106> L = null;
                var tasks = _SOP00106Repository.GetListWithPaging(x => x.CUSTNMBR.Contains(SearchTerm), PageSize, Page, ss => ss.DocumentID, null);

                DataCollection<SOP00106> result = tasks;
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
        public DataCollection<SOP00106> GetByCUSTNMBR(string CUSTNMBR, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<SOP00106> L = null;
                var tasks = _SOP00106Repository.GetListWithPaging(x => x.CUSTNMBR.Trim() == CUSTNMBR.Trim(), PageSize, Page, ss => ss.DocumentID, null);

                DataCollection<SOP00106> result = tasks;
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

        public IList<SOP00106> GetAll()
        {
            var tasks = _SOP00106Repository.GetAll();
            IList<SOP00106> result = tasks;
            return result;
        }
        public IList<SOP00106> GetAllByCUSTNMBR(string CUSTNMBR)
        {
            var tasks = _SOP00106Repository.GetAll(ss => ss.CUSTNMBR.Trim() == CUSTNMBR.Trim()
                , ss => new { ss.DocumentID });
            IList<SOP00106> result = tasks;
            return result;
        }
        public SOP00106 GetSingle(int DocumentID)
        {
            var tasks = _SOP00106Repository.GetSingle(x => x.DocumentID == DocumentID);
            return tasks;
        }

        public IList<Sys00007> GetAllSys00007()
        {
            Sys00007Repository r = new Sys00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<Sys00007> result = tasks;
            return result;
        }

        public KaizenResult AddSOP00106(SOP00106 newTask)
        {
            var result = _SOP00106Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP00106(IList<SOP00106> newTask)
        {
            var result = _SOP00106Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(SOP00106 newTask)
        {
            var result = _SOP00106Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP00106> newTask)
        {
            var result = _SOP00106Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP00106 newTask, string ServerPath)
        {
            var result = _SOP00106Repository.DeleteKaizenObject(newTask);
            if (result.Status)
            {
                string Destination = ServerPath + newTask.DocumentName.Trim() + newTask.PhotoExtension.Trim();
                if (System.IO.File.Exists(Destination))
                {
                    System.IO.File.Delete(Destination);
                }
            }
            return result;
        }
        public KaizenResult Delete(IList<SOP00106> newTask, string ServerPath)
        {
            var result = _SOP00106Repository.DeleteKaizenObject(newTask.ToArray());
            if (result.Status)
            {
                foreach (var item in newTask)
                {
                    string Destination = ServerPath + item.DocumentName.Trim() + item.PhotoExtension.Trim();
                    if (System.IO.File.Exists(Destination))
                    {
                        System.IO.File.Delete(Destination);
                    }
                }
            }
            return result;
        }

    }
}
