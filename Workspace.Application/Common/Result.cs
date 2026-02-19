namespace Workspace.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Data { get; private set; }
        public Error? Error { get; private set; }

        private Result(T? data)
        {
            IsSuccess = true;
            Data = data;
            Error = null;
        }
        private Result(Error? error)
        {
            IsSuccess = false;
            Data = default(T);
            Error = error;
        }

        public static Result<T> Success(T data) => new Result<T>(data);
        public static Result<T> Failure(Error error) => new Result<T>(error);

        public static implicit operator Result<T>(T data) => Success(data);
        public static implicit operator Result<T>(Error error) => Failure(error);
    }
}