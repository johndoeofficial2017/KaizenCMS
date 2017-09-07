using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_LetterViewMap : EntityTypeConfiguration<CM_LetterView>
    {
        public CM_LetterViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.MessageTRXID, t.CreatedBy, t.CreatedDate, t.TemplateID, t.TemplateContant, t.AddressCode });

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

            this.Property(t => t.TemplateID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TemplateContant)
                .IsRequired();

            this.Property(t => t.AddressCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_LetterView");
            this.Property(t => t.MessageTRXID).HasColumnName("MessageTRXID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.TrxComment).HasColumnName("TrxComment");
            this.Property(t => t.TemplateID).HasColumnName("TemplateID");
            this.Property(t => t.TemplateContant).HasColumnName("TemplateContant");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.TemplateContanta).HasColumnName("TemplateContanta");
        }
    }
}
