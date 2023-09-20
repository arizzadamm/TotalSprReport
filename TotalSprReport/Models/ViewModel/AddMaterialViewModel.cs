using System.ComponentModel.DataAnnotations;

namespace TotalSprReport.Models.ViewModel
{
    public class AddMaterialViewModel
    {
        public Guid Id { get; set; }
        public string? Material { get; set; }
        public bool TipeMaterial { get; set; }
    }
}
