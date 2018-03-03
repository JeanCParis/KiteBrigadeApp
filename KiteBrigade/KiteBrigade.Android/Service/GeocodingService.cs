using Android.Locations;
using Java.Util;
using KiteBrigade.Service.Interface;
using Plugin.CurrentActivity;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace KiteBrigade.Droid.Service
{
    public class GeocodingService : IGeocodingService
    {
        Geocoder _geocoder;

        public GeocodingService()
        {
            _geocoder = new Geocoder(CrossCurrentActivity.Current.Activity, Locale.Default);
        }

        public Task<IList<string>> GetAutocompletePredictions(string query)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetLocation(double latitude, double longitude)
        {
            IList<Address> addresses =  await _geocoder.GetFromLocationAsync(latitude, longitude, 1);
            return addresses?[0]?.SubLocality ?? addresses?[0]?.Locality ?? new StringBuilder().Append(latitude.ToString("00.0")).Append("°").Append(longitude.ToString("00.0")).Append("°").ToString();
        }
    }
}