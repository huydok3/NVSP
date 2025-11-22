using NVSP.DTOs;
using NVSP.Repositories;

namespace NVSP.Services
{
    public class BanToChucService : IBanToChucService
    {
        private readonly IBanToChucRepository _banToChucRepository;
        private readonly IGiangVienRepository _giangVienRepository;

        public BanToChucService(IBanToChucRepository banToChucRepository, IGiangVienRepository giangVienRepository)
        {
            _banToChucRepository = banToChucRepository;
            _giangVienRepository = giangVienRepository;
        }

        public async Task<BanToChucDTO> GetByMaGiangVienAsync(string maGiangVien)
        {
            var banToChuc = await _banToChucRepository.GetByMaGiangVienAsync(maGiangVien);
            return MapToDTO(banToChuc);
        }

        public async Task<IEnumerable<BanToChucDTO>> GetAllAsync()
        {
            var banToChucs = await _banToChucRepository.GetAllAsync();
            return banToChucs.Select(MapToDTO);
        }

        public async Task<IEnumerable<BanToChucDTO>> GetBanToChucDươngNhiemAsync()
        {
            var banToChucs = await _banToChucRepository.GetBanToChucDươngNhiemAsync();
            return banToChucs.Select(MapToDTO);
        }

        public async Task<BanToChucDTO> CreateAsync(CreateBanToChucDTO createBanToChucDTO)
        {
            var giangVien = await _giangVienRepository.GetByMaGiangVienAsync(createBanToChucDTO.MaGiangVien);
            if (giangVien == null)
            {
                throw new Exception("Giảng viên không tồn tại");
            }

            if (await _banToChucRepository.ExistsAsync(createBanToChucDTO.MaGiangVien))
            {
                throw new Exception("Giảng viên đã là thành viên Ban tổ chức");
            }

            var banToChuc = new Models.BanToChuc
            {
                MaGiangVien = createBanToChucDTO.MaGiangVien,
                BatDauNk = createBanToChucDTO.BatDauNk,
                KetThucNk = createBanToChucDTO.KetThucNk,
                TrangThai = true
            };

            await _banToChucRepository.CreateAsync(banToChuc);
            return await GetByMaGiangVienAsync(createBanToChucDTO.MaGiangVien);
        }

        public async Task<BanToChucDTO> UpdateAsync(string maGiangVien, CreateBanToChucDTO updateBanToChucDTO)
        {
            var banToChuc = await _banToChucRepository.GetByMaGiangVienAsync(maGiangVien);
            if (banToChuc == null)
            {
                throw new Exception("Ban tổ chức không tồn tại");
            }

            banToChuc.BatDauNk = updateBanToChucDTO.BatDauNk;
            banToChuc.KetThucNk = updateBanToChucDTO.KetThucNk;

            await _banToChucRepository.UpdateAsync(banToChuc);
            return await GetByMaGiangVienAsync(maGiangVien);
        }

        public async Task<bool> DeleteAsync(string maGiangVien)
        {
            await _banToChucRepository.DeleteAsync(maGiangVien);
            return true;
        }

        private BanToChucDTO MapToDTO(Models.BanToChuc banToChuc)
        {
            if (banToChuc == null) return null;

            return new BanToChucDTO
            {
                MaGiangVien = banToChuc.MaGiangVien,
                HoTen = banToChuc.GiangVien?.TaiKhoan?.HoTen,
                BatDauNk = banToChuc.BatDauNk,
                KetThucNk = banToChuc.KetThucNk,
                TrangThai = banToChuc.TrangThai
            };
        }
    }
}