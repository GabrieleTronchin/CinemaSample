using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Database.Repositories.Abstractions;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace ShowTimeProto
{

    public class ShowtimeServer  
    {
        private readonly IShowtimesRepository _showtimesRepository;

        public ShowtimeServer(IShowtimesRepository showtimesRepository)
        {
            _showtimesRepository = showtimesRepository;
        }

        public  async Task CreateShowtime()
        {
            throw new NotImplementedException();
        }
    }

}
