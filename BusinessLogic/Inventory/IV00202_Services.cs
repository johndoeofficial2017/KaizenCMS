using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Inventory.Repository;
using Kaizen.Inventory;
using System.Globalization;
namespace Kaizen.BusinessLogic.Inventory
{
    public class IV00202Services
    {
        #region Infrastructure

        private IV00202Repository _IV00202Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public IV00202Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _IV00202Repository = new IV00202Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public IList<IV00202> GetAll()
        {
            try
            {
                IList<IV00202> L = null;
                try
                {
                    var tasks = _IV00202Repository.GetAll();
                    IList<IV00202> result = tasks;
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

        public KaizenResult AddIV00202(IV00202 newTask)
        {
            var result = _IV00202Repository.AddKaizenObject(newTask);
            return result;
        }
       
   
        public KaizenResult Delete(IV00202 newTask)
        {
            var result = _IV00202Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<IV00202> newTask)
        {
            var result = _IV00202Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }



    }
}
