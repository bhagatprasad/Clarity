using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clarity.Web.Service.Models
{
    [Table("MessageType")]
    public class MessageType
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
