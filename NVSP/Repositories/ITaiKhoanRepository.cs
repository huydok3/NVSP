using NVSP.Models;

namespace NVSP.Repositories
{
    public interface ITaiKhoanRepository
    {
        Task<TaiKhoan> GetByMaCaNhanAsync(string maCaNhan);
        Task<TaiKhoan> GetByIdAsync(int id);
        Task<IEnumerable<TaiKhoan>> GetAllAsync();
        Task CreateAsync(TaiKhoan taiKhoan);
        Task UpdateAsync(TaiKhoan taiKhoan);
        Task DeleteAsync(int id);
        Task<bool> MaCaNhanExistsAsync(string maCaNhan);
        Task<IEnumerable<TaiKhoan>> SearchAsync(string keyword);
    }
}