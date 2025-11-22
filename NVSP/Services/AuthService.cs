using NVSP.DTOs;
using NVSP.Repositories;
using Microsoft.Extensions.Logging;

namespace NVSP.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITaiKhoanRepository _taiKhoanRepository;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            ITaiKhoanRepository taiKhoanRepository,
            IJwtService jwtService,
            ILogger<AuthService> logger)
        {
            _taiKhoanRepository = taiKhoanRepository;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(loginDTO.MaCaNhan) || string.IsNullOrWhiteSpace(loginDTO.MatKhau))
                {
                    return null;
                }

                var taiKhoan = await _taiKhoanRepository.GetByMaCaNhanAsync(loginDTO.MaCaNhan);

                if (taiKhoan == null)
                {
                    _logger.LogWarning("Login failed - Account not found: {MaCaNhan}", loginDTO.MaCaNhan);
                    return null;
                }

                if (!BCrypt.Net.BCrypt.Verify(loginDTO.MatKhau, taiKhoan.MatKhau))
                {
                    _logger.LogWarning("Login failed - Invalid password for: {MaCaNhan}", loginDTO.MaCaNhan);
                    return null;
                }

                var token = _jwtService.GenerateToken(taiKhoan);

                _logger.LogInformation("Login successful: {MaCaNhan}", loginDTO.MaCaNhan);

                return new LoginResponseDTO
                {
                    Token = token,
                    HoTen = taiKhoan.HoTen,
                    LoaiTk = taiKhoan.LoaiTk,
                    MaCaNhan = taiKhoan.MaCaNhan,
                    Email = taiKhoan.Email
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for MaCaNhan: {MaCaNhan}", loginDTO.MaCaNhan);
                return null;
            }
        }

        public async Task<bool> ChangePasswordAsync(string maCaNhan, string currentPassword, string newPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maCaNhan) ||
                    string.IsNullOrWhiteSpace(currentPassword) ||
                    string.IsNullOrWhiteSpace(newPassword))
                {
                    return false;
                }

                var taiKhoan = await _taiKhoanRepository.GetByMaCaNhanAsync(maCaNhan);

                if (taiKhoan == null)
                {
                    _logger.LogWarning("Change password failed - Account not found: {MaCaNhan}", maCaNhan);
                    return false;
                }

                if (!BCrypt.Net.BCrypt.Verify(currentPassword, taiKhoan.MatKhau))
                {
                    _logger.LogWarning("Change password failed - Invalid current password for: {MaCaNhan}", maCaNhan);
                    return false;
                }

                taiKhoan.MatKhau = BCrypt.Net.BCrypt.HashPassword(newPassword);

                await _taiKhoanRepository.UpdateAsync(taiKhoan);

                _logger.LogInformation("Password changed successfully for: {MaCaNhan}", maCaNhan);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password for MaCaNhan: {MaCaNhan}", maCaNhan);
                return false;
            }
        }
    }
}