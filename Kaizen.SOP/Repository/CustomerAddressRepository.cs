namespace Kaizen.SOP.Repository
{
    public class SOP00101Repository : GenericDataRepository<SOP00101>
    {
        public SOP00101Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
