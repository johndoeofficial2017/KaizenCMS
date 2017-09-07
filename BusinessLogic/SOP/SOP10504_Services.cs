using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP10504Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10504Repository _SOP10504Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10504Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10504Repository = new SOP10504Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<SOP10504> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP10504> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10504Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10504Repository.GetWhereListWithPaging("SOP10504", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP10504> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("IsBinGroup", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AppliedQuantity", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP10504> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP10504Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP10504Repository.GetWhereListWithPaging("SOP10504", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public IList<SOP10504> GetListByLineID(int LineID)
        {
            IList<SOP10504> result = null;
            //var tasks = _SOP10504Repository.GetAll(ss => ss.LineID == LineID);
            //result = tasks;
            return result;
        }

        public IList<SOP10504> GetAll()
        {
            var tasks = _SOP10504Repository.GetAll();
            IList<SOP10504> result = tasks;
            return result;
        }
        public SOP10504 GetSingle(int LineID,string BinID)
        {
            //var tasks = _SOP10504Repository.GetSingle(x => x.LineID == LineID && x.BinID == BinID);
            //return tasks;
            return null;
        }

        public KaizenResult AddSOP10504(SOP10504 newTask)
        {
            var result = _SOP10504Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP10504(IList<SOP10504> newTask)
        {
            var result = _SOP10504Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP10504 newTask)
        {
            var result = _SOP10504Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP10504> newTask)
        {
            var result = _SOP10504Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP10504 newTask)
        {
            var result = _SOP10504Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP10504> newTask)
        {
            var result = _SOP10504Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
