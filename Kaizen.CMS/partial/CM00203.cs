using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.CMS
{
    public partial class CM00203
    {

        [NotMapped]
        public CM00104 Address { get; set; }
        [NotMapped]
        public bool IsSeleceted { get; set; }
        [NotMapped]
        public int PaymentProgress
        {
            get
            {
                double tempTotalCallactedAMT = 0;
                if (TotalCallactedAMT.HasValue)
                    tempTotalCallactedAMT = TotalCallactedAMT.Value;
                if (!FinanceCharge.HasValue)
                    FinanceCharge = 0;
                return (int)(tempTotalCallactedAMT / (ClaimAmount + FinanceCharge) * 100);
            }
        }
        [NotMapped]
        public double outstandingAMT
        {
            get
            {
                if (!TotalCallactedAMT.HasValue)
                    TotalCallactedAMT = 0;
                if (!FinanceCharge.HasValue)
                    FinanceCharge = 0;
                return (ClaimAmount + FinanceCharge.Value) - TotalCallactedAMT.Value;
            }
        }
        [NotMapped]
        public double Balance
        {
            get
            {
                if (!FinanceCharge.HasValue)
                    FinanceCharge = 0;
                if (!WriteOffAMT.HasValue)
                    WriteOffAMT = 0;
                double tempClaimAmount = ClaimAmount + FinanceCharge.Value;
                tempClaimAmount = tempClaimAmount - WriteOffAMT.Value;
                return this.TotalCallactedAMT.HasValue ? tempClaimAmount - TotalCallactedAMT.Value: tempClaimAmount;
            }
        }
        [NotMapped]
        public int CaseDay
        {
            get
            {
                return this.BookingDate.Day;
            }
        }
        [NotMapped]
        public int CaseMonth
        {
            get
            {
                return this.BookingDate.Month;
            }
        }
        [NotMapped]
        public int CaseYear
        {
            get
            {
                return this.BookingDate.Year;
            }
        }
    }
}
