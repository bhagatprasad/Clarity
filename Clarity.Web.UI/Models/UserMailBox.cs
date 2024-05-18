namespace Clarity.Web.UI.Models
{
    public class UserMailBox
    {
        public long UserMailBoxId { get; set; }
        public long? UserId { get; set; }
        public long? MailBoxId { get; set; }
        public bool IsRead { get; set; }
        public long? ReadOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public virtual MailBox mailBox { get; set; }
    }
}
