using Microsoft.AspNetCore.Identity;

namespace Clarity.Web.UI.Models
{
    public class PayslipVM
    {
        public PayslipVM()
        {
            this.employee = new Employee();
            this.employeeSalary = new EmployeeSalary();
            this.employeeSalaryStructure = new EmployeeSalaryStructure();
            this.department = new Department();
            this.designation = new Designation();
        }
        public Employee employee { get; set; }
        public EmployeeSalary employeeSalary { get; set; }
        public Department department { get; set; }
        public Designation designation { get; set; }
        public EmployeeSalaryStructure employeeSalaryStructure { get; set; }
    }
}
