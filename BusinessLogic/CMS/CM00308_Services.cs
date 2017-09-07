using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
namespace Kaizen.BusinessLogic.CMS
{
    public class CM00308Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00308Repository _CM00308Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00308Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00308Repository = new CM00308Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<CM00308> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("PaymentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CaseRef", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Amount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00308Repository rep = new CM00308Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00308> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00308", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00308> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00308> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00308Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00308Repository.GetWhereListWithPaging("CM00308", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public IList<CM00308> GetAll()
        {
            var tasks = _CM00308Repository.GetAll();
            IList<CM00308> result = tasks;
            return result;
        }
        public CM00308 GetSingle(string PaymentID, string CaseRef)
        {
            var tasks = _CM00308Repository.GetSingle(x => x.PaymentID == PaymentID && x.CaseRef== CaseRef);
            return tasks;
        }

        public KaizenResult AddCM00308(CM00308 newTask)
        {
            var result = _CM00308Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00308(IList<CM00308> newTask)
        {
            var result = _CM00308Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00308 newTask)
        {
            var result = _CM00308Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00308> newTask)
        {
            var result = _CM00308Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00308 newTask)
        {
            var result = _CM00308Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00308> newTask)
        {
            var result = _CM00308Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}

