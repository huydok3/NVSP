using NVSP.DTOs;

namespace NVSP.Services
{
    public interface IAuthorizationService
    {
        Task<CurrentUserDTO> GetCurrentUserAsync(string maCaNhan);
        Task<bool> IsAdminAsync(string maCaNhan);
        Task<bool> IsGiangVienAsync(string maCaNhan);
        Task<bool> IsSinhVienAsync(string maCaNhan);
        Task<bool> CanManageTaiKhoanAsync(string currentUserMaCaNhan, string targetMaCaNhan);
        Task<bool> CanManageSinhVienAsync(string currentUserMaCaNhan);
        Task<bool> CanManageGiangVienAsync(string currentUserMaCaNhan);
        Task<bool> CanManageBcnKhoaAsync(string currentUserMaCaNhan);
        Task<bool> CanManageBanToChucAsync(string currentUserMaCaNhan);
        Task<bool> CanManageCanBoLopAsync(string currentUserMaCaNhan);
    }
}