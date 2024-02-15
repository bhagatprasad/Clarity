using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clarity.Web.Service.Models
{
    [Table("Roles")]
    public class Roles : Common
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
