

using System.Net;
using System.Text.Json;
using Backend.Domain;

namespace Backend.Configuration
{

  public class ErrorHandlerMiddleware
  {
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
      this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await next(context);
      }
      catch (Exception ex)
      {
        await HandleExceptionAsync(context, ex);
      }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
      string? result = null;
      switch (ex)
      {
        case DomainException dex:
          context.Response.StatusCode = (int)dex.Code;
          result = JsonSerializer.Serialize(new
          {
            errors = dex.Message
          });
          break;

        case Exception e:
          context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
          result = JsonSerializer.Serialize(new
          {
            errors = "Internal_Server_error"
          });
          break;
      }

      context.Response.ContentType = "application/json";
      await context.Response.WriteAsync(result ?? "{}");
    }

    private readonly RequestDelegate next;
  }

}