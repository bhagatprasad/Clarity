namespace Clarity.Web.UI.Models
{
    public class TaskItem
    {
        public long TaskItemId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public List<TaskCode> taskCodes { get; set; }
    }
}
