using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System;



namespace Kaizen.BusinessLogic.Configuration
{
    public class KaizenSessionServices
    {
        #region Infrastructure

        private KaizenSessionRepository _KaizenSessionRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public KaizenSessionServices()
        {
            _KaizenSessionRepository = new KaizenSessionRepository();
        }
        public KaizenSessionServices(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _KaizenSessionRepository = new KaizenSessionRepository(UserContext.UserName, UserContext.Password);
        }
        #endregion

        public DataCollection<KaizenSession> GetAllViewBYSQLQuery(string Filters,
DateTime FromDate, DateTime ToDate, string Searchcritaria, string UserName, string CompanyID,int PageSize, int Page, string Member, bool IsAscending)
        {
            DataCollection<KaizenSession> result = null;
            if (!string.IsNullOrEmpty(Filters))
                Filters += " and ";
            Filters += " CompanyID='" + CompanyID.Trim() + "'";
            Filters += " and UserName='" + UserName.Trim() + "'";
            Filters += " and DATEADD(dd, 0, DATEDIFF(dd, 0, LoginDate)) between DATEADD(dd, 0, DATEDIFF(dd, 0, '" + FromDate.ToString() + "')) and DATEADD(dd, 0, DATEDIFF(dd, 0, '" + ToDate.ToString() + "'))";

            result = _KaizenSessionRepository.GetWhereListWithPaging("KaizenSession", PageSize, Page, Filters, Member, IsAscending);
            return result;
        }
        public DataCollection<KaizenSession> GetListWithPagingFromKaizen(string Filters, string UserName, string CompanyID,
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
            KaizenSessionRepository rep = new KaizenSessionRepository();
            DataCollection<KaizenSession> result = null;
            result = rep.GetListWithPagingFromKaizen(ss => ss.UserName.Trim() == UserName.Trim() && ss.CompanyID.Trim() == CompanyID.Trim(), PageSize, Page, ss => Member);
            return result;
        }

        public DataCollection<KaizenSession> GetListWithPaging(string UserName, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<KaizenSession> result = null;

            var tasks = _KaizenSessionRepository.GetListWithPaging(x => x.UserName == UserName
                , PageSize, Page, ss => new { ss.LoginDate });
            result = tasks;
            return result;
        }
        public bool AddSession(KaizenSession newTask)
        {
            _KaizenSessionRepository.AddFromKaizen(newTask);
            return true;
        }
        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _KaizenSessionRepository.ExecuteSqlCommandInt(myQuery);
            return result;
        }

    }
}


