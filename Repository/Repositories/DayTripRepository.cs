using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class DayTripRepository : IDayTripRepository
    {
        private readonly IContext _context;
        public DayTripRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<DayTrip> AddAsync(DayTrip item)
        {
            _context.DayTrips.Add(item);
            await _context.save();
            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item == null)
                return false;
            _context.DayTrips.Remove(item);
            await _context.save();
            return true;
        }

        public async Task<List<DayTrip>> GetAllAsync()
        {
            return await BaseQuery().ToListAsync();
        }

        public async Task<List<DayTrip>> GetTopThreeOrderByNumberOfViewsAsync()
        {
            return await _context.DayTrips
                .Include(x => x.Reviews)
                .Include(x => x.Region)
                .Include(x => x.DayTripItems)
                .Where(d => d.ApprovalStatus == ApprovalStatus.Approved)
                .OrderByDescending(d => d.NumberOfViews)
                .Take(3)
                .ToListAsync();
        }

        public IQueryable<DayTrip> GetFilteredDayTrips()
        {
            return _context.DayTrips
                .Include(x => x.Reviews)
                .Include(x => x.Region)
                .Include(x => x.DayTripItems)
                .Where(d => d.ApprovalStatus == ApprovalStatus.Approved)
                .AsNoTracking()
                .AsQueryable();
        }

        public async Task<DayTrip?> GetByIdAsync(int id)
        {
            return await BaseQuery()
                //.AsNoTracking()
                .AsTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        //public async Task<DayTrip?> GetByIdAsync(int id)
        //{
        //    var trip = await _context.DayTrips
        //        .Include(x => x.Type)
        //        .Include(x => x.Region)
        //        .Include(x => x.Reviews).ThenInclude(d =>d.User)
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(x=>x.Id == id);

        //    if(trip == null)
        //        return null;

        //    await _context.DayTripItems
        //        .Where(i => i.DayTripId == id)
        //        .Include(x => x.Place).ThenInclude(d => d.Images)
        //        .Include(x => x.Place).ThenInclude(d => d.Type)
        //        .Include(x => x.Place).ThenInclude(d => d.Region)
        //        .Include(x => x.Route).ThenInclude(d => d.Images)
        //        .Include(x => x.Route).ThenInclude(d => d.RoutePoints)
        //        .Include(x => x.Route).ThenInclude(d => d.Type)
        //        .Include(x => x.Route).ThenInclude(d => d.Region)
        //        .AsNoTracking()
        //        .ToListAsync();

        //    return trip;

        //}

        public async Task<DayTrip> UpdateAsync(DayTrip item)
        {
            _context.DayTrips.Update(item);
            await _context.save();
            return item;
        }

        public async Task<bool> ExistsByHashAsync(int currentTripId, string hash)
        {
            return await _context.DayTrips.AnyAsync(t => t.Id != currentTripId && t.TripHash == hash);
        }

        public async Task<List<DayTrip>> GetByUserIdAsync(int userId)
        {
            return await _context.DayTrips.Where(t => t.CreatedByUserId == userId).ToListAsync();
        }

        private IQueryable<DayTrip> BaseQuery()
        {
            return _context.DayTrips
                .Include(x => x.Reviews)
                    .ThenInclude(d => d.User)
                .Include(x => x.Type)
                .Include(x => x.Region)
                .Include(x => x.DayTripItems)
                    .ThenInclude(i => i.Place)
                        .ThenInclude(j => j.Images)
                .Include(x => x.DayTripItems)
                    .ThenInclude(i => i.Place)
                        .ThenInclude(j => j.Type)
                .Include(x => x.DayTripItems)
                    .ThenInclude(i => i.Place)
                        .ThenInclude(j => j.Region)
                .Include(x => x.DayTripItems)
                    .ThenInclude(i => i.Route)
                        .ThenInclude(j => j.Images)
                .Include(x => x.DayTripItems)
                    .ThenInclude(i => i.Route)
                        .ThenInclude(j => j.RoutePoints)
                .Include(x => x.DayTripItems)
                    .ThenInclude(i => i.Route)
                        .ThenInclude(j => j.Type)
                .Include(x => x.DayTripItems)
                    .ThenInclude(i => i.Route)
                        .ThenInclude(j => j.Region);
        }


        public async Task<List<DayTrip>> GetFilteredTripsAsync()
        {
            return await BaseQuery()
                .Where(t => t.ApprovalStatus == ApprovalStatus.Approved)
                .ToListAsync();
        }

        public async Task<int> GetTotalTripsAsync()
        {
            return await _context.DayTrips.CountAsync(t => t.ApprovalStatus != ApprovalStatus.Draft);
        }
        public async Task<int> GetPendingTripsAsync()
        {
            return await _context.DayTrips.CountAsync(t => t.ApprovalStatus == ApprovalStatus.Pending);
        }

        public async Task<List<DayTrip>> GetAllTripsForAdminAsync()
        {
            return await _context.DayTrips
                .Include(x => x.User)
                .Include(x => x.Region)
                .Include(x => x.Type)
                .Include(x => x.DayTripItems)
                .Where(t => t.ApprovalStatus != ApprovalStatus.Draft)
                .ToListAsync();
        }
        public async Task<List<DayTrip>> GetPendingTripsForAdminAsync()
        {
            return await _context.DayTrips
                .Include(x => x.User)
                .Include(x => x.Region)
                .Include(x => x.Type)
                .Include(x => x.DayTripItems)
                .Where(t => t.ApprovalStatus == ApprovalStatus.Pending)
                .ToListAsync();
        }
    }
}