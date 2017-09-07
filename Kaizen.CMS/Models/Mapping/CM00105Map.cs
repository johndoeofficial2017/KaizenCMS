using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00105Map : EntityTypeConfiguration<CM00105>
    {
        public CM00105Map()
        {
            // Primary Key
            this.HasKey(t => t.AgentID);

            // Properties
            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TargetID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AgentGroup)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SupervisorID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SuffixID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.MidName)
                .HasMaxLength(50);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .HasMaxLength(50);

            this.Property(t => t.EmailAddress)
                .HasMaxLength(100);

            this.Property(t => t.PhoneCodeArea)
                .HasMaxLength(5);

            this.Property(t => t.PhoneExtension)
                .HasMaxLength(12);

            this.Property(t => t.DirectPhon)
                .HasMaxLength(15);

            this.Property(t => t.SignatureExtension)
                .HasMaxLength(5);

            this.Property(t => t.PhotoExtension)
                .HasMaxLength(5);

            this.Property(t => t.UserCode)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AgentColor)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("CM00105");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.CalendarID).HasColumnName("CalendarID");
            this.Property(t => t.TargetID).HasColumnName("TargetID");
            this.Property(t => t.IsPercentTarget).HasColumnName("IsPercentTarget");
            this.Property(t => t.TargetTypeID).HasColumnName("TargetTypeID");
            this.Property(t => t.AgentGroup).HasColumnName("AgentGroup");
            this.Property(t => t.SupervisorID).HasColumnName("SupervisorID");
            this.Property(t => t.SuffixID).HasColumnName("SuffixID");
            this.Property(t => t.MidName).HasColumnName("MidName");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.GenderID).HasColumnName("GenderID");
            this.Property(t => t.Inactive).HasColumnName("Inactive");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.PhoneCodeArea).HasColumnName("PhoneCodeArea");
            this.Property(t => t.PhoneExtension).HasColumnName("PhoneExtension");
            this.Property(t => t.DirectPhon).HasColumnName("DirectPhon");
            this.Property(t => t.SignatureExtension).HasColumnName("SignatureExtension");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.UserCode).HasColumnName("UserCode");
            this.Property(t => t.WhereCondition).HasColumnName("WhereCondition");
            this.Property(t => t.AgentColor).HasColumnName("AgentColor");

            // Relationships
            this.HasOptional(t => t.CM00005)
                .WithMany(t => t.CM00105)
                .HasForeignKey(d => d.CalendarID);
            this.HasRequired(t => t.CM00023)
                .WithMany(t => t.CM00105)
                .HasForeignKey(d => d.AgentGroup);
        }
    }
}
