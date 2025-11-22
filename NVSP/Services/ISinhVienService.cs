using NVSP.DTOs;

namespace NVSP.Services
{
    public interface ISinhVienService
    {
        Task<SinhVienDTO> GetByMaSinhVienAsync(string maSinhVien);
        Task<IEnumerable<SinhVienDTO>> GetAllAsync();
        Task<SinhVienDTO> CreateAsync(CreateSinhVienDTO createSinhVienDTO);
        Task<SinhVienDTO> UpdateAsync(string maSinhVien, UpdateSinhVienDTO updateSinhVienDTO);
        Task<bool> DeleteAsync(string maSinhVien);
    }
}