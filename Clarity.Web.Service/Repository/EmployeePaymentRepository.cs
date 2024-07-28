using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class EmployeePaymentRepository : IEmployeePaymentService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ITutionFeeService _tutionFeeService;
        public EmployeePaymentRepository(ApplicationDBContext applicationDBContext, ITutionFeeService tutionFeeService)
        {
            this._dbContext = applicationDBContext;
            _tutionFeeService = tutionFeeService;

        }

        public async Task<List<EmployeePaymentModel>> GetAllEmployeePayments()
        {
            var employeePayments = (from epayments in _dbContext.employeePayments
                                    join employee in _dbContext.employees.Where(x=>x.IsActive==true) on epayments.EmployeeId equals employee.EmployeeId into employeeJoin
                                    from employeeJoinInfo in employeeJoin.DefaultIfEmpty()
                                   join paymentMethod in _dbContext.paymentMethods on epayments.PaymentMethodId equals paymentMethod.Id into paymentMethodJoin
                                   from paymentMethodJoinInfo in paymentMethodJoin.DefaultIfEmpty()
                                       join paymentType in _dbContext.paymentTypes on epayments.PaymentTypeId equals paymentType.Id into paymenttypeJoin
                                       from paymentTypeJoinInfo in paymenttypeJoin.DefaultIfEmpty()
                                    select new EmployeePaymentModel
                                    {
                                        Id = epayments.Id,
                                        EmployeeId = epayments.EmployeeId,
                                        Amount = epayments.Amount,
                                        CreatedBy = epayments.CreatedBy,
                                        CreatedOn = epayments.CreatedOn,
                                        IsActive = epayments.IsActive,
                                        ModifiedBy = epayments.ModifiedBy,
                                        ModifiedOn = epayments.ModifiedOn,
                                        PaymentMessage = epayments.PaymentMessage,
                                        EmployeeFullName = employeeJoinInfo != null ? employeeJoinInfo.FirstName + "" + employeeJoinInfo.LastName : string.Empty,
                                        PaymentMethodId = epayments.PaymentMethodId,
                                      PaymentMethodName = paymentMethodJoinInfo != null ? paymentMethodJoinInfo.Name : string.Empty,
                                        PaymentTypeId = epayments.PaymentTypeId,
                                        PaymentTypeName = paymentTypeJoinInfo != null ? paymentTypeJoinInfo.Name : string.Empty

                                    }).ToList();

            return employeePayments;
        }

        public async Task<bool> InsertEmployeePayments(EmployeePayment employeePayment)
        {
            if (employeePayment != null)
            {
                await _dbContext.employeePayments.AddAsync(employeePayment);

                var employeeTutionFee = await _dbContext.tutionFees.Where(x => x.EmployeeId == employeePayment.EmployeeId).FirstOrDefaultAsync();

                if (employeeTutionFee != null)
                {
                    employeeTutionFee.RemaingFee = (employeeTutionFee.RemaingFee - employeePayment.Amount);
                    employeeTutionFee.PaidFee = (employeeTutionFee.PaidFee + employeePayment.Amount);
                    employeeTutionFee.CreatedOn = DateTimeOffset.Now;
                    employeeTutionFee.CreatedBy = employeePayment.CreatedBy;
                    employeeTutionFee.ModifiedBy = employeePayment.ModifiedBy;
                    employeeTutionFee.ModifiedOn = DateTimeOffset.Now;
                    employeeTutionFee.IsActive = true;
                }
            }

            var response = await _dbContext.SaveChangesAsync();

            return response == 1 ? true : false;
        }
    }
}
