using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Configuration;
using Kaizen.Configuration.Repository;

namespace Kaizen.BusinessLogic.Configuration
{
    public class Met00200Services
    {
        #region Infrastructure

        private Kaizen.Configuration.Repository.Met00200Repository _Met00200Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Met00200Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Met00200Repository = new Met00200Repository(UserContext.UserName, UserContext.Password);

        }
        #endregion
        public IList<Met00200> GetAll()
        {
            try
            {
                IList<Met00200> L = null;
                try
                {
                    var tasks = _Met00200Repository.GetAll(xx => xx.Met00201);
                    IList<Met00200> result = tasks;
                    result = FillAttendees(result);
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

        public IList<Met00200> FillAttendees(IList<Met00200> Met00200)
        {
            try
            {
                Met00202Repository _Met00202Repository = new Met00202Repository(UserContext.UserName, UserContext.Password);
                foreach (Met00200 obj in Met00200)
                {
                    IList<Met00202> Met00202List = new List<Met00202>();
                    Met00202List = _Met00202Repository.GetAll().Where(x => x.MeetingID == obj.MeetingID).ToList();

                    if (Met00202List.Count > 0)
                    {
                        foreach (Met00202 Met00202 in Met00202List)
                        {
                            obj.Met00202.Add(new Met00202()
                            {
                                AttendeeID = Met00202.AttendeeID,
                                AttendeeName = Met00202.AttendeeName,
                                MeetingID = Met00202.MeetingID
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            return Met00200;
        }
        public Met00200 GetSingle(int MeetingID)
        {
            var tasks = _Met00200Repository.GetSingle(x => x.MeetingID == MeetingID);
            return tasks;
        }

        //public IList<Met00011> GetAllCalenderNamesByMeeting()
        //{
        //    Met00201Repository rep = new Met00201Repository(UserContext.UserName, UserContext.Password);
        //    var calenderNames = rep.GetAll();
        //    IList<Met00011> result = calenderNames;
        //    return result;
        //}
        public KaizenResult AddMet00200(Met00200 newTask)
        {
            var result = _Met00200Repository.AddKaizenObject(newTask);
            //var result = _Met00200Repository.Add(newTask);
            return result;
        }
        public KaizenResult AddMet00200(IList<Met00200> newTask)
        {
            var result = _Met00200Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update00200(Met00200 newTask)
        {
            var result = _Met00200Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update00200(IList<Met00200> newTask)
        {
            var result = _Met00200Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult SaveMet00202(IList<Met00202> Met00202List)
        {
            Met00202Repository _Met00202Repository = new Met00202Repository(UserContext.UserName, UserContext.Password);
            foreach (Met00202 Met00202 in Met00202List)
            {
                Met00202.Met00200 = null;
                Met00202 obj = _Met00202Repository.GetAll().Where(x => x.AttendeeID == Met00202.AttendeeID).FirstOrDefault();
                if (obj == null)
                    _Met00202Repository.AddKaizenObject(Met00202);
                else
                    _Met00202Repository.UpdateKaizenObject(Met00202);
            }
            return null;
        }

        public KaizenResult SaveMet00201(IList<Met00201> Met00201List)
        {
            Met00201Repository _Met00201Repository = new Met00201Repository(UserContext.UserName, UserContext.Password);
            var result = _Met00201Repository.AddKaizenObject(Met00201List.ToArray());
            return null;
        }
        public KaizenResult SaveMet00203(IList<Met00203> Met00203List)
        {
            Met00203Repository _Met00203Repository = new Met00203Repository(UserContext.UserName, UserContext.Password);
            var result = _Met00203Repository.AddKaizenObject(Met00203List.ToArray());
            return result;
        }
        public KaizenResult Delete(int MeetingID)
        {
            Met00201Repository _Met00201Repository = new Met00201Repository(UserContext.UserName, UserContext.Password);
            IList<Met00201> Met00201List = _Met00201Repository.GetAll().Where(x => x.MeetingID == MeetingID).ToList();
            var result = (Met00201List.Count > 0) ? _Met00201Repository.DeleteKaizenObject(Met00201List.ToArray()) : null;
            return result;
        }
        public KaizenResult DeleteMet00203(int MeetingID)
        {
            Met00203Repository _Met00203Repository = new Met00203Repository(UserContext.UserName, UserContext.Password);
            IList<Met00203> Met00203List = _Met00203Repository.GetAll().Where(x => x.MeetingID == MeetingID).ToList();
            var result = (Met00203List.Count > 0) ? _Met00203Repository.DeleteKaizenObject(Met00203List.ToArray()) : null;
            return result;
        }
        public KaizenResult Delete(Met00200 newTask)
        {
            Met00202Repository _Met00202Repository = new Met00202Repository(UserContext.UserName, UserContext.Password);
            IList<Met00202> Met00202List = _Met00202Repository.GetAll().Where(x => x.MeetingID == newTask.MeetingID).ToList();
            if (Met00202List.Count > 0)
                _Met00202Repository.DeleteKaizenObject(Met00202List.ToArray());

            Met00201Repository _Met00201Repository = new Met00201Repository(UserContext.UserName, UserContext.Password);
            IList<Met00201> Met00201List = _Met00201Repository.GetAll().Where(x => x.MeetingID == newTask.MeetingID).ToList();
            if (Met00201List.Count > 0)
                _Met00201Repository.DeleteKaizenObject(Met00201List.ToArray());

            var result = _Met00200Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Met00200> newTask)
        {
            var result = _Met00200Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }
        public IList<Met00203> GetMet00203(int MeetingID)
        {
            Met00203Repository rep = new Met00203Repository(UserContext.UserName, UserContext.Password);
            var Met00203List = rep.GetAll().Where(x=>x.MeetingID== MeetingID).ToList();
            IList<Met00203> result = Met00203List;
            return result;
        }
        public IList<Met00011> GetAllCalenderNames()
        {
            Met00011Repository rep = new Met00011Repository(UserContext.UserName, UserContext.Password);
            var calenderNames = rep.GetAll();
            IList<Met00011> result = calenderNames;
            return result;
        }
        public IList<Met00204> GetAllRooms()
        {
            Met00204Repository rep = new Met00204Repository(UserContext.UserName, UserContext.Password);
            var rooms = rep.GetAll();
            IList<Met00204> result = rooms;
            return result;
        }
        public IList<Met00005> GetAllMeetingRepeatTypes()
        {
            Met00005Repository rep = new Met00005Repository(UserContext.UserName, UserContext.Password);
            var meetingRepeateTypes = rep.GetAll();
            IList<Met00005> result = meetingRepeateTypes;
            return result;
        }

        public Met00005 GetMeetingRepeatType(int meetingRepeatTypeID)
        {
            Met00005Repository rep = new Met00005Repository(UserContext.UserName, UserContext.Password);
            Met00005 meetingRepeateType = rep.GetAll(x => x.MeetingRepeatTypeID == meetingRepeatTypeID).FirstOrDefault();
            Met00005 result = meetingRepeateType;
            return result;
        }

    }
}
