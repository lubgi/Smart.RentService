using Microsoft.AspNetCore.Mvc;

namespace Smart.RentService.WebAPI.Responses
{
    public class NotFoundResponse : ProblemDetails
    {
        public NotFoundResponse(string message)
        {
            Title = "Not Found";
            Status = StatusCodes.Status404NotFound;
            Detail = message;
        }
    }
}
