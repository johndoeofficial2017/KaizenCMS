using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00104Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00104Repository _CM00104Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00104Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00104Repository = new CM00104Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00104> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00104> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00104Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00104Repository.GetWhereListWithPaging("CM00104", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00104> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("AddressCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DebtorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00104> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00104Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00104Repository.GetWhereListWithPaging("CM00104", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<CM00104> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("AddressCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DebtorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AddressName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("WebSite", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CountryID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00104> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00104Repository.GetListWithPaging(ss => ss.DebtorID.Trim() == DebtorID.Trim(), PageSize, Page, ss => Member);
            else
                result = _CM00104Repository.GetWhereListWithPaging("CM00104", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<CM00104> GetAllViewBYSQLQueryWithAddressCode(string Filters, string AddressCode,
int PageSize, int Page, string Member, bool IsAscending)
        {
            DataCollection<CM00104> result = null;
            result = _CM00104Repository.GetListWithPaging(ss => ss.AddressCode == AddressCode, PageSize, Page, ss => Member);
            return result;
        }

        public DataCollection<CM00104> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00104> L = null;
            var tasks = _CM00104Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00104> result = tasks;
            return result;
        }
        public DataCollection<CM00104> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00104> result = null;
            var tasks = _CM00104Repository.GetListWithPaging(PageSize, Page, ss => new { ss.AddressCode });
            result = tasks;
            return result;
        }

        public IList<CM00104> GetAll()
        {
            var tasks = _CM00104Repository.GetAll();
            IList<CM00104> result = tasks;
            return result;
        }

        public IList<CM00104> GetAll(string DebtorID)
        {
            var tasks = _CM00104Repository.GetAll(ss => ss.DebtorID.Trim() == DebtorID.Trim());
            IList<CM00104> result = tasks;
            return result;
        }
        public IList<CM00104> GetByDebtorID(string DebtorID)
        {
            var tasks = _CM00104Repository.GetAll(ss => ss.DebtorID.Trim() == DebtorID.Trim());
            IList<CM00104> result = tasks;
            return result;
        }

        public IList<CM00104> GetByAddressCode(string AddressCode)
        {
            var tasks = _CM00104Repository.GetAll(ss => ss.AddressCode.Trim() == AddressCode.Trim());
            IList<CM00104> result = tasks;
            return result;
        }

        public CM00104 GetSingle(string AddressCode, string DebtorID)
        {
            var tasks = _CM00104Repository.GetSingle(x => x.AddressCode == AddressCode && x.DebtorID.Trim() == DebtorID.Trim());
            return tasks;
        }

        public KaizenResult AddCM00104(CM00104 newTask)
        {
            var result=_CM00104Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00104(IList<CM00104> newTask)
        {
            var result = _CM00104Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IList<CM00104> UpdateCM00104)
        {
            var result = _CM00104Repository.UpdateKaizenObject(UpdateCM00104.ToArray());
            return result;
        }
        public KaizenResult Update(CM00104 UpdateCM00104)
        {
            var result = _CM00104Repository.UpdateKaizenObject(UpdateCM00104);
            return result;
        }
        public KaizenResult Delete(CM00104 newTask)
        {
            var result = _CM00104Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00104> newTask)
        {
            var result = _CM00104Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
