using System.ComponentModel;

namespace Clarity.Web.UI.Utility
{
    public enum DocumentTypeEnum
    {
        OfferLetter = 1000,
        HikeLetter = 1001,
        FORM_16 = 1002,
        EmploymentContract = 1003,
        ResignationLetter = 1004,
        PromotionLetter = 1005,
        TerminationLetter = 1006,
        TrainingCertificate = 1007,
        ExperienceCertificate = 1008,
        NonDisclosureAgreement = 1009,
        PerformanceAppraisal = 1010,
        MedicalCertificate = 1011,
        InsurancePolicy = 1012,
        BankStatement = 1013,
        LoanAgreement = 1014,
        LeaseAgreement = 1015,
        PurchaseOrder = 1016,
        Invoice = 1017,
        TaxReturn = 1018,
        CreditReport = 1019
    }
    public enum EmployeeDocumentEnum
    {
        [Description("Offer Letter")]
        OfferLetter,

        [Description("Hike Letter")]
        HikeLetter,

        [Description("FORM 16")]
        FORM16,

        [Description("Employment Contract")]
        EmploymentContract,

        [Description("Resignation Letter")]
        ResignationLetter,

        [Description("Promotion Letter")]
        PromotionLetter,

        [Description("Termination Letter")]
        TerminationLetter,

        [Description("Training Certificate")]
        TrainingCertificate,

        [Description("Experience Certificate")]
        ExperienceCertificate,

        [Description("Non-Disclosure Agreement")]
        NonDisclosureAgreement,

        [Description("Performance Appraisal")]
        PerformanceAppraisal,

        [Description("Medical Certificate")]
        MedicalCertificate,

        [Description("Insurance Policy")]
        InsurancePolicy,

        [Description("Bank Statement")]
        BankStatement,

        [Description("Loan Agreement")]
        LoanAgreement,

        [Description("Lease Agreement")]
        LeaseAgreement,

        [Description("Purchase Order")]
        PurchaseOrder,

        [Description("Invoice")]
        Invoice,

        [Description("Tax Return")]
        TaxReturn,

        [Description("Credit Report")]
        CreditReport
    }
}
