using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00200
    {
        public Met00200()
        {
            this.Met00201 = new List<Met00201>();
            this.Met00202 = new List<Met00202>();
            this.Met00203 = new List<Met00203>();
        }

        public int MeetingID { get; set; }
        public int TRXTypeID { get; set; }
        public int MeetingRoomID { get; set; }
        public string MeetingName { get; set; }
        public string MeetingDescription { get; set; }
        public Nullable<bool> IsFullDay { get; set; }
        public Nullable<int> MeetingRepeatTypeID { get; set; }
        public Nullable<int> RepeatEvery { get; set; }
        public Nullable<bool> RepeatSun { get; set; }
        public Nullable<bool> RepeatMon { get; set; }
        public Nullable<bool> RepeatTus { get; set; }
        public Nullable<bool> RepeatWed { get; set; }
        public Nullable<bool> RepeatThur { get; set; }
        public Nullable<bool> RepeatFrid { get; set; }
        public Nullable<bool> RepeatSat { get; set; }
        public Nullable<System.DateTime> RepeatEndOn { get; set; }
        public Nullable<int> RepeatEndAfter { get; set; }
        public Nullable<bool> RepeatEndNever { get; set; }
        public Nullable<int> RepeatTypeID { get; set; }
        public Nullable<int> MonthRepeatWeekDay { get; set; }
        public Nullable<int> MonthRepeatOn { get; set; }
        public Nullable<int> YearRepeatMonth { get; set; }
        public Nullable<int> YearRepeatOn { get; set; }
        public Nullable<int> YearRepeatMonthDay { get; set; }
        public Nullable<System.DateTime> StartDateTime { get; set; }
        public Nullable<System.DateTime> EndDateTime { get; set; }
        public string RepeatEndType { get; set; }
        public string MonthDayOccurence { get; set; }
        public string YearMonthDayOccurence { get; set; }
        public string RecurrenceRule { get; set; }
        public virtual Met00005 Met00005 { get; set; }
        public virtual Met00204 Met00204 { get; set; }
        public virtual ICollection<Met00201> Met00201 { get; set; }
        public virtual ICollection<Met00202> Met00202 { get; set; }
        public virtual ICollection<Met00203> Met00203 { get; set; }
    }
}
