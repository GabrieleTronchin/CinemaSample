using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using MoviesAggregator.Models;
using Movies.Aggregator.Domain;

namespace MoviesAggregator.Controllers
{
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ShowtimeController : Controller
    {
        private readonly ILogger<ShowtimeController> _logger;
        private readonly IShowtimeService _showtimeService;

        public ShowtimeController(ILogger<ShowtimeController> logger,
                                  IShowtimeService showtimeService)
        {
            _logger = logger;
            _showtimeService = showtimeService;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateShowTime payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _showtimeService.Create(new Movies.Aggregator.Domain.Models.CreateShowTime() { ImdbId=payload.ImdbId, AuditoriumId= payload.AuditoriumId, SessionDate= payload.SessionDate });

                return Created("", payload);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError($"{nameof(Post)}", e);

                return StatusCode(StatusCodes.Status400BadRequest, e);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Post)}", e);

                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}