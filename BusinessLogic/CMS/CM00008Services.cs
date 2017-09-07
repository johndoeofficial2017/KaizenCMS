﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00008Services
    {
        #region Infrastructure

        private CM00008Repository _CM00008Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00008Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00008Repository = new CM00008Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00008> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00008> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00008Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00008Repository.GetWhereListWithPaging("CM00008", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<CM00008> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("AddressCodeType", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AddressTypeName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00008> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00008Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00008Repository.GetWhereListWithPaging("CM00008", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00008> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<CM00008> L = null;
            var tasks = _CM00008Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM00008> result = tasks;
            return result;
        }
        public DataCollection<CM00008> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00008> result = null;
            var tasks = _CM00008Repository.GetListWithPaging(PageSize, Page, ss => new { ss.AddressCodeType });
            result = tasks;
            return result;
        }

        public IList<CM00008> GetAll()
        {
            try
            {
                IList<CM00008> L = null;
                try
                {
                    var tasks = _CM00008Repository.GetAll();
                    IList<CM00008> result = tasks;
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

        public CM00008 GetSingle(int AddressCodeType)
        {
            var tasks = _CM00008Repository.GetSingle(x => x.AddressCodeType == AddressCodeType);
            return tasks;
        }

        public KaizenResult AddCM00008(CM00008 newTask)
        {
            var result = _CM00008Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00008(IList<CM00008> newTask)
        {
            var result = _CM00008Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00008 newTask)
        {
            var result = _CM00008Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00008> newTask)
        {
            var result = _CM00008Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00008 newTask)
        {
            var result = _CM00008Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00008> newTask)
        {
            var result = _CM00008Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}
