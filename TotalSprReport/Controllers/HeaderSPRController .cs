using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TotalSprReport.Data;
using TotalSprReport.Models;
using TotalSprReport.Models.ViewModel;
using TotalSprReport.Services;

namespace TotalSprReport.Controllers
{
    //public class HeaderSPRController : Controller
    //{
    //    private readonly SprDBContext _context;
    //    public HeaderSPRController(SprDBContext context)
    //    {
    //        _context = context;
    //    }
    //    // Operasi CRUD untuk tabel header
    //    [HttpPost]
    //    public ActionResult CreateHeader(Header_SPR header, Guid proyekId, string peminta, DateTime tanggalMinta, string lokasiPeminta)
    //    {

    //        _context.Database.ExecuteSqlRaw("EXEC usp_spr_InsertHeader @ProyekId, @Peminta, @TanggalMinta, @LokasiPeminta",
    //        new SqlParameter("@ProyekId", proyekId),
    //        new SqlParameter("@Peminta", peminta),
    //        new SqlParameter("@TanggalMinta", tanggalMinta),
    //        new SqlParameter("@LokasiPeminta", lokasiPeminta));

    //        return View();

    //    }

    //    [HttpGet("{id}")]
    //    public ActionResult<Header_SPR> ReadHeader(int id)
    //    {
    //        var headerSprList = _context.Header_SPR.ToList();

    //        return View();
    //    }

    //    [HttpPut]
    //public ActionResult UpdateHeader(Header_SPR header, Guid proyekId, string peminta, DateTime tanggalMinta, string lokasiPeminta)
    //{

    //    _context.Database.ExecuteSqlRaw("EXEC usp_spr_UpdateHeader @ProyekId, @Peminta, @TanggalMinta, @LokasiPeminta",
    //    new SqlParameter("@ProyekId", proyekId),
    //    new SqlParameter("@Peminta", peminta),
    //    new SqlParameter("@TanggalMinta", tanggalMinta),
    //    new SqlParameter("@LokasiPeminta", lokasiPeminta));

    //    return View();

    //}


    //    [HttpDelete("{id}")]
    //    public ActionResult DeleteHeader(int id)
    //    {
    //        _context.Database.ExecuteSqlRaw("EXEC usp_spr_InsertHeader @ProyekId, @Peminta, @TanggalMinta, @LokasiPeminta",
    //        new SqlParameter("@Id", id));

    //        return View();
    //    }
    //    // Operasi CRUD untuk tabel detail
    //    [HttpPost]
    //    public ActionResult CreateDetil(Detil_SPR detil, Guid IdRef, Guid MaterialId, DateTime TanggalRencanaDiterima, bool StatusDisetujui)
    //    {
    //        _context.Database.ExecuteSqlRaw("EXEC usp_spr_InsertDetil @ProyekId, @Peminta, @TanggalMinta, @LokasiPeminta",
    //       new SqlParameter("@IdRef", IdRef),
    //       new SqlParameter("@MaterialId", MaterialId),
    //       new SqlParameter("@TanggalRencanaDiterima", TanggalRencanaDiterima),
    //       new SqlParameter("@StatusDisetujui", StatusDisetujui));

    //        return View();

    //    }

    //    [HttpGet("{id}")]
    //    public ActionResult<Detil_SPR> ReadDetil(int id)
    //    {
    //        var headerSprList = _context.Detil_SPR.ToList();

    //        return View();
    //    }

    //    [HttpPut]
    //    public ActionResult UpdateDetil(Detil_SPR detil, Guid IdRef, Guid MaterialId, DateTime TanggalRencanaDiterima, bool StatusDisetujui)
    //    {
    //        _context.Database.ExecuteSqlRaw("EXEC usp_spr_UpdateDetil @ProyekId, @Peminta, @TanggalMinta, @LokasiPeminta",
    //       new SqlParameter("@IdRef", IdRef),
    //       new SqlParameter("@MaterialId", MaterialId),
    //       new SqlParameter("@TanggalRencanaDiterima", TanggalRencanaDiterima),
    //       new SqlParameter("@StatusDisetujui", StatusDisetujui));

    //        return View();

    //    }

    //    [HttpDelete("{id}")]
    //    public ActionResult DeleteDetail(int id)
    //    {
    //        _context.Database.ExecuteSqlRaw("EXEC usp_spr_DeleteDetil @ProyekId, @Peminta, @TanggalMinta, @LokasiPeminta",
    //        new SqlParameter("@Id", id));

    //        return View();

    //    }
    //}

    public class HeaderSPRController : Controller
    {
        private readonly HeaderSPRService _headerService;
        private readonly ProyekService _proyekService;
        private readonly MaterialService _materialService;

