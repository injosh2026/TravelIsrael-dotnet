using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class AdminPendingTripsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public string Email { get; set; }
        public string Region { get; set; }
        public string Difficulty { get; set; }
        public string Type { get; set; }
        public int Stops { get; set; }
        public DateOnly? SubmittedDate { get; set; }
    }
}
