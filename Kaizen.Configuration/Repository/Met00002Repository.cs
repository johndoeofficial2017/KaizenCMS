namespace Kaizen.Configuration.Repository
{
    public class Met00002Repository : GenericDataRepository<Met00002>
    {
        public Met00002Repository()
           : base()     
        {
        }
        public Met00002Repository(string Met00002Name,string Met00002Password)
           : base(Met00002Name, Met00002Password)
        {
        }
    }
}
