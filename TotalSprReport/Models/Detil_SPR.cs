using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TotalSprReport.Models
{
    public class Detil_SPR
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdRef { get; set; }
        [ForeignKey("IdRef")] // FK Definition
        public Header_SPR? Header_SPR { get; set; }

        public Guid MaterialId { get; set; }
        [ForeignKey("MaterialId")]
        public Materials? Material { get; set; } // Menyambungkan ke tabel Materials

        public DateTime TanggalRencanaDiterima { get; set; }
        public bool StatusDisetujui { get; set; }
    }
}
