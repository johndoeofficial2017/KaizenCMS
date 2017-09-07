namespace Kaizen.SOP.Repository
{
    public class SOP00000Repository : GenericDataRepository<SOP00000>
    {
        public SOP00000Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
