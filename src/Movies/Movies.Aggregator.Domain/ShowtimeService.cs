using Microsoft.Extensions.Logging;
using Movies.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Aggregator.Domain
{
    internal class ShowtimeService
    {
        private readonly ILogger<ShowtimeService> _logger;
        private readonly IMoviesClientGrpc _moviesClient;

        public ShowtimeService(ILogger<ShowtimeService> logger, IMoviesClientGrpc moviesClient)
        {
          _logger = logger;
          _moviesClient = moviesClient;
        }

        public async Task Create() {

          //polly?!
          var response =  await _moviesClient.GetById("");


        }
        

    }
}
