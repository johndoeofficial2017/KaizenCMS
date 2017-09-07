using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Company
    {
        public Company()
        {
            this.CompanyAccesses = new List<CompanyAccess>();
            this.CompanySegments = new List<CompanySegment>();
            this.DT00100 = new List<DT00100>();
            this.Kaizen00011 = new List<Kaizen00011>();
            this.Kaizen00040 = new List<Kaizen00040>();
            this.Kaizen00050 = new List<Kaizen00050>();
            this.Kaizen00101 = new List<Kaizen00101>();
            this.KaizenSequences = new List<KaizenSequence>();
        }

        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string SegmentMark { get; set; }
        public string GLFormat { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<byte> DecimalDigit { get; set; }
        public string ExchangeTableID { get; set; }
        public Nullable<bool> IsMultiply { get; set; }
        public Nullable<int> ExchangeRateID { get; set; }
        public Nullable<double> ExchangeRate { get; set; }
        public Nullable<int> YearCode { get; set; }
        public Nullable<System.DateTime> CurrentDate { get; set; }
        public string CheckbookCode { get; set; }
        public string SOPTypeSetupID { get; set; }
        public Nullable<byte> SOPTYPE { get; set; }
        public string SiteID { get; set; }
        public virtual ICollection<CompanyAccess> CompanyAccesses { get; set; }
        public virtual ICollection<CompanySegment> CompanySegments { get; set; }
        public virtual Config00100 Config00100 { get; set; }
        public virtual ICollection<DT00100> DT00100 { get; set; }
        public virtual ICollection<Kaizen00011> Kaizen00011 { get; set; }
        public virtual ICollection<Kaizen00040> Kaizen00040 { get; set; }
        public virtual ICollection<Kaizen00050> Kaizen00050 { get; set; }
        public virtual ICollection<Kaizen00101> Kaizen00101 { get; set; }
        public virtual ICollection<KaizenSequence> KaizenSequences { get; set; }
    }
}
