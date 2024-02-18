using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clarity.Web.Service.Models
{
    [Table("State")]
    public class State : Common
    {
        [Key]
        public long StateId { get; set; }
        public long CountryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SateCode { get; set; }
        public string CountryCode { get; set; }
    }
}
