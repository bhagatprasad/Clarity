using Clarity.Web.Service.Models;
using Clarity.Web.Service.Repository;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.DBConfiguration
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> users { get; set; }
        public DbSet<Roles> roles { get; set; }
        public DbSet<Country> countries { get; set; }
        public DbSet<Department> department { get; set; }
        public DbSet<Designation> designation { get; set; }
        public DbSet<State> state { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<EmployeeAddress> employeeAddresses { get; set; }
        public DbSet<EmployeeEducation> employeeEducations { get; set; }
        public DbSet<EmployeeEmployment> employeeEmployments { get; set; }
        public DbSet<EmployeeEmergencyContact> employeeEmergencyContacts { get; set; }
        public DbSet<EmployeeSalaryStructure> employeeSalaryStructures { get; set; }
        public DbSet<EmployeeSalary> employeeSalaries { get; set; }
        public DbSet<MonthlySalary> monthlySalaries { get; set; }
        public DbSet<TaskItem> taskItems { get; set; }
        public DbSet<TaskCode> taskCodes { get; set; }
        public DbSet<RepotingManager> reportingManagers { get; set; }
        public DbSet<HolidayCallender> holidayCallenders { get; set; }
    }
}
