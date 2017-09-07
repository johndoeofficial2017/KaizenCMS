using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;

namespace Kaizen.BusinessLogic.Inventory
{
    public class IV000014Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV000014Repository _IV000014Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV000014Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV000014Repository = new IV000014Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV000014> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV000014> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV000014Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV000014Repository.GetWhereListWithPaging("IV000014", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV000014> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<IV000014> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV000014Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV000014Repository.GetWhereListWithPaging("IV000014", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public IList<IV000014> GetAll()
        {
            var tasks = _IV000014Repository.GetAll();
            IList<IV000014> result = tasks;
            return result;
        }
        public IV000014 GetSingle(int SetupID)
        {
            var tasks = _IV000014Repository.GetSingle(x => x.SetupID == SetupID);
            return tasks;
        }
        public KaizenResult AddIV000014(IV000014 newTask)
        {
            var result = _IV000014Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IV000014 newTask)
        {
            //IV000014 temp = this.GetSingle(newTask.SetupID, newTask.POPTYPE);
            //if (temp.NextDocumentNumber != newTask.NextDocumentNumber)
            //{
            //    string sql = "Purchase_Order_" + temp.SetupID.Trim() + temp.POPTYPE.ToString() + "_Sequence";
            //    this.ExecuteSqlCommand("drop SEQUENCE " + sql);
            //    sql = "CREATE SEQUENCE " + sql + " START WITH " +
            //        newTask.NextDocumentNumber.ToString() + "INCREMENT BY 1";
            //    this.ExecuteSqlCommand(sql);
            //}
            var result = _IV000014Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV000014> newTask)
        {
            var result = _IV000014Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV000014 newTask)
        {
            var result = _IV000014Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV000014> newTask)
        {
            var result = _IV000014Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _IV000014Repository.ExecuteSqlCommand(myQuery);
            return result;
        }

    }
}



