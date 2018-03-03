using KiteBrigade.Model;
using KiteBrigade.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KiteBrigade.Service
{
    public class SessionService : ISessionService
    {
        private readonly ISession _session;

        public bool IsLoggedIn => _session.User != null;

        public SessionService(ISession session)
        {
            _session = session;
        }

        public async Task GetSessionsAsync(string userId)
        {
            await Task.Delay(1000);
            _session.Sessions = new List<KiteSession>();
        }

        public async Task GetUserAsync(string userId)
        {
            await Task.Delay(1000);
            _session.User =  new User
            {
                Id = userId
            };
        }

        public async Task LogOutAsync()
        {
            _session.User = null;
            await Task.Delay(1);
        }

        public Task CreateSessionAsync()
        {
            _session.CurrentSessionIndex = _session.Sessions.Count;
            _session.CurrentSession = new KiteSession();
            return Task.FromResult(0);
        }

        public Task EditSessionAsync(int sessionIndex)
        {
            _session.CurrentSessionIndex = sessionIndex;
            _session.CurrentSession = new KiteSession(_session.Sessions[sessionIndex]);
            return Task.FromResult(0);
        }

        public Task SaveEditedSessionAsync()
        {
            try
            {
                _session.Sessions[_session.CurrentSessionIndex] = _session.CurrentSession;
            } catch (ArgumentOutOfRangeException e)
            {
                _session.Sessions.Add(_session.CurrentSession);
            }
           
            return Task.FromResult(0);
        }
    }
}
