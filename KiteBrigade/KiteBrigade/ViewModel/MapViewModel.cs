using KiteBrigade.Model;
using KiteBrigade.Service.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace KiteBrigade.ViewModel
{
    public class MapViewModel : LoggedViewModel
    {
        protected readonly ISession _session;
        protected readonly IGeocodingService _geocodingService;

        protected KiteSession CurrentSession => _session.CurrentSession;

        public MapViewModel(ILogService logService, ISessionService sessionService, ISession session, IGeocodingService geocodingService) : base(logService, sessionService)
        {
            _session = session;
            _geocodingService = geocodingService;
        }

        private double? _longitude;
        public double? Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                Set(ref _longitude, value);
            }
        }

        private double? _latitude;
        public double? Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                Set(ref _latitude, value);
            }
        }

        private string _location;
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                Set(ref _location, value);
            }
        }

        public async Task SetPosition(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
            Location = await _geocodingService.GetLocation(Latitude.Value, Longitude.Value);
        }

        public void SaveLocation()
        {
            CurrentSession.Location = Location;
            CurrentSession.LocationLatitude = Latitude;
            CurrentSession.LocationLongitude = Longitude;
        }

        protected override void RaiseCanExecuteChanged()
        {
            //
        }

        protected override Task StartAsync(CancellationToken ct)
        {
            _latitude = CurrentSession.LocationLatitude;
            Longitude = CurrentSession.LocationLongitude;
            Location = CurrentSession.Location;
            return Task.FromResult(0);
        }
    }
}
