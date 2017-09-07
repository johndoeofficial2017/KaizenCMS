using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin.Repository;
using Kaizen.Admin;
using System.Data.Entity;

namespace Kaizen.BusinessLogic.Admin
{
    public class Prn00100Services
    {
        #region Infrastructure
        
        private Prn00100Repository _Prn00100RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Prn00100Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Prn00100RepositoryDataRepository = new Prn00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
        }
        #endregion
        public DataCollection<Prn00100> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TrxID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            Prn00100Repository rep = new Prn00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<Prn00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("Prn00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Prn00100> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Prn00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Prn00100RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Prn00100RepositoryDataRepository.GetWhereListWithPaging("Prn00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<Prn00100> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _Prn00100RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<Prn00100> result = tasks;
                return result;
            }
            else
            {
                var tasks = _Prn00100RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<Prn00100> result = tasks;
                return result;
            }

        }
        public DataCollection<Prn00100> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Prn00100> result = null;
            var tasks = _Prn00100RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.TrxID });
            result = tasks;
            return result;
        }
        public IList<Prn00100> GetAll()
        {
            var tasks = _Prn00100RepositoryDataRepository.GetAll();
            IList<Prn00100> result = tasks;
            return result;
        }
        public Prn00100 GetSingle(int TrxID)
        {
            var tasks = _Prn00100RepositoryDataRepository.GetSingle(x => x.TrxID == TrxID);
            return tasks;
        }

        public KaizenResult AddPrn00100(Prn00100 newTask)
        {
            KaizenResult result = _Prn00100RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddPrn00100(IList<Prn00100> newTask)
        {
            KaizenResult result = _Prn00100RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Prn00100 newTask)
        {
            KaizenResult result = _Prn00100RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Prn00100> newTask)
        {
            KaizenResult result = _Prn00100RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(int TrxID)
        {
            KaizenResult result = _Prn00100RepositoryDataRepository.RemoveKaizenObject(xx => xx.TrxID == TrxID);
            return result;
        }
        public KaizenResult Remove(IList<Prn00100> newTask)
        {
            KaizenResult result = _Prn00100RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
