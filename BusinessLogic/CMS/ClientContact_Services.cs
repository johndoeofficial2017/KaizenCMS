using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00115Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00115Repository _CM00115Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00115Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00115Repository = new CM00115Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00115> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("Pnone01", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Pnone02", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Ext1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Ext2", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MobileNo1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MobileNo2", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OtherInfo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00115> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00115Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00115Repository.GetWhereListWithPaging("CM00115", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }
        public DataCollection<CM00115> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00115> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00115Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00115Repository.GetWhereListWithPaging("CM00115", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00115> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string ClientID,
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
                    SeachStr += Help.GetFilter("ClientID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContactTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContactPerson", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PersonPosition", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PersonEmailAdd", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Pnone01", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Pnone02", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Ext1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Ext2", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MobileNo1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MobileNo2", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OtherInfo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00115> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00115Repository.GetListWithPaging(ss => ss.ClientID.Trim() == ClientID.Trim(), PageSize, Page, ss => Member);
            else
                result = _CM00115Repository.GetWhereListWithPaging("CM00115", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00115> GetByClientID(string ClientID, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00115> L = null;
            var tasks = _CM00115Repository.GetListWithPaging(x => x.ClientID.Trim() == ClientID.Trim(), PageSize, Page, null);
            DataCollection<CM00115> result = tasks;
            return result;
        }

        public IList<CM00115> GetAll()
        {
            var tasks = _CM00115Repository.GetAll();
            IList<CM00115> result = tasks;
            return result;
        }
        public IList<CM00115> GetAllByClientID(string ClientID)
        {
            var tasks = _CM00115Repository.GetAll(ss => ss.ClientID.Trim() == ClientID.Trim());
            IList<CM00115> result = tasks;
            return result;
        }
        public CM00115 GetSingle(int ContactTypeID, string ClientID)
        {
            var tasks = _CM00115Repository.GetSingle(x => x.ContactTypeID == ContactTypeID && x.ClientID == ClientID);
            return tasks;
        }


        public KaizenResult AddCM00115(CM00115 newTask)
        {
            var result = _CM00115Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00115(IList<CM00115> newTask)
        {
            var result = _CM00115Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00115 newTask)
        {
            var result = _CM00115Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00115> newTask)
        {
            var result = _CM00115Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00115 newTask)
        {
            var result = _CM00115Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00115> newTask)
        {
            var result = _CM00115Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
