using System.Collections.Generic;
using System.Threading.Tasks;

namespace KiteBrigade.Service.Interface
{
    public interface IGeocodingService
    {
        Task<string> GetLocation(double latitude, double longitude);
        Task<IList<string>> GetAutocompletePredictions(string query);
    }
}
