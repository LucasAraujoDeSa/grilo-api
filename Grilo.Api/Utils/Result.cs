namespace Grilo.Api.Utils
{
    public class Result<T>
    {
        public string? Message { get; set; }
        public T? Content { get; set; }
        public bool IsSuccess { get; set; }
        public required string Status { get; set; }

        public static Result<T> Created(T content, string message)
        {
            return new Result<T> { IsSuccess = true, Content = content, Message = message, Status = "CREATED" };
        }

        public static Result<T> NotFound(string message)
        {
            return new Result<T> { IsSuccess = false, Content = default, Message = message, Status = "NOT_FOUND" };
        }

        public static Result<T> Ok(T content, string message)
        {
            return new Result<T> { IsSuccess = true, Content = content, Message = message, Status = "OK" };
        }

        public static Result<T> OperationalError(string message)
        {
            return new Result<T> { IsSuccess = false, Content = default, Message = message, Status = "OPERATIONAL_ERROR" };
        }

        public static Result<T> InternalError(string message)
        {
            return new Result<T> { IsSuccess = false, Content = default, Message = message, Status = "INTERNAL_ERROR" };
        }
    }
}