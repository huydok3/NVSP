using NVSP.DTOs;

namespace NVSP.Services
{
    public interface ICanBoLopService
    {
        Task<CanBoLopDTO> GetByMaSinhVienAsync(string maSinhVien);
        Task<IEnumerable<CanBoLopDTO>> GetAllAsync();
        Task<IEnumerable<CanBoLopDTO>> GetByLopAsync(string lop);
        Task<IEnumerable<CanBoLopDTO>> GetCanBoLopDươngNhiemAsync();
        Task<CanBoLopDTO> CreateAsync(CreateCanBoLopDTO createCanBoLopDTO);
        Task<CanBoLopDTO> UpdateAsync(string maSinhVien, CreateCanBoLopDTO updateCanBoLopDTO);
        Task<bool> DeleteAsync(string maSinhVien);
    }
}