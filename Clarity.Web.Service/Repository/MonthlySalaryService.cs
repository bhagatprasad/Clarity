using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace Clarity.Web.Service.Repository
{
    public class MonthlySalaryService : IMonthlySalaryService
    {
        private readonly ApplicationDBContext context;

        public MonthlySalaryService(ApplicationDBContext dbcontext)
        {
            this.context = dbcontext;
        }
        public async Task<List<MonthlySalary>> fetchAllMonthlySalaries()
        {
            return await context.monthlySalaries.ToListAsync();
        }

        public async Task<bool> PublishMonthlySalary(MonthlySalary monthlySalary)
        {

            await context.monthlySalaries.AddAsync(monthlySalary);

            await context.SaveChangesAsync();

            var activeEmployeeIds = await context.employees.Where(x => x.IsActive == true).Select(x => x.EmployeeId).ToListAsync();

            if (activeEmployeeIds.Any())
            {
                var salaryStructure = await context.employeeSalaryStructures.Where(x => activeEmployeeIds.Contains(x.EmployeeId.Value)).ToListAsync();

                var salaries = await context.employeeSalaries.Where(x => activeEmployeeIds.Contains(x.EmployeeId.Value)).ToListAsync();

                List<EmployeeSalary> employeeSalaries = new List<EmployeeSalary>();

                EmployeeSalary employeeSalary = null;

                //
                int stadardDays = MonthToYearConverter.GetDaysInMonth(monthlySalary.SalaryMonth, Convert.ToInt32(monthlySalary.SalaryYear));

                int adjustedMonth = MonthToYearConverter.GetAdjustedMonthNumber(monthlySalary.SalaryMonth, Convert.ToInt32(monthlySalary.SalaryYear));

                int lopDays = 0;

                foreach (var item in salaryStructure)
                {
                    employeeSalary = new EmployeeSalary();
                    employeeSalary.MonthlySalaryId = monthlySalary.MonthlySalaryId;
                    employeeSalary.EmployeeId = item.EmployeeId.Value;
                    employeeSalary.Title = monthlySalary.Title;
                    employeeSalary.SalaryMonth = monthlySalary.SalaryMonth;
                    employeeSalary.SalaryYear = monthlySalary.SalaryYear;
                    employeeSalary.LOCATION = monthlySalary.Location;
                    employeeSalary.STDDAYS = stadardDays;
                    employeeSalary.LOPDAYS = lopDays;
                    employeeSalary.WRKDAYS = stadardDays;

                    var maxEmployeeSalaryItem = salaries.Where(x => x.EmployeeId == item.EmployeeId && IsInFinancialYear(x.SalaryMonth, x.SalaryYear, monthlySalary.SalaryMonth, monthlySalary.SalaryYear)).OrderByDescending(x => x.Earning_YTD_Basic).FirstOrDefault();

                    if (maxEmployeeSalaryItem == null)
                    {
                        //Basic Calculation
                        employeeSalary.Earning_Monthly_Basic = item.BASIC;
                        employeeSalary.Earning_YTD_Basic = (item.BASIC) * adjustedMonth;

                        //HRA Calculation
                        employeeSalary.Earning_Montly_HRA = item.HRA;
                        employeeSalary.Earning_YTD_HRA = (item.HRA) * adjustedMonth;


                        //Conveyenace  Allowance
                        employeeSalary.Earning_YTD_CONVEYANCE = item.CONVEYANCE;
                        employeeSalary.Earning_Montly_CONVEYANCE = item.CONVEYANCE;

                        //Medical Allowance 
                        employeeSalary.Earning_Montly_MEDICALALLOWANCE = item.MEDICALALLOWANCE;
                        employeeSalary.Earning_YTD_MEDICALALLOWANCE = item.MEDICALALLOWANCE;


                        //Specail Allowance
                        employeeSalary.Earning_Montly_SPECIALALLOWANCE = item.SPECIALALLOWANCE;
                        employeeSalary.Earning_YTD_SPECIALALLOWANCE = (item.SPECIALALLOWANCE) * adjustedMonth;

                        employeeSalary.Earning_Montly_STATUTORYBONUS = item.STATUTORYBONUS;
                        employeeSalary.Earning_YTD_STATUTORYBONUS = (item.STATUTORYBONUS) * adjustedMonth;

                        employeeSalary.Earning_Montly_OTHERS = item.OTHERS;
                        employeeSalary.Earning_YTD_OTHERS = (item.OTHERS) * adjustedMonth;

                        employeeSalary.Earning_Montly_GROSSEARNINGS = item.GROSSEARNINGS;
                        employeeSalary.Earning_YTD_GROSSEARNINGS = (item.GROSSEARNINGS) * adjustedMonth;

                        //deducations
                        employeeSalary.Deduction_Montly_ProvidentFund = item.PF;
                        employeeSalary.Deduction_YTD_ProvidentFund = (item.PF) * adjustedMonth;

                        employeeSalary.Deduction_Montly_PROFESSIONALTAX = item.PROFESSIONALTAX;
                        employeeSalary.Deduction_YTD_PROFESSIONALTAX = (item.PROFESSIONALTAX) * adjustedMonth;

                        employeeSalary.Deduction_Montly_GroupHealthInsurance = item.GroupHealthInsurance;
                        employeeSalary.Deduction_YTD_GroupHealthInsurance = (item.GroupHealthInsurance) * adjustedMonth;

                        employeeSalary.Deduction_Montly_OTHERS = 0;
                        employeeSalary.Deduction_YTD_OTHERS = 0;

                        employeeSalary.Deduction_Montly_GROSSSDeduction = item.GROSSDEDUCTIONS;
                        employeeSalary.Deduction_YTD_GROSSSDeduction = (item.GROSSDEDUCTIONS) * adjustedMonth;

                        employeeSalary.NETTRANSFER = (item.GROSSEARNINGS - item.GROSSDEDUCTIONS);
                        employeeSalary.NETPAY = (item.GROSSEARNINGS - item.GROSSDEDUCTIONS);
                    }
                    else
                    {
                        // Basic Calculation
                        employeeSalary.Earning_Monthly_Basic = item.BASIC;
                        employeeSalary.Earning_YTD_Basic = (item.BASIC) + (maxEmployeeSalaryItem.Earning_YTD_Basic);

                        // HRA Calculation
                        employeeSalary.Earning_Montly_HRA = item.HRA;
                        employeeSalary.Earning_YTD_HRA = (item.HRA) + (maxEmployeeSalaryItem.Earning_YTD_HRA);

                        // Conveyance Allowance
                        employeeSalary.Earning_Montly_CONVEYANCE = item.CONVEYANCE;
                        employeeSalary.Earning_YTD_CONVEYANCE = (item.CONVEYANCE) + (maxEmployeeSalaryItem.Earning_YTD_CONVEYANCE);

                        // Medical Allowance
                        employeeSalary.Earning_Montly_MEDICALALLOWANCE = item.MEDICALALLOWANCE;
                        employeeSalary.Earning_YTD_MEDICALALLOWANCE = (item.MEDICALALLOWANCE) + (maxEmployeeSalaryItem.Earning_YTD_MEDICALALLOWANCE);

                        // Special Allowance
                        employeeSalary.Earning_Montly_SPECIALALLOWANCE = item.SPECIALALLOWANCE;
                        employeeSalary.Earning_YTD_SPECIALALLOWANCE = (item.SPECIALALLOWANCE) + (maxEmployeeSalaryItem.Earning_YTD_SPECIALALLOWANCE);

                        // Statutory Bonus
                        employeeSalary.Earning_Montly_STATUTORYBONUS = item.STATUTORYBONUS;
                        employeeSalary.Earning_YTD_STATUTORYBONUS = (item.STATUTORYBONUS) + (maxEmployeeSalaryItem.Earning_YTD_STATUTORYBONUS);

                        // Others
                        employeeSalary.Earning_Montly_OTHERS = item.OTHERS;
                        employeeSalary.Earning_YTD_OTHERS = (item.OTHERS) + (maxEmployeeSalaryItem.Earning_YTD_OTHERS);

                        // Gross Earnings
                        employeeSalary.Earning_Montly_GROSSEARNINGS = item.GROSSEARNINGS;
                        employeeSalary.Earning_YTD_GROSSEARNINGS = (item.GROSSEARNINGS) + (maxEmployeeSalaryItem.Earning_YTD_GROSSEARNINGS);

                        // Deductions
                        employeeSalary.Deduction_Montly_ProvidentFund = item.PF;
                        employeeSalary.Deduction_YTD_ProvidentFund = (item.PF) + (maxEmployeeSalaryItem.Deduction_YTD_ProvidentFund);

                        employeeSalary.Deduction_Montly_PROFESSIONALTAX = item.PROFESSIONALTAX;
                        employeeSalary.Deduction_YTD_PROFESSIONALTAX = (item.PROFESSIONALTAX) + (maxEmployeeSalaryItem.Deduction_YTD_PROFESSIONALTAX);

                        employeeSalary.Deduction_Montly_GroupHealthInsurance = item.GroupHealthInsurance;
                        employeeSalary.Deduction_YTD_GroupHealthInsurance = (item.GroupHealthInsurance) + (maxEmployeeSalaryItem.Deduction_YTD_GroupHealthInsurance);

                        employeeSalary.Deduction_Montly_OTHERS = 0;
                        employeeSalary.Deduction_YTD_OTHERS = maxEmployeeSalaryItem.Deduction_YTD_OTHERS + 0;

                        employeeSalary.Deduction_Montly_GROSSSDeduction = item.GROSSDEDUCTIONS;
                        employeeSalary.Deduction_YTD_GROSSSDeduction = (item.GROSSDEDUCTIONS) + (maxEmployeeSalaryItem.Deduction_YTD_GROSSSDeduction);

                        // Net Transfer and Net Pay
                        employeeSalary.NETTRANSFER = (item.GROSSEARNINGS - item.GROSSDEDUCTIONS);
                        employeeSalary.NETPAY = (item.GROSSEARNINGS - item.GROSSDEDUCTIONS);
                    }

                    decimal? netSalary = 0;

                    netSalary = (item.GROSSEARNINGS - item.GROSSDEDUCTIONS);

                    string inwordsRupees = IndianSalaryConverter.ConvertToWords(netSalary.Value);

                    employeeSalary.INWords = string.Empty;

                    employeeSalary.INWords = inwordsRupees;

                    employeeSalary.CreatedBy = monthlySalary.CreatedBy;
                    employeeSalary.ModifiedBy = monthlySalary.ModifiedBy;
                    employeeSalary.CreatedOn = monthlySalary.CreatedOn;
                    employeeSalary.ModifiedOn = monthlySalary.ModifiedOn;
                    employeeSalary.IsActive = true;


                    employeeSalaries.Add(employeeSalary);
                }

                context.employeeSalaries.AddRange(employeeSalaries);

                var responce = await context.SaveChangesAsync();

                return responce == 1 ? true : false;
            }
            return false;
        }
        private bool IsInFinancialYear(string recordMonth, string recordYear, string salaryMonth, string salaryYear)
        {
            // Convert years to integers
            int recordYearInt = int.Parse(recordYear);
            int salaryYearInt = int.Parse(salaryYear);

            // Convert months to numeric values
            int recordMonthNumber = GetMonthNumber(recordMonth);
            int salaryMonthNumber = GetMonthNumber(salaryMonth);

            // Determine the financial year range based on the given SalaryYear
            int financialYearStart, financialYearEnd;

            if (salaryMonthNumber >= 4) // April or later
            {
                // Financial year starts in the same year (e.g., April 2024 to March 2025)
                financialYearStart = salaryYearInt;
                financialYearEnd = salaryYearInt + 1;
            }
            else // January to March
            {
                // Financial year starts in the previous year (e.g., April 2023 to March 2024)
                financialYearStart = salaryYearInt - 1;
                financialYearEnd = salaryYearInt;
            }

            // Check if the record falls within the financial year range
            if (recordYearInt == financialYearStart && recordMonthNumber >= 4)
            {
                // Record is in April, May, ..., December of the start year
                return true;
            }
            else if (recordYearInt == financialYearEnd && recordMonthNumber <= 3)
            {
                // Record is in January, February, March of the end year
                return true;
            }

            return false;
        }

        private int GetMonthNumber(string monthName)
        {
            // Map month names to their numeric values
            var months = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
    {
        {"January", 1}, {"February", 2}, {"March", 3},
        {"April", 4}, {"May", 5}, {"June", 6},
        {"July", 7}, {"August", 8}, {"September", 9},
        {"October", 10}, {"November", 11}, {"December", 12}
    };

            return months.TryGetValue(monthName, out int monthNumber) ? monthNumber : 0;
        }

        private async Task<bool> PublishMonthlySalariesAsync(MonthlySalary monthlySalary)
        {
            try
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        await context.monthlySalaries.AddAsync(monthlySalary);
                        await context.SaveChangesAsync();

                        var activeEmployeeIds = await GetActiveEmployeeIdsAsync();

                        if (activeEmployeeIds.Any())
                        {
                            var salaryStructure = await GetEmployeeSalaryStructuresAsync(activeEmployeeIds);
                            var salaries = await GetEmployeeSalariesAsync(activeEmployeeIds);

                            var employeeSalaries = CalculateEmployeeSalaries(monthlySalary, salaryStructure, salaries);

                            context.employeeSalaries.AddRange(employeeSalaries);
                            var response = await context.SaveChangesAsync();

                            await transaction.CommitAsync();

                            return response > 0;
                        }

                        return false;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw; // Re-throw the exception to be handled by the caller
                    }
                }
            }
            catch (Exception ex)
            {
                throw; // Re-throw the exception to be handled by the caller
            }
        }

        private async Task<List<long>> GetActiveEmployeeIdsAsync()
        {
            return await context.employees.Where(x => x.IsActive == true).Select(x => x.EmployeeId).ToListAsync();
        }

        private async Task<List<EmployeeSalaryStructure>> GetEmployeeSalaryStructuresAsync(List<long> activeEmployeeIds)
        {
            return await context.employeeSalaryStructures
                .Where(x => activeEmployeeIds.Contains(x.EmployeeId.Value))
                .ToListAsync();
        }

        private async Task<List<EmployeeSalary>> GetEmployeeSalariesAsync(List<long> activeEmployeeIds)
        {
            return await context.employeeSalaries
                .Where(x => activeEmployeeIds.Contains(x.EmployeeId.Value))
                .ToListAsync();
        }

        private List<EmployeeSalary> CalculateEmployeeSalaries(MonthlySalary monthlySalary, List<EmployeeSalaryStructure> salaryStructure, List<EmployeeSalary> salaries)
        {
            var employeeSalaries = new List<EmployeeSalary>();

            int standardDays = MonthToYearConverter.GetDaysInMonth(monthlySalary.SalaryMonth, Convert.ToInt32(monthlySalary.SalaryYear));
            int adjustedMonth = MonthToYearConverter.GetAdjustedMonthNumber(monthlySalary.SalaryMonth, Convert.ToInt32(monthlySalary.SalaryYear));
            int lopDays = 0;

            foreach (var item in salaryStructure)
            {
                var employeeSalary = new EmployeeSalary
                {
                    MonthlySalaryId = monthlySalary.MonthlySalaryId,
                    EmployeeId = item.EmployeeId.Value,
                    Title = monthlySalary.Title,
                    SalaryMonth = monthlySalary.SalaryMonth,
                    SalaryYear = monthlySalary.SalaryYear,
                    LOCATION = monthlySalary.Location,
                    STDDAYS = standardDays,
                    LOPDAYS = lopDays,
                    WRKDAYS = standardDays,
                    CreatedBy = monthlySalary.CreatedBy,
                    ModifiedBy = monthlySalary.ModifiedBy,
                    CreatedOn = monthlySalary.CreatedOn,
                    ModifiedOn = monthlySalary.ModifiedOn,
                    IsActive = true
                };

                var maxEmployeeSalaryItem = salaries
                    .Where(x => x.EmployeeId == item.EmployeeId && x.SalaryYear == monthlySalary.SalaryYear)
                    .OrderByDescending(x => x.Earning_YTD_Basic)
                    .FirstOrDefault();

                if (maxEmployeeSalaryItem == null)
                {
                    CalculateNewEmployeeSalary(employeeSalary, item, adjustedMonth);
                }
                else
                {
                    CalculateExistingEmployeeSalary(employeeSalary, item, maxEmployeeSalaryItem);
                }

                var netSalary = (item.GROSSEARNINGS - item.GROSSDEDUCTIONS);
                employeeSalary.INWords = IndianSalaryConverter.ConvertToWords(netSalary.Value);

                employeeSalaries.Add(employeeSalary);
            }

            return employeeSalaries;
        }

        private void CalculateNewEmployeeSalary(EmployeeSalary employeeSalary, EmployeeSalaryStructure item, int adjustedMonth)
        {
            // Basic Calculation
            employeeSalary.Earning_Monthly_Basic = item.BASIC;
            employeeSalary.Earning_YTD_Basic = item.BASIC * adjustedMonth;

            // HRA Calculation
            employeeSalary.Earning_Montly_HRA = item.HRA;
            employeeSalary.Earning_YTD_HRA = item.HRA * adjustedMonth;

            // Conveyance Allowance
            employeeSalary.Earning_Montly_CONVEYANCE = item.CONVEYANCE;
            employeeSalary.Earning_YTD_CONVEYANCE = item.CONVEYANCE;

            // Medical Allowance
            employeeSalary.Earning_Montly_MEDICALALLOWANCE = item.MEDICALALLOWANCE;
            employeeSalary.Earning_YTD_MEDICALALLOWANCE = item.MEDICALALLOWANCE;

            // Special Allowance
            employeeSalary.Earning_Montly_SPECIALALLOWANCE = item.SPECIALALLOWANCE;
            employeeSalary.Earning_YTD_SPECIALALLOWANCE = item.SPECIALALLOWANCE * adjustedMonth;

            // Statutory Bonus
            employeeSalary.Earning_Montly_STATUTORYBONUS = item.STATUTORYBONUS;
            employeeSalary.Earning_YTD_STATUTORYBONUS = item.STATUTORYBONUS * adjustedMonth;

            // Others
            employeeSalary.Earning_Montly_OTHERS = item.OTHERS;
            employeeSalary.Earning_YTD_OTHERS = item.OTHERS * adjustedMonth;

            // Gross Earnings
            employeeSalary.Earning_Montly_GROSSEARNINGS = item.GROSSEARNINGS;
            employeeSalary.Earning_YTD_GROSSEARNINGS = item.GROSSEARNINGS * adjustedMonth;

            // Deductions
            employeeSalary.Deduction_Montly_ProvidentFund = item.PF;
            employeeSalary.Deduction_YTD_ProvidentFund = item.PF * adjustedMonth;

            employeeSalary.Deduction_Montly_PROFESSIONALTAX = item.PROFESSIONALTAX;
            employeeSalary.Deduction_YTD_PROFESSIONALTAX = item.PROFESSIONALTAX * adjustedMonth;

            employeeSalary.Deduction_Montly_GroupHealthInsurance = item.GroupHealthInsurance;
            employeeSalary.Deduction_YTD_GroupHealthInsurance = item.GroupHealthInsurance * adjustedMonth;

            employeeSalary.Deduction_Montly_OTHERS = 0;
            employeeSalary.Deduction_YTD_OTHERS = 0;

            employeeSalary.Deduction_Montly_GROSSSDeduction = item.GROSSDEDUCTIONS;
            employeeSalary.Deduction_YTD_GROSSSDeduction = item.GROSSDEDUCTIONS * adjustedMonth;

            // Net Transfer and Net Pay
            employeeSalary.NETTRANSFER = item.GROSSEARNINGS - item.GROSSDEDUCTIONS;
            employeeSalary.NETPAY = item.GROSSEARNINGS - item.GROSSDEDUCTIONS;
        }

        private void CalculateExistingEmployeeSalary(EmployeeSalary employeeSalary, EmployeeSalaryStructure item, EmployeeSalary maxEmployeeSalaryItem)
        {
            // Basic Calculation
            employeeSalary.Earning_Monthly_Basic = item.BASIC;
            employeeSalary.Earning_YTD_Basic = item.BASIC + maxEmployeeSalaryItem.Earning_YTD_Basic;

            // HRA Calculation
            employeeSalary.Earning_Montly_HRA = item.HRA;
            employeeSalary.Earning_YTD_HRA = item.HRA + maxEmployeeSalaryItem.Earning_YTD_HRA;

            // Conveyance Allowance
            employeeSalary.Earning_Montly_CONVEYANCE = item.CONVEYANCE;
            employeeSalary.Earning_YTD_CONVEYANCE = item.CONVEYANCE + maxEmployeeSalaryItem.Earning_YTD_CONVEYANCE;

            // Medical Allowance
            employeeSalary.Earning_Montly_MEDICALALLOWANCE = item.MEDICALALLOWANCE;
            employeeSalary.Earning_YTD_MEDICALALLOWANCE = item.MEDICALALLOWANCE + maxEmployeeSalaryItem.Earning_YTD_MEDICALALLOWANCE;

            // Special Allowance
            employeeSalary.Earning_Montly_SPECIALALLOWANCE = item.SPECIALALLOWANCE;
            employeeSalary.Earning_YTD_SPECIALALLOWANCE = item.SPECIALALLOWANCE + maxEmployeeSalaryItem.Earning_YTD_SPECIALALLOWANCE;

            // Statutory Bonus
            employeeSalary.Earning_Montly_STATUTORYBONUS = item.STATUTORYBONUS;
            employeeSalary.Earning_YTD_STATUTORYBONUS = item.STATUTORYBONUS + maxEmployeeSalaryItem.Earning_YTD_STATUTORYBONUS;

            // Others
            employeeSalary.Earning_Montly_OTHERS = item.OTHERS;
            employeeSalary.Earning_YTD_OTHERS = item.OTHERS + maxEmployeeSalaryItem.Earning_YTD_OTHERS;

            // Gross Earnings
            employeeSalary.Earning_Montly_GROSSEARNINGS = item.GROSSEARNINGS;
            employeeSalary.Earning_YTD_GROSSEARNINGS = item.GROSSEARNINGS + maxEmployeeSalaryItem.Earning_YTD_GROSSEARNINGS;

            // Deductions
            employeeSalary.Deduction_Montly_ProvidentFund = item.PF;
            employeeSalary.Deduction_YTD_ProvidentFund = item.PF + maxEmployeeSalaryItem.Deduction_YTD_ProvidentFund;

            employeeSalary.Deduction_Montly_PROFESSIONALTAX = item.PROFESSIONALTAX;
            employeeSalary.Deduction_YTD_PROFESSIONALTAX = item.PROFESSIONALTAX + maxEmployeeSalaryItem.Deduction_YTD_PROFESSIONALTAX;

            employeeSalary.Deduction_Montly_GroupHealthInsurance = item.GroupHealthInsurance;
            employeeSalary.Deduction_YTD_GroupHealthInsurance = item.GroupHealthInsurance + maxEmployeeSalaryItem.Deduction_YTD_GroupHealthInsurance;

            employeeSalary.Deduction_Montly_OTHERS = 0;
            employeeSalary.Deduction_YTD_OTHERS = maxEmployeeSalaryItem.Deduction_YTD_OTHERS + 0;

            employeeSalary.Deduction_Montly_GROSSSDeduction = item.GROSSDEDUCTIONS;
            employeeSalary.Deduction_YTD_GROSSSDeduction = item.GROSSDEDUCTIONS + maxEmployeeSalaryItem.Deduction_YTD_GROSSSDeduction;

            // Net Transfer and Net Pay
            employeeSalary.NETTRANSFER = item.GROSSEARNINGS - item.GROSSDEDUCTIONS;
            employeeSalary.NETPAY = item.GROSSEARNINGS - item.GROSSDEDUCTIONS;
        }
    }
}
