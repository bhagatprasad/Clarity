namespace Clarity.Web.Service.Models
{
    public class EmployeePaymentModel
    {
        public long Id { get; set; }
        public long? EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public long? PaymentMethodId { get; set; }
        public string? PaymentMethodName  { get; set; }
        public long? PaymentTypeId { get; set; }
        public string? PaymentTypeName { get; set; }
        public decimal? Amount { get; set; }
        public string? PaymentMessage { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
