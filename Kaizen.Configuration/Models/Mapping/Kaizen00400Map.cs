using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00400Map : EntityTypeConfiguration<Kaizen00400>
    {
        public Kaizen00400Map()
        {
            // Primary Key
            this.HasKey(t => t.KaizenLineID);

            // Properties
            this.Property(t => t.KaizenMessageLine)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserNameTo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Kaizen00400");
            this.Property(t => t.KaizenLineID).HasColumnName("KaizenLineID");
            this.Property(t => t.KaizenMessageLine).HasColumnName("KaizenMessageLine");
            this.Property(t => t.UserNameTo).HasColumnName("UserNameTo");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.MessageDate).HasColumnName("MessageDate");
            this.Property(t => t.IsReceived).HasColumnName("IsReceived");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Kaizen00400)
                .HasForeignKey(d => d.UserName);

        }
    }
}
