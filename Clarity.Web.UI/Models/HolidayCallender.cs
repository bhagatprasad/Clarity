namespace Clarity.Web.UI.Models
{
    public class HolidayCallender
    {
        public long Id { get; set; }
        public string FestivalName { get; set; }
        public DateTimeOffset? HolidayDate { get; set; }
        public int? Year { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
