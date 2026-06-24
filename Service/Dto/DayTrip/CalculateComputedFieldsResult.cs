using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.DayTrip
{
    public class CalculateComputedFieldsResult
    {
        public DayTripDetaileDto DayTrip { get; set; }
        public ScheduleResult ScheduleResult { get; set; }
    }
}
