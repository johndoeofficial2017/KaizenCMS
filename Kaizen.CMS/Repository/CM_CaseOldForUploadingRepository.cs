namespace Kaizen.CMS.Repository
{
    public class CM_CaseOldForUploadingRepository : GenericDataRepository<CM_CaseOldForUploading>
    {
        public CM_CaseOldForUploadingRepository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
