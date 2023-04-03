using Domain.Shared;

namespace Merc_Vista.Tests.Fixtures
{
    public class ResultFixture<T> where T : class
    {
        public Result<T> SuccessFixture(T result)
         => Result<T>.Success(result);

        public Result<T> FailureFixture(Error errors)
         => Result<T>.Failure(errors);
    }
}
