namespace Kaizen.Configuration.Repository
{
    public class KaizenAuditRepository : GenericDataRepository<KaizenAudit>
    {
        public KaizenAuditRepository(string _UserName, string _UserPassword)
           : base(_UserName, _UserPassword)
        {
        }
    }
}
