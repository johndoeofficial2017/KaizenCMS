namespace Kaizen.Configuration.Repository
{
    public class KaizenTaskRepository : GenericDataRepository<KaizenTask>
    {
        public KaizenTaskRepository(string _UserName, string _UserPassword)
            : base(_UserName, _UserPassword)
        {
        }
    }
}
