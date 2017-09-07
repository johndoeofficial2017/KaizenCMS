using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00104Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00104Repository _GL00104RepositoryDataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00104Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00104RepositoryDataRepository = new GL00104Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion

        public DataCollection<GL00104> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<GL00104> L = null;
            var tasks = _GL00104RepositoryDataRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<GL00104> result = tasks;
            return result;
        }
        public DataCollection<GL00104> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<GL00104> result = null;
            var tasks = _GL00104RepositoryDataRepository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }


        public DataCollection<GL00104> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection, string CurrencyCode)
        {
            DataCollection<GL00104> result = null;
            var tasks = _GL00104RepositoryDataRepository.GetListWithPaging(ss => ss.CurrencyCode == CurrencyCode.Trim(), PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }
        public DataCollection<GL00104> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection,string CurrencyCode)
        {
            DataCollection<GL00104> result = null;
            var tasks = _GL00104RepositoryDataRepository.GetListWithPaging(ss => ss.CurrencyCode == CurrencyCode.Trim(), PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }


        public IList<GL00104> GetAllByCurrencyCode(string CurrencyCode)
        {
            IList<GL00104> L = null;
            var tasks = _GL00104RepositoryDataRepository.GetAll(x => x.CurrencyCode.Trim() == CurrencyCode.Trim());
            IList<GL00104> result = tasks;
            return result;
        }
        public GL00104 GetSingle(string CurrencyCode)
        {
            var tasks = _GL00104RepositoryDataRepository.GetSingle(x => x.CurrencyCode.Trim() == CurrencyCode.Trim());
            return tasks;
        }

        public DataCollection<GL00104> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<GL00104> result = null;
            _GL00104RepositoryDataRepository = new GL00104Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _GL00104RepositoryDataRepository.GetListWithPaging(SearchTerm, PageSize, Page, ss => ss.CurrencyCode, true);
            result = tasks;
            return result;
        }

        public GL00104 GetGLByCurrency(string CurrencyCode)
        {
            GL00104 oGL00104 = _GL00104RepositoryDataRepository.GetSingle(x => x.CurrencyCode.Trim() == CurrencyCode.Trim());
            string str = string.Empty;
            if (oGL00104 != null)
            {
                if (oGL00104.RealizedGain.HasValue && oGL00104.RealizedGain != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += oGL00104.RealizedGain.Value.ToString();
                    else
                        str += "," + oGL00104.RealizedGain.Value.ToString();
                }
                if (oGL00104.RealizedLoss.HasValue && oGL00104.RealizedLoss != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += oGL00104.RealizedLoss.Value.ToString();
                    else
                        str += "," + oGL00104.RealizedLoss.Value.ToString();
                }
                if (oGL00104.RoundingDifference.HasValue && oGL00104.RoundingDifference != null)
                {
                    if (string.IsNullOrEmpty(str))
                        str += oGL00104.RoundingDifference.Value.ToString();
                    else
                        str += "," + oGL00104.RoundingDifference.Value.ToString();
                }

                GL00100Repository oGL00100Repository = new GL00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                string sql = "select * from GL00100 where AccountID in(" + str.Trim() + ")";
                var tasks = oGL00100Repository.GetSQLData(sql);
                DataCollection<GL00100> result = tasks;
                foreach (GL00100 o in result.Items)
                {
                    if (o.AccountID == oGL00104.RealizedGain)
                        oGL00104.RealizedGainAccount = o;
                    if (o.AccountID == oGL00104.RealizedLoss)
                        oGL00104.RealizedLossAccount = o;
                    if (o.AccountID == oGL00104.RoundingDifference)
                        oGL00104.RoundingDifferenceAccount = o;
                }
            }

            return oGL00104;
        }

        public KaizenResult AddGL00104(GL00104 newTask)
        {
            var result = _GL00104RepositoryDataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00104(IList<GL00104> newTask)
        {
            var result = _GL00104RepositoryDataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(GL00104 newTask)
        {
            var result = _GL00104RepositoryDataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00104> newTask)
        {
            var result = _GL00104RepositoryDataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00104 newTask)
        {
            var result = _GL00104RepositoryDataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00104> newTask)
        {
            var result = _GL00104RepositoryDataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }


    }
}
