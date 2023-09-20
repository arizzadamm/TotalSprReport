namespace TotalSprReport.Models.ViewModel
{
    public class EditHeaderViewModel
    {
        public Guid Id { get; set; }
        public Guid ProyekId { get; set; }
        public string? SPRCode { get; set; }
        public string? Peminta { get; set; }
        public DateTime TanggalMinta { get; set; }
        public string? LokasiPeminta { get; set; }
    }
}
