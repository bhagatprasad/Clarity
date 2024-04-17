using System.ComponentModel;

namespace Clarity.Web.Service.Helpers
{
    public enum EmployeeDocumentEnum 
    {
        [Description("All")]
        All,

        [Description("DocumentId")]
        DocumentId,

        [Description("ActiveDocumentType")]
        ActiveDocumentType,

        [Description("EmployeeLongDocumentTypeLong")]
        EmployeeLongDocumentTypeLong,

        [Description("EmployeeIdLongDocumentString")]
        EmployeeIdLongDocumentString,

        [Description("DocumentTypeStringUserIdLong")]
        DocumentTypeStringUserIdLong,

        [Description("DocumentTypeStringEmial")]
        DocumentTypeStringEmial,

        [Description("DocumentTypeString")]
        DocumentTypeString
    }
  
}
