using Microsoft.AspNetCore.Mvc;
using System;
using TotalSprReport.Data;
using TotalSprReport.Models;
using TotalSprReport.Models.ViewModel;
using TotalSprReport.Services;

namespace TotalSprReport.Controllers
{
    public class ProyekController : Controller
    {
        private readonly ProyekService _proyekService;

        public ProyekController(ProyekService proyekService)
        {
            _proyekService = proyekService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var proyekList = await _proyekService.GetProyekAsync();
            return View(proyekList); // Mengirim model ke tampilan Index
        }
        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var proyekList = await _proyekService.GetProyekAsync();
            return Json(new { data = proyekList });
        }
        [HttpGet("Proyek/Get/{id}")]
        public async Task<IActionResult> GetProyekById(Guid id)
        {
            try
            {
                var proyek = await _proyekService.GetProyekByIdAsync(id);

               
                return Json(proyek);
            }
            catch (Exception ex)
            {
                
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
        public async Task<ActionResult> Add(AddProyekViewModel addProyekViewModel)
        {
            await _proyekService.AddProyekAsync(addProyekViewModel);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Update([FromBody] EditProyekViewModel editProyekViewModel)
        {
            
            var result = await _proyekService.UpdateProyekAsync(editProyekViewModel);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await _proyekService.DeleteProyekAsync(Id);
            return NoContent();
        }
    }
}
