using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDBContext context;

        public EmployeeService(ApplicationDBContext dbcontext)
        {
            this.context = dbcontext;
        }
        public async Task<List<AddEditEmployee>> fetchAllEmployeesAsync()
        {
            var employees = new List<AddEditEmployee>();

            employees = (from emp in context.employees
                             //join empEmployment in context.employeeEmployments on emp.EmployeeId equals empEmployment.EmployeeId into employmentGroup
                             //join empEducation in context.employeeEducations on emp.EmployeeId equals empEducation.EmployeeId into empEducationGroup
                             //join empAdress in context.employeeAddresses on emp.EmployeeId equals empAdress.EmployeeId into empAdressGroup
                             //join empContact in context.employeeEmergencyContacts on emp.EmployeeId equals empContact.EmployeeId into empContactGroup
                         select new AddEditEmployee
                         {
                             employee = emp,
                             employeeAddresses = new List<EmployeeAddress>(),
                             employeeEducations = new List<EmployeeEducation>(),
                             employeeEmergencyContacts = new List<EmployeeEmergencyContact>(),
                             employeeEmployments = new List<EmployeeEmployment>()
                         }).ToList();

            return employees;
        }

        public async Task<AddEditEmployee> fetchEmployeeAsync(long employeeId)
        {
            var employees = new AddEditEmployee();

            var dbEmployee = await context.employees.FindAsync(employeeId);

            if (dbEmployee != null)
            {
                var employeeContact = await context.employeeEmergencyContacts.Where(x => x.EmployeeId == employeeId).ToListAsync();
                var employeeEmployments = await context.employeeEmployments.Where(x => x.EmployeeId == employeeId).ToListAsync();
                var employeeEducation = await context.employeeEducations.Where(x => x.EmployeeId == employeeId).ToListAsync();
                var employeeAddress = await context.employeeAddresses.Where(x => x.EmployeeId == employeeId).ToListAsync();

                employees.employee = dbEmployee;
                employees.employeeEmergencyContacts = employeeContact;
                employees.employeeEducations = employeeEducation;
                employees.employeeEmployments = employeeEmployments;
                employees.employeeAddresses = employeeAddress;
            }


            return employees;
        }

        public async Task<bool> InsertOrUpdateAsync(AddEditEmployee employee)
        {
            if (employee != null)
            {
                if (employee.employee.EmployeeId > 0)
                {
                    context.employees.Update(employee.employee);

                    // Assign EmployeeId to related entities
                    long employeeId = employee.employee.EmployeeId;
                    employee.employeeAddresses.ForEach(x => x.EmployeeId = employeeId);
                    employee.employeeEducations.ForEach(x => x.EmployeeId = employeeId);
                    employee.employeeEmployments.ForEach(x => x.EmployeeId = employeeId);
                    employee.employeeEmergencyContacts.ForEach(x => x.EmployeeId = employeeId);

                    // Update related entities in the context
                    context.employeeAddresses.UpdateRange(employee.employeeAddresses);
                    context.employeeEducations.UpdateRange(employee.employeeEducations);
                    context.employeeEmployments.UpdateRange(employee.employeeEmployments);
                    context.employeeEmergencyContacts.UpdateRange(employee.employeeEmergencyContacts);

                    // Save changes to the database
                    var responce = await context.SaveChangesAsync();

                    return responce == 1 ? true : false;
                }
                else
                {
                    await context.employees.AddAsync(employee.employee);
                    await context.SaveChangesAsync();

                    long employeeId = employee.employee.EmployeeId;

                    employee.employeeAddresses.ForEach(x => x.EmployeeId = employeeId);
                    employee.employeeEducations.ForEach(x => x.EmployeeId = employeeId);
                    employee.employeeEmployments.ForEach(x => x.EmployeeId = employeeId);
                    employee.employeeEmergencyContacts.ForEach(x => x.EmployeeId = employeeId);

                    context.employeeAddresses.AddRange(employee.employeeAddresses);
                    context.employeeEducations.AddRange(employee.employeeEducations);
                    context.employeeEmployments.AddRange(employee.employeeEmployments);
                    context.employeeEmergencyContacts.AddRange(employee.employeeEmergencyContacts);

                    var responce = await context.SaveChangesAsync();

                    return responce == 1 ? true : false;

                }
            }
            return false;
        }
    }
}
