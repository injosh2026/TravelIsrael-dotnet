using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class AdminStatsDto
    {
        public int PendingTrips { get; set; }   
        public int totalTrips { get; set; }   
        public int places { get; set; }   
        public int users { get; set; }   
    }
}
