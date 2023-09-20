using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TotalSprReport.Models.ViewModel
{
    public class AddDetilViewModel
    {
        [Required]
        public Guid IdRef { get; set; }

        [Required]
        public Guid MaterialId { get; set; }

        [Required]
        [Display(Name = "Tanggal Rencana Diterima")]
        public DateTime TanggalRencanaDiterima { get; set; }

        [Required]
        [Display(Name = "Status Disetujui")]
        public bool StatusDisetujui { get; set; }
        public List<SelectListItem>? MaterialList { get; set; }
    }
}
