using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.Suggestions
{
    public class CurrentTripStopDto
    {
        public int ItemId { get; set; }

        public ItemType ItemType { get; set; }

        public int OrderInTrip { get; set; }
    }
}
