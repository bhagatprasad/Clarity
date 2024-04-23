using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clarity.Web.Service.Models
{
    [Table("Timesheet")]
    public class Timesheet
    {
        [Key]
        public long Id { get; set; }
        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset? ToDate { get; set; }
        public string Description { get; set; }
        public long? EmployeeId { get; set; }
        public long? UserId { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? AssignedOn { get; set; }
        public long? AssignedTo { get; set; }
        public DateTimeOffset? ApprovedOn { get; set; }
        public long? ApprovedBy { get; set; }
        public string? ApprovedComments { get; set; }
        public DateTimeOffset? CancelledOn { get; set; }
        public long? CancelledBy { get; set; }
        public string? CancelledComments { get; set; }
        public DateTimeOffset? RejectedOn { get; set; }
        public long? RejectedBy { get; set; }
        public string? RejectedComments { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }

        public virtual List<TimesheetTask> timesheetTasks { get; set; }
    }
}
