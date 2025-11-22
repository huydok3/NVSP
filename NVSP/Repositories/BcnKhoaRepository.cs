using Microsoft.EntityFrameworkCore;
using NVSP.Data;
using NVSP.Models;

namespace NVSP.Repositories
{
    public class BcnKhoaRepository : IBcnKhoaRepository
    {
        private readonly ApplicationDbContext _context;

        public BcnKhoaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BcnKhoa> GetByMaGiangVienAsync(string maGiangVien)
        {
            return await _context.BcnKhoas
                .Include(b => b.GiangVien)
                .ThenInclude(g => g.TaiKhoan)
                .FirstOrDefaultAsync(b => b.MaGiangVien == maGiangVien);
        }

        public async Task<IEnumerable<BcnKhoa>> GetAllAsync()
        {
            return await _context.BcnKhoas
                .Include(b => b.GiangVien)
                .ThenInclude(g => g.TaiKhoan)
                .ToListAsync();
        }

        public async Task<IEnumerable<BcnKhoa>> GetByKhoaAsync(string khoa)
        {
            return await _context.BcnKhoas
                .Include(b => b.GiangVien)
                .ThenInclude(g => g.TaiKhoan)
                .Where(b => b.Khoa == khoa)
                .ToListAsync();
        }

        public async Task<BcnKhoa> GetBcnDươngNhiemByKhoaAsync(string khoa)
        {
            return await _context.BcnKhoas
                .Include(b => b.GiangVien)
                .ThenInclude(g => g.TaiKhoan)
                .FirstOrDefaultAsync(b => b.Khoa == khoa && b.TrangThai == true);
        }

        public async Task CreateAsync(BcnKhoa bcnKhoa)
        {
            _context.BcnKhoas.Add(bcnKhoa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BcnKhoa bcnKhoa)
        {
            _context.BcnKhoas.Update(bcnKhoa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string maGiangVien)
        {
            var bcnKhoa = await GetByMaGiangVienAsync(maGiangVien);
            if (bcnKhoa != null)
            {
                _context.BcnKhoas.Remove(bcnKhoa);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string maGiangVien)
        {
            return await _context.BcnKhoas.AnyAsync(b => b.MaGiangVien == maGiangVien);
        }
    }
}