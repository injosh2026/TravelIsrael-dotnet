using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class ApproveRequest
    {
        public ApprovalStatus ApprovalStatus { get; set; }
        public string? RejectReason { get; set; }
    }
}
