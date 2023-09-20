using System.ComponentModel.DataAnnotations.Schema;

namespace TotalSprReport.Models.ViewModel
{
    public class DetailProyekViewModel
    {
        public IEnumerable<TotalSprReport.Models.Header_SPR>? SPRList { get; set; }
        public List<TotalSprReport.Models.Proyek>? ProyekList { get; set; }
    }
}
