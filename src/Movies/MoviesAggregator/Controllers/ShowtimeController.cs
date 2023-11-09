﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using MoviesAggregator.Models;

namespace MoviesAggregator.Controllers
{
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ShowtimeController : Controller
    {
        private readonly ILogger<ShowtimeController> _logger;

        public ShowtimeController(ILogger<ShowtimeController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateShowTime payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }



                return Created("", payload);
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
}