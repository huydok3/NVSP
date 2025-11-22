using NVSP.DTOs;
using NVSP.Repositories;
using Microsoft.Extensions.Logging;

namespace NVSP.Services
{
    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly ITaiKhoanRepository _taiKhoanRepository;
        private readonly ILogger<TaiKhoanService> _logger;

        public TaiKhoanService(ITaiKhoanRepository taiKhoanRepository, ILogger<TaiKhoanService> logger)
        {
            _taiKhoanRepository = taiKhoanRepository;
            _logger = logger;
        }

        public async Task<TaiKhoanDTO> GetByMaCaNhanAsync(string maCaNhan)
        {
            var taiKhoan = await _taiKhoanRepository.GetByMaCaNhanAsync(maCaNhan);
            return MapToDTO(taiKhoan);
        }

        public async Task<IEnumerable<TaiKhoanDTO>> GetAllAsync()
        {
            var taiKhoans = await _taiKhoanRepository.GetAllAsync();
            return taiKhoans.Select(MapToDTO);
        }

        public async Task<TaiKhoanDTO> CreateAsync(CreateTaiKhoanDTO createTaiKhoanDTO)
        {
            if (await _taiKhoanRepository.MaCaNhanExistsAsync(createTaiKhoanDTO.MaCaNhan))
            {
                throw new Exception("Mã cá nhân đã tồn tại");
            }

            var taiKhoan = new Models.TaiKhoan
            {
                MaCaNhan = createTaiKhoanDTO.MaCaNhan,
                HoTen = createTaiKhoanDTO.HoTen,
                MatKhau = BCrypt.Net.BCrypt.HashPassword(createTaiKhoanDTO.MatKhau), 
                LoaiTk = createTaiKhoanDTO.LoaiTk,
                Email = createTaiKhoanDTO.Email
            };

            await _taiKhoanRepository.CreateAsync(taiKhoan);
            return MapToDTO(taiKhoan);
        }

        public async Task<TaiKhoanDTO> UpdateAsync(string maCaNhan, UpdateTaiKhoanDTO updateTaiKhoanDTO)
        {
            var taiKhoan = await _taiKhoanRepository.GetByMaCaNhanAsync(maCaNhan);
            if (taiKhoan == null)
            {
                throw new Exception("Tài khoản không tồn tại");
            }

            taiKhoan.HoTen = updateTaiKhoanDTO.HoTen;
            taiKhoan.Email = updateTaiKhoanDTO.Email;
            taiKhoan.LoaiTk = updateTaiKhoanDTO.LoaiTk;

            if (!string.IsNullOrEmpty(updateTaiKhoanDTO.MatKhau))
            {
                taiKhoan.MatKhau = BCrypt.Net.BCrypt.HashPassword(updateTaiKhoanDTO.MatKhau);
            }

            await _taiKhoanRepository.UpdateAsync(taiKhoan);
            return MapToDTO(taiKhoan);
        }

        public async Task<bool> DeleteAsync(string maCaNhan)
        {
            var taiKhoan = await _taiKhoanRepository.GetByMaCaNhanAsync(maCaNhan);
            if (taiKhoan == null)
            {
                return false;
            }

            await _taiKhoanRepository.DeleteAsync(taiKhoan.Id);
            return true;
        }

        public async Task<IEnumerable<TaiKhoanDTO>> SearchAsync(string keyword)
        {
            var taiKhoans = await _taiKhoanRepository.SearchAsync(keyword);
            return taiKhoans.Select(MapToDTO);
        }

        private TaiKhoanDTO MapToDTO(Models.TaiKhoan taiKhoan)
        {
            if (taiKhoan == null) return null;

            return new TaiKhoanDTO
            {
                MaCaNhan = taiKhoan.MaCaNhan,
                HoTen = taiKhoan.HoTen,
                LoaiTk = taiKhoan.LoaiTk,
                Email = taiKhoan.Email
            };
        }
    }
}