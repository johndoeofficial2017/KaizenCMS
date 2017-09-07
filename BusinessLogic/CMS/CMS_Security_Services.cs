using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CMSSecurityServices
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00029Repository _CM00029Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CMSSecurityServices(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00029Repository = new CM00029Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public IList<CM00107> GetAllCM00107()
        {
            CM00107Repository rep = new CM00107Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var tasks = rep.GetAll();
            return tasks;
        }

        public DataCollection<CM00029> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00029> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00029Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00029Repository.GetWhereListWithPaging("CM00029", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00029> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TRXTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TrxTypeName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00029> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00029Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00029Repository.GetWhereListWithPaging("CM00029", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }


        public DataCollection<CM00029> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM00029Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00029> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM00029Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00029> result = tasks;
                return result;
            }

        }
        public DataCollection<CM00029> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00029> result = null;
            var tasks = _CM00029Repository.GetListWithPaging(PageSize, Page, ss => new { ss.TRXTypeID });
            result = tasks;
            return result;
        }
        //public IList<CM00029> GetAllMe()
        //{
        //    var tasks = _CM00029Repository.GetAll(xx => xx.CM00118.Any(cc => cc.UserName ==UserContext.UserName.Trim()));
        //    IList<CM00029> result = tasks;
        //    return result;
        //}

        public IList<CM00029> GetAll()
        {
            try
            {
                IList<CM00029> L = null;
                try
                {
                    var tasks = _CM00029Repository.GetAll();
                    IList<CM00029> result = tasks;
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
        public List<CM00039> GetAllByTRXTypeID(int TRXTypeID)
        {
            CM00039Repository rep = new CM00039Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var tasks = rep.GetAll(ss => ss.TRXTypeID == TRXTypeID);
            IList<CM00039> result = tasks;
            foreach (CM00039 o in result)
            {
                o.FieldName = o.FieldName.Trim();
            }
            return result.OrderBy(x => x.FieldOrder).ToList(); ;
        }
        public CM00029 GetSingle(int TRXTypeID)
        {
            var tasks = _CM00029Repository.GetSingle(x => x.TRXTypeID == TRXTypeID);
            return tasks;
        }

        #region Debtor Role
        public IList<CM00119> GetRolesByDebtor(string DebtorID)
        {
            CM00119Repository rep = new CM00119Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var actionTyperoles = rep.GetAll(xx => xx.DebtorID.Trim().Equals(DebtorID.Trim()));
            IList<CM00119> result = actionTyperoles;
            return result;
        }

        public KaizenResult AddCM00119(CM00119 newTask)
        {
            CM00119Repository rep = new CM00119Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult DeleteCM00119(CM00119 newTask)
        {
            CM00119Repository rep = new CM00119Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(newTask);
            return result;
        }
        #endregion

        #region Debtor User
        public IList<CM00118> GetDebtorByUser(string userName)
        {
            CM00118Repository rep = new CM00118Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(xx => xx.UserName == userName);
            IList<CM00118> result = tasks;
            return result;
        }

        public KaizenResult DeleteDebtorByUser(CM00118 caseTypeUser)
        {
            CM00118Repository rep = new CM00118Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var result = rep.DeleteKaizenObject(caseTypeUser);
            return result;
        }

        public KaizenResult AddCM00118(CM00118 caseTypeUser)
        {
            CM00118Repository rep = new CM00118Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(caseTypeUser);
            return result;
        }
        #endregion

    }
}
