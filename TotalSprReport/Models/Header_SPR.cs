using System.ComponentModel.DataAnnotations.Schema;

namespace TotalSprReport.Models
{
    public class Header_SPR
    {
        public Guid Id { get; set; }
        public Guid ProyekId { get; set; }
        [ForeignKey("ProyekId")] // FK Definition
        public Proyek? Proyek { get; set; }
        public string? SPRCode { get; set; }
        public string? Peminta { get; set; }
        public DateTime TanggalMinta { get; set; }
        public string? LokasiPeminta { get; set; }
    }
}
