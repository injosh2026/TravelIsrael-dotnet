using Repository.Entities;
using Service.Dto;
using Service.Dto.DayTrip;
using Service.Dto.DayTripItem;
using Service.Dto.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IDayTripService
    {
        // --- מסלולים ---
        Task<List<DayTripDetaileDto>> GetAllAsync();
        Task<List<DayTripDto>> GetTopThreeOrderByNumberOfViewsAsync();
        Task<List<DayTripDto>> GetFilteredTripsAsync(TripFilterDto filter);
        Task<DayTripDetaileDto> GetByIdAsync(int id);
        // --- יצירה ועדכון ---
        Task<CalculateComputedFieldsResult> AddAsync(DayTripCreateDto dto);
        Task<CalculateComputedFieldsResult> UpdateAsync(int userId, int id, DayTripUpdateDto dto);
        // --- דירוג ---
        Task<DayTripDetaileDto> UpdateRatingAsync(int id, UpdateRatingDto dto);
        // --- אישור מסלול ---
        Task<DayTripDetaileDto> SetApprovalStatusAsync(int userId, int id, ApprovalStatus newStatus);
        Task<DayTripDetaileDto> ApproveDayTripWithItemsAsync(int adminId, int dayTripId, ApproveRequest request);
        // --- מחיקה ---
        Task<bool> DeleteAsync(int userId, int id);
        // --- ניהול מקומות במסלול ---
        Task<CalculateComputedFieldsResult> AddItemToDayTripAsync(int userId, int dayTripId, DayTripItemToAdd item);
        Task<CalculateComputedFieldsResult> ReplaceDayTripItemsAsync(int userId, int dayTripId, List<DayTripItemToAdd> newItems);
        Task<List<DayTripItemDto>> GetDayTripItemsAsync(int dayTripId);
        Task<CalculateComputedFieldsResult> UpdateOrderInDayTripAsync(int userId, int dayTripItemId, int newOrder);
        Task<CalculateComputedFieldsResult> DeleteDayTripItemAsync(int userId, int dayTripItemId);
    }
}

