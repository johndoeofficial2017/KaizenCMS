﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;

namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00010Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00010Repository _IV00010Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00010Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00010Repository = new IV00010Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00010> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00010> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00010Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00010Repository.GetWhereListWithPaging("IV00010", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00010> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ClassID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GroupName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00010> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00010Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00010Repository.GetWhereListWithPaging("IV00010", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00010> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<IV00010> L = null;
            var tasks = _IV00010Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<IV00010> result = tasks;
            return result;
        }
        public DataCollection<IV00010> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<IV00010> result = null;
            var tasks = _IV00010Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public IList<IV00010> GetAll()
        {
            try
            {
                IList<IV00010> L = null;
                try
                {
                    var tasks = _IV00010Repository.GetAll();
                    IList<IV00010> result = tasks;
                    return result;
                }
                catch (Exception ex)
                {
                }
                return null;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- GenderID: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return null;
        }
        public DataCollection<IV00010> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                DataCollection<IV00010> result = null;
                if (string.IsNullOrEmpty(SearchTerm))
                {
                    var tasks = _IV00010Repository.GetListWithPaging(x => x.LotNumber.Contains(SearchTerm)
                        || x.LotName.Contains(SearchTerm)
                        , PageSize, Page, ss => ss.LotNumber);

                    result = tasks;
                }
                else
                {
                    var tasks = _IV00010Repository.GetListWithPaging(PageSize, Page, ss => ss.LotNumber);

                    result = tasks;
                }
                return result;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- GenderID: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return null;
        }
        public DataCollection<IV00010> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<IV00010> result = null;
            var tasks = _IV00010Repository.GetListWithPaging(PageSize, Page, ss => new { ss.LotNumber });
            result = tasks;
            return result;
        }

        public IV00010 GetSingle(string LotNumber)
        {
            try
            {
                var tasks = _IV00010Repository.GetSingle(x => x.LotNumber == LotNumber);
                return tasks;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- GenderID: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return null;
        }

        public KaizenResult AddIV00010(IV00010 newTask)
        {
            var result = _IV00010Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00010(IList<IV00010> newTask)
        {
            var result = _IV00010Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00010 newTask)
        {
            var result = _IV00010Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00010> newTask)
        {
            var result = _IV00010Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00010 newTask)
        {
            var result = _IV00010Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00010> newTask)
        {
            var result = _IV00010Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}



