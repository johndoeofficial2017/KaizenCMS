namespace Kaizen.Configuration.Repository
{
    public class Met00007Repository : GenericDataRepository<Met00007>
    {
        public Met00007Repository()
           : base()
        {
        }
        public Met00007Repository(string Met00007Name,string Met00007Password)
           : base(Met00007Name, Met00007Password)
        {
        }
    }
}
