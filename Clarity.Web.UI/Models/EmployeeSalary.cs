namespace Clarity.Web.UI.Models
{
    public class EmployeeSalary
    {
        public long EmployeeSalaryId { get; set; }
        public long? EmployeeId { get; set; }
        public long? MonthlySalaryId { get; set; }
        public string Title { get; set; }
        public string SalaryMonth { get; set; }
        public string SalaryYear { get; set; }
        public string LOCATION { get; set; }
        public int? STDDAYS { get; set; }
        public int? WRKDAYS { get; set; }
        public int? LOPDAYS { get; set; }
        public decimal? Earning_Monthly_Basic { get; set; }
        public decimal? Earning_YTD_Basic { get; set; }
        public decimal? Earning_Montly_HRA { get; set; }
        public decimal? Earning_YTD_HRA { get; set; }
        public decimal? Earning_Montly_CONVEYANCE { get; set; }
        public decimal? Earning_YTD_CONVEYANCE { get; set; }
        public decimal? Earning_Montly_MEDICALALLOWANCE { get; set; }
        public decimal? Earning_YTD_MEDICALALLOWANCE { get; set; }
        public decimal? Earning_Montly_SPECIALALLOWANCE { get; set; }
        public decimal? Earning_YTD_SPECIALALLOWANCE { get; set; }
        public decimal? Earning_Montly_SPECIALBONUS { get; set; }
        public decimal? Earning_YTD_SPECIALBONUS { get; set; }
        public decimal? Earning_Montly_STATUTORYBONUS { get; set; }
        public decimal? Earning_YTD_STATUTORYBONUS { get; set; }
        public decimal? Earning_Montly_GROSSEARNINGS { get; set; }
        public decimal? Earning_YTD_GROSSEARNINGS { get; set; }
        public decimal? Earning_Montly_OTHERS { get; set; }
        public decimal? Earning_YTD_OTHERS { get; set; }
        public decimal? Deduction_Montly_PROFESSIONALTAX { get; set; }
        public decimal? Deduction_YTD_PROFESSIONALTAX { get; set; }
        public decimal? Deduction_Montly_ProvidentFund { get; set; }
        public decimal? Deduction_YTD_ProvidentFund { get; set; }
        public decimal? Deduction_Montly_GroupHealthInsurance { get; set; }
        public decimal? Deduction_YTD_GroupHealthInsurance { get; set; }
        public decimal? Deduction_Montly_OTHERS { get; set; }
        public decimal? Deduction_YTD_OTHERS { get; set; }
        public decimal? Deduction_Montly_GROSSSDeduction { get; set; }
        public decimal? Deduction_YTD_GROSSSDeduction { get; set; }
        public decimal? NETPAY { get; set; }
        public decimal? NETTRANSFER { get; set; }
        public string INWords { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
