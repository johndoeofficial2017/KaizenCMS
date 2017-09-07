using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00100
    {
        public Met00100()
        {
            this.Met00101 = new List<Met00101>();
            this.Met00102 = new List<Met00102>();
        }

        public int MeetingID { get; set; }
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
        public virtual Met00006 Met00006 { get; set; }
        public virtual Met00007 Met00007 { get; set; }
        public virtual ICollection<Met00101> Met00101 { get; set; }
        public virtual ICollection<Met00102> Met00102 { get; set; }
    }
}
