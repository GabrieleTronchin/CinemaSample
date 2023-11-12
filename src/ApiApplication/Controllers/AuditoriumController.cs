using Cinema.Application.Auditorium.Queries;

namespace Cinema.Api.Controllers;

[Route("v1/[controller]")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AuditoriumController : Controller
{
    private readonly IAuditoriumQueries _readModel;

    public AuditoriumController(IAuditoriumQueries readModel)

    {
        _readModel = readModel;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _readModel.GetAllAsync());
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
}