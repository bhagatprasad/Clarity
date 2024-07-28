using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IPaymentTypeService
    {
        Task<List<PaymentType>> GetAllPaymentTypes();
        //Task<bool> InsertPaymentTypes(PaymentType paymentType);
    }
}
