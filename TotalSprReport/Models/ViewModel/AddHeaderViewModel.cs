using System.ComponentModel.DataAnnotations.Schema;

namespace TotalSprReport.Models.ViewModel
{
    public class AddHeaderViewModel
    {
        public Guid Id { get; set; }
        public Guid ProyekId { get; set; }
        public string? SPRCode { get; set; }
        public string? Peminta { get; set; }
        public DateTime TanggalMinta { get; set; }
        public string? LokasiPeminta { get; set; }
    }
}
