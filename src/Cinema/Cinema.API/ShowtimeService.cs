using Cinema.Application.Showtime.Commands;
using Grpc.Core;
using ShowTimeProto;

namespace Cinema.Api
{

    public class ShowtimeService : ShowTimeApi.ShowTimeApiBase
    {
        private readonly ILogger<ShowtimeService> _logger;
        private readonly IMediator _mediator;
        private readonly IApiMapperAccessor _mapper;

        public ShowtimeService(ILogger<ShowtimeService> logger,
                               IMediator mediator,
                               IApiMapperAccessor apiMapperAccessor)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = apiMapperAccessor;
        }

        public override async Task<responseModel> CreateShowTime(ShowtimeCreationRequest request, ServerCallContext context)
        {
            try
            {
                _logger.LogDebug("New Request received on {grpcServiceName}", nameof(ShowtimeService));

                var response =  await _mediator.Send(_mapper.ApiMapper.Map<CreateShowtimeCommand>(request));

                _logger.LogDebug("Request completed {grpcServiceName}", nameof(ShowtimeService));

                return new responseModel() { Success = true, ShotimeId = response.Id.ToString() };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at {grpcServiceName}", nameof(ShowtimeService));
                var errorRetModel = new responseModel() { Success = false };
                errorRetModel.Exceptions.Add(new moviesApiException() { Message = ex.Message, StatusCode = 500 });
                return errorRetModel;
            }
        }
    }

}
