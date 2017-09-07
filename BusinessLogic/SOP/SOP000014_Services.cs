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
    public class SOP000014Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP000014Repository _SOP000014Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP000014Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP000014Repository = new SOP000014Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP000014> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<SOP000014> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP000014Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP000014Repository.GetWhereListWithPaging("SOP000014", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<SOP000014> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("SetupID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("POSPrefix", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("POSLenth", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("POSLastNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<SOP000014> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _SOP000014Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _SOP000014Repository.GetWhereListWithPaging("SOP000014", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public IList<SOP000014> GetAll()
        {
            var tasks = _SOP000014Repository.GetAll();
            IList<SOP000014> result = tasks;
            return result;
        }
        public SOP000014 GetSingle(int SetupID)
        {
            var tasks = _SOP000014Repository.GetSingle(x => x.SetupID == SetupID);
            return tasks;
        }
        public KaizenResult AddSOP000014(SOP000014 newTask)
        {
            var result = _SOP000014Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(SOP000014 newTask)
        {
            //SOP000014 temp = this.GetSingle(newTask.SetupID, newTask.POPTYPE);
            //if (temp.NextDocumentNumber != newTask.NextDocumentNumber)
            //{
            //    string sql = "Purchase_Order_" + temp.SetupID.Trim() + temp.POPTYPE.ToString() + "_Sequence";
            //    this.ExecuteSqlCommand("drop SEQUENCE " + sql);
            //    sql = "CREATE SEQUENCE " + sql + " START WITH " +
            //        newTask.NextDocumentNumber.ToString() + "INCREMENT BY 1";
            //    this.ExecuteSqlCommand(sql);
            //}
            var result = _SOP000014Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<SOP000014> newTask)
        {
            var result = _SOP000014Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(SOP000014 newTask)
        {
            var result = _SOP000014Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<SOP000014> newTask)
        {
            var result = _SOP000014Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _SOP000014Repository.ExecuteSqlCommand(myQuery);
            return result;
        }
    }
}
