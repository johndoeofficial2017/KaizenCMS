using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00200Map : EntityTypeConfiguration<Met00200>
    {
        public Met00200Map()
        {
            // Primary Key
            this.HasKey(t => t.MeetingID);

            // Properties
            this.Property(t => t.MeetingName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MeetingDescription)
                .HasMaxLength(50);

            this.Property(t => t.RepeatEndType)
                .HasMaxLength(10);

            this.Property(t => t.MonthDayOccurence)
                .HasMaxLength(20);

            this.Property(t => t.YearMonthDayOccurence)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Met00200");
            this.Property(t => t.MeetingID).HasColumnName("MeetingID");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.MeetingRoomID).HasColumnName("MeetingRoomID");
            this.Property(t => t.MeetingName).HasColumnName("MeetingName");
            this.Property(t => t.MeetingDescription).HasColumnName("MeetingDescription");
            this.Property(t => t.IsFullDay).HasColumnName("IsFullDay");
            this.Property(t => t.MeetingRepeatTypeID).HasColumnName("MeetingRepeatTypeID");
            this.Property(t => t.RepeatEvery).HasColumnName("RepeatEvery");
            this.Property(t => t.RepeatSun).HasColumnName("RepeatSun");
            this.Property(t => t.RepeatMon).HasColumnName("RepeatMon");
            this.Property(t => t.RepeatTus).HasColumnName("RepeatTus");
            this.Property(t => t.RepeatWed).HasColumnName("RepeatWed");
            this.Property(t => t.RepeatThur).HasColumnName("RepeatThur");
            this.Property(t => t.RepeatFrid).HasColumnName("RepeatFrid");
            this.Property(t => t.RepeatSat).HasColumnName("RepeatSat");
            this.Property(t => t.RepeatEndOn).HasColumnName("RepeatEndOn");
            this.Property(t => t.RepeatEndAfter).HasColumnName("RepeatEndAfter");
            this.Property(t => t.RepeatEndNever).HasColumnName("RepeatEndNever");
            this.Property(t => t.RepeatTypeID).HasColumnName("RepeatTypeID");
            this.Property(t => t.MonthRepeatWeekDay).HasColumnName("MonthRepeatWeekDay");
            this.Property(t => t.MonthRepeatOn).HasColumnName("MonthRepeatOn");
            this.Property(t => t.YearRepeatMonth).HasColumnName("YearRepeatMonth");
            this.Property(t => t.YearRepeatOn).HasColumnName("YearRepeatOn");
            this.Property(t => t.YearRepeatMonthDay).HasColumnName("YearRepeatMonthDay");
            this.Property(t => t.StartDateTime).HasColumnName("StartDateTime");
            this.Property(t => t.EndDateTime).HasColumnName("EndDateTime");
            this.Property(t => t.RepeatEndType).HasColumnName("RepeatEndType");
            this.Property(t => t.MonthDayOccurence).HasColumnName("MonthDayOccurence");
            this.Property(t => t.YearMonthDayOccurence).HasColumnName("YearMonthDayOccurence");
            this.Property(t => t.RecurrenceRule).HasColumnName("RecurrenceRule");

            // Relationships
            this.HasOptional(t => t.Met00005)
                .WithMany(t => t.Met00200)
                .HasForeignKey(d => d.MeetingRepeatTypeID);
            this.HasRequired(t => t.Met00204)
                .WithMany(t => t.Met00200)
                .HasForeignKey(d => d.MeetingRoomID);

        }
    }
}
