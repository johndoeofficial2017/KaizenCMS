namespace Kaizen.Configuration.Repository
{
    public class Met00003Repository : GenericDataRepository<Met00003>
    {
        public Met00003Repository()
           : base()     
        {
        }
        public Met00003Repository(string Met00003Name,string Met00003Password)
           : base(Met00003Name, Met00003Password)
        {
        }
    }
}
