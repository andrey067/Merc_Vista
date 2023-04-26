namespace Merc_Vista_Blazor.Services
{
    public class SpinnerService: ISpinnerService
    {
        public event Action OnShow;
        public event Action OnHide;

        public void Hide() => OnHide!.Invoke();
        public void Show() => OnShow!.Invoke();
    }
}
