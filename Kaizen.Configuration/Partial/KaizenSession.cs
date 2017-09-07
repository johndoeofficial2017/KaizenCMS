using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Configuration
{
    public partial class KaizenSession
    {
        [NotMapped]
        public string DashboardCode { get; set; }

        [NotMapped]
        public int Status { get; set; }
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string FirstName { get; set; }
        [NotMapped]
        public string LastName { get; set; }
        [NotMapped]
        public int UserRole { get; set; }
        [NotMapped]
        public string PhotoExtension { get; set; }
        [NotMapped]
        public List<string> ConnectionIds { get; set; } = new List<string>();

        [NotMapped]
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(this.FirstName) & string.IsNullOrEmpty(this.LastName))
                    return this.UserName;
                return (string.IsNullOrEmpty(this.FirstName) ? "" : this.FirstName) + " " +
                    (string.IsNullOrEmpty(this.LastName) ? "" : this.LastName);
            }
        }
        [NotMapped]
        public Screen CurrentScreen { get; set; }
        [NotMapped]
        public int KaizenID { get; set; }
        [NotMapped]
        public string TransactionID { get; set; }
        [NotMapped]
        public List<Screen> Screens { get; set; }
        [NotMapped]
        public string CompanyName { get; set; }
        [NotMapped]
        public string SegmentMark { get; set; }
        [NotMapped]
        public string GLFormat { get; set; }
        [NotMapped]
        public Nullable<int> YearCode { get; set; }
        [NotMapped]
        public string CurrencyCode { get; set; }
        [NotMapped]
        public Nullable<byte> DecimalDigit { get; set; }
        [NotMapped]
        public string ExchangeTableID { get; set; }
        [NotMapped]
        public Nullable<bool> IsMultiply { get; set; }
        [NotMapped]
        public Nullable<int> ExchangeRateID { get; set; }
        [NotMapped]
        public Nullable<double> ExchangeRate { get; set; }
        [NotMapped]
        public Nullable<System.DateTime> CurrentDate { get; set; }
        [NotMapped]
        public string CheckbookCode { get; set; }
        [NotMapped]
        public string SOPTypeSetupID { get; set; }
        [NotMapped]
        public Nullable<byte> SOPTYPE { get; set; }
        [NotMapped]
        public string SiteID { get; set; }
        [NotMapped]
        public string AgentID { get; set; }
    }
}
