namespace Kaizen.SOP
{
    public partial class IV00014
    {
        public int ListID { get; set; }
        public string ListName { get; set; }
        public string CollCode { get; set; }
        public virtual IV00013 IV00013 { get; set; }
    }
}
