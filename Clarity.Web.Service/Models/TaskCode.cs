using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clarity.Web.Service.Models
{
    [Table("TaskCode")]
    public class TaskCode
    {
        [Key]
        public long TaskCodeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? TaskItemId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }

    }
}
