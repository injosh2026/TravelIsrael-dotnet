using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAdminService
    {
        Task<AdminStatsDto> GetStatsForAdminAsync(int adminId);
        Task<List<AdminAllTripsDto>> GetAllTripsForAdminAsync(int adminId);
        Task<List<AdminPendingTripsDto>> GetPendingTripsForAdminAsync(int adminId);
        Task<List<AdminPlacesAndRoutesDto>> GetAllPlacesAndRoutesForAdminAsync(int adminId);
    }
}
