using System;
using System.Collections.Generic;
using Kaizen.GL.Repository;
using System.Linq;
using Kaizen.GL;
using Kaizen.BusinessLogic.Configuration;

namespace Kaizen.BusinessLogic.GL
{
    public class HistroyGLTransactionEntry
    {
        #region Infrastructure

        private GL00700Repository Rep;
        private Kaizen.Configuration.KaizenSession UserContext;
        public HistroyGLTransactionEntry(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            Rep = new GL00700Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public static bool IsNumeric(object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }
        public DataCollection<GL00700> GetDataListGrid(string Filters, string Searchcritaria,
                 string YearCode, int PERIODID,int SelectdViewID,
                 int PageSize, int Page, string Member, bool IsAscending)
        {
            DataCollection<GL00700> result = null;
            string SeachStr = Filters;
            if (!string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr += " and ";
                if (IsNumeric(Searchcritaria))
                {
                    SeachStr += " TransactionID =" + Searchcritaria.Trim();
                }
                else
                {
                    SeachStr += " GLReference like '%" + Searchcritaria.Trim() + "%'";
                    SeachStr += " and TrxDescription like '%" + Searchcritaria.Trim() + "%'";
                }
            }
            if (string.IsNullOrEmpty(SeachStr))
            {
                if (YearCode != string.Empty && PERIODID != -1)
                    result = Rep.GetListWithPaging(x => x.YearCode == YearCode 
                    && x.PERIODID == PERIODID
                    , PageSize, Page, ss => Member);
                else if (PERIODID != -1)
                    result = Rep.GetListWithPaging(x => x.PERIODID == PERIODID, PageSize, Page, ss => new { ss.TransactionID });
                else
                    result = Rep.GetListWithPaging(x => x.YearCode == YearCode
                , PageSize, Page, ss => Member);

            }
            else
            {
                if (YearCode != string.Empty && PERIODID != -1)
                    SeachStr += " and YearCode =" + YearCode.ToString() + " and PERIODID=" + PERIODID;
                else if (YearCode != string.Empty)
                    SeachStr += " and YearCode =" + YearCode.ToString();
                else if (PERIODID != -1)
                    SeachStr += " and PERIODID=" + PERIODID;

                result = Rep.GetWhereListWithPaging("GL00700", PageSize, Page, SeachStr, Member, IsAscending);
            }
            return result;
        }
        public GL00700 GetSingle(int TransactionID, int PERIODID)
        {
            var tasks = Rep.GetSingle(x => x.TransactionID == TransactionID && x.PERIODID == PERIODID);
            return tasks;
        }
        public IList<GL00701> GetByTransactionID(int TransactionID, int PERIODID)
        {
            GL00701Repository Rep = new GL00701Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = Rep.GetAll(xx => xx.TransactionID == TransactionID && xx.PERIODID == PERIODID);
            IList<GL00701> result = tasks;
            return result;
        }
    }
}
