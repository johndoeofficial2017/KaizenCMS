using System;
using System.Collections.Generic;
using Kaizen.GL.Repository;
using Kaizen.GL;
using System.Linq;
namespace Kaizen.BusinessLogic.GL
{
    public class GL00002Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00002Repository _GL00002Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00002Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00002Repository = new GL00002Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<GL00002> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<GL00002> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _GL00002Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _GL00002Repository.GetWhereListWithPaging("GL00002", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<GL00002> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("YearCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CurrencyCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BankIBN", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BankCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BankAccount", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ACTNUMBR", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CheckbookName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            GL00002Repository rep = new GL00002Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<GL00002> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("GL00002", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public IList<GL00002> GetAll()
        {
            var tasks = _GL00002Repository.GetAll();
            IList<GL00002> result = tasks;
            return result;
        }
        public DataCollection<GL00002> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<GL00002> L = null;
            var tasks = _GL00002Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<GL00002> result = tasks;
            return result;
        }
        public DataCollection<GL00002> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00002> result = null;

            var tasks = _GL00002Repository.GetListWithPaging(x => x.YearCode.ToString().Contains(SearchTerm) ||
                x.YearName.Contains(SearchTerm)
                , PageSize, Page, ss => new { ss.YearCode });
            result = tasks;
            return result;
        }
        public DataCollection<GL00002> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00002> result = null;
            var tasks = _GL00002Repository.GetListWithPaging(PageSize, Page, ss => new { ss.YearCode });
            result = tasks;
            return result;
        }
        public GL00002 GetSingle(string YearCode)
        {
            var tasks = _GL00002Repository.GetSingle(x => x.YearCode == YearCode, x => x.GL00003);
            return tasks;
        }
        public List<GL00002> GetAllOpenYears()
        {
            IList<GL00002> tasks = _GL00002Repository.GetAll(ss => !ss.IsClosed);
            if (tasks.Count > 0)
            {
                List<GL00002> result = tasks.ToList().OrderBy(ss => ss.YearCode).ToList();
                return result;
            }
            return null;
        }
        public IList<GL00002> GetAllCLosedYears()
        {
            var tasks = _GL00002Repository.GetAll(ss => ss.IsClosed);
            IList<GL00002> result = tasks;
            return result;
        }

        public KaizenResult AddGL00002(GL00002 newTask)
        {
            var result = _GL00002Repository.AddKaizenObject(newTask);
            //if (result.Status)
            //{
            //    Configuration.CompanyacessServices com = new Configuration.CompanyacessServices(this.UserContext);
            //    com.SetUpNextGLID(newTask.YearCode, newTask.NextGL);
            //}
            return result;
        }
        public KaizenResult Update(GL00002 newTask)
        {
            var result = _GL00002Repository.UpdateKaizenObject(newTask);
            if (result.Status)
            {
                GL00003Repository rep = new GL00003Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                rep.UpdateKaizenObject(newTask.GL00003.ToArray());
            }
            return result;
        }
        public KaizenResult Update(IList<GL00002> newTask)
        {
            var result = _GL00002Repository.UpdateKaizenObject(newTask.ToArray());
            if (result.Status)
            {
                GL00003Repository rep = new GL00003Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                foreach (var item in newTask)
                {
                    rep.UpdateKaizenObject(item.GL00003.ToArray());
                }
            }
            return result;
        }

        public KaizenResult Delete(GL00002 newTask)
        {
            KaizenResult returnResult = new KaizenResult();
            returnResult.Status = false;
            var result = _GL00002Repository.ExecuteSqlCommand("delete GL00002 where YearCode in('" + newTask.YearCode.Trim() + "');");
            if(result >0)
            {
                returnResult.Status = true;
                returnResult.Massage = "Data has been Deleted Successfully";
            }
            return returnResult;
        }
        public KaizenResult Delete(IList<GL00002> newTask)
        {
            var result = _GL00002Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
