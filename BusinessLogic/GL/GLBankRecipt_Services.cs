using System;
using System.Collections.Generic;
using Kaizen.GL.Repository;
using System.Linq;
using Kaizen.GL;


namespace Kaizen.BusinessLogic.GL
{
    public class GL00305Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00305Repository _GL00305RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00305Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00305RepositoryDataRepository = new GL00305Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00305> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00305> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00305RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00305RepositoryDataRepository.GetWhereListWithPaging("GL00305", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<GL00305> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("PaymentMethodID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
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

            GL00305Repository rep = new GL00305Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<GL00305> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("GL00305", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public IList<GL00305> GetAll()
        {
            var tasks = _GL00305RepositoryDataRepository.GetAll();
            IList<GL00305> result = tasks;
            return result;
        }
        public IList<Kaizen.SOP.Sys00020> GetAllSys00020()
        {
            Kaizen.SOP.Repository.Sys00020Repository rep = new Kaizen.SOP.Repository.Sys00020Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll();
            return tasks;
        }

        public DataCollection<GL00305> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<GL00305> L = null;
            var tasks = _GL00305RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<GL00305> result = tasks;
            return result;
        }
        public DataCollection<GL00305> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00305> result = null;
            var tasks = _GL00305RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.TransactionID });
            result = tasks;
            return result;
        }
        public GL00305 GetSingle(string TransactionID)
        {
            var tasks = _GL00305RepositoryDataRepository.GetSingle(x => x.TransactionID == TransactionID);
            return tasks;
        }

        public KaizenResult AddGL00305(GL00305 newTask)
        {
            var result = _GL00305RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00305(IList<GL00305> newTask)
        {
            var result = _GL00305RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00305 newTask)
        {
            var result = _GL00305RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00305> newTask)
        {
            var result = _GL00305RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00305 newTask)
        {
            var result = _GL00305RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00305> newTask)
        {
            var result = _GL00305RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
