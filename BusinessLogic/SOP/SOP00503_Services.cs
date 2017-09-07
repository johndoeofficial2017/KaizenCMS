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
    public class SOP00503Services
    {
        #region Infrastructure

        private Kaizen.SOP.Repository.SOP00503Repository _SOP00503Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public SOP00503Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _SOP00503Repository = new SOP00503Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<SOP00503> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<SOP00503> L = null;
            var tasks = _SOP00503Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                );
            DataCollection<SOP00503> result = tasks;
            return result;
        }

        public IList<SOP00503> GetAll()
        {
            try
            {
                IList<SOP00503> L = null;
                try
                {
                    var tasks = _SOP00503Repository.GetAll();
                    IList<SOP00503> result = tasks;
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
        public SOP00503 GetSingle(string TrxDocumentID)
        {
            var tasks = _SOP00503Repository.GetSingle(x => x.TrxDocumentID == TrxDocumentID);
            return tasks;
        }
        public IList<SOP00503> GetByTrxDocumentID(string TrxDocumentID)
        {
            var tasks = _SOP00503Repository.GetAll(xx => xx.TrxDocumentID == TrxDocumentID);
            IList<SOP00503> result = tasks;
            return result;
        }

        public bool AddSOP00503(IList<SOP00503> newTask)
        {
            _SOP00503Repository.Add(newTask.ToArray());
            return true;
        }
        public bool Update(IList<SOP00503> UpdateSOP00503)
        {

            try
            {
                foreach (SOP00503 item in UpdateSOP00503)
                {
                    _SOP00503Repository.Update(item);
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
        public bool Delete(IList<SOP00503> newTask)
        {
            if (newTask == null) return false;
            foreach (var item in newTask)
            {
                _SOP00503Repository.ExecuteSqlCommand("delete SOP00503 where TrxDocumentID='" + item.TrxDocumentID.Trim() +
                    "' and SOPNUMBE='" + item.SOPNUMBE.Trim() + "' and SOPTypeSetupID='" +
                    item.SOPTypeSetupID.Trim() + "' and SOPTYPE ='" + item.SOPTYPE + "';");
            }
            return true;
        }
    }
}
