using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Configuration;
using Kaizen.Configuration.Repository;

namespace Kaizen.BusinessLogic.Configuration
{
    public class Met00007Services
    {
        #region Infrastructure

        private Kaizen.Configuration.Repository.Met00007Repository _Met00007Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Met00007Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Met00007Repository = new Met00007Repository(UserContext.UserName, UserContext.Password);

        }
        #endregion
        public IList<Met00007> GetAll()
        {
            try
            {
                IList<Met00007> L = null;
                try
                {
                    var tasks = _Met00007Repository.GetAll();
                    IList<Met00007> result = tasks;
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
        public Met00007 GetSingle(int MeetingRoomID)
        {
            var tasks = _Met00007Repository.GetSingle(x => x.MeetingRoomID == MeetingRoomID);
            return tasks;
        }

        public KaizenResult AddMet00007(Met00007 newTask)
        {
            var result = _Met00007Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddMet00007(IList<Met00007> newTask)
        {
            var result = _Met00007Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update00007(Met00007 newTask)
        {
            var result = _Met00007Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update00007(IList<Met00007> newTask)
        {
            var result = _Met00007Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete00007(Met00007 newTask)
        {
            var result = _Met00007Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete00007(IList<Met00007> newTask)
        {
            var result = _Met00007Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        #region Meeting Room Role
        public IList<Met00008> GetRolesByMeetingRoom(int MeetingRoomID)
        {
            Met00008Repository rep = new Met00008Repository(UserContext.UserName, UserContext.Password);
            var actionTyperoles = rep.GetAll(xx => xx.MeetingRoomID == MeetingRoomID);
            IList<Met00008> result = actionTyperoles;
            return result;
        }

        public KaizenResult AddMet00008(Met00008 newTask)
        {
            Met00008Repository rep = new Met00008Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(newTask);
            //rep.ExecuteSqlCommand("exec [dbo].[Sys_TempReconcile20]");
            return result;
        }
        public KaizenResult DeleteMet00008(Met00008 newTask)
        {
            Met00008Repository rep = new Met00008Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(newTask);
            return result;
        }
        #endregion

        #region Meeting Room User
        public IList<Met00009> GetMeetingRoomsByUser(string userName)
        {
            Met00009Repository rep = new Met00009Repository(UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(xx => xx.UserName == userName);
            IList<Met00009> result = tasks;
            return result;
        }

        public KaizenResult DeleteMet00009(Met00009 caseTypeUser)
        {
            Met00009Repository rep = new Met00009Repository(UserContext.UserName, UserContext.Password);
            var result = rep.DeleteKaizenObject(caseTypeUser);
            return result;
        }

        public KaizenResult AddMet00009(Met00009 caseTypeUser)
        {
            Met00009Repository rep = new Met00009Repository(this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(caseTypeUser);
            return result;
        }
        #endregion
    }
}
