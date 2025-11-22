using NVSP.DTOs;
using NVSP.Helpers;
using NVSP.Repositories;

namespace NVSP.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ITaiKhoanRepository _taiKhoanRepository;

        public AuthorizationService(ITaiKhoanRepository taiKhoanRepository)
        {
            _taiKhoanRepository = taiKhoanRepository;
        }

        public async Task<CurrentUserDTO> GetCurrentUserAsync(string maCaNhan)
        {
            if (string.IsNullOrEmpty(maCaNhan))
                return null;

            var taiKhoan = await _taiKhoanRepository.GetByMaCaNhanAsync(maCaNhan);
            if (taiKhoan == null) return null;

            return new CurrentUserDTO
            {
                MaCaNhan = taiKhoan.MaCaNhan,
                HoTen = taiKhoan.HoTen,
                LoaiTk = taiKhoan.LoaiTk,
                Email = taiKhoan.Email
            };
        }

        public async Task<bool> IsAdminAsync(string maCaNhan)
        {
            var user = await GetCurrentUserAsync(maCaNhan);
            return user?.LoaiTk == UserRoles.Admin;
        }

        public async Task<bool> IsGiangVienAsync(string maCaNhan)
        {
            var user = await GetCurrentUserAsync(maCaNhan);
            return user?.LoaiTk == UserRoles.GiangVien;
        }

        public async Task<bool> IsSinhVienAsync(string maCaNhan)
        {
            var user = await GetCurrentUserAsync(maCaNhan);
            return user?.LoaiTk == UserRoles.SinhVien;
        }

        public async Task<bool> CanManageTaiKhoanAsync(string currentUserMaCaNhan, string targetMaCaNhan)
        {
            if (await IsAdminAsync(currentUserMaCaNhan))
                return true;

            return currentUserMaCaNhan == targetMaCaNhan;
        }

        public async Task<bool> CanManageSinhVienAsync(string currentUserMaCaNhan)
        {
            return await IsAdminAsync(currentUserMaCaNhan) ||
                   await IsGiangVienAsync(currentUserMaCaNhan);
        }

        public async Task<bool> CanManageGiangVienAsync(string currentUserMaCaNhan)
        {
            return await IsAdminAsync(currentUserMaCaNhan);
        }

        public async Task<bool> CanManageBcnKhoaAsync(string currentUserMaCaNhan)
        {
            return await IsAdminAsync(currentUserMaCaNhan);
        }

        public async Task<bool> CanManageBanToChucAsync(string currentUserMaCaNhan)
        {
            return await IsAdminAsync(currentUserMaCaNhan);
        }

        public async Task<bool> CanManageCanBoLopAsync(string currentUserMaCaNhan)
        {
            return await IsAdminAsync(currentUserMaCaNhan);
        }
    }
}