using Microsoft.EntityFrameworkCore;
using NVSP.Data;
using NVSP.Models;

namespace NVSP.Repositories
{
    public class TaiKhoanRepository : ITaiKhoanRepository
    {
        private readonly ApplicationDbContext _context;

        public TaiKhoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaiKhoan> GetByMaCaNhanAsync(string maCaNhan)
        {
            return await _context.TaiKhoans
                .FirstOrDefaultAsync(t => t.MaCaNhan == maCaNhan);
        }

        public async Task<TaiKhoan> GetByIdAsync(int id)
        {
            return await _context.TaiKhoans.FindAsync(id);
        }

        public async Task<IEnumerable<TaiKhoan>> GetAllAsync()
        {
            return await _context.TaiKhoans.ToListAsync();
        }

        public async Task CreateAsync(TaiKhoan taiKhoan)
        {
            _context.TaiKhoans.Add(taiKhoan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaiKhoan taiKhoan)
        {
            _context.TaiKhoans.Update(taiKhoan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var taiKhoan = await GetByIdAsync(id);
            if (taiKhoan != null)
            {
                _context.TaiKhoans.Remove(taiKhoan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> MaCaNhanExistsAsync(string maCaNhan)
        {
            return await _context.TaiKhoans.AnyAsync(t => t.MaCaNhan == maCaNhan);
        }

        public async Task<IEnumerable<TaiKhoan>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return await _context.TaiKhoans.ToListAsync();

            var searchTerm = keyword.ToLower().Trim();

            return await _context.TaiKhoans
                .Where(t =>
                    t.MaCaNhan.ToLower().Contains(searchTerm) ||
                    t.HoTen.ToLower().Contains(searchTerm))
                .OrderBy(t => t.MaCaNhan)
                .ToListAsync();
        }
    }
}