namespace Clarity.Web.UI.Models
{
    public class EmployeeEmergencyContact
    {
        public long EmployeeEmergencyContactId { get; set; }
        public long? EmployeeId { get; set; }
        public string Name { get; set; }
        public string Relation { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
