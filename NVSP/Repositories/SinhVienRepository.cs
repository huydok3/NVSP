using Microsoft.EntityFrameworkCore;
using NVSP.Data;
using NVSP.Models;

namespace NVSP.Repositories
{
    public class SinhVienRepository : ISinhVienRepository
    {
        private readonly ApplicationDbContext _context;

        public SinhVienRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SinhVien> GetByMaSinhVienAsync(string maSinhVien)
        {
            return await _context.SinhViens
                .Include(s => s.TaiKhoan)
                .FirstOrDefaultAsync(s => s.MaSinhVien == maSinhVien);
        }

        public async Task<IEnumerable<SinhVien>> GetAllAsync()
        {
            return await _context.SinhViens
                .Include(s => s.TaiKhoan)
                .ToListAsync();
        }

        public async Task CreateAsync(SinhVien sinhVien)
        {
            _context.SinhViens.Add(sinhVien);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SinhVien sinhVien)
        {
            _context.SinhViens.Update(sinhVien);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string maSinhVien)
        {
            var sinhVien = await GetByMaSinhVienAsync(maSinhVien);
            if (sinhVien != null)
            {
                _context.SinhViens.Remove(sinhVien);
                await _context.SaveChangesAsync();
            }
        }
    }
}