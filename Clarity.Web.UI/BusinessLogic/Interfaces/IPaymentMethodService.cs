using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<List<PaymentMethod>> GetAllPaymentMethods();
        //Task<bool> InsertPaymentMethods(PaymentMethod paymentMethod);
    }
}
