using ShowTimeProto;

namespace Cinema.Client
{
    public interface ICinemaClientGrpc
    {
        Task<responseModel> CreateShowTime(ShowtimeCreationRequest request);
    }
}