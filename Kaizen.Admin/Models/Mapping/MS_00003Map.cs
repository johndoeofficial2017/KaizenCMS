using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class MS_00003Map : EntityTypeConfiguration<MS_00003>
    {
        public MS_00003Map()
        {
            // Primary Key
            this.HasKey(t => t.DocumentID);

            // Properties
            this.Property(t => t.DocumentID)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MS_00003");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.MsgTemplateID).HasColumnName("MsgTemplateID");

            // Relationships
            this.HasRequired(t => t.MS_00002)
                .WithMany(t => t.MS_00003)
                .HasForeignKey(d => d.MsgTemplateID);

        }
    }
}
