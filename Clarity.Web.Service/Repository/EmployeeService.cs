using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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

            employees = (from emp in context.employees.Where(x => x.IsActive == true)
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

                    //GENARATEs salary structure 

                    if (employee.employee.CurrentPrice.HasValue && employee.employee.CurrentPrice.Value > 0)
                    {
                        decimal monthlyGross = employee.employee.CurrentPrice.Value / 12;
                        decimal basic = monthlyGross * 0.4m;
                        decimal hra = basic * 0.5m;

                        decimal pfAmount = 3600;
                        decimal insurance = 1000;
                        decimal professionalTax = 200;
                        decimal special_allowance = 5000;
                        decimal statunory = 2000;

                        decimal grossEarnings = basic + hra + special_allowance + statunory;
                        decimal grossDeductions = pfAmount + insurance + professionalTax;
                        decimal netPay = grossEarnings - grossDeductions;

                        decimal otherAmount = monthlyGross - grossEarnings;
                        decimal finalGrossEarnings = grossEarnings + otherAmount;
                        //string inWords = IndianSalaryConverter.ConvertToWords((double)netPay);

                        EmployeeSalaryStructure employeeSalaryStructure = new EmployeeSalaryStructure()
                        {
                            Adhar = employee.Adhar,
                            BankAccount = employee.BankAccount,
                            BankName = employee.BankName,
                            IFSC = employee.IFSC,
                            PAN = employee.PAN,
                            PFNO = employee.PFNO,
                            UAN = employee.UAN,
                            BASIC = basic,
                            HRA = hra,
                            CONVEYANCE = 0,
                            MEDICALALLOWANCE = 0,
                            SPECIALALLOWANCE = special_allowance,
                            SPECIALBONUS = 0,
                            STATUTORYBONUS = statunory,
                            OTHERS = otherAmount,
                            EmployeeId = employeeId,
                            ESIC = insurance,
                            GROSSDEDUCTIONS = grossDeductions,
                            GROSSEARNINGS = finalGrossEarnings,
                            GroupHealthInsurance = insurance,
                            PF = pfAmount,
                            PROFESSIONALTAX = professionalTax,
                            CreatedBy = employee.employee.CreatedBy,
                            ModifiedBy = employee.employee.ModifiedBy,
                            CreatedOn = employee.employee.CreatedOn,
                            ModifiedOn = employee.employee.ModifiedOn,
                            IsActive = true
                        };

                        await context.employeeSalaryStructures.AddAsync(employeeSalaryStructure);
                    }

                    var responce = await context.SaveChangesAsync();

                    return responce == 1 ? true : false;

                }
            }
            return false;
        }

        public async Task<bool> SalaryHikeAsync(SalaryHike salaryHike)
        {
            var employee = await context.employees.FindAsync(salaryHike.EmployeeId);

            if (employee != null)
            {
                employee.CurrentPrice = salaryHike.LatestSalary;
                employee.ModifiedBy = salaryHike.ModifiedBy;
                employee.ModifiedOn = salaryHike.ModifiedOn;

                await context.SaveChangesAsync();

                if (salaryHike.LatestSalary.HasValue && salaryHike.LatestSalary.Value > 0)
                {
                    decimal monthlyGross = salaryHike.LatestSalary.Value / 12;
                    decimal basic = monthlyGross * 0.4m;
                    decimal hra = basic * 0.5m;

                    decimal pfAmount = 3600;
                    decimal insurance = 1000;
                    decimal professionalTax = 200;
                    decimal special_allowance = 5000;
                    decimal statunory = 2000;

                    decimal grossEarnings = basic + hra + special_allowance + statunory;
                    decimal grossDeductions = pfAmount + insurance + professionalTax;
                    decimal netPay = grossEarnings - grossDeductions;

                    decimal otherAmount = monthlyGross - grossEarnings;
                    decimal finalGrossEarnings = grossEarnings + otherAmount;

                    var exstingSalaryStructer = await context.employeeSalaryStructures.Where(x => x.EmployeeId == employee.EmployeeId).FirstOrDefaultAsync();

                    if (exstingSalaryStructer != null)
                    {

                        exstingSalaryStructer.BASIC = basic;
                        exstingSalaryStructer.HRA = hra;
                        exstingSalaryStructer.CONVEYANCE = 0;
                        exstingSalaryStructer.MEDICALALLOWANCE = 0;
                        exstingSalaryStructer.SPECIALALLOWANCE = special_allowance;
                        exstingSalaryStructer.SPECIALBONUS = 0;
                        exstingSalaryStructer.STATUTORYBONUS = statunory;
                        exstingSalaryStructer.OTHERS = otherAmount;
                        exstingSalaryStructer.EmployeeId = employee.EmployeeId;
                        exstingSalaryStructer.ESIC = insurance;
                        exstingSalaryStructer.GROSSDEDUCTIONS = grossDeductions;
                        exstingSalaryStructer.GROSSEARNINGS = finalGrossEarnings;
                        exstingSalaryStructer.GroupHealthInsurance = insurance;
                        exstingSalaryStructer.PF = pfAmount;
                        exstingSalaryStructer.PROFESSIONALTAX = professionalTax;
                        exstingSalaryStructer.ModifiedBy = salaryHike.ModifiedBy;
                        exstingSalaryStructer.ModifiedOn = salaryHike.ModifiedOn;

                        await context.SaveChangesAsync();
                    }
                }
                return true;
            }

            return false;
        }
    }
}
