using NVSP.Models;

namespace NVSP.Repositories
{
    public interface ISinhVienRepository
    {
        Task<SinhVien> GetByMaSinhVienAsync(string maSinhVien);
        Task<IEnumerable<SinhVien>> GetAllAsync();
        Task CreateAsync(SinhVien sinhVien);
        Task UpdateAsync(SinhVien sinhVien);
        Task DeleteAsync(string maSinhVien);
    }
}