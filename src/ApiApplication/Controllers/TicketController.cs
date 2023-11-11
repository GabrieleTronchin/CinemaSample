using Cinema.Api.Mapper;
using Cinema.Api.Models.ConfirmReservation;
using Cinema.Api.Models.SeatReservation;
using Cinema.Application.Commands;

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
    public class TicketController : Controller
    {
        private readonly ILogger<TicketController> _logger;
        private readonly IMediator _mediator;
        private readonly IApiMapperAccessor _mapper;

        public TicketController(ILogger<TicketController> logger,
                                  IMediator mediator,
                                  IApiMapperAccessor apiMapperAccessor)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = apiMapperAccessor;
        }



        [HttpGet("Single/{id:required}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<SeatReservationResponse>> Post([FromBody] SeatReservationRequest payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _mediator.Send(_mapper.ApiMapper.Map<ReservationCommand>(payload));

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



        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<SeatReservationResponse>> Put([FromBody] ConfirmReservationRequest payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _mediator.Send(_mapper.ApiMapper.Map<ReservationConfirmationCommand>(payload));

                if (!result.Success)
                    throw new InvalidOperationException("An error occurred completing your payment.");


                return Accepted();

            }
            catch (ArgumentNullException e)
            {
                _logger.LogError($"{nameof(Put)}", e);

                return StatusCode(StatusCodes.Status400BadRequest, e);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Put)}", e);

                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}