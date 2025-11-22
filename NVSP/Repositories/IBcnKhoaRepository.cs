using NVSP.Models;

namespace NVSP.Repositories
{
    public interface IBcnKhoaRepository
    {
        Task<BcnKhoa> GetByMaGiangVienAsync(string maGiangVien);
        Task<IEnumerable<BcnKhoa>> GetAllAsync();
        Task<IEnumerable<BcnKhoa>> GetByKhoaAsync(string khoa);
        Task<BcnKhoa> GetBcnDươngNhiemByKhoaAsync(string khoa);
        Task CreateAsync(BcnKhoa bcnKhoa);
        Task UpdateAsync(BcnKhoa bcnKhoa);
        Task DeleteAsync(string maGiangVien);
        Task<bool> ExistsAsync(string maGiangVien);
    }
}