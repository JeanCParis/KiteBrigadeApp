using KiteBrigade.Model;
using KiteBrigade.Resx;
using KiteBrigade.Service.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace KiteBrigade.ViewModel
{
    public abstract class SessionViewModel : LoggedViewModel
    {
        protected readonly ISession _session;
        protected readonly IPhotoService _photoService;
        
        protected KiteSession CurrentSession => _session.CurrentSession;

        private int _windValue;
        public int WindValue
        {
            get
            {
                return _windValue;
            }
            set
            {
                CurrentSession.WindValue = value;
                Set(ref _windValue, value);
                CalulateScore();
            }
        }

        private int _waterValue;
        public int WaterValue
        {
            get
            {
                return _waterValue;
            }
            set
            {
                CurrentSession.WaterValue = value;
                Set(ref _waterValue, value);
                CalulateScore();
            }
        }

        private int _funValue;
        public int FunValue
        {
            get
            {
                return _funValue;
            }
            set
            {
                CurrentSession.FunValue = value;
                Set(ref _funValue, value);
                CalulateScore();
            }
        }

        private float _score;
        public float Score
        {
            get
            {
                return _score;
            }
            protected set
            {
                Set(ref _score, value);
            }
        }

        private string _location;
        public string Location
        {
            get
            {
                return _location;
            }
            private set
            {
                CurrentSession.Location = value;
                Set(ref _location, value);
            }
        }

        private int _duration;
        public int Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                CurrentSession.Duration = value;
                Set(ref _duration, value);
            }
        }

        private string _photoUri;
        public string PhotoUri
        {
            get
            {
                return _photoUri;
            }
            set
            {
                CurrentSession.PictureUri = value;
                Set(ref _photoUri, value);
            }
        }

        public SessionViewModel(ILogService logService, ISessionService sessionService, ISession session, IPhotoService photoService) : base(logService, sessionService)
        {
            _session = session;
            _photoService = photoService;
        }

        private void CalulateScore()
        {
            Score = Business.Score.Calculate(WindValue, WaterValue, FunValue);
        }

        public void SetPhoto()
        {
            _photoService.ShowPhotoSelector(AppResources.SelectPicture);
        }

        public void SetSessionPhotoUri(string uri)
        {
            _session.CurrentSession.PictureUri = uri;
        }

        public virtual async Task SaveSession()
        {
            await _sessionService.SaveEditedSessionAsync();
        }

        protected override async Task StartAsync(CancellationToken ct)
        {
            WindValue = CurrentSession.WindValue;
            FunValue = CurrentSession.FunValue;
            WaterValue = CurrentSession.WaterValue;
            Location = CurrentSession.Location;
            PhotoUri = CurrentSession.PictureUri;
            Duration = CurrentSession.Duration;

            await Task.FromResult(0);
        }

        public override void Cleanup()
        {
            _windValue = 0;
            _funValue = 0;
            _waterValue = 0;
            _score = 0;
            _location = null;
            _photoUri = null;
            _duration = 0;
        }
    }
}