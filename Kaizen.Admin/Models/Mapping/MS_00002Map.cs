using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class MS_00002Map : EntityTypeConfiguration<MS_00002>
    {
        public MS_00002Map()
        {
            // Primary Key
            this.HasKey(t => t.MsgTemplateID);

            // Properties
            this.Property(t => t.MsgTemplateName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MS_00002");
            this.Property(t => t.MsgTemplateID).HasColumnName("MsgTemplateID");
            this.Property(t => t.MsgTemplateName).HasColumnName("MsgTemplateName");
            this.Property(t => t.MsgTemplateContant).HasColumnName("MsgTemplateContant");
        }
    }
}
