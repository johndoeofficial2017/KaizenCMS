using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00215Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00215Repository _CM00215Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00215Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00215Repository = new CM00215Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00215> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00215> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00215Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00215Repository.GetWhereListWithPaging("CM00215", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00215> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("BucketID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00215> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00215Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00215Repository.GetWhereListWithPaging("CM00215", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }


        public DataCollection<CM00215> GetAllCM00215(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                IList<CM00215> L = null;
                var tasks = _CM00215Repository.GetListWithPaging(x => (x.BucketName.Contains(SearchTerm)
                    ), PageSize, Page, ss => ss.BucketID, null);

                DataCollection<CM00215> result = tasks;
                return result;
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
        public IList<CM00215> GetAll()
        {
            var tasks = _CM00215Repository.GetAll();
            IList<CM00215> result = tasks;
            return result;
        }
        public IList<CM00215> GetCyclePeriodsInfo(int BucketID , string AgentID, string YearCode)
        {
            var tasks = _CM00215Repository.GetAll(ss => ss.BucketID == BucketID && ss.AgentID == AgentID && ss.YearCode == YearCode);
            IList<CM00215> result = tasks;
            return result;
        }
        //public CM00215 GetSingle(string BucketID)
        //{
        //    var tasks = _CM00215Repository.GetSingle(x => x.BucketID == BucketID);
        //    return tasks;
        //}

        public KaizenResult AddCM00215(CM00215 newTask)
        {
            var result = _CM00215Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00215(IList<CM00215> newTask)
        {
            CM00215 first = newTask.FirstOrDefault();
            CM10110Services srv = new CM10110Services(this.UserContext);
            IList<CM10110> ss = srv.GetAll(first.AgentID, first.YearCode);
            if (ss.Count ==0)
            {
                foreach (CM00215 row in newTask)
                {
                    CM10110 o = new CM10110();
                    o.YearCode = row.YearCode;
                    o.AgentID = row.AgentID;
                    o.PERIODID = row.PERIODID;
                    srv.AddCM10110(o);
                }
            }
            KaizenResult result = null;
            switch (first.Status)
            {
                case 0:
                    result = _CM00215Repository.AddKaizenObject(newTask.ToArray());
                    break;
                case 1:
                    result = _CM00215Repository.UpdateKaizenObject(newTask.ToArray());
                    break;
            }
            return result;
        }

        public KaizenResult Update(CM00215 newTask)
        {
            var result = _CM00215Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00215> newTask)
        {
            var result = _CM00215Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00215 newTask)
        {
            var result = _CM00215Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00215> newTask)
        {
            var result = _CM00215Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
