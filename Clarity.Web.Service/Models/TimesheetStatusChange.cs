namespace Clarity.Web.Service.Models
{
    public class TimesheetStatusChange
    {
        public long TimesheetId { get; set; }
        public string? ChangeType { get; set; }
        public string? Status { get; set; }
        public string? Comment { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
