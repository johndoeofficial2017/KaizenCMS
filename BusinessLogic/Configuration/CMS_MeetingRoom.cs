using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Configuration;
using Kaizen.Configuration.Repository;

namespace Kaizen.BusinessLogic.Configuration
{
    public class Met00204Services
    {
        #region Infrastructure

        private Kaizen.Configuration.Repository.Met00204Repository _Met00204Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Met00204Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Met00204Repository = new Met00204Repository(UserContext.UserName, UserContext.Password);

        }
        #endregion
        public IList<Met00204> GetAll()
        {
            try
            {
                IList<Met00204> L = null;
                try
                {
                    var tasks = _Met00204Repository.GetAll();
                    IList<Met00204> result = tasks;
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
        public Met00204 GetSingle(int MeetingRoomID)
        {
            var tasks = _Met00204Repository.GetSingle(x => x.MeetingRoomID == MeetingRoomID);
            return tasks;
        }

        public KaizenResult AddMet00204(Met00204 newTask)
        {
            var result = _Met00204Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddMet00204(IList<Met00204> newTask)
        {
            var result = _Met00204Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update00204(Met00204 newTask)
        {
            var result = _Met00204Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update00204(IList<Met00204> newTask)
        {
            var result = _Met00204Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete00204(Met00204 newTask)
        {
            var result = _Met00204Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete00204(IList<Met00204> newTask)
        {
            var result = _Met00204Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        #region Meeting Room Role
        public IList<Met00206> GetRolesByMeetingRoom(int MeetingRoomID)
        {
            Met00206Repository rep = new Met00206Repository(UserContext.UserName, UserContext.Password);
            var actionTyperoles = rep.GetAll(xx => xx.MeetingRoomID == MeetingRoomID);
            IList<Met00206> result = actionTyperoles;
            return result;
        }

        public KaizenResult AddMet00206(Met00206 newTask)
        {
            Met00206Repository rep = new Met00206Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(newTask);
            //rep.ExecuteSqlCommand("exec [dbo].[Sys_TempReconcile20]");
            return result;
        }
        public KaizenResult DeleteMet00206(Met00206 newTask)
        {
            Met00206Repository rep = new Met00206Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(newTask);
            return result;
        }
        #endregion

        #region Meeting Room User
        public IList<Met00205> GetMeetingRoomsByUser(string userName)
        {
            Met00205Repository rep = new Met00205Repository(UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(xx => xx.UserName == userName);
            IList<Met00205> result = tasks;
            return result;
        }

        public KaizenResult DeleteMet00205(Met00205 caseTypeUser)
        {
            Met00205Repository rep = new Met00205Repository(UserContext.UserName, UserContext.Password);
            var result = rep.DeleteKaizenObject(caseTypeUser);
            return result;
        }

        public KaizenResult AddMet00205(Met00205 caseTypeUser)
        {
            Met00205Repository rep = new Met00205Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(caseTypeUser);
            return result;
        }
        #endregion
    }
}
