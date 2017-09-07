using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00020Map : EntityTypeConfiguration<Kaizen00020>
    {
        public Kaizen00020Map()
        {
            // Primary Key
            this.HasKey(t => t.EmailID);

            // Properties
            this.Property(t => t.EmailID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.EmailIPassword)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IncomingProtocal)
                .HasMaxLength(50);

            this.Property(t => t.OutGoingProtocal)
                .HasMaxLength(50);

            this.Property(t => t.EmailTitle)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Kaizen00020");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.EmailIPassword).HasColumnName("EmailIPassword");
            this.Property(t => t.IncomingProtocal).HasColumnName("IncomingProtocal");
            this.Property(t => t.OutGoingProtocal).HasColumnName("OutGoingProtocal");
            this.Property(t => t.IsSSL).HasColumnName("IsSSL");
            this.Property(t => t.InComingPort).HasColumnName("InComingPort");
            this.Property(t => t.OutGoingPort).HasColumnName("OutGoingPort");
            this.Property(t => t.EmailTitle).HasColumnName("EmailTitle");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Kaizen00020)
                .HasForeignKey(d => d.UserName);

        }
    }
}
