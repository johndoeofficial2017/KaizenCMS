using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Configuration;
using Kaizen.Configuration.Repository;

namespace Kaizen.BusinessLogic.Configuration
{
    public class Met00001Services
    {
        #region Infrastructure

        private Kaizen.Configuration.Repository.Met00001Repository _Met00001Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Met00001Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Met00001Repository = new Met00001Repository(UserContext.UserName, UserContext.Password);

        }
        #endregion
        public IList<Met00001> GetAll()
        {
            try
            {
                IList<Met00001> L = null;
                try
                {
                    var tasks = _Met00001Repository.GetAll();
                    IList<Met00001> result = tasks;
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
        public Met00001 GetSingle(int CalendarID)
        {
            var tasks = _Met00001Repository.GetSingle(x => x.CalendarID == CalendarID);
            return tasks;
        }

        public KaizenResult AddMet00001(Met00001 newTask)
        {
            //_Met00001Repository.Add(newTask);
            var result = _Met00001Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddMet00001(IList<Met00001> newTask)
        {
            var result = _Met00001Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update00001(Met00001 newTask)
        {
            var result = _Met00001Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update00001(IList<Met00001> newTask)
        {
            var result = _Met00001Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete00001(Met00001 newTask)
        {
            var result = _Met00001Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete00001(IList<Met00001> newTask)
        {
            var result = _Met00001Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        #region Calendar Role Security
        public KaizenResult SaveCalendarRole(Met00003 Met00003)
        {
            Met00003Repository rep = new Met00003Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(Met00003);
            return result;
        }
        public KaizenResult DeleteCalendarRole(Met00003 Met00003)
        {
            Met00003Repository rep = new Met00003Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(Met00003);
            return result;
        }
        public IList<Met00003> GetCalendarRoles(int CalendarID)
        {
            Met00003Repository rep = new Met00003Repository(UserContext.UserName, UserContext.Password);
            var sourceUsers = rep.GetAll().Where(x => x.CalendarID == CalendarID).ToList();
            IList<Met00003> result = sourceUsers;
            return result;
        }
        #endregion

        #region Source Users
        public KaizenResult SaveCalendarUser(Met00002 Met00002)
        {
            Met00002Repository rep = new Met00002Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(Met00002);
            return result;
        }
        public KaizenResult UpdateCalendarUser(Met00002 Met00002)
        {
            Met00002Repository rep = new Met00002Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(Met00002);
            return result;
        }
        public KaizenResult DeleteCalendarUser(Met00002 Met00002)
        {
            Met00002Repository rep = new Met00002Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(Met00002);
            return result;
        }
        public IList<Met00002> GetCalendarUsers(int CalendarID)
        {
            Met00002Repository rep = new Met00002Repository(UserContext.UserName, UserContext.Password);
            var calendarUsers = rep.GetAll().Where(x => x.CalendarID == CalendarID).ToList();
            IList<Met00002> result = calendarUsers;
            return result;
        }
        #endregion

    }
}
