namespace Kaizen.Configuration.Repository
{
    public class Met00005Repository : GenericDataRepository<Met00005>
    {
        public Met00005Repository()
           : base()
        {
        }
        public Met00005Repository(string Met00005Name,string Met00005Password)
           : base(Met00005Name, Met00005Password)
        {
        }
    }
}
