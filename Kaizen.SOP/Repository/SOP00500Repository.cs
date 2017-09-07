namespace Kaizen.SOP.Repository
{
    public class SOP00500Repository : GenericDataRepository<SOP00500>
    {
        public SOP00500Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
