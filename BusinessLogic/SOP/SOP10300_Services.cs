using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP10300Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10300Repository _SOP10300Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10300Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10300Repository = new SOP10300Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<SOP10300> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP10300> result = null;
            //if (string.IsNullOrEmpty(SeachStr))
            //    result = _SOP10300Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            //else
            result = _SOP10300Repository.GetWhereListWithPaging("SOP10300", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP10300> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<SOP10300> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10300Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10300Repository.GetWhereListWithPaging("SOP10300", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public IList<SOP10300> GetListByCUSTNMBR(string CUSTNMBR)
        {
            IList<SOP10300> result = null;
            var tasks = _SOP10300Repository.GetAll(ss => ss.CUSTNMBR == CUSTNMBR);
            result = tasks;
            return result;
        }

        public IList<SOP10300> GetAll()
        {
            var tasks = _SOP10300Repository.GetAll();
            IList<SOP10300> result = tasks;
            return result;
        }
        public SOP10300 GetSingle(string SOPNUMBE)
        {
            var tasks = _SOP10300Repository.GetSingle(x => x.SOPNUMBE == SOPNUMBE);
            return tasks;
        }

        public KaizenResult AddSOP10300(SOP10300 newTask)
        {
            newTask.DOCDATE = DateTime.Now;
            newTask.UserName = UserContext.UserName;
            newTask.KaizenID = UserContext.KaizenID;
            var result = _SOP10300Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP10300(IList<SOP10300> newTask)
        {
            var result = _SOP10300Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP10300 newTask)
        {
            var result = _SOP10300Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP10300> newTask)
        {
            var result = _SOP10300Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP10300 newTask)
        {
            var result = _SOP10300Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP10300> newTask)
        {
            var result = _SOP10300Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
