using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clarity.Web.Service.Models
{
    [Table("Department")]
    public class Department: Common
    {
        [Key]
        public long DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DepartmentCode { get; set; }
    }
}
