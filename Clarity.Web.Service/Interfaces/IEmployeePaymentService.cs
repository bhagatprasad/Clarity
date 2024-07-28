using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IEmployeePaymentService
    {
        Task<List<EmployeePaymentModel>> GetAllEmployeePayments();
        Task<bool> InsertEmployeePayments(EmployeePayment employeePayment);
    }
}
