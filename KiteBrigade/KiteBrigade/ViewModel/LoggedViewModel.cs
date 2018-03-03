using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiteBrigade.Service.Interface;

namespace KiteBrigade.ViewModel
{
    public abstract class LoggedViewModel : BaseViewModel
    {
        protected readonly ISessionService _sessionService;

        public LoggedViewModel(ILogService logService, ISessionService sessionService) : base(logService)
        {
            _sessionService = sessionService;
        }

        public bool IsLoggedIn => _sessionService.IsLoggedIn;
    }
}
