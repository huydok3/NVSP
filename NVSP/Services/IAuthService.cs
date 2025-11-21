using NVSP.DTOs;

namespace NVSP.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO);
        Task<bool> ChangePasswordAsync(string maCaNhan, string currentPassword, string newPassword);
    }
}