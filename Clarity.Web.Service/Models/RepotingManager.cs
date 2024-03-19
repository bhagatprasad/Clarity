using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Clarity.Web.Service.Models
{
    [Table("RepotingManager")]
    public class RepotingManager : Common
    {
        [Key]
        public long RepotingManagerId { get; set; }
        public long EmployeeId { get; set; }
        public long ManagerId { get; set; }

    }
}
