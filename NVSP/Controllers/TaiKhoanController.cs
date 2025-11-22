using Microsoft.AspNetCore.Mvc;
using NVSP.Attributes;
using NVSP.DTOs;
using NVSP.Helpers;
using NVSP.Services;

namespace NVSP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AuthorizeRole(UserRoles.Admin, UserRoles.GiangVien, UserRoles.SinhVien)]
    public class TaiKhoanController : ControllerBase
    {
        private readonly ITaiKhoanService _taiKhoanService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ILogger<TaiKhoanController> _logger;

        public TaiKhoanController(
            ITaiKhoanService taiKhoanService,
            IAuthorizationService authorizationService,
            ILogger<TaiKhoanController> logger)
        {
            _taiKhoanService = taiKhoanService;
            _authorizationService = authorizationService;
            _logger = logger;
        }

        [HttpGet]
        [AuthorizeRole(UserRoles.Admin, UserRoles.GiangVien)]
        public async Task<ActionResult<IEnumerable<TaiKhoanDTO>>> GetAll()
        {
            try
            {
                var taiKhoans = await _taiKhoanService.GetAllAsync();
                return Ok(taiKhoans);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all accounts");
                return StatusCode(500, new { Message = "Đã xảy ra lỗi khi lấy danh sách tài khoản", Error = ex.Message });
            }
        }

        [HttpGet("{maCaNhan}")]
        [AuthorizeRole(UserRoles.Admin, UserRoles.GiangVien, UserRoles.SinhVien)]
        public async Task<ActionResult<TaiKhoanDTO>> GetByMaCaNhan(string maCaNhan)
        {
            try
            {
                var currentUserMaCaNhan = User.FindFirst("MaCaNhan")?.Value;
                var currentUserRole = User.FindFirst("LoaiTk")?.Value;

                if (currentUserRole == UserRoles.SinhVien)
                {
                    if (maCaNhan != currentUserMaCaNhan)
                    {
                        return StatusCode(403, new { Message = "Bạn chỉ được xem thông tin của chính mình" });
                    }
                }

                var taiKhoan = await _taiKhoanService.GetByMaCaNhanAsync(maCaNhan);
                if (taiKhoan == null)
                {
                    return NotFound(new { Message = $"Không tìm thấy tài khoản với mã cá nhân: {maCaNhan}" });
                }

                return Ok(taiKhoan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting account by MaCaNhan: {MaCaNhan}", maCaNhan);
                return StatusCode(500, new { Message = "Đã xảy ra lỗi khi lấy thông tin tài khoản", Error = ex.Message });
            }
        }

        [HttpPost]
        [AuthorizeRole(UserRoles.Admin, UserRoles.GiangVien)]
        public async Task<ActionResult<TaiKhoanDTO>> Create(CreateTaiKhoanDTO createTaiKhoanDTO)
        {
            try
            {
                var currentUserMaCaNhan = User.FindFirst("MaCaNhan")?.Value;

                if (createTaiKhoanDTO.LoaiTk == UserRoles.GiangVien ||
                    createTaiKhoanDTO.LoaiTk == UserRoles.BCNKhoa ||
                    createTaiKhoanDTO.LoaiTk == UserRoles.BanToChuc)
                {
                    if (!await _authorizationService.IsAdminAsync(currentUserMaCaNhan))
                    {
                        return StatusCode(403, new { Message = "Chỉ Admin được tạo tài khoản Giảng viên và phân quyền" });
                    }
                }

                var taiKhoan = await _taiKhoanService.CreateAsync(createTaiKhoanDTO);
                return CreatedAtAction(nameof(GetByMaCaNhan), new { maCaNhan = taiKhoan.MaCaNhan }, taiKhoan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating account: {@CreateDTO}", createTaiKhoanDTO);
                return BadRequest(new { Message = "Không thể tạo tài khoản", Error = ex.Message });
            }
        }

        [HttpPut("{maCaNhan}")]
        [AuthorizeRole(UserRoles.Admin, UserRoles.GiangVien, UserRoles.SinhVien)]
        public async Task<ActionResult<TaiKhoanDTO>> Update(string maCaNhan, UpdateTaiKhoanDTO updateTaiKhoanDTO)
        {
            try
            {
                var currentUserMaCaNhan = User.FindFirst("MaCaNhan")?.Value;
                var currentUserRole = User.FindFirst("LoaiTk")?.Value;

                if (currentUserRole == UserRoles.SinhVien)
                {
                    return StatusCode(403, new { Message = "Sinh viên không được phép sửa thông tin tài khoản" });
                }

                var taiKhoan = await _taiKhoanService.UpdateAsync(maCaNhan, updateTaiKhoanDTO);
                return Ok(taiKhoan);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Account not found: {MaCaNhan}", maCaNhan);
                return NotFound(new { Message = $"Không tìm thấy tài khoản với mã cá nhân: {maCaNhan}" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating account {MaCaNhan}: {@UpdateDTO}", maCaNhan, updateTaiKhoanDTO);
                return BadRequest(new { Message = "Không thể cập nhật tài khoản", Error = ex.Message });
            }
        }

        [HttpDelete("{maCaNhan}")]
        [AuthorizeRole(UserRoles.Admin)]
        public async Task<ActionResult> Delete(string maCaNhan)
        {
            try
            {
                var currentUserMaCaNhan = User.FindFirst("MaCaNhan")?.Value;

                if (maCaNhan == currentUserMaCaNhan)
                {
                    return BadRequest(new { Message = "Không thể xóa tài khoản của chính mình" });
                }

                var success = await _taiKhoanService.DeleteAsync(maCaNhan); 
                if (!success)
                {
                    return NotFound(new { Message = $"Không tìm thấy tài khoản với mã cá nhân: {maCaNhan}" });
                }
                return Ok(new { Message = $"Đã xóa tài khoản {maCaNhan} thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting account: {MaCaNhan}", maCaNhan);
                return StatusCode(500, new { Message = "Đã xảy ra lỗi khi xóa tài khoản", Error = ex.Message });
            }
        }

        [HttpGet("search")]
        [AuthorizeRole(UserRoles.Admin, UserRoles.GiangVien)]
        public async Task<ActionResult<IEnumerable<TaiKhoanDTO>>> Search([FromQuery] string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    var allResults = await _taiKhoanService.GetAllAsync();
                    return Ok(allResults);
                }

                var results = await _taiKhoanService.SearchAsync(keyword);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching accounts with keyword: {Keyword}", keyword);
                return StatusCode(500, new { Message = "Đã xảy ra lỗi khi tìm kiếm", Error = ex.Message });
            }
        }
    }
}