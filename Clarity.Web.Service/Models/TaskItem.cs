using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clarity.Web.Service.Models
{
    [Table("TaskItem")]
    public class TaskItem
    {
        [Key]
        public long TaskItemId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public virtual List<TaskCode> taskCodes { get; set; }
    }
}
