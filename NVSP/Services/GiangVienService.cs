using NVSP.DTOs;
using NVSP.Repositories;

namespace NVSP.Services
{
    public class GiangVienService : IGiangVienService
    {
        private readonly IGiangVienRepository _giangVienRepository;
        private readonly ITaiKhoanRepository _taiKhoanRepository;

        public GiangVienService(IGiangVienRepository giangVienRepository, ITaiKhoanRepository taiKhoanRepository)
        {
            _giangVienRepository = giangVienRepository;
            _taiKhoanRepository = taiKhoanRepository;
        }

        public async Task<GiangVienDTO> GetByMaGiangVienAsync(string maGiangVien)
        {
            var giangVien = await _giangVienRepository.GetByMaGiangVienAsync(maGiangVien);
            return MapToDTO(giangVien);
        }

        public async Task<IEnumerable<GiangVienDTO>> GetAllAsync()
        {
            var giangViens = await _giangVienRepository.GetAllAsync();
            return giangViens.Select(MapToDTO);
        }

        public async Task<GiangVienDTO> CreateAsync(CreateGiangVienDTO createGiangVienDTO)
        {
            if (await _taiKhoanRepository.MaCaNhanExistsAsync(createGiangVienDTO.MaGiangVien))
            {
                throw new Exception("Mã giảng viên đã tồn tại trong hệ thống");
            }

            // Tạo tài khoản
            var taiKhoan = new Models.TaiKhoan
            {
                MaCaNhan = createGiangVienDTO.MaGiangVien,
                HoTen = createGiangVienDTO.HoTen,
                MatKhau = BCrypt.Net.BCrypt.HashPassword(createGiangVienDTO.MatKhau ?? "123456"),
                LoaiTk = "Giảng viên",
                Email = createGiangVienDTO.Email
            };

            await _taiKhoanRepository.CreateAsync(taiKhoan);

            // Tạo giảng viên
            var giangVien = new Models.GiangVien
            {
                MaGiangVien = createGiangVienDTO.MaGiangVien,
                Khoa = createGiangVienDTO.Khoa
            };

            await _giangVienRepository.CreateAsync(giangVien);
            return await GetByMaGiangVienAsync(createGiangVienDTO.MaGiangVien);
        }

        public async Task<GiangVienDTO> UpdateAsync(string maGiangVien, UpdateGiangVienDTO updateGiangVienDTO)
        {
            var giangVien = await _giangVienRepository.GetByMaGiangVienAsync(maGiangVien);
            if (giangVien == null)
            {
                throw new Exception("Giảng viên không tồn tại");
            }

            giangVien.Khoa = updateGiangVienDTO.Khoa;

            // Cập nhật thông tin tài khoản
            if (giangVien.TaiKhoan != null)
            {
                giangVien.TaiKhoan.HoTen = updateGiangVienDTO.HoTen;
                giangVien.TaiKhoan.Email = updateGiangVienDTO.Email;
            }

            await _giangVienRepository.UpdateAsync(giangVien);
            return await GetByMaGiangVienAsync(maGiangVien);
        }

        public async Task<bool> DeleteAsync(string maGiangVien)
        {
            await _giangVienRepository.DeleteAsync(maGiangVien);
            return true;
        }

        private GiangVienDTO MapToDTO(Models.GiangVien giangVien)
        {
            if (giangVien == null) return null;

            return new GiangVienDTO
            {
                MaGiangVien = giangVien.MaGiangVien,
                HoTen = giangVien.TaiKhoan?.HoTen,
                Khoa = giangVien.Khoa,
                Email = giangVien.TaiKhoan?.Email
            };
        }
    }

    public class UpdateGiangVienDTO
    {
        public string HoTen { get; set; }
        public string Khoa { get; set; }
        public string Email { get; set; }
    }
}