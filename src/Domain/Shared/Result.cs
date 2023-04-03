namespace Domain.Shared
{
    public class Result
    {
        public bool Succeeded { get; protected set; }
        public Error[] Errors { get; protected set; }

        protected Result(bool succeeded, Error[] errors)
        {
            Succeeded = succeeded;
            Errors = errors;
        }

        public static Result Success()
        {
            return new Result(true, Array.Empty<Error>());
        }

        public static Result Failure(params Error[] errors)
        {
            return new Result(false, errors);
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; protected set; }

        protected Result(bool succeeded, Error[] errors, T data) : base(succeeded, errors)
        {
            Data = data;
        }

        public static Result<T> Success(T data) => new Result<T>(true, Array.Empty<Error>(), data);


        public new static Result<T> Failure(params Error[] errors)
        {
            return new Result<T>(false, errors, default);
        }
    }
}
