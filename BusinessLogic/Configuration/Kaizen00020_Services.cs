using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00020Services
    {
        #region Infrastructure

        private Kaizen00020Repository _Kaizen00020Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Kaizen00020Services(KaizenSession _Kaizen00020Context)
        {
            UserContext = _Kaizen00020Context;
            _Kaizen00020Repository = new Kaizen00020Repository(UserContext.UserName, UserContext.Password);
        }
        #endregion
        public DataCollection<Kaizen00020> GetAllViewBYSQLQuery(string Filters,
            string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<Kaizen00020> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen00020Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Kaizen00020Repository.GetWhereListWithPaging("Kaizen00020", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00020> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("EmailID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("UserName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmailIPassword", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IncomingProtocal", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OutGoingProtocal", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsSSL", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmailTitle", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("InComingPort", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OutGoingPort", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<Kaizen00020> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _Kaizen00020Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _Kaizen00020Repository.GetWhereListWithPaging("Kaizen00020", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00020> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
            string UserName,int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            if (!string.IsNullOrEmpty(SeachStr))
                SeachStr += " and ";
            SeachStr = " UserName='"+ UserName.Trim() + "'";
                DataCollection<Kaizen00020> result = null;
            result = _Kaizen00020Repository.GetWhereListWithPaging("Kaizen00020", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<Kaizen00020> GetAllBYSQLQuery(string Filters, string Searchcritaria, int PageSize, int Page, string Member, string SortDirection)
        {
            Kaizen00020Repository _Kaizen00020Repository = new Kaizen00020Repository(UserContext.UserName, UserContext.Password);
            DataCollection<Kaizen00020> result = null;
            if (!string.IsNullOrEmpty(Filters))
            {
                Page = Page - 1;
                if (!string.IsNullOrEmpty(Searchcritaria))
                {
                    Filters += " and EmailID like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and UserName like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and EmailTitle like '%" + Searchcritaria.Trim() + "%'";
                }
                string sql = "select  * from Kaizen00020";
                sql += " where " + Filters + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
                var tasks = _Kaizen00020Repository.GetSQLData(sql);
                if (tasks != null)
                {
                    tasks.TotalItemCount = tasks.Items.Count;
                    tasks.TotalPageCount = (int)Math.Ceiling((double)tasks.TotalItemCount / (double)PageSize);
                }
                result = tasks;
                return result;
            }
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                var tasks = _Kaizen00020Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            else
            {
                var tasks = _Kaizen00020Repository.GetListWithPaging(xx => xx.UserName.Contains(Searchcritaria)
                    || xx.EmailID.Contains(Searchcritaria) || xx.EmailTitle.Contains(Searchcritaria)
                    , PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            return result;
        }
        public DataCollection<Kaizen00020> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<Kaizen00020> result = null;
            var tasks = _Kaizen00020Repository.GetListWithPaging(PageSize, Page, ss => new { ss.EmailID });
            result = tasks;
            return result;
        }
        public DataCollection<Kaizen00020> GetTop6()
        {
            DataCollection<Kaizen00020> result = new DataCollection<Kaizen00020>();
            var tasks = _Kaizen00020Repository.GetAll();
            result.Items = tasks.ToList();

            return result;
        }
        public List<Kaizen00020> GetAll()
        {
            var tasks = _Kaizen00020Repository.GetAll();
            List<Kaizen00020> result = tasks.ToList();
            return result;
        }
        public Kaizen00020 GetSingle(string EmailID)
        {
            var tasks = _Kaizen00020Repository.GetSingle(x => x.EmailID.Trim() == EmailID.Trim());
            return tasks;
        }
        public IList<Kaizen00020> GetEmailsByUserName(string UserName)
        {
            var tasks = _Kaizen00020Repository.GetAll(xx => xx.UserName.Trim() == UserName.Trim());
            return tasks;
        }
        //public string HashPassword(string password)
        //{
        //    return BCrypt.Net.BCrypt.HashPassword(password);
        //}

        //public bool Verify(string password, string hash)
        //{
        //    return BCrypt.Net.BCrypt.Verify(password, hash);
        //}

        public KaizenResult AddKaizen00020(Kaizen00020 newTask)
        {
            var result = _Kaizen00020Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddKaizen00020(IList<Kaizen00020> newTask)
        {
            var result = _Kaizen00020Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(Kaizen00020 newTask)
        {
            var result = _Kaizen00020Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<Kaizen00020> newTask)
        {
            var result = _Kaizen00020Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(Kaizen00020 newTask)
        {
            var result = _Kaizen00020Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Kaizen00020> newTask)
        {
            var result = _Kaizen00020Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        public IList<Kaizen00020> GetEmailSettings()
        {
            Kaizen00020Repository rep = new Kaizen00020Repository(UserContext.UserName, UserContext.Password);
            var emailSettings = rep.GetAll(xx => xx.UserName == UserContext.UserName);
            IList<Kaizen00020> result = emailSettings;
            return result;
        }
    }
}
