using System.Collections.Generic;
using System.Linq;

using Kaizen.Configuration.Repository;
using Kaizen.Configuration;

namespace Kaizen.BusinessLogic.Configuration
{
    public class KaizenUserRoleServices
    {
        #region Infrastructure

        private KaizenUserRoleRepository _KaizenUserRoleRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public KaizenUserRoleServices(KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
            }
            UserContext = _UserContext;
            _KaizenUserRoleRepository = new KaizenUserRoleRepository(UserContext.UserName, UserContext.Password);
        }
        #endregion
        public DataCollection<KaizenUserRole> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<KaizenUserRole> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _KaizenUserRoleRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _KaizenUserRoleRepository.GetWhereListWithPaging("KaizenUserRole", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<KaizenUserRole> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("RoleID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UserName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<KaizenUserRole> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _KaizenUserRoleRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _KaizenUserRoleRepository.GetWhereListWithPaging("KaizenUserRole", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<KaizenUserRole> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string UserName,
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
                    SeachStr += Help.GetFilter("RoleID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UserName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("YearCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DecimalDigit", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeTableID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsMultiply", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeRateID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeRate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrentDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CheckbookCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SOPTypeSetupID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SOPTYPE", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SiteID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AgentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<KaizenUserRole> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _KaizenUserRoleRepository.GetListWithPaging(ss => ss.UserName.Trim() == UserName.Trim(), PageSize, Page, ss => Member);
            else
                result = _KaizenUserRoleRepository.GetWhereListWithPaging("KaizenUserRole", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public List<KaizenUserRole> GetAll()
        {
            var tasks = _KaizenUserRoleRepository.GetAll();
            List<KaizenUserRole> result = tasks.ToList();
            return result;
        }
        public KaizenUserRole GetSingle(int RoleID, string UserName)
        {
            var tasks = _KaizenUserRoleRepository.GetSingle(x => x.RoleID == RoleID && x.UserName.Trim() == UserName.Trim());
            return tasks;
        }

        public IList<KaizenUserRole> GetByRoleID(int RoleID)
        {
            var tasks = _KaizenUserRoleRepository.GetAll(x => x.RoleID == RoleID);
            return tasks;
        }
        public IList<KaizenUserRole> GetByUserName(string UserName)
        {
            var tasks = _KaizenUserRoleRepository.GetAll(x => x.UserName.Trim() == UserName.Trim());
            return tasks;
        }

        public KaizenResult AddKaizenUserRole(KaizenUserRole newTask)
        {
            var result = _KaizenUserRoleRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizenUserRole(IList<KaizenUserRole> newTask)
        {
            var result = _KaizenUserRoleRepository.AddKaizenObject(newTask.ToArray());
            foreach (Company com in Master.InstalledCompany)
            {
                _KaizenUserRoleRepository.ExecuteSqlCommand("exec " + com.CompanyID.Trim() + ".[dbo].[Sys_TempReconcile20]");
            }
            return result;
        }
        public KaizenResult Update(KaizenUserRole newTask)
        {
            var result = _KaizenUserRoleRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<KaizenUserRole> newTask)
        {
            var result = _KaizenUserRoleRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(KaizenUserRole newTask)
        {
            var result = _KaizenUserRoleRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<KaizenUserRole> newTask)
        {
            var result = _KaizenUserRoleRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
