using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
using System.Globalization;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP00015Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00015Repository _SOP00015Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00015Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00015Repository = new SOP00015Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00015> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP00015> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00015Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00015Repository.GetWhereListWithPaging("SOP00015", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP00015> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("PaymentTermID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PaymentTermName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00015> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00015Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00015Repository.GetWhereListWithPaging("SOP00015", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP00015> GetAllSOP00015(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00015> L = null;
            var tasks = _SOP00015Repository.GetListWithPaging(x => x.PaymentTermID.Contains(SearchTerm), PageSize, Page, ss => ss.PaymentTermID, null);

            DataCollection<SOP00015> result = tasks;
            return result;
        }
        public DataCollection<SOP00015> GetByPaymentTermID(string PaymentTermID, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00015> L = null;
            var tasks = _SOP00015Repository.GetListWithPaging(x => x.PaymentTermID.Trim() == PaymentTermID.Trim(), PageSize, Page, ss => ss.PaymentTermID, null);

            DataCollection<SOP00015> result = tasks;
            return result;
        }

        public IList<SOP00015> GetAll()
        {
            var tasks = _SOP00015Repository.GetAll();
            IList<SOP00015> result = tasks;
            return result;
        }
        public SOP00015 GetSingle(string PaymentTermID)
        {
            var tasks = _SOP00015Repository.GetSingle(x => x.PaymentTermID.Trim() == PaymentTermID.Trim());
            return tasks;
        }

        public KaizenResult AddSOP00015(SOP00015 newTask)
        {
            var result = _SOP00015Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP00015(IList<SOP00015> newTask)
        {
            var result = _SOP00015Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP00015 newTask)
        {
            var result = _SOP00015Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP00015> newTask)
        {
            var result = _SOP00015Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP00015 newTask)
        {
            var result = _SOP00015Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP00015> newTask)
        {
            var result = _SOP00015Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
