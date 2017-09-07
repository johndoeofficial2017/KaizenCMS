using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00230Map : EntityTypeConfiguration<CM00230>
    {
        public CM00230Map()
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

            this.Property(t => t.EmailID)
                .HasMaxLength(50);

            this.Property(t => t.EmailIPassword)
                .HasMaxLength(50);

            this.Property(t => t.IncomingProtocal)
                .HasMaxLength(50);

            this.Property(t => t.OutGoingProtocal)
                .HasMaxLength(50);

            this.Property(t => t.EmailTitle)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00230");
            this.Property(t => t.MessageTRXID).HasColumnName("MessageTRXID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.TrxComment).HasColumnName("TrxComment");
            this.Property(t => t.TemplateID).HasColumnName("TemplateID");
            this.Property(t => t.TemplateContant).HasColumnName("TemplateContant");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.EmailIPassword).HasColumnName("EmailIPassword");
            this.Property(t => t.IncomingProtocal).HasColumnName("IncomingProtocal");
            this.Property(t => t.OutGoingProtocal).HasColumnName("OutGoingProtocal");
            this.Property(t => t.IsSSL).HasColumnName("IsSSL");
            this.Property(t => t.InComingPort).HasColumnName("InComingPort");
            this.Property(t => t.OutGoingPort).HasColumnName("OutGoingPort");
            this.Property(t => t.EmailTitle).HasColumnName("EmailTitle");
            this.Property(t => t.Email01).HasColumnName("Email01");
            this.Property(t => t.Email02).HasColumnName("Email02");
            this.Property(t => t.Email03).HasColumnName("Email03");
            this.Property(t => t.Email04).HasColumnName("Email04");

            // Relationships
            this.HasRequired(t => t.CM00030)
                .WithMany(t => t.CM00230)
                .HasForeignKey(d => d.TemplateID);

        }
    }
}
