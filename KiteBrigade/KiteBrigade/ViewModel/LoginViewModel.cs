using KiteBrigade.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KiteBrigade.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ISession _session;
        private readonly ISessionService _sessionService;

        private bool _isLogedIn;
        public bool IsLogedIn
        {
            get
            {
                return _isLogedIn;
            }
            set
            {
                Set(ref _isLogedIn, value);
            }
        }

        public LoginViewModel(ILogService logService, ISession session, ISessionService sessionService) : base(logService)
        {
            _session = session;
            _sessionService = sessionService;
        }

        protected override void RaiseCanExecuteChanged()
        {
            //
        }

        protected override async Task StartAsync(CancellationToken ct)
        {
            IsLogedIn = (_session.User != null);
            await Task.WhenAll(_sessionService.GetUserAsync("0"), _sessionService.GetSessionsAsync("0"));
            IsLogedIn = true;
        }
    }
}