namespace Kaizen.SOP.Repository
{
    public class SOP10200Repository : GenericDataRepository<SOP10200>
    {
        public SOP10200Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
