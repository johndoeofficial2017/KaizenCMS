using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Kaizen.BusinessLogic.Configuration
{
    public class KaizenAuditServices
    {
        #region Infrastructure

        private KaizenAuditRepository _KaizenAuditRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public KaizenAuditServices(KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _KaizenAuditRepository = new KaizenAuditRepository(UserContext.UserName, UserContext.Password);
        }
        #endregion

        public DataCollection<KaizenAudit> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string Kaizen_USER, string Kaizen_DB,
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
                    SeachStr += Help.GetFilter("Kaizen_USER", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Kaizen_Table", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Kaizen_HOST", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Kaizen_DB", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Kaizen_DATE", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Ins", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Del", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<KaizenAudit> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                if (!string.IsNullOrEmpty(Kaizen_DB))
                {
                    result = _KaizenAuditRepository.GetListWithPaging(ss => ss.Kaizen_USER.Trim() == Kaizen_USER.Trim() && ss.Kaizen_DB.Trim() == Kaizen_DB.Trim(), PageSize, Page, ss => Member);
                }
                else
                    result = _KaizenAuditRepository.GetListWithPaging(ss => ss.Kaizen_USER.Trim() == Kaizen_USER.Trim(), PageSize, Page, ss => Member);
            }
            else
                result = _KaizenAuditRepository.GetWhereListWithPaging("KaizenAudit", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<KaizenAudit> GetWhereListWithPaging(string Filters,
            string Searchcritaria, DateTime From, DateTime To, string Kaizen_DB,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty; 
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            if (string.IsNullOrEmpty(SeachStr))
                SeachStr += " and ";
            if (string.IsNullOrEmpty(Kaizen_DB))
                Kaizen_DB = "Kaizen";
            SeachStr = "Kaizen_DB ='" + Kaizen_DB.Trim() + "' and DATEADD(dd, 0, DATEDIFF(dd, 0, Kaizen_DATE)) between DATEADD(dd, 0, DATEDIFF(dd, 0, '" + From .ToString()+ "')) and DATEADD(dd, 0, DATEDIFF(dd, 0, '"+ To.ToString() + "'))";

            DataCollection<KaizenAudit> result = null;
            result = _KaizenAuditRepository.GetWhereListWithPaging("KaizenAudit", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<KaizenAudit> GetAllViewBYSQLQuery(string Filters, string Kaizen_USER, string Kaizen_DB,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (!string.IsNullOrEmpty(Filters))
                SeachStr = Filters;
            else
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = " and ";
            }

            DataCollection<KaizenAudit> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                if (!string.IsNullOrEmpty(Kaizen_DB))
                {
                    result = _KaizenAuditRepository.GetListWithPaging(ss => ss.Kaizen_USER.Trim() == Kaizen_USER.Trim() && ss.Kaizen_DB.Trim() == Kaizen_DB.Trim(), PageSize, Page, ss => Member);
                }
                else
                    result = _KaizenAuditRepository.GetListWithPaging(ss => ss.Kaizen_USER.Trim() == Kaizen_USER.Trim(), PageSize, Page, ss => Member);
            }
            else
                result = _KaizenAuditRepository.GetWhereListWithPaging("KaizenAudit", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public KaizenResult Delete(KaizenAudit newTask)
        {
            var result = _KaizenAuditRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<KaizenAudit> newTask)
        {
            var result = _KaizenAuditRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}


