namespace Kaizen.Configuration.Repository
{
    public class KaizenUserRoleRepository : GenericDataRepository<KaizenUserRole>
    {
        public KaizenUserRoleRepository(string _UserName, string _UserPassword)
            : base(_UserName, _UserPassword)
        {
        }
    }
}
