using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public abstract class BaseResponse : IActionResult
{
    public int StatusCode { get; set; }
    public object? Data { get; set; }

    // public BaseResponse(int statusCode, object? data)
    // {
    //     StatusCode = statusCode;
    //     Data = data;
    // }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(Data)
        {
            StatusCode = StatusCode
        };

        await objectResult.ExecuteResultAsync(context);
    }
}