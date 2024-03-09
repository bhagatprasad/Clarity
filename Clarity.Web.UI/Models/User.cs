namespace Clarity.Web.UI.Models
{
    public class User
    {
        public long Id { get; set; }
        public long? EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long? DepartmentId { get; set; }
        public long? RoleId { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTimeOffset? PasswordlastChangedOn { get; set; }
        public long? PasswordLastChangedBY { get; set; }
        public int UserWorngPasswordCount { get; set; }
        public DateTimeOffset? UserLastWrongPasswordOn { get; set; }
        public bool IsBlocked { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