        public HeaderSPRController(HeaderSPRService headerService, ProyekService proyekService, MaterialService materialService)
        {
            _headerService = headerService;
            _proyekService = proyekService;
            _materialService = materialService;
        }
        [HttpGet("HeaderSPR/Add/{proyekId}")]
        [Route("HeaderSPR/GetSPRList/{proyekId}")]
        public IActionResult Add([FromRoute] Guid proyekId)
        {
            return View("Add");
        }
        [HttpGet("HeaderSPR/Edit/{Id}")]
        public IActionResult Edit([FromRoute] Guid Id)
        {
            return View("Edit");
        }
        [HttpGet("HeaderSPR/GetSPRList/{proyekId}")]
        public async Task<IActionResult> GetSPRList(Guid proyekid)
        {
            try
            {
                var sprList = await _headerService.GetSPRListAsync(proyekid); 
                var proyekList = await _proyekService.GetProyekAsync();

                var viewModel = new DetailProyekViewModel
                {
                    SPRList = sprList,
                    ProyekList = proyekList
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                
                return View("Error"); 
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateHeader([FromBody] AddHeaderViewModel model)
            {
            if (ModelState.IsValid)
            {
                try
                {
                    await _headerService.CreateHeaderAsync(model.ProyekId, model.Peminta, model.TanggalMinta, model.LokasiPeminta);
                    return RedirectToAction("GetSPRList", new { proyekId = model.ProyekId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error: " + ex.Message);
                }
            }
            return View(model);
        }

        public async Task<ActionResult> UpdateHeader([FromBody] EditHeaderViewModel model)
        {
            await _headerService.UpdateHeaderAsync(model.Id, model.Peminta, model.TanggalMinta, model.LokasiPeminta);

            return View();
        }
        public async Task<ActionResult> DeleteHeader(Guid id)
        {
            await _headerService.DeleteHeaderAsync(id);
            return View();
        }
        [HttpGet("HeaderSPR/GetSPRDetil/{idRef}")]
        public async Task<IActionResult> GetSPRDetil(Guid idRef)
        {
            try
            {
                var sprdetail = await _headerService.GetSPRDetailAsync(idRef); // Ganti dengan proyekId yang sesuai
                var materiallist = await _materialService.GetMaterialAsync();

                var viewModel = new DetailSprViewModel
                {
                    SPRDetil = sprdetail,
                    Materials = materiallist
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Handle kesalahan sesuai kebutuhan Anda
                return View("Error"); // Contoh menampilkan halaman error
            }
        }

        [HttpGet("HeaderSPR/AddDetilSPR/{idRef}")]
        public IActionResult AddDetilSPR([FromRoute]Guid idRef)
        {
            var materialList = _materialService.GetMaterialAsync().Result;

            var materialSelectList = materialList
                .Select(material => new SelectListItem
                {
                    Value = material.Id.ToString(),
                    Text = material.Material
                })
                .ToList();

            var viewModel = new AddDetilViewModel
            {
                IdRef = idRef,
                MaterialList = materialSelectList
            };

            
            return View(viewModel);
        }
        [HttpGet("HeaderSPR/EditDetilSPR/{Id}")]
        public IActionResult EditDetilSPR([FromRoute] Guid Id)
        {
            var materialList = _materialService.GetMaterialAsync().Result;

            var materialSelectList = materialList
                .Select(material => new SelectListItem
                {
                    Value = material.Id.ToString(),
                    Text = material.Material
                })
                .ToList();

            var viewModel = new EditDetilViewModel
            {
                Id = Id,
                MaterialList = materialSelectList
            };
            return View(viewModel);
        }
        public async Task<IActionResult> GetByType(bool tipeMaterial)
        {
            var materials = await _materialService.GetMaterialByType(tipeMaterial);

            // Di sini Anda dapat mengirimkan data material ke tampilan Anda atau mengembalikan sebagai JSON
            return View(materials);
        }
  
        

        [HttpPost]
        public async Task<ActionResult> AddDetil ([FromBody] AddDetilViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _headerService.AddSPRDetailAsync(model.IdRef, model.MaterialId);

                    
                    return RedirectToAction("GetSPRDetil", new { IdRef = model.IdRef });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error: " + ex.Message);
                }
            }
            return View(model);
        }
        public async Task<ActionResult> UpdateDetil([FromBody] EditDetilViewModel model)
        {
            await _headerService.UpdateSPRDetailAsync(model.Id, model.MaterialId, model.TanggalRencanaDiterima);

            return View();
        }
        public async Task<ActionResult> DeleteDetil(Guid id)
        {
            await _headerService.DeleteDetailAsync(id);
            return NoContent();
        }
    }
}
