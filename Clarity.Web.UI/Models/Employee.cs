namespace Clarity.Web.UI.Models
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Gender { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long? UserId { get; set; }
        public long? RoleId { get; set; }
        public long? DepartmentId { get; set; }
        public long? DesignationId { get; set; }
        public DateTimeOffset? StartedOn { get; set; }
        public DateTimeOffset? EndedOn { get; set; }
        public DateTimeOffset? ResignedOn { get; set; }
        public DateTimeOffset? LastWorkingDay { get; set; }
        public DateTimeOffset? OfferRelesedOn { get; set; }
        public DateTimeOffset? OfferAcceptedOn { get; set; }
        public decimal? OfferPrice { get; set; }
        public decimal? CurrentPrice { get; set; }
        public decimal? JoiningBonus { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
