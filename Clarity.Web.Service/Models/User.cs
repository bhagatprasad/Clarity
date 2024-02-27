using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clarity.Web.Service.Models
{
    [Table("User")]
    public class User : Common
    {
        [Key]
        public long Id { get; set; }
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
    }
}
