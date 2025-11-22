using Microsoft.EntityFrameworkCore;
using NVSP.Data;
using NVSP.Models;

namespace NVSP.Repositories
{
    public class CanBoLopRepository : ICanBoLopRepository
    {
        private readonly ApplicationDbContext _context;

        public CanBoLopRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CanBoLop> GetByMaSinhVienAsync(string maSinhVien)
        {
            return await _context.CanBoLops
                .Include(c => c.SinhVien)
                .ThenInclude(s => s.TaiKhoan)
                .FirstOrDefaultAsync(c => c.MaSinhVien == maSinhVien);
        }

        public async Task<IEnumerable<CanBoLop>> GetAllAsync()
        {
            return await _context.CanBoLops
                .Include(c => c.SinhVien)
                .ThenInclude(s => s.TaiKhoan)
                .ToListAsync();
        }

        public async Task<IEnumerable<CanBoLop>> GetByLopAsync(string lop)
        {
            return await _context.CanBoLops
                .Include(c => c.SinhVien)
                .ThenInclude(s => s.TaiKhoan)
                .Where(c => c.SinhVien.Lop == lop)
                .ToListAsync();
        }

        public async Task<IEnumerable<CanBoLop>> GetCanBoLopDươngNhiemAsync()
        {
            return await _context.CanBoLops
                .Include(c => c.SinhVien)
                .ThenInclude(s => s.TaiKhoan)
                .Where(c => c.TrangThai == true)
                .ToListAsync();
        }

        public async Task CreateAsync(CanBoLop canBoLop)
        {
            _context.CanBoLops.Add(canBoLop);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CanBoLop canBoLop)
        {
            _context.CanBoLops.Update(canBoLop);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string maSinhVien)
        {
            var canBoLop = await GetByMaSinhVienAsync(maSinhVien);
            if (canBoLop != null)
            {
                _context.CanBoLops.Remove(canBoLop);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string maSinhVien)
        {
            return await _context.CanBoLops.AnyAsync(c => c.MaSinhVien == maSinhVien);
        }
    }
}