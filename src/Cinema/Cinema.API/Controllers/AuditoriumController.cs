using Api.Common;
using Cinema.Application.Auditorium.Queries;
using Cinema.Application.Auditorium.Queries.Commands;

namespace Cinema.Api.Controllers;

[Route("v1/[controller]")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AuditoriumController : Controller
{
    private readonly IMediator _mediator;

    public AuditoriumController(IMediator mediator)

    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _mediator.Send(new GetEntitiesCommand()));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(e));
        }
    }
}