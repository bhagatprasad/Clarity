namespace Clarity.Web.UI.Models
{
    public class MonthlySalary
    {
        public long MonthlySalaryId { get; set; }
        public string Title { get; set; }
        public string SalaryMonth { get; set; }
        public string SalaryYear { get; set; }
        public string Location { get; set; }
        public int StdDays { get; set; }
        public int WrkDays { get; set; }
        public int LopDays { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
