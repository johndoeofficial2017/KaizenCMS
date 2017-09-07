using System.Collections.Generic;
using System.Linq;
using Kaizen.Configuration.Repository;
using Kaizen.Configuration;

namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen002Services
    {
        #region Infrastructure

        private Kaizen002Repository _Kaizen002RepositoryDataRepository;
        private KaizenSession UserContext;
        public Kaizen002Services(KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Kaizen002RepositoryDataRepository = new Kaizen002Repository(UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<Kaizen002> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TaskTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TaskTitle", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            Kaizen002Repository rep = new Kaizen002Repository(UserContext.UserName, UserContext.Password);
            DataCollection<Kaizen002> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("Kaizen002", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen002> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Kaizen002> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen002RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Kaizen002RepositoryDataRepository.GetWhereListWithPaging("Kaizen002", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<Kaizen002> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _Kaizen002RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<Kaizen002> result = tasks;
                return result;
            }
            else
            {
                var tasks = _Kaizen002RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<Kaizen002> result = tasks;
                return result;
            }

        }
        public DataCollection<Kaizen002> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Kaizen002> result = null;
            var tasks = _Kaizen002RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.TaskID });
            result = tasks;
            return result;
        }
        public IList<Kaizen002> GetAll()
        {
            var tasks = _Kaizen002RepositoryDataRepository.GetAll();
            IList<Kaizen002> result = tasks;
            return result;
        }
        public IList<Kaizen002> GetAllByModuleID(int ModuleID)
        {
            var tasks = _Kaizen002RepositoryDataRepository.GetAll(xx=>xx.ModuleID==ModuleID);
            IList<Kaizen002> result = tasks;
            return result;
        }

        public Kaizen002 GetSingle(int TaskID, int ModuleID, int MenueTypeID)
        {
            var tasks = _Kaizen002RepositoryDataRepository.GetSingle(x => x.TaskID == TaskID && x.ModuleID == ModuleID && x.MenueTypeID == MenueTypeID);
            return tasks;
        }

        public KaizenResult AddKaizen002(Kaizen002 newTask)
        {
            KaizenResult result = _Kaizen002RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizen002(IList<Kaizen002> newTask)
        {
            KaizenResult result = _Kaizen002RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Kaizen002 newTask)
        {
            KaizenResult result = _Kaizen002RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Kaizen002> newTask)
        {
            KaizenResult result = _Kaizen002RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(IList<Kaizen002> newTask)
        {
            KaizenResult result = _Kaizen002RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
    }
}
