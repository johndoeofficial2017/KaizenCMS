namespace Kaizen.CMS.Repository
{
    public class CM_CaseNewForUploadingRepository : GenericDataRepository<CM_CaseNewForUploading>
    {
        public CM_CaseNewForUploadingRepository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
