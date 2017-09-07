using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen004Services
    {
        #region Infrastructure

        private Kaizen004Repository _Kaizen004Repository;
        private KaizenSession UserContext;
        public Kaizen004Services(KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Kaizen004Repository = new Kaizen004Repository(UserContext.UserName, UserContext.Password);
        }
        #endregion
        public IList<Kaizen004> GetList()
        {
            var tasks = _Kaizen004Repository.GetList();
            IList<Kaizen004> result = tasks;
            return result;
        }
        public IList<Kaizen004> GetModuleTasks(int ModuleID)
        {
            try
            {
                IList<Kaizen004> result;
                var tasks = _Kaizen004Repository.GetList(zz => zz.ModuleID == ModuleID);
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

        public IList<Kaizen004> GetRoleTasksByModuleID(int RoleID, int ModuleID, string CompanyID)
        {
            IList<Kaizen004> result;
            var tasks = _Kaizen004Repository.GetList(zz => zz.ModuleID == ModuleID && zz.RoleID == RoleID && zz.CompanyID == CompanyID);
            result = tasks;
            return result;
        }

        public KaizenResult AddKaizen004(IList<Kaizen004> newTask)
        {
            var result = _Kaizen004Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Update(IList<Kaizen004> newTask)
        {
            var result = _Kaizen004Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Delete(IList<Kaizen004> newTask)
        {
            var result = _Kaizen004Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

    }
}


