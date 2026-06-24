using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.Suggestions
{
    public class SuggestedStopDto
    {
        public int ItemId { get; set; }
        public ItemType ItemType { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? EndLatitude { get; set; }
        public double? EndLongitude { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string RegionName { get; set; } = string.Empty;
        public string? Difficulty { get; set; } = string.Empty;
        public double Score { get; set; }
        public double DistanceScore { get; set; }
        public double PopularityScore { get; set; }
        public double RatingScore { get; set; }
        public double? EstimatedDuration { get; set; }
        public byte[]? MainImage { get; set; }
    }
}
