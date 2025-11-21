using Microsoft.EntityFrameworkCore;
using NVSP.Data;
using NVSP.Models;

namespace NVSP.Repositories
{
    public class GiangVienRepository : IGiangVienRepository
    {
        private readonly ApplicationDbContext _context;

        public GiangVienRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GiangVien> GetByMaGiangVienAsync(string maGiangVien)
        {
            return await _context.GiangViens
                .Include(g => g.TaiKhoan)
                .FirstOrDefaultAsync(g => g.MaGiangVien == maGiangVien);
        }

        public async Task<IEnumerable<GiangVien>> GetAllAsync()
        {
            return await _context.GiangViens
                .Include(g => g.TaiKhoan)
                .ToListAsync();
        }

        public async Task CreateAsync(GiangVien giangVien)
        {
            _context.GiangViens.Add(giangVien);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GiangVien giangVien)
        {
            _context.GiangViens.Update(giangVien);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string maGiangVien)
        {
            var giangVien = await GetByMaGiangVienAsync(maGiangVien);
            if (giangVien != null)
            {
                _context.GiangViens.Remove(giangVien);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> MaGiangVienExistsAsync(string maGiangVien)
        {
            return await _context.GiangViens.AnyAsync(g => g.MaGiangVien == maGiangVien);
        }
    }
}