namespace Kaizen.SOP.Repository
{
    public class SOP10500Repository : GenericDataRepository<SOP10500>
    {
        public SOP10500Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
