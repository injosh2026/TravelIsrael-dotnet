using Service.Dto.Suggestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ISuggestedStopsService
    {
        Task<List<SuggestedStopDto>> GetSuggestedStopsAsync(SuggestedStopsRequestDto request);
    }
}
