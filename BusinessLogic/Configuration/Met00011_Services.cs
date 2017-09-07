using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Configuration;
using Kaizen.Configuration.Repository;

namespace Kaizen.BusinessLogic.Configuration
{
    public class Met00011Services
    {
        #region Infrastructure

        private Kaizen.Configuration.Repository.Met00011Repository _Met00011Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Met00011Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Met00011Repository = new Met00011Repository(UserContext.UserName, UserContext.Password);

        }
        #endregion
        public IList<Met00011> GetAll()
        {
            try
            {
                IList<Met00011> L = null;
                try
                {
                    var tasks = _Met00011Repository.GetAll();
                    IList<Met00011> result = tasks;
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
        public Met00011 GetSingle(int CalendarID)
        {
            var tasks = _Met00011Repository.GetSingle(x => x.CalendarID == CalendarID);
            return tasks;
        }

        public KaizenResult AddMet00011(Met00011 newTask)
        {
            //_Met00011Repository.Add(newTask);
            var result = _Met00011Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddMet00011(IList<Met00011> newTask)
        {
            var result = _Met00011Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update00011(Met00011 newTask)
        {
            var result = _Met00011Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update00011(IList<Met00011> newTask)
        {
            var result = _Met00011Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete00011(Met00011 newTask)
        {
            var result = _Met00011Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete00011(IList<Met00011> newTask)
        {
            var result = _Met00011Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        #region Calendar Role Security
        public KaizenResult SaveEventSetupRole(Met00013 Met00013)
        {
            Met00013Repository rep = new Met00013Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(Met00013);
            return result;
        }
        public KaizenResult DeleteEventSetupRole(Met00013 Met00013)
        {
            Met00013Repository rep = new Met00013Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(Met00013);
            return result;
        }
        public IList<Met00013> GetEventSetupRoles(int CalendarID)
        {
            Met00013Repository rep = new Met00013Repository(UserContext.UserName, UserContext.Password);
            var sourceUsers = rep.GetAll().Where(x => x.CalendarID == CalendarID).ToList();
            IList<Met00013> result = sourceUsers;
            return result;
        }
        #endregion

        #region Source Users
        public KaizenResult SaveEventSetupUser(Met00012 Met00012)
        {
            Met00012Repository rep = new Met00012Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(Met00012);
            return result;
        }
        public KaizenResult UpdateEventSetupUser(Met00012 Met00012)
        {
            Met00012Repository rep = new Met00012Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(Met00012);
            return result;
        }
        public KaizenResult DeleteEventSetupUser(Met00012 Met00012)
        {
            Met00012Repository rep = new Met00012Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(Met00012);
            return result;
        }
        public IList<Met00012> GetEventSetupUsers(int CalendarID)
        {
            Met00012Repository rep = new Met00012Repository(UserContext.UserName, UserContext.Password);
            var calendarUsers = rep.GetAll().Where(x => x.CalendarID == CalendarID).ToList();
            IList<Met00012> result = calendarUsers;
            return result;
        }
        #endregion

    }
}
