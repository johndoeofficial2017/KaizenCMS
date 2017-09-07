using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class MS_00200Map : EntityTypeConfiguration<MS_00200>
    {
        public MS_00200Map()
        {
            // Primary Key
            this.HasKey(t => t.TrxID);

            // Properties
            this.Property(t => t.TrxID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.MsgTemplateName)
                .HasMaxLength(50);

            this.Property(t => t.EmailID)
                .HasMaxLength(50);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("MS_00200");
            this.Property(t => t.TrxID).HasColumnName("TrxID");
            this.Property(t => t.MsgTemplateID).HasColumnName("MsgTemplateID");
            this.Property(t => t.MsgTemplateContant).HasColumnName("MsgTemplateContant");
            this.Property(t => t.MsgTemplateName).HasColumnName("MsgTemplateName");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.EntryDate).HasColumnName("EntryDate");
            this.Property(t => t.IsSent).HasColumnName("IsSent");

            // Relationships
            this.HasRequired(t => t.MS_00002)
                .WithMany(t => t.MS_00200)
                .HasForeignKey(d => d.MsgTemplateID);

        }
    }
}
