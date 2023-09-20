using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TotalSprReport.Data;
using TotalSprReport.Models;
using TotalSprReport.Models.ViewModel;
using System.Linq;

namespace TotalSprReport.Services
{
    public class HeaderSPRService
    {
        private readonly SprDBContext _sprDBContext;

        public HeaderSPRService(SprDBContext sprDBContext)
        {
            _sprDBContext = sprDBContext;
        }
        public async Task<Header_SPR> ReadHeaderIdAsync(Guid proyekId)
        {
            var sprH = await _sprDBContext.Header_SPR.FirstOrDefaultAsync(h => h.ProyekId == proyekId);

            if (sprH == null)
            {
                throw new Exception("Proyek tidak ditemukan");
            }

            return sprH;
        }
        public async Task<IEnumerable<Header_SPR>> GetSPRListAsync(Guid proyekId)
        {
            // Panggil stored procedure menggunakan FromSqlRaw
            var result = await _sprDBContext.Header_SPR
                .FromSqlRaw("EXEC usp_spr_GetSPRListHeader @ProyekId", new SqlParameter("@ProyekId", proyekId))
                .ToListAsync();

            return result;
        }
       


        public async Task CreateHeaderAsync([FromRoute]Guid proyekId, string peminta, DateTime tanggalMinta, string lokasiPeminta)
        {
            await _sprDBContext.Database.ExecuteSqlRawAsync("EXEC usp_spr_InsertHeader @ProyekId, @Peminta, @TanggalMinta, @LokasiPeminta",
                new SqlParameter("@ProyekId", proyekId),
                new SqlParameter("@Peminta", peminta),
                new SqlParameter("@TanggalMinta", tanggalMinta),
                new SqlParameter("@LokasiPeminta", lokasiPeminta));
        }

        public async Task UpdateHeaderAsync(Guid Id, string peminta, DateTime tanggalMinta, string lokasiPeminta)
        {
            await _sprDBContext.Database.ExecuteSqlRawAsync("EXEC usp_spr_UpdateHeader @Id, @Peminta, @TanggalMinta, @LokasiPeminta",
                new SqlParameter("@Id", Id),
                new SqlParameter("@Peminta", peminta),
                new SqlParameter("@TanggalMinta", tanggalMinta),
                new SqlParameter("@LokasiPeminta", lokasiPeminta));
        }
        public async Task DeleteHeaderAsync(Guid id)
        {
            var header = await _sprDBContext.Header_SPR.FirstOrDefaultAsync(h => h.Id == id);
            if (header != null)
            {
                _sprDBContext.Header_SPR.Remove(header);
                await _sprDBContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Detil_SPR>> GetSPRDetailAsync(Guid IdRef)
        {
            // Panggil stored procedure menggunakan FromSqlRaw
            var result = await _sprDBContext.Detil_SPR
                .FromSqlRaw("EXEC usp_spr_GetSPRDetail @@IdRef", new SqlParameter("@@IdRef", IdRef))
                .ToListAsync();

            return result;
        }

        public async Task AddSPRDetailAsync(Guid IdRef, Guid MaterialId)
        {
            await _sprDBContext.Database.ExecuteSqlRawAsync("EXEC usp_spr_InsertDetil @IdRef, @MaterialId",
                new SqlParameter("@IdRef", IdRef),
                new SqlParameter("@MaterialId", MaterialId));
        }
        public async Task UpdateSPRDetailAsync(Guid Id, Guid MaterialId, DateTime TanggalRencanaDiterima)
        {
            await _sprDBContext.Database.ExecuteSqlRawAsync("EXEC usp_spr_UpdateDetil @Id, @MaterialId, @TanggalRencanaDiterima",
                new SqlParameter("@Id", Id),
                new SqlParameter("@MaterialId", MaterialId),
                new SqlParameter("@TanggalRencanaDiterima", TanggalRencanaDiterima));
        }
        public async Task DeleteDetailAsync(Guid id)
        {
            var detil = await _sprDBContext.Detil_SPR.FirstOrDefaultAsync(h => h.Id == id);
            if (detil != null)
            {
                _sprDBContext.Detil_SPR.Remove(detil); ;
                await _sprDBContext.SaveChangesAsync();
            }
        }

    }

}
