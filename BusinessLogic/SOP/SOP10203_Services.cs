using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.SOP.Repository;
using Kaizen.SOP;
using System.Globalization;
namespace Kaizen.BusinessLogic.SOP
{
    public class SOP10203Services
    {
        #region Infrastructure

        private SOP10203Repository _SOP10203RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        private Kaizen.Configuration.Config00100 iInventoryConfig;
        public Kaizen.Configuration.Config00100 InventoryConfig
        {
            get
            {
                return Master.GetInventoryConfig(this.UserContext.CompanyID);
            }
        }
        public SOP10203Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10203RepositoryDataRepository = new SOP10203Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<SOP10203> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("LineID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DocumentName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DocumentDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PhotoExtension", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP10203> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _SOP10203RepositoryDataRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _SOP10203RepositoryDataRepository.GetWhereListWithPaging("SOP10203", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }


        public DataCollection<SOP10203> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<SOP10203> L = null;
            var tasks = _SOP10203RepositoryDataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<SOP10203> result = tasks;
            return result;
        }
        public IList<SOP10203> GetAllByQuotationLine(int LineID)
        {
            var tasks = _SOP10203RepositoryDataRepository.GetAll(x => x.LineID == LineID);
            return tasks;
        }

        public SOP10203 GetSingle(int DocumentID)
        {
            var tasks = _SOP10203RepositoryDataRepository.GetSingle(x => x.DocumentID == DocumentID);
            return tasks;
        }
        public bool AddSOP10203(SOP10203 newTask)
        {
            _SOP10203RepositoryDataRepository.Add(newTask);
            return true;
        }
        public bool Update(SOP10203 SOP10203)
        {
            _SOP10203RepositoryDataRepository.Update(SOP10203);
            return true;
        }
        public bool Delete(IList<SOP10203> newTask, string ServerPath)
        {
            if (newTask == null) return false;
            string str = string.Empty;
            foreach (var item in newTask)
            {
                str += item.DocumentID + ",";
            }
            str = str.Substring(0, str.Length - 1);
            int sucess = _SOP10203RepositoryDataRepository.ExecuteSqlCommand("delete SOP10203 where DocumentID in(" + str + ");");
            if (sucess > 0)
            {
                foreach (var item in newTask)
                {
                    string Destination = ServerPath + item.DocumentName.Trim() + item.PhotoExtension.Trim();
                    if (System.IO.File.Exists(Destination))
                    {
                        System.IO.File.Delete(Destination);
                    }
                }
            }
            return true;
        }
    }
}
