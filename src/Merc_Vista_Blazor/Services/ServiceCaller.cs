namespace Merc_Vista_Blazor.Services
{
    public class ServiceCaller : IServiceCaller
    {
        public event Action<bool> OnLoadingStateChanged;
        private bool isLoading = false;

        private readonly IServiceProvider _services;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnLoadingStateChanged?.Invoke(isLoading);
            }
        }

        public ServiceCaller(IServiceProvider services) => _services = services;

        public async Task<TResult> CallAsync<TService, TResult>(Func<TService, Task<TResult>> method) where TService : class
        {
            var service = _services.GetRequiredService<TService>();

            try
            {
                ShowLoading();
                TResult serviceCallResult = await method(service);
                return serviceCallResult;
            }
            finally
            {
                HideLoading();
            }
        }

        public async Task CallAsync<TService, TAction>(Func<TService, Action> method) where TService : class
        {
            var service = _services.GetRequiredService<TService>();

            try
            {
                ShowLoading();
                await Task.Run(() => method(service).Invoke());
            }
            finally
            {
                HideLoading();
            }
        }

        public void ShowLoading() => IsLoading = true;

        public void HideLoading() => IsLoading = false;

        public void Dispose() => OnLoadingStateChanged = null;
    }
}
