using KiteBrigade.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KiteBrigade.Service.Interface
{
    public interface ISessionService
    {
        bool IsLoggedIn { get; }
        Task GetUserAsync(string userId);
        Task GetSessionsAsync(string userId);
        Task CreateSessionAsync();
        Task EditSessionAsync(int sessionIndex);
        Task SaveEditedSessionAsync();
        Task LogOutAsync();
    }
}
