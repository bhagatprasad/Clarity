using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clarity.Web.Service.Models
{
    [Table("TimesheetTask")]
    public class TimesheetTask
    {
        [Key]
        public long Id { get; set; }
        public long? TimesheetId { get; set; }
        public long? TaskItemId { get; set; }
        public long? TaskCodeId { get; set; }
        public long? MondayHours { get; set; }
        public long? TuesdayHours { get; set; }
        public long? WednesdayHours { get; set; }
        public long? ThursdayHours { get; set; }
        public long? FridayHours { get; set; }
        public long? SaturdayHours { get; set; }
        public long? SundayHours { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
