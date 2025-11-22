using NVSP.DTOs;

namespace NVSP.Services
{
    public interface IBcnKhoaService
    {
        Task<BcnKhoaDTO> GetByMaGiangVienAsync(string maGiangVien);
        Task<IEnumerable<BcnKhoaDTO>> GetAllAsync();
        Task<IEnumerable<BcnKhoaDTO>> GetByKhoaAsync(string khoa);
        Task<BcnKhoaDTO> GetBcnDươngNhiemByKhoaAsync(string khoa);
        Task<BcnKhoaDTO> CreateAsync(CreateBcnKhoaDTO createBcnKhoaDTO);
        Task<BcnKhoaDTO> UpdateAsync(string maGiangVien, CreateBcnKhoaDTO updateBcnKhoaDTO);
        Task<bool> DeleteAsync(string maGiangVien);
    }
}