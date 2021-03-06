﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00204Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00204Repository _IV00204Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00204Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00204Repository = new IV00204Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00204> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00204> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00204Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00204Repository.GetWhereListWithPaging("IV00204", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00204> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ItemQuantity", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00204> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00204Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00204Repository.GetWhereListWithPaging("IV00204", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00204> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<IV00204> L = null;
            var tasks = _IV00204Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<IV00204> result = tasks;
            return result;
        }



        public IList<IV00204> GetAll()
        {
            var tasks = _IV00204Repository.GetAll();
            IList<IV00204> result = tasks;
            return result;
        }
       

        public KaizenResult AddIV00204(IV00204 newTask)
        {
            var result = _IV00204Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00204(IList<IV00204> newTask)
        {
            var result = _IV00204Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00204 newTask)
        {
            var result = _IV00204Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00204> newTask)
        {
            var result = _IV00204Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00204 newTask)
        {
            var result = _IV00204Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00204> newTask)
        {
            var result = _IV00204Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
