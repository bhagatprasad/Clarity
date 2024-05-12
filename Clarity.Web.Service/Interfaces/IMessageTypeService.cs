using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IMessageTypeService
    {
        Task<List<MessageType>> FetchAllMessageType();
        //Task<bool> InsertOrUpdateMessageTpe(MessageType messageType);
        //Task<bool> 
    }
}
