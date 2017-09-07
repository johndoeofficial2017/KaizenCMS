using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00235Map : EntityTypeConfiguration<CM00235>
    {
        public CM00235Map()
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

            // Table & Column Mappings
            this.ToTable("CM00235");
            this.Property(t => t.TableID).HasColumnName("TableID");
            this.Property(t => t.MessageTRXID).HasColumnName("MessageTRXID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.IsSent).HasColumnName("IsSent");

            // Relationships
            this.HasRequired(t => t.CM00234)
                .WithMany(t => t.CM00235)
                .HasForeignKey(d => d.MessageTRXID);

        }
    }
}
