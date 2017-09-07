using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00029Map : EntityTypeConfiguration<CM00029>
    {
        public CM00029Map()
        {
            // Primary Key
            this.HasKey(t => t.TRXTypeID);

            // Properties
            this.Property(t => t.TrxTypeName)
                .HasMaxLength(30);

            this.Property(t => t.NextDocumentNumber)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.NextNumberPrefix)
                .HasMaxLength(5);

            this.Property(t => t.CurrencyCode)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TableSource)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00029");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.TrxTypeName).HasColumnName("TrxTypeName");
            this.Property(t => t.IsCaseFixedSerial).HasColumnName("IsCaseFixedSerial");
            this.Property(t => t.NextDocumentNumber).HasColumnName("NextDocumentNumber");
            this.Property(t => t.NextNumberPrefix).HasColumnName("NextNumberPrefix");
            this.Property(t => t.NextNumberlenth).HasColumnName("NextNumberlenth");
            this.Property(t => t.StatusHTML).HasColumnName("StatusHTML");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.IsMultiply).HasColumnName("IsMultiply");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.TableSource).HasColumnName("TableSource");

            // Relationships
            this.HasRequired(t => t.CM00037)
                .WithMany(t => t.CM00029)
                .HasForeignKey(d => d.TableSource);

        }
    }
}
