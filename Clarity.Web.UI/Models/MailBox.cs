namespace Clarity.Web.UI.Models
{
    public class MailBox
    {
        public long MailBoxId { get; set; }

        public long? MessageTypeId { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string Message { get; set; }

        public string HTMLMessage { get; set; }

        public bool IsForAll { get; set; }

        public string FromUser { get; set; }

        public string ToUser { get; set; }

        public long? CreatedBy { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public bool? IsActive { get; set; }

        public virtual MessageType messageType { get; set; }
    }
}
