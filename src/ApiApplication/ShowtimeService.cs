using Cinema.Api.Mapper;
using Cinema.Application.Commands;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using ShowTimeProto;
using System;
using System.Threading.Tasks;

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

                await _mediator.Send(_mapper.ApiMapper.Map<AssignShowtimeCommand>(request));


                _logger.LogDebug("Request completed {grpcServiceName}", nameof(ShowtimeService));

                return new responseModel() { Success = true };
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
