using Cinema.Persistence.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class TicketController : Controller
    {
        private readonly ILogger<ShowtimeController> _logger;
        private readonly ITicketsRepository _ticketsRepository;

        public TicketController(ILogger<ShowtimeController> logger,
                                ITicketsRepository ticketsRepository)
        {
            _logger = logger;
            _ticketsRepository = ticketsRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] object payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //_ticketsRepository.CreateAsync()

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