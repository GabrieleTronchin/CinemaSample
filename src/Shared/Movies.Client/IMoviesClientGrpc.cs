using ProtoDefinitions;

namespace Movies.Client
{
    public interface IMoviesClientGrpc
    {
        Task<showResponse> GetById(string id);
    }
}