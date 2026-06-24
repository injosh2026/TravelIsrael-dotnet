using Microsoft.AspNetCore.Mvc;
using Service.Dto.Suggestions;
using Service.Interface;

namespace ProjectTrips.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionsController : ControllerBase
    {
        private readonly ISuggestedStopsService service;

        public SuggestionsController(ISuggestedStopsService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> GetSuggestions([FromBody] SuggestedStopsRequestDto request)
        {
            var result = await service.GetSuggestedStopsAsync(request);

            return Ok(result);
        }
    }
}
