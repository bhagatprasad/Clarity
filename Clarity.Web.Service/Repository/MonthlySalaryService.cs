using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            var salaryStructure = await context.employeeSalaryStructures.Where(x => activeEmployeeIds.Contains(x.EmployeeId.Value)).ToListAsync();

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

                var netSalary = (item.GROSSEARNINGS - item.GROSSDEDUCTIONS);

                employeeSalary.INWords = IndianSalaryConverter.ConvertDecimalToWords(netSalary.Value);


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
    }
}
