using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clarity.Web.Service.Models
{
    [Table("TutionFee")]
    public class TutionFee : Common
    {
        [Key]
        public long Id { get; set; }
        public long? EmployeeId { get; set; }
        public decimal? ActualFee { get; set; }
        public decimal? FinalFee { get; set; }
        public decimal? RemaingFee { get; set; }
        public decimal? PaidFee { get; set; }
    }
}
