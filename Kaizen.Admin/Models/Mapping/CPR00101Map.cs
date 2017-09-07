using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class CPR00101Map : EntityTypeConfiguration<CPR00101>
    {
        public CPR00101Map()
        {
            // Primary Key
            this.HasKey(t => t.DebtorID);

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.AddressArabic)
                .HasMaxLength(1000);

            this.Property(t => t.FirstNameArabic)
                .HasMaxLength(200);

            this.Property(t => t.LastNameArabic)
                .HasMaxLength(200);

            this.Property(t => t.MiddleName1Arabic)
                .HasMaxLength(200);

            this.Property(t => t.MiddleName2Arabic)
                .HasMaxLength(200);

            this.Property(t => t.MiddleName3Arabic)
                .HasMaxLength(200);

            this.Property(t => t.MiddleName4Arabic)
                .HasMaxLength(200);

            this.Property(t => t.BloodGroup)
                .HasMaxLength(200);

            this.Property(t => t.BuildingAlphaArabic)
                .HasMaxLength(200);

            this.Property(t => t.EmployerName1Arabic)
                .HasMaxLength(200);

            this.Property(t => t.OccupationArabic)
                .HasMaxLength(200);

            this.Property(t => t.SponserNameArabic)
                .HasMaxLength(200);

            this.Property(t => t.RoadNameArabic)
                .HasMaxLength(200);

            this.Property(t => t.BlockNameArabic)
                .HasMaxLength(200);

            this.Property(t => t.GovernorateNameArabic)
                .HasMaxLength(200);

            this.Property(t => t.LatestEducationDegreeArabic)
                .HasMaxLength(200);

            this.Property(t => t.OccupationDescription1Arabic)
                .HasMaxLength(200);

            this.Property(t => t.SponsorNameArabic)
                .HasMaxLength(200);

            this.Property(t => t.ClearingAgentIndicator)
                .HasMaxLength(200);

            this.Property(t => t.LfpNameArabic)
                .HasMaxLength(200);

            this.Property(t => t.ArabicCountryName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("CPR00101");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.AddressArabic).HasColumnName("AddressArabic");
            this.Property(t => t.FirstNameArabic).HasColumnName("FirstNameArabic");
            this.Property(t => t.LastNameArabic).HasColumnName("LastNameArabic");
            this.Property(t => t.MiddleName1Arabic).HasColumnName("MiddleName1Arabic");
            this.Property(t => t.MiddleName2Arabic).HasColumnName("MiddleName2Arabic");
            this.Property(t => t.MiddleName3Arabic).HasColumnName("MiddleName3Arabic");
            this.Property(t => t.MiddleName4Arabic).HasColumnName("MiddleName4Arabic");
            this.Property(t => t.BloodGroup).HasColumnName("BloodGroup");
            this.Property(t => t.BuildingAlphaArabic).HasColumnName("BuildingAlphaArabic");
            this.Property(t => t.EmployerName1Arabic).HasColumnName("EmployerName1Arabic");
            this.Property(t => t.OccupationArabic).HasColumnName("OccupationArabic");
            this.Property(t => t.SponserNameArabic).HasColumnName("SponserNameArabic");
            this.Property(t => t.RoadNameArabic).HasColumnName("RoadNameArabic");
            this.Property(t => t.BlockNameArabic).HasColumnName("BlockNameArabic");
            this.Property(t => t.GovernorateNameArabic).HasColumnName("GovernorateNameArabic");
            this.Property(t => t.LatestEducationDegreeArabic).HasColumnName("LatestEducationDegreeArabic");
            this.Property(t => t.OccupationDescription1Arabic).HasColumnName("OccupationDescription1Arabic");
            this.Property(t => t.SponsorNameArabic).HasColumnName("SponsorNameArabic");
            this.Property(t => t.ClearingAgentIndicator).HasColumnName("ClearingAgentIndicator");
            this.Property(t => t.LfpNameArabic).HasColumnName("LfpNameArabic");
            this.Property(t => t.ArabicCountryName).HasColumnName("ArabicCountryName");

            // Relationships
            this.HasRequired(t => t.CPR00100)
                .WithOptional(t => t.CPR00101);

        }
    }
}
