using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP10505Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10505Repository _SOP10505Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10505Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10505Repository = new SOP10505Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<SOP10505> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP10505> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10505Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10505Repository.GetWhereListWithPaging("SOP10505", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP10505> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("LineID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BinID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SubBinID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ItemQuantity", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP10505> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10505Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10505Repository.GetWhereListWithPaging("SOP10505", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public IList<SOP10505> GetListByLineID(int LineID)
        {
            IList<SOP10505> result = null;
            //var tasks = _SOP10505Repository.GetAll(ss => ss.LineID == LineID);
            //result = tasks;
            return result;
        }

        public IList<SOP10505> GetAll()
        {
            var tasks = _SOP10505Repository.GetAll();
            IList<SOP10505> result = tasks;
            return result;
        }
        public SOP10505 GetSingle(int LineID,string BinID,string SubBinID)
        {
            //var tasks = _SOP10505Repository.GetSingle(x => x.LineID == LineID && x.BinID == BinID && x.SubBinID == SubBinID);
            //return tasks;
            return null;
        }

        public KaizenResult AddSOP10505(SOP10505 newTask)
        {
            var result = _SOP10505Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP10505(IList<SOP10505> newTask)
        {
            var result = _SOP10505Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP10505 newTask)
        {
            var result = _SOP10505Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP10505> newTask)
        {
            var result = _SOP10505Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP10505 newTask)
        {
            var result = _SOP10505Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP10505> newTask)
        {
            var result = _SOP10505Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
