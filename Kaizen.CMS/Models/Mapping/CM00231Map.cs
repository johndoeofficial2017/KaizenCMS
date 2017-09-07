using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00231Map : EntityTypeConfiguration<CM00231>
    {
        public CM00231Map()
        {
            // Primary Key
            this.HasKey(t => t.TableID);

            // Properties
            this.Property(t => t.MessageTRXID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Email01)
                .HasMaxLength(50);

            this.Property(t => t.Email02)
                .HasMaxLength(50);

            this.Property(t => t.Email03)
                .HasMaxLength(50);

            this.Property(t => t.Email04)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00231");
            this.Property(t => t.TableID).HasColumnName("TableID");
            this.Property(t => t.MessageTRXID).HasColumnName("MessageTRXID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.IsSent).HasColumnName("IsSent");
            this.Property(t => t.Email01).HasColumnName("Email01");
            this.Property(t => t.Email02).HasColumnName("Email02");
            this.Property(t => t.Email03).HasColumnName("Email03");
            this.Property(t => t.Email04).HasColumnName("Email04");

            // Relationships
            this.HasRequired(t => t.CM00230)
                .WithMany(t => t.CM00231)
                .HasForeignKey(d => d.MessageTRXID);

        }
    }
}
