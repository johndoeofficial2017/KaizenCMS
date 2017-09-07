using System.Collections.Generic;
using Kaizen.GL.Repository;
using System.Linq;
using Kaizen.GL;


namespace Kaizen.BusinessLogic.GL
{
    public class GL00315Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00315Repository _GL00315RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00315Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00315RepositoryDataRepository = new GL00315Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<GL00315> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00315> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00315RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00315RepositoryDataRepository.GetWhereListWithPaging("GL00315", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00315> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TransaferID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CheckbookCodeFrom", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCodeFrom", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeRateFrom", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeRateIDFrom", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeTableIDFrom", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DecimalDigitFrom", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CheckbookCodeTo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCodeTo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeRateTo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeRateIDo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExchangeTableIDTo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DecimalDigitTo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountIDFrom", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountIDTo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountRefFrom", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountRefTo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DocDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DocAMT", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DocFrom", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Description", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VoidDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VoidDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            GL00315Repository rep = new GL00315Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<GL00315> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("GL00315", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public IList<GL00315> GetAll()
        {
            var tasks = _GL00315RepositoryDataRepository.GetAll();
            IList<GL00315> result = tasks;
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

        public DataCollection<GL00315> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<GL00315> L = null;
            var tasks = _GL00315RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<GL00315> result = tasks;
            return result;
        }
        public DataCollection<GL00315> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00315> result = null;
            var tasks = _GL00315RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.TransaferID });
            result = tasks;
            return result;
        }
        public GL00315 GetSingle(string TransaferID)
        {
            var tasks = _GL00315RepositoryDataRepository.GetSingle(x => x.TransaferID == TransaferID);
            return tasks;
        }

        public KaizenResult AddGL00315(GL00315 newTask)
        {
            var result = _GL00315RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00315(IList<GL00315> newTask)
        {
            var result = _GL00315RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00315 newTask)
        {
            var result = _GL00315RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00315> newTask)
        {
            var result = _GL00315RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00315 newTask)
        {
            var result = _GL00315RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00315> newTask)
        {
            var result = _GL00315RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
