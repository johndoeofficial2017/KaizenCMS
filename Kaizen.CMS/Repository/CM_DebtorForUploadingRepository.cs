namespace Kaizen.CMS.Repository
{
    public class CM_DebtorForUploadingRepository : GenericDataRepository<CM_DebtorForUploading>
    {
        public CM_DebtorForUploadingRepository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
