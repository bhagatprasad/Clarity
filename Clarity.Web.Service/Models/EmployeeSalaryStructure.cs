using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clarity.Web.Service.Models
{
    [Table("EmployeeSalaryStructure")]
    public class EmployeeSalaryStructure
    {
        [Key]
        public long EmployeeSalaryStructureId { get; set; }
        public long? EmployeeId { get; set; }
        public string PAN { get; set; }
        public string Adhar { get; set; }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public string IFSC { get; set; }
        public decimal? BASIC { get; set; }
        public string UAN { get; set; }
        public string PFNO { get; set; }
        public decimal? HRA { get; set; }
        public decimal? CONVEYANCE { get; set; }
        public decimal? MEDICALALLOWANCE { get; set; }
        public decimal? SPECIALALLOWANCE { get; set; }
        public decimal? SPECIALBONUS { get; set; }
        public decimal? STATUTORYBONUS { get; set; }
        public decimal? OTHERS { get; set; }
        public decimal? PF { get; set; }
        public decimal? ESIC { get; set; }
        public decimal? PROFESSIONALTAX { get; set; }
        public decimal? GroupHealthInsurance { get; set; }
        public decimal? GROSSEARNINGS { get; set; }
        public decimal? GROSSDEDUCTIONS { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
