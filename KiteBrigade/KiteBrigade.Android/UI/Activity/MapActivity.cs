using Android.OS;
using Android.Support.V7.Widget;
using TextView = Android.Widget.TextView;
using KiteBrigade.Resx;
using KiteBrigade.ViewModel;
using Android.App;
using Android.Content.PM;
using Android.Views;
using System;
using Android.Gms.Maps;
using static Android.Gms.Maps.GoogleMap;
using Android.Gms.Maps.Model;
using Android.Text;
using Java.Lang;

namespace KiteBrigade.Droid.UI.Activity
{
    [Activity(Label = "MapActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MapActivity : LoggedActivity<MapViewModel>, IOnMapReadyCallback
    {
        private View _backButton, _confirmButton, _loader;
        private AppCompatAutoCompleteTextView _location;
        private MapFragment _mapFragment;
        private GoogleMap _map;

        protected override int LayoutId => Resource.Layout.activity_map;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            _backButton = FindViewById(Resource.Id.button_back);
            _confirmButton = toolbar.FindViewById(Resource.Id.button_confirm);
            _loader = FindViewById(Resource.Id.loader);
            _location = FindViewById<AppCompatAutoCompleteTextView>(Resource.Id.edit_search);

            GoogleMapOptions mapOptions = new GoogleMapOptions()
              .InvokeMapType(GoogleMap.MapTypeSatellite)
              .InvokeZoomControlsEnabled(false)
              .InvokeCompassEnabled(true);

            FragmentTransaction fragTx = FragmentManager.BeginTransaction();
            _mapFragment = MapFragment.NewInstance(mapOptions);
            fragTx.Add(Resource.Id.fragment_map, _mapFragment, "map");
            fragTx.Commit();
            _mapFragment.GetMapAsync(this);

            toolbar.FindViewById<TextView>(Resource.Id.title).Text = AppResources.Location;
            (_confirmButton as TextView).Text = AppResources.Ok;
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;
            _map.MapClick += MapClickHandler;
            SetMarker();
            SetLocation();
            _loader.Visibility = ViewStates.Gone;
        }

        private async void MapClickHandler(object sender, MapClickEventArgs args)
        {
            await ViewModel.SetPosition(args.Point.Longitude, args.Point.Latitude);
        }

        protected override void SubscribeToViewEvents()
        {
            base.SubscribeToViewEvents();

            _backButton.Click += Back;
            _confirmButton.Click += SaveLocation;
        }

        protected override void UnsubscribeFromViewEvents()
        {
            base.UnsubscribeFromViewEvents();

            _backButton.Click -= Back;
            _confirmButton.Click -= SaveLocation;
        }

        protected override void OnViewModelPropertyChanged(string propertyName)
        {
            base.OnViewModelPropertyChanged(propertyName);
            switch (propertyName)
            {   
                case nameof(ViewModel.Latitude):
                case nameof(ViewModel.Longitude):
                    if (_map != null)
                    {
                        SetMarker();
                    }
                    break;
                case nameof(ViewModel.Location):
                    SetLocation();
                    break;
            }
        }

        private void SetMarker()
        {
            if (ViewModel.Longitude.HasValue && ViewModel.Latitude.HasValue)
            {
                _map?.Clear();
                _map?.AddMarker(new MarkerOptions().SetPosition(new LatLng(ViewModel.Latitude.Value, ViewModel.Longitude.Value)));
            }
        }

        private void SetLocation()
        {
            _location.Text = ViewModel.Location;
        }

        private void Back(object sender, EventArgs args)
        {
            OnBackPressed();
        }

        protected void SaveLocation(object sender, EventArgs args)
        {
            ViewModel.SaveLocation();
            Finish();
        }
    }

    public class LocationWatcher : Java.Lang.Object, ITextWatcher
    {
        public void AfterTextChanged(IEditable s)
        {
            //ViewModel.GetAutoCompleteResult(s.toString().trim(), true);
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            throw new NotImplementedException();
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            throw new NotImplementedException();
        }
    }
}