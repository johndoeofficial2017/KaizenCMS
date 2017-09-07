using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00101Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00101Repository _CM00101Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00101Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00101Repository = new CM00101Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00101> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string DebtorID,
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
                    SeachStr += Help.GetFilter("DebtorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
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

            DataCollection<CM00101> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00101Repository.GetListWithPaging(ss => ss.DebtorID.Trim() == DebtorID.Trim(), PageSize, Page, ss => Member);
            else
                result = _CM00101Repository.GetWhereListWithPaging("CM00101", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00101> GetByDebtorID(string DebtorID, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00101> L = null;
            var tasks = _CM00101Repository.GetListWithPaging(x => x.DebtorID.Trim() == DebtorID.Trim(), PageSize, Page, null);
            DataCollection<CM00101> result = tasks;
            return result;
        }

        public IList<CM00101> GetAll()
        {
            var tasks = _CM00101Repository.GetAll();
            IList<CM00101> result = tasks;
            return result;
        }
        public IList<CM00101> GetAllByDebtorID(string DebtorID)
        {
            var tasks = _CM00101Repository.GetAll(ss => ss.DebtorID.Trim() == DebtorID.Trim());
            IList<CM00101> result = tasks;
            return result;
        }
        public CM00101 GetSingle(int DocumentID)
        {
            var tasks = _CM00101Repository.GetSingle(x => x.DocumentID == DocumentID);
            return tasks;
        }
        public IList<Kaizen.SOP.Sys00007> GetAllSys00007()
        {
            Kaizen.SOP.Repository.Sys00007Repository r = new Kaizen.SOP.Repository.Sys00007Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = r.GetAll();
            IList<Kaizen.SOP.Sys00007> result = tasks;
            return result;
        }


        public KaizenResult AddCM00101(CM00101 newTask)
        {
            KaizenResult result = _CM00101Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00101(IList<CM00101> newTask)
        {
            KaizenResult result = _CM00101Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00101 newTask)
        {
            KaizenResult result = _CM00101Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00101> newTask)
        {
            KaizenResult result = _CM00101Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00101 newTask, string ServerPath)
        {
            KaizenResult result = _CM00101Repository.DeleteKaizenObject(newTask);
            string Destination = ServerPath + newTask.DocumentName.Trim() + newTask.PhotoExtension.Trim();
            if (System.IO.File.Exists(Destination))
            {
                System.IO.File.Delete(Destination);
            }
            return result;
        }
        public KaizenResult Delete(IList<CM00101> newTask, string ServerPath)
        {
            KaizenResult result = _CM00101Repository.DeleteKaizenObject(newTask.ToArray());
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
