using System.Net;

namespace JEPCO.Application.Exceptions;

public class CustomException : Exception
{
    /// <summary>
    /// Status Code
    /// </summary>
    public int Status { get; set; }
    /// <summary>
    /// Message to bre previewed to the user
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Status Code Title or URL
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// More Details
    /// </summary>
    public string? Detail { get; set; }

    public CustomException(FailResponseStatus failResponseStatus, string message)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        switch (failResponseStatus)
        {
            case FailResponseStatus.Unauthorized: statusCode = HttpStatusCode.Unauthorized; break;
            case FailResponseStatus.Forbidden: statusCode = HttpStatusCode.Forbidden; break;
            case FailResponseStatus.BadRequest: statusCode = HttpStatusCode.BadRequest; break;
            case FailResponseStatus.NotFound: statusCode = HttpStatusCode.NotFound; break;
            case FailResponseStatus.Conflict: statusCode = HttpStatusCode.Conflict; break;
            case FailResponseStatus.Failed: break;
        }

        Status = (int)statusCode;
        Type = statusCode.ToString();
        Title = message;
    }
}



public enum FailResponseStatus
{
    Unauthorized,
    Forbidden,
    BadRequest,
    NotFound,
    Failed,
    Conflict
}
