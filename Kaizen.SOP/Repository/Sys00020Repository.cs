namespace Kaizen.SOP.Repository
{
    public class Sys00020Repository : GenericDataRepository<Sys00020>
    {
        public Sys00020Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
