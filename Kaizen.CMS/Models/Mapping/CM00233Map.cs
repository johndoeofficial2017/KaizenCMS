using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00233Map : EntityTypeConfiguration<CM00233>
    {
        public CM00233Map()
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

            this.Property(t => t.MobileNo1)
                .HasMaxLength(50);

            this.Property(t => t.MobileNo2)
                .HasMaxLength(50);

            this.Property(t => t.MobileNo3)
                .HasMaxLength(50);

            this.Property(t => t.MobileNo4)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00233");
            this.Property(t => t.TableID).HasColumnName("TableID");
            this.Property(t => t.MessageTRXID).HasColumnName("MessageTRXID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.IsSent).HasColumnName("IsSent");
            this.Property(t => t.MobileNo1).HasColumnName("MobileNo1");
            this.Property(t => t.MobileNo2).HasColumnName("MobileNo2");
            this.Property(t => t.MobileNo3).HasColumnName("MobileNo3");
            this.Property(t => t.MobileNo4).HasColumnName("MobileNo4");

            // Relationships
            this.HasRequired(t => t.CM00232)
                .WithMany(t => t.CM00233)
                .HasForeignKey(d => d.MessageTRXID);

        }
    }
}
