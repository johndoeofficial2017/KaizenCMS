namespace Kaizen.SOP.Repository
{
    public class SOP00501Repository : GenericDataRepository<SOP00501>
    {
        public SOP00501Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
