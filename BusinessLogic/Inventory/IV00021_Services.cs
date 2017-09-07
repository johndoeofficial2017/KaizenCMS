using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00021Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00021Repository _IV00021Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00021Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00021Repository = new IV00021Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00021> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00021> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00021Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00021Repository.GetWhereListWithPaging("IV00021", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00021> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("SubBinID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BinID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SubBinName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00021> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00021Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00021Repository.GetWhereListWithPaging("IV00021", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00021> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string BinID,
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
                    SeachStr += Help.GetFilter("SubBinID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BinID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SubBinName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00021> result = null;
            if (!string.IsNullOrEmpty(BinID.ToString()))
                result = _IV00021Repository.GetListWithPaging(ss => ss.BinID == BinID, PageSize, Page, ss => Member);
            else
                result = _IV00021Repository.GetWhereListWithPaging("IV00021", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00021> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<IV00021> L = null;
            var tasks = _IV00021Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<IV00021> result = tasks;
            return result;
        }
        public DataCollection<IV00021> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<IV00021> result = null;
            var tasks = _IV00021Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<IV00021> GetAll()
        {
            var tasks = _IV00021Repository.GetAll();
            IList<IV00021> result = tasks;
            return result;
        }
        public IList<IV00021> GetByBinID(string BinID)
        {
            var tasks = _IV00021Repository.GetAll(ss=>ss.BinID == BinID);
            IList<IV00021> result = tasks;
            return result;
        }

        public IV00021 GetSingle( string  BinID)
        {
            var tasks = _IV00021Repository.GetSingle(x => x.BinID == BinID);
            return tasks;
        }
        public IList<IV00021> GetAllSubBins(string BinID)
        {
            var tasks = _IV00021Repository.GetAll(xx => xx.BinID == BinID);
            IList<IV00021> result = tasks;
            return result;
        }
        public KaizenResult AddIV00021(IV00021 newTask)
        {
            var result = _IV00021Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00021(IList<IV00021> newTask)
        {
            var result = _IV00021Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00021 newTask)
        {
            var result = _IV00021Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00021> newTask)
        {
            var result = _IV00021Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00021 newTask)
        {
            var result = _IV00021Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00021> newTask)
        {
            var result = _IV00021Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}



