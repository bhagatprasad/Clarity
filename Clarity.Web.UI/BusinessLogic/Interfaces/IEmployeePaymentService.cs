using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IEmployeePaymentService
    {
        Task<List<EmployeePaymentModel>> GetAllEmployeePayments();
        Task<bool> InsertEmployeePayments(EmployeePayment employeePayment);
    }
}
