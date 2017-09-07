using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.Configuration;
using Kaizen.Configuration.Repository;

namespace Kaizen.BusinessLogic.Admin
{
    public class DT00100Services
    {
        #region Infrastructure

        private Kaizen.Configuration.Repository.DT00100Repository _DT00100Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public DT00100Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _DT00100Repository = new DT00100Repository(UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<DT00100> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<DT00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _DT00100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _DT00100Repository.GetWhereListWithPaging("DT00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<DT00100> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ActionTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AccountID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<DT00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _DT00100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _DT00100Repository.GetWhereListWithPaging("DT00100", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }

        public DataCollection<DT00100> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<DT00100> L = null;
            var tasks = _DT00100Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<DT00100> result = tasks;
            return result;
        }
        public DataCollection<DT00100> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<DT00100> result = null;
            var tasks = _DT00100Repository.GetListWithPaging(PageSize, Page, ss => new { ss.SourceID });
            result = tasks;
            return result;
        }
        public IList<DT00100> GetAll()
        {
            var tasks = _DT00100Repository.GetAll();
            IList<DT00100> result = tasks;
            return result;
        }
        public DT00100 GetSingle(int SourceID)
        {
            var tasks = _DT00100Repository.GetSingle(x => x.SourceID == SourceID);
            return tasks;
        }
        public KaizenResult AddDT00100(DT00100 newTask)
        {
            var result = _DT00100Repository.AddKaizenObject(newTask);
            return result;
        }

        public KaizenResult AddDT00100(IList<DT00100> newTask)
        {
            var result = _DT00100Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(DT00100 newTask)
        {
            var result = _DT00100Repository.UpdateKaizenObject(newTask);
            return result;
        }

        public KaizenResult Update(IList<DT00100> newTask)
        {
            var result = _DT00100Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(DT00100 newTask)
        {
            var result = _DT00100Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<DT00100> newTask)
        {
            var result = _DT00100Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        public IList<DT00001> GetViewTypes()
        {
            DT00001Repository rep = new DT00001Repository(UserContext.UserName, UserContext.Password);
            var viewTypes = rep.GetAll();
            IList<DT00001> result = viewTypes;
            return result;
        }
        public IList<Kaizen001> GetMenueTypes()
        {
            Kaizen001Repository rep = new Kaizen001Repository(UserContext.UserName, UserContext.Password);
            var menueTypes = rep.GetAll();
            IList<Kaizen001> result = menueTypes;
            return result;
        }

        #region Source Users
        public KaizenResult SaveSourceUser(DT00103 DT00103)
        {
            DT00103Repository rep = new DT00103Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(DT00103);
            return result;
        }
        public KaizenResult UpdateSourceUser(DT00103 DT00103)
        {
            DT00103Repository rep = new DT00103Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(DT00103);
            return result;
        }
        public KaizenResult DeleteSourceUser(DT00103 DT00103)
        {
            DT00103Repository rep = new DT00103Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(DT00103);
            return result;
        }
        public IList<DT00103> GetSourceUsers(int SourceID)
        {
            DT00103Repository rep = new DT00103Repository(UserContext.UserName, UserContext.Password);
            var sourceUsers = rep.GetAll().Where(x => x.SourceID == SourceID).ToList();
            IList<DT00103> result = sourceUsers;
            return result;
        }
        #endregion

        #region Source Role Security
        public KaizenResult SaveSourceRole(DT00102 DT00102)
        {
            DT00102Repository rep = new DT00102Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(DT00102);
            return result;
        }
        public KaizenResult DeleteSourceRole(DT00102 DT00102)
        {
            DT00102Repository rep = new DT00102Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(DT00102);
            return result;
        }
        public IList<DT00102> GetSourceRoles(int SourceID)
        {
            DT00102Repository rep = new DT00102Repository(UserContext.UserName, UserContext.Password);
            var sourceUsers = rep.GetAll().Where(x => x.SourceID == SourceID).ToList();
            IList<DT00102> result = sourceUsers;
            return result;
        }
        #endregion

        #region source Field
        public IList<Kaizen00006> GetSourceFields()
        {
            Kaizen00006Repository rep = new Kaizen00006Repository(UserContext.UserName, UserContext.Password);
            var sourceFields = rep.GetAll();
            IList<Kaizen00006> result = sourceFields;
            return result;
        }
        public IList<DT00101> GetDT00101(int SourceID)
        {
            DT00101Repository rep = new DT00101Repository(UserContext.UserName, UserContext.Password);
            var sourceFields = rep.GetAll().Where(x=>x.SourceID==SourceID).ToList();
            IList<DT00101> result = sourceFields;
            return result;
        }
        public KaizenResult SaveSourceField(DT00101 sourceField)
        {
            DT00101Repository rep = new DT00101Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(sourceField);
            return result;
        }

        public KaizenResult UpdateSourceField(DT00101 sourceField)
        {
            DT00101Repository rep = new DT00101Repository(this.UserContext.UserName, this.UserContext.Password);
            KaizenResult result = rep.UpdateKaizenObject(sourceField);
            return result;
        }

        public KaizenResult DeleteSourceField(DT00101 sourceField)
        {
            DT00101Repository rep = new DT00101Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(sourceField);
            return result;
        }
        #endregion
    }
}
