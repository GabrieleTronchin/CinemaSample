using Cinema.Api.Mapper;
using Cinema.Api.Models.Showtime;
using Cinema.Application.Commands;
using Cinema.Application.Queries.Showtime;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Cinema.Api.Controllers
{
    [Route("v1/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ShowtimeController : Controller
    {
        private readonly ILogger<ShowtimeController> _logger;
        private readonly IShowtimeQueries _showtimeQueries;
        private readonly IMediator _mediator;
        private readonly IApiMapperAccessor _mapper;

        public ShowtimeController(ILogger<ShowtimeController> logger,
                                  IShowtimeQueries showtimeQueries,
                                  IMediator mediator,
                                  IApiMapperAccessor apiMapperAccessor)
        {
            _logger = logger;
            _showtimeQueries = showtimeQueries;
            _mediator = mediator;
            _mapper = apiMapperAccessor;
        }



        [HttpGet("Single/{id:required}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> GetSingle([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _showtimeQueries.GetSingleAsync(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _showtimeQueries.GetAllAsync());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateShowTimeRequest payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _mediator.Send(_mapper.ApiMapper.Map<AssignShowtimeCommand>(payload));

                return CreatedAtAction(nameof(GetSingle), new { id = result.Id }, result);
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