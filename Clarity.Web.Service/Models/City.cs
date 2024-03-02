using System.ComponentModel.DataAnnotations.Schema;

namespace Clarity.Web.Service.Models
{
    [Table("City")]
    public class City : Common
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? ContryId { get; set; }
        public long? StateId { get; set; }
    }
}
