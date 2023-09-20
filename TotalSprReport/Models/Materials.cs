using System.ComponentModel.DataAnnotations;

namespace TotalSprReport.Models
{
    public class Materials
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Material { get; set; }
        [Required]
        public bool TipeMaterial { get; set; }

    }
}
