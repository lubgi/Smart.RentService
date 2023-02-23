using Microsoft.AspNetCore.Mvc;

namespace Smart.RentService.WebAPI.Responses
{
    public class UnprocessableEntityResponse : ProblemDetails
    {
        public UnprocessableEntityResponse(string message)
        {
            Title = "Unprocessable Entity";
            Status = StatusCodes.Status422UnprocessableEntity;
            Detail = message;
        }
    }
}
