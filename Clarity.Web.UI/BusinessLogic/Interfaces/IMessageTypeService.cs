using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IMessageTypeService
    {
        Task<List<MessageType>> GetAllMessageTypes();
    }
}
