using NVSP.DTOs;

namespace NVSP.Services
{
    public interface IBanToChucService
    {
        Task<BanToChucDTO> GetByMaGiangVienAsync(string maGiangVien);
        Task<IEnumerable<BanToChucDTO>> GetAllAsync();
        Task<IEnumerable<BanToChucDTO>> GetBanToChucDươngNhiemAsync();
        Task<BanToChucDTO> CreateAsync(CreateBanToChucDTO createBanToChucDTO);
        Task<BanToChucDTO> UpdateAsync(string maGiangVien, CreateBanToChucDTO updateBanToChucDTO);
        Task<bool> DeleteAsync(string maGiangVien);
    }
}