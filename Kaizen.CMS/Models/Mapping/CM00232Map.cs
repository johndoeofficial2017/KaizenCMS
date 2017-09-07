using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00232Map : EntityTypeConfiguration<CM00232>
    {
        public CM00232Map()
        {
            // Primary Key
            this.HasKey(t => t.MessageTRXID);

            // Properties
            this.Property(t => t.MessageTRXID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TrxComment)
                .HasMaxLength(500);

            this.Property(t => t.TemplateContant)
                .IsRequired();

            this.Property(t => t.AddressCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SMSAccountCode)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00232");
            this.Property(t => t.MessageTRXID).HasColumnName("MessageTRXID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.TrxComment).HasColumnName("TrxComment");
            this.Property(t => t.TemplateID).HasColumnName("TemplateID");
            this.Property(t => t.TemplateContant).HasColumnName("TemplateContant");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.SMSAccountCode).HasColumnName("SMSAccountCode");
            this.Property(t => t.MobileNo1).HasColumnName("MobileNo1");
            this.Property(t => t.MobileNo2).HasColumnName("MobileNo2");
            this.Property(t => t.MobileNo3).HasColumnName("MobileNo3");
            this.Property(t => t.MobileNo4).HasColumnName("MobileNo4");

            // Relationships
            this.HasRequired(t => t.CM00035)
                .WithMany(t => t.CM00232)
                .HasForeignKey(d => d.TemplateID);
        }
    }
}
