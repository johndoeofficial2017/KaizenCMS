using System;
using System.Collections.Generic;
using System.Linq;

using Kaizen.Configuration.Repository;
using Kaizen.Configuration;

namespace Kaizen.BusinessLogic.Configuration
{
    public class CompanySegmentServices
    {
        #region Infrastructure

        private CompanySegmentRepository _CompanySegmentRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CompanySegmentServices(Kaizen.Configuration.KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
            }
            UserContext = _UserContext;
            _CompanySegmentRepository = new CompanySegmentRepository(UserContext.UserName, UserContext.Password);
        }
        #endregion
        public DataCollection<CompanySegment> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (!string.IsNullOrEmpty(Searchcritaria))
            {
                SeachStr += "";
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            return this.GetAllViewBYSQLQuery(SeachStr, PageSize, Page, Member, IsAscending);
        }
        public DataCollection<CompanySegment> GetAllViewBYSQLQuery(string Filters,int PageSize, int Page, string Member, bool IsAscending)
        {
            DataCollection<CompanySegment> result = null;
            if (string.IsNullOrEmpty(Filters))
                result = _CompanySegmentRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CompanySegmentRepository.GetWhereListWithPaging("CompanySegment", PageSize, Page, Filters, Member, IsAscending);
            return result;
        }
        public DataCollection<CompanySegment> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("SegmentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SegmentName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CompanyID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SegmentLength", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CompanySegment> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CompanySegmentRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CompanySegmentRepository.GetWhereListWithPaging("CompanySegment", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CompanySegment> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string CompanyID,
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
                    SeachStr += Help.GetFilter("SegmentID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SegmentName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CompanyID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SegmentLength", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CompanySegment> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CompanySegmentRepository.GetListWithPaging(ss => ss.CompanyID.Trim() == CompanyID.Trim(), PageSize, Page, ss => Member);
            else
                result = _CompanySegmentRepository.GetWhereListWithPaging("CompanySegment", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CompanySegment> GetAllBYSQLQuery(string Filters, string Searchcritaria, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CompanySegment> result = null;
            if (!string.IsNullOrEmpty(Filters))
            {
                Page = Page - 1;
                if (!string.IsNullOrEmpty(Searchcritaria))
                {
                    Filters += " and SegmentID like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and SegmentName like '%" + Searchcritaria.Trim() + "%'";
                }
                string sql = "select  * from CompanySegment ";
                sql += " where " + Filters + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
                var tasks = _CompanySegmentRepository.GetSQLData(sql);
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
                var tasks = _CompanySegmentRepository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            else
            {
                var tasks = _CompanySegmentRepository.GetListWithPaging(xx => xx.SegmentID.ToString().Contains(Searchcritaria)
                    || xx.SegmentName.Contains(Searchcritaria) , PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            return result;
        }

        public List<CompanySegment> GetAll()
        {
            var tasks = _CompanySegmentRepository.GetAll();
            List<CompanySegment> result = tasks.ToList();
            return result;
        }
        public CompanySegment GetSingle(int SegmentID)
        {
            var tasks = _CompanySegmentRepository.GetSingle(x => x.SegmentID == SegmentID);
            return tasks;
        }

        public IList<CompanySegment> GetByCompanyID(string CompanyID)
        {
            var tasks = _CompanySegmentRepository.GetAll(x => x.CompanyID.Trim() == CompanyID.Trim());
            return tasks;
        }

        public KaizenResult AddCompanySegment(CompanySegment newTask)
        {
            var result = _CompanySegmentRepository.AddKaizenObject(newTask);
            if(result.Status)
            {
                CompanyacessServices com = new CompanyacessServices(this.UserContext);
                com.UpdateGLFormat(newTask.CompanyID);
            }
           
            return result;
        }
        public KaizenResult AddCompanySegment(IList<CompanySegment> newTask)
        {
            var result = _CompanySegmentRepository.AddKaizenObject(newTask.ToArray());
            if (result.Status)
            {
                CompanyacessServices com = new CompanyacessServices(this.UserContext);
                com.UpdateGLFormat(newTask[0].CompanyID);
            }
            return result;
        }
        public KaizenResult Update(CompanySegment newTask)
        {
            var result = _CompanySegmentRepository.UpdateKaizenObject(newTask);
            if (result.Status)
            {
                CompanyacessServices com = new CompanyacessServices(this.UserContext);
                com.UpdateGLFormat(newTask.CompanyID);
            }
            return result;
        }
        public KaizenResult Update(IList<CompanySegment> newTask)
        {
            var result = _CompanySegmentRepository.UpdateKaizenObject(newTask.ToArray());
            if (result.Status)
            {
                CompanyacessServices com = new CompanyacessServices(this.UserContext);
                com.UpdateGLFormat(newTask[0].CompanyID);
            }
            
            return result;
        }

        public KaizenResult Delete(CompanySegment newTask)
        {
            KaizenResult result = new KaizenResult();
            result = _CompanySegmentRepository.DeleteKaizenObject(newTask);
            if (result.Status)
            {
                CompanyacessServices com = new CompanyacessServices(this.UserContext);
                com.UpdateGLFormat(newTask.CompanyID);
            }
            return result;
        }
        public KaizenResult Delete(IList<CompanySegment> newTask)
        {
            var result = _CompanySegmentRepository.DeleteKaizenObject(newTask.ToArray());
            if (result.Status)
            {
                CompanyacessServices com = new CompanyacessServices(this.UserContext);
                com.UpdateGLFormat(newTask[0].CompanyID);
            }
            return result;
        }
        //public int ExecuteSqlCommand(string myQuery)
        //{
        //    var result = _CompanySegmentRepository.ExecuteSqlCommand(myQuery);
        //    return result;
        //}

    }
}
