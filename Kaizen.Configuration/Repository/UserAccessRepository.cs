namespace Kaizen.Configuration.Repository
{
    public class UserAccessRepository : GenericDataRepository<KaizenUserRole>
    {
        public UserAccessRepository(string _UserName, string _UserPassword)
           : base(_UserName, _UserPassword)
        {
        }
    }
}
