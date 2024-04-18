namespace Clarity.Web.Service.Models
{
    public class ResetPassword
    {
        public long UserId { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
