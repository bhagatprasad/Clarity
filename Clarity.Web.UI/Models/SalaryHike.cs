namespace Clarity.Web.UI.Models
{
    public class SalaryHike
    {
        public long EmployeeId { get; set; }
        public decimal? OrignalSalary { get; set; }
        public decimal? LatestSalary { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
