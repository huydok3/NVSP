using System.Security.Cryptography;
using System.Text;
using NVSP.DTOs;
using NVSP.Repositories;

namespace NVSP.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITaiKhoanRepository _taiKhoanRepository;

        public AuthService(ITaiKhoanRepository taiKhoanRepository)
        {
            _taiKhoanRepository = taiKhoanRepository;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            var taiKhoan = await _taiKhoanRepository.GetByMaCaNhanAsync(loginDTO.MaCaNhan);

            if (taiKhoan == null || !VerifyPassword(loginDTO.MatKhau, taiKhoan.MatKhau))
            {
                return null;
            }

            return new LoginResponseDTO
            {
                Token = GenerateToken(taiKhoan),
                HoTen = taiKhoan.HoTen,
                LoaiTk = taiKhoan.LoaiTk,
                MaCaNhan = taiKhoan.MaCaNhan
            };
        }

        public async Task<bool> ChangePasswordAsync(string maCaNhan, string currentPassword, string newPassword)
        {
            var taiKhoan = await _taiKhoanRepository.GetByMaCaNhanAsync(maCaNhan);

            if (taiKhoan == null || !VerifyPassword(currentPassword, taiKhoan.MatKhau))
            {
                return false;
            }

            taiKhoan.MatKhau = HashPassword(newPassword);
            await _taiKhoanRepository.UpdateAsync(taiKhoan);

            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            var hashOfInput = HashPassword(inputPassword);
            return hashOfInput == storedHash;
        }

        private string GenerateToken(Models.TaiKhoan taiKhoan)
        {
            // Tạm thời trả về token đơn giản
            // Sau này sẽ tích hợp JWT
            return $"{taiKhoan.MaCaNhan}-{DateTime.Now.Ticks}";
        }
    }
}