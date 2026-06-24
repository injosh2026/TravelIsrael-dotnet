using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Dto;
using Service.Dto.User;
using Service.Interface;
using System.Security.Claims;

namespace ProjectTrips.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService service;

        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        // ===== Helper =====
        private int? CurrentUserId
        {
            get
            {
                var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(claim, out var id))
                    return null;
                return id;
            }
        }

        [HttpGet("stats")]
        [Authorize]
        public async Task<ActionResult<AdminStatsDto>> GetAsync()
        {
            var userId = CurrentUserId;
            if (userId == null)
                return Unauthorized();
            try
            {
                var adminStats = await service.GetStatsForAdminAsync(userId ?? 0);
                return Ok(adminStats);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"User with id {userId} not found.");
            }
        }


        [HttpGet("trips")]
        [Authorize]
        public async Task<ActionResult<AdminAllTripsDto>> GetAllTripsAsync()
        {
            var userId = CurrentUserId;
            if (userId == null)
                return Unauthorized();
            try
            {
                var adminAllTrips = await service.GetAllTripsForAdminAsync(userId ?? 0);
                return Ok(adminAllTrips);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"User with id {userId} not found.");
            }
        }

        [HttpGet("pending-trips")]
        [Authorize]
        public async Task<ActionResult<AdminPendingTripsDto>> GetPendingTripsAsync()
        {
            var userId = CurrentUserId;
            if (userId == null)
                return Unauthorized();
            try
            {
                var adminPendingTrips = await service.GetPendingTripsForAdminAsync(userId ?? 0);
                return Ok(adminPendingTrips);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"User with id {userId} not found.");
            }
        }

        [HttpGet("places-and-routes")]
        [Authorize]
        public async Task<ActionResult<AdminPlacesAndRoutesDto>> GetPlacesAndRoutesAsync()
        {
            var userId = CurrentUserId;
            if (userId == null)
                return Unauthorized();
            try
            {
                var adminPlacesAndRoutes = await service.GetAllPlacesAndRoutesForAdminAsync(userId ?? 0);
                return Ok(adminPlacesAndRoutes);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"User with id {userId} not found.");
            }
        }
    }
}
