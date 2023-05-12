﻿namespace Merc_Vista_Blazor.Services
{
    public interface IServiceCaller : IDisposable
    {
        event Action<bool> OnLoadingStateChanged;
        bool IsLoading { get; set; }
        Task<TResult> CallAsync<TService, TResult>(Func<TService, Task<TResult>> method) where TService : class;
        Task CallAsync<TService, TAction>(Func<TService, Action> method) where TService : class;
    }
}
