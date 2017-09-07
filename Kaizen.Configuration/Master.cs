namespace Kaizen.Configuration
{
    public class Master
    {
    }
    public enum MenueType
    {
        Configuration = 1,
        Utility = 2, Transaction = 3, Common = 4, Administration = 5
    }
    public enum Screen
    {
        IV_Item = 1,
        IV_Adjust = 200,
        GLTransaction = 100,
        CMS_Case = 12,
        CMS_TRX_CaseUploading = 20,
        CMS_Trx_CasesAssignment = 21,
        CMS_Uti_Letter = 22,
        CMS_Uti_SMS = 23,

        CMS_Payment = 3,

        SOP_Invoice = 2,
        Customer = 4,
        Debtor = 6,
        Employee = 8,
        Vendor = 9,
        Client = 10,
        Agent = 11,
        POP_Order = 15,
        POP_Payment = 16,
        CMS_CaseAssign = 13
    }
    public enum CMS_Case
    {
        CaseRef = 1,
        TRXTypeID = 2,
        ContractID = 3,
        ClientID = 4,
        JecketsID = 5,
        DebtorID = 6,
        ProductID = 7,
        ActionPlanID = 8,
        CaseTreeID = 9,
        CaseAddess = 10,
        CaseContractDetail = 11,
        AddressCode = 12,
        CaseAccountNo = 13,
        ClosingDate = 14,
        TransactionDate = 15,
        BookingDate = 16,
        CaseStatusID = 17,
        Remarks = 18,
        OriginalAmount = 19,
        ClaimAmount = 20,
        PrincipleAmount = 21,
        CreatedDate = 22,
        AgentID = 23,
        AssignDate = 24
    }
    public enum CMS_CaseAssign
    {
        CaseRef = 1,
        AgentID = 2,
        AssignHistoryDate = 3
    }

    public enum Module
    {
        Financial = 1,
        FixedAssets = 2,
        Purchase = 3,
        Sales = 4,
        Inventory = 5,
        Project = 6,
        HumanResources = 7,
        CRM = 8,
        CollectionManagementSystem = 9,
        Admin = 10, Medical = 11
            , PBX = 12
            , Tools = 13
            , Extender = 14
    }
}
