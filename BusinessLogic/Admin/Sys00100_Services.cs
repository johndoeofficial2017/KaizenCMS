using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin.Repository;
using Kaizen.Admin;
using System.Data.Entity;

namespace Kaizen.BusinessLogic.Admin
{
    public class Sys00100Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.Sys00100Repository _Sys00100RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Sys00100Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Sys00100RepositoryDataRepository = new Sys00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<Sys00100> GetTaskList()
        {
            DataCollection<Sys00100> result = null;
            var tasks = _Sys00100RepositoryDataRepository.GetListWithPaging(10, 1, ss => new { ss.TaskStartDate });
            result = tasks;
            return result;
        }
        public DataCollection<Sys00100> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TaskID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TaskTitle", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            Sys00100Repository rep = new Sys00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<Sys00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("Sys00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Sys00100> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Sys00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Sys00100RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Sys00100RepositoryDataRepository.GetWhereListWithPaging("Sys00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<Sys00100> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _Sys00100RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<Sys00100> result = tasks;
                return result;
            }
            else
            {
                var tasks = _Sys00100RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<Sys00100> result = tasks;
                return result;
            }

        }
        public DataCollection<Sys00100> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Sys00100> result = null;
            var tasks = _Sys00100RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.TaskID });
            result = tasks;
            return result;
        }
        public IList<Sys00100> GetAll()
        {
            var tasks = _Sys00100RepositoryDataRepository.GetAll();
            IList<Sys00100> result = tasks;
            return result;
        }
        public IList<Sys00100> GetTasksAssignedFromUser(string UserAsginFrom)
        {
            var tasks = _Sys00100RepositoryDataRepository.GetAll(xx => xx.UserAsginFrom == UserAsginFrom);
            IList<Sys00100> result = tasks;
            return result;
        }
        public IList<Sys00100> GetTasksAssignedToUser(string UserAsginTO)
        {
            var tasks = _Sys00100RepositoryDataRepository.GetAll(xx => xx.UserAsginTO == UserAsginTO);
            IList<Sys00100> result = tasks;
            return result;
        }

        public IList<Sys00100> GetTodayTasksAssignedToUser(string UserAsginTO)
        {
            var tasks = _Sys00100RepositoryDataRepository.GetAll(xx => xx.UserAsginTO.Trim() == UserAsginTO.Trim());
            IList<Sys00100> result = tasks;
            return result;
        }
        public Sys00100 GetSingle(int TaskID)
        {
            var tasks = _Sys00100RepositoryDataRepository.GetSingle(x => x.TaskID == TaskID);
            return tasks;
        }

        public KaizenResult AddSys00100(Sys00100 newTask)
        { 
            KaizenResult result = _Sys00100RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSys00100(IList<Sys00100> newTask)
        {
            KaizenResult result = _Sys00100RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Sys00100 newTask)
        {
            KaizenResult result = _Sys00100RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Sys00100> newTask)
        {
            KaizenResult result = _Sys00100RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(int TaskID)
        {
            KaizenResult result = _Sys00100RepositoryDataRepository.RemoveKaizenObject(xx => xx.TaskID == TaskID);
            return result;
        }
        public KaizenResult Remove(IList<Sys00100> newTask)
        {
            KaizenResult result = _Sys00100RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
