namespace Kaizen.SOP.Repository
{
    public class SOP10501Repository : GenericDataRepository<SOP10501>
    {
        public SOP10501Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
