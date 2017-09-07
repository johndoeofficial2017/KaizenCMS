using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class KaizenSessionFailMap : EntityTypeConfiguration<KaizenSessionFail>
    {
        public KaizenSessionFailMap()
        {
            // Primary Key
            this.HasKey(t => t.LineID);

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("KaizenSessionFail");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.LoginDate).HasColumnName("LoginDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.KaizenSessionFails)
                .HasForeignKey(d => d.UserName);

        }
    }
}
