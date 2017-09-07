using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;


namespace Kaizen.BusinessLogic.CMS
{
    public class CM00200Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00200Repository _CM00200Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00200Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00200Repository = new CM00200Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00200> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00200> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00200Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00200Repository.GetWhereListWithPaging("CM00200", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00200> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ContractID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClientID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContractName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContractStatusID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PaymentBaseID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BillingFrequencyID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Abbreviation", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("StartDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EndDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsPrivateCase", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Remarks", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00200Repository rep = new CM00200Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00200> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("CM00200", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00200> GetAllViewBYSQLQuery(string Filters, string ClientID,
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
                    SeachStr += Help.GetFilter("ContractID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContractName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContractStatusID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PaymentBaseID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BillingFrequencyID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Abbreviation", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("StartDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EndDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsPrivateCase", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Remarks", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            CM00200Repository rep = new CM00200Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00200> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetListWithPaging(xx => xx.ClientID == ClientID.Trim(), PageSize, Page, ss => Member);
            else
            {
                SeachStr += " and ClientID='" + ClientID.Trim() + "'";
                result = rep.GetWhereListWithPaging("CM00200", PageSize, Page, SeachStr, Member, IsAscending);
            }
            return result;
        }

        public DataCollection<CM00200> GetAllBYSQLQuery(string Filters, string Searchcritaria, int PageSize, int Page, string Member, string SortDirection)
        {
            Kaizen.CMS.Repository.CM00200Repository _CM00200Repository = new CM00200Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00200> result = null;
            if (!string.IsNullOrEmpty(Filters))
            {
                Page = Page - 1;
                if (!string.IsNullOrEmpty(Searchcritaria))
                {
                    Filters += " and ContractID like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and ContractName like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and Abbreviation like '%" + Searchcritaria.Trim() + "%'";
                }
                string sql = "select  * from CM00200";
                sql += " where " + Filters + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
                var tasks = _CM00200Repository.GetSQLData(sql);
                if (tasks != null)
                {
                    tasks.TotalItemCount = tasks.Items.Count;
                    tasks.TotalPageCount = (int)Math.Ceiling((double)tasks.TotalItemCount / (double)PageSize);
                }
                result = tasks;
                return result;
            }
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                var tasks = _CM00200Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            else
            {
                var tasks = _CM00200Repository.GetListWithPaging(xx => xx.ContractName.Contains(Searchcritaria)
                    || xx.Abbreviation.Contains(Searchcritaria) || xx.ContractID.Contains(Searchcritaria)
                    , PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            return result;
        }

        public DataCollection<CM00200> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<CM00200> L = null;
            var tasks = _CM00200Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<CM00200> result = tasks;
            return result;
        }
        public DataCollection<CM00200> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<CM00200> result = null;
            var tasks = _CM00200Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<CM00200> GetAll()
        {
            try
            {
                IList<CM00200> L = null;
                try
                {
                    var tasks = _CM00200Repository.GetAll();
                    IList<CM00200> result = tasks;
                    return result;
                }
                catch (Exception ex)
                {
                }
                return null;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- GenderID: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return null;
        }
        public IList<CM00200> GetAllByClientID(string ClientID)
        {
            var tasks = _CM00200Repository.GetAll(xx => xx.ClientID == ClientID, xx => xx.ContractID);
            IList<CM00200> result = tasks;
            return result;
        }
        public DataCollection<CM00200> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member
             , string SortDirection)
        {
            DataCollection<CM00200> result = null;

            var tasks = _CM00200Repository.GetListWithPaging(SearchTerm,
                PageSize, Page, ss => ss.ContractID, true);
            result = tasks;
            return result;
        }
        public IList<CM00018> GetAllPaymentBase()
        {
            CM00018Repository rep = new CM00018Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll();
            IList<CM00018> result = tasks;
            return result;
        }
        public IList<CM00013> GetAllBillingFrequency()
        {
            CM00013Repository rep = new CM00013Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll();
            IList<CM00013> result = tasks;
            return result;
        }
        public CM00200 GetSingle(string ContractID)
        {
            var tasks = _CM00200Repository.GetSingle(x => x.ContractID.Trim() == ContractID.Trim());
            return tasks;
        }

        public KaizenResult AddCM00200(CM00200 newTask)
        {
            KaizenResult result = _CM00200Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00200(IList<CM00200> newTask)
        {
            KaizenResult result = _CM00200Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00200 newTask)
        {
            KaizenResult result = _CM00200Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00200> newTask)
        {
            KaizenResult result = _CM00200Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00200 newTask)
        {
            KaizenResult result = _CM00200Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00200> newTask)
        {
            KaizenResult result = _CM00200Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
