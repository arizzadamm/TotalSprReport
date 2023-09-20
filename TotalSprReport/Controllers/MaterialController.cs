using Microsoft.AspNetCore.Mvc;
using TotalSprReport.Models;
using TotalSprReport.Models.ViewModel;
using TotalSprReport.Services;

namespace TotalSprReport.Controllers
{
    public class MaterialController : Controller
    {
        private readonly MaterialService _materialService;

        public MaterialController(MaterialService materialService)
        {
            _materialService = materialService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var materialList = await _materialService.GetMaterialAsync();
            return View(materialList); // Mengirim model ke tampilan Index
        }
        public async Task<IActionResult> GetData()
        {
            var materialList = await _materialService.GetMaterialAsync();
            return Json(new { data = materialList });
        }
        [HttpGet("Proyek/Get/{id}")]
        public async Task<IActionResult> GetMaterialById(Guid id)
        {
            try
            {
                var material = await _materialService.GetMaterialByIdAsync(id);

                // Jika proyek ditemukan, kembalikan sebagai respons JSON
                return Json(material);
            }
            catch (Exception ex)
            {
                // Jika proyek tidak ditemukan, tangani kesalahan dan kembalikan pesan kesalahan
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(AddMaterialViewModel addMaterialViewModel)
        {
            await _materialService.AddMaterialAsync(addMaterialViewModel);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> Update([FromBody] EditMaterialViewModel editMaterialViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Data yang dikirim tidak valid" });
                }

                var updatedMaterial = await _materialService.UpdateMaterialAsync(editMaterialViewModel);

                if (updatedMaterial != null)
                {
                    return Ok(new { success = true, message = "Material berhasil diperbarui" });
                }
                else
                {
                    return NotFound(new { success = false, message = "Material tidak ditemukan" });
                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { success = false, message = $"Terjadi kesalahan: {ex.Message}" });
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await _materialService.DeleteMaterialAsync(Id);
            return NoContent();
        }
    }
}
