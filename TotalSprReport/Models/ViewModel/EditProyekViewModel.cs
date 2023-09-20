using System.ComponentModel.DataAnnotations;

namespace TotalSprReport.Models.ViewModel
{
    public class EditProyekViewModel
    {
        public Guid Id { get; set; }
        public string? NamaProyek { get; set; }
        public string? LokasiProyek { get; set; }
    }
}
