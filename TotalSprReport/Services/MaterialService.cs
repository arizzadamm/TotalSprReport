using Microsoft.EntityFrameworkCore;
using TotalSprReport.Data;
using TotalSprReport.Models;
using TotalSprReport.Models.ViewModel;
using System.Linq;

namespace TotalSprReport.Services
{
    public class MaterialService
    {
        private readonly SprDBContext _sprDBContext;

        public MaterialService(SprDBContext sprDBContext)
        {
            _sprDBContext = sprDBContext;
        }
        public async Task<List<Materials>> GetMaterialAsync()
        {
            return await _sprDBContext.Material.ToListAsync();
        }
        public async Task<Materials> GetMaterialByIdAsync(Guid id)
        {
            var material = await _sprDBContext.Material.FindAsync(id);

            if (material == null)
            {
                throw new Exception("Proyek tidak ditemukan");
            }

            return material;
        }
        public async Task<List<Materials>> GetMaterialByType(bool TipeMaterial)
        {
            var materials = await _sprDBContext.Material
            .Where(h => h.TipeMaterial == TipeMaterial)
            .ToListAsync();

            return materials;
        }
        public async Task AddMaterialAsync(AddMaterialViewModel addMaterialViewModel)
        {
            var material = new Materials()
            {
                Material = addMaterialViewModel.Material,
                TipeMaterial = addMaterialViewModel.TipeMaterial
            };

            await _sprDBContext.Material.AddAsync(material);
            await _sprDBContext.SaveChangesAsync();
        }
        public async Task<Materials?> UpdateMaterialAsync(EditMaterialViewModel editMaterialViewModel)
        {
            var MaterialToUpdate = await _sprDBContext.Material.FirstOrDefaultAsync(p => p.Id == editMaterialViewModel.Id);

            if (MaterialToUpdate != null)
            {
                MaterialToUpdate.Material = editMaterialViewModel.Material;
                MaterialToUpdate.TipeMaterial = editMaterialViewModel.TipeMaterial;

                await _sprDBContext.SaveChangesAsync();

                return MaterialToUpdate;
            }

            return null;
     
        }
        public async Task DeleteMaterialAsync(Guid Id)
        {
            var material = await _sprDBContext.Material.FindAsync(Id);
            if (material != null)
            {
                _sprDBContext.Material.Remove(material);
                await _sprDBContext.SaveChangesAsync();
            }
        }
    }
}
