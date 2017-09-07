using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin.Repository;
using Kaizen.Admin;

namespace Kaizen.BusinessLogic.Admin
{
    public class Sys00001Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.Sys00001Repository _Sys00001Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Sys00001Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Sys00001Repository = new Sys00001Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<Sys00001> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Sys00001> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Sys00001Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Sys00001Repository.GetWhereListWithPaging("Sys00001", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Sys00001> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CompanyAddressCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CompanyAddressName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AddressName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CountryID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Pnone01", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Pnone02", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Pnone03", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Pnone04", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MobileNo1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MobileNo2", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MobileNo3", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MobileNo4", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("POBox", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Other01", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Other02", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Other03", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Other04", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Address1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Address2", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Address3", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Address4", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Email01", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Email02", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Email03", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Email04", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            Sys00001Repository rep = new Sys00001Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<Sys00001> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("Sys00001", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Sys00001> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Sys00001> L = null;
            var tasks = _Sys00001Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<Sys00001> result = tasks;
            return result;
        }
        public DataCollection<Sys00001> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Sys00001> result = null;
            var tasks = _Sys00001Repository.GetListWithPaging(PageSize, Page, ss => new { ss.CompanyAddressCode });
            result = tasks;
            return result;
        }

        public IList<Sys00001> GetAll()
        {
            var tasks = _Sys00001Repository.GetAll();
            IList<Sys00001> result = tasks;
            return result;
        }

        public Sys00001 GetSingle(string CompanyAddressCode)
        {
            var tasks = _Sys00001Repository.GetSingle(x => x.CompanyAddressCode == CompanyAddressCode);
            return tasks;
        }
  
        public KaizenResult AddSys00001(Sys00001 newTask)
        {
            var result=_Sys00001Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(Sys00001 newTask)
        {
            var result = _Sys00001Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(Sys00001 newTask)
        {
            var result = _Sys00001Repository.DeleteKaizenObject(newTask);
            return result;
        }
    }
}
