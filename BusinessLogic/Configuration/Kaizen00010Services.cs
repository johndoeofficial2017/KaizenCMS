using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System.Collections.Generic;
using System.Linq;



namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00010Services
    {
        #region Infrastructure

        private Kaizen00010Repository _Kaizen00010Repository;
        private KaizenSession UserContext;
        public Kaizen00010Services(KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
            }
            UserContext = _UserContext;
            _Kaizen00010Repository = new Kaizen00010Repository(UserContext.UserName, UserContext.Password);
        }
        public Kaizen00010Services()
        {
            _Kaizen00010Repository = new Kaizen00010Repository();
            UserContext = new KaizenSession();
        }
        #endregion
        public DataCollection<Kaizen00010> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Kaizen00010> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen00010Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Kaizen00010Repository.GetWhereListWithPaging("Kaizen00010", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00010> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ScreenID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ScreenName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ScreenDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("HasSubScreen", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<Kaizen00010> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen00010Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Kaizen00010Repository.GetWhereListWithPaging("Kaizen00010", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00010> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<Kaizen00010> L = null;
            var tasks = _Kaizen00010Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<Kaizen00010> result = tasks;
            return result;
        }
        public DataCollection<Kaizen00010> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Kaizen00010> result = null;
            var tasks = _Kaizen00010Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ScreenID });
            result = tasks;
            return result;
        }
        public List<Kaizen00010> GetAllScreens()
        {
            var tasks = _Kaizen00010Repository.GetAll();
            List<Kaizen00010> result = tasks.ToList();
            return result;
        }
        public IList<Kaizen00010> GetAll()
        {
            var tasks = _Kaizen00010Repository.GetAll();
            return tasks;
        }
        public List<Kaizen00010> GetAllScreensByModule(int ModuleID)
        {
            var tasks = _Kaizen00010Repository.GetAll(ss=>ss.ModuleID == ModuleID);
            List<Kaizen00010> result = tasks.ToList();
            return result;
        }

        public Kaizen00010 GetSingle(int ScreenID)
        {
            var tasks = _Kaizen00010Repository.GetSingleFromKaizen(x => x.ScreenID == ScreenID);
            return tasks;
        }
        public List<Kaizen00001> GetFormField(int ScreenID)
        {
            Kaizen00001Repository _Kaizen00001Repository = new Kaizen00001Repository(UserContext.UserName, UserContext.Password);
            var tasks = _Kaizen00001Repository.GetAll(x => x.ScreenID == ScreenID);
            return tasks.ToList();
        }
        public KaizenResult AddKaizen00010(Kaizen00010 newTask)
        {
            var result = _Kaizen00010Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizen00010(IList<Kaizen00010> newTask)
        {
            var result = _Kaizen00010Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Kaizen00010 newTask)
        {
            var result = _Kaizen00010Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Kaizen00010> newTask)
        {
            var result = _Kaizen00010Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Kaizen00010 newTask)
        {
            var result = _Kaizen00010Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Kaizen00010> newTask)
        {
            var result = _Kaizen00010Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
