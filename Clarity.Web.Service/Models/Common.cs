namespace Clarity.Web.Service.Models
{
    public class Common
    {
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
