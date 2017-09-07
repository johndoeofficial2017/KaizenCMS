namespace Kaizen.Configuration.Repository
{
    public class Met00102Repository : GenericDataRepository<Met00102>
    {
        public Met00102Repository()
           : base()     
        {
        }
        public Met00102Repository(string Met00102Name,string Met00102Password)
           : base(Met00102Name, Met00102Password)
        {
        }
    }
}
