namespace TotalSprReport.Models.ViewModel
{
    public class DetailSprViewModel
    {
        public IEnumerable<TotalSprReport.Models.Detil_SPR>? SPRDetil { get; set; }
        public List<TotalSprReport.Models.Materials>? Materials { get; set; }
        public IEnumerable<TotalSprReport.Models.Header_SPR>? SPRHeader { get; set; }
    }
}
