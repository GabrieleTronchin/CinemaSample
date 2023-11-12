using Cinema.Api.Models.Showtime;
using Cinema.Application.Showtime.Commands;

namespace Cinema.Api.Controllers;

[Route("v1/[controller]")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class ShowtimeController : Controller
{
    private readonly ILogger<ShowtimeController> _logger;
    private readonly IMediator _mediator;
    private readonly IApiMapperAccessor _mapper;

    public ShowtimeController(ILogger<ShowtimeController> logger,
                              IMediator mediator,
                              IApiMapperAccessor apiMapperAccessor)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = apiMapperAccessor;
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

            var response = await _mediator.Send(_mapper.ApiMapper.Map<CreateShowtimeCommand>(payload));

            return StatusCode(StatusCodes.Status201Created, response);
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