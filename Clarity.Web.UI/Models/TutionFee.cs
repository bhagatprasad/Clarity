namespace Clarity.Web.UI.Models
{
    public class TutionFee
    {
        public long Id { get; set; }
        public long? EmployeeId { get; set; }
        public decimal? ActualFee { get; set; }
        public decimal? FinalFee { get; set; }
        public decimal? RemaingFee { get; set; }
        public decimal? PaidFee { get; set; }
    }
}
