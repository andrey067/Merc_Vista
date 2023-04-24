namespace Application.Query.Responses
{
    public class GetRelativeStrengthResponse
    {
        public List<ChartPropResponse> ChartProp { get; set; } = new List<ChartPropResponse>();
        public IEnumerable<string> XAxisLabelsDatas { get; set; }
    }
}
