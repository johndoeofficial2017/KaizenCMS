using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin.Repository;
using Kaizen.Admin;

namespace Kaizen.BusinessLogic.Admin
{
    public class Sys00101Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.Sys00101Repository _Sys00101RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Sys00101Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Sys00101RepositoryDataRepository = new Sys00101Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<Sys00101> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ActionID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TaskTitle", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            Sys00101Repository rep = new Sys00101Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<Sys00101> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = rep.GetWhereListWithPaging("Sys00101", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Sys00101> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Sys00101> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Sys00101RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Sys00101RepositoryDataRepository.GetWhereListWithPaging("Sys00101", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<Sys00101> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _Sys00101RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<Sys00101> result = tasks;
                return result;
            }
            else
            {
                var tasks = _Sys00101RepositoryDataRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<Sys00101> result = tasks;
                return result;
            }

        }
        public DataCollection<Sys00101> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Sys00101> result = null;
            var tasks = _Sys00101RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => new { ss.ActionID });
            result = tasks;
            return result;
        }
        public IList<Sys00101> GetAll()
        {
            var tasks = _Sys00101RepositoryDataRepository.GetAll();
            IList<Sys00101> result = tasks;
            return result;
        }
        public IList<Sys00101> GetByAgentID(string UserName)
        {
            var tasks = _Sys00101RepositoryDataRepository.GetAll(xx => xx.UserName == UserName);
            IList<Sys00101> result = tasks;
            return result;
        }
        public IList<Sys00101> GetByTaskID(int TaskID)
        {
            var tasks = _Sys00101RepositoryDataRepository.GetAll(xx => xx.TaskID == TaskID, ss => ss.Sys00102);
            List<Sys00101> Sys00101List = new List<Sys00101>();
            foreach (var item in tasks)
            {
                List<Sys00102> Sys00102List = new List<Sys00102>();
                foreach (var Sys00102 in item.Sys00102)
                {
                    Sys00102List.Add(new Sys00102()
                    {
                        DocumentID = Sys00102.DocumentID,
                        ActionID = Sys00102.ActionID,
                        DocumentName = Sys00102.DocumentName,
                        DocumentDescription = Sys00102.DocumentDescription,
                        PhotoExtension = Sys00102.PhotoExtension
                    });
                }
                Sys00101 Sys00101 = new Sys00101()
                {
                    ActionID = item.ActionID,
                    TaskID = item.TaskID,
                    TaskDate = item.TaskDate,
                    TaskDescription = item.TaskDescription,
                    TaskProgress = item.TaskProgress,
                    UserName = item.UserName,
                    PhotoExtension = item.PhotoExtension,
                    UserAsginTO = item.UserAsginTO,
                    Sys00102 = Sys00102List
                };
                Sys00101List.Add(Sys00101);
            }
            return Sys00101List;
        }

        public Sys00101 GetSingle(int ActionID)
        {
            var tasks = _Sys00101RepositoryDataRepository.GetSingle(x => x.ActionID == ActionID);
            return tasks;
        }

        public KaizenResult AddSys00101(Sys00101 newTask)
        {
            KaizenResult result = _Sys00101RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddSys00101(IList<Sys00101> newTask)
        {
            KaizenResult result = _Sys00101RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Sys00101 newTask)
        {
            KaizenResult result = _Sys00101RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Sys00101> newTask)
        {
            KaizenResult result = _Sys00101RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(int ActionID)
        {
            KaizenResult result = _Sys00101RepositoryDataRepository.RemoveKaizenObject(xx => xx.ActionID == ActionID);
            return result;
        }
        public KaizenResult Remove(IList<Sys00101> newTask)
        {
            KaizenResult result = _Sys00101RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
