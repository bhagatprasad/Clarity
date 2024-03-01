namespace Clarity.Web.UI.Models
{
    public class EmployeeEducation
    {
        public long EmployeeEducationId { get; set; }
        public long? EmployeeId { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public string Institution { get; set; }
        public DateTimeOffset? YearOfCompletion { get; set; }
        public string PercentageMarks { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
