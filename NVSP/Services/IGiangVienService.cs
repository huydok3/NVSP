using NVSP.DTOs;

namespace NVSP.Services
{
    public interface IGiangVienService
    {
        Task<GiangVienDTO> GetByMaGiangVienAsync(string maGiangVien);
        Task<IEnumerable<GiangVienDTO>> GetAllAsync();
        Task<GiangVienDTO> CreateAsync(CreateGiangVienDTO createGiangVienDTO);
        Task<GiangVienDTO> UpdateAsync(string maGiangVien, UpdateGiangVienDTO updateGiangVienDTO);
        Task<bool> DeleteAsync(string maGiangVien);
    }
}