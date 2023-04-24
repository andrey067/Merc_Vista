namespace Merc_Vista_Blazor.Models
{
    public class ResultModel<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
