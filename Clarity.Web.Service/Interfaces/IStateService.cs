using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IStateService
    {
        Task<bool> CreateState(State state);
        Task<bool> UpdateState(long stateId, State state);
        Task<bool> DeleteState(long stateId);
        Task<State> GetState(long stateId);
        Task<List<State>> GetAllState();
        Task<bool> VerifyStateAlreadyExists(string state);
    }
}
