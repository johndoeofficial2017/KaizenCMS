using System;
using System.Collections.Generic;
using Kaizen.GL.Repository;
using Kaizen.GL;
using System.Linq;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00102Services
    {
        #region Infrastructure

        private GL00102Repository _GL00102Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00102Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00102Repository = new GL00102Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00102> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00102> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00102Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00102Repository.GetWhereListWithPaging("GL00102", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00102> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ISOCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Unit", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SubUnit", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }
            DataCollection<GL00102> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00102Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00102Repository.GetWhereListWithPaging("GL00102", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<GL00102> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<GL00102> L = null;
            var tasks = _GL00102Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<GL00102> result = tasks;
            return result;
        }
        public DataCollection<GL00102> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<GL00102> result = null;
            var tasks = _GL00102Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }
        public DataCollection<GL00102> GetAllGL00102(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<GL00102> L = null;
            var tasks = _GL00102Repository.GetListWithPaging(x => (x.CurrencyName.Contains(SearchTerm) || x.CurrencyCode.Contains(SearchTerm)
                ), PageSize, Page, ss => ss.CurrencyCode, null);

            DataCollection<GL00102> result = tasks;
            return result;
        }
        public IList<GL00102> GetAll()
        {
            var tasks = _GL00102Repository.GetAll();
            IList<GL00102> result = tasks;
            return result;
        }
        public GL00100 GetRealizedGainGL(string CurrencyCode)
        {
            GL00104Repository rep = new GL00104Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            GL00104 tasks = rep.GetSingle(x => x.CurrencyCode == CurrencyCode);
            GL00100 result = null;
            if (tasks.RealizedGain.HasValue)
            {
                GL00100Services srv = new GL00100Services(UserContext);
                result = srv.GetSingle(tasks.RealizedGain.Value);
            }
            return result;
        }
        public GL00100 GetRealizedLossGL(string CurrencyCode)
        {
            GL00104Repository rep = new GL00104Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            GL00104 tasks = rep.GetSingle(x => x.CurrencyCode == CurrencyCode);
            GL00100 result = null;
            if (tasks.RealizedLoss.HasValue)
            {
                GL00100Services srv = new GL00100Services(UserContext);
                result = srv.GetSingle(tasks.RealizedLoss.Value);
            }
            return result;
        }
        public GL00102 GetSingle(string CurrencyCode)
        {
            var tasks = _GL00102Repository.GetSingle(x => x.CurrencyCode.Trim() == CurrencyCode.Trim());
            return tasks;
        }
        public GL00102 GetSingle(string CurrencyCode, string CompanyID)
        {
            var tasks = _GL00102Repository.GetSingle(x => x.CurrencyCode.Trim() == CurrencyCode.Trim());
            return tasks;
        }

        public KaizenResult AddGL00102(GL00102 newTask)
        {
            var result = _GL00102Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00102(IList<GL00102> newTask)
        {
            var result = _GL00102Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(GL00102 newTask)
        {
            var result = _GL00102Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00102> newTask)
        {
            var result = _GL00102Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00102 newTask)
        {
            var result = _GL00102Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00102> newTask)
        {
            var result = _GL00102Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
