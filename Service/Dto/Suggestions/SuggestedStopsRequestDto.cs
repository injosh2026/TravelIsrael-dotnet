using Service.Dto.DayTripItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.Suggestions
{
    public class SuggestedStopsRequestDto
    {
        public List<CurrentTripStopDto> CurrentStops { get; set; } = [];

        public string? Search { get; set; }

        public int? RegionId { get; set; }

        public int Limit { get; set; } = 10;
    }
}
