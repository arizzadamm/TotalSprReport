using System.ComponentModel.DataAnnotations;

namespace TotalSprReport.Models
{
    public class Proyek
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? NamaProyek { get; set; }
        [Required]
        public string? LokasiProyek { get; set; }
    }
}
