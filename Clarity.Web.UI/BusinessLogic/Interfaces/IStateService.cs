using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IStateService
    {
        Task<bool> CreateState(State state);
        
        Task<List<State>> GetStates();
    }
}
