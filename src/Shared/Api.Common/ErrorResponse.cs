using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Common
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Message = "An error occurred during api call execution.";
            Details = "N/A";
        }

        public ErrorResponse(Exception e)
        {
            Message = e.Message;
            Details = e.InnerException?.Message ?? "N/A";
        }

        public string Message { get; set; }
        public string Details { get; set; }

    }
}
