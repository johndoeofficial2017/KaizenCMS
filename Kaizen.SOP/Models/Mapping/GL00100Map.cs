using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class GL00100Map : EntityTypeConfiguration<GL00100>
    {
        public GL00100Map()
        {
            // Primary Key
            this.HasKey(t => t.AccountID);

            // Properties
            this.Property(t => t.ACTNUMBR)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.AccountName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.AccountAlias)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ACTNUMBR_1)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ACTNUMBR_2)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ACTNUMBR_3)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ACTNUMBR_4)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ACTNUMBR_5)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ACTNUMBR_6)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ACTNUMBR_7)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ACTNUMBR_8)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ACTNUMBR_9)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ACTNUMBR_10)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ACTNUMBR_16)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ACTNUMBR_12)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ACTNUMBR_13)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ACTNUMBR_14)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ACTNUMBR_15)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("GL00100");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.ACTNUMBR).HasColumnName("ACTNUMBR");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.AccountName).HasColumnName("AccountName");
            this.Property(t => t.AccountAlias).HasColumnName("AccountAlias");
            this.Property(t => t.IsPL).HasColumnName("IsPL");
            this.Property(t => t.IsDebit).HasColumnName("IsDebit");
            this.Property(t => t.InSales).HasColumnName("InSales");
            this.Property(t => t.InPurchase).HasColumnName("InPurchase");
            this.Property(t => t.InInventory).HasColumnName("InInventory");
            this.Property(t => t.InPayroll).HasColumnName("InPayroll");
            this.Property(t => t.InManufacturing).HasColumnName("InManufacturing");
            this.Property(t => t.InExpenseManagement).HasColumnName("InExpenseManagement");
            this.Property(t => t.InPOS).HasColumnName("InPOS");
            this.Property(t => t.Inbank).HasColumnName("Inbank");
            this.Property(t => t.IsAllowAccountEntry).HasColumnName("IsAllowAccountEntry");
            this.Property(t => t.Inactive).HasColumnName("Inactive");
            this.Property(t => t.ACTNUMBR_1).HasColumnName("ACTNUMBR_1");
            this.Property(t => t.ACTNUMBR_2).HasColumnName("ACTNUMBR_2");
            this.Property(t => t.ACTNUMBR_3).HasColumnName("ACTNUMBR_3");
            this.Property(t => t.ACTNUMBR_4).HasColumnName("ACTNUMBR_4");
            this.Property(t => t.ACTNUMBR_5).HasColumnName("ACTNUMBR_5");
            this.Property(t => t.ACTNUMBR_6).HasColumnName("ACTNUMBR_6");
            this.Property(t => t.ACTNUMBR_7).HasColumnName("ACTNUMBR_7");
            this.Property(t => t.ACTNUMBR_8).HasColumnName("ACTNUMBR_8");
            this.Property(t => t.ACTNUMBR_9).HasColumnName("ACTNUMBR_9");
            this.Property(t => t.ACTNUMBR_10).HasColumnName("ACTNUMBR_10");
            this.Property(t => t.ACTNUMBR_16).HasColumnName("ACTNUMBR_16");
            this.Property(t => t.ACTNUMBR_12).HasColumnName("ACTNUMBR_12");
            this.Property(t => t.ACTNUMBR_13).HasColumnName("ACTNUMBR_13");
            this.Property(t => t.ACTNUMBR_14).HasColumnName("ACTNUMBR_14");
            this.Property(t => t.ACTNUMBR_15).HasColumnName("ACTNUMBR_15");

          

        }
    }
}
