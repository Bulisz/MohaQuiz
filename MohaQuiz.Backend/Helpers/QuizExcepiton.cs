using System.Net;

namespace MohaQuiz.Backend.Helpers;

public class QuizException : Exception
{
    public HttpStatusCode StatusCode { get; init; }
    public QuizException(HttpStatusCode statusCode, string? message) : base(message)
    {
        StatusCode = statusCode;
    }
}
