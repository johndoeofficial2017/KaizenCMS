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
    public class SOP10251Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP10251Repository _SOP10251Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP10251Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP10251Repository = new SOP10251Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP10251> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP10251> L = null;
            var tasks = _SOP10251Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<SOP10251> result = tasks;
            return result;
        }

        public IList<SOP10251> GetAll()
        {
            var tasks = _SOP10251Repository.GetAll();
            IList<SOP10251> result = tasks;
            return result;
        }
        public SOP10251 GetSingle(int LineID)
        {
            var tasks = _SOP10251Repository.GetSingle(x => x.LineID == LineID);
            return tasks;
        }
        public IList<SOP10251> GetBySOPNUMBE(string SOPNUMBE, string SOPTypeSetupID)
        {
            var tasks = _SOP10251Repository.GetAll(xx => xx.SOPNUMBE == SOPNUMBE && xx.SOPTypeSetupID == SOPTypeSetupID);
            IList<SOP10251> result = tasks;
            return result;
        }

        public bool AddSOP10251(IList<SOP10251> newTask)
        {
            foreach (SOP10251 item in newTask)
            {
                _SOP10251Repository.Add(item);
                if (!string.IsNullOrEmpty(item.LotNumber))
                {
                    string strCollumn = string.Empty;
                    string strCollumnValue = string.Empty;
                    foreach (var col in item.IV00013)
                    {
                        if (col != null)
                        {
                            strCollumn += col.CollCode.Trim() + ",";
                            if (string.IsNullOrEmpty(col.CollValue))
                                strCollumnValue += "' ',";
                            else
                            {
                                if (col.CollType == 3)
                                {
                                    string date = col.CollValue.Substring(4, 11);
                                    string s = DateTime.ParseExact(date, "MMM dd yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                                    strCollumnValue += "'" + s + "',";
                                }
                                else
                                    strCollumnValue += "'" + col.CollValue.Trim() + "',";
                            }
                        }
                    }
                    strCollumn = strCollumn.Substring(0, strCollumn.Length - 1);
                    strCollumnValue = strCollumnValue.Substring(0, strCollumnValue.Length - 1);
                    _SOP10251Repository.ExecuteSqlCommand("insert into IV_Lot_" + item.LotNumber.Trim() + "(ItemID,LineID,SOPNUMBE,"
                        + strCollumn + ") values('" + item.ItemID.Trim() + "'," + item.LineID.ToString() + ",'" + item.SOPNUMBE.Trim() + "',"
                        + strCollumnValue + ")");
                }
            }
            return true;
        }
        public bool Update(IList<SOP10251> UpdateSOP10251)
        {
            foreach (SOP10251 item in UpdateSOP10251)
            {
                _SOP10251Repository.Update(item);
            }
            return true;
        }
        public bool Delete(IList<SOP10251> newTask)
        {
            if (newTask == null) return false;
            string str = string.Empty;
            foreach (var item in newTask)
            {
                str += item.LineID + ",";
            }
            str = str.Substring(0, str.Length - 1);
            _SOP10251Repository.ExecuteSqlCommand("delete SOP10251 where LineID in(" + str + ");");
            return true;
        }
    }
}
