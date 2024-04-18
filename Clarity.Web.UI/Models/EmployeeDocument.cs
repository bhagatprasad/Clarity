namespace Clarity.Web.UI.Models
{
    public class EmployeeDocument
    {

        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public long DocumentTypeId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public string DocumentExtension { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
