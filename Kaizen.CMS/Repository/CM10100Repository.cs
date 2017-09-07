namespace Kaizen.CMS.Repository
{
    public class CM10100Repository : GenericDataRepository<CM10100>
    {
        public CM10100Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
