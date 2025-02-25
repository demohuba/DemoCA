using Microsoft.AspNetCore.Http;

namespace JEPCO.Shared.ModelsAbstractions;

public class Response
{
    public string Title { get; set; } = string.Empty;
    public int Status { get; set; } = (int)SuccessResponseEnum.Succeeded;

    public Response()
    {
    }

    public Response(SuccessResponseEnum responseStatus, string title)
    {
        Status = (int)responseStatus;
        Title = title;
    }
}

public class Response<T> : Response
{
    public T Data { get; set; }

    public Response(T data)
    {
        Data = data;
    }

    public Response(T data, string title)
    {
        Data = data;
        Title = title;
    }

    public Response(T data, SuccessResponseEnum responseStatus)
    {
        Data = data;
        Status = (int)responseStatus;
    }

    public Response(SuccessResponseEnum responseStatus, string title)
    {
        Status = (int)responseStatus;
        Title = title;
    }

    public Response(T data, SuccessResponseEnum responseStatus, string title)
    {
        Data = data;
        Status = (int)responseStatus;
        Title = title;
    }

}




public enum SuccessResponseEnum
{
    Succeeded = StatusCodes.Status200OK,
    Created = StatusCodes.Status201Created
}
