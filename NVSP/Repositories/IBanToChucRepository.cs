using NVSP.Models;

namespace NVSP.Repositories
{
    public interface IBanToChucRepository
    {
        Task<BanToChuc> GetByMaGiangVienAsync(string maGiangVien);
        Task<IEnumerable<BanToChuc>> GetAllAsync();
        Task<IEnumerable<BanToChuc>> GetBanToChucDươngNhiemAsync();
        Task CreateAsync(BanToChuc banToChuc);
        Task UpdateAsync(BanToChuc banToChuc);
        Task DeleteAsync(string maGiangVien);
        Task<bool> ExistsAsync(string maGiangVien);
    }
}