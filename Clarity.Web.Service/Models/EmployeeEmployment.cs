using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clarity.Web.Service.Models
{
    [Table("EmployeeEmployment")]
    public class EmployeeEmployment
    {
        [Key]
        public long EmployeeEmploymentId { get; set; }
        public long? EmployeeId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Designation { get; set; }
        public DateTimeOffset? StartedOn { get; set; }
        public DateTimeOffset? EndedOn { get; set; }
        public string Reason { get; set; }
        public string ReportingManager { get; set; }
        public string HREmail { get; set; }
        public string Reference { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
