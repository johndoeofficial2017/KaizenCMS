using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;


namespace Kaizen.BusinessLogic.CMS
{
    public class CM00208Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00208Repository _CM00208Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00208Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00208Repository = new CM00208Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00208> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string CaseRef,
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
                    SeachStr += Help.GetFilter("CaseRef", (KaizenFilterOperator)FltrOperator, Searchcritaria);
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

            DataCollection<CM00208> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00208Repository.GetListWithPaging(ss => ss.CaseRef.Trim() == CaseRef.Trim(), PageSize, Page, ss => Member);
            else
                result = _CM00208Repository.GetWhereListWithPaging("CM00208", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00208> GetAllCM00208(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
                var tasks = _CM00208Repository.GetListWithPaging(x => x.CaseRef.Contains(SearchTerm), PageSize, Page, ss => ss.DocumentID, null);
                DataCollection<CM00208> result = tasks;
                return result;
        }
        public IList<Kaizen.SOP.Sys00007> GetAllSys00007()
        {
            Kaizen.SOP.Repository.Sys00007Repository r = new Kaizen.SOP.Repository.Sys00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<Kaizen.SOP.Sys00007> result = tasks;
            return result;
        }

        public DataCollection<CM00208> GetByCaseRef(string CaseRef, int PageSize, int Page, string Member, string SortDirection)
        {
                var tasks = _CM00208Repository.GetListWithPaging(x => x.CaseRef.Trim() == CaseRef.Trim(), PageSize, Page, ss => ss.DocumentID, null);
                DataCollection<CM00208> result = tasks;
                return result;
        }

        public IList<CM00208> GetAll()
        {
                    var tasks = _CM00208Repository.GetAll();
                    IList<CM00208> result = tasks;
                    return result;
        }
        public IList<CM00208> GetAllByCaseRef(string CaseRef)
        {
            var tasks = _CM00208Repository.GetAll(ss => ss.CaseRef.Trim() == CaseRef.Trim());
            IList<CM00208> result = tasks;
            return result;
        }
        public CM00208 GetSingle(int DocumentID)
        {
                var tasks = _CM00208Repository.GetSingle(x => x.DocumentID == DocumentID);
                return tasks;
        }
  
        public KaizenResult AddCM00208(CM00208 newTask)
        {
            var result = _CM00208Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00208(IList<CM00208> newTask)
        {
            var result = _CM00208Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00208 newTask)
        {
            var result = _CM00208Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00208> newTask)
        {
            var result = _CM00208Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00208 newTask, string ServerPath)
        {
            KaizenResult result = _CM00208Repository.DeleteKaizenObject(newTask);
            string Destination = ServerPath + newTask.DocumentName.Trim() + newTask.PhotoExtension.Trim();
            if (System.IO.File.Exists(Destination))
            {
                System.IO.File.Delete(Destination);
            }
            return result;
        }
        public KaizenResult Delete(IList<CM00208> newTask, string ServerPath)
        {
            KaizenResult result = _CM00208Repository.DeleteKaizenObject(newTask.ToArray());
            foreach (var item in newTask)
            {
                string Destination = ServerPath + item.DocumentName.Trim() + item.PhotoExtension.Trim();
                if (System.IO.File.Exists(Destination))
                {
                    System.IO.File.Delete(Destination);
                }
            }
            return result;
        }

    }
}
