﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;
using System.Linq.Dynamic;
using Kaizen.Admin.DAL;
using Kaizen.Admin;
namespace Kaizen.Admin.Repository
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        public string CompanyID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        static GenericDataRepository()
        {
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }
        public GenericDataRepository(string CompanyID, string UserName, string serPassword)
        {
            this.CompanyID = CompanyID;
            this.UserName = UserName;
            this.UserPassword = serPassword;
        }

        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();
                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);


                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            }
            return list;
        }
        public virtual IList<T> GetAll(Expression<Func<T, bool>> where)
        {
            List<T> list;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();
                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }
        public virtual IList<T> GetAll(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }

        public virtual IList<T> GetAll(Expression<Func<T, object>> keySelector,
            Expression<Func<T, object>> keySelectorDesc, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                if (keySelector != null)
                    dbQuery = dbQuery.OrderBy(keySelector);

                if (keySelectorDesc != null)
                    dbQuery = dbQuery.OrderByDescending(keySelectorDesc);

                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            }
            return list;
        }

        public virtual IList<T> GetList(Expression<Func<T, bool>> where, Expression<Func<T, object>> keySelector,
         Expression<Func<T, object>> keySelectorDesc,
          params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                if (keySelector != null)
                    dbQuery = dbQuery.OrderBy(keySelector);

                if (keySelectorDesc != null)
                    dbQuery = dbQuery.OrderByDescending(keySelectorDesc);


                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }

        public virtual IList<T> GetList(string where, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();

                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }
        public virtual IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }
        public virtual IList<T> GetList(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            }
            return list;
        }
        public virtual IList<T> GetList(Expression<Func<T, bool>> where)
        {
            List<T> list;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();
                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }

        public virtual IList<T> GetList(string where, Expression<Func<T, object>> keySelector,
            Expression<Func<T, object>> keySelectorDesc,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                if (keySelector != null)
                    dbQuery = dbQuery.OrderBy(keySelector);

                if (keySelectorDesc != null)
                    dbQuery = dbQuery.OrderByDescending(keySelectorDesc);

                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }




        public virtual IList<T> GetSome(Expression<Func<T, bool>> where,
        params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();
                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);



                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }
        public virtual DataCollection<T> GetAllWithPaging(int PageSize, int CurrentPage, Expression<Func<T, object>> keySelector,
            Expression<Func<T, object>> keySelectorDesc,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            if ((CurrentPage == 0) || (PageSize == 0))
                return null;

            if (keySelector == null && keySelectorDesc == null)// if there is Paging at least one order should be there
                throw new NotSupportedException("In Paging there should be a sorting key");

            DataCollection<T> ItemCollection = null;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                ItemCollection = new DataCollection<T>();

                if (dbQuery != null)
                {

                    ItemCollection.TotalItemCount = dbQuery.Count();
                    if (ItemCollection.TotalItemCount == 0)
                        return ItemCollection;

                }
                ItemCollection.TotalPageCount = (int)Math.Ceiling((double)ItemCollection.TotalItemCount / (double)PageSize);

                if (keySelector != null)
                    dbQuery = dbQuery.OrderBy(keySelector);

                if (keySelectorDesc != null)
                    dbQuery = dbQuery.OrderByDescending(keySelectorDesc);

                dbQuery = dbQuery.Skip<T>((CurrentPage - 1) * PageSize).Take<T>(PageSize);

                ItemCollection.ThisPageItemCount = dbQuery.Count();

                ItemCollection.Items = dbQuery
               .AsNoTracking()
               .ToList<T>();
            }
            return ItemCollection;
        }

        public virtual void GetSome(ref DataCollection<T> ItemCollection, int PageSize, int CurrentPage, string Orderby, Expression<Func<T, bool>> whereCondition,
           params Expression<Func<T, object>>[] navigationProperties)
        {
            if ((CurrentPage == 0) || (PageSize == 0))
                return;


            ItemCollection = null;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Where(whereCondition).OrderBy(Orderby).Include<T, object>(navigationProperty);

                ItemCollection = new DataCollection<T>();

                if (dbQuery != null)
                {

                    ItemCollection.TotalItemCount = dbQuery.Count();

                }
                ItemCollection.TotalPageCount = (int)Math.Ceiling((double)ItemCollection.TotalItemCount / (double)PageSize);


                dbQuery = dbQuery.Skip<T>((CurrentPage - 1) * PageSize).Take<T>(PageSize);

                ItemCollection.ThisPageItemCount = dbQuery.Count();

                ItemCollection.Items = dbQuery
               .AsNoTracking()
               .ToList<T>();
            }
        }

        public virtual void GetSome(ref DataCollection<T> ItemCollection, int PageSize, int CurrentPage, string Orderby, string SearchFilter,
           params Expression<Func<T, object>>[] navigationProperties)
        {
            if ((CurrentPage == 0) || (PageSize == 0))
                return;


            ItemCollection = null;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Where(SearchFilter).OrderBy(Orderby).Include<T, object>(navigationProperty);

                ItemCollection = new DataCollection<T>();

                if (dbQuery != null)
                {

                    ItemCollection.TotalItemCount = dbQuery.Count();

                }
                ItemCollection.TotalPageCount = (int)Math.Ceiling((double)ItemCollection.TotalItemCount / (double)PageSize);

                dbQuery = dbQuery.Skip<T>((CurrentPage - 1) * PageSize).Take<T>(PageSize);

                ItemCollection.ThisPageItemCount = dbQuery.Count();

                ItemCollection.Items = dbQuery
               .AsNoTracking()
               .ToList<T>();
            }
        }

        public virtual void GetAll(ref DataCollection<T> ItemCollection, int PageSize, int CurrentPage, string Orderby,
           params Expression<Func<T, object>>[] navigationProperties)
        {
            if ((CurrentPage == 0) || (PageSize == 0))
                return;


            ItemCollection = null;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.OrderBy(Orderby).Include<T, object>(navigationProperty);

                ItemCollection = new DataCollection<T>();

                if (dbQuery != null)
                {

                    ItemCollection.TotalItemCount = dbQuery.Count();

                }
                ItemCollection.TotalPageCount = (int)Math.Ceiling((double)ItemCollection.TotalItemCount / (double)PageSize);

                dbQuery = dbQuery.Skip<T>((CurrentPage - 1) * PageSize).Take<T>(PageSize);

                ItemCollection.ThisPageItemCount = dbQuery.Count();

                ItemCollection.Items = dbQuery
               .AsNoTracking()
               .ToList<T>();
            }
        }

        public virtual DataCollection<T> GetListWithPaging(string where, int PageSize, int CurrentPage
    , Expression<Func<T, object>> keySelector, bool isasc, params Expression<Func<T, object>>[] navigationProperties)
        {
            try
            {
                if ((CurrentPage == 0) || (PageSize == 0)) return null;
                if (keySelector == null) throw new NotSupportedException("In Paging there should be a sorting key");

                DataCollection<T> ItemCollection = null;
                using (var context = new AdminContext(CompanyID, UserName, UserPassword))
                {
                    IQueryable<T> dbQuery = context.Set<T>();
                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(navigationProperty);

                    ItemCollection = new DataCollection<T>();
                    //dbQuery = dbQuery.Where(where);
                    if (dbQuery != null)
                    {
                        ItemCollection.TotalItemCount = dbQuery.Count();
                        if (ItemCollection.TotalItemCount == 0)
                            return ItemCollection;
                    }
                    ItemCollection.TotalPageCount = (int)Math.Ceiling((double)ItemCollection.TotalItemCount / (double)PageSize);
                    if (keySelector != null)
                        dbQuery = dbQuery.OrderBy(keySelector);

                    dbQuery = dbQuery.Skip<T>((CurrentPage - 1) * PageSize).Take<T>(PageSize);
                    ItemCollection.ThisPageItemCount = dbQuery.Count();
                    ItemCollection.Items = dbQuery.AsNoTracking().ToList<T>();
                }
                return ItemCollection;
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
            catch (System.Exception excep)
            {
                System.Diagnostics.Debug.WriteLine("errors:", excep.Message);
                return null;
            }
            return null;
        }

        public virtual DataCollection<T> GetListWithPaging(Expression<Func<T, bool>> where, int PageSize, int CurrentPage, Expression<Func<T, object>> keySelector = null, Expression<Func<T, object>> keySelectorDesc = null,
           params Expression<Func<T, object>>[] navigationProperties)
        {
            if ((CurrentPage == 0) || (PageSize == 0))
                return null;
            if (keySelector == null && keySelectorDesc == null)// if there is Paging at least one order should be there
                throw new NotSupportedException("In Paging there should be a sorting key");

            DataCollection<T> ItemCollection = null;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                ItemCollection = new DataCollection<T>();

                dbQuery = dbQuery.Where(where);

                if (dbQuery != null)
                {

                    ItemCollection.TotalItemCount = dbQuery.Count();
                    if (ItemCollection.TotalItemCount == 0)
                        return ItemCollection;

                }

                ItemCollection.TotalPageCount = (int)Math.Ceiling((double)ItemCollection.TotalItemCount / (double)PageSize);


                if (keySelector != null)
                    dbQuery = dbQuery.OrderBy(keySelector);

                if (keySelectorDesc != null)
                    dbQuery = dbQuery.OrderByDescending(keySelectorDesc);




                dbQuery = dbQuery.Skip<T>((CurrentPage - 1) * PageSize).Take<T>(PageSize);


                ItemCollection.ThisPageItemCount = dbQuery.Count();




                ItemCollection.Items = dbQuery
               .AsNoTracking()
               .ToList<T>();
            }

            return ItemCollection;
        }

        public virtual DataCollection<T> GetListWithPaging(string where, int PageSize, int CurrentPage, Expression<Func<T, object>> keySelector,
         Expression<Func<T, object>> keySelectorDesc,
         params Expression<Func<T, object>>[] navigationProperties)
        {
            if ((CurrentPage == 0) || (PageSize == 0))
                return null;

            if (keySelector == null && keySelectorDesc == null)// if there is Paging at least one order should be there
                throw new NotSupportedException("In Paging there should be a sorting key");

            DataCollection<T> ItemCollection = null;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                ItemCollection = new DataCollection<T>();
                if (dbQuery != null)
                {
                    ItemCollection.TotalItemCount = dbQuery.Count();
                    if (ItemCollection.TotalItemCount == 0)
                        return ItemCollection;
                }
                ItemCollection.TotalPageCount = (int)Math.Ceiling((double)ItemCollection.TotalItemCount / (double)PageSize);
                if (keySelector != null)
                    dbQuery = dbQuery.OrderBy(keySelector);

                if (keySelectorDesc != null)
                    dbQuery = dbQuery.OrderByDescending(keySelectorDesc);

                dbQuery = dbQuery.Where(where).Skip<T>((CurrentPage - 1) * PageSize).Take<T>(PageSize);

                ItemCollection.ThisPageItemCount = dbQuery.Count();



                ItemCollection.Items = dbQuery
                   .AsNoTracking()
                   .ToList<T>();
            }
            return ItemCollection;
        }
        public virtual DataCollection<T> GetWhereListWithPaging(string TableName, int PageSize, int CurrentPage, string Filters, string Member, bool IsAscending)
        {
            try
            {
                string SortDirection = string.Empty;
                string Searchcritaria = string.Empty;
                if (IsAscending)
                    SortDirection = " ORDER BY " + Member + " asc";
                else
                    SortDirection = " ORDER BY " + Member + " desc";

                if (!string.IsNullOrEmpty(Filters))
                    Searchcritaria = " where " + Filters;

                string sql = "select * from " + TableName + Searchcritaria + SortDirection;
                if (PageSize > 0)
                    sql += " OFFSET " + (PageSize * (CurrentPage - 1)).ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
                var tasks = this.GetSQLData(sql);
                if (tasks != null)
                {
                    tasks.TotalItemCount = this.GetSQLINT("select count(*) from " + TableName + " " + Searchcritaria);
                    tasks.TotalPageCount = (int)Math.Ceiling((double)tasks.TotalItemCount / (double)PageSize);
                }
                return tasks;
            }
            catch (System.Exception excep)
            {
                System.Diagnostics.Debug.WriteLine("errors:", excep.Message);
                return null;
            }
        }
        public virtual DataCollection<T> GetListWithPaging(int PageSize, int CurrentPage, Expression<Func<T, object>> keySelector = null, Expression<Func<T, object>> keySelectorDesc = null,
        params Expression<Func<T, object>>[] navigationProperties)
        {
            if ((CurrentPage == 0) || (PageSize == 0))
                return null;
            if (keySelector == null && keySelectorDesc == null)// if there is Paging at least one order should be there
                throw new NotSupportedException("In Paging there should be a sorting key");

            DataCollection<T> ItemCollection = null;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                ItemCollection = new DataCollection<T>();

                if (dbQuery != null)
                {

                    ItemCollection.TotalItemCount = dbQuery.Count();
                    if (ItemCollection.TotalItemCount == 0)
                        return ItemCollection;

                }

                ItemCollection.TotalPageCount = (int)Math.Ceiling((double)ItemCollection.TotalItemCount / (double)PageSize);


                if (keySelector != null)
                    dbQuery = dbQuery.OrderBy(keySelector);

                if (keySelectorDesc != null)
                    dbQuery = dbQuery.OrderByDescending(keySelectorDesc);




                dbQuery = dbQuery.Skip<T>((CurrentPage - 1) * PageSize).Take<T>(PageSize);

                ItemCollection.ThisPageItemCount = dbQuery.Count();

                ItemCollection.Items = dbQuery
               .AsNoTracking()
               .ToList<T>();
            }

            return ItemCollection;
        }
        public virtual DataCollection<T> GetWhereListWithPaging(string where, int PageSize, int CurrentPage,
           Expression<Func<T, object>> keySelector, bool SelectorType,
           params Expression<Func<T, object>>[] navigationProperties)
        {
            try
            {
                if ((CurrentPage == 0) || (PageSize == 0)) return null;
                if (keySelector == null) throw new NotSupportedException("In Paging there should be a sorting key");

                DataCollection<T> ItemCollection = null;
                using (var context = new AdminContext(CompanyID, UserName, UserPassword))
                {
                    IQueryable<T> dbQuery = context.Set<T>();
                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(navigationProperty);

                    ItemCollection = new DataCollection<T>();
                    if (!string.IsNullOrEmpty(where))
                        dbQuery = dbQuery.Where(where);
                    if (dbQuery != null)
                    {
                        ItemCollection.TotalItemCount = dbQuery.Count();
                        if (ItemCollection.TotalItemCount == 0)
                            return ItemCollection;
                    }
                    ItemCollection.TotalPageCount = (int)Math.Ceiling((double)ItemCollection.TotalItemCount / (double)PageSize);
                    if (SelectorType)
                        dbQuery = dbQuery.OrderBy(keySelector);
                    else
                        dbQuery = dbQuery.OrderByDescending(keySelector);

                    dbQuery = dbQuery.Skip<T>((CurrentPage - 1) * PageSize).Take<T>(PageSize);
                    ItemCollection.ThisPageItemCount = dbQuery.Count();
                    ItemCollection.Items = dbQuery.AsNoTracking().ToList<T>();
                }
                return ItemCollection;
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
            catch (System.Exception excep)
            {
                System.Diagnostics.Debug.WriteLine("errors:", excep.Message);
                return null;
            }
            return null;
        }
        public virtual DataCollection<T> GetWhereListWithPaging(int PageSize, int CurrentPage,
Expression<Func<T, object>> keySelector, bool SelectorType, params Expression<Func<T, object>>[] navigationProperties)
        {
            return this.GetWhereListWithPaging("", PageSize, CurrentPage, keySelector, SelectorType, navigationProperties);
        }

        public virtual IList<T> GetListDistinctBy(Expression<Func<T, bool>> where, Func<T, object> keyDistinctBy, Expression<Func<T, object>> keySelector,
            Expression<Func<T, object>> keySelectorDesc,
           params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                if (keySelector != null)
                    dbQuery = dbQuery.OrderBy(keySelector);

                if (keySelectorDesc != null)
                    dbQuery = dbQuery.OrderByDescending(keySelectorDesc);

                list = dbQuery
                        .AsNoTracking()
                        .Where(where)
                        .DistinctBy(keyDistinctBy).ToList<T>();
            }
            return list;
        }

        public void AddOrAttach(T entity)
        {
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                //var tracked= context.Set<T>().Find(context.KeyValuesFor(entity));
                //if (tracked != null)
                //{
                //    context.Entry(tracked).CurrentValues.SetValues(entity);

                //}
                //context.Set<T>().Add(entity);

            }
        }

        public virtual T GetSingle(Expression<Func<T, bool>> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            }
            return item;
        }

        public virtual int GetCount(Expression<Func<T, bool>> where)
        {
            int Count = 0;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();
                Count = dbQuery
                    .Where(where)
                    .Count<T>();
            }
            return Count;
        }

        public virtual long Count(Expression<Func<T, bool>> where)
        {
            long Count = 0;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();
                Count = dbQuery
                    .Where(where)
                    .Count<T>();
            }
            return Count;
        }

        public virtual long Count()
        {
            long Count = 0;
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                IQueryable<T> dbQuery = context.Set<T>();
                Count = dbQuery
                    .Count<T>();
            }
            return Count;
        }

        public virtual void Add(params T[] items)
        {
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Added;
                }
                context.SaveChanges();
            }
        }
        public virtual KaizenResult AddKaizenObject(params T[] items)
        {
            KaizenResult result = new KaizenResult();
            try
            {
                using (var context = new AdminContext(CompanyID, UserName, UserPassword))
                {
                    foreach (T item in items)
                    {
                        context.Entry(item).State = EntityState.Added;
                    }
                    if (context.SaveChanges() > 0)
                    {
                        result.Status = true;
                        result.Massage = "Data has been Saved Successfully";
                        result.Description = "";
                    }
                    else
                    {
                        result.Status = false;
                        result.Massage = "Failed to Save Data";
                        result.Description = "";
                    }
                }

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        result.Status = false;
                        result.Massage = "Failed to Save Data";
                        if (string.IsNullOrEmpty(result.Massage))
                        {
                            result.Description = string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                        else
                        {
                            result.Description += "<br/>" + string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
            }
            catch (System.Data.Entity.Core.UpdateException e)
            {
                foreach (var eve in e.StateEntries)
                {
                    result.Status = false;
                    result.Massage = "Failed to Save Data";
                    if (string.IsNullOrEmpty(result.Massage))
                    {
                        result.Description = string.Format("- Property: \"{0}\", State: \"{1}\"", eve.Entity.GetType().Name, eve.State);
                    }
                    else
                    {
                        result.Description += "<br/>" + string.Format("- Property: \"{0}\", State: \"{1}\"", eve.Entity.GetType().Name, eve.State);
                    }
                }
            }


            catch (System.Data.Entity.Infrastructure.DbUpdateException e) //DbContext
            {
                foreach (var eve in e.Entries)
                {
                    result.Status = false;
                    result.Massage = "Failed to Save Data";
                    if (string.IsNullOrEmpty(result.Massage))
                    {
                        result.Description = string.Format("- Property: \"{0}\", State: \"{1}\"", eve.Entity.GetType().Name, eve.State);
                    }
                    else
                    {
                        result.Description += "<br/>" + string.Format("- Property: \"{0}\", State: \"{1}\"", eve.Entity.GetType().Name, eve.State);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                if (ex.InnerException == null)
                {
                    result.Massage = ex.ToString();
                    result.Description = ex.Message;
                }
                else
                {
                    result.Massage = ex.InnerException.ToString();
                    result.Description = ex.InnerException.Message;
                }
            }
            return result;
        }

        public virtual void Update(params T[] items)
        {
            try
            {
                using (var context = new AdminContext(CompanyID, UserName, UserPassword))
                {
                    foreach (T item in items)
                    {
                        context.Entry(item).State = EntityState.Modified;
                    }
                    context.SaveChanges();
                }

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public virtual KaizenResult UpdateKaizenObject(params T[] items)
        {
            KaizenResult result = new KaizenResult();
            try
            {
                using (var context = new AdminContext(CompanyID, UserName, UserPassword))
                {
                    foreach (T item in items)
                    {
                        context.Entry(item).State = EntityState.Modified;
                    }
                    if (context.SaveChanges() > 0)
                    {
                        result.Status = true;
                        result.Massage = "Data has been Updated Successfully";
                        result.Description = "";
                    }
                    else
                    {
                        result.Status = false;
                        result.Massage = "Failed to Update Data";
                        result.Description = "";
                    }
                }

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        result.Status = false;
                        result.Massage = "Failed to Update Data";
                        if (string.IsNullOrEmpty(result.Massage))
                        {
                            result.Description = string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                        else
                        {
                            result.Description += "<br/>" + string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                if (ex.InnerException == null)
                {
                    result.Massage = ex.ToString();
                    result.Description = ex.Message;
                }
                else
                {
                    result.Massage = ex.InnerException.ToString();
                    result.Description = ex.InnerException.Message;
                }
            }
            return result;
        }
        public virtual KaizenResult UpdateWithPropertiesKaizenObject(T entity, params Expression<Func<T, object>>[] updatedProperties)
        {
            KaizenResult result = new KaizenResult();
            try
            {
                using (var context = new AdminContext(CompanyID, UserName, UserPassword))
                {
                    var dbEntityEntry = context.Entry(entity);
                    if (updatedProperties.Any())
                    {
                        foreach (var property in updatedProperties)
                        {
                            dbEntityEntry.Property(property).IsModified = true;
                        }
                    }
                    else
                    {
                        foreach (var property in dbEntityEntry.OriginalValues.PropertyNames)
                        {
                            var original = dbEntityEntry.OriginalValues.GetValue<object>(property);
                            var current = dbEntityEntry.CurrentValues.GetValue<object>(property);
                            if (original != null && !original.Equals(current))
                                dbEntityEntry.Property(property).IsModified = true;
                        }
                    }
                    if (context.SaveChanges() > 0)
                    {
                        result.Status = true;
                        result.Massage = "Data has been Updated Successfully";
                    }
                    else
                    {
                        result.Status = false;
                        result.Massage = "Failed to Update Data";
                    }
                }

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        result.Status = false;
                        result.Massage = ve.ErrorMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                if (ex.InnerException == null)
                    result.Massage = ex.ToString();
                else
                    result.Massage = ex.InnerException.ToString();
            }
            return result;
        }

        public virtual void Delete(params T[] items)
        {
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {

                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }
        }
        public virtual KaizenResult DeleteKaizenObject(params T[] items)
        {
            KaizenResult result = new KaizenResult();
            try
            {
                using (var context = new AdminContext(CompanyID, UserName, UserPassword))
                {
                    foreach (T item in items)
                    {
                        context.Entry(item).State = EntityState.Deleted;
                    }
                    if (context.SaveChanges() > 0)
                    {
                        result.Status = true;
                        result.Massage = "Data has been Deleted Successfully";
                    }
                    else
                    {
                        result.Status = false;
                        result.Massage = "Failed to Delete Data";
                    }
                }

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        result.Status = false;
                        result.Massage = "Failed to Delete Data";
                        if (string.IsNullOrEmpty(result.Massage))
                        {
                            result.Description = string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                        else
                        {
                            result.Description += "<br/>" + string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                if (ex.InnerException == null)
                {
                    result.Massage = ex.ToString();
                    result.Description = ex.Message;
                }
                else
                {
                    result.Massage = ex.InnerException.ToString();
                    result.Description = ex.InnerException.Message;
                }
            }
            return result;
        }

        public virtual void Remove(Expression<Func<T, bool>> where)
        {
            var objects = GetList(where, null, null);
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                foreach (var obj in objects)
                    context.Entry(obj).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
        public virtual KaizenResult RemoveKaizenObject(Expression<Func<T, bool>> where)
        {
            KaizenResult result = new KaizenResult();
            try
            {
                var objects = GetList(where, null, null);
                using (var context = new AdminContext(CompanyID, UserName, UserPassword))
                {
                    foreach (var obj in objects)
                        context.Entry(obj).State = EntityState.Deleted;
                    if (context.SaveChanges() > 0)
                    {
                        result.Status = true;
                        result.Massage = "Data has been Deleted Successfully";
                    }
                    else
                    {
                        result.Status = false;
                        result.Massage = "Failed to Delete Data";
                    }

                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        result.Status = false;
                        result.Massage = "Failed to Delete Data";
                        if (string.IsNullOrEmpty(result.Massage))
                        {
                            result.Description = string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                        else
                        {
                            result.Description += "<br/>" + string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                if (ex.InnerException == null)
                {
                    result.Massage = ex.ToString();
                    result.Description = ex.Message;
                }
                else
                {
                    result.Massage = ex.InnerException.ToString();
                    result.Description = ex.InnerException.Message;
                }
            }
            return result;
        }

        public IQueryable<T> GetQueryable()
        {
            using (var context = new AdminContext(CompanyID, UserName, UserPassword))
            {
                return context.Set<T>().AsQueryable<T>();
            }
        }

        ///-------------------------------------------------------------------
        public int GetSQLINT(string myQuery)
        {
            using (var db = new AdminContext(CompanyID, UserName, UserPassword))
            {
                db.Database.Connection.ConnectionString = db.Database.Connection.ConnectionString.Replace("TWO", CompanyID.Trim());
                var TransactionID = db.Database.ExecuteSqlCommand(myQuery);// db.Database.(myQuery).FirstOrDefault();
                return (int)TransactionID;
            }
        }
        public long GetSQLLong(string myQuery)
        {
            using (var db = new AdminContext(CompanyID, UserName, UserPassword))
            {
                db.Database.Connection.ConnectionString = db.Database.Connection.ConnectionString.Replace("TWO", CompanyID.Trim());
                var TransactionID = db.Database.SqlQuery<long>(myQuery).FirstOrDefault();
                return TransactionID;
            }
        }
        public int ExecuteSqlCommand(string myQuery)
        {
            using (var db = new AdminContext(CompanyID, UserName, UserPassword))
            {
                db.Database.Connection.ConnectionString = db.Database.Connection.ConnectionString.Replace("TWO", CompanyID.Trim());
                var TransactionID = db.Database.ExecuteSqlCommand(myQuery);
                return (int)TransactionID;
            }
        }
        public virtual DataCollection<T> GetSQLData(string myQuery)
        {
            try
            {
                DataCollection<T> ItemCollection = null;
                using (var context = new AdminContext(CompanyID, UserName, UserPassword))
                {
                    ItemCollection = new DataCollection<T>();
                    ItemCollection.Items = context.Database.SqlQuery<T>(myQuery).ToList();
                }
                return ItemCollection;
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
            catch (System.Exception excep)
            {
                System.Diagnostics.Debug.WriteLine("errors:", excep.Message);
                return null;
            }
            return null;
        }
    }
}