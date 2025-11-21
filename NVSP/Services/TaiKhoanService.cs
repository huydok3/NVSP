using NVSP.DTOs;
using NVSP.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace NVSP.Services
{
    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly ITaiKhoanRepository _taiKhoanRepository;

        public TaiKhoanService(ITaiKhoanRepository taiKhoanRepository)
        {
            _taiKhoanRepository = taiKhoanRepository;
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
                MatKhau = HashPassword(createTaiKhoanDTO.MatKhau),
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

            await _taiKhoanRepository.UpdateAsync(taiKhoan);
            return MapToDTO(taiKhoan);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _taiKhoanRepository.DeleteAsync(id);
            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
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