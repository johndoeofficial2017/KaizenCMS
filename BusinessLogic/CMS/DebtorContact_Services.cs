using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00106Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00106Repository _CM00106Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00106Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00106Repository = new CM00106Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00106> GetByDebtorID(string DebtorID, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00106> L = null;
            var tasks = _CM00106Repository.GetListWithPaging(x => x.DebtorID.Trim() == DebtorID.Trim(), PageSize, Page, null);
            DataCollection<CM00106> result = tasks;
            return result;
        }
        public DataCollection<CM00106> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ContactTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DebtorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContactPerson", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PersonPosition", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PersonEmailAdd", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Pnone01", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00106> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00106Repository.GetListWithPaging(ss => ss.DebtorID.Trim() == DebtorID.Trim(), PageSize, Page, ss => Member);
            else
                result = _CM00106Repository.GetWhereListWithPaging("CM00106", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public IList<CM00106> GetAll()
        {
            var tasks = _CM00106Repository.GetAll();
            IList<CM00106> result = tasks;
            return result;
        }
        public IList<Kaizen.SOP.Sys00006> GetAllSys00006()
        {
            Kaizen.SOP.Repository.Sys00006Repository rep = new Kaizen.SOP.Repository.Sys00006Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll();
            IList<Kaizen.SOP.Sys00006> result = tasks;
            return result;
        }

        public IList<CM00106> GetAllByDebtorID(string DebtorID)
        {
            var tasks = _CM00106Repository.GetAll(ss => ss.DebtorID == DebtorID);
            IList<CM00106> result = tasks;
            return result;
        }
        public CM00106 GetSingle(string DebtorID, int ContactTypeID)
        {
            var tasks = _CM00106Repository.GetSingle(x => x.DebtorID == DebtorID && x.ContactTypeID == ContactTypeID);
            return tasks;
        }

        public KaizenResult AddCM00106(CM00106 newTask)
        {
            KaizenResult result = _CM00106Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00106(IList<CM00106> newTask)
        {
            KaizenResult result = _CM00106Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00106 newTask)
        {
            KaizenResult result = _CM00106Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00106> newTask)
        {
            KaizenResult result = _CM00106Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00106 newTask)
        {
            KaizenResult result = _CM00106Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00106> newTask)
        {
            KaizenResult result = _CM00106Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
