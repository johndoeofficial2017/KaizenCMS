namespace Kaizen.Configuration.Repository
{
    public class Met00101Repository : GenericDataRepository<Met00101>
    {
        public Met00101Repository()
           : base()     
        {
        }
        public Met00101Repository(string Met00101Name,string Met00101Password)
           : base(Met00101Name, Met00101Password)
        {
        }
    }
}
