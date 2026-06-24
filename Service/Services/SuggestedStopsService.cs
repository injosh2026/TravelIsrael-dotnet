using Repository.Entities;
using Repository.Interfaces;
using Service.Dto.DayTripItem;
using Service.Dto.Suggestions;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SuggestedStopsService : ISuggestedStopsService
    {
        private readonly ISuggestedStopsRepository _repository;

        public SuggestedStopsService(
            ISuggestedStopsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SuggestedStopDto>> GetSuggestedStopsAsync(
            SuggestedStopsRequestDto request)
        {
            var places = await _repository.GetPlacesAsync();
            var routes = await _repository.GetRoutesAsync();

            var existingPlaceIds = request.CurrentStops
                .Where(x => x.ItemType == ItemType.Place)
                .Select(x => x.ItemId)
                .ToHashSet();

            var existingRouteIds = request.CurrentStops
                .Where(x => x.ItemType == ItemType.Route)
                .Select(x => x.ItemId)
                .ToHashSet();

            var selectedCoordinates = await GetAllTripCoordinatesAsync(request.CurrentStops);

            var suggestions = new List<SuggestedStopDto>();

            foreach (var place in places)
            {
                if (existingPlaceIds.Contains(place.Id))
                    continue;

                var distanceScore =
                    CalculateAverageDistanceScore(
                        selectedCoordinates,
                        place.Latitude,
                        place.Longitude);

                var popularityScore =
                    CalculatePopularityScore(
                        place.NumberOfViews);

                var ratingScore =
                    CalculateRatingScore(
                        place.AverageRating,
                        place.RatingsCount);

                var finalScore =
                    distanceScore * 0.6 +
                    popularityScore * 0.2 +
                    ratingScore * 0.2;

                suggestions.Add(new SuggestedStopDto
                {
                    ItemId = place.Id,
                    ItemType = ItemType.Place,
                    Name = place.Name,
                    Latitude = place.Latitude,
                    Longitude = place.Longitude,
                    EndLatitude = null, 
                    EndLongitude = null,
                    TypeName = place.Type.TypeName,
                    RegionName = place.Region.RegionName,
                    Difficulty = null,
                    DistanceScore = distanceScore,
                    PopularityScore = popularityScore,
                    RatingScore = ratingScore,
                    Score = finalScore,
                    EstimatedDuration = place.AverageStayMinutes,
                    MainImage = fromStringToByte(place.Images
                        .FirstOrDefault(x => x.IsMain)
                        ?.ImageUrl)
                });
            }

            foreach (var route in routes)
            {
                if (existingRouteIds.Contains(route.Id))
                    continue;

                var centerLat =
                    (route.StartLatitude + route.EndLatitude) / 2;

                var centerLng =
                    (route.StartLongitude + route.EndLongitude) / 2;

                var distanceScore =
                    CalculateAverageDistanceScore(
                        selectedCoordinates,
                        centerLat,
                        centerLng);

                var popularityScore =
                    CalculatePopularityScore(
                        route.NumberOfViews);

                var ratingScore =
                    CalculateRatingScore(
                        route.AverageRating,
                        route.RatingsCount);

                var finalScore =
                    distanceScore * 0.6 +
                    popularityScore * 0.2 +
                    ratingScore * 0.2;

                suggestions.Add(new SuggestedStopDto
                {
                    ItemId = route.Id,
                    ItemType = ItemType.Route,
                    Name = route.Name,
                    Latitude = route.StartLatitude,
                    Longitude = route.StartLatitude,
                    EndLatitude = route.EndLatitude,
                    EndLongitude = route.EndLongitude,
                    TypeName = route.Type.TypeName,
                    RegionName = route.Region.RegionName,
                    Difficulty = TranslateDifficulty(route.Difficulty),
                    DistanceScore = distanceScore,
                    PopularityScore = popularityScore,
                    RatingScore = ratingScore,
                    Score = finalScore,
                    EstimatedDuration = route.DurationMinutes,
                    MainImage = fromStringToByte(route.Images
                        .FirstOrDefault(x => x.IsMain)
                        ?.ImageUrl)
                });
            }

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                suggestions = suggestions
                    .Where(x =>
                        x.Name.Contains(
                            request.Search,
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (request.RegionId.HasValue)
            {
                suggestions = suggestions
                    .Where(x =>
                        x.RegionName != null)
                    .ToList();
            }

            return suggestions
                .OrderByDescending(x => x.Score)
                .Take(request.Limit)
                .ToList();
        }

        private async Task<List<(double lat, double lng)>>
    GetAllTripCoordinatesAsync(
        List<CurrentTripStopDto> items)
        {
            var result = new List<(double lat, double lng)>();

            var placeIds = items
                .Where(x => x.ItemType == ItemType.Place)
                .Select(x => x.ItemId)
                .ToList();

            var routeIds = items
                .Where(x => x.ItemType == ItemType.Route)
                .Select(x => x.ItemId)
                .ToList();

            var places = await _repository
                .GetPlacesByIdsAsync(placeIds);

            var routes = await _repository
                .GetRoutesByIdsAsync(routeIds);

            foreach (var place in places)
            {
                result.Add((
                    place.Latitude,
                    place.Longitude));
            }

            foreach (var route in routes)
            {
                result.Add((
                    route.StartLatitude,
                    route.StartLongitude));

                result.Add((
                    route.EndLatitude,
                    route.EndLongitude));
            }

            return result;
        }

        private double CalculateAverageDistanceScore(
            List<(double lat, double lng)> points,
            double targetLat,
            double targetLng)
        {
            if (!points.Any())
                return 50;

            var avgDistance = points.Average(point =>
                CalculateDistanceKm(
                    point.lat,
                    point.lng,
                    targetLat,
                    targetLng));

            return Math.Max(0, 100 - avgDistance);
        }

        private double CalculatePopularityScore(
            int views)
        {
            return Math.Min(100, views / 100.0);
        }

        private double CalculateRatingScore(
            double? rating,
            int? ratingsCount)
        {
            if (rating == null || ratingsCount == null)
                return 0;

            return rating.Value * 20;
        }

        private double CalculateDistanceKm(
            double lat1,
            double lon1,
            double lat2,
            double lon2)
        {
            const double R = 6371;

            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            var a =
                Math.Sin(dLat / 2) *
                Math.Sin(dLat / 2) +
                Math.Cos(DegreesToRadians(lat1)) *
                Math.Cos(DegreesToRadians(lat2)) *
                Math.Sin(dLon / 2) *
                Math.Sin(dLon / 2);

            var c =
                2 * Math.Atan2(
                    Math.Sqrt(a),
                    Math.Sqrt(1 - a));

            return R * c;
        }

        private double DegreesToRadians(
            double deg)
        {
            return deg * Math.PI / 180;
        }

        public byte[] fromStringToByte(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;

            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Images",
                fileName
            );

            if (!File.Exists(path))
                return null;

            return File.ReadAllBytes(path);
        }

        private string TranslateDifficulty(Difficulty value)
        {
            return value switch
            {
                Difficulty.Easy => "קל",
                Difficulty.EasyMedium => "קל-בינוני",
                Difficulty.Medium => "בינוני",
                Difficulty.MediumHard => "בינוני-קשה",
                Difficulty.Hard => "קשה",
                _ => value.ToString()
            };
        }
    }
}
