using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<List<PaymentMethod>> GetAllPaymentMethods();
        Task<bool> InsertPaymentMethods(PaymentMethod paymentMethod);
    }
}
