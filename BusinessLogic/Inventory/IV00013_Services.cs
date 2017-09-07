using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00013Services
    {
        #region Infrastructure

        private Kaizen.Inventory.Repository.IV00013Repository _IV00013Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00013Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00013Repository = new IV00013Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<IV00013> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<IV00013> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00013Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00013Repository.GetWhereListWithPaging("IV00013", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<IV00013> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("CollCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CollName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LotNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CollType", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LookupFrom", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LookupLotNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LookupLotName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00013> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00013Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _IV00013Repository.GetWhereListWithPaging("IV00013", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<IV00013> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string LotNumber,
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
                    SeachStr += Help.GetFilter("CollCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CollName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LotNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CollType", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LookupFrom", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LookupLotNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LookupLotName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<IV00013> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _IV00013Repository.GetListWithPaging(ss => ss.LotNumber.Trim() == LotNumber.Trim(), PageSize, Page, ss => Member);
            else
                result = _IV00013Repository.GetWhereListWithPaging("IV00013", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public IList<IV00013> GetAll()
        {
            var tasks = _IV00013Repository.GetAll();
            IList<IV00013> result = tasks;
            return result;
        }
        public DataCollection<IV00013> GetListWithPaging(string LotNumber, int PageSize, int Page, string Member, string SortDirection)
        {
            try
            {
                DataCollection<IV00013> result = null;
                var tasks = _IV00013Repository.GetListWithPaging(x => x.LotNumber.Trim() == LotNumber.Trim()
                    , PageSize, Page, ss => ss.LotNumber);
                result = tasks;
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
        public IV00013 GetSingle(string LotNumber)
        {
            try
            {
                var tasks = _IV00013Repository.GetSingle(x => x.LotNumber == LotNumber);
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
        public IList<IV00013> GetAllLotColumn(string LotNumber)
        {
            var tasks = _IV00013Repository.GetAll(xx => xx.LotNumber == LotNumber, ss => new { ss.CollCode }, xx => xx.IV00014);
            IList<IV00013> result = tasks;
            List<IV00013> L = new List<IV00013>();
            foreach (IV00013 item in result)
            {
                IV00013 temp = new IV00013()
                {
                    CollCode = item.CollCode,
                    CollName = item.CollName,
                    CollType = item.CollType,
                    LotNumber = item.LotNumber
                };
                if (item.IV00014 != null && item.IV00014.Count > 0)
                {
                    temp.IV00014 = new List<IV00014>();
                    foreach (IV00014 itemList in item.IV00014)
                    {
                        temp.IV00014.Add(new IV00014()
                        {
                            ListID = itemList.ListID,
                            ListName = itemList.ListName,
                            CollCode = itemList.CollCode
                        });
                    }
                }
                L.Add(temp);
            }
            return L;
        }

        public KaizenResult AddIV00013(IV00013 newTask)
        {
            var result = _IV00013Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddIV00013(IList<IV00013> newTask)
        {
            var result = _IV00013Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IV00013 newTask)
        {
            var result = _IV00013Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<IV00013> newTask)
        {
            var result = _IV00013Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(IV00013 newTask)
        {
            var result = _IV00013Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00013> newTask)
        {
            var result = _IV00013Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}



