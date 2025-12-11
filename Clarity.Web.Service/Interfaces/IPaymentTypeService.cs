using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IPaymentTypeService
    {
        Task<List<PaymentType>> GetAllPaymentTypes();
        Task<bool> InsertPaymentTypes(PaymentType paymentType);
    }
}
