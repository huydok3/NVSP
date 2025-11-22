using Microsoft.EntityFrameworkCore;
using NVSP.Data;
using NVSP.Models;

namespace NVSP.Repositories
{
    public class BanToChucRepository : IBanToChucRepository
    {
        private readonly ApplicationDbContext _context;

        public BanToChucRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BanToChuc> GetByMaGiangVienAsync(string maGiangVien)
        {
            return await _context.BanToChucs
                .Include(b => b.GiangVien)
                .ThenInclude(g => g.TaiKhoan)
                .FirstOrDefaultAsync(b => b.MaGiangVien == maGiangVien);
        }

        public async Task<IEnumerable<BanToChuc>> GetAllAsync()
        {
            return await _context.BanToChucs
                .Include(b => b.GiangVien)
                .ThenInclude(g => g.TaiKhoan)
                .ToListAsync();
        }

        public async Task<IEnumerable<BanToChuc>> GetBanToChucDươngNhiemAsync()
        {
            return await _context.BanToChucs
                .Include(b => b.GiangVien)
                .ThenInclude(g => g.TaiKhoan)
                .Where(b => b.TrangThai == true)
                .ToListAsync();
        }

        public async Task CreateAsync(BanToChuc banToChuc)
        {
            _context.BanToChucs.Add(banToChuc);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BanToChuc banToChuc)
        {
            _context.BanToChucs.Update(banToChuc);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string maGiangVien)
        {
            var banToChuc = await GetByMaGiangVienAsync(maGiangVien);
            if (banToChuc != null)
            {
                _context.BanToChucs.Remove(banToChuc);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string maGiangVien)
        {
            return await _context.BanToChucs.AnyAsync(b => b.MaGiangVien == maGiangVien);
        }
    }
}