using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class MessageTypeService : IMessageTypeService
    {
        private readonly ApplicationDBContext _dbContext;
        public MessageTypeService(ApplicationDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<List<MessageType>> FetchAllMessageType()
        {
            var messageType=await _dbContext.messageTypes.Where(x=>x.IsActive).ToListAsync();
            return messageType;
        }
    }
}
