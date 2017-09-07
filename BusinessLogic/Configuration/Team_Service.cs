using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Configuration;
using Kaizen.Configuration.Repository;

namespace Kaizen.BusinessLogic.Configuration
{
    public class Met00100Services
    {
        #region Infrastructure

        private Kaizen.Configuration.Repository.Met00100Repository _Met00100Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Met00100Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Met00100Repository = new Met00100Repository(UserContext.UserName, UserContext.Password);

        }
        #endregion
        public IList<Met00100> GetAll()
        {
            try
            { 
                IList<Met00100> L = null;
                try
                {
                    var tasks = _Met00100Repository.GetAll(xx => xx.Met00101);
                    IList<Met00100> result = tasks;
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

        public IList<Met00100> FillAttendees(IList<Met00100> Met00100)
        {
            try
            {
                Met00102Repository _Met00102Repository = new Met00102Repository(UserContext.UserName, UserContext.Password);
                foreach (Met00100 obj in Met00100)
                {
                    IList<Met00102> Met00102List = new List<Met00102>();
                    Met00102List = _Met00102Repository.GetAll().Where(x => x.MeetingID == obj.MeetingID).ToList();

                    if (Met00102List.Count > 0)
                    {
                        foreach (Met00102 Met00102 in Met00102List)
                        {
                            obj.Met00102.Add(new Met00102()
                            {
                                AttendeeID = Met00102.AttendeeID,
                                AttendeeName = Met00102.AttendeeName,
                                MeetingID = Met00102.MeetingID
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            return Met00100;
        }
        public Met00100 GetSingle(int MeetingID)
        {
            var tasks = _Met00100Repository.GetSingle(x => x.MeetingID == MeetingID);
            return tasks;
        }

        //public IList<Met00001> GetAllCalenderNamesByMeeting()
        //{
        //    Met00101Repository rep = new Met00101Repository(UserContext.UserName, UserContext.Password);
        //    var calenderNames = rep.GetAll();
        //    IList<Met00001> result = calenderNames;
        //    return result;
        //}
        public KaizenResult AddMet00100(Met00100 newTask)
        {
            _Met00100Repository.Add(newTask);
            //var result = _Met00100Repository.Add(newTask);
            return null;
        }
        public KaizenResult AddMet00100(IList<Met00100> newTask)
        {
            var result = _Met00100Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update00100(Met00100 newTask)
        {
            var result = _Met00100Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update00100(IList<Met00100> newTask)
        {
            var result = _Met00100Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult SaveMet00102(IList<Met00102> Met00102List)
        {
            Met00102Repository _Met00102Repository = new Met00102Repository(UserContext.UserName, UserContext.Password);
            foreach (Met00102 Met00102 in Met00102List)
            {
                Met00102.Met00100 = null;
                Met00102 obj = _Met00102Repository.GetAll().Where(x => x.AttendeeID == Met00102.AttendeeID).FirstOrDefault();
                if (obj == null)
                    _Met00102Repository.AddKaizenObject(Met00102);
                else
                    _Met00102Repository.UpdateKaizenObject(Met00102);
            }
            return null;
        }

        public KaizenResult SaveMet00101(IList<Met00101> Met00101List)
        {
            Met00101Repository _Met00101Repository = new Met00101Repository(UserContext.UserName, UserContext.Password);
            foreach (Met00101 Met00101 in Met00101List)
            {
                Met00101.Met00100 = null;
                _Met00101Repository.AddKaizenObject(Met00101);
            }
            return null;
        }
        public KaizenResult Delete(int MeetingID)
        {
            Met00101Repository _Met00101Repository = new Met00101Repository(UserContext.UserName, UserContext.Password);
            IList<Met00101> Met00101List = _Met00101Repository.GetAll().Where(x => x.MeetingID == MeetingID).ToList();
            var result = (Met00101List.Count > 0) ? _Met00101Repository.DeleteKaizenObject(Met00101List.ToArray()) : null;
            return result;
        }
        public KaizenResult Delete(Met00100 newTask)
        {
            Met00102Repository _Met00102Repository = new Met00102Repository(UserContext.UserName, UserContext.Password);
            IList<Met00102> Met00102List = _Met00102Repository.GetAll().Where(x => x.MeetingID == newTask.MeetingID).ToList();
            if (Met00102List.Count > 0)
                _Met00102Repository.DeleteKaizenObject(Met00102List.ToArray());

            Met00101Repository _Met00101Repository = new Met00101Repository(UserContext.UserName, UserContext.Password);
            IList<Met00101> Met00101List = _Met00101Repository.GetAll().Where(x => x.MeetingID == newTask.MeetingID).ToList();
            if (Met00101List.Count > 0)
                _Met00101Repository.DeleteKaizenObject(Met00101List.ToArray());

            var result = _Met00100Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<Met00100> newTask)
        {
            var result = _Met00100Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        public IList<Met00001> GetAllCalenderNames()
        {
            Met00001Repository rep = new Met00001Repository(UserContext.UserName, UserContext.Password);
            var calenderNames = rep.GetAll();
            IList<Met00001> result = calenderNames;
            return result;
        }
        public IList<Met00007> GetAllRooms()
        {
            Met00007Repository rep = new Met00007Repository(UserContext.UserName, UserContext.Password);
            var rooms = rep.GetAll();
            IList<Met00007> result = rooms;
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
