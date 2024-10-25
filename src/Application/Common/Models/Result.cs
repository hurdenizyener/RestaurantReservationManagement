using System.Net;
using System.Text.Json.Serialization;

namespace Application.Common.Models;

public sealed class Result
{

    [JsonIgnore]
    public HttpStatusCode Status { get; private set; }

    [JsonIgnore]
    public bool IsSuccess { get; private set; }

    public static Result Success(HttpStatusCode status = HttpStatusCode.OK) =>
              new()
              {
                  Status = status,
                  IsSuccess = true
              };

}