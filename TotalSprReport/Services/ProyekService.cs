using Microsoft.EntityFrameworkCore;
using TotalSprReport.Data;
using TotalSprReport.Models;
using TotalSprReport.Models.ViewModel;

namespace TotalSprReport.Services
{
        public class ProyekService
        {
            private readonly SprDBContext _sprDBContext;

            public ProyekService(SprDBContext sprDBContext)
            {
                _sprDBContext = sprDBContext;
            }

            public async Task<List<Proyek>> GetProyekAsync()
            {
                return await _sprDBContext.Proyek.ToListAsync();
            }
            public async Task<Proyek> GetProyekByIdAsync(Guid id)
            {
                var proyek = await _sprDBContext.Proyek.FindAsync(id);

                if (proyek == null)
                {
                throw new Exception("Proyek tidak ditemukan"); 
                }

                return proyek;
            }
            public async Task AddProyekAsync(AddProyekViewModel addProyekViewModel)
            {
                var proyek = new Proyek()
                {
                    NamaProyek = addProyekViewModel.NamaProyek,
                    LokasiProyek = addProyekViewModel.LokasiProyek
                };

                await _sprDBContext.Proyek.AddAsync(proyek);
                await _sprDBContext.SaveChangesAsync();
            }

            public async Task<Proyek?> UpdateProyekAsync(EditProyekViewModel editProyekViewModel)
            {
                var proyekToUpdate = await _sprDBContext.Proyek.FirstOrDefaultAsync(p => p.Id == editProyekViewModel.Id);

                if (proyekToUpdate != null)
                {
                         proyekToUpdate.NamaProyek = editProyekViewModel.NamaProyek;
                         proyekToUpdate.LokasiProyek = editProyekViewModel.LokasiProyek;

                         await _sprDBContext.SaveChangesAsync();

                         return proyekToUpdate;
                }

            return null;
            }

            public async Task DeleteProyekAsync(Guid Id)
            {
                 var proyek = await _sprDBContext.Proyek.FindAsync(Id);
                 if (proyek != null)
                 {
                    _sprDBContext.Proyek.Remove(proyek);
                    await _sprDBContext.SaveChangesAsync();
                 }
            }


        }
}
