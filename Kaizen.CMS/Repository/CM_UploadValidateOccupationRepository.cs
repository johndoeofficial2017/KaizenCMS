namespace Kaizen.CMS.Repository
{
    public class CM_UploadValidateOccupationRepository : GenericDataRepository<CM_UploadValidateOccupation>
    {
        public CM_UploadValidateOccupationRepository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
