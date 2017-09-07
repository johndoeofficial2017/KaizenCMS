namespace Kaizen.Configuration.Repository
{
    public class Met00100Repository : GenericDataRepository<Met00100>
    {
        public Met00100Repository()
           : base()
        {
        }
        public Met00100Repository(string Met00100Name,string Met00100Password)
           : base(Met00100Name, Met00100Password)
        {
        }
    }
}
