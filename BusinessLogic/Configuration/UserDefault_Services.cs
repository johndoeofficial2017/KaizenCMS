using System.Collections.Generic;
using System.Linq;

using Kaizen.Configuration.Repository;
using Kaizen.Configuration;

namespace Kaizen.BusinessLogic.Configuration
{
    public class CompanyAccessServices
    {
        #region Infrastructure

        private CompanyAccessRepository _CompanyAccessRepository;
        private KaizenSession UserContext;
        public CompanyAccessServices()
        {
        }
        public CompanyAccessServices(KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
            }
            UserContext = _UserContext;
            _CompanyAccessRepository = new CompanyAccessRepository(UserContext.UserName, UserContext.Password);
        }
        #endregion
        public DataCollection<CompanyAccess> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CompanyAccess> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CompanyAccessRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CompanyAccessRepository.GetWhereListWithPaging("CompanyAccess", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CompanyAccess> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CompanyID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
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

            DataCollection<CompanyAccess> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CompanyAccessRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CompanyAccessRepository.GetWhereListWithPaging("CompanyAccess", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CompanyAccess> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CompanyID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
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

            DataCollection<CompanyAccess> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CompanyAccessRepository.GetListWithPaging(ss => ss.UserName.Trim() == UserName.Trim(), PageSize, Page, ss => Member);
            else
                result = _CompanyAccessRepository.GetWhereListWithPaging("CompanyAccess", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public List<CompanyAccess> GetAll()
        {
            var tasks = _CompanyAccessRepository.GetAll();
            List<CompanyAccess> result = tasks.ToList();
            return result;
        }
        public CompanyAccess GetSingle(string CompanyID, string UserName)
        {
            var tasks = _CompanyAccessRepository.GetSingle(x => x.CompanyID.Trim() == CompanyID.Trim() && x.UserName.Trim() == UserName.Trim());
            return tasks;
        }
        public CompanyAccess GetSingleFromKaizen(string CompanyID, string UserName)
        {
            CompanyAccessRepository rep = new CompanyAccessRepository();
            var tasks = rep.GetSingleFromKaizen(x => x.CompanyID.Trim() == CompanyID.Trim() && x.UserName.Trim() == UserName.Trim());
            return tasks;
        }
        public IList<CompanyAccess> GetByCompanyID(string CompanyID)
        {
            var tasks = _CompanyAccessRepository.GetAll(x => x.CompanyID.Trim() == CompanyID.Trim());
            return tasks;
        }
        public IList<CompanyAccess> GetByUserName(string UserName)
        {
            var tasks = _CompanyAccessRepository.GetAll(x => x.UserName.Trim() == UserName.Trim());
            return tasks;
        }

        public KaizenResult AddCompanyAccess(CompanyAccess newTask)
        {
            var result = _CompanyAccessRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCompanyAccess(IList<CompanyAccess> newTask)
        {
            var result = _CompanyAccessRepository.AddKaizenObject(newTask.ToArray());
            if (result.Status)
            {
                foreach (CompanyAccess com in newTask)
                {
                    _CompanyAccessRepository.ExecuteSqlCommandFromKaizen("USE " + com.CompanyID.Trim() + " CREATE USER [" + com.UserName.Trim() + "] FOR LOGIN [" + com.UserName.Trim() + "];");
                    _CompanyAccessRepository.ExecuteSqlCommandFromKaizen("USE " + com.CompanyID.Trim() + " EXEC sp_addrolemember N'db_owner', N'" + com.UserName.Trim() + "';");
                }
            }
            return result;
        }
        public KaizenResult Update(CompanyAccess newTask)
        {
            var result = _CompanyAccessRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CompanyAccess> newTask)
        {
            var result = _CompanyAccessRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CompanyAccess newTask)
        {
            var result = _CompanyAccessRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CompanyAccess> newTask)
        {
            var result = _CompanyAccessRepository.DeleteKaizenObject(newTask.ToArray());
            if (result.Status)
            {
                foreach (CompanyAccess com in newTask)
                {
                    CM00000Repository rep = new CM00000Repository(com.CompanyID, this.UserContext.UserName, UserContext.Password);
                    rep.ExecuteSqlCommandFromKaizen(" exec sp_droprolemember 'db_owner','" + com.UserName.Trim() + "';");
                    //rep.ExecuteSqlCommand("exec [dbo].[Testing] '101'");
                }
            }
            return result;
        }

        #region Company SMS Account 
        public IList<Kaizen00040> GetSMSAccountByCompany(string CompanyID)
        {
            Kaizen00040Repository rep = new Kaizen00040Repository(UserContext.UserName, UserContext.Password);
            var companySMSAccount = rep.GetAll(xx => xx.CompanyID == CompanyID);
            IList<Kaizen00040> result = companySMSAccount;
            return result;
        }
        public KaizenResult AddKaizen00040(Kaizen00040 caseStatusField)
        {
            Kaizen00040Repository rep = new Kaizen00040Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(caseStatusField);
            return result;
        }

        public KaizenResult UpdateKaizen00040(Kaizen00040 caseStatusField)
        {
            Kaizen00040Repository rep = new Kaizen00040Repository(this.UserContext.UserName, this.UserContext.Password);
            KaizenResult result = rep.UpdateKaizenObject(caseStatusField);
            return result;
        }

        public KaizenResult DeleteKaizen00040(Kaizen00040 caseStatusField)
        {
            Kaizen00040Repository rep = new Kaizen00040Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(caseStatusField);
            return result;
        }
        #endregion
    }
}
