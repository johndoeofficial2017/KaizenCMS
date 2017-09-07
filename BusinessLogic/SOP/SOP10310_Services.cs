using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP10310Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10310Repository _SOP10310Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10310Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10310Repository = new SOP10310Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<SOP10310> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP10310> result = null;
            //if (string.IsNullOrEmpty(SeachStr))
            //    result = _SOP10310Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            //else
            result = _SOP10310Repository.GetWhereListWithPaging("SOP10310", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP10310> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("SOPNUMBE", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BatchID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CUSTNMBR", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CustomerPoNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP10310> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10310Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10310Repository.GetWhereListWithPaging("SOP10310", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public IList<SOP10310> GetListByCUSTNMBR(string CUSTNMBR)
        {
            IList<SOP10310> result = null;
            var tasks = _SOP10310Repository.GetAll(ss => ss.CUSTNMBR == CUSTNMBR);
            result = tasks;
            return result;
        }

        public IList<SOP10310> GetAll()
        {
            var tasks = _SOP10310Repository.GetAll();
            IList<SOP10310> result = tasks;
            return result;
        }
        public SOP10310 GetSingle(string SOPNUMBE)
        {
            var tasks = _SOP10310Repository.GetSingle(x => x.SOPNUMBE == SOPNUMBE);
            return tasks;
        }

        public KaizenResult AddSOP10310(SOP10310 newTask)
        {
            var result = _SOP10310Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP10310(IList<SOP10310> newTask)
        {
            var result = _SOP10310Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP10310 newTask)
        {
            var result = _SOP10310Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP10310> newTask)
        {
            var result = _SOP10310Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP10310 newTask)
        {
            var result = _SOP10310Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP10310> newTask)
        {
            var result = _SOP10310Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
