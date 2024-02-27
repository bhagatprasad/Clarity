namespace Clarity.Web.Service.Models
{
    public class ApplicationUser
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long? DepartmentId { get; set; }
        public long? RoleId { get; set; }
    }
}
