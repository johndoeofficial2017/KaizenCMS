namespace Kaizen.SOP.Repository
{
    public class SOP00001Repository : GenericDataRepository<SOP00001>
    {
        public SOP00001Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
