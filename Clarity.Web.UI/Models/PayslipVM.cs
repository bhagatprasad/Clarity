using Microsoft.AspNetCore.Identity;

namespace Clarity.Web.UI.Models
{
    public class PayslipVM
    {
        public PayslipVM()
        {
            this.employee = new Employee();
            this.employeeSalary = new EmployeeSalary();
        }
        public Employee employee { get; set; }
        public EmployeeSalary employeeSalary { get; set; }
        public Department department { get; set; }
        public Designation designation { get; set; }
    }
}
