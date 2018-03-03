using System.Threading;
using System.Threading.Tasks;
using KiteBrigade.Service.Interface;
using KiteBrigade.Model;
using System.Collections.Generic;

namespace KiteBrigade.ViewModel
{
    public class MainViewModel : LoggedViewModel
    {
        private readonly ISession _session;
        
        public IList<KiteSession> KiteSessions => _session.Sessions;

        public MainViewModel(ILogService logService, ISession session, ISessionService sessionService) : base(logService, sessionService)
        {
            _session = session;
        }

        public async void LogOut()
        {
            await _sessionService.LogOutAsync();
        }

        public async void CreateSession()
        {
            await _sessionService.CreateSessionAsync();
        }

        public async void SelectSession(int index)
        {
            await _sessionService.EditSessionAsync(index);
        }   

        protected override void RaiseCanExecuteChanged()
        {
            //
        }

        protected override async Task StartAsync(CancellationToken ct)
        {
            await Task.FromResult(0);
        }
    }
}