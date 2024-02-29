using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IStateService
    {
        Task<bool> CreateState(State state);
        Task<bool> UpdateState(long stateId, State _state);
        Task<bool> DeleteState(long stateId);
        Task<State> GetState(long stateId);
        Task<List<CommonStates>> GetAllStates();
        Task<List<State>> GetStates();
    }
}
