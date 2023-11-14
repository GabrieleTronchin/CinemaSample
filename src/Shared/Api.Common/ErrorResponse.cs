namespace Api.Common
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Message = "An error occurred during api call execution.";
        }

        public ErrorResponse(Exception e)
        {
            Message = e.Message;
        }

        public string Message { get; set; }
    }
}
