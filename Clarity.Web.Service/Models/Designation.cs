using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clarity.Web.Service.Models
{
    [Table("Designation")]
    public class Designation : Common
    {

        [Key]
        public long DesignationId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
