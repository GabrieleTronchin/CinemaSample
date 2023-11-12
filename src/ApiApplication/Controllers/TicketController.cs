using Cinema.Api.Models.ConfirmReservation;
using Cinema.Api.Models.SeatReservation;
using Cinema.Application.Ticket.Commands;
using MassTransit;

namespace Cinema.Api.Controllers;

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

            var response = await _mediator.Send(_mapper.ApiMapper.Map<ReservationCommand>(payload));

            var apiResult = _mapper.ApiMapper.Map<SeatReservationResponse>(response);
            return StatusCode(StatusCodes.Status201Created, apiResult);

        }
        catch (ArgumentNullException e)
        {
            _logger.LogError($"{nameof(Post)}", e);

            return StatusCode(StatusCodes.Status400BadRequest, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError($"{nameof(Post)}", e);

            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
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

            var response = await _mediator.Send(_mapper.ApiMapper.Map<ReservationConfirmationCommand>(payload));

            return StatusCode(StatusCodes.Status202Accepted);

        }
        catch (ArgumentNullException e)
        {
            _logger.LogError($"{nameof(Put)}", e);

            return StatusCode(StatusCodes.Status400BadRequest, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError($"{nameof(Put)}", e);

            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}