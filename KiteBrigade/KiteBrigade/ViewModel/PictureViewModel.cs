using KiteBrigade.Service.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace KiteBrigade.ViewModel
{
    public class PictureViewModel : LoggedViewModel
    {
        private readonly ISession _session;

        public PictureViewModel(ILogService logService, ISessionService sessionService, ISession session) : base(logService, sessionService)
        {
            _session = session;
        }

        private string _pictureUri;
        public string PictureUri
        {
            get
            {
                return _pictureUri;
            }
            set
            {
                _session.CurrentSession.PictureUri = value;
                Set(ref _pictureUri, value);
            }
        }

        protected override void RaiseCanExecuteChanged()
        {
            //
        }

        protected override Task StartAsync(CancellationToken ct)
        {
            PictureUri = _session.CurrentSession.PictureUri;
            return Task.FromResult(0);
        }

        public override void Cleanup()
        {
            _pictureUri = null;
        }
    }
}
