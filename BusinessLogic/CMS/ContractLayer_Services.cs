using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00020Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00020Repository _CM00020Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00020Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00020Repository = new CM00020Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00020> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00020> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00020Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00020Repository.GetWhereListWithPaging("CM00020", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00020> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ActionTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00020> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00020Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00020Repository.GetWhereListWithPaging("CM00020", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public IList<CM00020> GetAll()
        {
            var tasks = _CM00020Repository.GetAll();
            IList<CM00020> result = tasks;
            return result;
        }
        public IList<CM00020> GetAll(string ContractID)
        {
            var tasks = _CM00020Repository.GetAll(xx => xx.ContractID == ContractID.Trim());
            IList<CM00020> result = tasks;
            return result;
        }


        public KaizenResult AddCM00020(CM00020 newTask)
        {
            KaizenResult result = _CM00020Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00020(IList<CM00020> newTask)
        {
            KaizenResult result = _CM00020Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00020 newTask)
        {
            KaizenResult result = _CM00020Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00020> newTask)
        {
            KaizenResult result = _CM00020Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00020 newTask)
        {
            KaizenResult result = _CM00020Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00020> newTask)
        {
            KaizenResult result = _CM00020Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
