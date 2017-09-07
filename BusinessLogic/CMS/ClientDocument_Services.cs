using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00112Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00112Repository _CM00112Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00112Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00112Repository = new CM00112Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00112> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<CM00112> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00112Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00112Repository.GetWhereListWithPaging("CM00112", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<CM00112> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00112> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00112Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00112Repository.GetWhereListWithPaging("CM00112", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00112> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string ClientID,
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
                    SeachStr += Help.GetFilter("ClientID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
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

            DataCollection<CM00112> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00112Repository.GetListWithPaging(ss => ss.ClientID.Trim() == ClientID.Trim(), PageSize, Page, ss => Member);
            else
                result = _CM00112Repository.GetWhereListWithPaging("CM00112", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00112> GetByClientID(string ClientID, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<CM00112> L = null;
                var tasks = _CM00112Repository.GetListWithPaging(x => x.ClientID.Trim() == ClientID.Trim(), PageSize, Page, null);
                DataCollection<CM00112> result = tasks;
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

        public IList<CM00112> GetAll()
        {
            var tasks = _CM00112Repository.GetAll();
            IList<CM00112> result = tasks;
            return result;
        }
        public IList<CM00112> GetAllByClientID(string ClientID)
        {
            var tasks = _CM00112Repository.GetAll(ss => ss.ClientID.Trim() == ClientID.Trim());
            IList<CM00112> result = tasks;
            return result;
        }
        public CM00112 GetSingle(int DocumentID)
        {
            var tasks = _CM00112Repository.GetSingle(x => x.DocumentID == DocumentID);
            return tasks;
        }
        public IList<Kaizen.SOP.Sys00007> GetAllSys00007()
        {
            Kaizen.SOP.Repository.Sys00007Repository r = new Kaizen.SOP.Repository.Sys00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<Kaizen.SOP.Sys00007> result = tasks;
            return result;
        }


        public KaizenResult AddCM00112(CM00112 newTask)
        {
            var result = _CM00112Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00112(IList<CM00112> newTask)
        {
            var result = _CM00112Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00112 newTask)
        {
            var result = _CM00112Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00112> newTask)
        {
            var result = _CM00112Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00112 newTask, string ServerPath)
        {
            var result = _CM00112Repository.DeleteKaizenObject(newTask);
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
        public KaizenResult Delete(IList<CM00112> newTask, string ServerPath)
        {
            var result = _CM00112Repository.DeleteKaizenObject(newTask.ToArray());
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
