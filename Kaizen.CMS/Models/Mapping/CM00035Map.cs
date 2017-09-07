using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00035Map : EntityTypeConfiguration<CM00035>
    {
        public CM00035Map()
        {
            // Primary Key
            this.HasKey(t => t.TemplateID);

            // Properties
            this.Property(t => t.TemplateName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TemplateContant)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("CM00035");
            this.Property(t => t.TemplateID).HasColumnName("TemplateID");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.TemplateName).HasColumnName("TemplateName");
            this.Property(t => t.TemplateContant).HasColumnName("TemplateContant");
        }
    }
}
