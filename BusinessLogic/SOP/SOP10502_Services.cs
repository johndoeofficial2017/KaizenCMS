using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP10502Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10502Repository _SOP10502Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10502Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10502Repository = new SOP10502Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<SOP10502> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP10502> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10502Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10502Repository.GetWhereListWithPaging("SOP10502", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP10502> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("LotRowID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LineID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BarCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LOTLineCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AppliedQuantity", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP10502> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10502Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10502Repository.GetWhereListWithPaging("SOP10502", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public IList<SOP10502> GetListByLineID(int LineID)
        {
            IList<SOP10502> result = null;
            //var tasks = _SOP10502Repository.GetAll(ss => ss.LineID == LineID);
            //result = tasks;
            return result;
        }

        public IList<SOP10502> GetAll()
        {
            var tasks = _SOP10502Repository.GetAll();
            IList<SOP10502> result = tasks;
            return result;
        }
        public SOP10502 GetSingle(int LotRowID)
        {
            var tasks = _SOP10502Repository.GetSingle(x => x.LotRowID == LotRowID);
            return tasks;
        }

        public KaizenResult AddSOP10502(SOP10502 newTask)
        {
            var result = _SOP10502Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP10502(IList<SOP10502> newTask)
        {
            var result = _SOP10502Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP10502 newTask)
        {
            var result = _SOP10502Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP10502> newTask)
        {
            var result = _SOP10502Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP10502 newTask)
        {
            var result = _SOP10502Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP10502> newTask)
        {
            var result = _SOP10502Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
