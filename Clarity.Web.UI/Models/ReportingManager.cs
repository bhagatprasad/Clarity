namespace Clarity.Web.UI.Models
{
    public class ReportingManager 
    {
        public long RepotingManagerId { get; set; }
        public long EmployeeId { get; set; }
        public long ManagerId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
