using NVSP.DTOs;
using NVSP.Repositories;

namespace NVSP.Services
{
    public class BcnKhoaService : IBcnKhoaService
    {
        private readonly IBcnKhoaRepository _bcnKhoaRepository;
        private readonly IGiangVienRepository _giangVienRepository;

        public BcnKhoaService(IBcnKhoaRepository bcnKhoaRepository, IGiangVienRepository giangVienRepository)
        {
            _bcnKhoaRepository = bcnKhoaRepository;
            _giangVienRepository = giangVienRepository;
        }

        public async Task<BcnKhoaDTO> GetByMaGiangVienAsync(string maGiangVien)
        {
            var bcnKhoa = await _bcnKhoaRepository.GetByMaGiangVienAsync(maGiangVien);
            return MapToDTO(bcnKhoa);
        }

        public async Task<IEnumerable<BcnKhoaDTO>> GetAllAsync()
        {
            var bcnKhoas = await _bcnKhoaRepository.GetAllAsync();
            return bcnKhoas.Select(MapToDTO);
        }

        public async Task<IEnumerable<BcnKhoaDTO>> GetByKhoaAsync(string khoa)
        {
            var bcnKhoas = await _bcnKhoaRepository.GetByKhoaAsync(khoa);
            return bcnKhoas.Select(MapToDTO);
        }

        public async Task<BcnKhoaDTO> GetBcnDươngNhiemByKhoaAsync(string khoa)
        {
            var bcnKhoa = await _bcnKhoaRepository.GetBcnDươngNhiemByKhoaAsync(khoa);
            return MapToDTO(bcnKhoa);
        }

        public async Task<BcnKhoaDTO> CreateAsync(CreateBcnKhoaDTO createBcnKhoaDTO)
        {
            // Kiểm tra giảng viên tồn tại
            var giangVien = await _giangVienRepository.GetByMaGiangVienAsync(createBcnKhoaDTO.MaGiangVien);
            if (giangVien == null)
            {
                throw new Exception("Giảng viên không tồn tại");
            }

            // Kiểm tra đã là BCN khoa chưa
            if (await _bcnKhoaRepository.ExistsAsync(createBcnKhoaDTO.MaGiangVien))
            {
                throw new Exception("Giảng viên đã là BCN khoa");
            }

            // Kiểm tra khoa đã có BCN đương nhiệm chưa
            var currentBcn = await _bcnKhoaRepository.GetBcnDươngNhiemByKhoaAsync(createBcnKhoaDTO.Khoa);
            if (currentBcn != null)
            {
                throw new Exception($"Khoa {createBcnKhoaDTO.Khoa} đã có BCN đương nhiệm");
            }

            var bcnKhoa = new Models.BcnKhoa
            {
                MaGiangVien = createBcnKhoaDTO.MaGiangVien,
                Khoa = createBcnKhoaDTO.Khoa,
                BatDauNk = createBcnKhoaDTO.BatDauNk,
                KetThucNk = createBcnKhoaDTO.KetThucNk,
                TrangThai = true
            };

            await _bcnKhoaRepository.CreateAsync(bcnKhoa);
            return await GetByMaGiangVienAsync(createBcnKhoaDTO.MaGiangVien);
        }

        public async Task<BcnKhoaDTO> UpdateAsync(string maGiangVien, CreateBcnKhoaDTO updateBcnKhoaDTO)
        {
            var bcnKhoa = await _bcnKhoaRepository.GetByMaGiangVienAsync(maGiangVien);
            if (bcnKhoa == null)
            {
                throw new Exception("BCN khoa không tồn tại");
            }

            bcnKhoa.Khoa = updateBcnKhoaDTO.Khoa;
            bcnKhoa.BatDauNk = updateBcnKhoaDTO.BatDauNk;
            bcnKhoa.KetThucNk = updateBcnKhoaDTO.KetThucNk;

            await _bcnKhoaRepository.UpdateAsync(bcnKhoa);
            return await GetByMaGiangVienAsync(maGiangVien);
        }

        public async Task<bool> DeleteAsync(string maGiangVien)
        {
            await _bcnKhoaRepository.DeleteAsync(maGiangVien);
            return true;
        }

        private BcnKhoaDTO MapToDTO(Models.BcnKhoa bcnKhoa)
        {
            if (bcnKhoa == null) return null;

            return new BcnKhoaDTO
            {
                MaGiangVien = bcnKhoa.MaGiangVien,
                HoTen = bcnKhoa.GiangVien?.TaiKhoan?.HoTen,
                Khoa = bcnKhoa.Khoa,
                BatDauNk = bcnKhoa.BatDauNk,
                KetThucNk = bcnKhoa.KetThucNk,
                TrangThai = bcnKhoa.TrangThai
            };
        }
    }
}