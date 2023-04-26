namespace Merc_Vista_Blazor.Services
{
    public interface IServiceCaller
    {
        Task<TResult> CallAsync<TService, TResult>(Func<TService, Task<TResult>> method) where TService : class;
        Task CallAsync<TService, TAction>(Func<TService, Action> method) where TService : class;
    }
}
