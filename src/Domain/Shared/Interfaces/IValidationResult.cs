namespace Domain.Shared.Interfaces
{
    public interface IValidationResult
    {
        public static readonly Error[] ValidationError = new Error[] { new Error("ValidationError", "A validation problem occurred") };
        Error[] Errors { get; }
    }
}
