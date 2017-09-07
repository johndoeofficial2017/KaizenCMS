using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00234Map : EntityTypeConfiguration<CM00234>
    {
        public CM00234Map()
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

            // Table & Column Mappings
            this.ToTable("CM00234");
            this.Property(t => t.MessageTRXID).HasColumnName("MessageTRXID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.TrxComment).HasColumnName("TrxComment");
            this.Property(t => t.TemplateID).HasColumnName("TemplateID");
            this.Property(t => t.TemplateContant).HasColumnName("TemplateContant");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");

            // Relationships
            this.HasRequired(t => t.CM00036)
                .WithMany(t => t.CM00234)
                .HasForeignKey(d => d.TemplateID);

        }
    }
}
