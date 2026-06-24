using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ISuggestedStopsRepository
    {
        Task<List<Place>> GetPlacesAsync();
        Task<List<Route>> GetRoutesAsync();
        Task<List<Place>> GetPlacesByIdsAsync(List<int> ids);
        Task<List<Route>> GetRoutesByIdsAsync(List<int> ids);
    }
}
