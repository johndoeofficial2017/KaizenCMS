using System.ComponentModel.DataAnnotations.Schema;
namespace Kaizen.SOP
{
    public partial class SOP00100
    {
        [NotMapped]
        public SOP00101 MainAddress { get; set; }
        [NotMapped]
        public SOP00101 ShipAddress { get; set; }
        [NotMapped]
        public SOP00101 BillAddress { get; set; }
        [NotMapped]
        public SOP00101 StatementAddress { get; set; }
        
        [NotMapped]
        public string ShipToAddressName { get; set; }
        [NotMapped]
        public string ShipToSiteID { get; set; }
        [NotMapped]
        public string ShipToPnone01 { get; set; }
        [NotMapped]
        public string ShipToPnone02 { get; set; }
        [NotMapped]
        public string ShipToPnone03 { get; set; }
        [NotMapped]
        public string ShipToPnone04 { get; set; }
        [NotMapped]
        public string ShipToMobileNo1 { get; set; }
        [NotMapped]
        public string ShipToMobileNo2 { get; set; }
        [NotMapped]
        public string ShipToMobileNo3 { get; set; }
        [NotMapped]
        public string ShipToMobileNo4 { get; set; }
        [NotMapped]
        public string ShipToPOBox { get; set; }
        [NotMapped]
        public string ShipToOther01 { get; set; }
        [NotMapped]
        public string ShipToOther02 { get; set; }
        [NotMapped]
        public string ShipToOther03 { get; set; }
        [NotMapped]
        public string ShipToOther04 { get; set; }
        [NotMapped]
        public string ShipToAddress1 { get; set; }
        [NotMapped]
        public string ShipToAddress2 { get; set; }
        [NotMapped]
        public string ShipToAddress3 { get; set; }
        [NotMapped]
        public string ShipToAddress4 { get; set; }
        [NotMapped]
        public string ShipToEmail01 { get; set; }
        [NotMapped]
        public string ShipToEmail02 { get; set; }
        [NotMapped]
        public string ShipToEmail03 { get; set; }
        [NotMapped]
        public string ShipToEmail04 { get; set; }
        [NotMapped]
        public string BillToAddressName { get; set; }
        [NotMapped]
        public string BillToSiteID { get; set; }
        [NotMapped]
        public string BillToPnone01 { get; set; }
        [NotMapped]
        public string BillToPnone02 { get; set; }
        [NotMapped]
        public string BillToPnone03 { get; set; }
        [NotMapped]
        public string BillToPnone04 { get; set; }
        [NotMapped]
        public string BillToMobileNo1 { get; set; }
        [NotMapped]
        public string BillToMobileNo2 { get; set; }
        [NotMapped]
        public string BillToMobileNo3 { get; set; }
        [NotMapped]
        public string BillToMobileNo4 { get; set; }
        [NotMapped]
        public string BillToPOBox { get; set; }
        [NotMapped]
        public string BillToOther01 { get; set; }
        [NotMapped]
        public string BillToOther02 { get; set; }
        [NotMapped]
        public string BillToOther03 { get; set; }
        [NotMapped]
        public string BillToOther04 { get; set; }
        [NotMapped]
        public string BillToAddress1 { get; set; }
        [NotMapped]
        public string BillToAddress2 { get; set; }
        [NotMapped]
        public string BillToAddress3 { get; set; }
        [NotMapped]
        public string BillToAddress4 { get; set; }
        [NotMapped]
        public string BillToEmail01 { get; set; }
        [NotMapped]
        public string BillToEmail02 { get; set; }
        [NotMapped]
        public string BillToEmail03 { get; set; }
        [NotMapped]
        public string BillToEmail04 { get; set; }
        [NotMapped]
        public string StatementToAddressName { get; set; }
        [NotMapped]
        public string StatementToSiteID { get; set; }
        [NotMapped]
        public string StatementToPnone01 { get; set; }
        [NotMapped]
        public string StatementToPnone02 { get; set; }
        [NotMapped]
        public string StatementToPnone03 { get; set; }
        [NotMapped]
        public string StatementToPnone04 { get; set; }
        [NotMapped]
        public string StatementToMobileNo1 { get; set; }
        [NotMapped]
        public string StatementToMobileNo2 { get; set; }
        [NotMapped]
        public string StatementToMobileNo3 { get; set; }
        [NotMapped]
        public string StatementToMobileNo4 { get; set; }
        [NotMapped]
        public string StatementToPOBox { get; set; }
        [NotMapped]
        public string StatementToOther01 { get; set; }
        [NotMapped]
        public string StatementToOther02 { get; set; }
        [NotMapped]
        public string StatementToOther03 { get; set; }
        [NotMapped]
        public string StatementToOther04 { get; set; }
        [NotMapped]
        public string StatementToAddress1 { get; set; }
        [NotMapped]
        public string StatementToAddress2 { get; set; }
        [NotMapped]
        public string StatementToAddress3 { get; set; }
        [NotMapped]
        public string StatementToAddress4 { get; set; }
        [NotMapped]
        public string StatementToEmail01 { get; set; }
        [NotMapped]
        public string StatementToEmail02 { get; set; }
        [NotMapped]
        public string StatementToEmail03 { get; set; }
        [NotMapped]
        public string StatementToEmail04 { get; set; }
        [NotMapped]
        public string AddressName { get; set; }
        [NotMapped]
        public string SiteID { get; set; }
        [NotMapped]
        public string Pnone01 { get; set; }
        [NotMapped]
        public string Pnone02 { get; set; }
        [NotMapped]
        public string Pnone03 { get; set; }
        [NotMapped]
        public string Pnone04 { get; set; }
        [NotMapped]
        public string MobileNo1 { get; set; }
        [NotMapped]
        public string MobileNo2 { get; set; }
        [NotMapped]
        public string MobileNo3 { get; set; }
        [NotMapped]
        public string MobileNo4 { get; set; }
        [NotMapped]
        public string POBox { get; set; }
        [NotMapped]
        public string Other01 { get; set; }
        [NotMapped]
        public string Other02 { get; set; }
        [NotMapped]
        public string Other03 { get; set; }
        [NotMapped]
        public string Other04 { get; set; }
        [NotMapped]
        public string Address1 { get; set; }
        [NotMapped]
        public string Address2 { get; set; }
        [NotMapped]
        public string Address3 { get; set; }
        [NotMapped]
        public string Address4 { get; set; }
        [NotMapped]
        public string Email01 { get; set; }
        [NotMapped]
        public string Email02 { get; set; }
        [NotMapped]
        public string Email03 { get; set; }
        [NotMapped]
        public string Email04 { get; set; }
    }
}
