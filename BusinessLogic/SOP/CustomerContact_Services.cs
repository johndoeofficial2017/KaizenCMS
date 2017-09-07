using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;

namespace Kaizen.BusinessLogic.SOP
{
    public class SOP00105Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00105Repository _SOP00105Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00105Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00105Repository = new SOP00105Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00105> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ContactTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContactPerson", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PersonPosition", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PersonEmailAdd", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OtherInfo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00105> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00105Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00105Repository.GetWhereListWithPaging("SOP00105", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<SOP00105> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP00105> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00105Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP00105Repository.GetWhereListWithPaging("SOP00105", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<SOP00105> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string CUSTNMBR,
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
                    SeachStr += Help.GetFilter("CUSTNMBR", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContactTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContactPerson", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PersonPosition", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PersonEmailAdd", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OtherInfo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP00105> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP00105Repository.GetListWithPaging(ss => ss.CUSTNMBR.Trim() == CUSTNMBR.Trim(), PageSize, Page, ss => Member);
            else
                result = _SOP00105Repository.GetWhereListWithPaging("SOP00105", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<SOP00105> GetByCUSTNMBR(string CUSTNMBR, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00105> L = null;
            var tasks = _SOP00105Repository.GetListWithPaging(x => x.CUSTNMBR.Trim() == CUSTNMBR.Trim(), PageSize, Page, null);
            DataCollection<SOP00105> result = tasks;
            return result;
        }

        public IList<SOP00105> GetAll()
        {
            var tasks = _SOP00105Repository.GetAll();
            IList<SOP00105> result = tasks;
            return result;
        }
        public IList<Sys00006> GetAllSys00006()
        {
            Sys00006Repository rep = new Sys00006Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll();
            IList<Sys00006> result = tasks;
            return result;
        }

        public IList<SOP00105> GetAllByCUSTNMBR(string CUSTNMBR)
        {
            var tasks = _SOP00105Repository.GetAll(ss => ss.CUSTNMBR.Trim() == CUSTNMBR.Trim());
            IList<SOP00105> result = tasks;
            return result;
        }
        public SOP00105 GetSingle(string CUSTNMBR, int ContactTypeID)
        {
            var tasks = _SOP00105Repository.GetSingle(x => x.CUSTNMBR == CUSTNMBR, x => x.ContactTypeID == ContactTypeID);
            return tasks;
        }

        public KaizenResult AddSOP00105(SOP00105 newTask)
        {
            var result = _SOP00105Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSOP00105(IList<SOP00105> newTask)
        {
            var result = _SOP00105Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(SOP00105 newTask)
        {
            var result = _SOP00105Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP00105> newTask)
        {
            var result = _SOP00105Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(SOP00105 newTask)
        {
            var result = _SOP00105Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP00105> newTask)
        {
            var result = _SOP00105Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
