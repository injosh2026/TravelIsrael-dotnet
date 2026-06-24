using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SuggestedStopsRepository : ISuggestedStopsRepository
    {
        private readonly IContext _context;

        public SuggestedStopsRepository(IContext context)
        {
            _context = context;
        }

        public async Task<List<Place>> GetPlacesAsync()
        {
            return await _context.Places
                .Include(x => x.Region)
                .Include(x => x.Type)
                .Include(x => x.Images)
                .ToListAsync();
        }

        public async Task<List<Route>> GetRoutesAsync()
        {
            return await _context.Routes
                .Include(x => x.Region)
                .Include(x => x.Type)
                .Include(x => x.Images)
                .ToListAsync();
        }

        public async Task<List<Place>> GetPlacesByIdsAsync(List<int> ids)
        {
            return await _context.Places
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<List<Route>> GetRoutesByIdsAsync(List<int> ids)
        {
            return await _context.Routes
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }
    }
}
