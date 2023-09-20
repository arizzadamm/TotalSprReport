using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TotalSprReport.Models.ViewModel
{
    public class EditDetilViewModel
    {
        public Guid Id { get; set; }
        public Guid IdRef { get; set; }
        public Guid MaterialId { get; set; }

        [Display(Name = "Tanggal Rencana Diterima")]
        public DateTime TanggalRencanaDiterima { get; set; }

        [Display(Name = "Status Disetujui")]
        public bool StatusDisetujui { get; set; }

        public List<SelectListItem>? MaterialList { get; set; }
    }
}
