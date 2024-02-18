using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class StateService : IStateService
    {
        private readonly ApplicationDBContext dbcontext;
        public StateService(ApplicationDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<bool> CreateState(State states)
        {
            if (states != null)
                await dbcontext.state.AddAsync(states);
            var response = await dbcontext.SaveChangesAsync();
            return response == 1 ? true : false;

        }

        public async Task<bool> DeleteState(long stateId)
        {
            var states = await dbcontext.state.FindAsync(stateId);
            if (states != null) ;
            {
                dbcontext.state.Remove(states);
            }
            var response = await dbcontext.SaveChangesAsync();
            return response == 1 ? true : false;
        }

        public async Task<List<State>> GetAllState()
        {
            return await dbcontext.state.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<State> GetState(long stateId)
        {
            var states = await dbcontext.state.FindAsync(stateId);
            if (states != null)
            {
                return states;
            }
            return null;
        }

        public async Task<bool> UpdateState(long stateId, State _state)
        {
            State states = await dbcontext.state.FindAsync(stateId, _state);
            if (states != null)
            {
                states.CountryId = _state.CountryId;
                states.Name = _state.Name;
                states.Description = _state.Description;
                states.SateCode = _state.SateCode;
                states.CountryCode = _state.CountryCode;
                states.CreatedOn = DateTimeOffset.Now;
                states.CreatedBy = _state.CreatedBy;
                states.ModifiedOn = DateTimeOffset.Now;
                states.ModifiedBy = _state.ModifiedBy;
                states.IsActive = _state.IsActive;
            }
            var response = await dbcontext.SaveChangesAsync();
            return response == 1 ? true : false;
        }
        public async Task<bool> VerifyStateAlreadyExists(string state)
        {
            return await dbcontext.state.AnyAsync(x => x.Name.ToLower().Trim() == state.ToLower().Trim());
        }
    }
}
