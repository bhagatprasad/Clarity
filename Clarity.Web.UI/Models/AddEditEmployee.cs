namespace Clarity.Web.UI.Models
{
    public class AddEditEmployee
    {
        public AddEditEmployee()
        {
            this.employee = new Employee();
            this.employeeEmergencyContacts = new List<EmployeeEmergencyContact>();
            this.employeeEducations = new List<EmployeeEducation>();
            this.employeeAddresses = new List<EmployeeAddress>();
            this.employeeEmployments = new List<EmployeeEmployment>();
        }
        public Employee employee { get; set; }
        public List<EmployeeAddress> employeeAddresses { get; set; }
        public List<EmployeeEducation> employeeEducations { get; set; }
        public List<EmployeeEmergencyContact> employeeEmergencyContacts { get; set; }
        public List<EmployeeEmployment> employeeEmployments { get; set; }
    }
}
