using System;
using System.Collections.Generic;
using System.Linq;
using Kaizen.GL.Repository;
using Kaizen.GL;


namespace Kaizen.BusinessLogic.GL
{
    public class GL00310Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00310Repository _GL00310RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00310Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00310RepositoryDataRepository = new GL00310Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00310> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00310> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00310RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00310RepositoryDataRepository.GetWhereListWithPaging("GL00310", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<GL00310> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TransactionID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TransactionType", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CheckbookCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeRate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeRateID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeTableID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DocDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("RecAMT", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("RecFrom", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Description", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            GL00310Repository rep = new GL00310Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<GL00310> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("GL00310", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public IList<GL00310> GetAll()
        {
            var tasks = _GL00310RepositoryDataRepository.GetAll();
            IList<GL00310> result = tasks;
            return result;
        }
        public IList<Kaizen.Admin.Sys00021> GetAllSys00021()
        {
            Kaizen.Admin.Repository.Sys00021Repository rep = new Kaizen.Admin.Repository.Sys00021Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            IList<Kaizen.Admin.Sys00021> result = rep.GetAll();
            return result;
        }

        public IList<Kaizen.SOP.Sys00020> GetAllSys00020()
        {
            Kaizen.SOP.Repository.Sys00020Repository rep = new Kaizen.SOP.Repository.Sys00020Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll();
            return tasks;
        }

        public DataCollection<GL00310> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<GL00310> L = null;
            var tasks = _GL00310RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<GL00310> result = tasks;
            return result;
        }
        public DataCollection<GL00310> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00310> result = null;
            var tasks = _GL00310RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.TransactionID });
            result = tasks;
            return result;
        }
        public GL00310 GetSingle(string TransactionID)
        {
            var tasks = _GL00310RepositoryDataRepository.GetSingle(x => x.TransactionID == TransactionID);
            return tasks;
        }

        public KaizenResult AddGL00310(GL00310 newTask)
        {
            var result = _GL00310RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00310(IList<GL00310> newTask)
        {
            var result = _GL00310RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00310 newTask)
        {
            var result = _GL00310RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00310> newTask)
        {
            var result = _GL00310RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00310 newTask)
        {
            var result = _GL00310RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00310> newTask)
        {
            var result = _GL00310RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
