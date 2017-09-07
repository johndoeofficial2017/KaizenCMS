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
    public class SOP10201Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10201Repository _SOP10201Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10201Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10201Repository = new SOP10201Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP10201> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP10201> L = null;
            var tasks = _SOP10201Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<SOP10201> result = tasks;
            return result;
        }

        public IList<SOP10201> GetAll()
        {
            try
            {
                IList<SOP10201> L = null;
                try
                {
                    var tasks = _SOP10201Repository.GetAll();
                    IList<SOP10201> result = tasks;
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
        //public SOP10201 GetLineData(string CUSTNMBR, string CurrencyCode, string SiteID)
        //{
        //    var tasks = _SOP10201Repository.GetSingle(x => x.LineID == LineID);
        //    return tasks;
        //}

        public SOP10201 GetSingle(int LineID)
        {
            var tasks = _SOP10201Repository.GetSingle(x => x.LineID == LineID);
            return tasks;
        }
        public IList<SOP10201> GetBySOPNUMBE(string SOPNUMBE, string SOPTypeSetupID)
        {
            var tasks = _SOP10201Repository.GetAll(xx => xx.SOPNUMBE == SOPNUMBE 
            && xx.SOPTypeSetupID == SOPTypeSetupID, null, x => x.SOP10203);
            List<SOP10203> Attachments = new List<SOP10203>();
            foreach (var line in tasks)
            {
                foreach (var item in line.SOP10203)
                {
                    SOP10203 obj = new SOP10203()
                    {
                        DocumentDescription = item.DocumentDescription,
                        DocumentID = item.DocumentID,
                        DocumentName = item.DocumentName,
                        LineID = item.LineID,
                        PhotoExtension = item.PhotoExtension,
                        status = 0
                    };
                    Attachments.Add(obj);
                }
                line.SOP10203 = Attachments;
            }
            return tasks;
        }

        public bool AddSOP10201(IList<SOP10201> newTask)
        {
            foreach (SOP10201 item in newTask)
            {
                _SOP10201Repository.Add(item);
                //if (!string.IsNullOrEmpty(item.LotNumber))
                //{
                //    string strCollumn = string.Empty;
                //    string strCollumnValue = string.Empty;
                //    foreach (var col in item.IV00013)
                //    {
                //        if (col != null)
                //        {
                //            strCollumn += col.CollCode.Trim() + ",";
                //            if (string.IsNullOrEmpty(col.CollValue))
                //                strCollumnValue += "' ',";
                //            else
                //            {
                //                if (col.CollType == 3)
                //                {
                //                    string date = col.CollValue.Substring(4, 11);
                //                    string s = DateTime.ParseExact(date, "MMM dd yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                //                    strCollumnValue += "'" + s + "',";
                //                }
                //                else
                //                    strCollumnValue += "'" + col.CollValue.Trim() + "',";
                //            }
                //        }
                //    }
                //    strCollumn = strCollumn.Substring(0, strCollumn.Length - 1);
                //    strCollumnValue = strCollumnValue.Substring(0, strCollumnValue.Length - 1);
                //    _SOP10201Repository.ExecuteSqlCommand("insert into IV_Lot_" + item.LotNumber.Trim() + "(ItemID,LineID,SOPNUMBE,"
                //        + strCollumn + ") values('" + item.ItemID.Trim() + "'," + item.LineID.ToString() + ",'" + item.SOPNUMBE.Trim() + "',"
                //        + strCollumnValue + ")");
                //}
            }
            return true;
        }
        public bool Update(IList<SOP10201> UpdateSOP10201)
        {

            try
            {
                foreach (SOP10201 item in UpdateSOP10201)
                {
                    _SOP10201Repository.Update(item);
                }
                return true;
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
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(IList<SOP10201> newTask)
        {
            if (newTask == null) return false;
            string str = string.Empty;
            foreach (var item in newTask)
            {
                str += item.LineID + ",";
            }
            str = str.Substring(0, str.Length - 1);
            _SOP10201Repository.ExecuteSqlCommand("delete SOP10201 where LineID in(" + str + ");");
            return true;
        }
    }
}
