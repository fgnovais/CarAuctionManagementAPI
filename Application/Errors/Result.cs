namespace Application.Errors
{
    public class Result<T, TError>
    {
        private Result(T value, TError error, bool isSuccess)
        {
            _value = value;
            _error = error;
            this.IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public T _value { get; }
        public TError? _error { get; }
        public static Result<T, TError> Success(T value) => new(value, default!, true);
        public static Result<T, TError> Failure(TError error) => new(default!, error, false);
    }
}
