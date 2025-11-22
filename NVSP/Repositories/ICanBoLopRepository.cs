using NVSP.Models;

namespace NVSP.Repositories
{
    public interface ICanBoLopRepository
    {
        Task<CanBoLop> GetByMaSinhVienAsync(string maSinhVien);
        Task<IEnumerable<CanBoLop>> GetAllAsync();
        Task<IEnumerable<CanBoLop>> GetByLopAsync(string lop);
        Task<IEnumerable<CanBoLop>> GetCanBoLopDươngNhiemAsync();
        Task CreateAsync(CanBoLop canBoLop);
        Task UpdateAsync(CanBoLop canBoLop);
        Task DeleteAsync(string maSinhVien);
        Task<bool> ExistsAsync(string maSinhVien);
    }
}