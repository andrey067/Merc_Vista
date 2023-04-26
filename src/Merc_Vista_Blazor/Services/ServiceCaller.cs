namespace Merc_Vista_Blazor.Services
{
    public class ServiceCaller: IServiceCaller
    {
        private readonly IServiceProvider _services;
        private readonly ISpinnerService _spinnerService;

        public ServiceCaller(IServiceProvider services, ISpinnerService spinnerService)
        {
            _services = services;
            _spinnerService = spinnerService;
        }

        public async Task<TResult> CallAsync<TService, TResult>(Func<TService, Task<TResult>> method) where TService : class
        {
            var service = _services.GetRequiredService<TService>();

            try
            {
                _spinnerService.Show();
                TResult serviceCallResult = await method(service);
                return serviceCallResult;
            }
            finally
            {
                _spinnerService.Hide();
            }
        }

        public async Task CallAsync<TService, TAction>(Func<TService, Action> method) where TService : class
        {
            var service = _services.GetRequiredService<TService>();

            try
            {
                _spinnerService.Show();
                await Task.Run(() => method(service).Invoke());
            }
            finally
            {
                _spinnerService.Hide();
            }
        }
    }
}
