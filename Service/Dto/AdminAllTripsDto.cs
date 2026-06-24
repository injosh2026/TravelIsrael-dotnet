using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class AdminAllTripsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public string Email { get; set; }
        public string? Region { get; set; }
        public string Status { get; set; }
        public string? RejectReason { get; set; }
        public string Type { get; set; }
        public int Stops { get; set; }
        public double Rating { get; set; }
        public int Saves { get; set; }
        public int Views { get; set; }
        public string? Popularity { get; set; }
    }
}
