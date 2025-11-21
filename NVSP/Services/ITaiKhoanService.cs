using NVSP.DTOs;

namespace NVSP.Services
{
    public interface ITaiKhoanService
    {
        Task<TaiKhoanDTO> GetByMaCaNhanAsync(string maCaNhan);
        Task<IEnumerable<TaiKhoanDTO>> GetAllAsync();
        Task<TaiKhoanDTO> CreateAsync(CreateTaiKhoanDTO createTaiKhoanDTO);
        Task<TaiKhoanDTO> UpdateAsync(string maCaNhan, UpdateTaiKhoanDTO updateTaiKhoanDTO);
        Task<bool> DeleteAsync(int id);
    }
}