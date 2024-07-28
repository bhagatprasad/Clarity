using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clarity.Web.Service.Models
{
    [Table("EmployeePayment")]
    public class EmployeePayment : Common
    {
        [Key]
        public long Id { get; set; }
        public long? EmployeeId { get; set; }
        public long? TutionFeeId { get; set; }
        public long? PaymentMethodId { get; set; }
        public long? PaymentTypeId { get; set; }
        public decimal? Amount { get; set; }
        public string PaymentMessage { get; set; }
    }
}
