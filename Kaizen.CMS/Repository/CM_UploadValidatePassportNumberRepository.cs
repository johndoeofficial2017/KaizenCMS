namespace Kaizen.CMS.Repository
{
    public class CM_UploadValidatePassportNumberRepository : GenericDataRepository<CM_UploadValidatePassportNumber>
    {
        public CM_UploadValidatePassportNumberRepository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
