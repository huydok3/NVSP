using NVSP.DTOs;
using NVSP.Repositories;

namespace NVSP.Services
{
    public class SinhVienService : ISinhVienService
    {
        private readonly ISinhVienRepository _sinhVienRepository;
        private readonly ITaiKhoanRepository _taiKhoanRepository;

        public SinhVienService(ISinhVienRepository sinhVienRepository, ITaiKhoanRepository taiKhoanRepository)
        {
            _sinhVienRepository = sinhVienRepository;
            _taiKhoanRepository = taiKhoanRepository;
        }

        public async Task<SinhVienDTO> GetByMaSinhVienAsync(string maSinhVien)
        {
            var sinhVien = await _sinhVienRepository.GetByMaSinhVienAsync(maSinhVien);
            return MapToDTO(sinhVien);
        }

        public async Task<IEnumerable<SinhVienDTO>> GetAllAsync()
        {
            var sinhViens = await _sinhVienRepository.GetAllAsync();
            return sinhViens.Select(MapToDTO);
        }

        public async Task<SinhVienDTO> CreateAsync(CreateSinhVienDTO createSinhVienDTO)
        {
            // Kiểm tra tài khoản đã tồn tại chưa
            if (await _taiKhoanRepository.MaCaNhanExistsAsync(createSinhVienDTO.MaSinhVien))
            {
                throw new Exception("Mã sinh viên đã tồn tại trong hệ thống");
            }

            // Tạo tài khoản trước
            var taiKhoan = new Models.TaiKhoan
            {
                MaCaNhan = createSinhVienDTO.MaSinhVien,
                HoTen = createSinhVienDTO.HoTen,
                MatKhau = BCrypt.Net.BCrypt.HashPassword(createSinhVienDTO.MatKhau ?? "123456"),
                LoaiTk = "Sinh viên",
                Email = createSinhVienDTO.Email
            };

            await _taiKhoanRepository.CreateAsync(taiKhoan);

            // Tạo sinh viên
            var sinhVien = new Models.SinhVien
            {
                MaSinhVien = createSinhVienDTO.MaSinhVien,
                NienKhoa = createSinhVienDTO.NienKhoa,
                Lop = createSinhVienDTO.Lop,
                Nganh = createSinhVienDTO.Nganh,
                Khoa = createSinhVienDTO.Khoa
            };

            await _sinhVienRepository.CreateAsync(sinhVien);
            return await GetByMaSinhVienAsync(createSinhVienDTO.MaSinhVien);
        }

        public async Task<SinhVienDTO> UpdateAsync(string maSinhVien, UpdateSinhVienDTO updateSinhVienDTO)
        {
            var sinhVien = await _sinhVienRepository.GetByMaSinhVienAsync(maSinhVien);
            if (sinhVien == null)
            {
                throw new Exception("Sinh viên không tồn tại");
            }

            // Cập nhật thông tin sinh viên
            sinhVien.NienKhoa = updateSinhVienDTO.NienKhoa;
            sinhVien.Lop = updateSinhVienDTO.Lop;
            sinhVien.Nganh = updateSinhVienDTO.Nganh;
            sinhVien.Khoa = updateSinhVienDTO.Khoa;

            // Cập nhật thông tin tài khoản
            if (sinhVien.TaiKhoan != null)
            {
                sinhVien.TaiKhoan.HoTen = updateSinhVienDTO.HoTen;
                sinhVien.TaiKhoan.Email = updateSinhVienDTO.Email;
            }

            await _sinhVienRepository.UpdateAsync(sinhVien);
            return await GetByMaSinhVienAsync(maSinhVien);
        }

        public async Task<bool> DeleteAsync(string maSinhVien)
        {
            await _sinhVienRepository.DeleteAsync(maSinhVien);
            return true;
        }

        private SinhVienDTO MapToDTO(Models.SinhVien sinhVien)
        {
            if (sinhVien == null) return null;

            return new SinhVienDTO
            {
                MaSinhVien = sinhVien.MaSinhVien,
                HoTen = sinhVien.TaiKhoan?.HoTen,
                NienKhoa = sinhVien.NienKhoa,
                Lop = sinhVien.Lop,
                Nganh = sinhVien.Nganh,
                Khoa = sinhVien.Khoa,
                Email = sinhVien.TaiKhoan?.Email
            };
        }
    }

    public class UpdateSinhVienDTO
    {
        public string HoTen { get; set; }
        public string NienKhoa { get; set; }
        public string Lop { get; set; }
        public string Nganh { get; set; }
        public string Khoa { get; set; }
        public string Email { get; set; }
    }
}