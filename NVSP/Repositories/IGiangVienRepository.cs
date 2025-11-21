using NVSP.Models;

namespace NVSP.Repositories
{
    public interface IGiangVienRepository
    {
        Task<GiangVien> GetByMaGiangVienAsync(string maGiangVien);
        Task<IEnumerable<GiangVien>> GetAllAsync();
        Task CreateAsync(GiangVien giangVien);
        Task UpdateAsync(GiangVien giangVien);
        Task DeleteAsync(string maGiangVien);
        Task<bool> MaGiangVienExistsAsync(string maGiangVien);
    }
}